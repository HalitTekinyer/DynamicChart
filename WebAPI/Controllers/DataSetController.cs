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
        public List<DataElement> Get()
        {
            return _dataElementManager.GetAll();
        }
        [HttpPost]
        public List<DataElement> Post([FromBody]string dataType)
        {
            if (dataType == "sp")
            {
                return _dataElementManager.GetAllByStoredProcedure();
            }
            else if(dataType == "vw")
            {
                return _dataElementManager.GetAllByView();
            }
            else if (dataType == "fn")
            {
                return _dataElementManager.GetAllByFunction();
            }
            return _dataElementManager.GetAll();
        }
    }
}
