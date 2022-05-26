﻿using Hoquei.Data;
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
        public IActionResult Adicionar()
        {
            //ViewBag.ListaDeJogadores = _context.ListaDeJogadores.OrderBy(c => c.Name).ToList();
            return View();
        }

        // POST: Jogadores/Adicionar
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Adicionar([Bind("Num_Fed,Name,Num_Cam,Data_Nasc,Alcunha,Foto")] Jogador jogador, IFormFile imgFile, DateTime bornDate, int numeroCamisola)
        {
            string nomeImg = "";
            bool flagErro = false;

            if (ModelState.IsValid) { 
                //jogador.Foto = imgFile.;
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
                .FirstOrDefaultAsync(m => m.Num_Fed == id);
            if (jogador == null)
            {
                return NotFound();
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

            //var jogadores = await _context.Jogador.FindAsync(id);

            //adicionar ao jogador a foto dele
            var jogadores = await _context.Jogador.Include(j => j.Foto).Where(j => j.Num_Fed == id).FirstOrDefaultAsync();

            if (jogadores == null)
            {
                return View("Index");
            }
            return View(jogadores);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Num_Fed,Name,Num_Cam,Data_Nasc,Alcunha,Foto")] Jogador novoJogador, IFormFile imgFile, DateTime bornDate, int numeroCamisola)
        {
            string nomeImg = "";
            bool flagErro = false;

            if (ModelState.IsValid)
            {
            
                var jogador = await _context.Jogador.Include(j => j.Foto).Where(j => j.Num_Fed == id).FirstOrDefaultAsync();

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
    

