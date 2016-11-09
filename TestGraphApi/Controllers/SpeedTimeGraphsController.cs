using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TestGraphApi.Models;
using TestGraphApi.TestData;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.IO;
using System.Web.UI.DataVisualization.Charting;
using TestGraphApi.Services;
using System.Diagnostics;

namespace TestGraphApi.Controllers
{
    [Route("api/graphs/speedtime")]
    public class SpeedTimeGraphsController : ApiController
    {
        // GET api/graphs/speedtime
        public async Task<IHttpActionResult> Get()
        {
            // generating a graphData based on Test SpeedTime[] data
            var testSpeedTimes = new TestSpeedTimes();
            var testData = await Task.Factory.StartNew(() => testSpeedTimes.GetTestData(amount: 500));

            var graphData = new GraphData() { Points = testData };

            // serializing the graphData to a json
            string json = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(graphData));

            // and return it as IHttpActionResult
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StringContent(json);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("text/json");
            return ResponseMessage(result);
        }



        //TODO ASYNC REQUEST
        //TODO CLEAN UP POST METHOD

        // POST api/graphs/speedtime
        public IHttpActionResult Post([FromBody]GraphData graphData)
        {
            if (graphData == null)
                return BadRequest();

            var processGraphDataService = new ProcessGraphDataService();

            //graphData.Points = processGraphDataService.SortPoints_Linq(graphData.Points);
            //graphData.Points = processGraphDataService.SortPoints_MyQuickSort(graphData.Points);
            graphData.Points = graphData.Points.QuickSort((x, y) => x.Time.CompareTo(y.Time)).ToArray();
            var optimizedPoints = processGraphDataService.OptimizeSortedPoints(graphData.Points, optimizeFactor: 0.3f);


            // creating a chart
            var chart = new Chart() { Width = 500, Height = 200 };
            chart.Titles.Add("SpeedTime");

            var chartArea = new ChartArea();
            chartArea.AxisX.Title = "Time";
            chartArea.AxisX.LabelStyle.Format = "{0}";
            chartArea.AxisY.Title = "Speed";
            chartArea.AxisY.LabelStyle.Format = "{0}";
            chart.ChartAreas.Add(chartArea);


            // and merge data
            var series = new Series() { ChartType = SeriesChartType.Line };
            foreach (var point in graphData.Points)
            {
                series.Points.AddXY(point.Time, point.Speed);
            }
            chart.Series.Add(series);

            var optimizedSeries = new Series() { ChartType = SeriesChartType.Line };
            foreach (var point in optimizedPoints)
            {
                optimizedSeries.Points.AddXY(point.Time, point.Speed);
            }
            chart.Series.Add(optimizedSeries);



            // serializing the chart to a jpeg
            MemoryStream ms = new MemoryStream();
            chart.SaveImage(ms, ChartImageFormat.Png);

            // and return it as IHttpActionResult
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(ms.ToArray());
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            return ResponseMessage(result);
        }

    }
}
