using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImpactCalculateWebApplication.Models
{
    public class InputDataModel
    {
        public int ID { get; set; }

        // Расход материалов кг/ч
        public double Gabbro { get; set; }
        public double Limestone { get; set; }
        public double M_Limestone { get; set; }
        public double Cocks { get; set; }
        public double Gas { get; set; }
        //100%
        public double MaterialSum { get { return Gabbro + Limestone + M_Limestone + Cocks + Gas; } }
        //----------------------------------------------


        //Содержание оксидов в %
        public double SiO2 { get; set; }
        public double Al2O3 { get; set; }
        public double CaO { get; set; }
        public double MgO { get; set; }
        public double FeO { get; set; }
        //---------------------------------------------


        //Cooling Water Parametres
        public double InputWaterWaste { get; set; }
        public double OutputWaterWaste { get; set; }
        public double AverageWaterSteamTemperature { get; set; }
        //---------------------------------------------

        public double Air_Spend { get; set; }
        public double Air_Pressure { get; set; }
        public double Air_Temperature { get; set; }
        public double Smoke_Temperature { get; set; }
        public double Viscosity { get; set; }
        public double Smelt_Temperature { get; set; }
        public double CO_Percentage { get; set; }
        public double CO2_Percentage { get; set; }
        public double N2_Percentage { get; set; }
        public double O2_Percentage { get; set; }

        public static InputDataModel GetDefaultData()
        {
            return new InputDataModel
            {
                Air_Spend = 5850,
                Air_Pressure = 70,
                Air_Temperature = 610.2,
                Smoke_Temperature = 146.5,
                Viscosity = 22,
                Smelt_Temperature = 1521,
                CO_Percentage = 8.7,
                CO2_Percentage = 12.4,
                N2_Percentage = 77.67,
                O2_Percentage = 1.23
            };
        }

    }



}
