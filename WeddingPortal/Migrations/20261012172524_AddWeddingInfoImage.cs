using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeddingPortal.Migrations
{
    /// <inheritdoc />
    public partial class AddWeddingInfoImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "WeddingInfos",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "WeddingInfos");
        }
    }
}
