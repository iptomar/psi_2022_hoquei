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

        private readonly UserManager<ApplicationUser> _userManager;

        public JogadorController(HoqueiDB context, IWebHostEnvironment caminho, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _caminho = caminho;
            _userManager = userManager;
        }
        public async Task<IActionResult> IndexAsync()
        {
            return View(await _context.Jogador.ToListAsync());
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
                    
                }
                _context.Add(jogador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

                 
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

            var jogadores = await _context.Jogador.FindAsync(id);
            if (jogadores == null)
            {
                return NotFound();
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

            var jogador = await _context.Jogador.FindAsync(id);

            if (imgFile != null)
            {
                //novoJogador.Foto = imgFile.FileName; change this!!!!

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
    

