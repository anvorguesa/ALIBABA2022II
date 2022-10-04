using System.Data;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> Index(String? Clase, String? Subclase)
        {
            DataTable dt = new DataTable();
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
                if (String.IsNullOrEmpty(Clase) && String.IsNullOrEmpty(Subclase))
                {
                    HttpResponseMessage getData=await client.GetAsync("getproductos");
                    if (getData.IsSuccessStatusCode)
                        {
                            string result = getData.Content.ReadAsStringAsync().Result;
                            dt=JsonConvert.DeserializeObject<DataTable>(result); //
                        }else{
                            Console.WriteLine("Error Calling web API");
                        }
                        ViewData.Model = dt;
                }else if(String.IsNullOrEmpty(Clase))
                {
                    HttpResponseMessage getData=await client.GetAsync("getQueryProductos?Clase="+"&Subclase="+Subclase);
                    if (getData.IsSuccessStatusCode)
                        {
                            string result = getData.Content.ReadAsStringAsync().Result;
                            dt=JsonConvert.DeserializeObject<DataTable>(result); //
                        }else{
                            Console.WriteLine("Error Calling web API");
                        }
                        ViewData.Model = dt;
                }else if(!String.IsNullOrEmpty(Clase) || String.IsNullOrEmpty(Subclase))
                {
                    HttpResponseMessage getData=await client.GetAsync("getQueryProductos?Clase="+Clase+"&Subclase=");
                    if (getData.IsSuccessStatusCode)
                        {
                            string result = getData.Content.ReadAsStringAsync().Result;
                            dt=JsonConvert.DeserializeObject<DataTable>(result); //
                        }else{
                            Console.WriteLine("Error Calling web API");
                        }
                        ViewData.Model = dt;
                }else
                {
                    HttpResponseMessage getData=await client.GetAsync("getQuery2Productos?Clase="+Clase+"&Subclase="+Subclase);
                    if (getData.IsSuccessStatusCode)
                        {
                            string result = getData.Content.ReadAsStringAsync().Result;
                            dt=JsonConvert.DeserializeObject<DataTable>(result); //
                        }else{
                            Console.WriteLine("Error Calling web API");
                        }
                        ViewData.Model = dt;
                }
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

                HttpResponseMessage getData=await client.GetAsync("getproductos?id="+id);

                if (getData.IsSuccessStatusCode)
                {
                    string result = getData.Content.ReadAsStringAsync().Result;
                    dt=JsonConvert.DeserializeObject<DataTable>(result); //
                }else{
                    Console.WriteLine("Error Calling web API");
                }
                ViewData.Model = dt;
            }


            return View(dt);
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}