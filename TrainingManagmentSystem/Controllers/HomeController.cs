using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainingManagmentSystem.DAL;
using TrainingManagmentSystem.Models;
using TrainingManagmentSystem.ViewModels;

namespace TrainingManagmentSystem.Controllers
{
    public class HomeController : Controller
    {
        private OrganizationContext db = new OrganizationContext();

        [Authorize]
        public ActionResult Index(User LogedInUser)
        {
            var currentUser= db.Users.Where(user => user.UserName == User.Identity.Name).First();

            ViewBag.Message = $"ברוך הבא { currentUser.FirstName }";

            DateTime SevenDaysAgo = DateTime.Now.AddDays(-7);

            HomeViewModel HomeVM= new HomeViewModel();
            HomeVM.TrainingsNearExpiration = (from training in db.Trainings
                                              where training.TrainingEnd >= SevenDaysAgo
                                              select training).ToList() ;

            return View(HomeVM);

        }
        

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}