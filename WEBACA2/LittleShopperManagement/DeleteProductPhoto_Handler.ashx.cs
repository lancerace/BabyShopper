using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WEBACA2.Classes;
namespace WEBACA2.LittleShopperManagement
{
    /// <summary>
    /// Summary description for DeleteProductPhoto_Handler
    /// </summary>
    public class DeleteProductPhoto_Handler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string productImageID = context.Request.Form["key"].ToString();
            ProductManager productManager = new ProductManager();

            if (productManager.DeleteOneImage(productImageID))
            {
                var Response = new
                {
                    status = "Success!",
                    message = "Deleted one Product Image."
                };
                context.Response.ContentType = "application/json";
                context.Response.Write(JsonConvert.SerializeObject(Response));

            }
            else
            {
                var Response = new
                {
                    status = "Fail!",
                    message = "Unable to delete Product Image."
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