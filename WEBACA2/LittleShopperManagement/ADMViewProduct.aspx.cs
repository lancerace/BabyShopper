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
    public partial class ADMViewProduct : System.Web.UI.Page
    {
       [WebMethod]
        public static object getAllProductExcludingDeletedData()
       {
        ProductManager pm = new ProductManager();
        List<object> productList = new List<object>();

        productList = pm.getAllProductExcludingDeletedData();
        //generic obj ,hold a list of categories
        object response = productList;

        return response;
    }
    }
}