using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryAPI.Migrations
{
    /// <inheritdoc />
    public partial class deleteColumnTRansaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "borrowDate",
                table: "borrowFunctions");

            migrationBuilder.DropColumn(
                name: "returnDate",
                table: "borrowFunctions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "borrowDate",
                table: "borrowFunctions",
                type: "nvarchar(max)",
                rowVersion: true,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "returnDate",
                table: "borrowFunctions",
                type: "nvarchar(max)",
                rowVersion: true,
                nullable: false,
                defaultValue: "");
        }
    }
}
