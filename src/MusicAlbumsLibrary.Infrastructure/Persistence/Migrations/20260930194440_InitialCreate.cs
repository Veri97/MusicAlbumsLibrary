using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicAlbumsLibrary.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Libraries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Libraries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Libraries_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Albums",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LibraryId = table.Column<int>(type: "int", nullable: false),
                    MusicCatalogueProvider = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MusicCatalogueProviderAlbumId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ArtistName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    AlbumName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CoverUrl = table.Column<string>(type: "nvarchar(2500)", maxLength: 2500, nullable: true),
                    AlbumUrl = table.Column<string>(type: "nvarchar(2500)", maxLength: 2500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Albums_Libraries_LibraryId",
                        column: x => x.LibraryId,
                        principalTable: "Libraries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Albums_LibraryId_MusicCatalogueProvider_MusicCatalogueProviderAlbumId",
                table: "Albums",
                columns: new[] { "LibraryId", "MusicCatalogueProvider", "MusicCatalogueProviderAlbumId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Libraries_UserId",
                table: "Libraries",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Albums");

            migrationBuilder.DropTable(
                name: "Libraries");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
