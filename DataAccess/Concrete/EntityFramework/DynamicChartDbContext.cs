using Core.DataAccess.Concrete;
using Entities.Concrete;
using Entities.Views;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class DynamicChartDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString.Get());
        }
        public DbSet<DataElement> DataSets { get; set; }
        public DbSet<GetDataElementView> vwGetDatas { get; set; }
    }
}
