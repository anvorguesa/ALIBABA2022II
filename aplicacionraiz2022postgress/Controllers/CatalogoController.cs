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

namespace aplicacionraiz2022postgress.Controllers
{
    public class CatalogoController : Controller
    {
        private readonly ILogger<CatalogoController> _logger;

        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CatalogoController(ApplicationDbContext context,
                ILogger<CatalogoController> logger,
                UserManager<IdentityUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
    
        }

        public async Task<IActionResult> Index(string? searchString,string? searchString1,string? searchString2,string? searchString3,int? searchString4,int? searchString5,string? searchString6,string? searchString7,string? searchString8)
        {
            
            var productos = from o in _context.DataProductos select o;
            //SELECT * FROM t_productos -> &
            if(!String.IsNullOrEmpty(searchString )){
                productos = productos.Where(s => s.Keywords.Contains(searchString)); 
            }
            if(String.IsNullOrEmpty(searchString2 )){
                 
            }else{
                productos = productos.Where(s => s.Clase.Contains(searchString2));
            }
            if(String.IsNullOrEmpty(searchString3 )){
                 
            }else{
                productos = productos.Where(s => s.Estado.Contains(searchString3));
            }
            if(searchString4 == null && searchString5 == null){

            }else if(searchString4 == null && searchString5 >0){
                productos = productos.Where(s => s.Precio>=searchString5);
            }else if(searchString4 >0 && searchString5 == null){
                productos = productos.Where(s => s.Precio<=searchString4);
            }else{
                productos = productos.Where(s => s.Precio>=searchString4 && s.Precio<=searchString5);
            }

            productos = productos.Where(s => s.Status.Contains("Activo"));
            return View(await productos.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            Producto objProduct = await _context.DataProductos.FindAsync(id);
            if(objProduct == null){
                return NotFound();
            }
            return View(objProduct);
        }

        public async Task<IActionResult> Add(int? id){
            var userID = _userManager.GetUserName(User);
            if(userID == null){
                ViewData["Message"] = "Por favor debe loguearse antes de agregar un producto";
                List<Producto> productos = new List<Producto>();
                return  View("Index",productos);
            }else{
                var producto = await _context.DataProductos.FindAsync(id);
                Proforma proforma = new Proforma();
                proforma.Producto = producto;
                proforma.Precio = (decimal)producto.Precio;
                proforma.Cantidad = 1;
                proforma.UserID = userID;
                _context.Add(proforma);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

        }
    }
}