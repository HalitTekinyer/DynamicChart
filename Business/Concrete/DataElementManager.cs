using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class DataElementManager:IDataElementManager
    {
        IDataElementDal _dataElementDal;
        public DataElementManager(IDataElementDal dataElementDal)
        {
            _dataElementDal = dataElementDal;
        }
        public List<DataElement> Get(Property property)
        {
            return _dataElementDal.Get(property);
        }
        public List<DataElement> GetAll()
        {
            return _dataElementDal.GetAll();
        }

        public List<DataElement> GetAllByStoredProcedure()
        {
            return _dataElementDal.GetAllByStoredProcedure();
        }

        public List<DataElement> GetAllByView()
        {
            var viewElementsList = _dataElementDal.GetAllByView();
            List<DataElement> dataElementList = new List<DataElement>();
            foreach(var viewElement in viewElementsList)
            {
                dataElementList.Add(new DataElement()
                {
                    Id = viewElement.Id,
                    Year = viewElement.Year,
                    SoldProduct = viewElement.SoldProduct,
                    DataSetId = viewElement.DataSetId
                });
            }
            return dataElementList;
        }
        public List<DataElement> GetAllByFunction()
        {
            return _dataElementDal.GetAllByFunction();
        }
        public bool TryConnection()
        {
            return _dataElementDal.TryConnection();
        }
        public List<string> GetDatabaseNames()
        {
            return _dataElementDal.GetDatabaseNames();
        }
        public List<Property> GetPropertyNames()
        {
            return _dataElementDal.GetPropertyNames();
        }
    }
}
