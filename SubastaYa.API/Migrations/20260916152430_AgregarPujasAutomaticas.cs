using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SubastaYa.API.Migrations
{
    /// <inheritdoc />
    public partial class AgregarPujasAutomaticas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auditorias_Usuarios_UsuarioId",
                table: "Auditorias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Auditorias",
                table: "Auditorias");

            migrationBuilder.RenameTable(
                name: "Auditorias",
                newName: "Auditoria_Log");

            migrationBuilder.RenameIndex(
                name: "IX_Auditorias_UsuarioId",
                table: "Auditoria_Log",
                newName: "IX_Auditoria_Log_UsuarioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Auditoria_Log",
                table: "Auditoria_Log",
                column: "AuditoriaId");

            migrationBuilder.CreateTable(
                name: "PujasAutomaticas",
                columns: table => new
                {
                    PujaAutomaticaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SubastaId = table.Column<int>(type: "integer", nullable: false),
                    CompradorId = table.Column<int>(type: "integer", nullable: false),
                    MontoMaximo = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Activa = table.Column<bool>(type: "boolean", nullable: false),
                    FechaConfiguracion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PujasAutomaticas", x => x.PujaAutomaticaId);
                    table.ForeignKey(
                        name: "FK_PujasAutomaticas_Subastas_SubastaId",
                        column: x => x.SubastaId,
                        principalTable: "Subastas",
                        principalColumn: "SubastaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PujasAutomaticas_Usuarios_CompradorId",
                        column: x => x.CompradorId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Auditoria_Log",
                keyColumn: "AuditoriaId",
                keyValue: 1,
                columns: new[] { "Detalle_Json", "Entidad", "Fecha" },
                values: new object[] { "{}", "SUBASTA", new DateTime(2026, 9, 16, 15, 24, 28, 921, DateTimeKind.Utc).AddTicks(9997) });

            migrationBuilder.UpdateData(
                table: "Auditoria_Log",
                keyColumn: "AuditoriaId",
                keyValue: 2,
                columns: new[] { "Detalle_Json", "Entidad", "Fecha" },
                values: new object[] { "{}", "SUBASTA", new DateTime(2026, 9, 16, 15, 24, 28, 922, DateTimeKind.Utc).AddTicks(364) });

            migrationBuilder.UpdateData(
                table: "Auditoria_Log",
                keyColumn: "AuditoriaId",
                keyValue: 3,
                columns: new[] { "Detalle_Json", "Entidad", "Fecha" },
                values: new object[] { "{}", "SUBASTA", new DateTime(2026, 9, 16, 15, 24, 28, 922, DateTimeKind.Utc).AddTicks(365) });

            migrationBuilder.UpdateData(
                table: "Pujas",
                keyColumn: "PujaId",
                keyValue: 1,
                column: "FechaPuja",
                value: new DateTime(2026, 9, 16, 15, 14, 28, 922, DateTimeKind.Utc).AddTicks(2401));

            migrationBuilder.UpdateData(
                table: "Pujas",
                keyColumn: "PujaId",
                keyValue: 2,
                column: "FechaPuja",
                value: new DateTime(2026, 9, 16, 15, 19, 28, 922, DateTimeKind.Utc).AddTicks(3108));

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 1,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2026, 9, 16, 15, 54, 28, 921, DateTimeKind.Utc).AddTicks(6551), new DateTime(2026, 9, 16, 15, 24, 28, 921, DateTimeKind.Utc).AddTicks(6111) });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 2,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2026, 9, 16, 15, 25, 46, 921, DateTimeKind.Utc).AddTicks(7121), new DateTime(2026, 9, 16, 15, 24, 28, 921, DateTimeKind.Utc).AddTicks(7120) });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 3,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2026, 9, 19, 15, 24, 28, 921, DateTimeKind.Utc).AddTicks(7148), new DateTime(2026, 9, 17, 15, 24, 28, 921, DateTimeKind.Utc).AddTicks(7130) });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 4,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2026, 9, 13, 15, 24, 28, 921, DateTimeKind.Utc).AddTicks(7152), new DateTime(2026, 9, 11, 15, 24, 28, 921, DateTimeKind.Utc).AddTicks(7150) });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 5,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2026, 9, 15, 15, 24, 28, 921, DateTimeKind.Utc).AddTicks(7157), new DateTime(2026, 9, 14, 15, 24, 28, 921, DateTimeKind.Utc).AddTicks(7156) });

            migrationBuilder.UpdateData(
                table: "TransaccionesLedger",
                keyColumn: "TransaccionId",
                keyValue: 1,
                column: "Fecha",
                value: new DateTime(2026, 9, 15, 15, 24, 28, 922, DateTimeKind.Utc).AddTicks(5723));

            migrationBuilder.UpdateData(
                table: "TransaccionesLedger",
                keyColumn: "TransaccionId",
                keyValue: 2,
                column: "Fecha",
                value: new DateTime(2026, 9, 15, 15, 24, 28, 922, DateTimeKind.Utc).AddTicks(6054));

            migrationBuilder.UpdateData(
                table: "TransaccionesLedger",
                keyColumn: "TransaccionId",
                keyValue: 3,
                column: "Fecha",
                value: new DateTime(2026, 9, 15, 15, 24, 28, 922, DateTimeKind.Utc).AddTicks(6100));

            migrationBuilder.UpdateData(
                table: "TransaccionesLedger",
                keyColumn: "TransaccionId",
                keyValue: 4,
                column: "Fecha",
                value: new DateTime(2026, 9, 16, 15, 19, 28, 922, DateTimeKind.Utc).AddTicks(6102));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 1,
                columns: new[] { "ContrasenaHash", "FechaRegistro" },
                values: new object[] { "$2a$11$Y5/gWXJV140AgVZpmWqt7ul42uBYwfZI2RRgpgbOlORtzoin559/K", new DateTime(2026, 9, 16, 15, 24, 28, 524, DateTimeKind.Utc).AddTicks(2154) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 2,
                columns: new[] { "ContrasenaHash", "FechaRegistro" },
                values: new object[] { "$2a$11$I3LjK4DmAXJlrj7kv8JoZuMRQQdAp3AZHAvhtJERqf5suWywRZxom", new DateTime(2026, 9, 16, 15, 24, 28, 670, DateTimeKind.Utc).AddTicks(773) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 3,
                columns: new[] { "ContrasenaHash", "FechaRegistro" },
                values: new object[] { "$2a$11$otBHdEv5NKfSPCpBkhfmVu3sSQ9MeGSdPHMquOO3dSEe6z88gQn3q", new DateTime(2026, 9, 16, 15, 24, 28, 794, DateTimeKind.Utc).AddTicks(2290) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 4,
                columns: new[] { "ContrasenaHash", "FechaRegistro" },
                values: new object[] { "$2a$11$gPjSHStJez58s681PA1B0.31rQ/YRYBcfertkFzhh/8B9oYGRXhZ.", new DateTime(2026, 9, 16, 15, 24, 28, 920, DateTimeKind.Utc).AddTicks(2598) });

            migrationBuilder.CreateIndex(
                name: "IX_PujasAutomaticas_CompradorId",
                table: "PujasAutomaticas",
                column: "CompradorId");

            migrationBuilder.CreateIndex(
                name: "IX_PujasAutomaticas_SubastaId",
                table: "PujasAutomaticas",
                column: "SubastaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Auditoria_Log_Usuarios_UsuarioId",
                table: "Auditoria_Log",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auditoria_Log_Usuarios_UsuarioId",
                table: "Auditoria_Log");

            migrationBuilder.DropTable(
                name: "PujasAutomaticas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Auditoria_Log",
                table: "Auditoria_Log");

            migrationBuilder.RenameTable(
                name: "Auditoria_Log",
                newName: "Auditorias");

            migrationBuilder.RenameIndex(
                name: "IX_Auditoria_Log_UsuarioId",
                table: "Auditorias",
                newName: "IX_Auditorias_UsuarioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Auditorias",
                table: "Auditorias",
                column: "AuditoriaId");

            migrationBuilder.UpdateData(
                table: "Auditorias",
                keyColumn: "AuditoriaId",
                keyValue: 1,
                columns: new[] { "Detalle_Json", "Entidad", "Fecha" },
                values: new object[] { "", "", new DateTime(2026, 9, 15, 19, 12, 14, 581, DateTimeKind.Utc).AddTicks(5951) });

            migrationBuilder.UpdateData(
                table: "Auditorias",
                keyColumn: "AuditoriaId",
                keyValue: 2,
                columns: new[] { "Detalle_Json", "Entidad", "Fecha" },
                values: new object[] { "", "", new DateTime(2026, 9, 15, 19, 12, 14, 581, DateTimeKind.Utc).AddTicks(6422) });

            migrationBuilder.UpdateData(
                table: "Auditorias",
                keyColumn: "AuditoriaId",
                keyValue: 3,
                columns: new[] { "Detalle_Json", "Entidad", "Fecha" },
                values: new object[] { "", "", new DateTime(2026, 9, 15, 19, 12, 14, 581, DateTimeKind.Utc).AddTicks(6424) });

            migrationBuilder.UpdateData(
                table: "Pujas",
                keyColumn: "PujaId",
                keyValue: 1,
                column: "FechaPuja",
                value: new DateTime(2026, 9, 15, 19, 2, 14, 581, DateTimeKind.Utc).AddTicks(8385));

            migrationBuilder.UpdateData(
                table: "Pujas",
                keyColumn: "PujaId",
                keyValue: 2,
                column: "FechaPuja",
                value: new DateTime(2026, 9, 15, 19, 7, 14, 581, DateTimeKind.Utc).AddTicks(9083));

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 1,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2026, 9, 15, 19, 42, 14, 581, DateTimeKind.Utc).AddTicks(2385), new DateTime(2026, 9, 15, 19, 12, 14, 581, DateTimeKind.Utc).AddTicks(2038) });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 2,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2026, 9, 15, 19, 13, 32, 581, DateTimeKind.Utc).AddTicks(2933), new DateTime(2026, 9, 15, 19, 12, 14, 581, DateTimeKind.Utc).AddTicks(2933) });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 3,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2026, 9, 18, 19, 12, 14, 581, DateTimeKind.Utc).AddTicks(3019), new DateTime(2026, 9, 16, 19, 12, 14, 581, DateTimeKind.Utc).AddTicks(3001) });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 4,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2026, 9, 12, 19, 12, 14, 581, DateTimeKind.Utc).AddTicks(3023), new DateTime(2026, 9, 10, 19, 12, 14, 581, DateTimeKind.Utc).AddTicks(3023) });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 5,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2026, 9, 14, 19, 12, 14, 581, DateTimeKind.Utc).AddTicks(3026), new DateTime(2026, 9, 13, 19, 12, 14, 581, DateTimeKind.Utc).AddTicks(3025) });

            migrationBuilder.UpdateData(
                table: "TransaccionesLedger",
                keyColumn: "TransaccionId",
                keyValue: 1,
                column: "Fecha",
                value: new DateTime(2026, 9, 14, 19, 12, 14, 582, DateTimeKind.Utc).AddTicks(2013));

            migrationBuilder.UpdateData(
                table: "TransaccionesLedger",
                keyColumn: "TransaccionId",
                keyValue: 2,
                column: "Fecha",
                value: new DateTime(2026, 9, 14, 19, 12, 14, 582, DateTimeKind.Utc).AddTicks(2354));

            migrationBuilder.UpdateData(
                table: "TransaccionesLedger",
                keyColumn: "TransaccionId",
                keyValue: 3,
                column: "Fecha",
                value: new DateTime(2026, 9, 14, 19, 12, 14, 582, DateTimeKind.Utc).AddTicks(2356));

            migrationBuilder.UpdateData(
                table: "TransaccionesLedger",
                keyColumn: "TransaccionId",
                keyValue: 4,
                column: "Fecha",
                value: new DateTime(2026, 9, 15, 19, 7, 14, 582, DateTimeKind.Utc).AddTicks(2358));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 1,
                columns: new[] { "ContrasenaHash", "FechaRegistro" },
                values: new object[] { "$2a$11$kEn8vgCdUc6dqn7rZYcxGO9AG.Ddka6Qb5T7/F1LyS2pxjJTP8GuC", new DateTime(2026, 9, 15, 19, 12, 14, 199, DateTimeKind.Utc).AddTicks(2624) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 2,
                columns: new[] { "ContrasenaHash", "FechaRegistro" },
                values: new object[] { "$2a$11$puTAohSa7.kqt/Z4Mka9EujCnVO1rOzpLaZYxK0fsqrV1LeJwRNDS", new DateTime(2026, 9, 15, 19, 12, 14, 324, DateTimeKind.Utc).AddTicks(8897) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 3,
                columns: new[] { "ContrasenaHash", "FechaRegistro" },
                values: new object[] { "$2a$11$VdNFPjLGKLbFJxULCn9tnuPeR4MyPJgQjolM2LiNDCsjpcI11RzSy", new DateTime(2026, 9, 15, 19, 12, 14, 453, DateTimeKind.Utc).AddTicks(8064) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 4,
                columns: new[] { "ContrasenaHash", "FechaRegistro" },
                values: new object[] { "$2a$11$pnt5CMJdQneh.H3wjMIcY.LYrzu/moyP8mbZis.ILYNdYoGZxR4mO", new DateTime(2026, 9, 15, 19, 12, 14, 579, DateTimeKind.Utc).AddTicks(8860) });

            migrationBuilder.AddForeignKey(
                name: "FK_Auditorias_Usuarios_UsuarioId",
                table: "Auditorias",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
