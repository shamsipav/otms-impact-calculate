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
        public double L1 { get; set; }
        public double L2 { get; set; }
        public double S1 { get; set; }
        public double S2 { get; set; }
        public double Wgr { get; set; }

        public double Cocks_Combustion_Temperature { get { return 4.1868 * selectedCocks.Q; } }

        public double L0 { get { return 0.001 * L1 * Cocks_Combustion_Temperature + L2 * selectedCocks.W; } }
        public double DeltaV { get { return S1 - 0.001 * S2 * Cocks_Combustion_Temperature - 0.00124 * (selectedCocks.W - Wgr); } }
        public double V0 { get { return L0 + DeltaV; } }

        //Averages:
        public double AverageGabbro { get { return Results.Select(x => x.materialBalance.Gabbro).Sum() / Results.Count; } }
        public double AverageLimestone { get { return Results.Select(x => x.materialBalance.Limestone).Sum() / Results.Count; } }
        public double Average_M_Limestone { get { return Results.Select(x => x.materialBalance.M_Limestone).Sum() / Results.Count; } }
        public double AverageCocks { get { return Results.Select(x => x.materialBalance.Cocks).Sum() / Results.Count; } }
        public double AverageGas { get { return Results.Select(x => x.materialBalance.Gas).Sum() / Results.Count; } }
        //-----
        public double AverageSiO2 { get { return Inputs.Select(x => x.oxidPercent.SiO2).Sum() / Inputs.Count; } }
        public double AverageAl2O3 { get { return Inputs.Select(x => x.oxidPercent.Al2O3).Sum() / Inputs.Count; } }
        public double AverageCaO { get { return Inputs.Select(x => x.oxidPercent.CaO).Sum() / Inputs.Count; } }
        public double AverageMgO { get { return Inputs.Select(x => x.oxidPercent.MgO).Sum() / Inputs.Count; } }
        public double AverageFeO { get { return Inputs.Select(x => x.oxidPercent.FeO).Sum() / Inputs.Count; } }

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

            result.Gas = CalcGas(input);
            result.Device = CalcDevice(input);
            result.Waste_Difference = result.Gas.V_Waste - result.Device.V_Waste;
            result.La = result.Device.A * L0;

            //Q-шки
            result.qCO = -0.0000000165d * Math.Pow(input.Smoke_Temperature, 3) + 0.0000120241d * Math.Pow(input.Smoke_Temperature, 2)
                         - 0.0043796347d * input.Smoke_Temperature + 1.2486284818d;
            result.qCO2 = 0.00000007d * Math.Pow(input.Smoke_Temperature, 2)+ 0.00005d * input.Smoke_Temperature + 1.256d;
            result.qO2 = -0.0000000206d * Math.Pow(input.Smoke_Temperature, 3) + 0.000014366d * Math.Pow(input.Smoke_Temperature, 2)
                         - 0.0050555626d * input.Smoke_Temperature + 1.4267518409d;
            result.qN2 = -0.0000000164d * Math.Pow(input.Smoke_Temperature, 3) + 0.0000119935d * Math.Pow(input.Smoke_Temperature, 2) 
                         - 0.0043758306d * input.Smoke_Temperature + 1.2486235488d;
            result.qSum = result.qCO + result.qCO2 + result.qN2 + result.qO2;
            //------------------

            result.materialBalance = CalcMaterialBalance(input, result);

            result.materialBalanceOnTonOfSmelt = CalcMaterialBalanceOnTonOfSmelt(input, result);

            result.ID = input.ID;

            return result;
        }

        //РАСЧЕТ ГАЗА И ПРИБОРА
        public A_AV_VW CalcGas(InputDataModel input)
        {
            double _A = 1f / (1f - 3.76f * (input.O2_Percentage - 0.5f * input.CO_Percentage) / input.N2_Percentage);
            double _V_Alpha = L0 * _A + DeltaV;
            double _V_Waste = input.materials.Cocks * ((12d / 22.4d) * input.CO_Percentage + (12d / 22.4d) * input.CO2_Percentage);
           
            A_AV_VW gas = new A_AV_VW()
            {
                A = _A,
                V_Alpha = _V_Alpha,
                V_Waste = _V_Waste,
                ID = input.ID
            };

            return gas;
        }
        public A_AV_VW CalcDevice(InputDataModel input)
        {
            double _A = input.Air_Spend / (input.materials.Cocks * L0);
            double _V_Alpha = L0 * _A + DeltaV;
            double _V_Waste = input.materials.Cocks * _V_Alpha;
           
            A_AV_VW device = new A_AV_VW()
            {
                A = _A,
                V_Alpha = _V_Alpha,
                V_Waste = _V_Waste,
                ID = input.ID
            };

            return device;
        }

        //МАТЕРИАЛЬНЫЙ БАЛАНС
        public MaterialBalance CalcMaterialBalance(InputDataModel input, ResultDataModel result)
        {
            double OutputGas = result.Gas.V_Waste * result.qSum;
            double Dust = (input.materials.Cocks + input.materials.Gabbro + input.materials.Limestone) * 0.03d;
            double Smelt = input.materials.MaterialSum - OutputGas - Dust;

            double WasteSum = Smelt + Dust + OutputGas;

            MaterialBalance mb = new MaterialBalance()
            {
                Cocks = input.materials.Cocks / input.materials.MaterialSum * 100,
                Gabbro = input.materials.Cocks / input.materials.MaterialSum * 100,
                Limestone = input.materials.Cocks / input.materials.MaterialSum * 100,
                M_Limestone = input.materials.Cocks / input.materials.MaterialSum * 100,
                Gas = input.materials.Cocks / input.materials.MaterialSum * 100,

                ID = input.ID,

                Smelt = Smelt,
                OutputGas = OutputGas,
                Dust = Dust,
                WasteSum = WasteSum

            };
            return mb;
        }
        public MaterialBalanceOnTonOfSmelt CalcMaterialBalanceOnTonOfSmelt(InputDataModel input, ResultDataModel result)
        {
            MaterialBalanceOnTonOfSmelt mb = new MaterialBalanceOnTonOfSmelt()
            {
                Cocks = result.materialBalance.Cocks * 1000 / result.materialBalance.Smelt,
                Gabbro = result.materialBalance.Gabbro * 1000 / result.materialBalance.Smelt,
                Limestone = result.materialBalance.Limestone * 1000 / result.materialBalance.Smelt,
                M_Limestone = result.materialBalance.M_Limestone * 1000 / result.materialBalance.Smelt,
                Gas = result.materialBalance.Gas * 1000 / result.materialBalance.Smelt,

                ID = input.ID,

                Smelt = result.materialBalance.Smelt * 1000 / result.materialBalance.Smelt,
                OutputGas = result.materialBalance.OutputGas * 1000 / result.materialBalance.Smelt,
                Dust = result.materialBalance.Dust * 1000 / result.materialBalance.Smelt,
                WasteSum = result.materialBalance.WasteSum * 1000 / result.materialBalance.Smelt,

            };
            return mb;
        }

        //ТЕПЛОВОЙ БАЛАНС
        public TeploBalance CalcTeploBalance(InputDataModel input, ResultDataModel result)
        {
            var Gas = 33500d * input.materials.Gas / 3600d;
            var Cocks = input.materials.Cocks * Cocks_Combustion_Temperature / 3600d;
            var Air = input.Air_Spend * (0.00000009d * input.Air_Temperature * input.Air_Temperature + 0.00004d * input.Air_Temperature + 1.296d)* input.Air_Temperature / 3600d;
           
            var SumPlus = Gas + Cocks + Air;


            var MeltGeneration = result.materialBalance.Smelt * input.Melt_Temperature *
               (AverageSiO2 * (0.00007d * input.Melt_Temperature + 1.1296d) +
                AverageAl2O3 * (0.0002d * input.Melt_Temperature + 1.0934d) +
                AverageCaO * (0.00009d * input.Melt_Temperature + 0.8804d) +
                AverageMgO * (0.0001d * input.Melt_Temperature + 1.2024d) +
                AverageFeO * (0.0001d * input.Melt_Temperature + 0.7232d)) / 3600d;

            var OutputGas = result.Device.V_Waste * input.Smoke_Temperature * (input.CO_Percentage *
                (0.0000001d * input.Smoke_Temperature * input.Smoke_Temperature + 0.00005d * input.Smoke_Temperature + 1.2979) / 100d + input.CO2_Percentage *
                (0.0000005d * input.Smoke_Temperature * input.Smoke_Temperature + 0.001d * input.Smoke_Temperature + 1.6016d) / 100d + 0.0125d *
                (0.00000003d * input.Smoke_Temperature * input.Smoke_Temperature + 0.0002d * input.Smoke_Temperature + 1.301d) + input.N2_Percentage *
                (0.0000007d * input.Smoke_Temperature * input.Smoke_Temperature + 0.00001d * input.Smoke_Temperature + 1.2981d) / 100d) / 3600d;

            var Dust = input.materials.Limestone * input.Smoke_Temperature *
               (0.4915d * (0.00007d * input.Smoke_Temperature + 1.1296d) +
                0.1323d * (0.0002d * input.Smoke_Temperature + 1.0934d) +
                0.2243d * (0.00009d * input.Smoke_Temperature + 0.8804d) +
                0.1077d * (0.0001d * input.Smoke_Temperature + 1.2024d) +
                0.0345d * (0.0001d * input.Smoke_Temperature + 0.7232d)) / 3600d;

            var ChemistryUnderburning = result.Device.V_Waste * input.CO_Percentage * 127.7d / 3600d;

            var CoolingWater = 1549.87427778d;


            var Endoterm_Reactions = SumPlus - MeltGeneration - OutputGas - Dust - ChemistryUnderburning - CoolingWater;

            var SumWaste = Endoterm_Reactions + CoolingWater + ChemistryUnderburning + Dust + OutputGas + MeltGeneration;

            TeploBalance tb = new TeploBalance()
            {
                Gas = Gas,
                Cocks = Cocks,
                Air = Air,
                SumPlus = SumPlus,

                ID = input.ID,

                MeltGeneration= MeltGeneration,
                OutputGas=OutputGas,
                Dust=Dust,

                ChemistryUnderburning=ChemistryUnderburning,
                Endoterm_Reactions=Endoterm_Reactions,
                CoolingWater=CoolingWater,
                SumWaste=SumWaste,
            };
            return tb;
        }
        public TeploBalanceOnTonOfSmelt CalcTeploBalanceOnTonOfSmelt(InputDataModel input, ResultDataModel result)
        {


            TeploBalanceOnTonOfSmelt tb = new TeploBalanceOnTonOfSmelt()
            {
             //   = result.teploBalance.Gas * 1000 / B129

            };
            return tb;
        }
    }
}
