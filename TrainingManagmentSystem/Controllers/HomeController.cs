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

        public ActionResult Index(User LogedInUser)
        {

            ViewBag.Message = $"Welcome: { Session["username"] }";

            HomeViewModel HomeVM= new HomeViewModel();            
            HomeVM.TrainingsNearExpiration = from training in db.Trainings
                                             where training.TrainingEnd >= (DateTime.Now.AddDays(- 7))
                                             select training;

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