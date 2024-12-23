using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hr_Vacancy_Managment.Migrations
{
    /// <inheritdoc />
    public partial class updated_employee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Employee_Status",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Employee_Status",
                table: "Employees");
        }
    }
}
