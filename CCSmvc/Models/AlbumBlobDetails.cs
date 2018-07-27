using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CCSmvc.Models
{
    public class AlbumBlobDetails
    {
       
        public string Name { get; set; }
        public string Thumnail { get; set; }
        public AlbumBlobDetails()
        {
            Url = "https://sarojwebappstorage.blob.core.windows.net/teamalbum/";
            ThumbnailUrl = "https://sarojwebappstorage.blob.core.windows.net/teamalbumthumbnail/";
        }
    
        public string Url { get; set; }
        public string ThumbnailUrl { get; set; }
    }
    public class AlbumBlobServices
    {

        public CloudBlobContainer GetCloudBlobContainer()
        {
            //string connString = "DefaultEndpointsProtocol=https;AccountName=sarojwebappstorage;AccountKey=bcXBWqEdljs7PbmVM83w+AtYqYazQIhp2O+9gikYWwlC2a4fNTHVnvgc83ETZpLquQTYGTl+4CrupCK4zWnXDg==";
            //string destContainer = "sarojcontainer";

           // // Get a reference to the storage account  
            //CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connString);
            //CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            //CloudBlobContainer blobContainer = blobClient.GetContainerReference(destContainer);

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("teamalbum");
            //CloudBlockBlob blob = container.GetBlockBlobReference(fileName);//Changed path to fileName

            if (container.CreateIfNotExists())
            {
                container.SetPermissions(new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                });

            }
            return container;

        }
    }
}