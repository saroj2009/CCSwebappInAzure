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
}