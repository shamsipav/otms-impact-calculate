using Microsoft.EntityFrameworkCore.Migrations;

namespace ImpactCalculateWebApplication.Migrations
{
    public partial class ImpactCalculateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Inputs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Gabbro = table.Column<double>(type: "REAL", nullable: false),
                    Limestone = table.Column<double>(type: "REAL", nullable: false),
                    M_Limestone = table.Column<double>(type: "REAL", nullable: false),
                    Cocks = table.Column<double>(type: "REAL", nullable: false),
                    Gas = table.Column<double>(type: "REAL", nullable: false),
                    SiO2 = table.Column<double>(type: "REAL", nullable: false),
                    Al2O3 = table.Column<double>(type: "REAL", nullable: false),
                    CaO = table.Column<double>(type: "REAL", nullable: false),
                    MgO = table.Column<double>(type: "REAL", nullable: false),
                    FeO = table.Column<double>(type: "REAL", nullable: false),
                    InputWaterWaste = table.Column<double>(type: "REAL", nullable: false),
                    OutputWaterWaste = table.Column<double>(type: "REAL", nullable: false),
                    AverageWaterSteamTemperature = table.Column<double>(type: "REAL", nullable: false),
                    Air_Spend = table.Column<double>(type: "REAL", nullable: false),
                    Air_Pressure = table.Column<double>(type: "REAL", nullable: false),
                    Air_Temperature = table.Column<double>(type: "REAL", nullable: false),
                    Smoke_Temperature = table.Column<double>(type: "REAL", nullable: false),
                    Viscosity = table.Column<double>(type: "REAL", nullable: false),
                    Smelt_Temperature = table.Column<double>(type: "REAL", nullable: false),
                    CO_Percentage = table.Column<double>(type: "REAL", nullable: false),
                    CO2_Percentage = table.Column<double>(type: "REAL", nullable: false),
                    N2_Percentage = table.Column<double>(type: "REAL", nullable: false),
                    O2_Percentage = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inputs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Gas_A = table.Column<double>(type: "REAL", nullable: false),
                    Gas_V_Alpha = table.Column<double>(type: "REAL", nullable: false),
                    Gas_V_Waste = table.Column<double>(type: "REAL", nullable: false),
                    Device_A = table.Column<double>(type: "REAL", nullable: false),
                    Device_V_Alpha = table.Column<double>(type: "REAL", nullable: false),
                    Device_V_Waste = table.Column<double>(type: "REAL", nullable: false),
                    Waste_Difference = table.Column<double>(type: "REAL", nullable: false),
                    La = table.Column<double>(type: "REAL", nullable: false),
                    MaterialBalance_Gabbro = table.Column<double>(type: "REAL", nullable: false),
                    MaterialBalance_Limestone = table.Column<double>(type: "REAL", nullable: false),
                    MaterialBalance_M_Limestone = table.Column<double>(type: "REAL", nullable: false),
                    MaterialBalance_Cocks = table.Column<double>(type: "REAL", nullable: false),
                    MaterialBalance_Gas = table.Column<double>(type: "REAL", nullable: false),
                    MaterialBalance_SumPlus = table.Column<double>(type: "REAL", nullable: false),
                    MaterialBalance_Smelt = table.Column<double>(type: "REAL", nullable: false),
                    MaterialBalance_OutputGas = table.Column<double>(type: "REAL", nullable: false),
                    MaterialBalance_Dust = table.Column<double>(type: "REAL", nullable: false),
                    MaterialBalance_WasteSum = table.Column<double>(type: "REAL", nullable: false),
                    MaterialBalanceOnTonOfSmelt_Gabbro = table.Column<double>(type: "REAL", nullable: false),
                    MaterialBalanceOnTonOfSmelt_Limestone = table.Column<double>(type: "REAL", nullable: false),
                    MaterialBalanceOnTonOfSmelt_M_Limestone = table.Column<double>(type: "REAL", nullable: false),
                    MaterialBalanceOnTonOfSmelt_Cocks = table.Column<double>(type: "REAL", nullable: false),
                    MaterialBalanceOnTonOfSmelt_Gas = table.Column<double>(type: "REAL", nullable: false),
                    MaterialBalanceOnTonOfSmelt_SumPlus = table.Column<double>(type: "REAL", nullable: false),
                    MaterialBalanceOnTonOfSmelt_Smelt = table.Column<double>(type: "REAL", nullable: false),
                    MaterialBalanceOnTonOfSmelt_OutputGas = table.Column<double>(type: "REAL", nullable: false),
                    MaterialBalanceOnTonOfSmelt_Dust = table.Column<double>(type: "REAL", nullable: false),
                    MaterialBalanceOnTonOfSmelt_WasteSum = table.Column<double>(type: "REAL", nullable: false),
                    TeploBalance_Air = table.Column<double>(type: "REAL", nullable: false),
                    TeploBalance_Cocks = table.Column<double>(type: "REAL", nullable: false),
                    TeploBalance_Gas = table.Column<double>(type: "REAL", nullable: false),
                    TeploBalance_SumPlus = table.Column<double>(type: "REAL", nullable: false),
                    TeploBalance_MeltGeneration = table.Column<double>(type: "REAL", nullable: false),
                    TeploBalance_OutputGas = table.Column<double>(type: "REAL", nullable: false),
                    TeploBalance_Dust = table.Column<double>(type: "REAL", nullable: false),
                    TeploBalance_ChemistryUnderburning = table.Column<double>(type: "REAL", nullable: false),
                    TeploBalance_Endoterm_Reactions = table.Column<double>(type: "REAL", nullable: false),
                    TeploBalance_CoolingWater = table.Column<double>(type: "REAL", nullable: false),
                    TeploBalance_SumWaste = table.Column<double>(type: "REAL", nullable: false),
                    TeploBalanceOnTonOfSmelt_Air = table.Column<double>(type: "REAL", nullable: false),
                    TeploBalanceOnTonOfSmelt_Cocks = table.Column<double>(type: "REAL", nullable: false),
                    TeploBalanceOnTonOfSmelt_Gas = table.Column<double>(type: "REAL", nullable: false),
                    TeploBalanceOnTonOfSmelt_SumPlus = table.Column<double>(type: "REAL", nullable: false),
                    TeploBalanceOnTonOfSmelt_MeltGeneration = table.Column<double>(type: "REAL", nullable: false),
                    TeploBalanceOnTonOfSmelt_OutputGas = table.Column<double>(type: "REAL", nullable: false),
                    TeploBalanceOnTonOfSmelt_Dust = table.Column<double>(type: "REAL", nullable: false),
                    TeploBalanceOnTonOfSmelt_ChemistryUnderburning = table.Column<double>(type: "REAL", nullable: false),
                    TeploBalanceOnTonOfSmelt_Endoterm_Reactions = table.Column<double>(type: "REAL", nullable: false),
                    TeploBalanceOnTonOfSmelt_CoolingWater = table.Column<double>(type: "REAL", nullable: false),
                    TeploBalanceOnTonOfSmelt_SumWaste = table.Column<double>(type: "REAL", nullable: false),
                    qCO = table.Column<double>(type: "REAL", nullable: false),
                    qCO2 = table.Column<double>(type: "REAL", nullable: false),
                    qO2 = table.Column<double>(type: "REAL", nullable: false),
                    qN2 = table.Column<double>(type: "REAL", nullable: false),
                    qSum = table.Column<double>(type: "REAL", nullable: false),
                    W_m = table.Column<double>(type: "REAL", nullable: false),
                    W_g = table.Column<double>(type: "REAL", nullable: false),
                    W_m_g = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inputs");

            migrationBuilder.DropTable(
                name: "Results");
        }
    }
}
