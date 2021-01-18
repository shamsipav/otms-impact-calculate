using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImpactCalculateWebApplication.Models.HomeViewModels
{

    public class IndexViewModel
    {      
        public static int LastID = 1;
        public Cocks selectedCocks = CocksModel.Kemerovo_3_4;

        public const double qAir = 1.293d;

        public double L1 { get; set; } = 0.2627d;
        public double L2 { get; set; } = 0.07d;
        public double S1 { get; set; } = 0.4d;
        public double S2 { get; set; } = 0.0086d;
        public double Wgr { get; set; } = 12d;

        public double Cocks_Combustion_Temperature { get { return 4.1868 * selectedCocks.Q; } }

        public double L0 { get { return 0.001 * L1 * Cocks_Combustion_Temperature + L2 * selectedCocks.W; } }
        public double DeltaV { get { return S1 - 0.001 * S2 * Cocks_Combustion_Temperature - 0.00124 * (selectedCocks.W - Wgr); } }
        public double V0 { get { return L0 + DeltaV; } }

        //Averages:
        public double AverageGabbro { get { return Results.Select(x => x.MaterialBalance_Gabbro).Sum() / Results.Count; } }
        public double AverageLimestone { get { return Results.Select(x => x.MaterialBalance_Limestone).Sum() / Results.Count; } }
        public double Average_M_Limestone { get { return Results.Select(x => x.MaterialBalance_M_Limestone).Sum() / Results.Count; } }
        public double AverageCocks { get { return Results.Select(x => x.MaterialBalance_Cocks).Sum() / Results.Count; } }
        public double AverageGas { get { return Results.Select(x => x.MaterialBalance_Gas).Sum() / Results.Count; } }
        //-----
        public double AverageSiO2 { get { return Inputs.Select(x => x.SiO2).Sum() / Inputs.Count; } }
        public double AverageAl2O3 { get { return Inputs.Select(x => x.Al2O3).Sum() / Inputs.Count; } }
        public double AverageCaO { get { return Inputs.Select(x => x.CaO).Sum() / Inputs.Count; } }
        public double AverageMgO { get { return Inputs.Select(x => x.MgO).Sum() / Inputs.Count; } }
        public double AverageFeO { get { return Inputs.Select(x => x.FeO).Sum() / Inputs.Count; } }

        //----------------------------------------

        //Percents Of M_Limestone:
        public double GabbroPercent { get { return 100 * AverageGabbro / Average_M_Limestone; } }
        public double LimestonePercent { get { return 100 * AverageLimestone / Average_M_Limestone; } }
        public double CocksPercent { get { return 100 * AverageCocks / Average_M_Limestone; } }
        public double GasPercent { get { return 100 * AverageGas / Average_M_Limestone; } }
        //----------------------------------------

        public List<InputDataModel> Inputs { get; set; }
        public List<ResultDataModel> Results { get; set; }

        public IndexViewModel(List<InputDataModel> inputs)
        {
            Inputs = inputs;
            Results = new List<ResultDataModel>();
        }

        //-------------------------------------------//
        public void CalculateResults()
        {
            foreach (InputDataModel input in Inputs)
            {
                Results.Add(CalculateResult(input));
            }

        }

        //РАСЧЕТ РЕЗУЛЬТАТА
        public ResultDataModel CalculateResult(InputDataModel input)
        {
            var result = new ResultDataModel();

            CalcGas(input, result);
            CalcDevice(input, result);
            result.Waste_Difference = result.Gas_V_Waste - result.Device_V_Waste;
            result.La = result.Device_A * L0;

            //Q-шки
            result.qCO = -0.0000000165d * Math.Pow(input.Smoke_Temperature, 3) + 0.0000120241d * Math.Pow(input.Smoke_Temperature, 2)
                         - 0.0043796347d * input.Smoke_Temperature + 1.2486284818d;
            result.qCO2 = 0.00000007d * Math.Pow(input.Smoke_Temperature, 2)+ 0.00005d * input.Smoke_Temperature + 1.256d;
            result.qO2 = -0.0000000206d * Math.Pow(input.Smoke_Temperature, 3) + 0.000014366d * Math.Pow(input.Smoke_Temperature, 2)
                         - 0.0050555626d * input.Smoke_Temperature + 1.4267518409d;
            result.qN2 = -0.0000000164d * Math.Pow(input.Smoke_Temperature, 3) + 0.0000119935d * Math.Pow(input.Smoke_Temperature, 2) 
                         - 0.0043758306d * input.Smoke_Temperature + 1.2486235488d;
            result.qSum = (result.qCO * input.CO_Percentage + result.qCO2 * input.CO2_Percentage + result.qN2 * input.N2_Percentage + result.qO2 * input.O2_Percentage) / 100d;
            //------------------

            CalcMaterialBalance(input, result);

            CalcMaterialBalanceOnTonOfSmelt(input, result);

            CalcTeploBalance(input, result);

            CalcTeploBalanceOnTonOfSmelt(input, result);

            //W-шки
            result.W_m = (input.Cocks * result.MaterialBalance_Cocks * 1.008d +
                          input.Gabbro * result.MaterialBalance_Gabbro * 0.468d +
                          input.Limestone * result.MaterialBalance_Limestone * 1.34d +
                          input.M_Limestone * result.MaterialBalance_M_Limestone * 1.34d) / 100d;

            result.W_g = result.TeploBalance_OutputGas * 3600d / input.Smoke_Temperature;
            result.W_m_g = result.W_m / result.W_g;
            //------------------

            result.ID = input.ID;

            return result;
        }

        //РАСЧЕТ ГАЗА И ПРИБОРА
        public void CalcGas(InputDataModel input, ResultDataModel result)
        {
            double _A = 1f / (1f - 3.76f * (input.O2_Percentage - 0.5f * input.CO_Percentage) / input.N2_Percentage);
            double _V_Alpha = L0 * _A + DeltaV;
            double _V_Waste = input.Cocks * ((12d / 22.4d) * input.CO_Percentage + (12d / 22.4d) * input.CO2_Percentage);
           
            result.Gas_A = _A;
            result.Gas_V_Alpha = _V_Alpha;
            result.Gas_V_Waste = _V_Waste;
        }
        public void CalcDevice(InputDataModel input, ResultDataModel result)
        {
            double _A = input.Air_Spend / (input.Cocks * L0);
            double _V_Alpha = L0 * _A + DeltaV;
            double _V_Waste = input.Cocks * _V_Alpha;

            result.Device_A = _A;
            result.Device_V_Alpha = _V_Alpha;
            result.Device_V_Waste = _V_Waste;
        }

        //МАТЕРИАЛЬНЫЙ БАЛАНС
        public void CalcMaterialBalance(InputDataModel input, ResultDataModel result)
        {
            result.MaterialBalance_Air = input.Air_Spend * qAir;
            result.MaterialBalance_SumPlus = input.MaterialSum + result.MaterialBalance_Air;

            result.MaterialBalance_Cocks = 100 * input.Cocks / result.MaterialBalance_SumPlus;
            result.MaterialBalance_Gabbro = 100 * input.Gabbro / result.MaterialBalance_SumPlus;
            result.MaterialBalance_Limestone = 100 * input.Limestone / result.MaterialBalance_SumPlus;
            result.MaterialBalance_M_Limestone = 100 * input.M_Limestone / result.MaterialBalance_SumPlus;
            result.MaterialBalance_Gas = 100 * input.Gas / result.MaterialBalance_SumPlus;

            double OutputGas = result.Gas_V_Waste * result.qSum;
            double Dust = (input.Cocks + input.Gabbro + input.Limestone) * 0.03d;
            double Smelt = result.MaterialBalance_SumPlus - OutputGas - Dust;
            double WasteSum = Smelt + Dust + OutputGas;

            result.MaterialBalance_Smelt = Smelt;
            result.MaterialBalance_OutputGas = OutputGas;
            result.MaterialBalance_Dust = Dust;
            result.MaterialBalance_WasteSum = WasteSum;
        }
        public void CalcMaterialBalanceOnTonOfSmelt(InputDataModel input, ResultDataModel result)
        {
            result.MaterialBalanceOnTonOfSmelt_Cocks = result.MaterialBalance_Cocks * 1000 / result.MaterialBalance_Smelt;
            result.MaterialBalanceOnTonOfSmelt_Gabbro = result.MaterialBalance_Gabbro * 1000 / result.MaterialBalance_Smelt;
            result.MaterialBalanceOnTonOfSmelt_Limestone = result.MaterialBalance_Limestone * 1000 / result.MaterialBalance_Smelt;
            result.MaterialBalanceOnTonOfSmelt_M_Limestone = result.MaterialBalance_M_Limestone * 1000 / result.MaterialBalance_Smelt;
            result.MaterialBalanceOnTonOfSmelt_Gas = result.MaterialBalance_Gas * 1000 / result.MaterialBalance_Smelt;
            result.MaterialBalanceOnTonOfSmelt_Air = result.MaterialBalance_Air * 1000 / result.MaterialBalance_Smelt;
            result.MaterialBalanceOnTonOfSmelt_SumPlus = result.MaterialBalance_SumPlus * 1000 / result.MaterialBalance_Smelt;

            result.MaterialBalanceOnTonOfSmelt_Smelt = result.MaterialBalance_Smelt * 1000 / result.MaterialBalance_Smelt;
            result.MaterialBalanceOnTonOfSmelt_OutputGas = result.MaterialBalance_OutputGas * 1000 / result.MaterialBalance_Smelt;
            result.MaterialBalanceOnTonOfSmelt_Dust = result.MaterialBalance_Dust * 1000 / result.MaterialBalance_Smelt;
            result.MaterialBalanceOnTonOfSmelt_WasteSum = result.MaterialBalance_WasteSum * 1000 / result.MaterialBalance_Smelt;

        }

        //ТЕПЛОВОЙ БАЛАНС
        public void CalcTeploBalance(InputDataModel input, ResultDataModel result)
        {
            var Gas = 33500d * input.Gas / 3600d;
            var Cocks = input.Cocks * Cocks_Combustion_Temperature / 3600d;
            var Air = input.Air_Spend * (0.00000009d * input.Air_Temperature * input.Air_Temperature + 0.00004d * input.Air_Temperature + 1.296d)* input.Air_Temperature / 3600d;
           
            var SumPlus = Gas + Cocks + Air;


            var MeltGeneration = result.MaterialBalance_Smelt * input.Smelt_Temperature *
               (AverageSiO2 * (0.00007d * input.Smelt_Temperature + 1.1296d) +
                AverageAl2O3 * (0.0002d * input.Smelt_Temperature + 1.0934d) +
                AverageCaO * (0.00009d * input.Smelt_Temperature + 0.8804d) +
                AverageMgO * (0.0001d * input.Smelt_Temperature + 1.2024d) +
                AverageFeO * (0.0001d * input.Smelt_Temperature + 0.7232d)) / 3600d;

            var OutputGas = result.Device_V_Waste * input.Smoke_Temperature * (input.CO_Percentage *
                (0.0000001d * input.Smoke_Temperature * input.Smoke_Temperature + 0.00005d * input.Smoke_Temperature + 1.2979) / 100d + input.CO2_Percentage *
                (0.0000005d * input.Smoke_Temperature * input.Smoke_Temperature + 0.001d * input.Smoke_Temperature + 1.6016d) / 100d + 0.0125d *
                (0.00000003d * input.Smoke_Temperature * input.Smoke_Temperature + 0.0002d * input.Smoke_Temperature + 1.301d) + input.N2_Percentage *
                (0.0000007d * input.Smoke_Temperature * input.Smoke_Temperature + 0.00001d * input.Smoke_Temperature + 1.2981d) / 100d) / 3600d;

            var Dust = input.Limestone * input.Smoke_Temperature *
               (0.4915d * (0.00007d * input.Smoke_Temperature + 1.1296d) +
                0.1323d * (0.0002d * input.Smoke_Temperature + 1.0934d) +
                0.2243d * (0.00009d * input.Smoke_Temperature + 0.8804d) +
                0.1077d * (0.0001d * input.Smoke_Temperature + 1.2024d) +
                0.0345d * (0.0001d * input.Smoke_Temperature + 0.7232d)) / 3600d;

            var ChemistryUnderburning = result.Device_V_Waste * input.CO_Percentage * 127.7d / 3600d;

            var CoolingWater = input.AverageWaterSteamTemperature * (input.InputWaterWaste * 4.2023d - input.OutputWaterWaste * 4.1934d) * 1000d / 3600d;


            var Endoterm_Reactions = SumPlus - MeltGeneration - OutputGas - Dust - ChemistryUnderburning - CoolingWater;

            var SumWaste = Endoterm_Reactions + CoolingWater + ChemistryUnderburning + Dust + OutputGas + MeltGeneration;


            result.TeploBalance_Gas = Gas;
            result.TeploBalance_Cocks = Cocks;
            result.TeploBalance_Air = Air;
            result.TeploBalance_SumPlus = SumPlus;


            result.TeploBalance_MeltGeneration = MeltGeneration;
            result.TeploBalance_OutputGas = OutputGas;
            result.TeploBalance_Dust = Dust;

            result.TeploBalance_ChemistryUnderburning = ChemistryUnderburning;
            result.TeploBalance_Endoterm_Reactions = Endoterm_Reactions;
            result.TeploBalance_CoolingWater = CoolingWater;
            result.TeploBalance_SumWaste = SumWaste;
            
        }
        public void CalcTeploBalanceOnTonOfSmelt(InputDataModel input, ResultDataModel result)
        {
            var Gas = result.TeploBalance_Gas * 1000 / result.MaterialBalance_Smelt;
            var Cocks = result.TeploBalance_Cocks * 1000 / result.MaterialBalance_Smelt;
            var Air = result.TeploBalance_Air * 1000 / result.MaterialBalance_Smelt;

            var SumPlus = Gas + Cocks + Air;


            var MeltGeneration = result.TeploBalance_MeltGeneration * 1000 / result.MaterialBalance_Smelt;
            var OutputGas = result.TeploBalance_OutputGas * 1000 / result.MaterialBalance_Smelt;
            var Dust = result.TeploBalance_Dust * 1000 / result.MaterialBalance_Smelt;
            var ChemistryUnderburning = result.TeploBalance_ChemistryUnderburning * 1000 / result.MaterialBalance_Smelt;
            var CoolingWater = result.TeploBalance_CoolingWater * 1000 / result.MaterialBalance_Smelt;
            var Endoterm_Reactions = result.TeploBalance_Endoterm_Reactions * 1000 / result.MaterialBalance_Smelt;

            var SumWaste = MeltGeneration + OutputGas + Dust + ChemistryUnderburning + CoolingWater + Endoterm_Reactions;


            result.TeploBalanceOnTonOfSmelt_Gas = Gas;
            result.TeploBalanceOnTonOfSmelt_Cocks = Cocks;
            result.TeploBalanceOnTonOfSmelt_Air = Air;
            result.TeploBalanceOnTonOfSmelt_SumPlus = SumPlus;


            result.TeploBalanceOnTonOfSmelt_MeltGeneration = MeltGeneration;
            result.TeploBalanceOnTonOfSmelt_OutputGas = OutputGas;
            result.TeploBalanceOnTonOfSmelt_Dust = Dust;

            result.TeploBalanceOnTonOfSmelt_ChemistryUnderburning = ChemistryUnderburning;
            result.TeploBalanceOnTonOfSmelt_Endoterm_Reactions = Endoterm_Reactions;
            result.TeploBalanceOnTonOfSmelt_CoolingWater = CoolingWater;
            result.TeploBalanceOnTonOfSmelt_SumWaste = SumWaste;
        }
    }
}
