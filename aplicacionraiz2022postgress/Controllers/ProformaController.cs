using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using aplicacionraiz2022postgress.Models;
using aplicacionraiz2022postgress.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Dynamic;




namespace aplicacionraiz2022postgress.Controllers
{
    //CARRITO
    public class ProformaController: Controller
    {
        private readonly ILogger<CatalogoController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager; 

        public ProformaController(ApplicationDbContext context,
            ILogger<CatalogoController> logger,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;

        }
        public async Task<IActionResult> Index(){
            var userID = _userManager.GetUserName(User);
            var items = from o in _context.DataProforma select o;
            items = items.
                Include(p => p.Producto).
                Where(w => w.UserID.Equals(userID) && w.Status.Equals("PENDIENTE"));

            var carrito = await items.ToListAsync();
            var total = carrito.Sum(c => c.Cantidad * c.Precio);

            dynamic model = new ExpandoObject();
            model.montoTotal = total;
            model.elementosCarrito = carrito;

            return View(model);
        }

        // GET: Produtos/Delete/5








        
        public async Task<IActionResult> Delete(int? id)

        {

            if (id == null)

            {

                return NotFound();

            }



            var proform = await _context.DataProforma

                .FirstOrDefaultAsync(m => m.Id == id);

            if (proform == null)

            {

                return NotFound();

            }



            return View(proform);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var proform = await _context.DataProforma.FindAsync(id);
            _context.DataProforma.Remove(proform);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }




    }
}