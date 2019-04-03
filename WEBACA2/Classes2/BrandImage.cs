using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBACA2.Classes2
{
    public class BrandImage
    {
        public Brand Brand {get; set;}
        public int BrandImageId { get; set; }
        public int BrandID { get; set; }
        public string BrandImageFileName { get; set; }
        public string BrandImageContentType { get; set; }
        public int BrandImageContentLength { get; set; }
        public byte[] Photo { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public int CreatedBy { get; set; }
        public int DeletedBy { get; set; }
        public bool IsPrimaryPhoto { get; set; }
        public BrandImage()
        {
            this.Brand = new Brand();
        }

    }
}