﻿namespace ScreenDrafts.Api.Persistence;
public class ApplicationDbContext : IdentityDbContext<ApplicationUser,
    ApplicationRole,
    string,
    IdentityUserClaim<string>,
    IdentityUserRole<string>,
    IdentityUserLogin<string>,
    ApplicationRoleClaim,
    IdentityUserToken<string>>, IUnitOfWork
{
    private readonly ICurrentUserService _currentUser;
    private readonly ISerializerService _serializer;
    private readonly DatabaseSettings _dbSettings;
    private readonly IEventPublisher _events;
    private readonly IDateTimeProvider _dateTime;

    public ApplicationDbContext(
        ICurrentUserService currentUser,
        ISerializerService serializer,
        IOptions<DatabaseSettings> dbSettings,
        IEventPublisher events,
        DbContextOptions<ApplicationDbContext> options,
        IDateTimeProvider dateTime)
        : base(options)
    {
        _currentUser = currentUser;
        _serializer = serializer;
        _dbSettings = dbSettings.Value;
        _events = events;
        _dateTime = dateTime;
    }

    public DbSet<Trail> AuditTrails => Set<Trail>();
    public DbSet<Host> Hosts => Set<Host>();
    public DbSet<Draft> Drafts => Set<Draft>();
    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<Drafter> Drafters => Set<Drafter>();
    public DbSet<MovieCastMember> MovieCastMembers => Set<MovieCastMember>();
    public DbSet<MovieCrewMember> MovieCrewMembers => Set<MovieCrewMember>();
    public DbSet<CrewMember> CrewMember => Set<CrewMember>();
    public DbSet<CastMember> CastMember => Set<CastMember>();

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var auditEntries = HandleAuditingBeforeSaveChanges(_currentUser.GetUserId());

        var result = await base.SaveChangesAsync(cancellationToken);

        await HandleAuditingAfterSaveChanges(auditEntries, cancellationToken);

        await SendDomainEventsAsync();

        return result;
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.ConfigureSmartEnum();
        base.ConfigureConventions(configurationBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseDatabase(_dbSettings.DBProvider, _dbSettings.ConnectionString);
    }

    private async Task SendDomainEventsAsync()
    {
        var entitiesWithEvents = ChangeTracker.Entries<IEntity>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Count > 0)
            .ToArray();

        foreach (var entity in entitiesWithEvents)
        {
            var domainEvents = entity.DomainEvents.ToArray();
            entity.DomainEvents.Clear();
            foreach (var domainEvent in domainEvents)
            {
                await _events.PublishAsync(domainEvent);
            }
        }
    }

    private Task HandleAuditingAfterSaveChanges(List<AuditTrail> trailEntries, CancellationToken cancellationToken = new())
    {
        if (trailEntries == null || trailEntries.Count == 0)
        {
            return Task.CompletedTask;
        }

        foreach (var entry in trailEntries)
        {
            foreach (var prop in entry.TemporaryProperties!)
            {
                if (prop.Metadata.IsPrimaryKey())
                {
                    entry.KeyValues![prop.Metadata.Name] = prop.CurrentValue;
                }
                else
                {
                    entry.NewValues![prop.Metadata.Name] = prop.CurrentValue;
                }
            }

            AuditTrails.Add(entry.ToAuditTrail());
        }

        return SaveChangesAsync(cancellationToken);
    }

    private List<AuditTrail> HandleAuditingBeforeSaveChanges(DefaultIdType userId)
    {
        foreach (var entry in ChangeTracker.Entries<IAuditableEntity>().ToList())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = userId;
                    entry.Entity.ModifiedBy = userId;
                    entry.Entity.CreatedOnUtc = _dateTime.UtcNow;
                    entry.Entity.ModifiedOnUtc = _dateTime.UtcNow;
                    break;

                case EntityState.Modified:
                    entry.Entity.ModifiedOnUtc = _dateTime.UtcNow;
                    entry.Entity.ModifiedBy = userId;
                    break;

                case EntityState.Deleted:
                    if (entry.Entity is ISoftDelete softDelete)
                    {
                        softDelete.DeletedBy = userId;
                        softDelete.DeletedOn = _dateTime.UtcNow;
                        entry.State = EntityState.Modified;
                    }

                    break;
            }
        }

        ChangeTracker.DetectChanges();

        var trailEntries = new List<AuditTrail>();
        foreach (var entry in ChangeTracker.Entries<IAuditableEntity>()
            .Where(e => e.State is EntityState.Added or EntityState.Deleted or EntityState.Modified)
            .ToList())
        {
            var trailEntry = new AuditTrail(entry, _serializer)
            {
                TableName = entry.Entity.GetType().Name,
                UserId = userId,
            };
            trailEntries.Add(trailEntry);
            foreach (var property in entry.Properties)
            {
                if (property.IsTemporary)
                {
                    trailEntry.TemporaryProperties?.Add(property);
                    continue;
                }

                string propertyName = property.Metadata.Name;
                if (property.Metadata.IsPrimaryKey())
                {
                    trailEntry.KeyValues[propertyName] = property.CurrentValue;
                    continue;
                }

                switch (entry.State)
                {
                    case EntityState.Added:
                        trailEntry.TrailType = TrailType.Create;
                        trailEntry.NewValues![propertyName] = property.CurrentValue;
                        break;

                    case EntityState.Deleted:
                        trailEntry.TrailType = TrailType.Delete;
                        trailEntry.OldValues![propertyName] = property.OriginalValue;
                        break;

                    case EntityState.Modified:
                        if (property.IsModified && entry.Entity is ISoftDelete && property.OriginalValue == null && property.CurrentValue != null)
                        {
                            trailEntry.ChangedColumns.Add(propertyName);
                            trailEntry.TrailType = TrailType.Delete;
                            trailEntry.OldValues![propertyName] = property.OriginalValue;
                            trailEntry.NewValues![propertyName] = property.CurrentValue;
                        }
                        else if (property.IsModified && property.OriginalValue?.Equals(property.CurrentValue) == false)
                        {
                            trailEntry.ChangedColumns.Add(propertyName);
                            trailEntry.TrailType = TrailType.Update;
                            trailEntry.OldValues![propertyName] = property.OriginalValue;
                            trailEntry.NewValues![propertyName] = property.CurrentValue;
                        }

                        break;
                }
            }
        }

        foreach (var auditEntry in trailEntries.Where(e => !e.HasTemporaryProperties))
        {
            AuditTrails.Add(auditEntry.ToAuditTrail());
        }

        return trailEntries.Where(e => e.HasTemporaryProperties).ToList();
    }
}
