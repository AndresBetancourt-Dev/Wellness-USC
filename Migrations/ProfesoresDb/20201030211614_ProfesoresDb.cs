using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Wellness_USC.Migrations.ProfesoresDb
{
    public partial class ProfesoresDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clase");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "Horario");

            migrationBuilder.DropTable(
                name: "Profesores");
        }
    }
}
