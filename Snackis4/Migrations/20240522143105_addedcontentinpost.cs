using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Snackis4.Migrations
{
    /// <inheritdoc />
    public partial class addedcontentinpost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Post",
                newName: "Content");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Post",
                newName: "Description");
        }
    }
}
