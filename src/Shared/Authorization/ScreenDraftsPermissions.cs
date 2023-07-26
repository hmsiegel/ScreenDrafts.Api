namespace ScreenDrafts.Api.Shared.Authorization;
public static class ScreenDraftsAction
{
    public const string View = nameof(View);
    public const string Search = nameof(Search);
    public const string Create = nameof(Create);
    public const string Update = nameof(Update);
    public const string Delete = nameof(Delete);
    public const string Export = nameof(Export);
    public const string Generate = nameof(Generate);
    public const string Clean = nameof(Clean);
    public const string UpgradeSubscription = nameof(UpgradeSubscription);
}

public static class ScreenDraftsResource
{
    public const string Hangfire = nameof(Hangfire);
    public const string Users = nameof(Users);
    public const string UserRoles = nameof(UserRoles);
    public const string Roles = nameof(Roles);
    public const string RoleClaims = nameof(RoleClaims);
    public const string Drafts = nameof(Drafts);
    public const string Drafters = nameof(Drafters);
    public const string Movies = nameof(Movies);
    public const string Hosts = nameof(Hosts);
    public const string Imdb = nameof(Imdb);
    public const string CrewMembers = nameof(CrewMembers);
    public const string CastMembers = nameof(CastMembers);
}

public static class ScreenDraftsPermissions
{
    private static readonly ScreenDraftsPermission[] _all =
    {
        new(Description: "View Hangfire", Action: ScreenDraftsAction.View, Resource: ScreenDraftsResource.Hangfire),
        new(Description: "View Users", Action: ScreenDraftsAction.View, Resource: ScreenDraftsResource.Users, IsBasic: true, IsHost: true, IsDrafter: true),
        new(Description: "Search Users", Action: ScreenDraftsAction.Search, Resource: ScreenDraftsResource.Users, IsBasic: true, IsHost: true, IsDrafter: true),
        new(Description: "Create Users", Action: ScreenDraftsAction.Create, Resource: ScreenDraftsResource.Users),
        new(Description: "Update Users", Action: ScreenDraftsAction.Update, Resource: ScreenDraftsResource.Users),
        new(Description: "Delete Users", Action: ScreenDraftsAction.Delete, Resource: ScreenDraftsResource.Users),
        new(Description: "Export Users", Action: ScreenDraftsAction.Export, Resource: ScreenDraftsResource.Users),
        new(Description: "View UserRoles", Action: ScreenDraftsAction.View, Resource: ScreenDraftsResource.UserRoles),
        new(Description: "Update UserRoles", Action: ScreenDraftsAction.Update, Resource: ScreenDraftsResource.UserRoles),
        new(Description: "View Roles", Action: ScreenDraftsAction.View, Resource: ScreenDraftsResource.Roles),
        new(Description: "Create Roles", Action: ScreenDraftsAction.Create, Resource: ScreenDraftsResource.Roles),
        new(Description: "Update Roles", Action: ScreenDraftsAction.Update, Resource: ScreenDraftsResource.Roles),
        new(Description: "Delete Roles", Action: ScreenDraftsAction.Delete, Resource: ScreenDraftsResource.Roles),
        new(Description: "View RoleClaims", Action: ScreenDraftsAction.View, Resource: ScreenDraftsResource.RoleClaims),
        new(Description: "Update RoleClaims", Action: ScreenDraftsAction.Update, Resource: ScreenDraftsResource.RoleClaims),
        new(Description: "Create Drafts", Action : ScreenDraftsAction.Create, Resource : ScreenDraftsResource.Drafts, IsHost: true),
        new(Description: "View Drafts", Action: ScreenDraftsAction.View, Resource: ScreenDraftsResource.Drafts, IsBasic: true, IsHost: true, IsDrafter: true),
        new(Description: "Update Drafts", Action : ScreenDraftsAction.Update, Resource : ScreenDraftsResource.Drafts, IsHost: true),
        new(Description: "Search Drafts", Action: ScreenDraftsAction.Search, Resource: ScreenDraftsResource.Drafts, IsBasic: true, IsHost: true, IsDrafter: true),
        new(Description: "Create Drafters", Action : ScreenDraftsAction.Create, Resource : ScreenDraftsResource.Drafters, IsHost: true),
        new(Description: "View Drafters", Action: ScreenDraftsAction.View, Resource: ScreenDraftsResource.Drafters, IsBasic: true, IsHost: true, IsDrafter: true),
        new(Description: "Update Drafters", Action : ScreenDraftsAction.Update, Resource : ScreenDraftsResource.Drafters, IsHost: true, IsDrafter: true),
        new(Description: "Search Drafters", Action: ScreenDraftsAction.Search, Resource: ScreenDraftsResource.Drafters, IsBasic: true, IsHost: true, IsDrafter: true),
        new(Description: "Create Hosts", Action : ScreenDraftsAction.Create, Resource : ScreenDraftsResource.Hosts, IsHost: true),
        new(Description: "View Hosts", Action: ScreenDraftsAction.View, Resource: ScreenDraftsResource.Hosts, IsBasic: true, IsHost: true, IsDrafter: true),
        new(Description: "Update Hosts", Action : ScreenDraftsAction.Update, Resource : ScreenDraftsResource.Hosts, IsHost: true, IsDrafter: true),
        new(Description: "Search Hosts", Action: ScreenDraftsAction.Search, Resource: ScreenDraftsResource.Hosts, IsBasic: true, IsHost: true, IsDrafter: true),
        new(Description: "Search IMDB", Action: ScreenDraftsAction.Search, Resource: ScreenDraftsResource.Imdb, IsHost: true, IsDrafter: true),
        new(Description: "Create Movies", Action : ScreenDraftsAction.Create, Resource : ScreenDraftsResource.Movies, IsHost: true, IsDrafter: true),
        new(Description: "View Movies", Action: ScreenDraftsAction.View, Resource: ScreenDraftsResource.Movies, IsBasic: true, IsHost: true, IsDrafter: true),
        new(Description: "Update Movies", Action : ScreenDraftsAction.Update, Resource : ScreenDraftsResource.Movies, IsHost: true, IsDrafter: true),
        new(Description: "Search Movies", Action: ScreenDraftsAction.Search, Resource: ScreenDraftsResource.Movies, IsBasic: true, IsHost: true, IsDrafter: true),
        new(Description: "Create Cast Member", Action : ScreenDraftsAction.Create, Resource : ScreenDraftsResource.CastMembers, IsHost: true, IsDrafter: true),
        new(Description: "View Cast Member", Action: ScreenDraftsAction.View, Resource: ScreenDraftsResource.CastMembers, IsBasic: true, IsHost: true, IsDrafter: true),
        new(Description: "Update Cast Member", Action : ScreenDraftsAction.Update, Resource : ScreenDraftsResource.CastMembers, IsHost: true, IsDrafter: true),
        new(Description: "Search Cast Member", Action: ScreenDraftsAction.Search, Resource: ScreenDraftsResource.CastMembers, IsBasic: true, IsHost: true, IsDrafter: true),
        new(Description: "Create Crew Member", Action : ScreenDraftsAction.Create, Resource : ScreenDraftsResource.CrewMembers, IsHost: true, IsDrafter: true),
        new(Description: "View Crew Member", Action: ScreenDraftsAction.View, Resource: ScreenDraftsResource.CrewMembers, IsBasic: true, IsHost: true, IsDrafter: true),
        new(Description: "Update Crew Member", Action : ScreenDraftsAction.Update, Resource : ScreenDraftsResource.CrewMembers, IsHost: true, IsDrafter: true),
        new(Description: "Search Crew Member", Action: ScreenDraftsAction.Search, Resource: ScreenDraftsResource.CrewMembers, IsBasic: true, IsHost: true, IsDrafter: true),
    };

