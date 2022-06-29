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
using Microsoft.AspNetCore.Authorization;

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
            return View(await _context.Campeonato.ToListAsync());
            //return View(await _context.Campeonato.Include(l => l.escalao).ToListAsync());
        }

        // GET: Campeonatoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            var campeonato = await _context.Campeonato.Include(c => c.ListaDeJogos)
                                                      .ThenInclude(a => a.Clube_Casa)
                                                      .ThenInclude(a => a.Foto)
                                                      .Include(c => c.ListaDeJogos)
                                                      .ThenInclude(b => b.Clube_Fora)
                                                      .ThenInclude(a => a.Foto)
                                                      .Include(c => c.ListaDeClassificacoes)
                                                      .FirstOrDefaultAsync(m => m.Id == id);

            //var classificacoes =  _context.Classificacoes
            //    .Where(f => f.Campeonato_Id.Id == id)
            //    .OrderByDescending(f => f.Pontos)
            //    .FirstOrDefaultAsync();

            List < Jogo >  jogos= new List<Jogo>();
            jogos = campeonato.ListaDeJogos.ToList();

            if (campeonato == null)
            {
                return NotFound();
            }

            //List<Jogo> jogos = new List<Jogo>();
            //jogos = campeonato.ListaDeJogos.ToList();

            //if (campeonato == null)
            //{
            //    return NotFound();
            //}
            ViewBag.ListaDeClassificacoes = _context.Classificacoes.Where(i => i.Campeonato_Id.Id == id).OrderBy(c => c.Pontos).ToListAsync();
            return View(campeonato);
        }
        [Authorize(Roles = "Admin")]
        // GET: Campeonatoes/Create
        public IActionResult Create()
        {
            ViewBag.data =  _context.Escalao.ToList();
            ViewBag.ListaDeClubes = _context.Clube.OrderBy(c => c.Id).ToList();
            //ViewBag.data = _context.Escalao.OrderBy(e => e.Id).ToListAsync();

            return View();
        }

        // POST: Campeonatoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Designacao")] Campeonato campeonato, int[] Clubes)
        {
            
            if (Clubes.Length == 0)
            {
                //É gerada uma mensagem de erro
                ModelState.AddModelError("", "É necessário selecionar pelo menos um Clube.");
                // gerar as listas
                ViewBag.data = _context.Escalao.ToList();
                ViewBag.ListaDeClubes = _context.Clube.OrderBy(c => c.Id).ToList();
                // devolver controlo à View
                return View(campeonato);
            }

            List<Clube> ListaDeClubesEscolhidos = new List<Clube>();
            // Para cada objeto escolhido..
            foreach (int item in Clubes)
            {
                //procurar o jogador
                Clube clube = _context.Clube.Find(item);
                // adicionar o jogador à lista
                ListaDeClubesEscolhidos.Add(clube);
            }

            // adicionar a lista ao objeto de jogo
            campeonato.ListaDeClubes = ListaDeClubesEscolhidos;

            List<Classificacoes> ListaDeClassificacoes = new List<Classificacoes>();

            foreach (Clube item in campeonato.ListaDeClubes)
            {
                //procurar o jogador
                Clube clube = _context.Clube.Find(item.Id);
                Classificacoes classificacoes = new Classificacoes();
                classificacoes.Campeonato_Id = campeonato;
                classificacoes.Clube = item;
                classificacoes.Pontos = 0;
                classificacoes.Golos_Marcados = 0;
                classificacoes.Golos_Sofridos = 0;
                ListaDeClassificacoes.Add(classificacoes);
            }

            campeonato.ListaDeClassificacoes = ListaDeClassificacoes;

            if (ModelState.IsValid)
            {
               // campeonato.escalao = await _context.Escalao.FindAsync(int.Parse(Request.Form["Escalao"]));

                _context.Add(campeonato);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(campeonato);
        }
        [Authorize(Roles = "Admin")]
        // GET: Campeonatoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var items = _context.Escalao.ToList();
            ViewBag.data = items;

            //var campeonato = await _context.Campeonato.FirstOrDefaultAsync(i => i.Id == id);

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
        [Authorize(Roles = "Admin")]
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
                ////Verificar se os dados da dropdown foram alterados
                ////caso não tenham sido alterados a view retorna o nome do escalão
                ////caso tenha sido alterado a view retorna o id do escalao em formato string ex. "1"
                int number;
                if (Int32.TryParse(Request.Form["Escalao"], out number))
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