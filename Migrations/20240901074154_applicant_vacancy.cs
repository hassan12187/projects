using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hr_Vacancy_Managment.Migrations
{
    /// <inheritdoc />
    public partial class applicant_vacancy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Applicant_Vacancy",
                columns: table => new
                {
                    Applicant_Vacancy_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Applicant_ID = table.Column<int>(type: "int", nullable: false),
                    Vacancy_ID = table.Column<int>(type: "int", nullable: false),
                    Applied_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    App_Vac_Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applicant_Vacancy", x => x.Applicant_Vacancy_ID);
                    table.ForeignKey(
                        name: "FK_Applicant_Vacancy_Applicant_Applicant_ID",
                        column: x => x.Applicant_ID,
                        principalTable: "Applicant",
                        principalColumn: "Applicant_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Applicant_Vacancy_Vacancy_Vacancy_ID",
                        column: x => x.Vacancy_ID,
                        principalTable: "Vacancy",
                        principalColumn: "Vacancy_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applicant_Vacancy_Applicant_ID",
                table: "Applicant_Vacancy",
                column: "Applicant_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Applicant_Vacancy_Vacancy_ID",
                table: "Applicant_Vacancy",
                column: "Vacancy_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Applicant_Vacancy");
        }
    }
}
