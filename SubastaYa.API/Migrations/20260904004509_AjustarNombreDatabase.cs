using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SubastaYa.API.Migrations
{
    /// <inheritdoc />
    public partial class AjustarNombreDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Auditorias",
                keyColumn: "AuditoriaId",
                keyValue: 1,
                column: "Fecha",
                value: new DateTime(2026, 9, 4, 0, 45, 9, 235, DateTimeKind.Utc).AddTicks(8342));

            migrationBuilder.UpdateData(
                table: "Auditorias",
                keyColumn: "AuditoriaId",
                keyValue: 2,
                column: "Fecha",
                value: new DateTime(2026, 9, 4, 0, 45, 9, 235, DateTimeKind.Utc).AddTicks(8672));

            migrationBuilder.UpdateData(
                table: "Auditorias",
                keyColumn: "AuditoriaId",
                keyValue: 3,
                column: "Fecha",
                value: new DateTime(2026, 9, 4, 0, 45, 9, 235, DateTimeKind.Utc).AddTicks(8674));

            migrationBuilder.UpdateData(
                table: "Pujas",
                keyColumn: "PujaId",
                keyValue: 1,
                column: "FechaPuja",
                value: new DateTime(2026, 9, 4, 0, 35, 9, 236, DateTimeKind.Utc).AddTicks(453));

            migrationBuilder.UpdateData(
                table: "Pujas",
                keyColumn: "PujaId",
                keyValue: 2,
                column: "FechaPuja",
                value: new DateTime(2026, 9, 4, 0, 40, 9, 236, DateTimeKind.Utc).AddTicks(1218));

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 1,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2026, 9, 4, 1, 15, 9, 235, DateTimeKind.Utc).AddTicks(5119), new DateTime(2026, 9, 4, 0, 45, 9, 235, DateTimeKind.Utc).AddTicks(4792) });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 2,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2026, 9, 4, 0, 46, 27, 235, DateTimeKind.Utc).AddTicks(5614), new DateTime(2026, 9, 4, 0, 45, 9, 235, DateTimeKind.Utc).AddTicks(5613) });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 3,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2026, 9, 7, 0, 45, 9, 235, DateTimeKind.Utc).AddTicks(5634), new DateTime(2026, 9, 5, 0, 45, 9, 235, DateTimeKind.Utc).AddTicks(5621) });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 4,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2026, 9, 1, 0, 45, 9, 235, DateTimeKind.Utc).AddTicks(5637), new DateTime(2026, 8, 30, 0, 45, 9, 235, DateTimeKind.Utc).AddTicks(5636) });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 5,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2026, 9, 3, 0, 45, 9, 235, DateTimeKind.Utc).AddTicks(5639), new DateTime(2026, 9, 2, 0, 45, 9, 235, DateTimeKind.Utc).AddTicks(5639) });

            migrationBuilder.UpdateData(
                table: "TransaccionesLedger",
                keyColumn: "TransaccionId",
                keyValue: 1,
                column: "fecha",
                value: new DateTime(2026, 9, 3, 0, 45, 9, 236, DateTimeKind.Utc).AddTicks(3743));

            migrationBuilder.UpdateData(
                table: "TransaccionesLedger",
                keyColumn: "TransaccionId",
                keyValue: 2,
                column: "fecha",
                value: new DateTime(2026, 9, 3, 0, 45, 9, 236, DateTimeKind.Utc).AddTicks(4073));

            migrationBuilder.UpdateData(
                table: "TransaccionesLedger",
                keyColumn: "TransaccionId",
                keyValue: 3,
                column: "fecha",
                value: new DateTime(2026, 9, 3, 0, 45, 9, 236, DateTimeKind.Utc).AddTicks(4081));

            migrationBuilder.UpdateData(
                table: "TransaccionesLedger",
                keyColumn: "TransaccionId",
                keyValue: 4,
                column: "fecha",
                value: new DateTime(2026, 9, 4, 0, 40, 9, 236, DateTimeKind.Utc).AddTicks(4083));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 1,
                columns: new[] { "ContrasenaHash", "FechaRegistro" },
                values: new object[] { "$2a$11$6nJLDV5/fqJ5Aebnp4nMOOEC3qCR2STWqEf6SesqdN4wtJ7mq5zwO", new DateTime(2026, 9, 4, 0, 45, 8, 855, DateTimeKind.Utc).AddTicks(8866) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 2,
                columns: new[] { "ContrasenaHash", "FechaRegistro" },
                values: new object[] { "$2a$11$EW9YXtYRVCB8oaQMzTtDW.s4cV1/GcNWtXJkl5EzJVuUpamqVMC8O", new DateTime(2026, 9, 4, 0, 45, 9, 29, DateTimeKind.Utc).AddTicks(6351) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 3,
                columns: new[] { "ContrasenaHash", "FechaRegistro" },
                values: new object[] { "$2a$11$rr3ujlhIuV.1l1E5p2Gg4O5LyQz/ZLfOPUcb5ysqTu0HQ7LYbj8ES", new DateTime(2026, 9, 4, 0, 45, 9, 133, DateTimeKind.Utc).AddTicks(330) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 4,
                columns: new[] { "ContrasenaHash", "FechaRegistro" },
                values: new object[] { "$2a$11$foQ0WavbMQyZ7NYRM2gD2eafofyc2lf3I8ZeFU.wz3cHswoN1YPoG", new DateTime(2026, 9, 4, 0, 45, 9, 234, DateTimeKind.Utc).AddTicks(6466) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.UpdateData(
                table: "Pujas",
                keyColumn: "PujaId",
                keyValue: 1,
                column: "FechaPuja",
                value: new DateTime(2026, 9, 4, 0, 33, 0, 276, DateTimeKind.Utc).AddTicks(8049));

            migrationBuilder.UpdateData(
                table: "Pujas",
                keyColumn: "PujaId",
                keyValue: 2,
                column: "FechaPuja",
                value: new DateTime(2026, 9, 4, 0, 38, 0, 276, DateTimeKind.Utc).AddTicks(8709));

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

            migrationBuilder.UpdateData(
                table: "TransaccionesLedger",
                keyColumn: "TransaccionId",
                keyValue: 1,
                column: "fecha",
                value: new DateTime(2026, 9, 3, 0, 43, 0, 277, DateTimeKind.Utc).AddTicks(1223));

            migrationBuilder.UpdateData(
                table: "TransaccionesLedger",
                keyColumn: "TransaccionId",
                keyValue: 2,
                column: "fecha",
                value: new DateTime(2026, 9, 3, 0, 43, 0, 277, DateTimeKind.Utc).AddTicks(1558));

            migrationBuilder.UpdateData(
                table: "TransaccionesLedger",
                keyColumn: "TransaccionId",
                keyValue: 3,
                column: "fecha",
                value: new DateTime(2026, 9, 3, 0, 43, 0, 277, DateTimeKind.Utc).AddTicks(1567));

            migrationBuilder.UpdateData(
                table: "TransaccionesLedger",
                keyColumn: "TransaccionId",
                keyValue: 4,
                column: "fecha",
                value: new DateTime(2026, 9, 4, 0, 38, 0, 277, DateTimeKind.Utc).AddTicks(1569));

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
    }
}
