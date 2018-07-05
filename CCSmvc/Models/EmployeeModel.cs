using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CCSmvc1.Models
{
    public class EmployeeModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Required(ErrorMessage = "First name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "DOB is required.")]
        public string DOB { get; set; }
        public string ImagePath { get; set; }
        public string aid { get; set; }
        public string dvid { get; set; }
        public EmployeeModel()
        {
            Url = "https://sarojwebappstorage.blob.core.windows.net/sarojcontainer/";
        }
        public string Url { get; set; }
    }

    public class BlobServices
    {
        public CloudBlobContainer GetCloudBlobContainer()
        {
            string connString = "DefaultEndpointsProtocol=https;AccountName=sarojwebappstorage;AccountKey=bcXBWqEdljs7PbmVM83w+AtYqYazQIhp2O+9gikYWwlC2a4fNTHVnvgc83ETZpLquQTYGTl+4CrupCK4zWnXDg==";
            string destContainer = "sarojcontainer";

            // Get a reference to the storage account  
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer blobContainer = blobClient.GetContainerReference(destContainer);
            if (blobContainer.CreateIfNotExists())
            {
                blobContainer.SetPermissions(new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                });

            }
            return blobContainer;

        }
    }
}