using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeddingPortal.Migrations
{
    /// <inheritdoc />
    public partial class AddWeddingImageUpload : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "WeddingInfos",
                newName: "ImagePath");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "WeddingInfos",
                newName: "ImageUrl");
        }
    }
}
