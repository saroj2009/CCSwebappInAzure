﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CCSmvc.Repository;
using CCSmvc1.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using Microsoft.Azure;

using CCSmvc.Models;
using System.Data.Entity;


namespace CCSmvc.Controllers
{
    public class EmployeeController : Controller
    {
        //// GET: Employee/GetAllEmpDetails
        //public ActionResult GetAllEmpDetails()
        //{
        //    EmpRepository EmpRepo = new EmpRepository();
        //    return View(EmpRepo.GetAllEmployees());
        //}
        
        // GET: Employee/GetAllEmpDetails
        public ActionResult GetAllEmpDetails(string msg)
        {
            ViewBag.loginmsg = msg;
            EmpRepository EmpRepo = new EmpRepository();
            return View(EmpRepo.GetAllEmployees());
        }
        // GET: Employee/AddEmployee
        public ActionResult AddEmployee()
        {
            EmployeeModel Emp = new EmployeeModel();
            EmpRepository erobj = new EmpRepository();
            Emp.Id = erobj.RandomNumber(1, 1000);
            return View(Emp);
        }
        // POST: Employee/AddEmployee
        [HttpPost]
        public ActionResult AddEmployee(EmployeeModel Emp, FormCollection image)
        {

            //var clientPath = Path.GetDirectoryName(photo.FileName);
            //var filename = Path.GetFileName(photo.FileName);
            //var fileName = Path.GetDirectoryName(photo.FileName);
            //var fileName2 = Path.GetFullPath(photo.FileName);
            ////HttpPostedFileBase photo = Request.Files["photo"];
            ////string ss = form["photo"].ToString();
            ////var file2 = Request.Files[0];



            try
            {
                if (ModelState.IsValid)
                {
                    //EmpRepository EmpRepo = new EmpRepository();

                    //if (photo != null && photo.ContentLength > 0)
                    //{
                    //    var fileName = Path.GetFileName(photo.FileName);
                    //    var path = Path.Combine(Server.MapPath("~/TempFile/"), fileName); photo.SaveAs(path);
                    //    UploadImage_URL(path, fileName);

                    //    var fullPath = Server.MapPath("~/TempFile/" + fileName);

                    //    if (System.IO.File.Exists(fullPath))
                    //    {
                    //        System.IO.File.Delete(fullPath);
                    //        //ViewBag.deleteSuccess = "true";
                    //    }
                    //}

                    HttpPostedFileBase fileNameglobal = null;
                    foreach (string item in Request.Files)
                    {
                        HttpPostedFileBase file = Request.Files[item] as HttpPostedFileBase;
                        fileNameglobal = file;
                        if (file.ContentLength == 0)
                            continue;

                        if (file.ContentLength > 0)
                        {

                            CloudBlobContainer blobContainer = _blobServices.GetCloudBlobContainer();
                            CloudBlockBlob blob = blobContainer.GetBlockBlobReference(file.FileName);
                            blob.UploadFromStream(file.InputStream);


                        }
                    }

                    //var fileName = Path.GetFileName(photo.FileName);
                    var fileName = Path.GetFileName(fileNameglobal.FileName);
                    if (fileName == "" || fileName == null)
                        fileName = "NoImage.png";
                    EmpRepository EmpRepo = new EmpRepository();
                    Emp.ImagePath = fileName;
                    EmpRepo.AddEmployee(Emp);
                    ViewBag.Message = "Records added successfully.";
                }

                //return View();
                return RedirectToAction("GetAllEmpDetails");
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        // GET: Bind controls to Update details
        public ActionResult EditEmpDetails(int id)
        {
            EmpRepository EmpRepo = new EmpRepository();
            if (Convert.ToString(TempData["uid"]) == "" || Convert.ToString(TempData["uid"]) == null)
            {
                return RedirectToAction("GetAllEmpDetails", "Employee", new { msg = "Please log in for profile update" });
                
            }
           else if (Convert.ToString(TempData["uid"]) == Convert.ToString(id))
                {
               
                return View(EmpRepo.GetAllEmployees().Find(Emp => Emp.Id == id));
            }
            else
            {
                return RedirectToAction("GetAllEmpDetails", "Employee", new { msg = "You can update only for your Emp ID: " + Convert.ToString(TempData["uid"]) });
            }
            
        }
        // POST:Update the details into database
        [HttpPost]
        public ActionResult EditEmpDetails(int id, EmployeeModel obj, FormCollection image)
        {
            try
            {
                EmpRepository EmpRepo = new EmpRepository();

                HttpPostedFileBase fileNameglobal = null;
                foreach (string item in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[item] as HttpPostedFileBase;
                    fileNameglobal = file;
                    if (file.ContentLength == 0)
                        continue;

                    if (file.ContentLength > 0)
                    {

                        CloudBlobContainer blobContainer = _blobServices.GetCloudBlobContainer();
                        CloudBlockBlob blob = blobContainer.GetBlockBlobReference(file.FileName);
                        blob.UploadFromStream(file.InputStream);


                    }
                }

                //var fileName = Path.GetFileName(photo.FileName);
                var fileName = Path.GetFileName(fileNameglobal.FileName);
                if (fileName == "" || fileName == null)
                    fileName = null;
                obj.ImagePath = fileName;

                EmpRepo.UpdateEmployee(obj);
                return RedirectToAction("GetAllEmpDetails");
            }
            catch(Exception ex)
            {
                return View();
            }
        }
        // GET: Delete  Employee details by id
        public ActionResult DeleteEmp(int id, string filename)
        {
            try
            {
               
                EmpRepository EmpRepo = new EmpRepository();
                //if (EmpRepo.DeleteEmployee(id))
                //{
                //    ViewBag.AlertMsg = "Employee details deleted successfully";
                //}
                //return RedirectToAction("GetAllEmpDetails");


                
                if (Convert.ToString(TempData["uid"]) == "" || Convert.ToString(TempData["uid"]) == null)
                {
                    return RedirectToAction("GetAllEmpDetails", "Employee", new { msg = "Please log in for profile update" });

                }
                else if (Convert.ToString(TempData["uid"]) == Convert.ToString(id))
                {
                    DeleteBlob(filename);
                    if (EmpRepo.DeleteEmployee(id))
                    {
                        ViewBag.AlertMsg = "Employee details deleted successfully";
                       
                    }
                }
                else
                {
                    return RedirectToAction("GetAllEmpDetails", "Employee", new { msg = "You can update only for your Emp ID: " + Convert.ToString(TempData["uid"]) });
                }
                return RedirectToAction("GetAllEmpDetails");
            }
            catch (Exception ex)
            {
                return RedirectToAction("GetAllEmpDetails");
            }
        }

        [NonAction]
        public void DeleteBlob(string fileName)
        {
            EmpRepository erObj = new EmpRepository();
            int count = erObj.GetImageCount(fileName);
            if (count == 1)
            {
                EmployeeModel obj = new EmployeeModel();

                string path = obj.Url + fileName;


                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference("sarojcontainer");
                CloudBlockBlob blob = container.GetBlockBlobReference(fileName);//Changed path to fileName
                blob.Delete();
            }
        }
        BlobServices _blobServices = new BlobServices();
        public ActionResult Upload()
        {
            CloudBlobContainer blobContainer = _blobServices.GetCloudBlobContainer();
            List<string> blobs = new List<string>();
            foreach (var blobItem in blobContainer.ListBlobs())
            {
                blobs.Add(blobItem.Uri.ToString());

            }
            return View(blobs);
        }

        public JsonResult GetEmpDetails(string sord, int page, int rows, string searchString)
        {
            // Create Instance of DatabaseContext class for Accessing Database.
            DatabaseContext db = new DatabaseContext();

            //Setting Paging
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            //var Results = db.EmpDetails.Select(
            //    a => new
            //    {
            //        a.Id,
            //        a.Name,
            //        a.Description,
            //        a.DOB,
                   
            //    });

            var Results = (from pd in db.EmpDetails
                     join od in db.Payments on pd.Id.ToString() equals od.EmpID
                     orderby od.EmpID
                     select new
                     {
                         pd.Id,
                         pd.Name,
                         pd.Description,
                         od.DOB,
                     }).Distinct();
          

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
        //[NonAction]
        //public void UploadImage_URL(string filepath, string filename)
        //{
        //    // string accountname = "sarojwebappstorage";

        //    // string accesskey = "bcXBWqEdljs7PbmVM83w+AtYqYazQIhp2O+9gikYWwlC2a4fNTHVnvgc83ETZpLquQTYGTl+4CrupCK4zWnXDg==";

        //    try
        //    {
        //        // string connectionString = string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", accountname, accesskey);

        //        // StorageCredentials creden = new StorageCredentials(accountname, accesskey);
        //        // StorageCredentialsAccountAndKey ss = new StorageCredentialsAccountAndKey(accountname, accesskey);

        //        CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

        //        CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

        //        CloudBlobContainer container = blobClient.GetContainerReference("sarojcontainer");

        //        container.CreateIfNotExist();

        //        container.SetPermissions(new BlobContainerPermissions
        //        {
        //            PublicAccess = BlobContainerPublicAccessType.Blob

        //        });

        //        // Retrieve reference to a blob named "myblob".
        //        CloudBlockBlob blockBlob = container.GetBlockBlobReference(filename);
        //        // Create or overwrite the "myblob" blob with contents from a local file.
        //        using (var fileStream = System.IO.File.OpenRead(@filepath))
        //        {
        //            blockBlob.UploadFromStream(fileStream);
        //        }
        //        //// Loop over items within the container and output the length and URI.
        //        //foreach (IListBlobItem item in container.ListBlobs(null, false))
        //        //{
        //        //    if (item.GetType() == typeof(CloudBlockBlob))
        //        //    {
        //        //        CloudBlockBlob blob = (CloudBlockBlob)item;

        //        //        Console.WriteLine("Block blob of length {0}: {1}", blob.Properties.Length, blob.Uri);

        //        //    }
        //        //    else if (item.GetType() == typeof(CloudPageBlob))
        //        //    {
        //        //        CloudPageBlob pageBlob = (CloudPageBlob)item;

        //        //        Console.WriteLine("Page blob of length {0}: {1}", pageBlob.Properties.Length, pageBlob.Uri);

        //        //    }
        //        //    else if (item.GetType() == typeof(CloudBlobDirectory))
        //        //    {
        //        //        CloudBlobDirectory directory = (CloudBlobDirectory)item;

        //        //        Console.WriteLine("Directory: {0}", directory.Uri);
        //        //    }
        //        //}


        //        //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(file);
        //        //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        //        //Stream inputStream = response.GetResponseStream();
        //        //CloudBlockBlob cblob = container.GetBlockBlobReference(ImageName);
        //        //cblob.UploadFromStream(inputStream);
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //}


        //public void UploadImage_URL(string file, string ImageName)
        //{
        //    string accountname = "<YOUR_ACCOUNT_NAME>";

        //    string accesskey = "<YOUR_ACCESS_KEY>";

        //    try
        //    {

        //        StorageCredentials creden = new StorageCredentials(accountname, accesskey);

        //        CloudStorageAccount acc = new CloudStorageAccount(creden, useHttps: true);

        //        CloudBlobClient client = acc.CreateCloudBlobClient();

        //        CloudBlobContainer cont = client.GetContainerReference("<YOUR_CONTAINER_NAME>");

        //        cont.CreateIfNotExists();

        //        cont.SetPermissions(new BlobContainerPermissions
        //        {
        //            PublicAccess = BlobContainerPublicAccessType.Blob

        //        });
        //        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(file);
        //        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        //        Stream inputStream = response.GetResponseStream();
        //        CloudBlockBlob cblob = cont.GetBlockBlobReference(ImageName);
        //        cblob.UploadFromStream(inputStream);
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //}
    }
}