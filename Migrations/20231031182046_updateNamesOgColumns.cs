using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryAPI.Migrations
{
    /// <inheritdoc />
    public partial class updateNamesOgColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "type_user",
                table: "users",
                newName: "user_type");

            migrationBuilder.RenameColumn(
                name: "patronName",
                table: "users",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "PatronPassword",
                table: "users",
                newName: "UserPassword");

            migrationBuilder.RenameColumn(
                name: "PatronEmail",
                table: "users",
                newName: "UserEmail");

            migrationBuilder.RenameColumn(
                name: "patronId",
                table: "users",
                newName: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "user_type",
                table: "users",
                newName: "type_user");

            migrationBuilder.RenameColumn(
                name: "UserPassword",
                table: "users",
                newName: "PatronPassword");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "users",
                newName: "patronName");

            migrationBuilder.RenameColumn(
                name: "UserEmail",
                table: "users",
                newName: "PatronEmail");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "users",
                newName: "patronId");
        }
    }
}
