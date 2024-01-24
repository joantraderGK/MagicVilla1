using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_Api1.Migrations
{
    /// <inheritdoc />
    public partial class AlimentarTablaVilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "villas",
                columns: new[] { "Id", "Amenidad", "Detalle", "FechaActulizacion", "FechaCreacion", "ImagenUr", "MetroCuadrado", "Nombre", "Ocupante", "Tarifa" },
                values: new object[] { 1, "", "Detalle de la Villa...", new DateTime(2024, 1, 24, 8, 19, 45, 14, DateTimeKind.Local).AddTicks(7979), new DateTime(2024, 1, 24, 8, 19, 45, 14, DateTimeKind.Local).AddTicks(7960), "", 50.0, "Villa Real", 5, 200 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "villas",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
