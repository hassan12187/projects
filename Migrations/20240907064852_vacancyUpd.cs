using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hr_Vacancy_Managment.Migrations
{
    /// <inheritdoc />
    public partial class vacancyUpd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Timing",
                table: "Vacancy",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "salary",
                table: "Vacancy",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Timing",
                table: "Vacancy");

            migrationBuilder.DropColumn(
                name: "salary",
                table: "Vacancy");
        }
    }
}
