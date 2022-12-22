using Business.Abstract;
using Business.Concrete;
using Core.DataAccess.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

internal class Program
{
    private static void Main(string[] args)
    {
        ConnectionString.ServerIp = "127.0.0.1";
        ConnectionString.DatabaseName = "DyanmicChartDb";
        ConnectionString.User = "sa";
        ConnectionString.Password = "123456";
        IDataElementManager dataElementManager = new DataElementManager(new EFDataElementDal());
        List<DataElement> dataElements = dataElementManager.GetAllByFunction();
        foreach(var dataElement in dataElements)
        {
            Console.WriteLine(dataElement.Id + " " + dataElement.Year + " " + dataElement.SoldProduct + " " + dataElement.DataSetId);
        }
    }
}