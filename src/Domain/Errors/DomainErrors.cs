namespace ScreenDrafts.Api.Domain.Errors;
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

    public static class Roles
    {
        public static readonly Error NotFound = new(
            "Role.NotFound",
            "Role not found.");
    }

    public static class Draft
    {
        public static readonly Error NotFound = new(
            "Draft.NotFound",
            "Draft not found.");
    }

    public static class Host
    {
        public static readonly Error UserNotFound = new(
            "Host.UserNotFound",
            "User not found.");

        public static readonly Error UserAlreadyAssigned = new(
            "Host.UserAlreadyAssigned",
            "User already assigned.");

        public static readonly Error NotFound = new(
            "Host.NotFound",
            "Host not found.");
    }

    public static class CastMember
    {
        public static readonly Error NotFound = new(
            "CastMember.NotFound",
            "Cast Member not found.");
    }

    public static class CrewMember
    {
        public static readonly Error NotFound = new(
            "CrewMember.NotFound",
            "Crew Member not found.");
    }

    public static class Movie
    {
        public static readonly Error NotFound = new(
            "Movie.NotFound",
            "Movie not found.");
    }
}
