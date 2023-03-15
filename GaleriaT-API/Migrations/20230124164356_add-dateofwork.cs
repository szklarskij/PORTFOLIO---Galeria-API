using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GaleriaTAPI.Migrations
{
    /// <inheritdoc />
    public partial class adddateofwork : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfWork",
                table: "GalleryPosts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfWork",
                table: "GalleryPosts");
        }
    }
}
