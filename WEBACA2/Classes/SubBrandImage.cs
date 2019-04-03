using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBACA2.Classes
{
    public class SubBrandImage
    {
        public int SubBrandImageID { get; set; }
        public string SubBrandImageName { get; set; }
        public byte[] SubBrandImageData { get; set; }    
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public int SubBrandID { get; set; }

    }
}