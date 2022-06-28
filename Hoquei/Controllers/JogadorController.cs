using Hoquei.Data;
using Hoquei.Models;
using Microsoft.AspNetCore.Authorization;
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

        private readonly UserManager<ApplicationUser> _userManager;

        public JogadorController(HoqueiDB context, IWebHostEnvironment caminho, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _caminho = caminho;
            _userManager = userManager;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var aux = _context.Jogador.Include(j => j.Foto);
            //return View(await _context.Jogador.ToListAsync());
            return View(await aux.ToListAsync());
        }

        // GET: Jogadores/Adicionar
        [Authorize(Roles = "Admin,Utilizador")]
        public IActionResult Adicionar()
        {
            ViewBag.ListaDeClubes = _context.Clube.OrderBy(c => c.Name).ToList();
            return View();
        }

        // POST: Jogadores/Adicionar
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,Utilizador")]
        [HttpPost]
        public async Task<IActionResult> Adicionar([Bind("Num_Fed,Name,Num_Cam,Data_Nasc,Clube,Alcunha,Foto")] Jogador jogador, IFormFile imgFile, DateTime bornDate, int numeroCamisola, int[] ClubeEscolhido)
        {
            string nomeImg = "";
            bool flagErro = false;

            if (ModelState.IsValid) { 
                //jogador.Foto = imgFile.;
                jogador.ListaDeClubes = null;
                jogador.Data_Nasc = bornDate;
                jogador.Num_Cam = numeroCamisola;

                var imgext = Path.GetExtension(imgFile.FileName);

                if (imgext == ".jpg" || imgext == ".png" || imgext == ".jpeg")
                {
                    Fotos foto = new Fotos();
                    //definir novo nome da fotografia
                    Guid g;
                    g = Guid.NewGuid();

                    nomeImg = jogador.Num_Fed + "" + g.ToString();

                    //determinar a extensão do nome da imagem
                    string extensao = Path.GetExtension(imgFile.FileName).ToLower();

                    // agora, consigo ter o nome final do ficheiro
                    nomeImg = nomeImg + extensao;
                    foto.Nome = nomeImg;

                    // associar este ficheiro aos dados da Fotografia do jogador
                    jogador.Foto = foto;

                    string localizacaoFicheiro = _caminho.WebRootPath;
                    nomeImg = Path.Combine(localizacaoFicheiro, "fotos", nomeImg);
                }
                else
                {
                    //se foram adicionados ficheiros inválidos
                    //adicionar msg de erro
                    ModelState.AddModelError("", "Os ficheiros adicionados não são válidos");
                    flagErro = true;

                }
                if (!flagErro) {
                    //processo de guardar foto do disco
                    using var fileFoto = new FileStream(nomeImg, FileMode.Create);
                    await imgFile.CopyToAsync(fileFoto);

                    _context.Add(jogador);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
    
            }
            return View(jogador);
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
                .Include(f => f.Foto)
                .FirstOrDefaultAsync(m => m.Num_Fed == id);
            if (jogador == null)
            {
                return NotFound();
            }

            ViewBag.ListaDeClubes = _context.Clube.OrderBy(c => c.Name).ToList();
            
            return View(jogador);

        }

        // GET: User/Edit/5
        [Authorize(Roles = "Admin")]
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
            var jogadores = await _context.Jogador.Include(j => j.Foto).Include(l => l.ListaDeClubes)
                                                  .Where(j => j.Num_Fed == id).FirstOrDefaultAsync();


            if (jogadores == null)
            {
                return View("Index");
            }
            ViewBag.ListaDeClubes = _context.Clube.OrderBy(c => c.Name).ToList();
            return View(jogadores);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Num_Fed,Name,Num_Cam,Data_Nasc,Clube,Alcunha,Foto")] Jogador novoJogador,
            IFormFile imgFile, DateTime bornDate, int[] ClubeEscolhido)
        {
            string nomeImg = "";
            bool flagErro = false;

            if (ModelState.IsValid)
            {
            
                var jogador = await _context.Jogador.Include(l => l.ListaDeClubes).Include(j => j.Foto).Where(j => j.Num_Fed == id).FirstOrDefaultAsync();
                
                // obter a lista dos IDs das Clubes associadas ao jogador, antes da edição
                var oldListaClubes = jogador.ListaDeClubes
                                           .Select(c => c.Id)
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
                        var clubToAdd = await _context.Clube.FirstOrDefaultAsync(c => c.Id == newClub);
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
                ViewBag.ListaDeClubes = _context.Clube.OrderBy(c => c.Id).ToList();
                // devolver controlo à View
                return View(novoJogador);
            }

            // avalia se o array com a lista de categorias escolhidas associadas ao Componente está vazio ou não
            if (ClubeEscolhido.Length < 1)
            {
                //É gerada uma mensagem de erro
                ModelState.AddModelError("", "Selecione apenas um clube.");
                // gerar a lista Clubes que podem ser associadas ao Jogador
                ViewBag.ListaDeClubes = _context.Clube.OrderBy(c => c.Id).ToList();
                // devolver controlo à View
                return View(novoJogador);
            }

            // criar uma lista com os objetos escolhidos das Clubes
            List<Clube> listaDeClubesEscolhidas = new List<Clube>();
            // Para cada objeto escolhido..
            foreach (int item in ClubeEscolhido)
            {
                //procurar a categoria
                Clube Clube = _context.Clube.Find(item);
                // adicionar a Categoria à lista
                listaDeClubesEscolhidas.Add(Clube);
            }

            // adicionar a lista ao objeto de "Componente"
            novoJogador.ListaDeClubes = listaDeClubesEscolhidas;
           

                //significa que adicionámos uma foto nova
                if (imgFile != null)
                {
                    //apagamos a foto antiga da base de dados
                    var fotoAntiga = jogador.Foto;
                    _context.Foto.Remove(fotoAntiga);

                    //apagamos a foto antiga do disco
                    var removerDisco = Path.Combine(_caminho.WebRootPath, "fotos", fotoAntiga.Nome);
                    System.IO.File.Delete(removerDisco);

                    //processar a nova imagem
                    var imgext = Path.GetExtension(imgFile.FileName);
                
                    if (imgext == ".jpg" || imgext == ".png" || imgext == ".jpeg")
                    {
                        Fotos foto = new Fotos();
                        //definir novo nome da fotografia
                        Guid g;
                        g = Guid.NewGuid();

                        nomeImg = jogador.Num_Fed + "" + g.ToString();


                        //determinar a extensão do nome da imagem
                        string extensao = Path.GetExtension(imgFile.FileName).ToLower();

                        // agora, consigo ter o nome final do ficheiro
                        nomeImg = nomeImg + extensao;
                        foto.Nome = nomeImg;

                        // associar este ficheiro aos dados da Fotografia do jogador
                        jogador.Foto = foto;

                        string localizacaoFicheiro = _caminho.WebRootPath;
                        nomeImg = Path.Combine(localizacaoFicheiro, "fotos", nomeImg);
                    }
                    else
                    {
                        //se foram adicionados ficheiros inválidos
                        //adicionar msg de erro
                        ModelState.AddModelError("", "Os ficheiros adicionados não são válidos");
                        flagErro = true;

                    }
                    if (!flagErro)
                    {
                        jogador.Name = novoJogador.Name;
                        jogador.Num_Cam = novoJogador.Num_Cam;
                        jogador.Data_Nasc = bornDate;
                        jogador.Alcunha = novoJogador.Alcunha;

                        try { 
                            //processo de guardar foto do disco
                            using var fileFoto = new FileStream(nomeImg, FileMode.Create);
                            await imgFile.CopyToAsync(fileFoto);

                            _context.Update(jogador);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));
                        }
                        catch (Exception ex)
                        {
                            ModelState.AddModelError("", ex.GetBaseException().ToString());
                        }
                    }

                }
                else //significa que não alterámos a foto
                {
                    jogador.Name = novoJogador.Name;
                    jogador.Num_Cam = novoJogador.Num_Cam;
                    jogador.Data_Nasc = bornDate;
                    jogador.Alcunha = novoJogador.Alcunha;

                    novoJogador.Foto = jogador.Foto;
                    try
                    {
                        _context.Update(jogador);

                        await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", ex.GetBaseException().ToString());
                    }
                }

                /***************************************************/

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
    

