using Entities.Concrete;
using Entities.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IDataElementDal
    {
        public List<DataElement> Get(Property property);
        public List<DataElement> GetAll();
        public List<GetDataElementView> GetAllByView();
        public List<DataElement> GetAllByStoredProcedure();
        public List<DataElement> GetAllByFunction();
        public bool TryConnection();
        public List<string> GetDatabaseNames();
        public List<Property> GetPropertyNames();
    }
}
