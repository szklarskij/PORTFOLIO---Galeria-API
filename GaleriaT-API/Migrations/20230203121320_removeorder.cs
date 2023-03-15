using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GaleriaTAPI.Migrations
{
    /// <inheritdoc />
    public partial class removeorder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "GalleryPosts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "GalleryPosts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
