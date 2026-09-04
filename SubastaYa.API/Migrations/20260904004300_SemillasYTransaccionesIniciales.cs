using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SubastaYa.API.Migrations
{
    /// <inheritdoc />
    public partial class SemillasYTransaccionesIniciales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Auditorias",
                keyColumn: "AuditoriaId",
                keyValue: 1,
                column: "Fecha",
                value: new DateTime(2026, 9, 4, 0, 43, 0, 276, DateTimeKind.Utc).AddTicks(5969));

            migrationBuilder.UpdateData(
                table: "Auditorias",
                keyColumn: "AuditoriaId",
                keyValue: 2,
                column: "Fecha",
                value: new DateTime(2026, 9, 4, 0, 43, 0, 276, DateTimeKind.Utc).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Auditorias",
                keyColumn: "AuditoriaId",
                keyValue: 3,
                column: "Fecha",
                value: new DateTime(2026, 9, 4, 0, 43, 0, 276, DateTimeKind.Utc).AddTicks(6309));

            migrationBuilder.InsertData(
                table: "Pujas",
                columns: new[] { "PujaId", "CompradorFK", "CompradorId", "FechaPuja", "MontoPuja", "SubastaFK", "SubastaId" },
                values: new object[,]
                {
                    { 1, 3, null, new DateTime(2026, 9, 4, 0, 33, 0, 276, DateTimeKind.Utc).AddTicks(8049), 30000m, 1, null },
                    { 2, 2, null, new DateTime(2026, 9, 4, 0, 38, 0, 276, DateTimeKind.Utc).AddTicks(8709), 45000m, 1, null }
                });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 1,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2026, 9, 4, 1, 13, 0, 276, DateTimeKind.Utc).AddTicks(2667), new DateTime(2026, 9, 4, 0, 43, 0, 276, DateTimeKind.Utc).AddTicks(2329) });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 2,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2026, 9, 4, 0, 44, 18, 276, DateTimeKind.Utc).AddTicks(3115), new DateTime(2026, 9, 4, 0, 43, 0, 276, DateTimeKind.Utc).AddTicks(3114) });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 3,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2026, 9, 7, 0, 43, 0, 276, DateTimeKind.Utc).AddTicks(3135), new DateTime(2026, 9, 5, 0, 43, 0, 276, DateTimeKind.Utc).AddTicks(3122) });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 4,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2026, 9, 1, 0, 43, 0, 276, DateTimeKind.Utc).AddTicks(3137), new DateTime(2026, 8, 30, 0, 43, 0, 276, DateTimeKind.Utc).AddTicks(3137) });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 5,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2026, 9, 3, 0, 43, 0, 276, DateTimeKind.Utc).AddTicks(3140), new DateTime(2026, 9, 2, 0, 43, 0, 276, DateTimeKind.Utc).AddTicks(3139) });

            migrationBuilder.InsertData(
                table: "TransaccionesLedger",
                columns: new[] { "TransaccionId", "BilleteraFK", "BilleteraFk", "Monto", "SubastaFK", "SubastaFk", "TipoTransaccion", "fecha" },
                values: new object[,]
                {
                    { 1, 2, null, 150000m, null, null, "DEPOSITO", new DateTime(2026, 9, 3, 0, 43, 0, 277, DateTimeKind.Utc).AddTicks(1223) },
                    { 2, 3, null, 200000m, null, null, "DEPOSITO", new DateTime(2026, 9, 3, 0, 43, 0, 277, DateTimeKind.Utc).AddTicks(1558) },
                    { 3, 4, null, 500m, null, null, "DEPOSITO", new DateTime(2026, 9, 3, 0, 43, 0, 277, DateTimeKind.Utc).AddTicks(1567) },
                    { 4, 2, null, -45000m, 1, null, "RETENCION", new DateTime(2026, 9, 4, 0, 38, 0, 277, DateTimeKind.Utc).AddTicks(1569) }
                });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 1,
                columns: new[] { "ContrasenaHash", "FechaRegistro" },
                values: new object[] { "$2a$11$urzy5BQ.peX3CWqny04lauFiUj0fVTjFY.4FmR70caaI.zyw5/ap.", new DateTime(2026, 9, 4, 0, 42, 59, 967, DateTimeKind.Utc).AddTicks(6824) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 2,
                columns: new[] { "ContrasenaHash", "FechaRegistro" },
                values: new object[] { "$2a$11$tbCIZSbHxY.cs9RtvVZWZOYl/4V3WPsaX7zWt2EM..7esvrFZ7oNS", new DateTime(2026, 9, 4, 0, 43, 0, 70, DateTimeKind.Utc).AddTicks(6812) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 3,
                columns: new[] { "ContrasenaHash", "FechaRegistro" },
                values: new object[] { "$2a$11$Z/KGJtdWm.H9XLsCK3n7iuU3aO2fS98M50BN6tE2La8HtjKvvkcNW", new DateTime(2026, 9, 4, 0, 43, 0, 172, DateTimeKind.Utc).AddTicks(6974) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 4,
                columns: new[] { "ContrasenaHash", "FechaRegistro" },
                values: new object[] { "$2a$11$J3L8oXjYSzLwhvHZircI3eoKINyoG2/BDo8uVrCJKjvAOqAieaXza", new DateTime(2026, 9, 4, 0, 43, 0, 275, DateTimeKind.Utc).AddTicks(4561) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Pujas",
                keyColumn: "PujaId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Pujas",
                keyColumn: "PujaId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TransaccionesLedger",
                keyColumn: "TransaccionId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TransaccionesLedger",
                keyColumn: "TransaccionId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TransaccionesLedger",
                keyColumn: "TransaccionId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TransaccionesLedger",
                keyColumn: "TransaccionId",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Auditorias",
                keyColumn: "AuditoriaId",
                keyValue: 1,
                column: "Fecha",
                value: new DateTime(2026, 9, 4, 0, 21, 12, 599, DateTimeKind.Utc).AddTicks(5129));

            migrationBuilder.UpdateData(
                table: "Auditorias",
                keyColumn: "AuditoriaId",
                keyValue: 2,
                column: "Fecha",
                value: new DateTime(2026, 9, 4, 0, 21, 12, 599, DateTimeKind.Utc).AddTicks(5470));

            migrationBuilder.UpdateData(
                table: "Auditorias",
                keyColumn: "AuditoriaId",
                keyValue: 3,
                column: "Fecha",
                value: new DateTime(2026, 9, 4, 0, 21, 12, 599, DateTimeKind.Utc).AddTicks(5472));

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 1,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2026, 9, 4, 0, 51, 12, 599, DateTimeKind.Utc).AddTicks(1788), new DateTime(2026, 9, 4, 0, 21, 12, 599, DateTimeKind.Utc).AddTicks(1454) });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 2,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2026, 9, 4, 0, 22, 30, 599, DateTimeKind.Utc).AddTicks(2280), new DateTime(2026, 9, 4, 0, 21, 12, 599, DateTimeKind.Utc).AddTicks(2279) });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 3,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2026, 9, 7, 0, 21, 12, 599, DateTimeKind.Utc).AddTicks(2301), new DateTime(2026, 9, 5, 0, 21, 12, 599, DateTimeKind.Utc).AddTicks(2288) });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 4,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2026, 9, 1, 0, 21, 12, 599, DateTimeKind.Utc).AddTicks(2304), new DateTime(2026, 8, 30, 0, 21, 12, 599, DateTimeKind.Utc).AddTicks(2304) });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 5,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2026, 9, 3, 0, 21, 12, 599, DateTimeKind.Utc).AddTicks(2307), new DateTime(2026, 9, 2, 0, 21, 12, 599, DateTimeKind.Utc).AddTicks(2307) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 1,
                columns: new[] { "ContrasenaHash", "FechaRegistro" },
                values: new object[] { "$2a$11$9EfSBCNzg.HtLMolc6UOo.1QUnfaz4Nv2Tt3hCt9RyTD/ADx/hXJW", new DateTime(2026, 9, 4, 0, 21, 12, 173, DateTimeKind.Utc).AddTicks(6698) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 2,
                columns: new[] { "ContrasenaHash", "FechaRegistro" },
                values: new object[] { "$2a$11$TiroUcoTbmQJ9QSdjExPpuVE97IV.yDiAXjw.C01BL8ckSLJzUfVe", new DateTime(2026, 9, 4, 0, 21, 12, 391, DateTimeKind.Utc).AddTicks(9287) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 3,
                columns: new[] { "ContrasenaHash", "FechaRegistro" },
                values: new object[] { "$2a$11$651BTSGIwdCUvaZ2YM1JbOOzVJqsEHsUcKC16W1lKjORmQ817EMAa", new DateTime(2026, 9, 4, 0, 21, 12, 495, DateTimeKind.Utc).AddTicks(618) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 4,
                columns: new[] { "ContrasenaHash", "FechaRegistro" },
                values: new object[] { "$2a$11$2SbtF03Sw9RdrfX3NRXJvOZ8/nYe0K7IsO6b/SYIfZqgpkfaflk8e", new DateTime(2026, 9, 4, 0, 21, 12, 598, DateTimeKind.Utc).AddTicks(2500) });
        }
    }
}
