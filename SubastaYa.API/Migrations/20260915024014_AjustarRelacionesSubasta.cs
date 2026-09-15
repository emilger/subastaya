using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SubastaYa.API.Migrations
{
    /// <inheritdoc />
    public partial class AjustarRelacionesSubasta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subastas_Categorias_ProductoFK",
                table: "Subastas");

            migrationBuilder.DropForeignKey(
                name: "FK_Subastas_Usuarios_VendedorFK",
                table: "Subastas");

            migrationBuilder.DropIndex(
                name: "IX_Subastas_ProductoFK",
                table: "Subastas");

            migrationBuilder.DropColumn(
                name: "ProductoFK",
                table: "Subastas");

            migrationBuilder.DropColumn(
                name: "ProductoId",
                table: "Subastas");

            migrationBuilder.RenameColumn(
                name: "VendedorFK",
                table: "Subastas",
                newName: "CategoriaId");

            migrationBuilder.RenameIndex(
                name: "IX_Subastas_VendedorFK",
                table: "Subastas",
                newName: "IX_Subastas_CategoriaId");

            migrationBuilder.AlterColumn<int>(
                name: "VendedorId",
                table: "Subastas",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

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
                columns: new[] { "FechaFin", "FechaInicio", "VendedorId" },
                values: new object[] { new DateTime(2026, 9, 15, 3, 10, 14, 215, DateTimeKind.Utc).AddTicks(4598), new DateTime(2026, 9, 15, 2, 40, 14, 215, DateTimeKind.Utc).AddTicks(4258), 1 });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 2,
                columns: new[] { "CategoriaId", "FechaFin", "FechaInicio", "VendedorId" },
                values: new object[] { 2, new DateTime(2026, 9, 15, 2, 41, 32, 215, DateTimeKind.Utc).AddTicks(5039), new DateTime(2026, 9, 15, 2, 40, 14, 215, DateTimeKind.Utc).AddTicks(5038), 1 });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 3,
                columns: new[] { "CategoriaId", "FechaFin", "FechaInicio", "VendedorId" },
                values: new object[] { 3, new DateTime(2026, 9, 18, 2, 40, 14, 215, DateTimeKind.Utc).AddTicks(5059), new DateTime(2026, 9, 16, 2, 40, 14, 215, DateTimeKind.Utc).AddTicks(5047), 1 });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 4,
                columns: new[] { "CategoriaId", "FechaFin", "FechaInicio", "VendedorId" },
                values: new object[] { 4, new DateTime(2026, 9, 12, 2, 40, 14, 215, DateTimeKind.Utc).AddTicks(5062), new DateTime(2026, 9, 10, 2, 40, 14, 215, DateTimeKind.Utc).AddTicks(5062), 1 });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 5,
                columns: new[] { "FechaFin", "FechaInicio", "VendedorId" },
                values: new object[] { new DateTime(2026, 9, 14, 2, 40, 14, 215, DateTimeKind.Utc).AddTicks(5065), new DateTime(2026, 9, 13, 2, 40, 14, 215, DateTimeKind.Utc).AddTicks(5064), 1 });

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

            migrationBuilder.CreateIndex(
                name: "IX_Subastas_VendedorId",
                table: "Subastas",
                column: "VendedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Pujas_SubastaId",
                table: "Pujas",
                column: "SubastaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pujas_Subastas_SubastaId",
                table: "Pujas",
                column: "SubastaId",
                principalTable: "Subastas",
                principalColumn: "SubastaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subastas_Categorias_CategoriaId",
                table: "Subastas",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "CategoriaId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subastas_Usuarios_VendedorId",
                table: "Subastas",
                column: "VendedorId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pujas_Subastas_SubastaId",
                table: "Pujas");

            migrationBuilder.DropForeignKey(
                name: "FK_Subastas_Categorias_CategoriaId",
                table: "Subastas");

            migrationBuilder.DropForeignKey(
                name: "FK_Subastas_Usuarios_VendedorId",
                table: "Subastas");

            migrationBuilder.DropIndex(
                name: "IX_Subastas_VendedorId",
                table: "Subastas");

            migrationBuilder.DropIndex(
                name: "IX_Pujas_SubastaId",
                table: "Pujas");

            migrationBuilder.RenameColumn(
                name: "CategoriaId",
                table: "Subastas",
                newName: "VendedorFK");

            migrationBuilder.RenameIndex(
                name: "IX_Subastas_CategoriaId",
                table: "Subastas",
                newName: "IX_Subastas_VendedorFK");

            migrationBuilder.AlterColumn<int>(
                name: "VendedorId",
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
                name: "ProductoId",
                table: "Subastas",
                type: "integer",
                nullable: true);

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
                columns: new[] { "FechaFin", "FechaInicio", "ProductoFK", "ProductoId", "VendedorId" },
                values: new object[] { new DateTime(2026, 9, 4, 1, 15, 9, 235, DateTimeKind.Utc).AddTicks(5119), new DateTime(2026, 9, 4, 0, 45, 9, 235, DateTimeKind.Utc).AddTicks(4792), 1, null, null });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 2,
                columns: new[] { "FechaFin", "FechaInicio", "ProductoFK", "ProductoId", "VendedorFK", "VendedorId" },
                values: new object[] { new DateTime(2026, 9, 4, 0, 46, 27, 235, DateTimeKind.Utc).AddTicks(5614), new DateTime(2026, 9, 4, 0, 45, 9, 235, DateTimeKind.Utc).AddTicks(5613), 2, null, 1, null });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 3,
                columns: new[] { "FechaFin", "FechaInicio", "ProductoFK", "ProductoId", "VendedorFK", "VendedorId" },
                values: new object[] { new DateTime(2026, 9, 7, 0, 45, 9, 235, DateTimeKind.Utc).AddTicks(5634), new DateTime(2026, 9, 5, 0, 45, 9, 235, DateTimeKind.Utc).AddTicks(5621), 3, null, 1, null });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 4,
                columns: new[] { "FechaFin", "FechaInicio", "ProductoFK", "ProductoId", "VendedorFK", "VendedorId" },
                values: new object[] { new DateTime(2026, 9, 1, 0, 45, 9, 235, DateTimeKind.Utc).AddTicks(5637), new DateTime(2026, 8, 30, 0, 45, 9, 235, DateTimeKind.Utc).AddTicks(5636), 4, null, 1, null });

            migrationBuilder.UpdateData(
                table: "Subastas",
                keyColumn: "SubastaId",
                keyValue: 5,
                columns: new[] { "FechaFin", "FechaInicio", "ProductoFK", "ProductoId", "VendedorId" },
                values: new object[] { new DateTime(2026, 9, 3, 0, 45, 9, 235, DateTimeKind.Utc).AddTicks(5639), new DateTime(2026, 9, 2, 0, 45, 9, 235, DateTimeKind.Utc).AddTicks(5639), 1, null, null });

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

            migrationBuilder.CreateIndex(
                name: "IX_Subastas_ProductoFK",
                table: "Subastas",
                column: "ProductoFK");

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
        }
    }
}
