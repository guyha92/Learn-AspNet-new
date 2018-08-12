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
    public class ExternalTrainingController : Controller
    {
        private OrganizationContext db = new OrganizationContext();

        [Authorize]
        // GET: ExternalTraining
        public ViewResult Index(string sortOrder, string searchString, int? page)
        {
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }

            var externalTrainings = from t in db.ExternalTrainings
                            select t;

            if (!String.IsNullOrEmpty(searchString))
            {
                externalTrainings = externalTrainings.Where(s => s.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "Date":
                    externalTrainings = externalTrainings.OrderBy(s => s.TrainingDate);
                    break;
                case "date_desc":
                    externalTrainings = externalTrainings.OrderByDescending(s => s.TrainingDate);
                    break;
                default:
                    externalTrainings = externalTrainings.OrderByDescending(s => s.TrainingDate);
                    break;
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(externalTrainings.ToPagedList(pageNumber, pageSize));            
        }

        // GET: ExternalTraining/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExternalTraining externalTraining = db.ExternalTrainings.Find(id);
            if (externalTraining == null)
            {
                return HttpNotFound();
            }
            return View(externalTraining);
        }

        // GET: ExternalTraining/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ExternalTraining/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExternalTrainingID,Name,type,TrainingDate,NumberOfMeetings,Duration,TrainingEnd,Location,Cost")] ExternalTraining externalTraining, int[] Employees)
        {
            if (ModelState.IsValid)
            {
                db.ExternalTrainings.Add(externalTraining);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(externalTraining);
        }

        // GET: ExternalTraining/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExternalTraining externalTraining = db.ExternalTrainings.Find(id);
            if (externalTraining == null)
            {
                return HttpNotFound();
            }
            return View(externalTraining);
        }

        // POST: ExternalTraining/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExternalTrainingID,Name,type,TrainingDate,NumberOfMeetings,Duration,TrainingEnd,Location,Cost")] ExternalTraining externalTraining)
        {
            if (ModelState.IsValid)
            {
                db.Entry(externalTraining).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(externalTraining);
        }

        // GET: ExternalTraining/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExternalTraining externalTraining = db.ExternalTrainings.Find(id);
            if (externalTraining == null)
            {
                return HttpNotFound();
            }
            return View(externalTraining);
        }

        // POST: ExternalTraining/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExternalTraining externalTraining = db.ExternalTrainings.Find(id);
            db.ExternalTrainings.Remove(externalTraining);
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
