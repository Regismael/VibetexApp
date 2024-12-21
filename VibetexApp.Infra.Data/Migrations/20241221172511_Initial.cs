using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VibetexApp.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "USUARIO",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NOME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EMAIL = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    SENHA = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TIPO_PERFIL = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PONTO",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    INICIO_EXPEDIENTE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FIM_EXPEDIENTE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    INICIO_PAUSA = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RETORNO_PAUSA = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HORAS_TRABALHADAS = table.Column<TimeSpan>(type: "TIME", nullable: false),
                    HORAS_EXTRAS = table.Column<TimeSpan>(type: "TIME", nullable: false),
                    HORAS_DEVIDAS = table.Column<TimeSpan>(type: "TIME", nullable: false),
                    OBSERVACOES = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    LATITUDE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LONGITUDE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    USUARIO_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PONTO", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PONTO_USUARIO_USUARIO_ID",
                        column: x => x.USUARIO_ID,
                        principalTable: "USUARIO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PONTO_USUARIO_ID",
                table: "PONTO",
                column: "USUARIO_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PONTO");

            migrationBuilder.DropTable(
                name: "USUARIO");
        }
    }
}
