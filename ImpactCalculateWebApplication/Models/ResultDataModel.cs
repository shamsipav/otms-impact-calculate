using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ImpactCalculateWebApplication.Models
{
    public class ResultDataModel
    {
        public int ID { get; set; }

        //Gas
        public double Gas_A { get; set; }
        public double Gas_V_Alpha { get; set; }
        public double Gas_V_Waste { get; set; }

        //Device
        public double Device_A { get; set; }
        public double Device_V_Alpha { get; set; }
        public double Device_V_Waste { get; set; }


        public double Waste_Difference { get; set; }
        public double La { get; set; }

        //Балансы:

        #region Материальный Баланс
        //Приход
        public double MaterialBalance_Gabbro { get; set; }
        public double MaterialBalance_Limestone { get; set; }
        public double MaterialBalance_M_Limestone { get; set; }
        public double MaterialBalance_Cocks { get; set; }
        public double MaterialBalance_Gas { get; set; }

        //Расход
        public double MaterialBalance_Smelt { get; set; }
        public double MaterialBalance_OutputGas { get; set; }
        public double MaterialBalance_Dust { get; set; }
        public double MaterialBalance_WasteSum { get; set; }
        #endregion Материальный Баланс

        #region Материальный Баланс На Тонну Расплава
        //Приход
        public double MaterialBalanceOnTonOfSmelt_Gabbro { get; set; }
        public double MaterialBalanceOnTonOfSmelt_Limestone { get; set; }
        public double MaterialBalanceOnTonOfSmelt_M_Limestone { get; set; }
        public double MaterialBalanceOnTonOfSmelt_Cocks { get; set; }
        public double MaterialBalanceOnTonOfSmelt_Gas { get; set; }

        //Расход
        public double MaterialBalanceOnTonOfSmelt_Smelt { get; set; }
        public double MaterialBalanceOnTonOfSmelt_OutputGas { get; set; }
        public double MaterialBalanceOnTonOfSmelt_Dust { get; set; }
        public double MaterialBalanceOnTonOfSmelt_WasteSum { get; set; }
        #endregion Материальный Баланс На Тонну Расплава

        #region Тепловой Баланс
        //Приход
        public double TeploBalance_Air { get; set; }
        public double TeploBalance_Cocks { get; set; }
        public double TeploBalance_Gas { get; set; }
        public double TeploBalance_SumPlus { get; set; }

        //Расход
        public double TeploBalance_MeltGeneration { get; set; }
        public double TeploBalance_OutputGas { get; set; }
        public double TeploBalance_Dust { get; set; }
        public double TeploBalance_ChemistryUnderburning { get; set; }
        public double TeploBalance_Endoterm_Reactions { get; set; }
        public double TeploBalance_CoolingWater { get; set; }
        public double TeploBalance_SumWaste { get; set; }
        #endregion Тепловой Баланс

        #region Тепловой Баланс На Тонну Расплава
        //Приход
        public double TeploBalanceOnTonOfSmelt_Air { get; set; }
        public double TeploBalanceOnTonOfSmelt_Cocks { get; set; }
        public double TeploBalanceOnTonOfSmelt_Gas { get; set; }
        public double TeploBalanceOnTonOfSmelt_SumPlus { get; set; }

        //Расход
        public double TeploBalanceOnTonOfSmelt_MeltGeneration { get; set; }
        public double TeploBalanceOnTonOfSmelt_OutputGas { get; set; }
        public double TeploBalanceOnTonOfSmelt_Dust { get; set; }
        public double TeploBalanceOnTonOfSmelt_ChemistryUnderburning { get; set; }
        public double TeploBalanceOnTonOfSmelt_Endoterm_Reactions { get; set; }
        public double TeploBalanceOnTonOfSmelt_CoolingWater { get; set; }
        public double TeploBalanceOnTonOfSmelt_SumWaste { get; set; }
        #endregion Тепловой Баланс На Тонну Расплава


        public double qCO{get;set;}
        public double qCO2 { get; set; }
        public double qO2 { get; set; }
        public double qN2 { get; set; }
        public double qSum { get; set; }


    }


}
