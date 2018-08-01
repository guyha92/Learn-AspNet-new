using PagedList;
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
    public class EmployeeController : Controller
    {
        private OrganizationContext db = new OrganizationContext();
         
        // GET: Employee
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.DepartmentSortParm = sortOrder == "Department" ? "Department_desc" : "Department";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var employees = from s in db.Employees
                            select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                employees = employees.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    employees = employees.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    employees = employees.OrderBy(s => s.StartDate);
                    break;
                case "date_desc":
                    employees = employees.OrderByDescending(s => s.StartDate);
                    break;                                
                case "Department":
                    employees = employees.OrderBy(s => s.DepartmentID);
                    break;
                case "Department_desc":
                    employees = employees.OrderByDescending(s => s.DepartmentID);
                    break;


                default:
                    employees = employees.OrderBy(s => s.LastName);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(employees.ToPagedList(pageNumber, pageSize));            
        }

        public ActionResult Employee ()
        { return View(new TrainingManagmentSystem.Models.Employee()); }

        // GET: Employee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }




        // GET: Employee/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name");
            ViewBag.RankingID = new SelectList(db.Rankings, "RankingID", "Name");
            ViewBag.Sectors = new SelectList(db.Sectors, "SectorID", "SectorType");
            ViewBag.SubSectorID = new SelectList(db.SubSectors, "SubSectorID", "SubSectorType");


            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                    
                    db.Employees.Add(employee);
                    db.SaveChanges();
                    return RedirectToAction("Index");
            }

                ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", employee.DepartmentID);
                ViewBag.RankingID = new SelectList(db.Rankings, "RankingID", "Name", employee.RankingID);
                ViewBag.Sectors = new SelectList(db.Sectors, "SectorID", "SectorType");
                ViewBag.SubSectorID = new SelectList(db.SubSectors, "SubSectorID", "SubSectorType",employee.SubSectorID);


            return View(employee);

        }

        //public ActionResult GetTrainingBySector(int id)
        //{
        //    List<Training> trainings =
        //       (from training in db.Trainings
        //        where (training.SectorID == id && training.TrainingEnd>=DateTime.Now)
        //        select training).ToList();
        //    var result = trainings;
        //    return View(result);

        //    //var result = db.Trainings.Where(x => x.SectorID == id);
        //    //return PartialView(result);
        //    //var result = db.Trainings.ToList();
        //    //return View(result);
        //}

        //public ActionResult TrainingsHistory(int id)
        //{

        //    List<Training> trainings =
        //       (from training in db.Trainings
        //        where (training.SectorID == id && training.TrainingEnd < DateTime.Now)
        //        select training).ToList();
        //    var result = trainings;
        //    return View(result);
        //}


        // GET: Employee/Edit

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", employee.DepartmentID);
            ViewBag.RankingID = new SelectList(db.Rankings, "RankingID", "Name", employee.RankingID);
            ViewBag.Sectors = new SelectList(db.Sectors, "SectorID", "SectorType");

            ViewBag.SubSectorID = new SelectList(db.SubSectors, "SubSectorID", "SubSectorType", employee.SubSectorID);
            return View(employee);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name",employee.DepartmentID);
            ViewBag.RankingID = new SelectList(db.Rankings, "RankingID", "Name", employee.RankingID);
            ViewBag.Sectors = new SelectList(db.Sectors, "SectorID", "SectorType");
            ViewBag.SubSectorID = new SelectList(db.SubSectors, "SubSectorID", "SubSectorType", employee.SubSectorID);

            return View(employee);
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }

            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
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
