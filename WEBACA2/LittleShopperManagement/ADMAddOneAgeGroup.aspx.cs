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
    public partial class ADMAddOneAgeGroup : System.Web.UI.Page
    {
      
        [WebMethod]
        public static object AddOneAgeGroup(string WebFormData)
        {
            var webFormData = JsonConvert.DeserializeObject<dynamic>(WebFormData);
            bool addAgeGroup;


            string message = "";
            try
            {
                AgeGroupManager ageGroupManger = new AgeGroupManager();
                addAgeGroup=ageGroupManger.addOneAgeGroup(webFormData);
                if (addAgeGroup==true)
                {
                    var response = new
                    {
                        status = "success",
                        message = "Created a new AgeGroup record."
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
                        status = "fail",
                        message = "Unable to save AgeGroup record."
                    };
                    return response;

                }

            }
            catch (Exception ex)
            {
                var response = new
                {
                    addAgeGroup = false,
                    status = "fail",
                    message = "Unable to save AgeGroup record. " + ex.Message
                };
                return response;
            }

        }
    }
}