using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBACA2.Classes2
{
    public class AgeGroup
    {
        public int AgeGroupID { get; set; }
        public string AgeGroupName { get; set; }
        public int MinimumAge { get; set; }
        public int MaximumAge { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
    }
}