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

namespace alibabaproy2022_v2.Controllers
{
    public class ContactoController: Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactoController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult Create(Contacto objContacto)
        {
            _context.Add(objContacto);
            _context.SaveChanges();
            ViewData["Message"] = "Se registro el contacto";
            return View("Index");
        }



        public async Task<IActionResult> Indexadmin(){
        var items = from o in _context.DataContactos select o;
        return View(await items.OrderByDescending(w => w.Id).ToListAsync());
        }
        public async Task<IActionResult> IndexadminResueltos(){
        var items = from o in _context.DataContactos select o;
        items = items.Where(s => s.Status.Contains("RESUELTO"));
        return View(await items.OrderByDescending(w => w.Id).ToListAsync());
        }
        public async Task<IActionResult> IndexadminSinResolver(){
        var items = from o in _context.DataContactos select o;
        items = items.Where(s => s.Status.Contains("SIN_RESOLVER"));
        
        return View(await items.OrderByDescending(w => w.Id).ToListAsync());
        }
        public async Task<IActionResult> IndexadminPendientes(){
        var items = from o in _context.DataContactos select o;
        items = items.Where(s => s.Status.Contains("PENDIENTE"));
        
        return View(await items.OrderByDescending(w => w.Id).ToListAsync());
        }



        // GET: Produtos/Delete/5

        public async Task<IActionResult> Delete(int? id)

        {

            if (id == null)

            {

                return NotFound();

            }



            var produto = await _context.DataContactos

                .FirstOrDefaultAsync(m => m.Id == id);

            if (produto == null)

            {

                return NotFound();

            }



            return View(produto);

        }



        // POST: Produtos/Delete/5

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