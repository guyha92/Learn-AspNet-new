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
    public class EmployeeInExternalTrainingController : Controller
    {
        private OrganizationContext db = new OrganizationContext();

        // GET: EmployeeInTraining
        public ActionResult Index()
        {
            var employeeInTrainings = db.EmployeeInExternalTrainings.Include(e => e.Employee).Include(e => e.Training);
            return View(employeeInTrainings.ToList());
        }

        // GET: EmployeeInTraining/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeInExternalTraining employeeInTraining = db.EmployeeInExternalTrainings.Find(id);
            if (employeeInTraining == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = id;
            return View(employeeInTraining);
        }

        // GET: EmployeeInTraining/Create
        public ActionResult Create(int id)
        {                       
            var employeeAlreadInTraining = (from emp in db.EmployeeInExternalTrainings
                                            where emp.ExternalTrainingID == id
                                            select emp.EmployeeID).ToList();

            var allEmployees = db.Employees.Where((emp) => !employeeAlreadInTraining.Contains(emp.EmployeeID));

            ViewBag.EmployeeIDs = new SelectList(allEmployees, "EmployeeID", "FirstName").ToList();
            ViewBag.ExternalTrainingID = new SelectList(db.ExternalTrainings.Where(x=>id==x.ExternalTrainingID),"ExternalTrainingID","Name");
            ViewBag.id = id;
            return View();
        }


        // To protect from overposting attacks, p        // POST: EmployeeInTraining/Createlease enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeInExternalTrainingsID,ExternalTrainingID")] EmployeeInExternalTraining employeeInTraining, int[] EmployeeIDs)
        {
            if (ModelState.IsValid)
            {            
                foreach (var id in EmployeeIDs)
                {
                    EmployeeInExternalTraining emp = new EmployeeInExternalTraining();
                    emp.ExternalTrainingID = employeeInTraining.ExternalTrainingID;
                    emp.EmployeeID = id;
                    db.EmployeeInExternalTrainings.Add(emp);
                    db.SaveChanges();
                }          
            }

            return RedirectToAction("EmployeesInExternalTraining", new { id = employeeInTraining.ExternalTrainingID });           
        }

        public ActionResult EmployeesInExternalTraining(int? id)
        {
            List<EmployeeInExternalTraining> trainings =
               (from training in db.EmployeeInExternalTrainings
                where (training.ExternalTrainingID == id)
                select training).ToList();
            var result = trainings;
            ViewBag.id = id;
            return View(result);

        }

        public ActionResult EmployeeNotApproved()
        {
            var trainings = from emp in db.EmployeeInExternalTrainings
                            where emp.TrainingStatus == EmployeeInExternalTraining.TrainingStatuses.נדחה
                            select emp;
            
            return View(trainings.ToList());
        }

        public ActionResult EmployeeWaitingForApproval()
        {
            var trainings = from emp in db.EmployeeInExternalTrainings
                            where emp.TrainingStatus == EmployeeInExternalTraining.TrainingStatuses.ממתין
                            select emp;

            return View(trainings.ToList());
        }

        // GET: EmployeeInExternalTraining/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeInExternalTraining employeeInTraining = db.EmployeeInExternalTrainings.Find(id);
            if (employeeInTraining == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "LastName", employeeInTraining.EmployeeID);
            ViewBag.ExternalTrainingID = new SelectList(db.ExternalTrainings, "ExternalTrainingID", "Name", employeeInTraining.ExternalTrainingID);
            ViewBag.id = id;
            return View(employeeInTraining);
        }

        // POST: EmployeeInTraining/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( EmployeeInExternalTraining employeeInTraining)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employeeInTraining).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("EmployeesInExternalTraining", new { id = employeeInTraining.ExternalTrainingID });
            }
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "LastName", employeeInTraining.EmployeeID);
            ViewBag.ExternalTrainingID = new SelectList(db.ExternalTrainings, "ExternalTrainingID", "Name", employeeInTraining.ExternalTrainingID);
            return View(employeeInTraining);
        }

        // GET: EmployeeInTraining/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeInExternalTraining employeeInTraining = db.EmployeeInExternalTrainings.Find(id);
            if (employeeInTraining == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = id;

            return View(employeeInTraining);
        }

        // POST: EmployeeInTraining/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmployeeInExternalTraining employeeInTraining = db.EmployeeInExternalTrainings.Find(id);
            db.EmployeeInExternalTrainings.Remove(employeeInTraining);
            db.SaveChanges();
            return RedirectToAction("EmployeesInExternalTraining", new { id = employeeInTraining.ExternalTrainingID });
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
