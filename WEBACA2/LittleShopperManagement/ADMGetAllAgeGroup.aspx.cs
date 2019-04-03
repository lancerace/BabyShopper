using WEBACA2.Classes2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBACA2.LittleShopperManagement
{
    public partial class ADMAddAgeGroup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static object GetAllAgeGroup()
        {
            object ageGroupList = new object();
            AgeGroupManager ageGroupManager = new AgeGroupManager();
            ageGroupList = ageGroupManager.getAllAgeGroup();
            
            object response = new object();// can dont use this line of code just put return ageGroupList at the bottom.

            response = ageGroupList;


            return response;
        }
    }
}