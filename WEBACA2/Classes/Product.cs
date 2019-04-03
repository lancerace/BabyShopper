using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBACA2.Classes
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int PriceID { get; set; }
        public int PointsID { get; set; }
        public int ProdVideoID { get; set; }
        public int BrandID { get; set; }
        public int SubBrandID { get; set; }
        public int StockID { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeleteAt { get; set; }
        public bool ViewablebyPublic { get; set; }
        public DateTime PublishedToPublicViewAt { get; set; }
        public bool AlertOutOfStock { get; set; }
        public int AgeGroupID { get; set; }
        public int SubCategoryID { get; set; }


    }
}