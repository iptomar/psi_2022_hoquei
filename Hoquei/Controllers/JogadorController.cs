using Hoquei.Data;
using Hoquei.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hoquei.Controllers
{
    public class JogadorController : Controller
    {
        /// <summary>
        /// atributo que representa a base de dados do projeto
        /// </summary>
        private readonly HoqueiDB _context;

        /// <summary>
        /// atributo que contém os dados da app web no servidor
        /// </summary>
        private readonly IWebHostEnvironment _caminho;

        /// <summary>
        /// variavel que recolhe os dados da pessoa que se autenticou
        /// </summary>
        private readonly UserManager<ApplicationUser> _userManager;

        public JogadorController(HoqueiDB context, IWebHostEnvironment caminho, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _caminho = caminho;
            _userManager = userManager;
        }

        // GET: Jogador
        public async Task<IActionResult> IndexAsync()
        {
            return View(await _context.Jogador.ToListAsync());
        }

        // GET: Jogador/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jogador = await _context.Jogador
                .Where(f => f.Num_Fed == id)
                .OrderByDescending(f => f.Num_Fed)
                .Include(fc => fc.ListaDeClubes)
                .FirstOrDefaultAsync(m => m.Num_Fed == id);

            if (jogador == null)
            {
                return NotFound();
            }

            ViewBag.ListaDeClubes = _context.ListaDeClubes.OrderBy(c => c.Nome).ToList();

            return View(jogador);
        }



        // GET: Jogadores/Adicionar
        public IActionResult Adicionar()
        {
            ViewBag.ListaDeClubes = _context.ListaDeClubes.OrderBy(c => c.Nome).ToList();
            return View();
        }

        // POST: Jogadores/Adicionar
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Adicionar([Bind("Num_Fed,Name,Num_Cam,Data_Nasc,Clube,Alcunha,Foto")] Jogador jogador, IFormFile imgFile, DateTime bornDate, int numeroCamisola, int[] ClubeEscolhido)
        {

            // avalia se o array com a lista de categorias escolhidas associadas ao componente está vazio ou não
            if (ClubeEscolhido.Length == 0)
            {
                //É gerada uma mensagem de erro
                ModelState.AddModelError("", "É necessário selecionar pelo menos um Clube.");
                // gerar a lista Categorias que podem ser associadas ao componente
                ViewBag.ListaDeClubes = _context.ListaDeClubes.OrderBy(c => c.Id).ToList();
                // devolver controlo à View
                return View(jogador);
            }

            // avalia se o array com a lista de categorias escolhidas associadas ao Componente está vazio ou não
            if (ClubeEscolhido.Length < 1)
            {
                //É gerada uma mensagem de erro
                ModelState.AddModelError("", "Selecione apenas um Clube.");
                // gerar a lista Categorias que podem ser associadas ao Componente
                ViewBag.ListaDeClubes = _context.ListaDeClubes.OrderBy(c => c.Id).ToList();
                // devolver controlo à View
                return View(jogador);
            }

            // criar uma lista com os objetos escolhidos das Categorias
            List<Clube> listaDeClubesEscolhidos = new List<Clube>();
            // Para cada objeto escolhido..
            foreach (int item in ClubeEscolhido)
            {
                //procurar a categoria
                Clube clube = _context.ListaDeClubes.Find(item);
                // adicionar a Categoria à lista
                listaDeClubesEscolhidos.Add(clube);
            }

            // adicionar a lista ao objeto de "componente"
            jogador.ListaDeClubes = listaDeClubesEscolhidos;

            jogador.Foto = imgFile.FileName;
            jogador.Data_Nasc = bornDate;
            jogador.Num_Cam = numeroCamisola;

            //_webhost.WebRootPath vai ter o path para a pasta wwwroot
            var saveimg = Path.Combine(_caminho.WebRootPath, "fotos", imgFile.FileName);

            var imgext = Path.GetExtension(imgFile.FileName);

            if (imgext == ".jpg" || imgext == ".png" || imgext == ".JPG" || imgext == ".PNG")
            {
                using (var uploadimg = new FileStream(saveimg, FileMode.Create))
                {
                    await imgFile.CopyToAsync(uploadimg);

                }
            }

            if (ModelState.IsValid)
            {
                _context.Add(jogador);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }
            return View(jogador);

        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jogadores = await _context.Jogador
                                                   .Include(l => l.ListaDeClubes)
                                                   .FirstOrDefaultAsync(m => m.Num_Fed == id);

            if (jogadores == null)
            {
                return NotFound();
            }

            ViewBag.ListaDeClubes = _context.ListaDeClubes.OrderBy(c => c.Nome).ToList();

            return View(jogadores);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        /// <summary>
        /// Edição dos dados de uma Lesson
        /// </summary>
        /// <param name="id">Id da jogador</param>
        /// <param name="novoJogador">novos dados a associar à Lesson</param>
        /// <param name="ClubeEscolhido">Lista de Clubes a que a Lesson deve estar associada</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Num_Fed,Name,Num_Cam,Data_Nasc,Clube,Alcunha,Foto")] Jogador novoJogador, 
            IFormFile imgFile, DateTime bornDate, int[] ClubeEscolhido)
        {

            // dados anteriormente guardados do jogador
            var jogador = await _context.Jogador
                                                .Where(l => l.Num_Fed == id)
                                                .Include(l => l.ListaDeClubes)
                                                .FirstOrDefaultAsync();

            // obter a lista dos IDs das Clubes associadas ao jogador, antes da edição
            var oldListaClubes = jogador.ListaDeClubes
                                           .Select(c => c. Id)
                                           .ToList();

            // avaliar se o utilizador alterou alguma categoria associada ao componente
            // adicionadas -> lista de categorias adicionadas
            // retiradas   -> lista de categorias retiradas
            var adicionadas = ClubeEscolhido.Except(oldListaClubes);
            var retiradas = oldListaClubes.Except(ClubeEscolhido.ToList());

            // se alguma Category foi adicionada ou retirada
            // é necessário alterar a lista de categorias 
            // associada à Lesson
            if (adicionadas.Any() || retiradas.Any())
            {

                if (retiradas.Any())
                {
                    // retirar a Category 
                    foreach (int oldClube in retiradas)
                    {
                        var clubToRemove = jogador.ListaDeClubes.FirstOrDefault(c => c.Id == oldClube);
                        jogador.ListaDeClubes.Remove(clubToRemove);
                    }
                }
                if (adicionadas.Any())
                {
                    // adicionar a Categoria 
                    foreach (int newClub in adicionadas)
                    {
                        var clubToAdd = await _context.ListaDeClubes.FirstOrDefaultAsync(c => c.Id == newClub);
                        jogador.ListaDeClubes.Add(clubToAdd);
                    }
                }
            }

            // avalia se o array com a lista de categorias escolhidas associadas ao Componente está vazio ou não
            if (ClubeEscolhido.Length == 0)
            {
                //É gerada uma mensagem de erro
                ModelState.AddModelError("", "É necessário selecionar pelo menos um clube.");
                // gerar a lista clubes que podem ser associadas ao jogador
                ViewBag.ListaDeClubes = _context.ListaDeClubes.OrderBy(c => c.Id).ToList();
                // devolver controlo à View
                return View(novoJogador);
            }

            // avalia se o array com a lista de categorias escolhidas associadas ao Componente está vazio ou não
            if (ClubeEscolhido.Length < 1)
            {
                //É gerada uma mensagem de erro
                ModelState.AddModelError("", "Selecione apenas um clube.");
                // gerar a lista Clubes que podem ser associadas ao Jogador
                ViewBag.ListaDeClubes = _context.ListaDeClubes.OrderBy(c => c.Id).ToList();
                // devolver controlo à View
                return View(novoJogador);
            }

            // criar uma lista com os objetos escolhidos das Clubes
            List<Clube> listaDeClubesEscolhidas = new List<Clube>();
            // Para cada objeto escolhido..
            foreach (int item in ClubeEscolhido)
            {
                //procurar a categoria
                Clube Clube = _context.ListaDeClubes.Find(item);
                // adicionar a Categoria à lista
                listaDeClubesEscolhidas.Add(Clube);
            }

            // adicionar a lista ao objeto de "Componente"
            novoJogador.ListaDeClubes = listaDeClubesEscolhidas;


            /**************************************************/
            if (imgFile != null)
            {
                novoJogador.Foto = imgFile.FileName;

                //_webhost.WebRootPath vai ter o path para a pasta wwwroot
                var saveimg = Path.Combine(_caminho.WebRootPath, "fotos", imgFile.FileName);

                var imgext = Path.GetExtension(imgFile.FileName);

                if (imgext == ".jpg" || imgext == ".png" || imgext == ".JPG" || imgext == ".PNG")
                {
                    using var uploadimg = new FileStream(saveimg, FileMode.Create);
                    await imgFile.CopyToAsync(uploadimg);
                }
            }
            else
            {
                Jogador jogador1 = _context.Jogador.Find(novoJogador.Num_Fed);

                _context.Entry<Jogador>(jogador1).State = EntityState.Detached;


                novoJogador.Foto = jogador1.Foto;
            }

            /***************************************************/
            if (ModelState.IsValid)
            {
                try
                {

                    jogador.Name = novoJogador.Name;
                    jogador.Num_Cam = novoJogador.Num_Cam;
                    jogador.Data_Nasc = bornDate;
                    jogador.Alcunha = novoJogador.Alcunha;
                    jogador.Foto = novoJogador.Foto;


 
                    _context.Update(jogador);
                   
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JogadorExists(jogador.Num_Fed))
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
            return View(novoJogador);
        }
        private bool JogadorExists(int id)
        {
            return _context.Jogador.Any(e => e.Num_Fed == id);
        }


    }
}
    

