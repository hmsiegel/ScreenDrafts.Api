using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Migrators.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class AddInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "auditTrails",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    userId = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<string>(type: "text", nullable: true),
                    tableName = table.Column<string>(type: "text", nullable: true),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    oldValues = table.Column<string>(type: "text", nullable: true),
                    newValues = table.Column<string>(type: "text", nullable: true),
                    affectedColumns = table.Column<string>(type: "text", nullable: true),
                    primaryKey = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_auditTrails", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "CastMembers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    imdbId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    createdBy = table.Column<Guid>(type: "uuid", nullable: false),
                    createdOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    modifiedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_CastMembers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "CrewMembers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    imdbId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    createdBy = table.Column<Guid>(type: "uuid", nullable: false),
                    createdOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    modifiedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_CrewMembers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Drafts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    draftType = table.Column<string>(type: "text", nullable: false),
                    releaseDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    runtime = table.Column<int>(type: "integer", nullable: true),
                    episodeNumber = table.Column<string>(type: "text", nullable: true),
                    numberOfDrafters = table.Column<int>(type: "integer", nullable: true),
                    numberOfFilms = table.Column<int>(type: "integer", nullable: true),
                    createdBy = table.Column<Guid>(type: "uuid", nullable: false),
                    createdOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    modifiedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_Drafts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    year = table.Column<string>(type: "text", nullable: true),
                    imageUrl = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    imdbUrl = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    isInMarqueeOfFame = table.Column<bool>(type: "boolean", nullable: true),
                    createdBy = table.Column<Guid>(type: "uuid", nullable: false),
                    createdOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    modifiedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_Movies", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    concurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_Roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    firstName = table.Column<string>(type: "text", nullable: true),
                    lastName = table.Column<string>(type: "text", nullable: true),
                    imageUrl = table.Column<string>(type: "text", nullable: true),
                    isActive = table.Column<bool>(type: "boolean", nullable: false),
                    drafterId = table.Column<Guid>(type: "uuid", nullable: true),
                    hostId = table.Column<Guid>(type: "uuid", nullable: true),
                    refreshToken = table.Column<string>(type: "text", nullable: true),
                    refreshTokenExpiryTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    objectId = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    userName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    emailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    passwordHash = table.Column<string>(type: "text", nullable: true),
                    securityStamp = table.Column<string>(type: "text", nullable: true),
                    concurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    phoneNumber = table.Column<string>(type: "text", nullable: true),
                    phoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    twoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    lockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    lockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    accessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_Users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "DraftDrafterIds",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DrafterId = table.Column<Guid>(type: "uuid", nullable: false),
                    draftId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_DraftDrafterIds", x => x.id);
                    table.ForeignKey(
                        name: "fK_DraftDrafterIds_Drafts_draftId",
                        column: x => x.draftId,
                        principalTable: "Drafts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DraftHostIds",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HostId = table.Column<Guid>(type: "uuid", nullable: false),
                    draftId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_DraftHostIds", x => x.id);
                    table.ForeignKey(
                        name: "fK_DraftHostIds_Drafts_draftId",
                        column: x => x.draftId,
                        principalTable: "Drafts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SelectedMovies",
                columns: table => new
                {
                    SelectedMovieId = table.Column<Guid>(type: "uuid", nullable: false),
                    draftId = table.Column<Guid>(type: "uuid", nullable: false),
                    MovieId = table.Column<Guid>(type: "uuid", nullable: false),
                    draftPosition = table.Column<int>(type: "integer", nullable: false),
                    createdBy = table.Column<Guid>(type: "uuid", nullable: false),
                    createdOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    modifiedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_SelectedMovies", x => new { x.SelectedMovieId, x.draftId });
                    table.ForeignKey(
                        name: "fK_SelectedMovies_Drafts_draftId",
                        column: x => x.draftId,
                        principalTable: "Drafts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieCast",
                columns: table => new
                {
                    MovieCastId = table.Column<Guid>(type: "uuid", nullable: false),
                    movieId = table.Column<Guid>(type: "uuid", nullable: false),
                    castMemberId = table.Column<Guid>(type: "uuid", nullable: false),
                    roleDescription = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_MovieCast", x => new { x.MovieCastId, x.movieId });
                    table.ForeignKey(
                        name: "fK_MovieCast_Movies_movieId",
                        column: x => x.movieId,
                        principalTable: "Movies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieDirectors",
                columns: table => new
                {
                    DirectorId = table.Column<Guid>(type: "uuid", nullable: false),
                    movieId = table.Column<Guid>(type: "uuid", nullable: false),
                    crewMemberId = table.Column<Guid>(type: "uuid", nullable: false),
                    jobDescription = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_MovieDirectors", x => new { x.DirectorId, x.movieId });
                    table.ForeignKey(
                        name: "fK_MovieDirectors_Movies_movieId",
                        column: x => x.movieId,
                        principalTable: "Movies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieProducers",
                columns: table => new
                {
                    ProducerId = table.Column<Guid>(type: "uuid", nullable: false),
                    movieId = table.Column<Guid>(type: "uuid", nullable: false),
                    crewMemberId = table.Column<Guid>(type: "uuid", nullable: false),
                    jobDescription = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_MovieProducers", x => new { x.ProducerId, x.movieId });
                    table.ForeignKey(
                        name: "fK_MovieProducers_Movies_movieId",
                        column: x => x.movieId,
                        principalTable: "Movies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieWriters",
                columns: table => new
                {
                    WriterId = table.Column<Guid>(type: "uuid", nullable: false),
                    movieId = table.Column<Guid>(type: "uuid", nullable: false),
                    crewMemberId = table.Column<Guid>(type: "uuid", nullable: false),
                    jobDescription = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_MovieWriters", x => new { x.WriterId, x.movieId });
                    table.ForeignKey(
                        name: "fK_MovieWriters_Movies_movieId",
                        column: x => x.movieId,
                        principalTable: "Movies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    createdBy = table.Column<string>(type: "text", nullable: true),
                    createdDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    roleId = table.Column<string>(type: "text", nullable: false),
                    claimType = table.Column<string>(type: "text", nullable: true),
                    claimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_RoleClaims", x => x.id);
                    table.ForeignKey(
                        name: "fK_RoleClaims_AspNetRoles_roleId",
                        column: x => x.roleId,
                        principalTable: "Roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Drafters",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    userId = table.Column<string>(type: "text", nullable: true),
                    hasRolloverVeto = table.Column<bool>(type: "boolean", nullable: true),
                    hasRolloverVetooverride = table.Column<bool>(type: "boolean", nullable: true),
                    createdOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modifiedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    createdBy = table.Column<Guid>(type: "uuid", nullable: false),
                    modifiedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_Drafters", x => x.id);
                    table.ForeignKey(
                        name: "fK_Drafters_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Hosts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    userId = table.Column<string>(type: "text", nullable: true),
                    predictionPoints = table.Column<int>(type: "integer", nullable: true),
                    createdOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modifiedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    createdBy = table.Column<Guid>(type: "uuid", nullable: false),
                    modifiedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_Hosts", x => x.id);
                    table.ForeignKey(
                        name: "fK_Hosts_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userId = table.Column<string>(type: "text", nullable: false),
                    claimType = table.Column<string>(type: "text", nullable: true),
                    claimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_UserClaims", x => x.id);
                    table.ForeignKey(
                        name: "fK_UserClaims_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    loginProvider = table.Column<string>(type: "text", nullable: false),
                    providerKey = table.Column<string>(type: "text", nullable: false),
                    providerDisplayName = table.Column<string>(type: "text", nullable: true),
                    userId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_UserLogins", x => new { x.loginProvider, x.providerKey });
                    table.ForeignKey(
                        name: "fK_UserLogins_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    userId = table.Column<string>(type: "text", nullable: false),
                    roleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_UserRoles", x => new { x.userId, x.roleId });
                    table.ForeignKey(
                        name: "fK_UserRoles_Roles_roleId",
                        column: x => x.roleId,
                        principalTable: "Roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fK_UserRoles_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    userId = table.Column<string>(type: "text", nullable: false),
                    loginProvider = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_UserTokens", x => new { x.userId, x.loginProvider, x.name });
                    table.ForeignKey(
                        name: "fK_UserTokens_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PickDecisions",
                columns: table => new
                {
                    PickDecisionId = table.Column<Guid>(type: "uuid", nullable: false),
                    draftId = table.Column<Guid>(type: "uuid", nullable: false),
                    selectedMovieId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    decision = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_PickDecisions", x => new { x.PickDecisionId, x.draftId, x.selectedMovieId });
                    table.ForeignKey(
                        name: "fK_PickDecisions_SelectedMovies_selectedMovieId_DraftId",
                        columns: x => new { x.selectedMovieId, x.draftId },
                        principalTable: "SelectedMovies",
                        principalColumns: new[] { "SelectedMovieId", "draftId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "iX_DraftDrafterIds_draftId",
                table: "DraftDrafterIds",
                column: "draftId");

            migrationBuilder.CreateIndex(
                name: "iX_Drafters_userId",
                table: "Drafters",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "iX_DraftHostIds_draftId",
                table: "DraftHostIds",
                column: "draftId");

            migrationBuilder.CreateIndex(
                name: "iX_Hosts_userId",
                table: "Hosts",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "iX_MovieCast_movieId",
                table: "MovieCast",
                column: "movieId");

            migrationBuilder.CreateIndex(
                name: "iX_MovieDirectors_movieId",
                table: "MovieDirectors",
                column: "movieId");

            migrationBuilder.CreateIndex(
                name: "iX_MovieProducers_movieId",
                table: "MovieProducers",
                column: "movieId");

            migrationBuilder.CreateIndex(
                name: "iX_MovieWriters_movieId",
                table: "MovieWriters",
                column: "movieId");

            migrationBuilder.CreateIndex(
                name: "iX_PickDecisions_selectedMovieId_draftId",
                table: "PickDecisions",
                columns: new[] { "selectedMovieId", "draftId" });

            migrationBuilder.CreateIndex(
                name: "iX_RoleClaims_roleId",
                table: "RoleClaims",
                column: "roleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "normalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "iX_SelectedMovies_draftId",
                table: "SelectedMovies",
                column: "draftId");

            migrationBuilder.CreateIndex(
                name: "iX_UserClaims_userId",
                table: "UserClaims",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "iX_UserLogins_userId",
                table: "UserLogins",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "iX_UserRoles_roleId",
                table: "UserRoles",
                column: "roleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "normalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "normalizedUserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "auditTrails");

            migrationBuilder.DropTable(
                name: "CastMembers");

            migrationBuilder.DropTable(
                name: "CrewMembers");

            migrationBuilder.DropTable(
                name: "DraftDrafterIds");

            migrationBuilder.DropTable(
                name: "Drafters");

            migrationBuilder.DropTable(
                name: "DraftHostIds");

            migrationBuilder.DropTable(
                name: "Hosts");

            migrationBuilder.DropTable(
                name: "MovieCast");

            migrationBuilder.DropTable(
                name: "MovieDirectors");

            migrationBuilder.DropTable(
                name: "MovieProducers");

            migrationBuilder.DropTable(
                name: "MovieWriters");

            migrationBuilder.DropTable(
                name: "PickDecisions");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "SelectedMovies");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Drafts");
        }
    }
}
