using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_Api1.Migrations
{
    /// <inheritdoc />
    public partial class AgregarNumeroVillaTabla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "numerovillas",
                columns: table => new
                {
                    VillaNo = table.Column<int>(type: "int", nullable: false),
                    VillaId = table.Column<int>(type: "int", nullable: false),
                    DealleEspecial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_numerovillas", x => x.VillaNo);
                    table.ForeignKey(
                        name: "FK_numerovillas_villas_VillaId",
                        column: x => x.VillaId,
                        principalTable: "villas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActulizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2024, 1, 26, 0, 16, 36, 604, DateTimeKind.Local).AddTicks(7827), new DateTime(2024, 1, 26, 0, 16, 36, 604, DateTimeKind.Local).AddTicks(7815) });

            migrationBuilder.CreateIndex(
                name: "IX_numerovillas_VillaId",
                table: "numerovillas",
                column: "VillaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "numerovillas");

            migrationBuilder.UpdateData(
                table: "villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActulizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2024, 1, 24, 8, 19, 45, 14, DateTimeKind.Local).AddTicks(7979), new DateTime(2024, 1, 24, 8, 19, 45, 14, DateTimeKind.Local).AddTicks(7960) });
        }
    }
}
