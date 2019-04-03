using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBACA2.LittleShopperManagement
{
    /// <summary>
    /// Summary description for AddProductImage_Handler
    /// </summary>
    public class AddProductImage_Handler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
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