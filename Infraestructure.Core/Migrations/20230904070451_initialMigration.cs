using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Core.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Logistic");

            migrationBuilder.EnsureSchema(
                name: "General");

            migrationBuilder.EnsureSchema(
                name: "Security");

            migrationBuilder.CreateTable(
                name: "Country",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                schema: "Security",
                columns: table => new
                {
                    IdPermission = table.Column<int>(type: "int", nullable: false),
                    Permission = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Ambit = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.IdPermission);
                });

            migrationBuilder.CreateTable(
                name: "Rol",
                schema: "Security",
                columns: table => new
                {
                    IdRol = table.Column<int>(type: "int", nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol", x => x.IdRol);
                });

            migrationBuilder.CreateTable(
                name: "TypeProduct",
                schema: "Logistic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeProduct = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeProduct", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                schema: "Logistic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Direction = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    IdCountry = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Client_Country_IdCountry",
                        column: x => x.IdCountry,
                        principalSchema: "General",
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Seaports",
                schema: "Logistic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IdCountry = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seaports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seaports_Country_IdCountry",
                        column: x => x.IdCountry,
                        principalSchema: "General",
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                schema: "Logistic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Direction = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IdCountry = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Warehouses_Country_IdCountry",
                        column: x => x.IdCountry,
                        principalSchema: "General",
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolesPermissions",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdRol = table.Column<int>(type: "int", nullable: false),
                    IdPermission = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolesPermissions_Permission_IdPermission",
                        column: x => x.IdPermission,
                        principalSchema: "Security",
                        principalTable: "Permission",
                        principalColumn: "IdPermission",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolesPermissions_Rol_IdRol",
                        column: x => x.IdRol,
                        principalSchema: "Security",
                        principalTable: "Rol",
                        principalColumn: "IdRol",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdRol = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Rol_IdRol",
                        column: x => x.IdRol,
                        principalSchema: "Security",
                        principalTable: "Rol",
                        principalColumn: "IdRol",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaritimeLot",
                schema: "Logistic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTypeProduct = table.Column<int>(type: "int", nullable: false),
                    IdClient = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdSeaport = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FleetNumber = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    GuideNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaritimeLot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaritimeLot_Client_IdClient",
                        column: x => x.IdClient,
                        principalSchema: "Logistic",
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaritimeLot_TypeProduct_IdTypeProduct",
                        column: x => x.IdTypeProduct,
                        principalSchema: "Logistic",
                        principalTable: "TypeProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "MaritimeLotEntity_xx_constraint",
                        column: x => x.IdSeaport,
                        principalSchema: "Logistic",
                        principalTable: "Seaports",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LandLot",
                schema: "Logistic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTypeProduct = table.Column<int>(type: "int", nullable: false),
                    IdClient = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdWarehouse = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    VehiclePlate = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    GuideNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LandLot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LandLot_Client_IdClient",
                        column: x => x.IdClient,
                        principalSchema: "Logistic",
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LandLot_TypeProduct_IdTypeProduct",
                        column: x => x.IdTypeProduct,
                        principalSchema: "Logistic",
                        principalTable: "TypeProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "LandLotEntity_xx_constraint",
                        column: x => x.IdWarehouse,
                        principalSchema: "Logistic",
                        principalTable: "Warehouses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Client_IdCountry",
                schema: "Logistic",
                table: "Client",
                column: "IdCountry");

            migrationBuilder.CreateIndex(
                name: "IX_LandLot_IdClient",
                schema: "Logistic",
                table: "LandLot",
                column: "IdClient");

            migrationBuilder.CreateIndex(
                name: "IX_LandLot_IdTypeProduct",
                schema: "Logistic",
                table: "LandLot",
                column: "IdTypeProduct");

            migrationBuilder.CreateIndex(
                name: "IX_LandLot_IdWarehouse",
                schema: "Logistic",
                table: "LandLot",
                column: "IdWarehouse");

            migrationBuilder.CreateIndex(
                name: "IX_MaritimeLot_IdClient",
                schema: "Logistic",
                table: "MaritimeLot",
                column: "IdClient");

            migrationBuilder.CreateIndex(
                name: "IX_MaritimeLot_IdSeaport",
                schema: "Logistic",
                table: "MaritimeLot",
                column: "IdSeaport");

            migrationBuilder.CreateIndex(
                name: "IX_MaritimeLot_IdTypeProduct",
                schema: "Logistic",
                table: "MaritimeLot",
                column: "IdTypeProduct");

            migrationBuilder.CreateIndex(
                name: "IX_RolesPermissions_IdPermission",
                schema: "Security",
                table: "RolesPermissions",
                column: "IdPermission");

            migrationBuilder.CreateIndex(
                name: "IX_RolesPermissions_IdRol",
                schema: "Security",
                table: "RolesPermissions",
                column: "IdRol");

            migrationBuilder.CreateIndex(
                name: "IX_Seaports_IdCountry",
                schema: "Logistic",
                table: "Seaports",
                column: "IdCountry");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                schema: "Security",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_IdRol",
                schema: "Security",
                table: "User",
                column: "IdRol");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_IdCountry",
                schema: "Logistic",
                table: "Warehouses",
                column: "IdCountry");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LandLot",
                schema: "Logistic");

            migrationBuilder.DropTable(
                name: "MaritimeLot",
                schema: "Logistic");

            migrationBuilder.DropTable(
                name: "RolesPermissions",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "User",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "Warehouses",
                schema: "Logistic");

            migrationBuilder.DropTable(
                name: "Client",
                schema: "Logistic");

            migrationBuilder.DropTable(
                name: "TypeProduct",
                schema: "Logistic");

            migrationBuilder.DropTable(
                name: "Seaports",
                schema: "Logistic");

            migrationBuilder.DropTable(
                name: "Permission",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "Rol",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "Country",
                schema: "General");
        }
    }
}
