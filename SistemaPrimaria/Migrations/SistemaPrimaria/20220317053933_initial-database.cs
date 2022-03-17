using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaPrimaria.Migrations.SistemaPrimaria
{
    public partial class initialdatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Calificacion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    calificacion = table.Column<int>(nullable: false),
                    IdEstudiante = table.Column<int>(nullable: false),
                    IdMateria = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calificacion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Estudiante",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Matricula = table.Column<string>(nullable: true),
                    Nombre = table.Column<string>(nullable: true),
                    ApellidoMaterno = table.Column<string>(nullable: true),
                    ApellidoPaterno = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: true),
                    Direccion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estudiante", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Maestro",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cedula = table.Column<string>(nullable: true),
                    Nombre = table.Column<string>(nullable: true),
                    ApellidoMaterno = table.Column<string>(nullable: true),
                    ApellidoPaterno = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: true),
                    Direccion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maestro", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Materia",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materia", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Calificacion");

            migrationBuilder.DropTable(
                name: "Estudiante");

            migrationBuilder.DropTable(
                name: "Maestro");

            migrationBuilder.DropTable(
                name: "Materia");
        }
    }
}
