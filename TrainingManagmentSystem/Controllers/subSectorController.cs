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
    public class SubSectorController : Controller
    {
        private OrganizationContext db = new OrganizationContext();

        // GET: subSector
        public ActionResult Index()
        {
            return View(db.SubSectors.ToList());
        }

        // GET: subSector/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubSector subSector = db.SubSectors.Find(id);
            if (subSector == null)
            {
                return HttpNotFound();
            }
            return View(subSector);
        }

        // GET: subSector/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: subSector/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SubSectorID,SubSectortype")] SubSector subSector)
        {
            if (ModelState.IsValid)
            {
                db.SubSectors.Add(subSector);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(subSector);
        }

        // GET: subSector/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubSector subSector = db.SubSectors.Find(id);
            if (subSector == null)
            {
                return HttpNotFound();
            }
            return View(subSector);
        }

        // POST: subSector/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SubSectorID,SubSectortype")] SubSector subSector)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subSector).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(subSector);
        }

        // GET: subSector/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubSector subSector = db.SubSectors.Find(id);
            if (subSector == null)
            {
                return HttpNotFound();
            }
            return View(subSector);
        }

        // POST: subSector/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubSector subSector = db.SubSectors.Find(id);
            db.SubSectors.Remove(subSector);
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
