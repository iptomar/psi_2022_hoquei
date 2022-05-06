using Hoquei.Data;
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
        public async Task<IActionResult> Adicionar([Bind("JogoId,Local,Data,Clube_Casa,Clube_Fora,Foto")] Jogador jogador, IFormFile imgFile, DateTime bornDate, int numeroCamisola)
        {

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
    }
}
