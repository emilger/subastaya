using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SubastaYa.API.Migrations
{
    /// <inheritdoc />
    public partial class AjustarRelacionSubasta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Subastas",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Auditorias",
                keyColumn: "AuditoriaId",
                keyValue: 1,
                column: "Fecha",
                value: new DateTime(2026, 9, 15, 2, 48, 28, 559, DateTimeKind.Utc).AddTicks(7723));

            migrationBuilder.UpdateData(
                table: "Auditorias",
                keyColumn: "AuditoriaId",
                keyValue: 2,
                column: "Fecha",
                value: new DateTime(2026, 9, 15, 2, 48, 28, 559, DateTimeKind.Utc).AddTicks(8059));

            migrationBuilder.UpdateData(
                table: "Auditorias",
                keyColumn: "AuditoriaId",
                keyValue: 3,
                column: "Fecha",
                value: new DateTime(2026, 9, 15, 2, 48, 28, 559, DateTimeKind.Utc).AddTicks(8061));

            migrationBuilder.UpdateData(
                table: "Pujas",
                keyColumn: "PujaId",
                keyValue: 1,
                column: "FechaPuja",
                value: new DateTime(2026, 9, 15, 2, 38, 28, 559, DateTimeKind.Utc).AddTicks(9760));

            migrationBuilder.UpdateData(
                table: "Pujas",
                keyColumn: "PujaId",
                keyValue: 2,
                column: "FechaPuja",
                value: new DateTime(2026, 9, 15, 2, 43, 28, 560, DateTimeKind.Utc).AddTicks(471));

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 1,
                columns: new[] { "FechaFin", "FechaInicio", "UsuarioId" },
                values: new object[] { new DateTime(2026, 9, 15, 3, 18, 28, 559, DateTimeKind.Utc).AddTicks(4555), new DateTime(2026, 9, 15, 2, 48, 28, 559, DateTimeKind.Utc).AddTicks(4222), null });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 2,
                columns: new[] { "FechaFin", "FechaInicio", "UsuarioId" },
                values: new object[] { new DateTime(2026, 9, 15, 2, 49, 46, 559, DateTimeKind.Utc).AddTicks(5020), new DateTime(2026, 9, 15, 2, 48, 28, 559, DateTimeKind.Utc).AddTicks(5019), null });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 3,
                columns: new[] { "FechaFin", "FechaInicio", "UsuarioId" },
                values: new object[] { new DateTime(2026, 9, 18, 2, 48, 28, 559, DateTimeKind.Utc).AddTicks(5054), new DateTime(2026, 9, 16, 2, 48, 28, 559, DateTimeKind.Utc).AddTicks(5027), null });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 4,
                columns: new[] { "FechaFin", "FechaInicio", "UsuarioId" },
                values: new object[] { new DateTime(2026, 9, 12, 2, 48, 28, 559, DateTimeKind.Utc).AddTicks(5059), new DateTime(2026, 9, 10, 2, 48, 28, 559, DateTimeKind.Utc).AddTicks(5059), null });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 5,
                columns: new[] { "FechaFin", "FechaInicio", "UsuarioId" },
                values: new object[] { new DateTime(2026, 9, 14, 2, 48, 28, 559, DateTimeKind.Utc).AddTicks(5063), new DateTime(2026, 9, 13, 2, 48, 28, 559, DateTimeKind.Utc).AddTicks(5062), null });

            migrationBuilder.UpdateData(
                table: "TransaccionesLedger",
                keyColumn: "TransaccionId",
                keyValue: 1,
                column: "fecha",
                value: new DateTime(2026, 9, 14, 2, 48, 28, 560, DateTimeKind.Utc).AddTicks(2929));

            migrationBuilder.UpdateData(
                table: "TransaccionesLedger",
                keyColumn: "TransaccionId",
                keyValue: 2,
                column: "fecha",
                value: new DateTime(2026, 9, 14, 2, 48, 28, 560, DateTimeKind.Utc).AddTicks(3263));

            migrationBuilder.UpdateData(
                table: "TransaccionesLedger",
                keyColumn: "TransaccionId",
                keyValue: 3,
                column: "fecha",
                value: new DateTime(2026, 9, 14, 2, 48, 28, 560, DateTimeKind.Utc).AddTicks(3272));

            migrationBuilder.UpdateData(
                table: "TransaccionesLedger",
                keyColumn: "TransaccionId",
                keyValue: 4,
                column: "fecha",
                value: new DateTime(2026, 9, 15, 2, 43, 28, 560, DateTimeKind.Utc).AddTicks(3274));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 1,
                columns: new[] { "ContrasenaHash", "FechaRegistro" },
                values: new object[] { "$2a$11$JDrsc5cOaK5WOV.Vt3jTJuADQu32MPV95oKUAv2idO.sXKR55CIPW", new DateTime(2026, 9, 15, 2, 48, 28, 251, DateTimeKind.Utc).AddTicks(9496) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 2,
                columns: new[] { "ContrasenaHash", "FechaRegistro" },
                values: new object[] { "$2a$11$ThvPGUByAqbXuOGnt5IRreDM29KjN75/IWT904ss1AOOoBq0feLr2", new DateTime(2026, 9, 15, 2, 48, 28, 354, DateTimeKind.Utc).AddTicks(8340) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 3,
                columns: new[] { "ContrasenaHash", "FechaRegistro" },
                values: new object[] { "$2a$11$RmAKHkpGFhiGOhQmk2gXmuC4HKlTyRXi0nzfOaGmd6RiBxYsbMriu", new DateTime(2026, 9, 15, 2, 48, 28, 457, DateTimeKind.Utc).AddTicks(272) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 4,
                columns: new[] { "ContrasenaHash", "FechaRegistro" },
                values: new object[] { "$2a$11$/j7t4NBxhHO7p.wflNjs.OztGv1WvMf8ypuGag8D9v7HDdqIpOFFS", new DateTime(2026, 9, 15, 2, 48, 28, 558, DateTimeKind.Utc).AddTicks(4932) });

            migrationBuilder.CreateIndex(
                name: "IX_Subastas_UsuarioId",
                table: "Subastas",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subastas_Usuarios_UsuarioId",
                table: "Subastas",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subastas_Usuarios_UsuarioId",
                table: "Subastas");

            migrationBuilder.DropIndex(
                name: "IX_Subastas_UsuarioId",
                table: "Subastas");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Subastas");

            migrationBuilder.UpdateData(
                table: "Auditorias",
                keyColumn: "AuditoriaId",
                keyValue: 1,
                column: "Fecha",
                value: new DateTime(2026, 9, 15, 2, 40, 14, 215, DateTimeKind.Utc).AddTicks(7980));

            migrationBuilder.UpdateData(
                table: "Auditorias",
                keyColumn: "AuditoriaId",
                keyValue: 2,
                column: "Fecha",
                value: new DateTime(2026, 9, 15, 2, 40, 14, 215, DateTimeKind.Utc).AddTicks(8314));

            migrationBuilder.UpdateData(
                table: "Auditorias",
                keyColumn: "AuditoriaId",
                keyValue: 3,
                column: "Fecha",
                value: new DateTime(2026, 9, 15, 2, 40, 14, 215, DateTimeKind.Utc).AddTicks(8316));

            migrationBuilder.UpdateData(
                table: "Pujas",
                keyColumn: "PujaId",
                keyValue: 1,
                column: "FechaPuja",
                value: new DateTime(2026, 9, 15, 2, 30, 14, 216, DateTimeKind.Utc).AddTicks(67));

            migrationBuilder.UpdateData(
                table: "Pujas",
                keyColumn: "PujaId",
                keyValue: 2,
                column: "FechaPuja",
                value: new DateTime(2026, 9, 15, 2, 35, 14, 216, DateTimeKind.Utc).AddTicks(791));

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 1,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2026, 9, 15, 3, 10, 14, 215, DateTimeKind.Utc).AddTicks(4598), new DateTime(2026, 9, 15, 2, 40, 14, 215, DateTimeKind.Utc).AddTicks(4258) });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 2,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2026, 9, 15, 2, 41, 32, 215, DateTimeKind.Utc).AddTicks(5039), new DateTime(2026, 9, 15, 2, 40, 14, 215, DateTimeKind.Utc).AddTicks(5038) });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 3,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2026, 9, 18, 2, 40, 14, 215, DateTimeKind.Utc).AddTicks(5059), new DateTime(2026, 9, 16, 2, 40, 14, 215, DateTimeKind.Utc).AddTicks(5047) });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 4,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2026, 9, 12, 2, 40, 14, 215, DateTimeKind.Utc).AddTicks(5062), new DateTime(2026, 9, 10, 2, 40, 14, 215, DateTimeKind.Utc).AddTicks(5062) });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 5,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2026, 9, 14, 2, 40, 14, 215, DateTimeKind.Utc).AddTicks(5065), new DateTime(2026, 9, 13, 2, 40, 14, 215, DateTimeKind.Utc).AddTicks(5064) });

            migrationBuilder.UpdateData(
                table: "TransaccionesLedger",
                keyColumn: "TransaccionId",
                keyValue: 1,
                column: "fecha",
                value: new DateTime(2026, 9, 14, 2, 40, 14, 216, DateTimeKind.Utc).AddTicks(3284));

            migrationBuilder.UpdateData(
                table: "TransaccionesLedger",
                keyColumn: "TransaccionId",
                keyValue: 2,
                column: "fecha",
                value: new DateTime(2026, 9, 14, 2, 40, 14, 216, DateTimeKind.Utc).AddTicks(3618));

            migrationBuilder.UpdateData(
                table: "TransaccionesLedger",
                keyColumn: "TransaccionId",
                keyValue: 3,
                column: "fecha",
                value: new DateTime(2026, 9, 14, 2, 40, 14, 216, DateTimeKind.Utc).AddTicks(3625));

            migrationBuilder.UpdateData(
                table: "TransaccionesLedger",
                keyColumn: "TransaccionId",
                keyValue: 4,
                column: "fecha",
                value: new DateTime(2026, 9, 15, 2, 35, 14, 216, DateTimeKind.Utc).AddTicks(3628));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 1,
                columns: new[] { "ContrasenaHash", "FechaRegistro" },
                values: new object[] { "$2a$11$Oa7dCfrkXiRcdMc39040AuMF8eAhG9AZObJDTuBu9XRBf6S0I.kXi", new DateTime(2026, 9, 15, 2, 40, 13, 908, DateTimeKind.Utc).AddTicks(7308) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 2,
                columns: new[] { "ContrasenaHash", "FechaRegistro" },
                values: new object[] { "$2a$11$kKOB2IK.B6p2K2ZHhjCSD.r4SsxO4bN05j33gl0trujLMXSwaVggm", new DateTime(2026, 9, 15, 2, 40, 14, 11, DateTimeKind.Utc).AddTicks(3366) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 3,
                columns: new[] { "ContrasenaHash", "FechaRegistro" },
                values: new object[] { "$2a$11$0p9ZTnHE4azHOoDg8JToaedy4JMZdSZKrkp1a6ikdJv0/XINwLSlm", new DateTime(2026, 9, 15, 2, 40, 14, 112, DateTimeKind.Utc).AddTicks(8528) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 4,
                columns: new[] { "ContrasenaHash", "FechaRegistro" },
                values: new object[] { "$2a$11$pBmOwTx5cH70.zBZ7Y/zj.y7t7MdRKcxev.RJwAwNe/VkqKf/6h9S", new DateTime(2026, 9, 15, 2, 40, 14, 214, DateTimeKind.Utc).AddTicks(5567) });
        }
    }
}
