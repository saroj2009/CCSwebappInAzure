using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CCSmvc.Models;

namespace CCSmvc.Controllers
{
    public class AlbumController : Controller
    {
        AlbumBlobServices _blobServices = new AlbumBlobServices();
        // GET: Album
        public ActionResult Index()
        {
            List<AlbumBlobDetails> blobs = new List<AlbumBlobDetails>();
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
            CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("teamalbum");

            // Loop over items within the container and output the length and URI.
            foreach (IListBlobItem item in container.ListBlobs(null, false))
            {
               
                if (item.GetType() == typeof(CloudBlockBlob))
                {
                    CloudBlockBlob blob = (CloudBlockBlob)item;
                    AlbumBlobDetails bDetails = new AlbumBlobDetails();
                    var imgName= blob.Name.ToString();
                    bDetails.Name = imgName;
                    bDetails.Thumnail = imgName.Replace(".","_thumb.");
                    blobs.Add(bDetails);
                    Console.WriteLine("Block blob of length {0}: {1}", blob.Properties.Length, blob.Name);
                }
                else if (item.GetType() == typeof(CloudPageBlob))
                {
                    CloudPageBlob pageBlob = (CloudPageBlob)item;
                    Console.WriteLine("Page blob of length {0}: {1}", pageBlob.Properties.Length, pageBlob.Uri);
                }
                else if (item.GetType() == typeof(CloudBlobDirectory))
                {
                    CloudBlobDirectory directory = (CloudBlobDirectory)item;
                    Console.WriteLine("Directory: {0}", directory.Uri);
                }
            }
            return View(blobs);
        }
        [HttpPost]
        public ActionResult AddImage(FormCollection image)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    
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

                    ViewBag.Message = "Records added successfully.";
                }

                //return View();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}