using Microsoft.EntityFrameworkCore.Migrations;

namespace ImpactCalculateWebApplication.Migrations
{
    public partial class impactCalcDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "A_AV_VW",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    A = table.Column<double>(type: "REAL", nullable: false),
                    V_Alpha = table.Column<double>(type: "REAL", nullable: false),
                    V_Waste = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_A_AV_VW", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Gabbro = table.Column<double>(type: "REAL", nullable: false),
                    Limestone = table.Column<double>(type: "REAL", nullable: false),
                    M_Limestone = table.Column<double>(type: "REAL", nullable: false),
                    Cocks = table.Column<double>(type: "REAL", nullable: false),
                    Gas = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GasID = table.Column<int>(type: "INTEGER", nullable: true),
                    DeviceID = table.Column<int>(type: "INTEGER", nullable: true),
                    Waste_Difference = table.Column<double>(type: "REAL", nullable: false),
                    La = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Results_A_AV_VW_DeviceID",
                        column: x => x.DeviceID,
                        principalTable: "A_AV_VW",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Results_A_AV_VW_GasID",
                        column: x => x.GasID,
                        principalTable: "A_AV_VW",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Inputs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    materialsID = table.Column<int>(type: "INTEGER", nullable: true),
                    Air_Spend = table.Column<double>(type: "REAL", nullable: false),
                    Air_Pressure = table.Column<double>(type: "REAL", nullable: false),
                    Air_Temperature = table.Column<double>(type: "REAL", nullable: false),
                    Smoke_Temperature = table.Column<double>(type: "REAL", nullable: false),
                    Viscosity = table.Column<double>(type: "REAL", nullable: false),
                    Melt_Temperature = table.Column<double>(type: "REAL", nullable: false),
                    CO_Percentage = table.Column<double>(type: "REAL", nullable: false),
                    CO2_Percentage = table.Column<double>(type: "REAL", nullable: false),
                    N2_Percentage = table.Column<double>(type: "REAL", nullable: false),
                    O2_Percentage = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inputs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Inputs_Materials_materialsID",
                        column: x => x.materialsID,
                        principalTable: "Materials",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inputs_materialsID",
                table: "Inputs",
                column: "materialsID");

            migrationBuilder.CreateIndex(
                name: "IX_Results_DeviceID",
                table: "Results",
                column: "DeviceID");

            migrationBuilder.CreateIndex(
                name: "IX_Results_GasID",
                table: "Results",
                column: "GasID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inputs");

            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "A_AV_VW");
        }
    }
}
