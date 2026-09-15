using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SubastaYa.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pujas_Subastas_SubastaId1",
                table: "Pujas");

            migrationBuilder.DropIndex(
                name: "IX_Pujas_SubastaId1",
                table: "Pujas");

            migrationBuilder.DropColumn(
                name: "SubastaId1",
                table: "Pujas");

            migrationBuilder.UpdateData(
                table: "Auditorias",
                keyColumn: "AuditoriaId",
                keyValue: 1,
                column: "Fecha",
                value: new DateTime(2026, 9, 15, 3, 29, 57, 334, DateTimeKind.Utc).AddTicks(879));

            migrationBuilder.UpdateData(
                table: "Auditorias",
                keyColumn: "AuditoriaId",
                keyValue: 2,
                column: "Fecha",
                value: new DateTime(2026, 9, 15, 3, 29, 57, 334, DateTimeKind.Utc).AddTicks(1216));

            migrationBuilder.UpdateData(
                table: "Auditorias",
                keyColumn: "AuditoriaId",
                keyValue: 3,
                column: "Fecha",
                value: new DateTime(2026, 9, 15, 3, 29, 57, 334, DateTimeKind.Utc).AddTicks(1217));

            migrationBuilder.UpdateData(
                table: "Pujas",
                keyColumn: "PujaId",
                keyValue: 1,
                column: "FechaPuja",
                value: new DateTime(2026, 9, 15, 3, 19, 57, 334, DateTimeKind.Utc).AddTicks(2985));

            migrationBuilder.UpdateData(
                table: "Pujas",
                keyColumn: "PujaId",
                keyValue: 2,
                column: "FechaPuja",
                value: new DateTime(2026, 9, 15, 3, 24, 57, 334, DateTimeKind.Utc).AddTicks(3713));

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 1,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2026, 9, 15, 3, 59, 57, 333, DateTimeKind.Utc).AddTicks(7759), new DateTime(2026, 9, 15, 3, 29, 57, 333, DateTimeKind.Utc).AddTicks(7379) });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 2,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2026, 9, 15, 3, 31, 15, 333, DateTimeKind.Utc).AddTicks(8216), new DateTime(2026, 9, 15, 3, 29, 57, 333, DateTimeKind.Utc).AddTicks(8215) });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 3,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2026, 9, 18, 3, 29, 57, 333, DateTimeKind.Utc).AddTicks(8235), new DateTime(2026, 9, 16, 3, 29, 57, 333, DateTimeKind.Utc).AddTicks(8223) });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 4,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2026, 9, 12, 3, 29, 57, 333, DateTimeKind.Utc).AddTicks(8239), new DateTime(2026, 9, 10, 3, 29, 57, 333, DateTimeKind.Utc).AddTicks(8238) });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 5,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2026, 9, 14, 3, 29, 57, 333, DateTimeKind.Utc).AddTicks(8241), new DateTime(2026, 9, 13, 3, 29, 57, 333, DateTimeKind.Utc).AddTicks(8241) });

            migrationBuilder.UpdateData(
                table: "TransaccionesLedger",
                keyColumn: "TransaccionId",
                keyValue: 1,
                column: "fecha",
                value: new DateTime(2026, 9, 14, 3, 29, 57, 334, DateTimeKind.Utc).AddTicks(6210));

            migrationBuilder.UpdateData(
                table: "TransaccionesLedger",
                keyColumn: "TransaccionId",
                keyValue: 2,
                column: "fecha",
                value: new DateTime(2026, 9, 14, 3, 29, 57, 334, DateTimeKind.Utc).AddTicks(6549));

            migrationBuilder.UpdateData(
                table: "TransaccionesLedger",
                keyColumn: "TransaccionId",
                keyValue: 3,
                column: "fecha",
                value: new DateTime(2026, 9, 14, 3, 29, 57, 334, DateTimeKind.Utc).AddTicks(6556));

            migrationBuilder.UpdateData(
                table: "TransaccionesLedger",
                keyColumn: "TransaccionId",
                keyValue: 4,
                column: "fecha",
                value: new DateTime(2026, 9, 15, 3, 24, 57, 334, DateTimeKind.Utc).AddTicks(6559));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 1,
                columns: new[] { "ContrasenaHash", "FechaRegistro" },
                values: new object[] { "$2a$11$5sd.bS..HaglyD75PpsghuqR258dlcM2QpAns9y.EHjHRClYUk0dC", new DateTime(2026, 9, 15, 3, 29, 57, 23, DateTimeKind.Utc).AddTicks(7036) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 2,
                columns: new[] { "ContrasenaHash", "FechaRegistro" },
                values: new object[] { "$2a$11$BXzcdwDgo4JbEy2tzNhHJO9ZuyQ5t3GfM2enqMgHh7.k5QbOcG552", new DateTime(2026, 9, 15, 3, 29, 57, 126, DateTimeKind.Utc).AddTicks(4106) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 3,
                columns: new[] { "ContrasenaHash", "FechaRegistro" },
                values: new object[] { "$2a$11$ReqRYhkBqa9EDNyWNv2Kr.PueVHePuOQdHra05unYu7JpIzA1Dc7S", new DateTime(2026, 9, 15, 3, 29, 57, 229, DateTimeKind.Utc).AddTicks(1940) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 4,
                columns: new[] { "ContrasenaHash", "FechaRegistro" },
                values: new object[] { "$2a$11$wu3Fm26IcavtvTFW3cpWV.KiG1Lo4ubdlPlD6cjLRn4NzVr7WSJ3K", new DateTime(2026, 9, 15, 3, 29, 57, 332, DateTimeKind.Utc).AddTicks(7756) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubastaId1",
                table: "Pujas",
                type: "integer",
                nullable: true);

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
                columns: new[] { "FechaPuja", "SubastaId1" },
                values: new object[] { new DateTime(2026, 9, 15, 3, 6, 20, 135, DateTimeKind.Utc).AddTicks(4184), null });

            migrationBuilder.UpdateData(
                table: "Pujas",
                keyColumn: "PujaId",
                keyValue: 2,
                columns: new[] { "FechaPuja", "SubastaId1" },
                values: new object[] { new DateTime(2026, 9, 15, 3, 11, 20, 135, DateTimeKind.Utc).AddTicks(4909), null });

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

            migrationBuilder.CreateIndex(
                name: "IX_Pujas_SubastaId1",
                table: "Pujas",
                column: "SubastaId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Pujas_Subastas_SubastaId1",
                table: "Pujas",
                column: "SubastaId1",
                principalTable: "Subastas",
                principalColumn: "SubastaId");
        }
    }
}
