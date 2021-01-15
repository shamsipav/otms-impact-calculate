using Microsoft.EntityFrameworkCore.Migrations;

namespace ImpactCalculateWebApplication.Migrations
{
    public partial class ImpactCalculationDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "A_AV_VW",
                columns: table => new
                {
                    Key = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    A = table.Column<double>(type: "REAL", nullable: false),
                    V_Alpha = table.Column<double>(type: "REAL", nullable: false),
                    V_Waste = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_A_AV_VW", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "Inputs",
                columns: table => new
                {
                    Key = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
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
                    table.PrimaryKey("PK_Inputs", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    Key = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GasKey = table.Column<int>(type: "INTEGER", nullable: true),
                    DeviceKey = table.Column<int>(type: "INTEGER", nullable: true),
                    Waste_Difference = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.Key);
                    table.ForeignKey(
                        name: "FK_Results_A_AV_VW_DeviceKey",
                        column: x => x.DeviceKey,
                        principalTable: "A_AV_VW",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Results_A_AV_VW_GasKey",
                        column: x => x.GasKey,
                        principalTable: "A_AV_VW",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Results_DeviceKey",
                table: "Results",
                column: "DeviceKey");

            migrationBuilder.CreateIndex(
                name: "IX_Results_GasKey",
                table: "Results",
                column: "GasKey");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inputs");

            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "A_AV_VW");
        }
    }
}
