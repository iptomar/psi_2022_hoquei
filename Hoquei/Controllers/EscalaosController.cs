using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hoquei.Data;
using Hoquei.Models;
using Microsoft.AspNetCore.Authorization;

namespace Hoquei.Controllers
{
    public class EscalaosController : Controller
    {
        private readonly HoqueiDB _context;

        public EscalaosController(HoqueiDB context)
        {
            _context = context;
        }

        // GET: Escalaos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Escalao.ToListAsync());
        }

        // GET: Escalaos/Details/5


        // GET: Escalaos/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Escalaos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,designacao")] Escalao escalao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(escalao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(escalao);
        }

   
        private bool EscalaoExists(int id)
        {
            return _context.Escalao.Any(e => e.Id == id);
        }
    }
}
