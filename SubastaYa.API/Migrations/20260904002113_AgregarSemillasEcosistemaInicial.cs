using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SubastaYa.API.Migrations
{
    /// <inheritdoc />
    public partial class AgregarSemillasEcosistemaInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Billeteras_Usuarios_UsuarioId",
                table: "Billeteras");

            migrationBuilder.DropForeignKey(
                name: "FK_Pujas_Subastas_SubastaId",
                table: "Pujas");

            migrationBuilder.DropForeignKey(
                name: "FK_Pujas_Usuarios_CompradorId",
                table: "Pujas");

            migrationBuilder.DropForeignKey(
                name: "FK_Subastas_Categorias_ProductoId",
                table: "Subastas");

            migrationBuilder.DropForeignKey(
                name: "FK_Subastas_Usuarios_VendedorId",
                table: "Subastas");

            migrationBuilder.DropForeignKey(
                name: "FK_TransaccionesLedger_Billeteras_BilleteraFk",
                table: "TransaccionesLedger");

            migrationBuilder.DropForeignKey(
                name: "FK_TransaccionesLedger_Subastas_SubastaFk",
                table: "TransaccionesLedger");

            migrationBuilder.DropIndex(
                name: "IX_TransaccionesLedger_BilleteraFk",
                table: "TransaccionesLedger");

            migrationBuilder.DropIndex(
                name: "IX_TransaccionesLedger_SubastaFk",
                table: "TransaccionesLedger");

            migrationBuilder.DropIndex(
                name: "IX_Subastas_ProductoId",
                table: "Subastas");

            migrationBuilder.DropIndex(
                name: "IX_Subastas_VendedorId",
                table: "Subastas");

            migrationBuilder.DropIndex(
                name: "IX_Pujas_CompradorId",
                table: "Pujas");

            migrationBuilder.DropIndex(
                name: "IX_Pujas_SubastaId",
                table: "Pujas");

            migrationBuilder.DropIndex(
                name: "IX_Billeteras_UsuarioId",
                table: "Billeteras");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Categorias",
                newName: "CategoriaId");

            migrationBuilder.AlterColumn<int>(
                name: "BilleteraFk",
                table: "TransaccionesLedger",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "BilleteraFK",
                table: "TransaccionesLedger",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SubastaFK",
                table: "TransaccionesLedger",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VendedorId",
                table: "Subastas",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "ProductoId",
                table: "Subastas",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "ProductoFK",
                table: "Subastas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VendedorFK",
                table: "Subastas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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
                    { 1, "elque vende", "$2a$11$9EfSBCNzg.HtLMolc6UOo.1QUnfaz4Nv2Tt3hCt9RyTD/ADx/hXJW", "vendedor@test.com", new DateTime(2026, 9, 4, 0, 21, 12, 173, DateTimeKind.Utc).AddTicks(6698), "Franco" },
                    { 2, "el que compra1", "$2a$11$TiroUcoTbmQJ9QSdjExPpuVE97IV.yDiAXjw.C01BL8ckSLJzUfVe", "comprador1@test.com", new DateTime(2026, 9, 4, 0, 21, 12, 391, DateTimeKind.Utc).AddTicks(9287), "Juan" },
                    { 3, "el que compra2", "$2a$11$651BTSGIwdCUvaZ2YM1JbOOzVJqsEHsUcKC16W1lKjORmQ817EMAa", "comprador2@test.com", new DateTime(2026, 9, 4, 0, 21, 12, 495, DateTimeKind.Utc).AddTicks(618), "Pedro" },
                    { 4, "la que compra3", "$2a$11$2SbtF03Sw9RdrfX3NRXJvOZ8/nYe0K7IsO6b/SYIfZqgpkfaflk8e", "sinfondos@test.com", new DateTime(2026, 9, 4, 0, 21, 12, 598, DateTimeKind.Utc).AddTicks(2500), "Maria" }
                });

            migrationBuilder.InsertData(
                table: "Auditorias",
                columns: new[] { "AuditoriaId", "Accion", "Detalle_Json", "Entidad", "EntidadId", "Fecha", "UsuarioFK" },
                values: new object[,]
                {
                    { 1, "Creación de subasta", "", "", 1, new DateTime(2026, 9, 4, 0, 21, 12, 599, DateTimeKind.Utc).AddTicks(5129), 1 },
                    { 2, "Creación de subasta", "", "", 2, new DateTime(2026, 9, 4, 0, 21, 12, 599, DateTimeKind.Utc).AddTicks(5470), 1 },
                    { 3, "Creación de subasta", "", "", 3, new DateTime(2026, 9, 4, 0, 21, 12, 599, DateTimeKind.Utc).AddTicks(5472), 1 }
                });

            migrationBuilder.InsertData(
                table: "Billeteras",
                columns: new[] { "BilleteraId", "SaldoRetenido", "SaldoTotal", "UsuarioFK", "UsuarioId", "Version" },
                values: new object[,]
                {
                    { 1, 0m, 0m, 1, null, 1 },
                    { 2, 45000m, 150000m, 2, null, 1 },
                    { 3, 0m, 200000m, 3, null, 1 },
                    { 4, 0m, 500m, 4, null, 1 }
                });

            migrationBuilder.InsertData(
                table: "Subastas",
                columns: new[] { "SubastaId", "Descripcion", "Estado", "FechaFin", "FechaInicio", "PrecioInicial", "ProductoFK", "ProductoId", "PujaMinima", "Titulo", "UrlImagen", "VendedorFK", "VendedorId", "Version" },
                values: new object[,]
                {
                    { 1, "Laptop de gama alta", "ACTIVA", new DateTime(2026, 9, 4, 0, 51, 12, 599, DateTimeKind.Utc).AddTicks(1788), new DateTime(2026, 9, 4, 0, 21, 12, 599, DateTimeKind.Utc).AddTicks(1454), 5000m, 1, null, 1000m, "Laptop", "https://picsum.photos/id/0/600/400", 1, null, 1 },
                    { 2, "Maquina de escribir vintage en buen estado", "ACTIVA", new DateTime(2026, 9, 4, 0, 22, 30, 599, DateTimeKind.Utc).AddTicks(2280), new DateTime(2026, 9, 4, 0, 21, 12, 599, DateTimeKind.Utc).AddTicks(2279), 50m, 2, null, 10m, "Maquina de escribir vintage", "https://picsum.photos/id/111/600/400", 1, null, 1 },
                    { 3, "Campera de invierno en buen estado", "PROGRAMADA", new DateTime(2026, 9, 7, 0, 21, 12, 599, DateTimeKind.Utc).AddTicks(2301), new DateTime(2026, 9, 5, 0, 21, 12, 599, DateTimeKind.Utc).AddTicks(2288), 100m, 3, null, 300m, "Campera de invierno", "https://picsum.photos/id/338/600/400", 1, null, 1 },
                    { 4, "Autos antiguos en buen estado", "ACTIVA", new DateTime(2026, 9, 1, 0, 21, 12, 599, DateTimeKind.Utc).AddTicks(2304), new DateTime(2026, 8, 30, 0, 21, 12, 599, DateTimeKind.Utc).AddTicks(2304), 300m, 4, null, 1000m, "Autos antiguos", "https://picsum.photos/id/133/600/400", 1, null, 1 },
                    { 5, "Smartphone de última generación", "ACTIVA", new DateTime(2026, 9, 3, 0, 21, 12, 599, DateTimeKind.Utc).AddTicks(2307), new DateTime(2026, 9, 2, 0, 21, 12, 599, DateTimeKind.Utc).AddTicks(2307), 800m, 1, null, 200m, "Smartphone", "https://picsum.photos/id/3/600/400", 1, null, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransaccionesLedger_BilleteraFK",
                table: "TransaccionesLedger",
                column: "BilleteraFK");

            migrationBuilder.CreateIndex(
                name: "IX_TransaccionesLedger_SubastaFK",
                table: "TransaccionesLedger",
                column: "SubastaFK");

            migrationBuilder.CreateIndex(
                name: "IX_Subastas_ProductoFK",
                table: "Subastas",
                column: "ProductoFK");

            migrationBuilder.CreateIndex(
                name: "IX_Subastas_VendedorFK",
                table: "Subastas",
                column: "VendedorFK");

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
                name: "FK_Pujas_Usuarios_CompradorFK",
                table: "Pujas",
                column: "CompradorFK",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subastas_Categorias_ProductoFK",
                table: "Subastas",
                column: "ProductoFK",
                principalTable: "Categorias",
                principalColumn: "CategoriaId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subastas_Usuarios_VendedorFK",
                table: "Subastas",
                column: "VendedorFK",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Billeteras_Usuarios_UsuarioFK",
                table: "Billeteras");

            migrationBuilder.DropForeignKey(
                name: "FK_Pujas_Subastas_SubastaFK",
                table: "Pujas");

            migrationBuilder.DropForeignKey(
                name: "FK_Pujas_Usuarios_CompradorFK",
                table: "Pujas");

            migrationBuilder.DropForeignKey(
                name: "FK_Subastas_Categorias_ProductoFK",
                table: "Subastas");

            migrationBuilder.DropForeignKey(
                name: "FK_Subastas_Usuarios_VendedorFK",
                table: "Subastas");

            migrationBuilder.DropForeignKey(
                name: "FK_TransaccionesLedger_Billeteras_BilleteraFK",
                table: "TransaccionesLedger");

            migrationBuilder.DropForeignKey(
                name: "FK_TransaccionesLedger_Subastas_SubastaFK",
                table: "TransaccionesLedger");

            migrationBuilder.DropIndex(
                name: "IX_TransaccionesLedger_BilleteraFK",
                table: "TransaccionesLedger");

            migrationBuilder.DropIndex(
                name: "IX_TransaccionesLedger_SubastaFK",
                table: "TransaccionesLedger");

            migrationBuilder.DropIndex(
                name: "IX_Subastas_ProductoFK",
                table: "Subastas");

            migrationBuilder.DropIndex(
                name: "IX_Subastas_VendedorFK",
                table: "Subastas");

            migrationBuilder.DropIndex(
                name: "IX_Pujas_CompradorFK",
                table: "Pujas");

            migrationBuilder.DropIndex(
                name: "IX_Pujas_SubastaFK",
                table: "Pujas");

            migrationBuilder.DropIndex(
                name: "IX_Billeteras_UsuarioFK",
                table: "Billeteras");

            migrationBuilder.DeleteData(
                table: "Auditorias",
                keyColumn: "AuditoriaId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Auditorias",
                keyColumn: "AuditoriaId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Auditorias",
                keyColumn: "AuditoriaId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Billeteras",
                keyColumn: "BilleteraId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Billeteras",
                keyColumn: "BilleteraId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Billeteras",
                keyColumn: "BilleteraId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Billeteras",
                keyColumn: "BilleteraId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "CategoriaId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "CategoriaId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "CategoriaId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "CategoriaId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "BilleteraFK",
                table: "TransaccionesLedger");

            migrationBuilder.DropColumn(
                name: "SubastaFK",
                table: "TransaccionesLedger");

            migrationBuilder.DropColumn(
                name: "ProductoFK",
                table: "Subastas");

            migrationBuilder.DropColumn(
                name: "VendedorFK",
                table: "Subastas");

            migrationBuilder.DropColumn(
                name: "CompradorFK",
                table: "Pujas");

            migrationBuilder.DropColumn(
                name: "SubastaFK",
                table: "Pujas");

            migrationBuilder.DropColumn(
                name: "UsuarioFK",
                table: "Billeteras");

            migrationBuilder.RenameColumn(
                name: "CategoriaId",
                table: "Categorias",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "BilleteraFk",
                table: "TransaccionesLedger",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VendedorId",
                table: "Subastas",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProductoId",
                table: "Subastas",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

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

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Billeteras",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransaccionesLedger_BilleteraFk",
                table: "TransaccionesLedger",
                column: "BilleteraFk");

            migrationBuilder.CreateIndex(
                name: "IX_TransaccionesLedger_SubastaFk",
                table: "TransaccionesLedger",
                column: "SubastaFk");

            migrationBuilder.CreateIndex(
                name: "IX_Subastas_ProductoId",
                table: "Subastas",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Subastas_VendedorId",
                table: "Subastas",
                column: "VendedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Pujas_CompradorId",
                table: "Pujas",
                column: "CompradorId");

            migrationBuilder.CreateIndex(
                name: "IX_Pujas_SubastaId",
                table: "Pujas",
                column: "SubastaId");

            migrationBuilder.CreateIndex(
                name: "IX_Billeteras_UsuarioId",
                table: "Billeteras",
                column: "UsuarioId",
                unique: true);

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
                name: "FK_Pujas_Usuarios_CompradorId",
                table: "Pujas",
                column: "CompradorId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subastas_Categorias_ProductoId",
                table: "Subastas",
                column: "ProductoId",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subastas_Usuarios_VendedorId",
                table: "Subastas",
                column: "VendedorId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransaccionesLedger_Billeteras_BilleteraFk",
                table: "TransaccionesLedger",
                column: "BilleteraFk",
                principalTable: "Billeteras",
                principalColumn: "BilleteraId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransaccionesLedger_Subastas_SubastaFk",
                table: "TransaccionesLedger",
                column: "SubastaFk",
                principalTable: "Subastas",
                principalColumn: "SubastaId",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
