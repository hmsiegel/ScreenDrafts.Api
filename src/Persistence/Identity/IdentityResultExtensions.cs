﻿namespace ScreenDrafts.Api.Persistence.Identity;
internal static class IdentityResultExtensions
{
    public static List<string> GetErrors(this IdentityResult result) =>
        result.Errors.Select(error => error.Description.ToString()).ToList();
}
