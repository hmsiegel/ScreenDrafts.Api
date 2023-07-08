using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateHost_and_Drafter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_drafters_users_id",
                table: "Drafters");

            migrationBuilder.DropForeignKey(
                name: "fk_hosts_users_id",
                table: "Hosts");

            migrationBuilder.AddColumn<string>(
                name: "user_id",
                table: "Hosts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "user_id",
                table: "Drafters",
                type: "text",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_drafters_asp_net_users_user_id",
                table: "Drafters",
                column: "id",
                principalTable: "Users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_hosts_asp_net_users_user_id",
                table: "Hosts",
                column: "id",
                principalTable: "Users",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_drafters_asp_net_users_user_id",
                table: "Drafters");

            migrationBuilder.DropForeignKey(
                name: "fk_hosts_asp_net_users_user_id",
                table: "Hosts");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "Hosts");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "Drafters");

            migrationBuilder.AddForeignKey(
                name: "fk_drafters_users_id",
                table: "Drafters",
                column: "id",
                principalTable: "Users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_hosts_users_id",
                table: "Hosts",
                column: "id",
                principalTable: "Users",
                principalColumn: "id");
        }
    }
}
