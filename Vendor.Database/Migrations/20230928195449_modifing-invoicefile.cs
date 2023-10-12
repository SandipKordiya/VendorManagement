using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vendor.Database.Migrations
{
    /// <inheritdoc />
    public partial class modifinginvoicefile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "InvoiceDate",
                table: "VendorFiles",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "InvoiceNumber",
                table: "VendorFiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSentEmail",
                table: "VendorFiles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PoNumber",
                table: "VendorFiles",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoiceDate",
                table: "VendorFiles");

            migrationBuilder.DropColumn(
                name: "InvoiceNumber",
                table: "VendorFiles");

            migrationBuilder.DropColumn(
                name: "IsSentEmail",
                table: "VendorFiles");

            migrationBuilder.DropColumn(
                name: "PoNumber",
                table: "VendorFiles");
        }
    }
}
