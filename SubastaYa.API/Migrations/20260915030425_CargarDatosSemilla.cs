using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SubastaYa.API.Migrations
{
    /// <inheritdoc />
    public partial class CargarDatosSemilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auditorias_Usuarios_UsuarioFK",
                table: "Auditorias");

            migrationBuilder.DropForeignKey(
                name: "FK_Billeteras_Usuarios_UsuarioFK",
                table: "Billeteras");

            migrationBuilder.DropForeignKey(
                name: "FK_Pujas_Subastas_SubastaFK",
                table: "Pujas");

            migrationBuilder.DropForeignKey(
                name: "FK_Pujas_Subastas_SubastaId",
                table: "Pujas");

            migrationBuilder.DropForeignKey(
                name: "FK_Pujas_Usuarios_CompradorFK",
                table: "Pujas");

            migrationBuilder.DropForeignKey(
                name: "FK_TransaccionesLedger_Billeteras_BilleteraFK",
                table: "TransaccionesLedger");

            migrationBuilder.DropForeignKey(
                name: "FK_TransaccionesLedger_Subastas_SubastaFK",
                table: "TransaccionesLedger");

            migrationBuilder.DropIndex(
                name: "IX_TransaccionesLedger_SubastaFK",
                table: "TransaccionesLedger");

            migrationBuilder.DropIndex(
                name: "IX_Pujas_CompradorFK",
                table: "Pujas");

            migrationBuilder.DropIndex(
                name: "IX_Pujas_SubastaFK",
                table: "Pujas");

            migrationBuilder.DropIndex(
                name: "IX_Billeteras_UsuarioFK",
                table: "Billeteras");

            migrationBuilder.DropIndex(
                name: "IX_Auditorias_UsuarioFK",
                table: "Auditorias");

            migrationBuilder.DropColumn(
                name: "BilleteraFk",
                table: "TransaccionesLedger");

            migrationBuilder.DropColumn(
                name: "SubastaFK",
                table: "TransaccionesLedger");

            migrationBuilder.DropColumn(
                name: "CompradorFK",
                table: "Pujas");

            migrationBuilder.DropColumn(
                name: "SubastaFK",
                table: "Pujas");

            migrationBuilder.DropColumn(
                name: "UsuarioFK",
                table: "Billeteras");

            migrationBuilder.DropColumn(
                name: "UsuarioFK",
                table: "Auditorias");

            migrationBuilder.RenameColumn(
                name: "SubastaFk",
                table: "TransaccionesLedger",
                newName: "SubastaId");

            migrationBuilder.RenameColumn(
                name: "BilleteraFK",
                table: "TransaccionesLedger",
                newName: "BilleteraId");

            migrationBuilder.RenameIndex(
                name: "IX_TransaccionesLedger_BilleteraFK",
                table: "TransaccionesLedger",
                newName: "IX_TransaccionesLedger_BilleteraId");

            migrationBuilder.AlterColumn<int>(
                name: "SubastaId",
                table: "Pujas",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CompradorId",
                table: "Pujas",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubastaId1",
                table: "Pujas",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Billeteras",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Auditorias",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Auditorias",
                keyColumn: "AuditoriaId",
                keyValue: 1,
                columns: new[] { "Fecha", "UsuarioId" },
                values: new object[] { new DateTime(2026, 9, 15, 3, 4, 24, 668, DateTimeKind.Utc).AddTicks(5868), 1 });

            migrationBuilder.UpdateData(
                table: "Auditorias",
                keyColumn: "AuditoriaId",
                keyValue: 2,
                columns: new[] { "Fecha", "UsuarioId" },
                values: new object[] { new DateTime(2026, 9, 15, 3, 4, 24, 668, DateTimeKind.Utc).AddTicks(6199), 1 });

            migrationBuilder.UpdateData(
                table: "Auditorias",
                keyColumn: "AuditoriaId",
                keyValue: 3,
                columns: new[] { "Fecha", "UsuarioId" },
                values: new object[] { new DateTime(2026, 9, 15, 3, 4, 24, 668, DateTimeKind.Utc).AddTicks(6200), 1 });

            migrationBuilder.UpdateData(
                table: "Billeteras",
                keyColumn: "BilleteraId",
                keyValue: 1,
                column: "UsuarioId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Billeteras",
                keyColumn: "BilleteraId",
                keyValue: 2,
                column: "UsuarioId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Billeteras",
                keyColumn: "BilleteraId",
                keyValue: 3,
                column: "UsuarioId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Billeteras",
                keyColumn: "BilleteraId",
                keyValue: 4,
                column: "UsuarioId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Pujas",
                keyColumn: "PujaId",
                keyValue: 1,
                columns: new[] { "CompradorId", "FechaPuja", "SubastaId", "SubastaId1" },
                values: new object[] { 3, new DateTime(2026, 9, 15, 2, 54, 24, 668, DateTimeKind.Utc).AddTicks(7932), 1, null });

            migrationBuilder.UpdateData(
                table: "Pujas",
                keyColumn: "PujaId",
                keyValue: 2,
                columns: new[] { "CompradorId", "FechaPuja", "SubastaId", "SubastaId1" },
                values: new object[] { 2, new DateTime(2026, 9, 15, 2, 59, 24, 668, DateTimeKind.Utc).AddTicks(8660), 1, null });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 1,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2026, 9, 15, 3, 34, 24, 668, DateTimeKind.Utc).AddTicks(2423), new DateTime(2026, 9, 15, 3, 4, 24, 668, DateTimeKind.Utc).AddTicks(2078) });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 2,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2026, 9, 15, 3, 5, 42, 668, DateTimeKind.Utc).AddTicks(2937), new DateTime(2026, 9, 15, 3, 4, 24, 668, DateTimeKind.Utc).AddTicks(2936) });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 3,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2026, 9, 18, 3, 4, 24, 668, DateTimeKind.Utc).AddTicks(2957), new DateTime(2026, 9, 16, 3, 4, 24, 668, DateTimeKind.Utc).AddTicks(2944) });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 4,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2026, 9, 12, 3, 4, 24, 668, DateTimeKind.Utc).AddTicks(2960), new DateTime(2026, 9, 10, 3, 4, 24, 668, DateTimeKind.Utc).AddTicks(2960) });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 5,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2026, 9, 14, 3, 4, 24, 668, DateTimeKind.Utc).AddTicks(2963), new DateTime(2026, 9, 13, 3, 4, 24, 668, DateTimeKind.Utc).AddTicks(2962) });

            migrationBuilder.UpdateData(
                table: "TransaccionesLedger",
                keyColumn: "TransaccionId",
                keyValue: 1,
                column: "fecha",
                value: new DateTime(2026, 9, 14, 3, 4, 24, 669, DateTimeKind.Utc).AddTicks(1182));

            migrationBuilder.UpdateData(
                table: "TransaccionesLedger",
                keyColumn: "TransaccionId",
                keyValue: 2,
                column: "fecha",
                value: new DateTime(2026, 9, 14, 3, 4, 24, 669, DateTimeKind.Utc).AddTicks(1515));

            migrationBuilder.UpdateData(
                table: "TransaccionesLedger",
                keyColumn: "TransaccionId",
                keyValue: 3,
                column: "fecha",
                value: new DateTime(2026, 9, 14, 3, 4, 24, 669, DateTimeKind.Utc).AddTicks(1526));

            migrationBuilder.UpdateData(
                table: "TransaccionesLedger",
                keyColumn: "TransaccionId",
                keyValue: 4,
                columns: new[] { "SubastaId", "fecha" },
                values: new object[] { 1, new DateTime(2026, 9, 15, 2, 59, 24, 669, DateTimeKind.Utc).AddTicks(1528) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 1,
                columns: new[] { "ContrasenaHash", "FechaRegistro" },
                values: new object[] { "$2a$11$cmtmkFOXBv4jZn66O44e6ejI3vQ66bsZke7QdS4eqqQSSUFfdNSq6", new DateTime(2026, 9, 15, 3, 4, 24, 361, DateTimeKind.Utc).AddTicks(8414) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 2,
                columns: new[] { "ContrasenaHash", "FechaRegistro" },
                values: new object[] { "$2a$11$QSCnuxBMT6HU8apvGkU.wOLkOfF9lEHkWwpKPN33Dimwz7D2wpxqq", new DateTime(2026, 9, 15, 3, 4, 24, 462, DateTimeKind.Utc).AddTicks(6567) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 3,
                columns: new[] { "ContrasenaHash", "FechaRegistro" },
                values: new object[] { "$2a$11$pu3iwFf0GR9pfp23OsyCB.hhtPIf9VWLGJ1sjugeSpKbXg0ybOX.2", new DateTime(2026, 9, 15, 3, 4, 24, 563, DateTimeKind.Utc).AddTicks(6462) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 4,
                columns: new[] { "ContrasenaHash", "FechaRegistro" },
                values: new object[] { "$2a$11$008MDkOyFR5MHkI.05mWxuD8Ka/5CUJbTGkBsCKu6/bhelFLDqJT.", new DateTime(2026, 9, 15, 3, 4, 24, 667, DateTimeKind.Utc).AddTicks(2308) });

            migrationBuilder.CreateIndex(
                name: "IX_TransaccionesLedger_SubastaId",
                table: "TransaccionesLedger",
                column: "SubastaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pujas_CompradorId",
                table: "Pujas",
                column: "CompradorId");

            migrationBuilder.CreateIndex(
                name: "IX_Pujas_SubastaId1",
                table: "Pujas",
                column: "SubastaId1");

            migrationBuilder.CreateIndex(
                name: "IX_Billeteras_UsuarioId",
                table: "Billeteras",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Auditorias_UsuarioId",
                table: "Auditorias",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Auditorias_Usuarios_UsuarioId",
                table: "Auditorias",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Billeteras_Usuarios_UsuarioId",
                table: "Billeteras",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pujas_Subastas_SubastaId",
                table: "Pujas",
                column: "SubastaId",
                principalTable: "Subastas",
                principalColumn: "SubastaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pujas_Subastas_SubastaId1",
                table: "Pujas",
                column: "SubastaId1",
                principalTable: "Subastas",
                principalColumn: "SubastaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pujas_Usuarios_CompradorId",
                table: "Pujas",
                column: "CompradorId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransaccionesLedger_Billeteras_BilleteraId",
                table: "TransaccionesLedger",
                column: "BilleteraId",
                principalTable: "Billeteras",
                principalColumn: "BilleteraId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransaccionesLedger_Subastas_SubastaId",
                table: "TransaccionesLedger",
                column: "SubastaId",
                principalTable: "Subastas",
                principalColumn: "SubastaId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auditorias_Usuarios_UsuarioId",
                table: "Auditorias");

            migrationBuilder.DropForeignKey(
                name: "FK_Billeteras_Usuarios_UsuarioId",
                table: "Billeteras");

            migrationBuilder.DropForeignKey(
                name: "FK_Pujas_Subastas_SubastaId",
                table: "Pujas");

            migrationBuilder.DropForeignKey(
                name: "FK_Pujas_Subastas_SubastaId1",
                table: "Pujas");

            migrationBuilder.DropForeignKey(
                name: "FK_Pujas_Usuarios_CompradorId",
                table: "Pujas");

            migrationBuilder.DropForeignKey(
                name: "FK_TransaccionesLedger_Billeteras_BilleteraId",
                table: "TransaccionesLedger");

            migrationBuilder.DropForeignKey(
                name: "FK_TransaccionesLedger_Subastas_SubastaId",
                table: "TransaccionesLedger");

            migrationBuilder.DropIndex(
                name: "IX_TransaccionesLedger_SubastaId",
                table: "TransaccionesLedger");

            migrationBuilder.DropIndex(
                name: "IX_Pujas_CompradorId",
                table: "Pujas");

            migrationBuilder.DropIndex(
                name: "IX_Pujas_SubastaId1",
                table: "Pujas");

            migrationBuilder.DropIndex(
                name: "IX_Billeteras_UsuarioId",
                table: "Billeteras");

            migrationBuilder.DropIndex(
                name: "IX_Auditorias_UsuarioId",
                table: "Auditorias");

            migrationBuilder.DropColumn(
                name: "SubastaId1",
                table: "Pujas");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Auditorias");

            migrationBuilder.RenameColumn(
                name: "SubastaId",
                table: "TransaccionesLedger",
                newName: "SubastaFk");

            migrationBuilder.RenameColumn(
                name: "BilleteraId",
                table: "TransaccionesLedger",
                newName: "BilleteraFK");

            migrationBuilder.RenameIndex(
                name: "IX_TransaccionesLedger_BilleteraId",
                table: "TransaccionesLedger",
                newName: "IX_TransaccionesLedger_BilleteraFK");

            migrationBuilder.AddColumn<int>(
                name: "BilleteraFk",
                table: "TransaccionesLedger",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubastaFK",
                table: "TransaccionesLedger",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SubastaId",
                table: "Pujas",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "CompradorId",
                table: "Pujas",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "CompradorFK",
                table: "Pujas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SubastaFK",
                table: "Pujas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Billeteras",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioFK",
                table: "Billeteras",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioFK",
                table: "Auditorias",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Auditorias",
                keyColumn: "AuditoriaId",
                keyValue: 1,
                columns: new[] { "Fecha", "UsuarioFK" },
                values: new object[] { new DateTime(2026, 9, 15, 2, 48, 28, 559, DateTimeKind.Utc).AddTicks(7723), 1 });

            migrationBuilder.UpdateData(
                table: "Auditorias",
                keyColumn: "AuditoriaId",
                keyValue: 2,
                columns: new[] { "Fecha", "UsuarioFK" },
                values: new object[] { new DateTime(2026, 9, 15, 2, 48, 28, 559, DateTimeKind.Utc).AddTicks(8059), 1 });

            migrationBuilder.UpdateData(
                table: "Auditorias",
                keyColumn: "AuditoriaId",
                keyValue: 3,
                columns: new[] { "Fecha", "UsuarioFK" },
                values: new object[] { new DateTime(2026, 9, 15, 2, 48, 28, 559, DateTimeKind.Utc).AddTicks(8061), 1 });

            migrationBuilder.UpdateData(
                table: "Billeteras",
                keyColumn: "BilleteraId",
                keyValue: 1,
                columns: new[] { "UsuarioFK", "UsuarioId" },
                values: new object[] { 1, null });

            migrationBuilder.UpdateData(
                table: "Billeteras",
                keyColumn: "BilleteraId",
                keyValue: 2,
                columns: new[] { "UsuarioFK", "UsuarioId" },
                values: new object[] { 2, null });

            migrationBuilder.UpdateData(
                table: "Billeteras",
                keyColumn: "BilleteraId",
                keyValue: 3,
                columns: new[] { "UsuarioFK", "UsuarioId" },
                values: new object[] { 3, null });

            migrationBuilder.UpdateData(
                table: "Billeteras",
                keyColumn: "BilleteraId",
                keyValue: 4,
                columns: new[] { "UsuarioFK", "UsuarioId" },
                values: new object[] { 4, null });

            migrationBuilder.UpdateData(
                table: "Pujas",
                keyColumn: "PujaId",
                keyValue: 1,
                columns: new[] { "CompradorFK", "CompradorId", "FechaPuja", "SubastaFK", "SubastaId" },
                values: new object[] { 3, null, new DateTime(2026, 9, 15, 2, 38, 28, 559, DateTimeKind.Utc).AddTicks(9760), 1, null });

            migrationBuilder.UpdateData(
                table: "Pujas",
                keyColumn: "PujaId",
                keyValue: 2,
                columns: new[] { "CompradorFK", "CompradorId", "FechaPuja", "SubastaFK", "SubastaId" },
                values: new object[] { 2, null, new DateTime(2026, 9, 15, 2, 43, 28, 560, DateTimeKind.Utc).AddTicks(471), 1, null });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 1,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2026, 9, 15, 3, 18, 28, 559, DateTimeKind.Utc).AddTicks(4555), new DateTime(2026, 9, 15, 2, 48, 28, 559, DateTimeKind.Utc).AddTicks(4222) });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 2,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2026, 9, 15, 2, 49, 46, 559, DateTimeKind.Utc).AddTicks(5020), new DateTime(2026, 9, 15, 2, 48, 28, 559, DateTimeKind.Utc).AddTicks(5019) });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 3,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2026, 9, 18, 2, 48, 28, 559, DateTimeKind.Utc).AddTicks(5054), new DateTime(2026, 9, 16, 2, 48, 28, 559, DateTimeKind.Utc).AddTicks(5027) });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 4,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2026, 9, 12, 2, 48, 28, 559, DateTimeKind.Utc).AddTicks(5059), new DateTime(2026, 9, 10, 2, 48, 28, 559, DateTimeKind.Utc).AddTicks(5059) });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 5,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2026, 9, 14, 2, 48, 28, 559, DateTimeKind.Utc).AddTicks(5063), new DateTime(2026, 9, 13, 2, 48, 28, 559, DateTimeKind.Utc).AddTicks(5062) });

            migrationBuilder.UpdateData(
                table: "TransaccionesLedger",
                keyColumn: "TransaccionId",
                keyValue: 1,
                columns: new[] { "BilleteraFk", "SubastaFK", "fecha" },
                values: new object[] { null, null, new DateTime(2026, 9, 14, 2, 48, 28, 560, DateTimeKind.Utc).AddTicks(2929) });

            migrationBuilder.UpdateData(
                table: "TransaccionesLedger",
                keyColumn: "TransaccionId",
                keyValue: 2,
                columns: new[] { "BilleteraFk", "SubastaFK", "fecha" },
                values: new object[] { null, null, new DateTime(2026, 9, 14, 2, 48, 28, 560, DateTimeKind.Utc).AddTicks(3263) });

            migrationBuilder.UpdateData(
                table: "TransaccionesLedger",
                keyColumn: "TransaccionId",
                keyValue: 3,
                columns: new[] { "BilleteraFk", "SubastaFK", "fecha" },
                values: new object[] { null, null, new DateTime(2026, 9, 14, 2, 48, 28, 560, DateTimeKind.Utc).AddTicks(3272) });

            migrationBuilder.UpdateData(
                table: "TransaccionesLedger",
                keyColumn: "TransaccionId",
                keyValue: 4,
                columns: new[] { "BilleteraFk", "SubastaFK", "SubastaFk", "fecha" },
                values: new object[] { null, 1, null, new DateTime(2026, 9, 15, 2, 43, 28, 560, DateTimeKind.Utc).AddTicks(3274) });

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
                name: "IX_TransaccionesLedger_SubastaFK",
                table: "TransaccionesLedger",
                column: "SubastaFK");

            migrationBuilder.CreateIndex(
                name: "IX_Pujas_CompradorFK",
                table: "Pujas",
                column: "CompradorFK");

            migrationBuilder.CreateIndex(
                name: "IX_Pujas_SubastaFK",
                table: "Pujas",
                column: "SubastaFK");

            migrationBuilder.CreateIndex(
                name: "IX_Billeteras_UsuarioFK",
                table: "Billeteras",
                column: "UsuarioFK",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Auditorias_UsuarioFK",
                table: "Auditorias",
                column: "UsuarioFK");

            migrationBuilder.AddForeignKey(
                name: "FK_Auditorias_Usuarios_UsuarioFK",
                table: "Auditorias",
                column: "UsuarioFK",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Billeteras_Usuarios_UsuarioFK",
                table: "Billeteras",
                column: "UsuarioFK",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pujas_Subastas_SubastaFK",
                table: "Pujas",
                column: "SubastaFK",
                principalTable: "Subastas",
                principalColumn: "SubastaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pujas_Subastas_SubastaId",
                table: "Pujas",
                column: "SubastaId",
                principalTable: "Subastas",
                principalColumn: "SubastaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pujas_Usuarios_CompradorFK",
                table: "Pujas",
                column: "CompradorFK",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransaccionesLedger_Billeteras_BilleteraFK",
                table: "TransaccionesLedger",
                column: "BilleteraFK",
                principalTable: "Billeteras",
                principalColumn: "BilleteraId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransaccionesLedger_Subastas_SubastaFK",
                table: "TransaccionesLedger",
                column: "SubastaFK",
                principalTable: "Subastas",
                principalColumn: "SubastaId",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
