using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBACA2.Classes2
{
    public class ProductImage
    {
        public Product Product { get; set; }
        public int ProductImageID { get; set; }
        public int ProductID { get; set; }
        public string ProductImageName { get; set; }
        public byte[] ProductImageData { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public bool PrimaryImage { get; set; }
        public ProductImage()
        {
            this.Product = new Product();
        }

    }
}