using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataSetController
    {
        IDataElementManager _dataElementManager;
        public DataSetController(IDataElementDal dataElementDal)
        {
            _dataElementManager = new DataElementManager(dataElementDal);
        }
        [HttpGet]
        public List<Property> Get()
        {
            return _dataElementManager.GetPropertyNames();
        }
        [HttpPost]
        public List<DataElement> Post([FromBody]Property property)
        {
            return _dataElementManager.Get(property);
        }
    }
}
