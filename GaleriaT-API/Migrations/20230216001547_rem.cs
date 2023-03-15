using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GaleriaTAPI.Migrations
{
    /// <inheritdoc />
    public partial class rem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageHeight",
                table: "GalleryPosts");

            migrationBuilder.DropColumn(
                name: "ImageWidth",
                table: "GalleryPosts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ImageHeight",
                table: "GalleryPosts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ImageWidth",
                table: "GalleryPosts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
