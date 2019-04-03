using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBACA2.Classes
{
    public class ProductImage
    {
        public int ProductImageID { get; set; }
        public string ProductImageName { get; set; }
        public byte[] ProductImageData { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public int ProductID { get; set; }
    }
}