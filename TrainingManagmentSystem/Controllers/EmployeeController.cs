using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.OleDb;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrainingManagmentSystem.DAL;
using TrainingManagmentSystem.Models;
using TrainingManagmentSystem.ViewModels;

namespace TrainingManagmentSystem.Controllers

{
    public class EmployeeController : Controller
    {
        private OrganizationContext db = new OrganizationContext();

        // GET: Employee
        [Authorize]
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

        public ActionResult LoadFromExcel()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoadFromExcel(HttpPostedFileBase file)
        {
            DataSet ds = new DataSet();
            if (Request.Files["file"].ContentLength > 0)
            {
                string fileExtension =
                                     System.IO.Path.GetExtension(Request.Files["file"].FileName);

                if (fileExtension == ".xls" || fileExtension == ".xlsx")
                {
                    string fileLocation = Server.MapPath("~/Content/") + System.IO.Path.GetFileName(Request.Files["file"].FileName);
                    if (System.IO.File.Exists(fileLocation))
                    {

                        System.IO.File.Delete(fileLocation);
                    }
                    Request.Files["file"].SaveAs(fileLocation);
                    string excelConnectionString = string.Empty;
                    excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    //connection String for xls file format.
                    if (fileExtension == ".xls")
                    {
                        excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    }
                    //connection String for xlsx file format.
                    else if (fileExtension == ".xlsx")
                    {

                        excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    }
                    //Create Connection to Excel work book and add oledb namespace
                    OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                    excelConnection.Open();
                    DataTable dt = new DataTable();

                    dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    if (dt == null)
                    {
                        return null;
                    }

                    String[] excelSheets = new String[dt.Rows.Count];
                    int t = 0;
                    //excel data saves in temp file here.
                    foreach (DataRow row in dt.Rows)
                    {
                        excelSheets[t] = row["TABLE_NAME"].ToString();
                        t++;
                    }
                    OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);


                    string query = string.Format("Select * from [{0}]", excelSheets[0]);
                    using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                    {
                        dataAdapter.Fill(ds);
                    }
                }
                else
                {
                    throw new Exception("invalid excel file");
                }
                

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    Employee employee = new Employee();

                    employee.EmployeeZehut = int.Parse(ds.Tables[0].Rows[i]["EmployeeZehut"].ToString());
                    employee.Gender = (Gender)Enum.Parse(typeof( Gender),ds.Tables[0].Rows[i]["Gender"].ToString());
                    employee.DepartmentID = int.Parse(ds.Tables[0].Rows[i]["DepartmentID"].ToString());
                    employee.SubSectorID= int.Parse(ds.Tables[0].Rows[i]["SubSectorID"].ToString());
                    employee.RankingID = int.Parse(ds.Tables[0].Rows[i]["RankingID"].ToString());
                    employee.PositionPercentage= Double.Parse(ds.Tables[0].Rows[i]["PositionPercentage"].ToString());
                    employee.LastName = ds.Tables[0].Rows[i]["LastName"].ToString();
                    employee.FirstName = ds.Tables[0].Rows[i]["FirstName"].ToString();
                    employee.TrainingBudget= int.Parse(ds.Tables[0].Rows[i]["TrainingBudget"].ToString());
                    employee.NumberOfTrainings = int.Parse(ds.Tables[0].Rows[i]["NumberOfTrainings"].ToString());
                    employee.StartDate = DateTime.Parse(ds.Tables[0].Rows[i]["StartDate"].ToString());
                    employee.BirthDate= DateTime.Parse(ds.Tables[0].Rows[i]["BirthDate"].ToString());

                    employee.RemainingBudget = employee.TrainingBudget;
                    employee.RemainingTrainings = employee.NumberOfTrainings;

                    db.Employees.Add(employee);                    
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
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

            var defaultEmployee= new Employee();
            defaultEmployee.TrainingBudget = 700;
            defaultEmployee.NumberOfTrainings = 2;

            return View(defaultEmployee);
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

                try
                {
                    employee.RemainingBudget = employee.TrainingBudget;
                    employee.RemainingTrainings = employee.NumberOfTrainings;

                    db.Employees.Add(employee);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ViewBag.ErrorMessage = "תקלה בזמן השמירה עקב שגיאה בנתונים או שתעודת הזהות כבר קיימת במאגר";
                }
            }

                ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", employee.DepartmentID);
                ViewBag.RankingID = new SelectList(db.Rankings, "RankingID", "Name", employee.RankingID);
                ViewBag.Sectors = new SelectList(db.Sectors, "SectorID", "SectorType");
                ViewBag.SubSectorID = new SelectList(db.SubSectors, "SubSectorID", "SubSectorType",employee.SubSectorID);


            return View(employee);

        }

        public ActionResult EmployeeTrainings(int empId)
        {
            var ViewModel = new EmployeeTrainingsVM();

            var Employee = db.Employees.Where(emp => emp.EmployeeID == empId).First();

            ViewModel.EmployeeName = $"{Employee.LastName} {Employee.FirstName}";

            ViewModel.InternalTrainings = (from empTrain in db.EmployeeInTrainings
                                           where empTrain.EmployeeID == empId
                                           select empTrain).ToList();

            ViewModel.ExternalTrainings = (from empTrain in db.EmployeeInExternalTrainings
                                           where empTrain.EmployeeID == empId
                                           select empTrain).ToList();

            return View(ViewModel);
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
