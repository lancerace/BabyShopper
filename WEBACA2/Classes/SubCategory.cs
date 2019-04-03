using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBACA2.Classes
{
    public class SubCategory
    {

        public int SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }  
        public int CategoryID { get; set; }
    }
}