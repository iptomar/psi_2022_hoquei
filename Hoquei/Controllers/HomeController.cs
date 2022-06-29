using Hoquei.Data;
using Hoquei.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Hoquei.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly HoqueiDB _context;

        public HomeController(ILogger<HomeController> logger, HoqueiDB context)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {

            return View(await _context.Jogo.Include(c => c.Clube_Casa)
                                           .ThenInclude(c => c.Foto)
                                           .Include(c => c.Clube_Fora)
                                           .ThenInclude(c => c.Foto)
                                           //.Include(c => c.)
                                           .ToListAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
