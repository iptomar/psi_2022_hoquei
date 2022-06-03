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
    public class ClubeController : Controller
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

        public ClubeController(HoqueiDB context, IWebHostEnvironment caminho, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _caminho = caminho;
            _userManager = userManager;
        }


        // GET: Clube/index
        public async Task<IActionResult> IndexAsync()
        {
            return View(await _context.ListaDeClubes.ToListAsync());
        }

        // GET: Clubes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clube = await _context.ListaDeClubes.Where(f => f.Id == id)
                                                    .OrderByDescending(f => f.Nome)
                                                    .Include(fc => fc.ListaDeJogadores)
                                                    .FirstOrDefaultAsync(m => m.Id == id);
                
                //.Include(f => f.ListaDeJogadores)
                //.FirstOrDefaultAsync(m => m.Id == id);


            if (clube == null)
            {
                return NotFound();
            }

            ViewBag.ListaDeJogadores = _context.Jogador.OrderBy(c => c.Name).ToList();

            return View(clube);
        }

        // GET: Clubes/Create
        public IActionResult Create()
        {
            ViewBag.ListaDeJogadores = _context.Jogador.OrderBy(c => c.Name).ToList();
            return View();
        }

        // POST: Clubes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Nome,Data_Fundacao,FotografiasID")] Clube clube, IFormFile imgFile, DateTime bornDate)
        {

            clube.FotografiasID = imgFile.FileName;
            clube.Data_Fundacao = bornDate;

            //_webhost.WebRootPath vai ter o path para a pasta wwwroot
            var saveimg = Path.Combine(_caminho.WebRootPath, "clubefotos", imgFile.FileName);

            var imgext = Path.GetExtension(imgFile.FileName);

            if (imgext == ".jpg" || imgext == ".png" || imgext == ".JPG" || imgext == ".PNG")
            {
                using var uploadimg = new FileStream(saveimg, FileMode.Create);
                await imgFile.CopyToAsync(uploadimg);
            }

            if (ModelState.IsValid)
            {
                _context.Add(clube);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return View(clube);

        }

        // GET: Clubes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clubes = await _context.ListaDeClubes.Include(l => l.ListaDeJogadores)
                                                     .FirstOrDefaultAsync(m => m.Id == id);
                                                    
            if (clubes == null)
            {
                return NotFound();
            }

            ViewBag.ListaDeJogadores = _context.Jogador.OrderBy(c => c.Name).ToList();

            return View(clubes);
        }

        // POST: Clubes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Data_Fundacao,Jogador,FotografiasID")] Clube novoClube,
            IFormFile imgFile, DateTime bornDate, int[] jogadorEscolhido)
        {

            var clube = await _context.ListaDeClubes
                                                .Where(l => l.Id == id)
                                                    .Include(l => l.ListaDeJogadores)
                                                    .FirstOrDefaultAsync();


            // obter a lista dos IDs das Clubes associadas ao jogador, antes da edição
            var oldListaJogadores = clube.ListaDeJogadores
                                           .Select(c => c.Num_Fed)
                                           .ToList();

            // avaliar se o utilizador alterou alguma categoria associada ao componente
            // adicionadas -> lista de categorias adicionadas
            // retiradas   -> lista de categorias retiradas
            var adicionadas = jogadorEscolhido.Except(oldListaJogadores);
            var retiradas = oldListaJogadores.Except(jogadorEscolhido.ToList());

            // se alguma Category foi adicionada ou retirada
            // é necessário alterar a lista de categorias 
            // associada à Lesson
            if (adicionadas.Any() || retiradas.Any())
            {

                if (retiradas.Any())
                {
                    // retirar o CLube 
                    foreach (int oldClube in retiradas)
                    {
                        var clubToRemove = clube.ListaDeJogadores.FirstOrDefault(c => c.Num_Fed == oldClube);
                        clube.ListaDeJogadores.Remove(clubToRemove);
                    }
                }
                if (adicionadas.Any())
                {
                    // adicionar o CLube 
                    foreach (int newClub in adicionadas)
                    {
                        var clubToAdd = await _context.Jogador.FirstOrDefaultAsync(c => c.Num_Fed == newClub);
                        clube.ListaDeJogadores.Add(clubToAdd);
                    }
                }
            }

            //// avalia se o array com a lista de categorias escolhidas associadas ao Componente está vazio ou não
            //if (jogadorEscolhido.Length == 0)
            //{
            //    //É gerada uma mensagem de erro
            //    ModelState.AddModelError("", "É necessário selecionar pelo menos um clube.");
            //    // gerar a lista clubes que podem ser associadas ao jogador
            //    ViewBag.ListaDeJogadores = _context.Jogador.OrderBy(c => c.Num_Fed).ToList();
            //    // devolver controlo à View
            //    return View(novoClube);
            //}

            //// avalia se o array com a lista de categorias escolhidas associadas ao Componente está vazio ou não
            //if (jogadorEscolhido.Length < 1)
            //{
            //    //É gerada uma mensagem de erro
            //    ModelState.AddModelError("", "Selecione apenas um clube.");
            //    // gerar a lista Clubes que podem ser associadas ao Jogador
            //    ViewBag.ListaDeJogadores = _context.Jogador.OrderBy(c => c.Num_Fed).ToList();
            //    // devolver controlo à View
            //    return View(novoClube);
            //}

            // criar uma lista com os objetos escolhidos das Clubes
            List<Jogador> listaDeClubesEscolhidas = new List<Jogador>();
            // Para cada objeto escolhido..
            foreach (int item in jogadorEscolhido)
            {
                //procurar a categoria
                Jogador jogador = _context.Jogador.Find(item);
                // adicionar a Categoria à lista
                listaDeClubesEscolhidas.Add(jogador);
            }

            // adicionar a lista ao objeto de "Componente"
            novoClube.ListaDeJogadores = listaDeClubesEscolhidas;

            /*****************************************************/
            if (imgFile != null)
            {
                novoClube.FotografiasID = imgFile.FileName;

                //_webhost.WebRootPath vai ter o path para a pasta wwwroot
                var saveimg = Path.Combine(_caminho.WebRootPath, "clubefotos", imgFile.FileName);

                var imgext = Path.GetExtension(imgFile.FileName);

                if (imgext == ".jpg" || imgext == ".png" || imgext == ".JPG" || imgext == ".PNG")
                {
                    using var uploadimg = new FileStream(saveimg, FileMode.Create);
                    await imgFile.CopyToAsync(uploadimg);
                }
            }
            else
            {
                Clube clube1 = _context.ListaDeClubes.Find(novoClube.Id);

                _context.Entry<Clube>(clube1).State = EntityState.Detached;


                novoClube.FotografiasID = clube1.FotografiasID;
            }

            /***************************************************/
            if (ModelState.IsValid)
            {
                try
                {

                    clube.Nome = novoClube.Nome;
                    clube.Data_Fundacao = bornDate;
                    clube.FotografiasID = novoClube.FotografiasID;

                    _context.Update(clube);
                   
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CLubeExists(clube.Id))
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
            return View(novoClube);
        }

        // GET: ClubController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clubes = await _context.ListaDeClubes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clubes == null)
            {
                return NotFound();
            }

            return View(clubes);
        }

        // POST: ClubController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clubes = await _context.ListaDeClubes.FindAsync(id);
            _context.ListaDeClubes.Remove(clubes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CLubeExists(int id)
        {
            return _context.ListaDeClubes.Any(e => e.Id == id);
        }


    }
}
    

