using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBACA2.Classes
{
    public class Brand
    {
        //public Brand() { ;}
        //public Brand(int brandID,string brandName,string description,int brandVideoID,DateTime CreatedAt,DateTime UpdatedAt) 
        //{
        //    this.BrandID = brandID;
        //    this.BrandName = brandName;
        //    this.Description = description;
        //    this.BrandVideoID = brandVideoID;
        //    this.CreatedAt = CreatedAt;
        //    this.UpdatedAt = UpdatedAt;
        //}
        public int BrandID { get; set; }
        public string BrandName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt{ get; set; }
        public DateTime UpdatedAt{ get; set; }
        public int BrandVideoID { get; set; }

    }
}