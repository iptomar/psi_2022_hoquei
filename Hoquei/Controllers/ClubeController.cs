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
        public async Task<IActionResult> IndexAsync()
        {
            return View(await _context.Clube.ToListAsync());
        }

        // GET: Clube/Adicionar
        public IActionResult Adicionar()
        {
            //ViewBag.Clube = _context.Clube.OrderBy(c => c.Name).ToList();
            return View();
        }

        // POST: Clube/Adicionar
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Adicionar([Bind("Id,Name,Data_Fundacao,Foto")] Clube clube, IFormFile imgFile, DateTime bornDate)
        {

            clube.Foto = imgFile.FileName;
            clube.Data_Fundacao = bornDate;

            //_webhost.WebRootPath vai ter o path para a pasta wwwroot
            var saveimg = Path.Combine(_caminho.WebRootPath, "clubefotos", imgFile.FileName);

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
                _context.Add(clube);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

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

            var clube = await _context.Clube.Include(f => f.ListaDeJogadores)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clube == null)
            {
                return NotFound();
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

            var clubes = await _context.Clube.FindAsync(id);
            if (clubes == null)
            {
                return NotFound();
            }
            return View(clubes);
        }

        // POST: Clubes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Data_Fundacao,Foto")] Clube novoClube, IFormFile imgFile, DateTime bornDate)
        {

            var clube = await _context.Clube.FindAsync(id);

            if (imgFile != null)
            {
                novoClube.Foto = imgFile.FileName;

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
                Clube clube1 = _context.Clube.Find(novoClube.Id);

                _context.Entry<Clube>(clube1).State = EntityState.Detached;


                novoClube.Foto = clube1.Foto;
            }

            /***************************************************/
            if (ModelState.IsValid)
            {
                try
                {

                    clube.Name = novoClube.Name;
                    clube.Data_Fundacao = bornDate;
                    clube.Foto = novoClube.Foto;

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

            var clubes = await _context.Clube
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
    

