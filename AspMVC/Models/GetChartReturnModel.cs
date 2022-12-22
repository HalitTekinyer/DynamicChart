using System.Collections.Specialized;

namespace AspMVC.Models
{
    public class GetChartReturnModel
    {
        public ChartTypeModel chartTypeModel { get; set; }
        public string dataSets { get; set; }
        public string dataSetLabels { get; set; }
        public bool formPosted { get; set; } = false;
    }
}
