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
    public class JogadorController : Controller
    {
        /// <summary>
        /// atributo que representa a base de dados do projeto
        /// </summary>
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// atributo que contém os dados da app web no servidor
        /// </summary>
        private readonly IWebHostEnvironment _caminho;

        public JogadorController(ApplicationDbContext context, IWebHostEnvironment caminho)
        {
            _context = context;
            _caminho = caminho;
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
        public async Task<IActionResult> Adicionar([Bind("Num_Fed,Name,Num_Cam,Data_Nasc,Alcunha")] Jogador jogador)
        {

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
