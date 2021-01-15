using System;
using ImpactCalculateWebApplication.Models;
using ImpactCalculateWebApplication.Models.HomeViewModels;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            IndexViewModel vm = new IndexViewModel(new System.Collections.Generic.List<InputDataModel>());

            Console.ReadLine();
        }
    }
}
