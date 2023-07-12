using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class AddMoviesAndDrafts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "prediction_points",
                table: "Hosts",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<Guid>(
                name: "created_by",
                table: "Hosts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "draft_id",
                table: "Hosts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "modified_by",
                table: "Hosts",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "has_rollover_vetooverride",
                table: "Drafters",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<bool>(
                name: "has_rollover_veto",
                table: "Drafters",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AddColumn<Guid>(
                name: "created_by",
                table: "Drafters",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "modified_by",
                table: "Drafters",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "audit_trails",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<string>(type: "text", nullable: true),
                    table_name = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    old_values = table.Column<string>(type: "text", nullable: true),
                    new_values = table.Column<string>(type: "text", nullable: true),
                    affected_columns = table.Column<string>(type: "text", nullable: true),
                    primary_key = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_audit_trails", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    title = table.Column<string>(type: "text", nullable: true),
                    year = table.Column<string>(type: "text", nullable: true),
                    director = table.Column<string>(type: "text", nullable: true),
                    image_url = table.Column<string>(type: "text", nullable: true),
                    imdb_url = table.Column<string>(type: "text", nullable: true),
                    is_in_marquee_of_fame = table.Column<bool>(type: "boolean", nullable: true),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    created_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_by = table.Column<Guid>(type: "uuid", nullable: true),
                    modified_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_movies", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Drafts",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    draft_type = table.Column<string>(type: "text", nullable: false),
                    release_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    runtime = table.Column<int>(type: "integer", nullable: true),
                    episode_number = table.Column<string>(type: "text", nullable: true),
                    number_of_drafters = table.Column<int>(type: "integer", nullable: true),
                    main_host_id = table.Column<string>(type: "text", nullable: true),
                    co_host_id = table.Column<string>(type: "text", nullable: true),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    created_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_by = table.Column<Guid>(type: "uuid", nullable: true),
                    modified_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    movie_id = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_drafts", x => x.id);
                    table.ForeignKey(
                        name: "fk_drafts_movies_movie_id",
                        column: x => x.movie_id,
                        principalTable: "Movies",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "draft_drafter",
                columns: table => new
                {
                    drafters_id = table.Column<string>(type: "text", nullable: false),
                    drafts_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_draft_drafter", x => new { x.drafters_id, x.drafts_id });
                    table.ForeignKey(
                        name: "fk_draft_drafter_drafters_drafters_id",
                        column: x => x.drafters_id,
                        principalTable: "Drafters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_draft_drafter_drafts_drafts_id",
                        column: x => x.drafts_id,
                        principalTable: "Drafts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "selected_movies",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    movie_id = table.Column<string>(type: "text", nullable: false),
                    draft_position = table.Column<int>(type: "integer", nullable: false),
                    drafter_id = table.Column<string>(type: "text", nullable: false),
                    is_vetoed = table.Column<bool>(type: "boolean", nullable: false),
                    drafter_who_played_veto_id = table.Column<string>(type: "text", nullable: false),
                    was_veto_override = table.Column<bool>(type: "boolean", nullable: false),
                    drafter_who_played_veto_override_id = table.Column<string>(type: "text", nullable: false),
                    was_commissoner_override = table.Column<bool>(type: "boolean", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    created_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_by = table.Column<Guid>(type: "uuid", nullable: true),
                    modified_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    draft_id = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_selected_movies", x => x.id);
                    table.ForeignKey(
                        name: "fk_selected_movies_drafters_drafter_id",
                        column: x => x.drafter_id,
                        principalTable: "Drafters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_selected_movies_drafters_drafter_who_played_veto_id",
                        column: x => x.drafter_who_played_veto_id,
                        principalTable: "Drafters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_selected_movies_drafters_drafter_who_played_veto_override_id",
                        column: x => x.drafter_who_played_veto_override_id,
                        principalTable: "Drafters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_selected_movies_drafts_draft_id",
                        column: x => x.draft_id,
                        principalTable: "Drafts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_selected_movies_movies_movie_id",
                        column: x => x.movie_id,
                        principalTable: "Movies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_hosts_draft_id",
                table: "Hosts",
                column: "draft_id");

            migrationBuilder.CreateIndex(
                name: "ix_draft_drafter_drafts_id",
                table: "draft_drafter",
                column: "drafts_id");

            migrationBuilder.CreateIndex(
                name: "ix_drafts_movie_id",
                table: "Drafts",
                column: "movie_id");

            migrationBuilder.CreateIndex(
                name: "ix_selected_movies_draft_id",
                table: "selected_movies",
                column: "draft_id");

            migrationBuilder.CreateIndex(
                name: "ix_selected_movies_drafter_id",
                table: "selected_movies",
                column: "drafter_id");

            migrationBuilder.CreateIndex(
                name: "ix_selected_movies_drafter_who_played_veto_id",
                table: "selected_movies",
                column: "drafter_who_played_veto_id");

            migrationBuilder.CreateIndex(
                name: "ix_selected_movies_drafter_who_played_veto_override_id",
                table: "selected_movies",
                column: "drafter_who_played_veto_override_id");

            migrationBuilder.CreateIndex(
                name: "ix_selected_movies_movie_id",
                table: "selected_movies",
                column: "movie_id");

            migrationBuilder.AddForeignKey(
                name: "fk_hosts_drafts_draft_id",
                table: "Hosts",
                column: "draft_id",
                principalTable: "Drafts",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_hosts_drafts_draft_id",
                table: "Hosts");

            migrationBuilder.DropTable(
                name: "audit_trails");

            migrationBuilder.DropTable(
                name: "draft_drafter");

            migrationBuilder.DropTable(
                name: "selected_movies");

            migrationBuilder.DropTable(
                name: "Drafts");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropIndex(
                name: "ix_hosts_draft_id",
                table: "Hosts");

            migrationBuilder.DropColumn(
                name: "created_by",
                table: "Hosts");

            migrationBuilder.DropColumn(
                name: "draft_id",
                table: "Hosts");

            migrationBuilder.DropColumn(
                name: "modified_by",
                table: "Hosts");

            migrationBuilder.DropColumn(
                name: "created_by",
                table: "Drafters");

            migrationBuilder.DropColumn(
                name: "modified_by",
                table: "Drafters");

            migrationBuilder.AlterColumn<int>(
                name: "prediction_points",
                table: "Hosts",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "has_rollover_vetooverride",
                table: "Drafters",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "has_rollover_veto",
                table: "Drafters",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);
        }
    }
}
