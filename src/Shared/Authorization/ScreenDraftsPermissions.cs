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
}

public static class ScreenDraftsPermissions
{
    private static readonly ScreenDraftsPermission[] _all =
    {
        new(Description: "View Hangfire", Action: ScreenDraftsAction.View, Resource: ScreenDraftsResource.Hangfire, IsAdmin: true),
        new(Description: "View Users", Action: ScreenDraftsAction.View, Resource: ScreenDraftsResource.Users, IsAdmin: true),
        new(Description: "Search Users", Action: ScreenDraftsAction.Search, Resource: ScreenDraftsResource.Users, IsAdmin: true, IsHost: true),
        new(Description: "Create Users", Action: ScreenDraftsAction.Create, Resource: ScreenDraftsResource.Users, IsAdmin: true),
        new(Description: "Update Users", Action: ScreenDraftsAction.Update, Resource: ScreenDraftsResource.Users, IsAdmin: true),
        new(Description: "Delete Users", Action: ScreenDraftsAction.Delete, Resource: ScreenDraftsResource.Users, IsAdmin: true),
        new(Description: "Export Users", Action: ScreenDraftsAction.Export, Resource: ScreenDraftsResource.Users, IsAdmin: true),
        new(Description: "View UserRoles", Action: ScreenDraftsAction.View, Resource: ScreenDraftsResource.UserRoles, IsAdmin: true),
        new(Description: "Update UserRoles", Action: ScreenDraftsAction.Update, Resource: ScreenDraftsResource.UserRoles, IsAdmin: true),
        new(Description: "View Roles", Action: ScreenDraftsAction.View, Resource: ScreenDraftsResource.Roles, IsAdmin: true),
        new(Description: "Create Roles", Action: ScreenDraftsAction.Create, Resource: ScreenDraftsResource.Roles, IsAdmin: true),
        new(Description: "Update Roles", Action: ScreenDraftsAction.Update, Resource: ScreenDraftsResource.Roles, IsAdmin: true),
        new(Description: "Delete Roles", Action: ScreenDraftsAction.Delete, Resource: ScreenDraftsResource.Roles, IsAdmin: true),
        new(Description: "View RoleClaims", Action: ScreenDraftsAction.View, Resource: ScreenDraftsResource.RoleClaims, IsAdmin: true),
        new(Description: "Update RoleClaims", Action: ScreenDraftsAction.Update, Resource: ScreenDraftsResource.RoleClaims, IsAdmin: true),
        new(Description: "Create Drafts", Action: ScreenDraftsAction.Create, Resource: ScreenDraftsResource.Drafts, IsAdmin: true, IsHost: true),
        new(Description: "View Drafts", Action: ScreenDraftsAction.View, Resource: ScreenDraftsResource.Drafts),
        new(Description: "Update Drafts", Action: ScreenDraftsAction.Update, Resource: ScreenDraftsResource.Drafts, IsAdmin: true, IsHost: true),
        new(Description: "Search Drafts", Action: ScreenDraftsAction.Search, Resource: ScreenDraftsResource.Drafts),
        new(Description: "Create Drafters", Action: ScreenDraftsAction.Create, Resource: ScreenDraftsResource.Drafters, IsAdmin: true, IsHost: true),
        new(Description: "View Drafters", Action: ScreenDraftsAction.View, Resource: ScreenDraftsResource.Drafters),
        new(Description: "Update Drafters", Action: ScreenDraftsAction.Update, Resource: ScreenDraftsResource.Drafters, IsAdmin: true, IsHost: true, IsDrafter: true),
        new(Description: "Search Drafters", Action: ScreenDraftsAction.Search, Resource: ScreenDraftsResource.Drafters),
    };

    public static IReadOnlyList<ScreenDraftsPermission> All { get; } = new ReadOnlyCollection<ScreenDraftsPermission>(list: _all);
    public static IReadOnlyList<ScreenDraftsPermission> Admin { get; } = new ReadOnlyCollection<ScreenDraftsPermission>(list: _all.Where(predicate: p => p.IsAdmin && p.IsBasic).ToArray());
    public static IReadOnlyList<ScreenDraftsPermission> Basic { get; } = new ReadOnlyCollection<ScreenDraftsPermission>(list: _all.Where(predicate: p => p.IsBasic).ToArray());
    public static IReadOnlyList<ScreenDraftsPermission> Host { get; } = new ReadOnlyCollection<ScreenDraftsPermission>(list: _all.Where(predicate: p => p.IsHost && p.IsBasic).ToArray());
    public static IReadOnlyList<ScreenDraftsPermission> Drafter { get; } = new ReadOnlyCollection<ScreenDraftsPermission>(list: _all.Where(predicate: p => p.IsDrafter && p.IsBasic).ToArray());
}

public record ScreenDraftsPermission(string Description, string Action, string Resource, bool IsBasic = true, bool IsAdmin = false, bool IsHost = false, bool IsDrafter = false)
{
    public string Name => NameFor(Action, Resource);
    public static string NameFor(string action, string resource) => $"Permissions.{resource}.{action}";
}
