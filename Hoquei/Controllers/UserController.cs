using Hoquei.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hoquei.Data;

namespace Hoquei.Controllers
{
    public class UserController : Controller
    {

        /// <summary>
        /// referência à base de dados
        /// </summary>
        private readonly HoqueiDB _context;

        public UserController(HoqueiDB context)
        {
            _context = context;
        }

        // GET: UserController
        public async Task<ActionResult> Index()
        {
            return View(await _context.User.ToListAsync());
        }

        // GET: UserController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        //// GET: UserController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: UserController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
        // -------------------------------------------------------------------------------------------------------
        // GET: User/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.User.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,UserName,Email,NumTele,CC,DataNascimento")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            return View(user);
        }

        // -------------------------------------------------------------------------------------------------------
        /*---------------------------------------------------*/

        // GET: UserController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var utilizadores = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);

            await DeleteConfirmed(utilizadores.Id);
            if (utilizadores == null)
            {
                return NotFound();
            }

            //return View(utilizadores);
            return RedirectToAction(nameof(Index));
        }

        // POST: UserController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int? id)
        {
            var utilizadorARemover = await _context.User.FindAsync(id);
            _context.User.Remove(utilizadorARemover);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}
