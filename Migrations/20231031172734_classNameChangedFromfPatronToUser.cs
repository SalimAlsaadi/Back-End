using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryAPI.Migrations
{
    /// <inheritdoc />
    public partial class classNameChangedFromfPatronToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_borrowFunctions_patrons_patronId",
                table: "borrowFunctions");

            migrationBuilder.DropTable(
                name: "patrons");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    patronId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    patronName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PatronEmail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PatronPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    age = table.Column<int>(type: "int", maxLength: 3, nullable: false),
                    bank_account = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    type_user = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.patronId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_borrowFunctions_users_patronId",
                table: "borrowFunctions",
                column: "patronId",
                principalTable: "users",
                principalColumn: "patronId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_borrowFunctions_users_patronId",
                table: "borrowFunctions");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.CreateTable(
                name: "patrons",
                columns: table => new
                {
                    patronId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatronEmail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PatronPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    age = table.Column<int>(type: "int", maxLength: 3, nullable: false),
                    bank_account = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    patronName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    phoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patrons", x => x.patronId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_borrowFunctions_patrons_patronId",
                table: "borrowFunctions",
                column: "patronId",
                principalTable: "patrons",
                principalColumn: "patronId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
