using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WEBACA2.Classes;

namespace WEBACA2.LittleShopperManagement
{
    /// <summary>
    /// Summary description for deleteSubBrandPhoto_Handler
    /// </summary>
    public class deleteSubBrandPhoto_Handler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            string subBrandImageID = context.Request.Form["key"].ToString();

            SubBrandManager subBrandManager = new SubBrandManager();
            if (subBrandManager.DeleteOneImage(subBrandImageID))
            {
                var Response = new
                    {
                        status = "Success!",
                        message = "Deleted one SubBrand Image."
                    };
                context.Response.ContentType = "application/json";
                context.Response.Write(JsonConvert.SerializeObject(Response));

            }
            else
            {
                var Response = new
                {
                    status = "Fail!",
                    message = "Unable to delete SubBrand Image."
                };
                context.Response.ContentType = "application/json";
                context.Response.Write(JsonConvert.SerializeObject(Response));
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}