using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrainingManagmentSystem.DAL;
using TrainingManagmentSystem.FilterAttributes;
using TrainingManagmentSystem.Models;

namespace TrainingManagmentSystem.Controllers
{
    public class EmployeeInTrainingController : Controller
    {
        private OrganizationContext db = new OrganizationContext();

        [Authorize]
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

        private List<EmployeeInTraining> GetEmployeeNotPassed()
        {
            var employeesNotPassed = db.EmployeeInTrainings.Where((emp) => emp.IfPass == false).Include(e => e.Employee).Include(e => e.Training);
            return employeesNotPassed.ToList();
        }

        public ActionResult EmployeesNotPassed()
        {                        
            return View(GetEmployeeNotPassed());
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

        private void ExportToExcel(object DataSource)
        {
            var gv = new GridView();
            gv.DataSource = DataSource;
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=DemoExcel.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "utf-8";

            //Here we set the correct encoding so that all characters show!
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Charset = "65001";
            byte[] b = new byte[] {
                0xef,
                0xbb,
                0xbf
            };
            Response.BinaryWrite(b);

            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            gv.RenderControl(objHtmlTextWriter);
            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();
        }

        public ActionResult EmployeesNotPassedExcel()
        {
            var employeesInTrainings = GetEmployeeNotPassed();
            var result = (from empInTrain in employeesInTrainings
                          join emp in db.Employees on empInTrain.EmployeeID equals emp.EmployeeID
                          join train in db.Trainings on empInTrain.TrainingID equals train.TrainingID
                          select new { תעודת_זהות = emp.EmployeeZehut, שם_משפחה = emp.LastName, שם_פרטי = emp.FirstName, שם_הדרכה = train.Name, עבר = empInTrain.IfPass }).ToList();

            ExportToExcel(result);            
            return View(employeesInTrainings);
        }

        [WordDocument]
        public ActionResult Diploma(int? id)
        {
            EmployeeInTraining employeeInTraining = db.EmployeeInTrainings.Find(id);
            return View(employeeInTraining);
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
