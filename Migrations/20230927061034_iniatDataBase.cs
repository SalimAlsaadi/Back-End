using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryAPI.Migrations
{
    /// <inheritdoc />
    public partial class iniatDataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "books",
                columns: table => new
                {
                    bookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    bookName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    author = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    pub_year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    available_status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_books", x => x.bookId);
                });

            migrationBuilder.CreateTable(
                name: "patrons",
                columns: table => new
                {
                    patronId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    patronName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    phoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    borrowHistory = table.Column<string>(type: "nvarchar(max)", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patrons", x => x.patronId);
                });

            migrationBuilder.CreateTable(
                name: "borrowFunctions",
                columns: table => new
                {
                    transID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    borrowDate = table.Column<string>(type: "nvarchar(max)", rowVersion: true, nullable: true),
                    returnDate = table.Column<string>(type: "nvarchar(max)", rowVersion: true, nullable: true),
                    patronId = table.Column<int>(type: "int", nullable: false),
                    bookId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_borrowFunctions", x => x.transID);
                    table.ForeignKey(
                        name: "FK_borrowFunctions_books_bookId",
                        column: x => x.bookId,
                        principalTable: "books",
                        principalColumn: "bookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_borrowFunctions_patrons_patronId",
                        column: x => x.patronId,
                        principalTable: "patrons",
                        principalColumn: "patronId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_borrowFunctions_bookId",
                table: "borrowFunctions",
                column: "bookId");

            migrationBuilder.CreateIndex(
                name: "IX_borrowFunctions_patronId",
                table: "borrowFunctions",
                column: "patronId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "borrowFunctions");

            migrationBuilder.DropTable(
                name: "books");

            migrationBuilder.DropTable(
                name: "patrons");
        }
    }
}