    public static IReadOnlyList<ScreenDraftsPermission> All { get; } = new ReadOnlyCollection<ScreenDraftsPermission>(list: _all);
    public static IReadOnlyList<ScreenDraftsPermission> Admin { get; } = new ReadOnlyCollection<ScreenDraftsPermission>(list: _all.Where(predicate: p => p.IsAdmin).ToArray());
    public static IReadOnlyList<ScreenDraftsPermission> Basic { get; } = new ReadOnlyCollection<ScreenDraftsPermission>(list: _all.Where(predicate: p => p.IsBasic).ToArray());
    public static IReadOnlyList<ScreenDraftsPermission> Host { get; } = new ReadOnlyCollection<ScreenDraftsPermission>(list: _all.Where(predicate: p => p.IsHost).ToArray());
    public static IReadOnlyList<ScreenDraftsPermission> Drafter { get; } = new ReadOnlyCollection<ScreenDraftsPermission>(list: _all.Where(predicate: p => p.IsDrafter).ToArray());
}

public record ScreenDraftsPermission(string Description, string Action, string Resource, bool IsBasic = false, bool IsAdmin = true, bool IsHost = false, bool IsDrafter = false)
{
    public string Name => NameFor(Action, Resource);
    public static string NameFor(string action, string resource) => $"Permissions.{resource}.{action}";
}
