using Hoquei.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hoquei.Data;
using Microsoft.AspNetCore.Identity;

namespace Hoquei.Controllers
{
    public class UserController : Controller
    {

        /// <summary>
        /// referência à base de dados
        /// </summary>
        private readonly HoqueiDB _context;

        private readonly UserManager<ApplicationUser> _userManager;
        public UserController(HoqueiDB context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,UserName,NumTele,CC,DataNascimento,UserNameId,Email")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                User emailUser = await _context.User.Where(e => e.Email == user.Email).FirstOrDefaultAsync();
                
                if (emailUser != null /*&& aux != id*/)

                {
                    ModelState.AddModelError("", "Email já em uso");
                    return View();
                }

                User nameUser = await _context.User.Where(e => e.UserName == user.UserName).FirstOrDefaultAsync();
                
                if (nameUser != null /*&& aux1 != id*/)
                {
                    ModelState.AddModelError("", "Username já em uso");
                    return View();
                }

                //vamos realizar as alterações também nas tabelas da autenticação
                ApplicationUser identidade = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == user.UserNameId); ;
                
                if (identidade == null)
                {
                    ModelState.AddModelError("", "Ocorreu um erro..");
                    return View();
                }
                //verificar a idade do utilizador
                if (user.DataNascimento.CompareTo(DateTime.Now.AddYears(-18)) > 0)
                {
                    ModelState.AddModelError("", "Para entrar no site é necessário ser maior de 18 anos");
                    return View(user);
                }

                identidade.Email = user.Email;
                identidade.UserName = user.UserName;
                identidade.NormalizedEmail = identidade.Email.ToUpper();
                identidade.NormalizedUserName = identidade.UserName.ToUpper();
             
                try
                {
                    //preparar as alterações 
                    _context.Users.Update(identidade);
                    _context.User.Update(user);
                    //fazer commit das alterações na bd
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
            var userARemover = await _context.Users.FirstOrDefaultAsync(u => u.UserName == utilizadorARemover.UserName);
            
            _context.User.Remove(utilizadorARemover);
            _context.Users.Remove(userARemover);
            

           

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}
