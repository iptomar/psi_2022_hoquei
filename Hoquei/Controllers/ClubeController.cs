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
        public async Task<IActionResult> IndexAsync()
        {
            var aux = _context.Clube.Include(j => j.Foto);
            return View(await aux.ToListAsync());
        }
        [Authorize(Roles = "Admin")]
        // GET: Clube/Adicionar
        public IActionResult Adicionar()
        {
            //ViewBag.Clube = _context.Clube.OrderBy(c => c.Name).ToList();
            ViewBag.ListaDeJogadores = _context.Jogador.OrderBy(c => c.Name).ToList();
            return View();
        }

        // POST: Clube/Adicionar
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Adicionar([Bind("Id,Name,Data_Fundacao,Foto")] Clube clube, IFormFile imgFile, DateTime bornDate)
        {
            string nomeImg = "";
            bool flagErro = false;

            if (ModelState.IsValid)
            {
                clube.Data_Fundacao = bornDate;

                var imgext = Path.GetExtension(imgFile.FileName);

                if (imgext == ".jpg" || imgext == ".png" || imgext == ".jpeg")
                {
                    Fotos foto = new Fotos();
                    Guid g;
                    g = Guid.NewGuid();

                    nomeImg = clube.Id + "" + g.ToString();

                    //determinar a extensão do nome da imagem
                    string extensao = Path.GetExtension(imgFile.FileName).ToLower();

                    // agora, consigo ter o nome final do ficheiro
                    nomeImg = nomeImg + extensao;
                    foto.Nome = nomeImg;

                    // associar este ficheiro aos dados da Fotografia do clube
                    clube.Foto = foto;

                    string localizacaoFicheiro = _caminho.WebRootPath;
                    nomeImg = Path.Combine(localizacaoFicheiro, "fotos", nomeImg);
                }
                else {
                    //se foram adicionados ficheiros inválidos
                    //adicionar msg de erro
                    ModelState.AddModelError("", "Os ficheiros adicionados não são válidos");
                    flagErro = true;
                }
                if (!flagErro) {
                    //processo de guardar foto do disco
                    using var fileFoto = new FileStream(nomeImg, FileMode.Create);
                    await imgFile.CopyToAsync(fileFoto);

                    _context.Add(clube);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
            }
            return View(clube);

        }

        // GET: Clube/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clube = await _context.Clube.Where(f => f.Id == id)
                                            .OrderByDescending(f => f.Name)
                                            .Include(fc => fc.ListaDeJogadores)
                                            .Include(ft => ft.Foto)
                                            .FirstOrDefaultAsync(m => m.Id == id);
                
            if (clube == null)
            {
                return NotFound();
            }

            ViewBag.ListaDeJogadores = _context.Jogador.OrderBy(c => c.Name).ToList();

            return View(clube);
        }

        // GET: Clubes/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clubes = await _context.Clube.Include(l => l.ListaDeJogadores).Include(c => c.Foto)
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
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Data_Fundacao,Foto")] Clube novoClube, IFormFile imgFile, DateTime bornDate, int[] jogadorEscolhido)
        {
            string nomeImg = "";
            bool flagErro = false;

            if (ModelState.IsValid) { 

                var clube = await _context.Clube.Where(l => l.Id == id)
                                                .Include(l => l.ListaDeJogadores)
                                                .Include(f => f.Foto)
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




                /****************************************************************/

                //significa que adicionámos uma foto nova
                if (imgFile != null)
                {
                    //apagamos a foto antiga da base de dados
                    var fotoAntiga = clube.Foto;
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

                        nomeImg = clube.Id + "" + g.ToString();


                        //determinar a extensão do nome da imagem
                        string extensao = Path.GetExtension(imgFile.FileName).ToLower();

                        // agora, consigo ter o nome final do ficheiro
                        nomeImg = nomeImg + extensao;
                        foto.Nome = nomeImg;

                        // associar este ficheiro aos dados da Fotografia do clube
                        clube.Foto = foto;

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
                        clube.Name = novoClube.Name;
                        clube.Data_Fundacao = novoClube.Data_Fundacao;

                        try
                        {
                            //processo de guardar foto do disco
                            using var fileFoto = new FileStream(nomeImg, FileMode.Create);
                            await imgFile.CopyToAsync(fileFoto);

                            _context.Update(clube);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));
                        }
                        catch (Exception ex)
                        {
                            ModelState.AddModelError("", ex.GetBaseException().ToString());
                        }
                    }

                }
                else  //significa que não alterámos a foto
                {
                    try
                    {
                        clube.Name = novoClube.Name;
                        clube.Data_Fundacao = bornDate;
                        novoClube.Foto = clube.Foto;

                        _context.Update(clube);

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
            return View(novoClube);
        }

        // GET: ClubController/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clubes = await _context.Clube
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clubes == null)
            {
                return NotFound();
            }

            return View(clubes);
        }

        // POST: ClubController/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clubes = await _context.Clube.FindAsync(id);
            _context.Clube.Remove(clubes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool CLubeExists(int id)
        {
            return _context.Clube.Any(e => e.Id == id);
        }
    }
}
    

