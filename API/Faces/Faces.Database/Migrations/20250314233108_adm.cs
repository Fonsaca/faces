using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faces.Database.Migrations
{
    /// <inheritdoc />
    public partial class adm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2025, 3, 14, 23, 31, 0, 0, DateTimeKind.Utc));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2025, 3, 14, 23, 29, 0, 0, DateTimeKind.Utc));
        }
    }
}
