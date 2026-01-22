using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitHubAdmin.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clienti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nume = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    DataInregistrare = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clienti", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Abonamente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Tip = table.Column<int>(type: "INTEGER", nullable: false),
                    Durata = table.Column<int>(type: "INTEGER", nullable: false),
                    Pret = table.Column<decimal>(type: "TEXT", nullable: false),
                    DataStart = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataExpirare = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ClientId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abonamente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Abonamente_Clienti_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clienti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IstoricAcces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DataAcces = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ClientId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IstoricAcces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IstoricAcces_Clienti_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clienti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Abonamente_ClientId",
                table: "Abonamente",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_IstoricAcces_ClientId",
                table: "IstoricAcces",
                column: "ClientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Abonamente");

            migrationBuilder.DropTable(
                name: "IstoricAcces");

            migrationBuilder.DropTable(
                name: "Clienti");
        }
    }
}
