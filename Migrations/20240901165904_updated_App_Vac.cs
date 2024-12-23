using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hr_Vacancy_Managment.Migrations
{
    /// <inheritdoc />
    public partial class updated_App_Vac : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Interviewer_Name",
                table: "Applicant_Vacancy",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Interviewer_Number",
                table: "Applicant_Vacancy",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Scheduled_Interview_Date",
                table: "Applicant_Vacancy",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Interviewer_Name",
                table: "Applicant_Vacancy");

            migrationBuilder.DropColumn(
                name: "Interviewer_Number",
                table: "Applicant_Vacancy");

            migrationBuilder.DropColumn(
                name: "Scheduled_Interview_Date",
                table: "Applicant_Vacancy");
        }
    }
}
