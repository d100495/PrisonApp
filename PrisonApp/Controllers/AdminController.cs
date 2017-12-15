using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using PrisonApp.Models;
using PrisonApp.Models.Identity;
using PrisonApp.Models.ViewModels;


namespace PrisonApp.Content
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private Model _db = new Model();
        private AppUserManager _userManager;
        //   private ApplicationRoleManager _roleManager;
        public AdminController()
        {
        }

        public AdminController(AppUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;

            // RoleManager = roleManager;
        }




        public AppUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Admin
        public ActionResult WyswietlUzytkownikow()
        {
            var list = _db.Users.ToList(); ;
            return View(list);
        }

        [HttpGet]
        public ActionResult EdytujUzytkownika(string id)
        {

            AppUser _user = new AppUser();
            _user = UserManager.FindById(id);
            EdytujUzytkownikaViewModel user = new EdytujUzytkownikaViewModel
            {
                Id = _user.Id,
                UserName = _user.UserName,
                Name = _user.Imie,
                Surname = _user.Nazwisko,
                Email = _user.Email,
                Pesel = _user.Pesel
            };

            return View(user);
        }

        [HttpPost]
        public async Task<ActionResult> EdytujUzytkownika(EdytujUzytkownikaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var store = new UserStore<AppUser>(_db);
                var manager = new AppUserManager(store);
                var currentUser = manager.FindById(model.Id);
                currentUser.UserName = model.UserName;
                currentUser.Imie = model.Name;
                currentUser.Nazwisko = model.Surname;
                currentUser.Email = model.Email;
                currentUser.Pesel = model.Pesel;
                await manager.UpdateAsync(currentUser);
                var ctx = store.Context;
                ctx.SaveChanges();
                return RedirectToAction("WyswietlUzytkownikow");
            }
            return View();
        }

    }
}