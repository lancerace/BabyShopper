using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection; //get generic object properties
using WEBACA2.Classes;

namespace WEBACA2.LittleShopperManagement
{
    public partial class ViewSubBrand : System.Web.UI.Page
    {

        [WebMethod]
        public static object GetAllSubBrandbyBrandId(string BrandID)
        {

            SubBrandManager subBrandManager = new SubBrandManager();
            //string subBrandId = HttpContext.Current.Session["subBrandID"].ToString();
            //string subBrandID = "2";
            object response = subBrandManager.GetAllSubBrandbyBrandId(BrandID);
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

        [WebMethod]
        public static object UpdateOneSubBrandByRecordId(string WebFormDataParameter)
        {
            SubBrandManager subBrandManager = new SubBrandManager();
            dynamic clientsideData = JsonConvert.DeserializeObject<dynamic>(WebFormDataParameter);
            object response = new object();
            //function WebFormData(inSubBrandId, inSubBrandName, inDescription, inSubBrandVideoLink) { 
            //this.subBrandId = inSubBrandId;
            //this.subBrandName = inSubBrandName;
            //this.description = inDescription;
            //this.subBrandVideoLink = inSubBrandVideoLink;
            //}
            try
            {
                bool status = subBrandManager.UpdateOneSubBrandByRecordId(clientsideData.subBrandId.Value.ToString(), clientsideData.subBrandName.Value, clientsideData.description.Value, clientsideData.subBrandVideoLink.Value);
                if (status == true)
                    response = new
                    {
                        status = "success",
                        message = "SubBrand Record Saved"
                    };
                else
                    response = new
                    {
                        status = "fail",
                        message = "Unable to save SubBrand Record"
                    };

            }//end try

            catch (Exception ex)
            {
                response = new
                {
                    status = "fail",
                    message = ex.Message
                };
            }



            return response;
        }
        [WebMethod]
        public static object DeleteOneSubBrand(string inSubBrandId)
        {
            SubBrandManager sbm = new SubBrandManager();
            bool status = sbm.DeleteOneSubBrand(inSubBrandId);
            return status;
        }

        [WebMethod]
        //addOneSubBrand(string inSubBrandName, string inDescription, string inBrandID)
        public static object addOneSubBrand(string WebFormDataParameter)
        {
            var webFormData = JsonConvert.DeserializeObject<dynamic>(WebFormDataParameter);
            SubBrandManager subBrandManager = new SubBrandManager();
            object collectedSubBrandIDAndValidationMsg = subBrandManager.addOneSubBrand(webFormData.subBrandName.Value, webFormData.description.Value, webFormData.subBrandVideoLink.Value, webFormData.getSubBrandID.Value);

            //need to convert object to type in order to get the properties
            Type type = collectedSubBrandIDAndValidationMsg.GetType();
            PropertyInfo info = type.GetProperty("uniqueConstraint");
            string collectedUniqueConstraint = info.GetValue(collectedSubBrandIDAndValidationMsg).ToString();

            //collect subbrandID from OUTPUT inserted.SubBrandID from sql Command 
            PropertyInfo subBrandID = type.GetProperty("collectedSubBrandID");
            string collectedSubBrandID = subBrandID.GetValue(collectedSubBrandIDAndValidationMsg).ToString();


            object response = new
            {
                uniqueConstraint = collectedUniqueConstraint,
                subBrandID = collectedSubBrandID
            };


            return response;
        
        }

    }
}