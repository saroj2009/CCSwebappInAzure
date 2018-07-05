using CCSmvc.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;


namespace CCSmvc.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetPaymentDetails(string sord, int page, int rows, string searchString)
        {
            // Create Instance of DatabaseContext class for Accessing Database.
            DatabaseContext db = new DatabaseContext();

            //Setting Paging
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            var Results = db.Payments.Select(
                a => new
                {
                    a.Id,
                    a.EmpID,
                    a.Name,
                    a.Description,
                    a.Email,
                    a.DOB,
                    a.Amount,
                    a.Date,
                    a.Month,
                    a.Status,
                });

            //Get Total Row Count
            int totalRecords = Results.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

            //Setting Sorting
            if (sord.ToUpper() == "DESC")
            {
                Results = Results.OrderByDescending(s => s.Name);
                Results = Results.Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                Results = Results.OrderBy(s => s.Name);
                Results = Results.Skip(pageIndex * pageSize).Take(pageSize);
            }
            //Setting Search
            if (!string.IsNullOrEmpty(searchString))
            {
                Results = Results.Where(m => m.Name == searchString || m.Name == searchString);
            }
            //Sending Json Object to View.
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = Results
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult CreatePayments([Bind(Exclude = "Id")] Payments payments)
        {
            StringBuilder msg = new StringBuilder();
            try
            {
                if (ModelState.IsValid)
                {
                    using (DatabaseContext db = new DatabaseContext())
                    {
                        db.Payments.Add(payments);
                        db.SaveChanges();
                        return Json("Saved Successfully", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    var errorList = (from item in ModelState
                                     where item.Value.Errors.Any()
                                     select item.Value.Errors[0].ErrorMessage).ToList();

                    return Json(errorList, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                var errormessage = "Error occured: " + ex.Message;
                return Json(errormessage, JsonRequestBehavior.AllowGet);
            }

        }

        public string EditPayments(Payments payments)
        {
            string msg;
            try
            {
                if (ModelState.IsValid)
                {
                    using (DatabaseContext db = new DatabaseContext())
                    {
                        db.Entry(payments).State = EntityState.Modified;
                        db.SaveChanges();
                        msg = "Saved Successfully";
                    }
                }
                else
                {
                    msg = "Some Validation ";
                }
            }
            catch (Exception ex)
            {
                msg = "Error occured:" + ex.Message;
            }
            return msg;
        }

        public string DeletePayments(int Id)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                Payments payments = db.Payments.Find(Id);
                db.Payments.Remove(payments);
                db.SaveChanges();
                return "Deleted successfully";
            }
        }
       
    }
}