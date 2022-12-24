using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IDataElementManager
    {
        public List<DataElement> Get(Property property);
        public List<DataElement> GetAll();
        public List<DataElement> GetAllByView();
        public List<DataElement> GetAllByStoredProcedure();
        public List<DataElement> GetAllByFunction();
        public bool TryConnection();
        public List<string> GetDatabaseNames();
        public List<Property> GetPropertyNames();
    }
}
