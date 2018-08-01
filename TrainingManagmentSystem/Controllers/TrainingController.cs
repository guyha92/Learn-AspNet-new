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
    public class TrainingController : Controller
    {
        private OrganizationContext db = new OrganizationContext();

        // GET: Training
        public ViewResult Index(string sortOrder, string searchString, int? page)
        {
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }

            var trainings = from t in db.Trainings
                            select t;

            if (!String.IsNullOrEmpty(searchString))
            {
                trainings = trainings.Where(s => s.Name.Contains(searchString));
            }

            switch (sortOrder)
            {                
                case "Date":
                    trainings = trainings.OrderBy(s => s.TrainingDate);
                    break;
                case "date_desc":
                    trainings = trainings.OrderByDescending(s => s.TrainingDate);
                    break;               
                default:
                    trainings = trainings.OrderByDescending(s => s.TrainingDate);
                    break;
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(trainings.ToPagedList(pageNumber, pageSize));
        }

        public JsonResult GetSectorList(string searchTerm)
        {
            var SectorList = db.Sectors.ToList();

            if (searchTerm != null)
            {
                SectorList = db.Sectors.Where(x => x.SectorType.Contains(searchTerm)).ToList();
            }

            var modifiedData = SectorList.Select(x => new
            {
                id = x.SectorID,
                text = x.SectorType
            });
            return Json(modifiedData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSubSectorList(string sectors)
        {
            List<int> sectorsList = new List<int>();
            sectorsList = sectors.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            db.Configuration.ProxyCreationEnabled = false;
            List<SubSector> SubSectorList = db.SubSectors.Where(x => sectorsList.Contains(x.SectorID)).ToList();

            return Json(SubSectorList, JsonRequestBehavior.AllowGet);
        }

        // GET: Training/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Training training = db.Trainings.Find(id);
            if (training == null)
            {
                return HttpNotFound();
            }

            return View(training);
        }

        // GET: Training/Create
        public ActionResult Create()
        {

            ViewBag.Sectors = new MultiSelectList(db.Sectors, "SectorID", "SectorType");
            ViewBag.subSectors = new MultiSelectList(db.SubSectors, "SubSectorID", "SubSectortype");

            return View();
        }

        // POST: Training/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TrainingID,Name,TrainingDate,Location,TrainingEnd,NumberOfMeetings,Duration,ExpireDate,ExpirationDate")] Training training, int[] subsectors)
        {

            if (ModelState.IsValid)
            {
                //if (sectors != null)
                //{
                //    foreach (var id in sectors)
                //    {
                //        Sector sector = db.Sectors.Find(id);
                //        training.TrainingSectors.Add(sector);


                //    }
                foreach (var id in subsectors)
                {
                    SubSector subsector = db.SubSectors.Find(id);
                    training.AddSubSector(subsector);



                }



                db.Trainings.Add(training);
                db.SaveChanges();
                return RedirectToAction("Index");
            }



            return View(training);
        }


            // GET: Training/Edit/5
            public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Training training = db.Trainings.Find(id);
            if (training == null)
            {
                return HttpNotFound();
            }
            var sectorlist = (from s in training.TrainingSubSectors
                              select s.SubSector.SectorID).ToArray();

            var subsectorlist = (from s in training.TrainingSubSectors
                                 select s.SubSectorID).ToArray();

            ViewBag.Sectors = new MultiSelectList(db.Sectors, "SectorID", "SectorType");
            ViewBag.subSectors = new MultiSelectList(db.SubSectors, "SubSectorID", "SubSectortype", subsectorlist);



            return View(training);
        }

        // POST: Training/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TrainingID,Name,TrainingDate,Location,TrainingEnd,NumberOfMeetings,Duration,ExpireDate,ExpirationDate")] Training training, int[] subsectors)
        {
            if (ModelState.IsValid)
            {
                db.Entry(training).State = EntityState.Modified;
                // training.TrainingSubSectors.Clear();

                var SubSectorToTrainingList = db.TrainingSubSectors.Where(t => t.TrainingID == training.TrainingID).ToList();

                foreach (var train in SubSectorToTrainingList)
                {
                    db.TrainingSubSectors.Remove(train);

                }





                foreach (var id in subsectors)
                {
                    
                        SubSector sub = db.SubSectors.Find(id);
                        training.AddSubSector(sub);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(training);

        }


        // GET: Training/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Training training = db.Trainings.Find(id);
            if (training == null)
            {
                return HttpNotFound();
            }
            return View(training);
        }

        // POST: Training/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Training training = db.Trainings.Find(id);
            db.Trainings.Remove(training);
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
