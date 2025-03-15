using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faces.Database.Migrations
{
    /// <inheritdoc />
    public partial class adm2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "ID", "BirthDate", "CreationDate", "DeletionDate", "DocNumber", "Email", "FirstName", "IsDeleted", "JobFunctionCode", "LastName", "ManagerID", "PasswordHash" },
                values: new object[] { -1, "1990-01-01", new DateTime(2025, 3, 14, 23, 35, 0, 0, DateTimeKind.Utc), null, "99999999999", "admin.rh@faces.com", "admin", false, "0001", "Rh", null, "i+rcXhRFTdUTkNVTJ07ydQ==.HXcBql16eQsd/uam7bZdKvPhAIAotA8kbwx7FBsrRBc=" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "ID",
                keyValue: -1);

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "ID", "BirthDate", "CreationDate", "DeletionDate", "DocNumber", "Email", "FirstName", "IsDeleted", "JobFunctionCode", "LastName", "ManagerID", "PasswordHash" },
                values: new object[] { 1, "1990-01-01", new DateTime(2025, 3, 14, 23, 31, 0, 0, DateTimeKind.Utc), null, "99999999999", "admin.rh@faces.com", "admin", false, "0001", "Rh", null, "i+rcXhRFTdUTkNVTJ07ydQ==.HXcBql16eQsd/uam7bZdKvPhAIAotA8kbwx7FBsrRBc=" });
        }
    }
}
