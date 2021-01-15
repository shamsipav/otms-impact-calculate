using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ImpactCalculateWebApplication.Models
{
    public class ResultDataModel
    {
        public int ID { get; set; }

        public A_AV_VW Gas { get; set; } = new A_AV_VW();
        public A_AV_VW Device { get; set; } = new A_AV_VW();
        public double Waste_Difference { get; set; }
        public double La { get; set; }
        public MaterialBalance materialBalance { get; set; }
        public MaterialBalanceOnTonOfSmelt materialBalanceOnTonOfSmelt { get; set; }
        public TeploBalance teploBalance { get; set; }
        public TeploBalanceOnTonOfSmelt teploBalanceOnTonOfSmelt { get; set; }


        public double qCO{get;set;}
        public double qCO2 { get; set; }
        public double qO2 { get; set; }
        public double qN2 { get; set; }
        public double qSum { get; set; }


    }

    public class A_AV_VW
    {
        public int ID { get; set; }

        public double A { get; set; }
        public double V_Alpha { get; set; }
        public double V_Waste { get; set; }

    }

    //Материальный Баланс
    public class MaterialBalance
    {
        public int ID { get; set; }

        //Percent of:

        //Приход
        public double Gabbro { get; set; }
        public double Limestone { get; set; }
        public double M_Limestone { get; set; }
        public double Cocks { get; set; }
        public double Gas { get; set; }

        //Расход
        public double Smelt { get; set; }
        public double OutputGas { get; set; }
        public double Dust { get; set; }
        public double WasteSum { get; set; } 


    }
    public class MaterialBalanceOnTonOfSmelt
    {
        public int ID { get; set; }

        //Percent of:

        //Приход
        public double Gabbro { get; set; }
        public double Limestone { get; set; }
        public double M_Limestone { get; set; }
        public double Cocks { get; set; }
        public double Gas { get; set; }

        //Расход
        public double Smelt { get; set; }
        public double OutputGas { get; set; }
        public double Dust { get; set; }
        public double WasteSum { get; set; }

    }

    //Тепловой Баланс
    public class TeploBalance
    {
        public int ID { get; set; }

        //Percent of:

        //Приход
        public double Air { get; set; }
        public double Cocks { get; set; }
        public double Gas { get; set; }
        public double SumPlus { get; set; }

        //Расход
        public double MeltGeneration { get; set; }
        public double OutputGas { get; set; }
        public double Dust { get; set; }
        public double ChemistryUnderburning { get; set; }
        public double Endoterm_Reactions { get; set; }
        public double CoolingWater { get; set; }
        public double SumWaste { get; set; }

    }


    public class TeploBalanceOnTonOfSmelt
    {
        public int ID { get; set; }

        //Percent of:

        //Приход
        public double Air { get; set; }
        public double Cocks { get; set; }
        public double Gas { get; set; }

        //Расход
        public double SmeltGeneration { get; set; }
        public double OutputGas { get; set; }
        public double Dust { get; set; }
        public double ChemistryUnderburning { get; set; }
        public double Endoterm_Reactions { get; set; }
        public double CoolingWater { get; set; }
        public double WasteSum { get; set; }

    }

}
