using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hr_Vacancy_Managment.Migrations
{
    /// <inheritdoc />
    public partial class vacancy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vacancy",
                columns: table => new
                {
                    Vacancy_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vacancy_Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vacancy_Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vacancy_Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number_Of_Jobs_Under_Vacancy = table.Column<int>(type: "int", nullable: false),
                    Depart_ID = table.Column<int>(type: "int", nullable: false),
                    DepartmentsDepartment_ID = table.Column<int>(type: "int", nullable: false),
                    Last_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Date_Of_Creation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Owned_By = table.Column<int>(type: "int", nullable: false),
                    Vacancy_Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacancy", x => x.Vacancy_ID);
                    table.ForeignKey(
                        name: "FK_Vacancy_Department_DepartmentsDepartment_ID",
                        column: x => x.DepartmentsDepartment_ID,
                        principalTable: "Department",
                        principalColumn: "Department_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vacancy_DepartmentsDepartment_ID",
                table: "Vacancy",
                column: "DepartmentsDepartment_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vacancy");
        }
    }
}
