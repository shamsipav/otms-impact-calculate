using System;
using System.Collections.Generic;
using System.Text;
using ImpactCalculateWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace ImpactCalculateWebApplication
{
    class ImpactCalculationDBContext : DbContext
    {
        public DbSet<InputDataModel> Inputs { get; set; }
        public DbSet<ResultDataModel> Results { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=ImpactCalculateDB.db");

        
    }



}
