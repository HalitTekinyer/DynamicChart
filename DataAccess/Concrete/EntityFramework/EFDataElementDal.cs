using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Views;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFDataElementDal : IDataElementDal
    {
        public List<DataElement> GetAll()
        {
            using (DynamicChartDbContext context = new DynamicChartDbContext())
            {
                return context.DataSets.ToList();
            }
        }
        public List<GetDataElementView> GetAllByView()
        {
            using (DynamicChartDbContext context = new DynamicChartDbContext())
            {
                return context.vwGetDatas.ToList();
            }
        }
        public List<DataElement> GetAllByStoredProcedure()
        {
            using (DynamicChartDbContext context = new DynamicChartDbContext())
            {
                return context.DataSets.FromSql($"spGetDatas").ToList();
            }
        }
        public List<DataElement> GetAllByFunction()
        {
            using (DynamicChartDbContext context = new DynamicChartDbContext())
            {
                return context.DataSets.FromSql($"select * from fnGetDatas()").ToList();
            }
        }

        public bool TryConnection()
        {
            try
            {
                using(DynamicChartDbContext context = new DynamicChartDbContext())
                {
                    context.DataSets.Count();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
