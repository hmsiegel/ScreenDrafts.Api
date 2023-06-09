﻿namespace ScreenDrafts.Api.Domain.Errors;
public static class DomainErrors
{
    public static class Drafter
    {
        public static readonly Error UserNotFound = new(
            "Drafter.UserNotFound",
            "User not found.");

        public static readonly Error UserAlreadyAssigned = new(
            "Drafter.UserAlreadyAssigned",
            "User already assigned.");

        public static readonly Error NotFound = new(
            "Drafter.NotFound",
            "Drafter not found.");
    }

    public static class User
    {
        public static readonly Error NotFound = new(
            "User.NotFound",
            "User not found.");

        public static readonly Error InvalidEmail = new(
            "User.InvalidEmail",
            "Invalid email.");

        public static readonly Error InvalidPassword = new(
            "User.InvalidPassword",
            "Invalid password.");
    }
}
