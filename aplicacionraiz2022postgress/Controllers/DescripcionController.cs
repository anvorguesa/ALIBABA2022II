using Microsoft.AspNetCore.Mvc;
using aplicacionraiz2022postgress.Models;
using aplicacionraiz2022postgress.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Dynamic;
using System.Data;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace aplicacionraiz2022postgress.Controllers
{

    public class DescripcionController: Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

    public DescripcionController(ApplicationDbContext context ,UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index() 
        {
           var UserID = _userManager.GetUserName(User);
            var Description = from o in _context.DataDescripcion select o;
                Description = Description.Where(x => x.UserId.Equals(UserID));
            return View(await Description.ToListAsync());
        }
    }


}