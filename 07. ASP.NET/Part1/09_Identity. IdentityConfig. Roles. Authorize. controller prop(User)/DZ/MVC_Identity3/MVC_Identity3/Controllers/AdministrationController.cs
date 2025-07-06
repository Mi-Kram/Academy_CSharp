using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MVC_Identity3.Models;
using MVC_Identity3.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVC_Identity3.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdministrationController : Controller
    {
        // GET: Administration
        public ActionResult Index()
        {
            // Создать хранилище ролей
            var roleStore = new RoleStore<IdentityRole>(new ApplicationDbContext());

            // Создать менеджер ролей
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            // Создать контекст для работы с БД
            var dbContext = new ApplicationDbContext();

            var viewModel = new AdministrationFormViewModel
            {
                Roles = roleManager.Roles.ToList(),     // получить список ролей
                Users = dbContext.Users.ToList()        // получить список пользователей
            };

            return View(viewModel);
        }

        // GET: /Administration/New
        [HttpGet, ActionName("New")]
        public async Task<ActionResult> NewUser()
        {
            // Создание нового пользователя
            var _context = new ApplicationDbContext();
            var store = new UserStore<ApplicationUser>(_context);
            var manager = new ApplicationUserManager(store);
            var user = new ApplicationUser()
            {
                Email = "ivanchik@mail.com",
                UserName = "Ivan",
                Address = "Moskow",
                Age = 45
            };
            await manager.CreateAsync(user, "Qwerty_123");

            return RedirectToAction("Index");
        }

        // GET: /Administration/New
        [HttpGet, ActionName("NewRole")]
        public async Task<ActionResult> CreateNewRole()
        {
            // Создание новой роли
            var roleStore = new RoleStore<IdentityRole>(new ApplicationDbContext());
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            await roleManager.CreateAsync(new IdentityRole("ContentManager"));

            return RedirectToAction("Index");
        }

        // GET: /Administration/DeleteRole/1
        [HttpGet, ActionName("DeleteRole")]
        public async Task<ActionResult> DeleteRoleConfirmed(string id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                // Удаление роли
                var roleStore = new RoleStore<IdentityRole>(new ApplicationDbContext());
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var role = await roleManager.FindByIdAsync(id);
                await roleManager.DeleteAsync(role);

                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        // GET: /Administration/Delete/5
        [HttpGet, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                // Удаление пользователя
                var _context = new ApplicationDbContext();
                var store = new UserStore<ApplicationUser>(_context);
                var _userManager = new ApplicationUserManager(store);

                var user = await _userManager.FindByIdAsync(id);
                var logins = user.Logins;
                var rolesForUser = await _userManager.GetRolesAsync(id);

                // Открытие транзакции для комплексного удаления
                using (var transaction = _context.Database.BeginTransaction())
                {
                    // Удалить логин пользователя
                    foreach (var login in logins.ToList())
                    {
                        await _userManager.RemoveLoginAsync(login.UserId, new UserLoginInfo(login.LoginProvider, login.ProviderKey));
                    }

                    // Удалить пользователя из ролей
                    if (rolesForUser.Count() > 0)
                    {
                        foreach (var item in rolesForUser.ToList())
                        {
                            // item should be the name of the role
                            var result = await _userManager.RemoveFromRoleAsync(user.Id, item);
                        }
                    }

                    // Удаление пользователя
                    await _userManager.DeleteAsync(user);

                    // Фиксация транзакции удаления
                    transaction.Commit();
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

    }
}