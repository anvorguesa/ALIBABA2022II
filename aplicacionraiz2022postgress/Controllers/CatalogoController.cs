using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using aplicacionraiz2022postgress.Data;
using aplicacionraiz2022postgress.Models;
using Microsoft.AspNetCore.Identity;
using System.Data;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace aplicacionraiz2022postgress.Controllers
{
    public class CatalogoController : Controller
    {
        private readonly ILogger<CatalogoController> _logger;

        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        string baseUrl ="https://appfunctions-ab2022ii.azurewebsites.net/api/";

        public CatalogoController(ApplicationDbContext context,
                ILogger<CatalogoController> logger,
                UserManager<IdentityUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
    
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

         public async Task<IActionResult> Add(int? id)
        {
            var userID = _userManager.GetUserName(User);
            if(userID == null){
                ViewData["Message"] = "Por favor debe loguearse antes de agregar un producto";
                List<Producto> productos = new List<Producto>();
                return  View("Index",productos);
            }else{
                var producto = await _context.DataProductos.FindAsync(id);
                Proforma proforma = new Proforma();
                proforma.Producto = producto;
                decimal? precio = producto.Precio;
                proforma.Precio = (decimal)precio;
                proforma.Cantidad = 1;
                proforma.UserID = userID;
                _context.Add(proforma);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

        }
        public async Task<IActionResult> Add2(int? id,int? canti)
        {
            var userID = _userManager.GetUserName(User);
            if(userID == null){
                ViewData["Message"] = "Por favor debe loguearse antes de agregar un producto";
                List<Producto> productos = new List<Producto>();
                return  View("Index");
            }else{
                var producto = await _context.DataProductos.FindAsync(id);
                

                    ViewData["Message"] = "Ya se encuentra en carrito, borralo";
                    List<Producto> productos = new List<Producto>();
                    return RedirectToAction(nameof(Index)); 
                
                    Proforma proforma = new Proforma();
                    proforma.Producto = producto;
                    decimal? precio = producto.Precio;
                    proforma.Precio = (decimal)precio;
                    proforma.Cantidad = (int)canti;
                    proforma.UserID = userID;
                    _context.Add(proforma);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));    
                
                
            }

        }
    }
}