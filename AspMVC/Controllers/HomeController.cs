using AspMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
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
            using(HttpClient client = new HttpClient())
            {
                var endpoint = "https://localhost:44369/ConnectionString";
                var response = client.PostAsJsonAsync(endpoint, connectionStringModel);
                response.Wait();
                if (response.Result.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetChart");
                }
                return View(false);
            }
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View(true);
        }
        [HttpPost]
        public async Task<ActionResult> GetChart(ChartTypeModel chartTypeModelInMethod)
        {
            using(HttpClient client = new HttpClient())
            {
                GetChartReturnModel returnModel = new GetChartReturnModel();
                returnModel.chartTypeModel = chartTypeModelInMethod;
                var endpoint = "https://localhost:44369/DataSet";
                var response = client.PostAsJsonAsync(endpoint, chartTypeModelInMethod.dataType);
                response.Wait();
                if (response.Result.IsSuccessStatusCode)
                {
                    returnModel.formPosted = true;
                    var readData = response.Result.Content.ReadFromJsonAsync<List<DataModel>>();
                    readData.Wait();
                    List<DataSetModel> dataSetsInMethod = new List<DataSetModel>();
                    List<string> dataSetLabelsInMethod = new List<string>();
                    foreach(var dataModel in readData.Result)
                    {
                        if (dataSetLabelsInMethod.Any(label => label == dataModel.Year.ToString()) == false)
                        {
                            dataSetLabelsInMethod.Add(dataModel.Year.ToString());
                        }
                        DataSetModel foundDataSet = dataSetsInMethod.SingleOrDefault(dataSet => dataSet.label == dataModel.DataSetId.ToString());
                        if(foundDataSet == null)
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
                    return View(returnModel);
                }
                return View(returnModel);
            }
        }
        [HttpGet]
        public IActionResult GetChart()
        {
            return View(new GetChartReturnModel());
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}