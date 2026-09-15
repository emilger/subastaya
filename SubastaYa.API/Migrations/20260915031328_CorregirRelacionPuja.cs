using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SubastaYa.API.Migrations
{
    /// <inheritdoc />
    public partial class CorregirRelacionPuja : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Auditorias",
                keyColumn: "AuditoriaId",
                keyValue: 1,
                column: "Fecha",
                value: new DateTime(2026, 9, 15, 3, 4, 24, 668, DateTimeKind.Utc).AddTicks(5868));

            migrationBuilder.UpdateData(
                table: "Auditorias",
                keyColumn: "AuditoriaId",
                keyValue: 2,
                column: "Fecha",
                value: new DateTime(2026, 9, 15, 3, 4, 24, 668, DateTimeKind.Utc).AddTicks(6199));

            migrationBuilder.UpdateData(
                table: "Auditorias",
                keyColumn: "AuditoriaId",
                keyValue: 3,
                column: "Fecha",
                value: new DateTime(2026, 9, 15, 3, 4, 24, 668, DateTimeKind.Utc).AddTicks(6200));

            migrationBuilder.UpdateData(
                table: "Pujas",
                keyColumn: "PujaId",
                keyValue: 1,
                column: "FechaPuja",
                value: new DateTime(2026, 9, 15, 2, 54, 24, 668, DateTimeKind.Utc).AddTicks(7932));

            migrationBuilder.UpdateData(
                table: "Pujas",
                keyColumn: "PujaId",
                keyValue: 2,
                column: "FechaPuja",
                value: new DateTime(2026, 9, 15, 2, 59, 24, 668, DateTimeKind.Utc).AddTicks(8660));

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
                column: "fecha",
                value: new DateTime(2026, 9, 15, 2, 59, 24, 669, DateTimeKind.Utc).AddTicks(1528));

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
        }
    }
}
