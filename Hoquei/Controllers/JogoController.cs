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
            ViewBag.ListaDeClubes = _context.Clube.OrderBy(c => c.Id).ToList();
            ViewBag.ListaDeJogadores = _context.Jogador.OrderBy(c => c.Num_Fed).ToList();
            ViewBag.ListaDeMarcadores = _context.ListaDeJogadores.OrderBy(c => c.Num_Fed).ToList();
            return View();
        }

        // POST: Jogo/Adicionar
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Adicionar([Bind("JogoId,Local,Data,Clube_Casa,Clube_Fora,Escalao,GolosCasa, GolosFora, Capitao_Casa, Capitao_Fora")] Jogo jogo, DateTime Date, int GolosCasa, int GolosFora, int Clube_CasaEscolhido, int Clube_ForaEscolhido, int Capitao_CasaEscolhido, int Capitao_ForaEscolhido, int[] Marcadores)
        {

            //avalia se o array com a lista de clubes escolhidos está vazio ou não
            if (Clube_CasaEscolhido == 0)
            {
                //É gerada uma mensagem de erro
                ModelState.AddModelError("", "É necessário selecionar um clube.");
                // gerar as listas
                ViewBag.ListaDeClubes = _context.Clube.OrderBy(c => c.Id).ToList();
                ViewBag.ListaDeJogadores = _context.Jogador.OrderBy(c => c.Num_Fed).ToList();
                ViewBag.ListaDeMarcadores = _context.ListaDeJogadores.OrderBy(c => c.Num_Fed).ToList();

                // devolver controlo à View
                return View(jogo);
            }

            Clube clube_casaEscolhido = _context.Clube.Find(Clube_CasaEscolhido);
            jogo.Clube_Casa = clube_casaEscolhido;


            // avalia se o array com a lista de clubes escolhidos está vazio ou não
            if (Clube_ForaEscolhido == 0)
            {
                //É gerada uma mensagem de erro
                ModelState.AddModelError("", "É necessário selecionar um clube.");
                // gerar as listas
                ViewBag.ListaDeClubes = _context.Clube.OrderBy(c => c.Id).ToList();
                ViewBag.ListaDeJogadores = _context.Jogador.OrderBy(c => c.Num_Fed).ToList();
                ViewBag.ListaDeMarcadores = _context.ListaDeJogadores.OrderBy(c => c.Num_Fed).ToList();
                // devolver controlo à View
                return View(jogo);
            }

            Clube clube_foraEscolhido = _context.Clube.Find(Clube_ForaEscolhido);
            jogo.Clube_Fora = clube_foraEscolhido;

            // avalia se o array com a lista de jogadores escolhidos está vazio ou não
            if (Capitao_CasaEscolhido == 0)
            {
                //É gerada uma mensagem de erro
                ModelState.AddModelError("", "É necessário selecionar o Capitão da Equipa da casa.");
                // gerar as listas
                ViewBag.ListaDeClubes = _context.Clube.OrderBy(c => c.Id).ToList();
                ViewBag.ListaDeJogadores = _context.Jogador.OrderBy(c => c.Num_Fed).ToList();
                ViewBag.ListaDeMarcadores = _context.ListaDeJogadores.OrderBy(c => c.Num_Fed).ToList();

                // devolver controlo à View
                return View(jogo);
            }

            Jogador capitao_casaEscolhido = _context.Jogador.Find(Capitao_CasaEscolhido);
            jogo.Capitao_Casa = capitao_casaEscolhido;

            // avalia se o array com a lista de jogadores escolhidos está vazio ou não
            if (Capitao_ForaEscolhido == 0)
            {
                //É gerada uma mensagem de erro
                ModelState.AddModelError("", "É necessário selecionar o Capitão da Equipa da casa.");
                // gerar a lista clubes
                ViewBag.ListaDeClubes = _context.Clube.OrderBy(c => c.Id).ToList();
                ViewBag.ListaDeJogadores = _context.Jogador.OrderBy(c => c.Num_Fed).ToList();
                ViewBag.ListaDeMarcadores = _context.ListaDeJogadores.OrderBy(c => c.Num_Fed).ToList();
                // devolver controlo à View
                return View(jogo);
            }

            Jogador capitao_foraEscolhido = _context.Jogador.Find(Capitao_ForaEscolhido);
            jogo.Capitao_Fora = capitao_foraEscolhido;

            // avalia se o array com a lista de marcas escolhidas associadas ao carro está vazio ou não
            if (Marcadores.Length == 0)
            {
                //É gerada uma mensagem de erro
                ModelState.AddModelError("", "É necessário selecionar pelo menos um marcador.");
                // gerar as listas
                ViewBag.ListaDeClubes = _context.Clube.OrderBy(c => c.Id).ToList();
                ViewBag.ListaDeJogadores = _context.Jogador.OrderBy(c => c.Num_Fed).ToList();
                ViewBag.ListaDeMarcadores = _context.ListaDeJogadores.OrderBy(c => c.Num_Fed).ToList();
                // devolver controlo à View
                return View(jogo);
            }

            // criar uma lista com os objetos escolhidos dos jogadores
            List<Jogador> listaDeMarcadoresEscolhidos = new List<Jogador>();
            // Para cada objeto escolhido..
            foreach (int item in Marcadores)
            {
                //procurar o jogador
                Jogador marcador = _context.ListaDeJogadores.Find(item);
                // adicionar o jogador à lista
                listaDeMarcadoresEscolhidos.Add(marcador);
            }

            // adicionar a lista ao objeto de jogo
            jogo.ListaDeMarcadores = listaDeMarcadoresEscolhidos;

            jogo.Data = Date;
            jogo.GolosCasa = GolosCasa;
            jogo.GolosFora = GolosFora;

            //if (ModelState.IsValid)
            //{
                _context.Add(jogo);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            //}
            //ViewBag.ListaDeClubes = _context.Clube.OrderBy(c => c.Id).ToList();
            //ViewBag.ListaDeJogadores = _context.Jogador.OrderBy(c => c.Num_Fed).ToList();
            //ViewBag.ListaDeMarcadores = _context.ListaDeJogadores.OrderBy(c => c.Num_Fed).ToList();
            //return View(jogo);
            

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
            ViewBag.ListaDeClubes = _context.Clube.OrderBy(c => c.Id).ToList();
            ViewBag.ListaDeJogadores = _context.Jogador.OrderBy(c => c.Num_Fed).ToList();
            ViewBag.ListaDeMarcadores = _context.ListaDeJogadores.OrderBy(c => c.Num_Fed).ToList();

            return View(jogo);
        }
    }
}
