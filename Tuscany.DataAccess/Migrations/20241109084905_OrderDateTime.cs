using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tuscany.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class OrderDateTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "Orders",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "Orders");
        }
    }
}
