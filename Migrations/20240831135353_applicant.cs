using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hr_Vacancy_Managment.Migrations
{
    /// <inheritdoc />
    public partial class applicant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Applicant",
                columns: table => new
                {
                    Applicant_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Applicant_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Applicant_Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Applicant_Phone = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    Applicant_CV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Applied_For = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Applicant_Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Creation_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Applicant_Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applicant", x => x.Applicant_ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Applicant");
        }
    }
}
