using Hoquei.Data;
using Hoquei.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoquei.Controllers
{
    public class JogoController : Controller
    {
        /// <summary>
        /// atributo que representa a base de dados do projeto
        /// </summary>
        private readonly HoqueiDB _context;

        /// <summary>
        /// atributo que contém os dados da app web no servidor
        /// </summary>
        private readonly IWebHostEnvironment _caminho;

        private readonly UserManager<ApplicationUser> _userManager;

        public JogoController(HoqueiDB context, IWebHostEnvironment caminho, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _caminho = caminho;
            _userManager = userManager;
        }

        public async Task<IActionResult> IndexAsync()
        {
            return View(await _context.Jogo.ToListAsync());
        }

        // GET: Jogo/Adicionar
        public IActionResult Adicionar()
        {
            //ViewBag.ListaDeJogos = _context.ListaDeJogos.OrderBy(c => c.JogoId).ToList();
            return View();
        }

        // POST: Jogo/Adicionar
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Adicionar([Bind("JogoId,Local,Data,Clube_Casa,Clube_Fora,Escalao,GolosCasa, GolosFora, Capitao_Casa, Capitao_Fora")] Jogo jogo, DateTime Date, int GolosCasa, int GolosFora, Clube Clube_Casa, Clube Clube_Fora, Jogador Capitao_Casa, Jogador Capitao_Fora, Jogador Marcadores)
        {

            jogo.Data = Date;
            jogo.GolosCasa = GolosCasa;
            jogo.GolosFora = GolosFora;
            jogo.Capitao_Casa = Capitao_Casa;
            jogo.Capitao_Fora = Capitao_Fora;
            jogo.Clube_Casa = Clube_Casa;
            jogo.Clube_Fora = Clube_Fora;
            jogo.ListaDeMarcadores.Add(Marcadores);

            if (ModelState.IsValid)
            {
                _context.Add(jogo);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }
            return View(jogo);

        }

        // GET: Jogador/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jogo = await _context.Jogo
                .Where(f => f.JogoId == id)
                .OrderByDescending(f => f.Data)
                .FirstOrDefaultAsync(m => m.JogoId == id);
            if (jogo == null)
            {
                return NotFound();
            }

            return View(jogo);
        }
    }
}
