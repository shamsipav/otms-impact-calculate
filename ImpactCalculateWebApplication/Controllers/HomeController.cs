using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ImpactCalculateWebApplication.Models;
using ImpactCalculateWebApplication.Models.HomeViewModels;
using Microsoft.EntityFrameworkCore;

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
        public IActionResult Index(List<InputDataModel> input, Cocks selectedCocks, double L1, double L2, double S1, double S2, double Wgr)
        {
            db.Inputs.RemoveRange(db.Inputs);
            db.Results.RemoveRange(db.Results);

            IndexViewModel viewModel = new IndexViewModel(input);

                                      // Установить выбранный на форме кокс*
            viewModel.selectedCocks = CocksModel.Kemerovo_3_4;

            //viewModel.L1 = L1;
            //viewModel.L2 = L2;
            //viewModel.S1 = S1;
            //viewModel.S2 = S2;
            //viewModel.Wgr = Wgr;

            viewModel.CalculateResults();

            db.Inputs.AddRange(input);
            db.Results.AddRange(viewModel.Results);

            //db.SaveChanges();

            //IndexViewModel.LastID = input[input.Count - 1].ID;

            return View("Result", viewModel);
        }

        public IActionResult Index()
        {
            // 15.01.2020
            List<InputDataModel> inputs = new List<InputDataModel>()
            {
                InputDataModel.GetDefaultData(),
                InputDataModel.GetDefaultData(),
                InputDataModel.GetDefaultData(),
                InputDataModel.GetDefaultData(),
                InputDataModel.GetDefaultData(),
                InputDataModel.GetDefaultData(),
                InputDataModel.GetDefaultData(),
                InputDataModel.GetDefaultData(),
                InputDataModel.GetDefaultData(),
                InputDataModel.GetDefaultData(),
                InputDataModel.GetDefaultData(),
                InputDataModel.GetDefaultData(),
                InputDataModel.GetDefaultData(),
                InputDataModel.GetDefaultData(),
                InputDataModel.GetDefaultData()
            };

            //var Inputs = db.Inputs.ToList();

            //if(Inputs.Count!=0)
            //IndexViewModel.LastID = Inputs[Inputs.Count-1].ID;

            //return View(Inputs);

            return View(inputs);
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
