using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hoquei.Data;
using Hoquei.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Hoquei.Controllers
{
    public class CampeonatoController : Controller
    {
        private readonly HoqueiDB _context;

        public CampeonatoController(HoqueiDB context)
        {
            _context = context;
        }
        
        // GET: Campeonatoes
        public async Task<IActionResult> Index()
        {

            return View(await _context.Campeonato.Include(l => l.escalao).ToListAsync());
        }

        // GET: Campeonatoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campeonato = await _context.Campeonato.Include(c =>c.ListaDeJogos)
                                                      .ThenInclude(a => a.Clube_Casa)
                                                      .Include(c => c.ListaDeJogos)
                                                      .ThenInclude(b => b.Clube_Fora)
                                                      .FirstOrDefaultAsync(m => m.Id == id);

            List < Jogo >  jogos= new List<Jogo>();
            jogos = campeonato.ListaDeJogos.ToList();

            if (campeonato == null)
            {
                return NotFound();
            }

            return View(campeonato);
        }

        // GET: Campeonatoes/Create
        public IActionResult Create()
        {
            ViewBag.data =  _context.Escalao.ToList();
            //ViewBag.data = _context.Escalao.OrderBy(e => e.Id).ToListAsync();
            return View();
        }

        // POST: Campeonatoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Designacao")] Campeonato campeonato)
        {
            if (ModelState.IsValid)
            {
            campeonato.escalao = await _context.Escalao.FindAsync(int.Parse( Request.Form["Escalao"]));
            
                _context.Add(campeonato);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(campeonato);
        }

        // GET: Campeonatoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var items = _context.Escalao.ToList();
            ViewBag.data = items;

            var campeonato = await _context.Campeonato.Include(e => e.escalao).FirstOrDefaultAsync(i => i.Id == id);
            if (campeonato == null)
            {
                return NotFound();
            }
            return View(campeonato);
        }

        // POST: Campeonatoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Designacao")] Campeonato campeonato)
        {
            if (id != campeonato.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //Verificar se os dados da dropdown foram alterados
                //caso não tenham sido alterados a view retorna o nome do escalão
                //caso tenha sido alterado a view retorna o id do escalao em formato string ex. "1"
                int number;
                if(Int32.TryParse(Request.Form["Escalao"], out number))
                {
                    campeonato.escalao = await _context.Escalao.FindAsync(number); 
                } 
                
             
                try
                {
                    _context.Update(campeonato);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CampeonatoExists(campeonato.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(campeonato);
        }


        private bool CampeonatoExists(int id)
        {
            return _context.Campeonato.Any(e => e.Id == id);
        }
    }
}
