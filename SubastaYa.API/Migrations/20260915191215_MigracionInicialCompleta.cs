using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SubastaYa.API.Migrations
{
    /// <inheritdoc />
    public partial class MigracionInicialCompleta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    CategoriaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    UrlIcono = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.CategoriaId);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreUsuario = table.Column<string>(type: "text", nullable: false),
                    Alias = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    ContrasenaHash = table.Column<string>(type: "text", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioId);
                });

            migrationBuilder.CreateTable(
                name: "Auditorias",
                columns: table => new
                {
                    AuditoriaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    Entidad = table.Column<string>(type: "text", nullable: false),
                    EntidadId = table.Column<int>(type: "integer", nullable: false),
                    Accion = table.Column<string>(type: "text", nullable: false),
                    Detalle_Json = table.Column<string>(type: "text", nullable: false),
                    Fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auditorias", x => x.AuditoriaId);
                    table.ForeignKey(
                        name: "FK_Auditorias_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Billeteras",
                columns: table => new
                {
                    BilleteraId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    SaldoTotal = table.Column<decimal>(type: "numeric", nullable: false),
                    SaldoRetenido = table.Column<decimal>(type: "numeric", nullable: false),
                    SaldoDisponible = table.Column<decimal>(type: "numeric", nullable: false, computedColumnSql: "\"SaldoTotal\" - \"SaldoRetenido\"", stored: true),
                    Version = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Billeteras", x => x.BilleteraId);
                    table.ForeignKey(
                        name: "FK_Billeteras_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subastas",
                columns: table => new
                {
                    SubastaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VendedorId = table.Column<int>(type: "integer", nullable: false),
                    CategoriaId = table.Column<int>(type: "integer", nullable: false),
                    Titulo = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    UrlImagen = table.Column<string>(type: "text", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    PrecioInicial = table.Column<decimal>(type: "numeric", nullable: false),
                    PujaMinima = table.Column<decimal>(type: "numeric", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subastas", x => x.SubastaId);
                    table.ForeignKey(
                        name: "FK_Subastas_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "CategoriaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Subastas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId");
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
                    BilleteraId = table.Column<int>(type: "integer", nullable: false),
                    SubastaId = table.Column<int>(type: "integer", nullable: true),
                    TipoTransaccion = table.Column<string>(type: "text", nullable: false),
                    Monto = table.Column<decimal>(type: "numeric", nullable: false),
                    Fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransaccionesLedger", x => x.TransaccionId);
                    table.ForeignKey(
                        name: "FK_TransaccionesLedger_Billeteras_BilleteraId",
                        column: x => x.BilleteraId,
                        principalTable: "Billeteras",
                        principalColumn: "BilleteraId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransaccionesLedger_Subastas_SubastaId",
                        column: x => x.SubastaId,
                        principalTable: "Subastas",
                        principalColumn: "SubastaId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "CategoriaId", "Nombre", "UrlIcono" },
                values: new object[,]
                {
                    { 1, "Tecnologia", "bi-cpu" },
                    { 2, "Coleccionables", "bi-star" },
                    { 3, "Indumentaria", "bi-tshirt" },
                    { 4, "Vehiculos", "bi-car" }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "UsuarioId", "Alias", "ContrasenaHash", "Email", "FechaRegistro", "NombreUsuario" },
                values: new object[,]
                {
                    { 1, "elque vende", "$2a$11$kEn8vgCdUc6dqn7rZYcxGO9AG.Ddka6Qb5T7/F1LyS2pxjJTP8GuC", "vendedor@test.com", new DateTime(2026, 9, 15, 19, 12, 14, 199, DateTimeKind.Utc).AddTicks(2624), "Franco" },
                    { 2, "el que compra1", "$2a$11$puTAohSa7.kqt/Z4Mka9EujCnVO1rOzpLaZYxK0fsqrV1LeJwRNDS", "comprador1@test.com", new DateTime(2026, 9, 15, 19, 12, 14, 324, DateTimeKind.Utc).AddTicks(8897), "Juan" },
                    { 3, "el que compra2", "$2a$11$VdNFPjLGKLbFJxULCn9tnuPeR4MyPJgQjolM2LiNDCsjpcI11RzSy", "comprador2@test.com", new DateTime(2026, 9, 15, 19, 12, 14, 453, DateTimeKind.Utc).AddTicks(8064), "Pedro" },
                    { 4, "la que compra3", "$2a$11$pnt5CMJdQneh.H3wjMIcY.LYrzu/moyP8mbZis.ILYNdYoGZxR4mO", "sinfondos@test.com", new DateTime(2026, 9, 15, 19, 12, 14, 579, DateTimeKind.Utc).AddTicks(8860), "Maria" }
                });

            migrationBuilder.InsertData(
                table: "Auditorias",
                columns: new[] { "AuditoriaId", "Accion", "Detalle_Json", "Entidad", "EntidadId", "Fecha", "UsuarioId" },
                values: new object[,]
                {
                    { 1, "Creación de subasta", "", "", 1, new DateTime(2026, 9, 15, 19, 12, 14, 581, DateTimeKind.Utc).AddTicks(5951), 1 },
                    { 2, "Creación de subasta", "", "", 2, new DateTime(2026, 9, 15, 19, 12, 14, 581, DateTimeKind.Utc).AddTicks(6422), 1 },
                    { 3, "Creación de subasta", "", "", 3, new DateTime(2026, 9, 15, 19, 12, 14, 581, DateTimeKind.Utc).AddTicks(6424), 1 }
                });

            migrationBuilder.InsertData(
                table: "Billeteras",
                columns: new[] { "BilleteraId", "SaldoRetenido", "SaldoTotal", "UsuarioId", "Version" },
                values: new object[,]
                {
                    { 1, 0m, 0m, 1, 1 },
                    { 2, 45000m, 150000m, 2, 1 },
                    { 3, 0m, 200000m, 3, 1 },
                    { 4, 0m, 500m, 4, 1 }
                });

            migrationBuilder.InsertData(
                table: "Subastas",
                columns: new[] { "SubastaId", "CategoriaId", "Descripcion", "Estado", "FechaFin", "FechaInicio", "PrecioInicial", "PujaMinima", "Titulo", "UrlImagen", "UsuarioId", "VendedorId", "Version" },
                values: new object[,]
                {
                    { 1, 1, "Laptop de gama alta", "ACTIVA", new DateTime(2026, 9, 15, 19, 42, 14, 581, DateTimeKind.Utc).AddTicks(2385), new DateTime(2026, 9, 15, 19, 12, 14, 581, DateTimeKind.Utc).AddTicks(2038), 5000m, 1000m, "Laptop", "https://picsum.photos/id/0/600/400", null, 1, 1 },
                    { 2, 2, "Maquina de escribir vintage en buen estado", "ACTIVA", new DateTime(2026, 9, 15, 19, 13, 32, 581, DateTimeKind.Utc).AddTicks(2933), new DateTime(2026, 9, 15, 19, 12, 14, 581, DateTimeKind.Utc).AddTicks(2933), 50m, 10m, "Maquina de escribir vintage", "https://picsum.photos/id/111/600/400", null, 1, 1 },
                    { 3, 3, "Campera de invierno en buen estado", "PROGRAMADA", new DateTime(2026, 9, 18, 19, 12, 14, 581, DateTimeKind.Utc).AddTicks(3019), new DateTime(2026, 9, 16, 19, 12, 14, 581, DateTimeKind.Utc).AddTicks(3001), 100m, 300m, "Campera de invierno", "https://picsum.photos/id/338/600/400", null, 1, 1 },
                    { 4, 4, "Autos antiguos en buen estado", "ACTIVA", new DateTime(2026, 9, 12, 19, 12, 14, 581, DateTimeKind.Utc).AddTicks(3023), new DateTime(2026, 9, 10, 19, 12, 14, 581, DateTimeKind.Utc).AddTicks(3023), 300m, 1000m, "Autos antiguos", "https://picsum.photos/id/133/600/400", null, 1, 1 },
                    { 5, 1, "Smartphone de última generación", "ACTIVA", new DateTime(2026, 9, 14, 19, 12, 14, 581, DateTimeKind.Utc).AddTicks(3026), new DateTime(2026, 9, 13, 19, 12, 14, 581, DateTimeKind.Utc).AddTicks(3025), 800m, 200m, "Smartphone", "https://picsum.photos/id/3/600/400", null, 1, 1 }
                });

            migrationBuilder.InsertData(
                table: "Pujas",
                columns: new[] { "PujaId", "CompradorId", "FechaPuja", "MontoPuja", "SubastaId" },
                values: new object[,]
                {
                    { 1, 3, new DateTime(2026, 9, 15, 19, 2, 14, 581, DateTimeKind.Utc).AddTicks(8385), 30000m, 1 },
                    { 2, 2, new DateTime(2026, 9, 15, 19, 7, 14, 581, DateTimeKind.Utc).AddTicks(9083), 45000m, 1 }
                });

            migrationBuilder.InsertData(
                table: "TransaccionesLedger",
                columns: new[] { "TransaccionId", "BilleteraId", "Fecha", "Monto", "SubastaId", "TipoTransaccion" },
                values: new object[,]
                {
                    { 1, 2, new DateTime(2026, 9, 14, 19, 12, 14, 582, DateTimeKind.Utc).AddTicks(2013), 150000m, null, "DEPOSITO" },
                    { 2, 3, new DateTime(2026, 9, 14, 19, 12, 14, 582, DateTimeKind.Utc).AddTicks(2354), 200000m, null, "DEPOSITO" },
                    { 3, 4, new DateTime(2026, 9, 14, 19, 12, 14, 582, DateTimeKind.Utc).AddTicks(2356), 500m, null, "DEPOSITO" },
                    { 4, 2, new DateTime(2026, 9, 15, 19, 7, 14, 582, DateTimeKind.Utc).AddTicks(2358), -45000m, 1, "RETENCION" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Auditorias_UsuarioId",
                table: "Auditorias",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Billeteras_UsuarioId",
                table: "Billeteras",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pujas_CompradorId",
                table: "Pujas",
                column: "CompradorId");

            migrationBuilder.CreateIndex(
                name: "IX_Pujas_SubastaId",
                table: "Pujas",
                column: "SubastaId");

            migrationBuilder.CreateIndex(
                name: "IX_Subastas_CategoriaId",
                table: "Subastas",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Subastas_UsuarioId",
                table: "Subastas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Subastas_VendedorId",
                table: "Subastas",
                column: "VendedorId");

            migrationBuilder.CreateIndex(
                name: "IX_TransaccionesLedger_BilleteraId",
                table: "TransaccionesLedger",
                column: "BilleteraId");

            migrationBuilder.CreateIndex(
                name: "IX_TransaccionesLedger_SubastaId",
                table: "TransaccionesLedger",
                column: "SubastaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Auditorias");

            migrationBuilder.DropTable(
                name: "Pujas");

            migrationBuilder.DropTable(
                name: "TransaccionesLedger");

            migrationBuilder.DropTable(
                name: "Billeteras");

            migrationBuilder.DropTable(
                name: "Subastas");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
