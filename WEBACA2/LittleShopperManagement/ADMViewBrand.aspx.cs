using WEBACA2.Classes2;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBACA2.LittleShopperManagement
{
    public partial class ADMViewBrand : System.Web.UI.Page
    {

        [WebMethod]
        public static object getAllBrandExcludingDeletedData()
        {

            BrandManager bm = new BrandManager();
            List<object> brandList = new List<object>();

            brandList = bm.getAllBrandExcludingDeletedData();
            //generic obj ,hold a list of categories
            object response = brandList;

            return response;
        }
        [WebMethod]
        public static object AddOneBrand(string WebFormData)
        {
            var webFormData = JsonConvert.DeserializeObject<dynamic>(WebFormData);
            int newBrandId = 0;


            try
            {
                BrandManager brandManager = new BrandManager();
                newBrandId = brandManager.AddOneBrand(webFormData);
                if (newBrandId > 0)
                {
                    var response = new
                    {
                        newBrandId = newBrandId,
                        status = "success",
                        message = "Created a new brand record."
                    };
                    return response;

                    //--- The following code is not needed. 
                    //--- because the function is configured to return an object (not a string)
                    //string response = JsonConvert.SerializeObject(successResponse);

                }
                else
                {
                    //Due to the simplicity of this example, this section is rarely tested.
                    var response = new
                    {
                        newBrandId = 0,
                        status = "fail",
                        message = "Unable to save Brand record."
                    };
                    return response;

                }

            }
            catch (Exception ex)
            {
                var response = new
                {
                    newBrandId = 0,
                    status = "fail",
                    message = "Unable to save student record. " + ex.Message
                };
                return response;
            }

        }//end of addOneBrand
        [WebMethod]
        public static object deleteOneBrand(string WebFormData)
        {
            BrandManager bm = new BrandManager();
            bool status = bm.deleteOneBrand(WebFormData);
            return status;
        }
        [WebMethod]
        public static object getOneBrand(string brandID)
        {
            BrandManager bm = new BrandManager();
            object response = bm.getOneBrand(brandID);
            return response;
        }
        [WebMethod]
        public static object GetBrandImages(string inBrandId)
        {
            BrandManager bm = new BrandManager();
            object response = bm.GetBrandImages(inBrandId);
            return response;        
        }
        [WebMethod]
        public static object CountSubBrand (string inBrandId)
        {
            BrandManager bm = new BrandManager();
            object response = bm.CountSubBrand(inBrandId);
            return response;
        }
    }
}