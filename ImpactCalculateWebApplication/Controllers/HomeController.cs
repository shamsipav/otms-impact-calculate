using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ImpactCalculateWebApplication.Models;
using ImpactCalculateWebApplication.Models.HomeViewModels;
using Newtonsoft.Json;
using System.IO;

namespace ImpactCalculateWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ImpactCalculationDBContext db = new ImpactCalculationDBContext();


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //------------------------------------------------------//
        //------------------------------------------------------//
        //------------------------------------------------------//


        [HttpPost]
        public IActionResult Index(List<InputDataModel> input, string selectedCocks, double L1, double L2, double S1, double S2, double Wgr)
        {
            db.Inputs.RemoveRange(db.Inputs);
            db.Results.RemoveRange(db.Results);

            IndexViewModel viewModel = new IndexViewModel(input);

            System.Reflection.PropertyInfo[] cockses = typeof(CocksModel).GetProperties();
            viewModel.selectedCocks = (Cocks)cockses.First(x => x.Name == selectedCocks).GetValue(null);

            viewModel.L1 = L1;
            viewModel.L2 = L2;
            viewModel.S1 = S1;
            viewModel.S2 = S2;
            viewModel.Wgr = Wgr;

            SettingsModel settings = new SettingsModel() { L1 = L1, L2 = L2, S1 = S1, S2 = S2, Wgr = Wgr, SelectedCocks = selectedCocks };

            StreamWriter sw = new StreamWriter(@"jija.json");
            sw.Write(JsonConvert.SerializeObject(settings));
            sw.Close();


            viewModel.CalculateResults();

            db.Inputs.AddRange(input);
            db.Results.AddRange(viewModel.Results);

            db.SaveChanges();

            return View("Result", viewModel);
        }

        public IActionResult Index()
        {
            StreamReader sr = new StreamReader(@"jija.json");
            var settings = JsonConvert.DeserializeObject<SettingsModel>(sr.ReadToEnd());
            sr.Close();

            //SettingsModel settings = new SettingsModel() { L1 = 0.2627d, L2 = 0.07d, S1 = 0.4d, S2 = 0.0086d, Wgr = 12d, SelectedCocks = "Kemerovo_3_4" };

            //List<InputDataModel> Inputs = new List<InputDataModel>()
            //{
            //    InputDataModel.GetDefaultData()
            //};

            var Inputs = db.Inputs.ToList();

    
            ViewBag.settings = settings;
            return View(Inputs);
        }

        //------------------------------------------------------//
        //------------------------------------------------------//
        //------------------------------------------------------//

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }

   


}
