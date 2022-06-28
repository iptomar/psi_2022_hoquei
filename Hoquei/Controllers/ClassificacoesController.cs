using Hoquei.Data;
using Hoquei.Models;
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
            return View(await _context.Classificacoes.Include(c => c.Clube).Include(c => c.Campeonato_Id).ToListAsync());
        }


        // GET: Classificações/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var classificacoes = await _context.Classificacoes.FirstOrDefaultAsync();


            if (classificacoes == null)
            {
                return View("Index");
            }
            return View(classificacoes);
        }

        // POST: Campeonatoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Pontos, Golos_Marcados, Golos_Sofridos")] Classificacoes novasClassificacoes)
        {

            var classificacoes = await _context.Classificacoes.Where(l => l.Id_TabelaDeClassificacoes == id).FirstOrDefaultAsync();

            if (ModelState.IsValid)
            {
                try
                {

                    classificacoes.Pontos = novasClassificacoes.Pontos;
                    classificacoes.Golos_Marcados = novasClassificacoes.Golos_Marcados;
                    classificacoes.Golos_Sofridos = novasClassificacoes.Golos_Sofridos;
                    _context.Update(classificacoes);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassificacoesExists(classificacoes.Id_TabelaDeClassificacoes))
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
            return View(novasClassificacoes);
        }
        private bool ClassificacoesExists(int id)
        {
            return _context.Classificacoes.Any(e => e.Id_TabelaDeClassificacoes == id);
        }
    }
}
