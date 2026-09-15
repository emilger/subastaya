using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SubastaYa.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Auditorias",
                keyColumn: "AuditoriaId",
                keyValue: 1,
                column: "Fecha",
                value: new DateTime(2026, 9, 15, 3, 16, 20, 135, DateTimeKind.Utc).AddTicks(2062));

            migrationBuilder.UpdateData(
                table: "Auditorias",
                keyColumn: "AuditoriaId",
                keyValue: 2,
                column: "Fecha",
                value: new DateTime(2026, 9, 15, 3, 16, 20, 135, DateTimeKind.Utc).AddTicks(2402));

            migrationBuilder.UpdateData(
                table: "Auditorias",
                keyColumn: "AuditoriaId",
                keyValue: 3,
                column: "Fecha",
                value: new DateTime(2026, 9, 15, 3, 16, 20, 135, DateTimeKind.Utc).AddTicks(2404));

            migrationBuilder.UpdateData(
                table: "Pujas",
                keyColumn: "PujaId",
                keyValue: 1,
                column: "FechaPuja",
                value: new DateTime(2026, 9, 15, 3, 6, 20, 135, DateTimeKind.Utc).AddTicks(4184));

            migrationBuilder.UpdateData(
                table: "Pujas",
                keyColumn: "PujaId",
                keyValue: 2,
                column: "FechaPuja",
                value: new DateTime(2026, 9, 15, 3, 11, 20, 135, DateTimeKind.Utc).AddTicks(4909));

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 1,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2026, 9, 15, 3, 46, 20, 134, DateTimeKind.Utc).AddTicks(8255), new DateTime(2026, 9, 15, 3, 16, 20, 134, DateTimeKind.Utc).AddTicks(7911) });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 2,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2026, 9, 15, 3, 17, 38, 134, DateTimeKind.Utc).AddTicks(8834), new DateTime(2026, 9, 15, 3, 16, 20, 134, DateTimeKind.Utc).AddTicks(8806) });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 3,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2026, 9, 18, 3, 16, 20, 134, DateTimeKind.Utc).AddTicks(8857), new DateTime(2026, 9, 16, 3, 16, 20, 134, DateTimeKind.Utc).AddTicks(8841) });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 4,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2026, 9, 12, 3, 16, 20, 134, DateTimeKind.Utc).AddTicks(8860), new DateTime(2026, 9, 10, 3, 16, 20, 134, DateTimeKind.Utc).AddTicks(8860) });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 5,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2026, 9, 14, 3, 16, 20, 134, DateTimeKind.Utc).AddTicks(8863), new DateTime(2026, 9, 13, 3, 16, 20, 134, DateTimeKind.Utc).AddTicks(8862) });

            migrationBuilder.UpdateData(
                table: "TransaccionesLedger",
                keyColumn: "TransaccionId",
                keyValue: 1,
                column: "fecha",
                value: new DateTime(2026, 9, 14, 3, 16, 20, 135, DateTimeKind.Utc).AddTicks(7402));

            migrationBuilder.UpdateData(
                table: "TransaccionesLedger",
                keyColumn: "TransaccionId",
                keyValue: 2,
                column: "fecha",
                value: new DateTime(2026, 9, 14, 3, 16, 20, 135, DateTimeKind.Utc).AddTicks(7738));

            migrationBuilder.UpdateData(
                table: "TransaccionesLedger",
                keyColumn: "TransaccionId",
                keyValue: 3,
                column: "fecha",
                value: new DateTime(2026, 9, 14, 3, 16, 20, 135, DateTimeKind.Utc).AddTicks(7748));

            migrationBuilder.UpdateData(
                table: "TransaccionesLedger",
                keyColumn: "TransaccionId",
                keyValue: 4,
                column: "fecha",
                value: new DateTime(2026, 9, 15, 3, 11, 20, 135, DateTimeKind.Utc).AddTicks(7750));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 1,
                columns: new[] { "ContrasenaHash", "FechaRegistro" },
                values: new object[] { "$2a$11$XT.B4GKC8SGT2DeyAWSKP.GqNmOGuITrVC5yxwNWa879MmKDm1OwK", new DateTime(2026, 9, 15, 3, 16, 19, 826, DateTimeKind.Utc).AddTicks(6803) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 2,
                columns: new[] { "ContrasenaHash", "FechaRegistro" },
                values: new object[] { "$2a$11$N/yUlrjnd6Q/nzm.479D1.rPaRe2fgP.78O0OgJpn8KD5zTiudL6K", new DateTime(2026, 9, 15, 3, 16, 19, 929, DateTimeKind.Utc).AddTicks(298) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 3,
                columns: new[] { "ContrasenaHash", "FechaRegistro" },
                values: new object[] { "$2a$11$TRTzxQE5dHnf2LJzpMswFuqdXtcA1Bg72dJbDW0.1M9tzutUsahd.", new DateTime(2026, 9, 15, 3, 16, 20, 31, DateTimeKind.Utc).AddTicks(994) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 4,
                columns: new[] { "ContrasenaHash", "FechaRegistro" },
                values: new object[] { "$2a$11$o2Oedu5tKYGHKlEAzyWF5Ox1lAqTXJZpYqn0V.q7ab/Vky2rxCmxm", new DateTime(2026, 9, 15, 3, 16, 20, 133, DateTimeKind.Utc).AddTicks(7714) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Auditorias",
                keyColumn: "AuditoriaId",
                keyValue: 1,
                column: "Fecha",
                value: new DateTime(2026, 9, 15, 3, 13, 28, 189, DateTimeKind.Utc).AddTicks(8042));

            migrationBuilder.UpdateData(
                table: "Auditorias",
                keyColumn: "AuditoriaId",
                keyValue: 2,
                column: "Fecha",
                value: new DateTime(2026, 9, 15, 3, 13, 28, 189, DateTimeKind.Utc).AddTicks(8388));

            migrationBuilder.UpdateData(
                table: "Auditorias",
                keyColumn: "AuditoriaId",
                keyValue: 3,
                column: "Fecha",
                value: new DateTime(2026, 9, 15, 3, 13, 28, 189, DateTimeKind.Utc).AddTicks(8390));

            migrationBuilder.UpdateData(
                table: "Pujas",
                keyColumn: "PujaId",
                keyValue: 1,
                column: "FechaPuja",
                value: new DateTime(2026, 9, 15, 3, 3, 28, 190, DateTimeKind.Utc).AddTicks(159));

            migrationBuilder.UpdateData(
                table: "Pujas",
                keyColumn: "PujaId",
                keyValue: 2,
                column: "FechaPuja",
                value: new DateTime(2026, 9, 15, 3, 8, 28, 190, DateTimeKind.Utc).AddTicks(1013));

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 1,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2026, 9, 15, 3, 43, 28, 189, DateTimeKind.Utc).AddTicks(4867), new DateTime(2026, 9, 15, 3, 13, 28, 189, DateTimeKind.Utc).AddTicks(4539) });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 2,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2026, 9, 15, 3, 14, 46, 189, DateTimeKind.Utc).AddTicks(5351), new DateTime(2026, 9, 15, 3, 13, 28, 189, DateTimeKind.Utc).AddTicks(5350) });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 3,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2026, 9, 18, 3, 13, 28, 189, DateTimeKind.Utc).AddTicks(5372), new DateTime(2026, 9, 16, 3, 13, 28, 189, DateTimeKind.Utc).AddTicks(5358) });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 4,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2026, 9, 12, 3, 13, 28, 189, DateTimeKind.Utc).AddTicks(5375), new DateTime(2026, 9, 10, 3, 13, 28, 189, DateTimeKind.Utc).AddTicks(5375) });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 5,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2026, 9, 14, 3, 13, 28, 189, DateTimeKind.Utc).AddTicks(5378), new DateTime(2026, 9, 13, 3, 13, 28, 189, DateTimeKind.Utc).AddTicks(5377) });

            migrationBuilder.UpdateData(
                table: "TransaccionesLedger",
                keyColumn: "TransaccionId",
                keyValue: 1,
                column: "fecha",
                value: new DateTime(2026, 9, 14, 3, 13, 28, 190, DateTimeKind.Utc).AddTicks(4209));

            migrationBuilder.UpdateData(
                table: "TransaccionesLedger",
                keyColumn: "TransaccionId",
                keyValue: 2,
                column: "fecha",
                value: new DateTime(2026, 9, 14, 3, 13, 28, 190, DateTimeKind.Utc).AddTicks(4545));

            migrationBuilder.UpdateData(
                table: "TransaccionesLedger",
                keyColumn: "TransaccionId",
                keyValue: 3,
                column: "fecha",
                value: new DateTime(2026, 9, 14, 3, 13, 28, 190, DateTimeKind.Utc).AddTicks(4554));

            migrationBuilder.UpdateData(
                table: "TransaccionesLedger",
                keyColumn: "TransaccionId",
                keyValue: 4,
                column: "fecha",
                value: new DateTime(2026, 9, 15, 3, 8, 28, 190, DateTimeKind.Utc).AddTicks(4558));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 1,
                columns: new[] { "ContrasenaHash", "FechaRegistro" },
                values: new object[] { "$2a$11$8RRPiIbRSXxpzzc4anI4z.8yHBIvGjvpGxUqWUtcKr5GXFojMG616", new DateTime(2026, 9, 15, 3, 13, 27, 799, DateTimeKind.Utc).AddTicks(9582) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 2,
                columns: new[] { "ContrasenaHash", "FechaRegistro" },
                values: new object[] { "$2a$11$.p0ayeKgRY3oZKxF9VGLlOCAPy0nkYPxKP9.kdf0rjBlZaxh49F/2", new DateTime(2026, 9, 15, 3, 13, 27, 984, DateTimeKind.Utc).AddTicks(8744) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 3,
                columns: new[] { "ContrasenaHash", "FechaRegistro" },
                values: new object[] { "$2a$11$gx/D3/oY/n92DGdyBqdFO.9sb6Z47CLSJd95ur23mi1SFYuB1rBOe", new DateTime(2026, 9, 15, 3, 13, 28, 87, DateTimeKind.Utc).AddTicks(3855) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 4,
                columns: new[] { "ContrasenaHash", "FechaRegistro" },
                values: new object[] { "$2a$11$NGsvZ4YWO3v2zNDQx411belX39IdAZE3zzKYEuLuqe1AH629ZUs82", new DateTime(2026, 9, 15, 3, 13, 28, 188, DateTimeKind.Utc).AddTicks(5375) });
        }
    }
}
