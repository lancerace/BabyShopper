
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBACA2.Classes2
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
    }
}