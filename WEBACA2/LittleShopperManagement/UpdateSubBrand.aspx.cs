using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEBACA2.Classes;

namespace WEBACA2.LittleShopperManagement
{
    public partial class updateSubBrand : System.Web.UI.Page
    {
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //}



        [WebMethod]
        public static object getAllImagesBySubBrandID(string inSubBrandID)
        {
            SubBrandManager subbrandManager = new SubBrandManager();
            object response = new object();
            //sent response of object that include byte[] of image produced error.....
            //error : during serialization or deserialization using the JSON JavaScriptSerializer. The length of the string exceeds the value set on the maxJsonLength property.
            //there is a limitation of string that can be sent via json to client? 
            //resolved : dont send byte of imagedata to client,no point. ashx will handle the image processing         
            response = subbrandManager.getAllImagesBySubBrandID(inSubBrandID);
            return response;
        }



        [WebMethod]
        public static object GetOneSubBrandWVideoLinkByRecordId(string subBrandID)
        {

            try
            {
                SubBrandManager subBrandManager = new SubBrandManager();
                object response = subBrandManager.GetOneSubBrandWVideoLinkByRecordId(subBrandID);
                return response;
            }
            catch
            {            //for client-side jQuery's ajax().fail() 
                throw new HttpException((int)HttpStatusCode.InternalServerError,
                    "Fail to get SubBrand Record");
            }

        }
    }
}