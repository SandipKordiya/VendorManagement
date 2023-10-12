using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vendor.Database.Migrations
{
    /// <inheritdoc />
    public partial class addedbotemail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBotEmail",
                table: "AspNetUsers",
                type: "bit",
                nullable: true,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBotEmail",
                table: "AspNetUsers");
        }
    }
}
