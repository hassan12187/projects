using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hr_Vacancy_Managment.Migrations
{
    /// <inheritdoc />
    public partial class vacancy_updated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vacancy_Department_DepartmentsDepartment_ID",
                table: "Vacancy");

            migrationBuilder.DropIndex(
                name: "IX_Vacancy_DepartmentsDepartment_ID",
                table: "Vacancy");

            migrationBuilder.DropColumn(
                name: "DepartmentsDepartment_ID",
                table: "Vacancy");

            migrationBuilder.CreateIndex(
                name: "IX_Vacancy_Depart_ID",
                table: "Vacancy",
                column: "Depart_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Vacancy_Department_Depart_ID",
                table: "Vacancy",
                column: "Depart_ID",
                principalTable: "Department",
                principalColumn: "Department_ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vacancy_Department_Depart_ID",
                table: "Vacancy");

            migrationBuilder.DropIndex(
                name: "IX_Vacancy_Depart_ID",
                table: "Vacancy");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentsDepartment_ID",
                table: "Vacancy",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Vacancy_DepartmentsDepartment_ID",
                table: "Vacancy",
                column: "DepartmentsDepartment_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Vacancy_Department_DepartmentsDepartment_ID",
                table: "Vacancy",
                column: "DepartmentsDepartment_ID",
                principalTable: "Department",
                principalColumn: "Department_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
