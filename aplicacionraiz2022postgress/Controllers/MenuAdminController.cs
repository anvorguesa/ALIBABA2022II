using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace aplicacionraiz2022postgress.Controllers
{
    public class MenuAdminController : Controller
    {
        private readonly ILogger<MenuAdminController> _logger;

    public MenuAdminController(ILogger<MenuAdminController> logger)
    {
        _logger = logger;
    }
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Menureclamos()
    {
        return View();
    }
    public IActionResult Menupagos()
    {
        return View();
    }
    public IActionResult Menumatriculas()
    {
        return View();
    }
    }
}