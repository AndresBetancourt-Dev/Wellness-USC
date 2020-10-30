using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Wellness_USC.Migrations.ClaseDb
{
    public partial class ClaseDb : Migration
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
