using AspMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebAPI;

namespace AspMVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpPost]
        public async Task<ActionResult> Index(ConnectionStringModel connectionStringModel)
        {
            using (HttpClient client = new HttpClient())
            {
                var endpoint = "https://localhost:44369/ConnectionString";
                var response = client.PostAsJsonAsync(endpoint, connectionStringModel);
                response.Wait();
                if (response.Result.IsSuccessStatusCode)
                {
                    var readData = response.Result.Content.ReadFromJsonAsync<List<string>>();
                    readData.Wait();
                    return RedirectToAction("GetChart", new { databaseNames = readData.Result });
                }
                return View(false);
            }
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View(true);
        }
        [HttpGet]
        public IActionResult GetChart(List<string> databaseNames)
        {
            return View(databaseNames);
        }
        [HttpGet]
        public async Task<ActionResult> Chart(ChartTypeModel chartTypeModelInMethod)
        {
            using (HttpClient client = new HttpClient())
            {
                DataTypeModel dataTypeModel = new DataTypeModel() {dataTypeName = chartTypeModelInMethod.dataTypeName, dataTypeValue = chartTypeModelInMethod.dataTypeValue };
                GetChartReturnModel returnModel = new GetChartReturnModel();
                returnModel.chartTypeModel = chartTypeModelInMethod;
                var endpoint = "https://localhost:44369/DataSet";
                var response = client.PostAsJsonAsync(endpoint, dataTypeModel);
                response.Wait();
                if (response.Result.IsSuccessStatusCode)
                {
                    returnModel.formPosted = true;
                    var readData = response.Result.Content.ReadFromJsonAsync<List<DataModel>>();
                    readData.Wait();
                    List<DataSetModel> dataSetsInMethod = new List<DataSetModel>();
                    List<string> dataSetLabelsInMethod = new List<string>();
                    foreach (var dataModel in readData.Result)
                    {
                        if (dataSetLabelsInMethod.Any(label => label == dataModel.Year.ToString()) == false)
                        {
                            dataSetLabelsInMethod.Add(dataModel.Year.ToString());
                        }
                        DataSetModel foundDataSet = dataSetsInMethod.SingleOrDefault(dataSet => dataSet.label == dataModel.DataSetId.ToString());
                        if (foundDataSet == null)
                        {
                            dataSetsInMethod.Add(new DataSetModel()
                            {
                                label = dataModel.DataSetId.ToString(),
                                data = new List<string> { dataModel.SoldProduct.ToString() }
                            });
                        }
                        else
                        {
                            foundDataSet.data.Add(dataModel.SoldProduct.ToString());
                        }
                    }
                    returnModel.dataSetLabels = JsonSerializer.Serialize(dataSetLabelsInMethod);
                    returnModel.dataSets = JsonSerializer.Serialize(dataSetsInMethod);
                    return PartialView("_Chart", returnModel);
                }
                return PartialView("_Chart", returnModel);
            }
        }
        [HttpGet]
        public async Task<ActionResult> DataType(DataTypeReturnModel dataTypeReturnModel)
        {
            using (HttpClient client = new HttpClient())
            {
                List<string> objects = new List<string>();
                ConnectionStringModel connectionStringModel = new ConnectionStringModel();
                var endpointConnectionString = "https://localhost:44369/ConnectionString";
                var endpointDataSet = "https://localhost:44369/DataSet";
                var responseGetConnectionString = client.GetAsync(endpointConnectionString);
                responseGetConnectionString.Wait();
                var readDataGetConnectionString = responseGetConnectionString.Result.Content.ReadFromJsonAsync<ConnectionStringModel>();
                connectionStringModel.ServerIp = readDataGetConnectionString.Result.ServerIp;
                connectionStringModel.DatabaseName = dataTypeReturnModel.DatabaseName;
                connectionStringModel.User = readDataGetConnectionString.Result.User;
                connectionStringModel.Password = readDataGetConnectionString.Result.Password;
                var responsePostConnectionString = client.PostAsJsonAsync(endpointConnectionString, connectionStringModel);
                responsePostConnectionString.Wait();
                var responseGetPropertyNames = client.GetAsync(endpointDataSet);
                responseGetPropertyNames.Wait();
                if (responseGetPropertyNames.Result.IsSuccessStatusCode)
                {
                    var readDataGetProperyNames = responseGetPropertyNames.Result.Content.ReadFromJsonAsync<List<DatabasePropertyModel>>();
                    readDataGetProperyNames.Wait();
                    return PartialView("_DataType", readDataGetProperyNames.Result);
                }
                return PartialView("_DataType");
            }
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}