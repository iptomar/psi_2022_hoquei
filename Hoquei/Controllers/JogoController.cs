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

            ViewBag.ListaDeClubes = _context.Clube.OrderBy(c => c.Id).ToList();
            return View(await _context.Jogo.ToListAsync());
        }

        // GET: Jogo/Adicionar
        public IActionResult Adicionar()
        {
            ViewBag.ListaDeClubes = _context.Clube.OrderBy(c => c.Id).ToList();
            ViewBag.ListaDeJogadores = _context.Jogador.OrderBy(c => c.Num_Fed).ToList();
            ViewBag.ListaDeMarcadoresCasa = _context.ListaDeJogadores.OrderBy(c => c.Num_Fed).ToList();
            ViewBag.ListaDeMarcadoresFora = _context.ListaDeJogadores.OrderBy(c => c.Num_Fed).ToList();
            ViewBag.ListaDeEscaloes = _context.Escalao.OrderBy(c => c.Id).ToList();
            return View();
        }

        // POST: Jogo/Adicionar
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Adicionar([Bind("JogoId,Local,Data,Clube_Casa,Clube_Fora,Escalao,GolosCasa, GolosFora, Capitao_Casa, Capitao_Fora")] Jogo jogo, DateTime Date, int GolosCasa, int GolosFora, string Escalao,/*int Escalao_Escolhido,*/ int Clube_CasaEscolhido, int Clube_ForaEscolhido, int Capitao_CasaEscolhido, int Capitao_ForaEscolhido, int[] MarcadoresCasa, int[] MarcadoresFora)
        {

            ////avalia se o array com a lista de clubes escolhidos está vazio ou não
            //if (Escalao_Escolhido == 0)
            //{
            //    //É gerada uma mensagem de erro
            //    ModelState.AddModelError("", "É necessário selecionar um escalao.");
            //    // gerar as listas
            //    ViewBag.ListaDeClubes = _context.Clube.OrderBy(c => c.Id).ToList();
            //    ViewBag.ListaDeJogadores = _context.Jogador.OrderBy(c => c.Num_Fed).ToList();
            //    ViewBag.ListaDeMarcadoresCasa = _context.ListaDeJogadores.OrderBy(c => c.Num_Fed).ToList();
            //    ViewBag.ListaDeEscaloes = _context.Escalao.OrderBy(c => c.Id).ToList();

            //    // devolver controlo à View
            //    return View(jogo);
            //}

            //Escalao escalao = _context.Escalao.Find(Escalao_Escolhido);
            //jogo.Escalao = escalao;

            //avalia se o array com a lista de clubes escolhidos está vazio ou não
            if (Clube_CasaEscolhido == 0)
            {
                //É gerada uma mensagem de erro
                ModelState.AddModelError("", "É necessário selecionar um clube.");
                // gerar as listas
                ViewBag.ListaDeClubes = _context.Clube.OrderBy(c => c.Id).ToList();
                ViewBag.ListaDeJogadores = _context.Jogador.OrderBy(c => c.Num_Fed).ToList();
                ViewBag.ListaDeMarcadoresCasa = _context.ListaDeJogadores.OrderBy(c => c.Num_Fed).ToList();
                ViewBag.ListaDeMarcadoresFora = _context.ListaDeJogadores.OrderBy(c => c.Num_Fed).ToList();
                ViewBag.ListaDeEscaloes = _context.Escalao.OrderBy(c => c.Id).ToList();

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
                ViewBag.ListaDeMarcadoresCasa = _context.ListaDeJogadores.OrderBy(c => c.Num_Fed).ToList();
                ViewBag.ListaDeMarcadoresFora = _context.ListaDeJogadores.OrderBy(c => c.Num_Fed).ToList();
                ViewBag.ListaDeEscaloes = _context.Escalao.OrderBy(c => c.Id).ToList();
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
                ViewBag.ListaDeMarcadoresCasa = _context.ListaDeJogadores.OrderBy(c => c.Num_Fed).ToList();
                ViewBag.ListaDeMarcadoresFora = _context.ListaDeJogadores.OrderBy(c => c.Num_Fed).ToList();
                ViewBag.ListaDeEscaloes = _context.Escalao.OrderBy(c => c.Id).ToList();

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
                ViewBag.ListaDeMarcadoresCasa = _context.ListaDeJogadores.OrderBy(c => c.Num_Fed).ToList();
                ViewBag.ListaDeMarcadoresFora = _context.ListaDeJogadores.OrderBy(c => c.Num_Fed).ToList();
                ViewBag.ListaDeEscaloes = _context.Escalao.OrderBy(c => c.Id).ToList();
                // devolver controlo à View
                return View(jogo);
            }

            Jogador capitao_foraEscolhido = _context.Jogador.Find(Capitao_ForaEscolhido);
            jogo.Capitao_Fora = capitao_foraEscolhido;

            //// avalia se o array com a lista de marcas escolhidas associadas ao carro está vazio ou não
            //if (MarcadoresCasa.Length == 0)
            //{
            //    //É gerada uma mensagem de erro
            //    ModelState.AddModelError("", "É necessário selecionar pelo menos um marcadorcasa.");
            //    // gerar as listas
            //    ViewBag.ListaDeClubes = _context.Clube.OrderBy(c => c.Id).ToList();
            //    ViewBag.ListaDeJogadores = _context.Jogador.OrderBy(c => c.Num_Fed).ToList();
            //    ViewBag.ListaDeMarcadoresCasa = _context.ListaDeJogadores.OrderBy(c => c.Num_Fed).ToList();
            //    ViewBag.ListaDeMarcadoresFora = _context.ListaDeJogadores.OrderBy(c => c.Num_Fed).ToList();
            //    ViewBag.ListaDeEscaloes = _context.Escalao.OrderBy(c => c.Id).ToList();
            //    // devolver controlo à View
            //    return View(jogo);
            //}

            // criar uma lista com os objetos escolhidos dos jogadores
            List<Jogador> listaDeMarcadoresCasaEscolhidos = new List<Jogador>();
            
            // Para cada objeto escolhido..
            foreach (int item in MarcadoresCasa)
            {
                //procurar o jogador
                Jogador marcadorcasa = _context.ListaDeJogadores.Find(item);
                // adicionar o jogador à lista
                listaDeMarcadoresCasaEscolhidos.Add(marcadorcasa);
                
            }

            List<Jogador> listaDeMarcadoresForaEscolhidos = new List<Jogador>();
            // Para cada objeto escolhido..
            foreach (int item in MarcadoresFora)
            {
                //procurar o jogador
                Jogador marcadorfora = _context.ListaDeJogadores.Find(item);
                // adicionar o jogador à lista
                listaDeMarcadoresForaEscolhidos.Add(marcadorfora);
            }

            // adicionar a lista ao objeto de jogo
            jogo.ListaDeMarcadoresCasa = listaDeMarcadoresCasaEscolhidos;
            jogo.ListaDeMarcadoresFora = listaDeMarcadoresForaEscolhidos;
            jogo.Escalao = Escalao;
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
            //ViewBag.ListaDeMarcadoresCasa = _context.ListaDeJogadores.OrderBy(c => c.Num_Fed).ToList();
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
            ViewBag.ListaDeMarcadoresCasa = _context.ListaDeJogadores.OrderBy(c => c.Num_Fed).ToList();
            ViewBag.ListaDeMarcadoresFora = _context.ListaDeJogadores.OrderBy(c => c.Num_Fed).ToList();

            return View(jogo);
        }

        //// GET: Jogo/Edit
        //public IActionResult Edit()
        //{
        //    ViewBag.ListaDeClubes = _context.Clube.OrderBy(c => c.Id).ToList();
        //    ViewBag.ListaDeJogadores = _context.Jogador.OrderBy(c => c.Num_Fed).ToList();
        //    ViewBag.ListaDeMarcadoresCasa = _context.ListaDeJogadores.OrderBy(c => c.Num_Fed).ToList();
        //    ViewBag.ListaDeMarcadoresFora = _context.ListaDeJogadores.OrderBy(c => c.Num_Fed).ToList();
        //    return View();
        //}

        // GET: Jogo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // var jogadores = await _context.Jogador
            //                                        .Include(l => l.ListaDeClubes)
            //                                       .FirstOrDefaultAsync(m => m.Num_Fed == id);

            //var jogadores = await _context.Jogador.FindAsync(id);

            //adicionar ao jogador a foto dele
            var jogos = await _context.Jogo.Include(l => l.ListaDeMarcadoresCasa)
                                           .Include(l => l.ListaDeMarcadoresCasa)
                                           .Include(l => l.Capitao_Fora)
                                           .Include(l => l.Capitao_Casa)
                                           .FirstOrDefaultAsync(m => m.JogoId == id);



            if (jogos == null)
            {
                return View("Index");
            }
            ViewBag.ListaDeClubes = _context.Clube.OrderBy(c => c.Id).ToList();
            ViewBag.ListaDeJogadores = _context.Jogador.OrderBy(c => c.Num_Fed).ToList();
            ViewBag.ListaDeMarcadoresCasa = _context.ListaDeJogadores.OrderBy(c => c.Num_Fed).ToList();
            ViewBag.ListaDeMarcadoresFora = _context.ListaDeJogadores.OrderBy(c => c.Num_Fed).ToList();
            return View(jogos);
        }




        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JogoId, Local, Data, Clube_Casa, Clube_Fora, Escalao, GolosCasa, GolosFora, Capitao_Casa, Capitao_Fora")] Jogo jogo, DateTime Date, int GolosCasa, int GolosFora, int Clube_CasaEscolhido, int Clube_ForaEscolhido, int Capitao_CasaEscolhido, int Capitao_ForaEscolhido, int[] MarcadoresCasa, int[] MarcadoresFora)
        {

            //avalia se o array com a lista de clubes escolhidos está vazio ou não
            if (Clube_CasaEscolhido == 0)
            {
                //É gerada uma mensagem de erro
                ModelState.AddModelError("", "É necessário selecionar um clube.");
                // gerar as listas
                ViewBag.ListaDeClubes = _context.Clube.OrderBy(c => c.Id).ToList();
                ViewBag.ListaDeJogadores = _context.Jogador.OrderBy(c => c.Num_Fed).ToList();
                ViewBag.ListaDeMarcadoresCasa = _context.ListaDeJogadores.OrderBy(c => c.Num_Fed).ToList();
                ViewBag.ListaDeMarcadoresFora = _context.ListaDeJogadores.OrderBy(c => c.Num_Fed).ToList();

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
                ViewBag.ListaDeMarcadoresCasa = _context.ListaDeJogadores.OrderBy(c => c.Num_Fed).ToList();
                ViewBag.ListaDeMarcadoresFora = _context.ListaDeJogadores.OrderBy(c => c.Num_Fed).ToList();
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
                ViewBag.ListaDeMarcadoresCasa = _context.ListaDeJogadores.OrderBy(c => c.Num_Fed).ToList();
                ViewBag.ListaDeMarcadoresFora = _context.ListaDeJogadores.OrderBy(c => c.Num_Fed).ToList();

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
                ViewBag.ListaDeMarcadoresCasa = _context.ListaDeJogadores.OrderBy(c => c.Num_Fed).ToList();
                ViewBag.ListaDeMarcadoresFora = _context.ListaDeJogadores.OrderBy(c => c.Num_Fed).ToList();
                // devolver controlo à View
                return View(jogo);
            }

            Jogador capitao_foraEscolhido = _context.Jogador.Find(Capitao_ForaEscolhido);
            jogo.Capitao_Fora = capitao_foraEscolhido;

            // avalia se o array com a lista de marcas escolhidas associadas ao carro está vazio ou não
            if (MarcadoresCasa.Length == 0)
            {
                //É gerada uma mensagem de erro
                ModelState.AddModelError("", "É necessário selecionar pelo menos um marcadorcasa casa.");
                // gerar as listas
                ViewBag.ListaDeClubes = _context.Clube.OrderBy(c => c.Id).ToList();
                ViewBag.ListaDeJogadores = _context.Jogador.OrderBy(c => c.Num_Fed).ToList();
                ViewBag.ListaDeMarcadoresCasa = _context.ListaDeJogadores.OrderBy(c => c.Num_Fed).ToList();
                ViewBag.ListaDeMarcadoresFora = _context.ListaDeJogadores.OrderBy(c => c.Num_Fed).ToList();
                // devolver controlo à View
                return View(jogo);
            }

            // criar uma lista com os objetos escolhidos dos jogadores
            List<Jogador> listaDeMarcadoresCasaEscolhidos = new List<Jogador>();
            List<Jogador> listaDeMarcadoresForaEscolhidos = new List<Jogador>();
            // Para cada objeto escolhido..
            foreach (int item in MarcadoresCasa)
            {
                //procurar o jogador
                Jogador marcadorcasa = _context.ListaDeJogadores.Find(item);
                // adicionar o jogador à lista
                listaDeMarcadoresCasaEscolhidos.Add(marcadorcasa);
                
            }




            // avalia se o array com a lista de marcas escolhidas associadas ao carro está vazio ou não
            if (MarcadoresFora.Length == 0)
            {
                //É gerada uma mensagem de erro
                ModelState.AddModelError("", "É necessário selecionar pelo menos um marcadorcasa fora.");
                // gerar as listas
                ViewBag.ListaDeClubes = _context.Clube.OrderBy(c => c.Id).ToList();
                ViewBag.ListaDeJogadores = _context.Jogador.OrderBy(c => c.Num_Fed).ToList();
                ViewBag.ListaDeMarcadoresCasa = _context.ListaDeJogadores.OrderBy(c => c.Num_Fed).ToList();
                ViewBag.ListaDeMarcadoresFora = _context.ListaDeJogadores.OrderBy(c => c.Num_Fed).ToList();
                // devolver controlo à View
                return View(jogo);
            }
            // Para cada objeto escolhido..
            foreach (int item in MarcadoresFora)
            {
                //procurar o jogador
                Jogador marcador = _context.ListaDeJogadores.Find(item);
                // adicionar o jogador à lista
                listaDeMarcadoresForaEscolhidos.Add(marcador);
            }




            // adicionar a lista ao objeto de jogo
            jogo.ListaDeMarcadoresCasa = listaDeMarcadoresCasaEscolhidos;
            jogo.ListaDeMarcadoresFora = listaDeMarcadoresForaEscolhidos;

            jogo.Data = Date;
            jogo.GolosCasa = GolosCasa;
            jogo.GolosFora = GolosFora;

            var jogo1 = await _context.Jogo.FindAsync(id);

            /***************************************************/
            //if (ModelState.IsValid)
            //{
            try
            {
                jogo1.Local = jogo.Local;
                jogo1.Data = Date;
                jogo1.Clube_Casa = jogo.Clube_Casa;
                jogo1.Clube_Fora = jogo.Clube_Fora;
                jogo1.Escalao = jogo.Escalao;
                jogo1.GolosCasa = jogo.GolosCasa;
                jogo1.GolosFora = jogo.GolosFora;
                jogo1.Capitao_Casa = jogo.Capitao_Casa;
                jogo1.Capitao_Fora = jogo.Capitao_Fora;
                jogo1.ListaDeMarcadoresCasa = jogo.ListaDeMarcadoresCasa;
                jogo1.ListaDeMarcadoresFora = jogo.ListaDeMarcadoresFora;

                _context.Update(jogo1);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JogoExists(jogo1.JogoId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));
            //}
            ViewBag.ListaDeClubes = _context.Clube.OrderBy(c => c.Id).ToList();
            ViewBag.ListaDeJogadores = _context.Jogador.OrderBy(c => c.Num_Fed).ToList();
            ViewBag.ListaDeMarcadoresCasa = _context.ListaDeJogadores.OrderBy(c => c.Num_Fed).ToList();
            ViewBag.ListaDeMarcadoresFora = _context.ListaDeJogadores.OrderBy(c => c.Num_Fed).ToList();
            return View(jogo);
        }

        private bool JogoExists(int id)
        {
            return _context.Jogo.Any(e => e.JogoId == id);
        }

    }


}
