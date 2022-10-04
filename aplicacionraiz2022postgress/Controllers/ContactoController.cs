using Microsoft.AspNetCore.Mvc;
using aplicacionraiz2022postgress.Models;
using aplicacionraiz2022postgress.Data;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net.Http.Headers;
using Newtonsoft.Json;
namespace aplicacionraiz2022postgress.Controllers
{
    public class ContactoController: Controller
    {
        private readonly ApplicationDbContext _context;
        
        string baseUrl ="https://appfunctions-ab2022ii.azurewebsites.net/api/";

        public ContactoController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,name,email,numero,subject,comment,AnotacionAdmin,Status")] Contacto contacto)
        {
            if (ModelState.IsValid){
            _context.Add(contacto);
            await _context.SaveChangesAsync();
            ViewData["Message"] = "Se registro el contacto";
            return View("Index");
            }
            ViewData["Message"] = "Complete todo el formulario";
            return View("Index");
        }



        public async Task<IActionResult> Indexadmin(){
            DataTable dt = new DataTable();
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage getData=await client.GetAsync("getContacto");
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
        public async Task<IActionResult> IndexadminResueltos(){
            DataTable dt = new DataTable();
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage getData=await client.GetAsync("getContacto?Status=RESUELTO");
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
        public async Task<IActionResult> IndexadminSinResolver(){
            DataTable dt = new DataTable();
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage getData=await client.GetAsync("getContacto?Status=SIN_RESOLVER");
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
        public async Task<IActionResult> IndexadminPendientes(){
            DataTable dt = new DataTable();
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage getData=await client.GetAsync("getContacto?Status=PENDIENTE");
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

        public async Task<IActionResult> Delete(int? id)

        {
            DataTable dt = new DataTable();
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData=await client.GetAsync("getContacto?id="+id+"&Status=");

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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produto = await _context.DataContactos.FindAsync(id);
            _context.DataContactos.Remove(produto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Indexadmin));

        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var contacto = await _context.DataContactos.FindAsync(id);
            if (contacto == null)
            {
                return NotFound();
            }
            return View(contacto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,name,email,numero,subject,comment,AnotacionAdmin,Status")] Contacto contacto)
        {
            if (id != contacto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contacto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactoExists(contacto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Indexadmin));
            }
            return View(contacto);
        }

        private bool ContactoExists(int id)
        {
            return _context.DataContactos.Any(e => e.Id == id);
        }
    }
}