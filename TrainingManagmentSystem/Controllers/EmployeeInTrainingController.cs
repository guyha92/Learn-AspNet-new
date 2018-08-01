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
    public class EmployeeInTrainingController : Controller
    {
        private OrganizationContext db = new OrganizationContext();

        // GET: EmployeeInTraining
        public ActionResult Index()
        {
            var employeeInTrainings = db.EmployeeInTrainings.Include(e => e.Employee).Include(e => e.Training);
            return View(employeeInTrainings.ToList());
        }

        // GET: EmployeeInTraining/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeInTraining employeeInTraining = db.EmployeeInTrainings.Find(id);
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
            var employeeAlreadInTraining = (from emp in db.EmployeeInTrainings
                                            where emp.TrainingID == id
                                            select emp.EmployeeID).ToList();

            var allEmployees = db.Employees.Where((emp) => !employeeAlreadInTraining.Contains(emp.EmployeeID));

            ViewBag.EmployeeIDs = new SelectList(allEmployees, "EmployeeID", "FirstName").ToList();
            ViewBag.TrainingID = new SelectList(db.Trainings.Where(x=>id==x.TrainingID),"TrainingID","Name");
            ViewBag.id = id;
            return View();
        }


        // To protect from overposting attacks, p        // POST: EmployeeInTraining/Createlease enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeInTrainingID,TrainingID")] EmployeeInTraining employeeInTraining, int[] EmployeeIDs)
        {
            if (ModelState.IsValid)
            {            
                foreach (var id in EmployeeIDs)
                {
                    EmployeeInTraining emp = new EmployeeInTraining();
                    emp.TrainingID = employeeInTraining.TrainingID;
                    emp.EmployeeID = id;
                    db.EmployeeInTrainings.Add(emp);
                    db.SaveChanges();
                }          
            }

            return RedirectToAction("EmployeesInTraining", new { id = employeeInTraining.TrainingID });
            //ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "LastName", employeeInTraining.EmployeeID);
            //ViewBag.TrainingID = new SelectList(db.Trainings, "TrainingID", "Name", employeeInTraining.TrainingID);
            //return View(employeeInTraining);
        }

        public ActionResult EmployeesNotPassed()
        {            
            var employeesNotPassed = db.EmployeeInTrainings.Where((emp)=> emp.IfPass==false).Include(e => e.Employee).Include(e => e.Training);
            return View(employeesNotPassed.ToList());
        }

        public ActionResult EmployeesInTraining(int? id)
        {
            List<EmployeeInTraining> trainings =
               (from training in db.EmployeeInTrainings
                where (training.TrainingID == id)
                select training).ToList();
            var result = trainings;
            ViewBag.id = id;
            return View(result);

        }

        // GET: EmployeeInTraining/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeInTraining employeeInTraining = db.EmployeeInTrainings.Find(id);
            if (employeeInTraining == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "LastName", employeeInTraining.EmployeeID);
            ViewBag.TrainingID = new SelectList(db.Trainings, "TrainingID", "Name", employeeInTraining.TrainingID);
            ViewBag.id = id;
            return View(employeeInTraining);
        }

        // POST: EmployeeInTraining/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeInTrainingID,EmployeeID,TrainingID,IfPass")] EmployeeInTraining employeeInTraining)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employeeInTraining).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("EmployeesInTraining", new { id = employeeInTraining.TrainingID });                
            }
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "LastName", employeeInTraining.EmployeeID);
            ViewBag.TrainingID = new SelectList(db.Trainings, "TrainingID", "Name", employeeInTraining.TrainingID);
            return View(employeeInTraining);
        }

        // GET: EmployeeInTraining/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeInTraining employeeInTraining = db.EmployeeInTrainings.Find(id);
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
            EmployeeInTraining employeeInTraining = db.EmployeeInTrainings.Find(id);
            db.EmployeeInTrainings.Remove(employeeInTraining);
            db.SaveChanges();
            return RedirectToAction("EmployeesInTraining", new { id = employeeInTraining.TrainingID });
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
