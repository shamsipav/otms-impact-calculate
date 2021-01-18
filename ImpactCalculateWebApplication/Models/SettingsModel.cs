using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImpactCalculateWebApplication.Models
{
    public class SettingsModel
    {
        public double L1 { get; set; } 
        public double L2 { get; set; } 
        public double S1 { get; set; } 
        public double S2 { get; set; } 
        public double Wgr { get; set; }

        public string SelectedCocks { get; set; }
    }
}
