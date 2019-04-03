using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBACA2.Classes
{
    public class SubBrand
    {
        public SubBrand() { ;}
        public SubBrand(int subBrandID, string subBrandName, string description, int subBrandVideoID, DateTime CreatedAt, DateTime UpdatedAt, int brandId)
        {
            this.SubBrandID = subBrandID;
            this.SubBrandName = subBrandName;
            this.Description = description;
            this.SubBrandVideoID = subBrandVideoID;
            this.CreatedAt = CreatedAt;
            this.UpdatedAt = UpdatedAt;
            this.BrandID = brandId;
        }
        public int SubBrandID { get; set; }
        public string SubBrandName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt{ get; set; }
        public DateTime UpdatedAt{ get; set; }
        public int SubBrandVideoID { get; set; }
        public int BrandID { get; set; }
    }
}