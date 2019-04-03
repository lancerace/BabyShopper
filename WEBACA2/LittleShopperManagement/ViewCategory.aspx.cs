using Newtonsoft.Json;        //JsonConvert utility
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services; //WebMethod
using System.Web.UI;
using System.Web.UI.WebControls;
using WEBACA2.Classes;

namespace WEBACA2.LittleShopperManagement
{


    public partial class ViewCategory : System.Web.UI.Page
    {


        [WebMethod]
        public static object getAllCategory()
        {

            CategoryManager cm = new CategoryManager();
            List<Category> categoryList = new List<Category>();

            categoryList = cm.GetAllCategory();
            //generic obj ,hold a list of categories
            object response = categoryList;

            return response;
        }


        [WebMethod]
        public static object getOneCategory(string categoryID)
        {
            try
            {
                CategoryManager categoryManager = new CategoryManager();
                object response = categoryManager.GetOneCategory(categoryID);
                return response;
            }
            catch
            {
                //for client-side jQuery's ajax().fail()
                throw new HttpException((int)HttpStatusCode.InternalServerError,
                    "Fail to get Category record");

            }
        }


        [WebMethod]
        //parameter WebFormDataParameter correspond to ajax call 
        public static object addOneCategory(string WebFormDataParameter)
        {

            object response = new object();
            //reconstruct stringify json string to usable object for server side

            dynamic webFormData = JsonConvert.DeserializeObject<dynamic>(WebFormDataParameter);
            //deserialize json format to asp server side format so it is usable when invoking classes
            //method addOneCategory();


            string collectedCategoryName = webFormData.CategoryName.Value;


            CategoryManager categoryManager = new CategoryManager();
            try
            {
                bool status = categoryManager.AddOneCategory(collectedCategoryName);

                if (status == true)
                    response = new
                    {
                        status = "success",
                        message = "Category Record Saved"
                    };
                else
                    response = new
                    {
                        status = "fail",
                        message = "Unable to save Category Record"
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
        public static object updateOneCategory(string WebFormDataParameter)
        {

            object response = new object();
            //reconstruct stringify json string to usable object for server side

            dynamic clientsideData = JsonConvert.DeserializeObject<dynamic>(WebFormDataParameter);
            //deserialize json format to asp server side format so it is usable when invoking classes
            //method updateOneCategory();

            string collectedCategoryName = clientsideData.CategoryName.Value;
            string collectedCategoryID = clientsideData.CategoryId.Value.ToString();

            CategoryManager categoryManager = new CategoryManager();
            try
            {
                bool status = categoryManager.UpdateOneCategory(collectedCategoryID, collectedCategoryName);
                if (status == true)
                    response = new
                    {
                        status = "success",
                        message = "Category Record Saved"
                    };
                else
                    response = new
                    {
                        status = "fail",
                        message = "Unable to save Category Record"
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








        //protected void Page_Load(object sender, EventArgs e)
        //{

        //}//end addOneCategory
    }
}