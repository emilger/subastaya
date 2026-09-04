using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SubastaYa.API.Migrations
{
    /// <inheritdoc />
    public partial class MigracionEsquemaCompleto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auditoria_Log_Usuarios_UsuarioId",
                table: "Auditoria_Log");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Auditoria_Log",
                table: "Auditoria_Log");

            migrationBuilder.DropIndex(
                name: "IX_Auditoria_Log_UsuarioId",
                table: "Auditoria_Log");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Auditoria_Log");

            migrationBuilder.RenameTable(
                name: "Auditoria_Log",
                newName: "Auditorias");

            migrationBuilder.AddColumn<decimal>(
                name: "SaldoDisponible",
                table: "Billeteras",
                type: "numeric",
                nullable: false,
                computedColumnSql: "\"SaldoTotal\" - \"SaldoRetenido\"",
                stored: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Auditorias",
                table: "Auditorias",
                column: "AuditoriaId");

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    UrlIcono = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subastas",
                columns: table => new
                {
                    SubastaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VendedorId = table.Column<int>(type: "integer", nullable: false),
                    ProductoId = table.Column<int>(type: "integer", nullable: false),
                    Titulo = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    UrlImagen = table.Column<string>(type: "text", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    PrecioInicial = table.Column<decimal>(type: "numeric", nullable: false),
                    PujaMinima = table.Column<decimal>(type: "numeric", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subastas", x => x.SubastaId);
                    table.ForeignKey(
                        name: "FK_Subastas_Categorias_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Subastas_Usuarios_VendedorId",
                        column: x => x.VendedorId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pujas",
                columns: table => new
                {
                    PujaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SubastaId = table.Column<int>(type: "integer", nullable: false),
                    CompradorId = table.Column<int>(type: "integer", nullable: false),
                    FechaPuja = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MontoPuja = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pujas", x => x.PujaId);
                    table.ForeignKey(
                        name: "FK_Pujas_Subastas_SubastaId",
                        column: x => x.SubastaId,
                        principalTable: "Subastas",
                        principalColumn: "SubastaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pujas_Usuarios_CompradorId",
                        column: x => x.CompradorId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransaccionesLedger",
                columns: table => new
                {
                    TransaccionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BilleteraFk = table.Column<int>(type: "integer", nullable: false),
                    SubastaFk = table.Column<int>(type: "integer", nullable: true),
                    TipoTransaccion = table.Column<string>(type: "text", nullable: false),
                    Monto = table.Column<decimal>(type: "numeric", nullable: false),
                    fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransaccionesLedger", x => x.TransaccionId);
                    table.ForeignKey(
                        name: "FK_TransaccionesLedger_Billeteras_BilleteraFk",
                        column: x => x.BilleteraFk,
                        principalTable: "Billeteras",
                        principalColumn: "BilleteraId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransaccionesLedger_Subastas_SubastaFk",
                        column: x => x.SubastaFk,
                        principalTable: "Subastas",
                        principalColumn: "SubastaId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Auditorias_UsuarioFK",
                table: "Auditorias",
                column: "UsuarioFK");

            migrationBuilder.CreateIndex(
                name: "IX_Pujas_CompradorId",
                table: "Pujas",
                column: "CompradorId");

            migrationBuilder.CreateIndex(
                name: "IX_Pujas_SubastaId",
                table: "Pujas",
                column: "SubastaId");

            migrationBuilder.CreateIndex(
                name: "IX_Subastas_ProductoId",
                table: "Subastas",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Subastas_VendedorId",
                table: "Subastas",
                column: "VendedorId");

            migrationBuilder.CreateIndex(
                name: "IX_TransaccionesLedger_BilleteraFk",
                table: "TransaccionesLedger",
                column: "BilleteraFk");

            migrationBuilder.CreateIndex(
                name: "IX_TransaccionesLedger_SubastaFk",
                table: "TransaccionesLedger",
                column: "SubastaFk");

            migrationBuilder.AddForeignKey(
                name: "FK_Auditorias_Usuarios_UsuarioFK",
                table: "Auditorias",
                column: "UsuarioFK",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auditorias_Usuarios_UsuarioFK",
                table: "Auditorias");

            migrationBuilder.DropTable(
                name: "Pujas");

            migrationBuilder.DropTable(
                name: "TransaccionesLedger");

            migrationBuilder.DropTable(
                name: "Subastas");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Auditorias",
                table: "Auditorias");

            migrationBuilder.DropIndex(
                name: "IX_Auditorias_UsuarioFK",
                table: "Auditorias");

            migrationBuilder.DropColumn(
                name: "SaldoDisponible",
                table: "Billeteras");

            migrationBuilder.RenameTable(
                name: "Auditorias",
                newName: "Auditoria_Log");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Auditoria_Log",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Auditoria_Log",
                table: "Auditoria_Log",
                column: "AuditoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Auditoria_Log_UsuarioId",
                table: "Auditoria_Log",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Auditoria_Log_Usuarios_UsuarioId",
                table: "Auditoria_Log",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId");
        }
    }
}
