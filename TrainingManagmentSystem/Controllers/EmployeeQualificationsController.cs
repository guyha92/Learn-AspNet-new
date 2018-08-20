using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrainingManagmentSystem.DAL;
using TrainingManagmentSystem.Models;

namespace TrainingManagmentSystem.Controllers
{
    public class EmployeeQualificationsController : Controller
    {
        private OrganizationContext db = new OrganizationContext();

        // GET: EmployeeQualifications
        public ActionResult Index(int empId)
        {
            var employeeQualification = db.EmployeeQualification.Include(e => e.Employee).Include(e => e.Qualification).
                                        Where(empQual=> empQual.EmployeeID == empId);
            return View(employeeQualification.ToList());
        }

        // GET: EmployeeQualifications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeQualification employeeQualification = db.EmployeeQualification.Include(e => e.Employee).Include(e => e.Qualification).
                                                            Where(empQual=> empQual.EmployeeQualificationID == id).FirstOrDefault();
            if (employeeQualification == null)
            {
                return HttpNotFound();
            }
            return View(employeeQualification);
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
