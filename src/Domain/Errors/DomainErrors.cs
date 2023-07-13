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
    }
}
