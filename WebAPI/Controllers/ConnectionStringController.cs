using Business.Abstract;
using Business.Concrete;
using Core.DataAccess.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    public class ConnectionStringController
    {
        IDataElementManager _dataElementManager;
        public ConnectionStringController(IDataElementDal dataElementDal)
        {
            _dataElementManager = new DataElementManager(dataElementDal);
        }
        [HttpPost]
        public IActionResult post([FromBody]ConnectionStringTemplate connectionStringTemplate)
        {
            try
            {
                Console.WriteLine(connectionStringTemplate.ServerIp);
                ConnectionString.ServerIp = connectionStringTemplate.ServerIp;
                ConnectionString.DatabaseName = connectionStringTemplate.DatabaseName;
                ConnectionString.User = connectionStringTemplate.User;
                ConnectionString.Password = connectionStringTemplate.Password;
                if (_dataElementManager.TryConnection())
                {
                    return new OkResult();
                }
                return new BadRequestResult();
            }
            catch
            {
                return new BadRequestResult();
            }
        }
    }
}
