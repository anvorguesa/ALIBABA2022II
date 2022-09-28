using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace aplicacionraiz2022postgress.Controllers
{
    public class apiCatalogoController : Controller
    {
        private readonly ILogger<apiCatalogoController> _logger;

        string baseUrl ="https://appfunctions-ab2022ii.azurewebsites.net/api/";
        public apiCatalogoController(ILogger<apiCatalogoController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            DataTable dt = new DataTable();
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData=await client.GetAsync("getproductos");

                if (getData.IsSuccessStatusCode)
                {
                    string result = getData.Content.ReadAsStringAsync().Result;
                    dt=JsonConvert.DeserializeObject<DataTable>(result); //
                }else{
                    Console.WriteLine("Error Calling web API");
                }
                ViewData.Model = dt;
            }


            return View();
        }
    public async Task<IActionResult> Details(int? id)
    {
        DataTable dt = new DataTable();
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData=await client.GetAsync("getproductos");

                if (getData.IsSuccessStatusCode)
                {
                    string result = getData.Content.ReadAsStringAsync().Result;
                    dt=JsonConvert.DeserializeObject<DataTable>(result); //
                }else{
                    Console.WriteLine("Error Calling web API");
                }
                ViewData.Model = dt;
            }


            return View();
    }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}