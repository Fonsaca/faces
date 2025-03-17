using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Faces.Database.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobFunctions",
                columns: table => new
                {
                    Code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    HierarchyLevel = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobFunctions", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DocNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    BirthDate = table.Column<string>(type: "text", nullable: false),
                    JobFunctionCode = table.Column<string>(type: "character varying(20)", nullable: false),
                    ManagerID = table.Column<int>(type: "integer", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PasswordHash = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Employees_Employees_ManagerID",
                        column: x => x.ManagerID,
                        principalTable: "Employees",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Employees_JobFunctions_JobFunctionCode",
                        column: x => x.JobFunctionCode,
                        principalTable: "JobFunctions",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Phones",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Number = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    EmployeeID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phones", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Phones_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "JobFunctions",
                columns: new[] { "Code", "HierarchyLevel", "Name" },
                values: new object[,]
                {
                    { "0001", (short)1000, "RH Manager" },
                    { "0002", (short)500, "RH Analyst" },
                    { "0003", (short)500, "Tech Leader" },
                    { "0004", (short)100, "IT Analyst" },
                    { "0005", (short)1, "Intern" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "ID", "BirthDate", "CreationDate", "DeletionDate", "DocNumber", "Email", "FirstName", "IsDeleted", "JobFunctionCode", "LastName", "ManagerID", "PasswordHash" },
                values: new object[] { -1, "1990-01-01", new DateTime(2025, 3, 17, 14, 35, 0, 0, DateTimeKind.Utc), null, "0001", "admin.rh@faces.com", "RH", false, "0001", "Admin", null, "i+rcXhRFTdUTkNVTJ07ydQ==.HXcBql16eQsd/uam7bZdKvPhAIAotA8kbwx7FBsrRBc=" });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DocNumber",
                table: "Employees",
                column: "DocNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_JobFunctionCode",
                table: "Employees",
                column: "JobFunctionCode");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ManagerID",
                table: "Employees",
                column: "ManagerID");

            migrationBuilder.CreateIndex(
                name: "IX_Phones_EmployeeID",
                table: "Phones",
                column: "EmployeeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Phones");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "JobFunctions");
        }
    }
}
