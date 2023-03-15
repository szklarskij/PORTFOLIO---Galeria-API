using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GaleriaTAPI.Migrations
{
    /// <inheritdoc />
    public partial class addthumbimg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ThumbImageUrl",
                table: "GalleryPosts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThumbImageUrl",
                table: "GalleryPosts");
        }
    }
}
