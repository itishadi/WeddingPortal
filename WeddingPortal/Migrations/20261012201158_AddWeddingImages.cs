using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeddingPortal.Migrations
{
    /// <inheritdoc />
    public partial class AddWeddingImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "WeddingInfos");

            migrationBuilder.CreateTable(
                name: "WeddingImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ImagePath = table.Column<string>(type: "TEXT", nullable: false),
                    WeddingInfoId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeddingImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeddingImages_WeddingInfos_WeddingInfoId",
                        column: x => x.WeddingInfoId,
                        principalTable: "WeddingInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WeddingImages_WeddingInfoId",
                table: "WeddingImages",
                column: "WeddingInfoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeddingImages");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "WeddingInfos",
                type: "TEXT",
                nullable: true);
        }
    }
}
