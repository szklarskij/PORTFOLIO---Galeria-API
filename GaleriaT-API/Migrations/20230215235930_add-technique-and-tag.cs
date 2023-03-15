using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GaleriaTAPI.Migrations
{
    /// <inheritdoc />
    public partial class addtechniqueandtag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "GalleryPosts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "GalleryPosts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Technique",
                table: "GalleryPosts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tag",
                table: "GalleryPosts");

            migrationBuilder.DropColumn(
                name: "Technique",
                table: "GalleryPosts");

            migrationBuilder.AlterColumn<string>(
                name: "Price",
                table: "GalleryPosts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
