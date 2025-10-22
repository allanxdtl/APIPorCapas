using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WEBAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddVentasProductosClientes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Usuarios__idRol__49C3F6B7",
                table: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Usuarios__3213E83F05CD331F",
                table: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Roles__3214EC0743C1B571",
                table: "Roles");

            migrationBuilder.RenameColumn(
                name: "username",
                table: "Usuarios",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "Usuarios",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "nombre",
                table: "Usuarios",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "idRol",
                table: "Usuarios",
                newName: "IdRol");

            migrationBuilder.RenameColumn(
                name: "apellido",
                table: "Usuarios",
                newName: "Apellido");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Usuarios",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Usuarios_idRol",
                table: "Usuarios",
                newName: "IX_Usuarios_IdRol");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Apellido = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Telefono = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "varchar(13)", unicode: false, maxLength: 13, nullable: false),
                    Descripcion = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ventas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Folio = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdCliente = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ventas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ventas_Clientes_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ventas_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VentaDetalles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdVentaHeader = table.Column<int>(type: "int", nullable: false),
                    IdProducto = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VentaDetalles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VentaDetalles_Productos_IdProducto",
                        column: x => x.IdProducto,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VentaDetalles_Ventas_IdVentaHeader",
                        column: x => x.IdVentaHeader,
                        principalTable: "Ventas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VentaDetalles_IdProducto",
                table: "VentaDetalles",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_VentaDetalles_IdVentaHeader",
                table: "VentaDetalles",
                column: "IdVentaHeader");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_IdCliente",
                table: "Ventas",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_IdUsuario",
                table: "Ventas",
                column: "IdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Roles_IdRol",
                table: "Usuarios",
                column: "IdRol",
                principalTable: "Roles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Roles_IdRol",
                table: "Usuarios");

            migrationBuilder.DropTable(
                name: "VentaDetalles");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Ventas");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Usuarios",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Usuarios",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Usuarios",
                newName: "nombre");

            migrationBuilder.RenameColumn(
                name: "IdRol",
                table: "Usuarios",
                newName: "idRol");

            migrationBuilder.RenameColumn(
                name: "Apellido",
                table: "Usuarios",
                newName: "apellido");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Usuarios",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_Usuarios_IdRol",
                table: "Usuarios",
                newName: "IX_Usuarios_idRol");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Usuarios__3213E83F05CD331F",
                table: "Usuarios",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Roles__3214EC0743C1B571",
                table: "Roles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK__Usuarios__idRol__49C3F6B7",
                table: "Usuarios",
                column: "idRol",
                principalTable: "Roles",
                principalColumn: "Id");
        }
    }
}
