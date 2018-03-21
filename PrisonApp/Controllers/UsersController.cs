using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using PrisonApplication.Models;
using PrisonApplication.Models.Identity;

namespace PrisonApplication.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private PrisonDatabase _db = new PrisonDatabase();
        private AppUserManager _userManager;


        public UsersController()
            {}

        public UsersController(AppUserManager userManager)
        {
            UserManager = userManager;

          
        }




        public AppUserManager UserManager
        {
            get => _userManager ?? HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            private set => _userManager = value;
        }



        // GET: Users
        public ActionResult GetUsers()
        {
            var list = _db.Users.ToList(); ;
            return View(list);
        }

        [HttpGet]
        public ActionResult EditUser(string id)
        {

            ApplicationUser _user = new ApplicationUser();
            _user = UserManager.FindById(id);
            ApplicationUser user = new ApplicationUser()
            {
                Id = _user.Id,
                UserName = _user.UserName,
                Name = _user.Name,
                Surname = _user.Surname,
                Email = _user.Email,
            };

            return View(user);
        }

        [HttpPost]
        public async Task<ActionResult> EditUser(ApplicationUser model)
        {
            if (ModelState.IsValid)
            {
                var store = new UserStore<ApplicationUser>(_db);
                var manager = new AppUserManager(store);
                var currentUser = manager.FindById(model.Id);
                currentUser.UserName = model.UserName;
                currentUser.Name = model.Name;
                currentUser.Surname = model.Surname;
                currentUser.Email = model.Email;
                await manager.UpdateAsync(currentUser);
                var ctx = store.Context;
                ctx.SaveChanges();
                return RedirectToAction("GetUsers");
            }
            return View();
        }
    }
}