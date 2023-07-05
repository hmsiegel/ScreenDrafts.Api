﻿namespace ScreenDrafts.Api.Shared.Authorization;
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
    private static readonly ScreenDraftsPermission[] _all = new ScreenDraftsPermission[]
    {
        new("View Hangfire", ScreenDraftsAction.View, ScreenDraftsResource.Hangfire),
        new("View Users", ScreenDraftsAction.View, ScreenDraftsResource.Users),
        new("Search Users", ScreenDraftsAction.Search, ScreenDraftsResource.Users),
        new("Create Users", ScreenDraftsAction.Create, ScreenDraftsResource.Users),
        new("Update Users", ScreenDraftsAction.Update, ScreenDraftsResource.Users),
        new("Delete Users", ScreenDraftsAction.Delete, ScreenDraftsResource.Users),
        new("Export Users", ScreenDraftsAction.Export, ScreenDraftsResource.Users),
        new("View UserRoles", ScreenDraftsAction.View, ScreenDraftsResource.UserRoles),
        new("Update UserRoles", ScreenDraftsAction.Update, ScreenDraftsResource.UserRoles),
        new("View Roles", ScreenDraftsAction.View, ScreenDraftsResource.Roles),
        new("Create Roles", ScreenDraftsAction.Create, ScreenDraftsResource.Roles),
        new("Update Roles", ScreenDraftsAction.Update, ScreenDraftsResource.Roles),
        new("Delete Roles", ScreenDraftsAction.Delete, ScreenDraftsResource.Roles),
        new("View RoleClaims", ScreenDraftsAction.View, ScreenDraftsResource.RoleClaims),
        new("Update RoleClaims", ScreenDraftsAction.Update, ScreenDraftsResource.RoleClaims),
        new("Create Drafts", ScreenDraftsAction.Create, ScreenDraftsResource.Drafts, IsHost: true),
        new("View Drafts", ScreenDraftsAction.View, ScreenDraftsResource.Drafts),
        new("Update Drafts", ScreenDraftsAction.Update, ScreenDraftsResource.Drafts, IsHost: true),
        new("Search Drafts", ScreenDraftsAction.Search, ScreenDraftsResource.Drafts),
        new("Create Drafters", ScreenDraftsAction.Create, ScreenDraftsResource.Drafters),
        new("View Drafters", ScreenDraftsAction.View, ScreenDraftsResource.Drafters),
        new("Update Drafters", ScreenDraftsAction.Update, ScreenDraftsResource.Drafters),
        new("Search Drafters", ScreenDraftsAction.Search, ScreenDraftsResource.Drafters),
    };

    public static IReadOnlyList<ScreenDraftsPermission> All { get; } = new ReadOnlyCollection<ScreenDraftsPermission>(_all);
    public static IReadOnlyList<ScreenDraftsPermission> Admin { get; } = new ReadOnlyCollection<ScreenDraftsPermission>(_all.Where(p => !p.IsBasic && !p.IsDrafter && !p.IsHost).ToArray());
    public static IReadOnlyList<ScreenDraftsPermission> Basic { get; } = new ReadOnlyCollection<ScreenDraftsPermission>(_all.Where(p => p.IsBasic).ToArray());
    public static IReadOnlyList<ScreenDraftsPermission> Host { get; } = new ReadOnlyCollection<ScreenDraftsPermission>(_all.Where(p => p.IsHost).ToArray());
    public static IReadOnlyList<ScreenDraftsPermission> Drafter { get; } = new ReadOnlyCollection<ScreenDraftsPermission>(_all.Where(p => p.IsDrafter).ToArray());
}

public record ScreenDraftsPermission(string Description, string Action, string Resource, bool IsBasic = false, bool IsHost = false, bool IsDrafter = false)
{
    public string Name => NameFor(Action, Resource);
    public static string NameFor(string action, string resource) => $"Permissions.{resource}.{action}";
}
