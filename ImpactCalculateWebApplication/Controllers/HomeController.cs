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
        public IActionResult Index(List<InputDataModel> input)
        {
            //db.Inputs.RemoveRange(db.Inputs);

            //db.Inputs.AddRange(input);           

            //db.SaveChanges();

            //IndexViewModel.LastID = input[input.Count - 1].ID;

            IndexViewModel viewModel = new IndexViewModel(input);

            viewModel.CalculateResults();

            return View("Result", viewModel);
        }

        public IActionResult Index()
        {
            // 15.01.2020
            List<InputDataModel> inputs = new List<InputDataModel>()
            {
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
