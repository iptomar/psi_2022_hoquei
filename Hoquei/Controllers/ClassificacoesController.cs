using Hoquei.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoquei.Controllers
{
    public class ClassificacoesController : Controller
    {
        private readonly HoqueiDB _context;

        public ClassificacoesController(HoqueiDB context)
        {
            _context = context;
        }

        // GET: Calssificacoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Classificacoes.ToListAsync());
        }
    }
}
