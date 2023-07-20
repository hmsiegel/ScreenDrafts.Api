namespace ScreenDrafts.Api.Persistence.Common;
internal static class DatabaseConstants
{
    public const string Id = "Id";
    internal static class TableNames
    {
        public const string Users = "Users";
        public const string Roles = "Roles";
        public const string UserRoles = "UserRoles";
        public const string UserClaims = "UserClaims";
        public const string Permissions = "Permissions";
        public const string RoleClaims = "RoleClaims";
        public const string RolePermissions = "RolePermissions";
        public const string Drafters = "Drafters";
        public const string Hosts = "Hosts";
        public const string UserLogins = "UserLogins";
        public const string UserTokens = "UserTokens";
        public const string Drafts = "Drafts";
        public const string Movies = "Movies";
        public const string MovieDirectors = "MovieDirectors";
        public const string MovieWriters = "MovieWriters";
        public const string MovieProducers = "MovieProducers";
        public const string MovieCast = "MovieCast";
        public const string SelectedMovies = "SelectedMovies";
        public const string DraftDrafterIds = "DraftDrafterIds";
        public const string DraftHostIds = "DraftHostIds";
        public const string PickDecisions = "PickDecisions";
        public const string CastMembers = "CastMembers";
        public const string CrewMembers =  "CrewMembers";
        public const string MovieCrewMembers = "MovieCrewMembers";
        public const string MovieCastMembers = "MovieCastMembers";
    }

    internal static class ObjectNames
    {
        public const string DraftId = "DraftId";
        public const string DrafterId = "DrafterId";
        public const string HostId = "HostId";
        public const string MovieId = "MovieId";
        public const string UserId = "UserId";
        public const string SelectedMovieId = "SelectedMovieId";
        public const string PickDecisionId = "PickDecisionId";
        public const string DirectorId = "DirectorId";
        public const string WriterId = "WriterId";
        public const string ProducerId = "ProducerId";
        public const string MovieCastId = "MovieCastId";
        public const string CrewMemberId = "CrewMemberId";
        public const string CastMemberId = "CastMemberId";
        public const string _directors = "_directors";
        public const string _writers = "_writers";
        public const string _producers = "_producers";
        public const string _cast = "_cast";
        public const string _pickDecisions = "_pickDecisions";
    }
}
