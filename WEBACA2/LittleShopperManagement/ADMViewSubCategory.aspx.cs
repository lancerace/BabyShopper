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
    public partial class ADMViewSubCategory : System.Web.UI.Page
    {

        [WebMethod]
        public static object getAllSubCategory(string CategoryID)
        {

            SubCategoryManager scm = new SubCategoryManager();
            List<object> subCategoryList = new List<object>();
            subCategoryList = scm.getAllSubCategoryExcludeDeletedDataByInCategoryID(CategoryID);
            //generic obj ,hold a list of categories
            object response = subCategoryList;

            return response;
        }
        [WebMethod]
        public static object getAllCategory()
        {
            CategoryManager cm = new CategoryManager();
            List<Category> categoryList = new List<Category>();
            categoryList = cm.getAllCategory();
            object response = categoryList;
            return response;
        }
          [WebMethod]
        public static object addOneSubCategory(string WebFormData)
        {
             object response = new object();
             var webFormData = JsonConvert.DeserializeObject<dynamic>(WebFormData);
             SubCategoryManager scm = new SubCategoryManager();
             string collectedCategoryId = webFormData.CategoryId.Value.ToString();
             string collectedSubCategoryName = webFormData.SubCategoryName.Value;
             try 
             { 
                bool status=scm.addOneSubCategory(collectedCategoryId,collectedSubCategoryName);
                if (status == true)
                response = new
                {
                    status = "success",
                    message="subCategory record saved"
                };
                 else
                 response=new
                 {
                     status="fail",
                     message="Error, unable to save record"
                 };
             }
              catch(Exception ex)
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
          public static object deleteOneSubCategory(string WebFormData)
          {
              SubCategoryManager scm = new SubCategoryManager();
              var webFormData = JsonConvert.DeserializeObject<dynamic>(WebFormData);
              //string collectedsubCategoryId = webFormData.toString();
              bool status = scm.deleteOneSubCategory(WebFormData);
              return status;
          }
        [WebMethod]
        public static object getOneSubCategory(string subCategoryID)
        {
            SubCategoryManager scm = new SubCategoryManager();
            object response =scm.getOneSubCateogry(subCategoryID);
            return response;
        }
        [WebMethod]
        public static object updateOneSubCategory(string WebFormData)
        {
            object response = new object();
            var webFormData = JsonConvert.DeserializeObject<dynamic>(WebFormData);
            SubCategoryManager scm = new SubCategoryManager();
            string collectedSubCategoryId = webFormData.SubCategoryId.Value.ToString();
            string collectedCategoryId = webFormData.CategoryId.Value.ToString();
            string collectedSubCategoryName = webFormData.SubCategoryName.Value;
            try
            {
                bool status = scm.updateOneSubCategory(collectedSubCategoryId,collectedCategoryId, collectedSubCategoryName);
                if (status == true)
                    response = new
                    {
                        status = "success",
                        message = "subCategory record saved"
                    };
                else
                    response = new
                    {
                        status = "fail",
                        message = "Error, unable to save record"
                    };
            }
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
    }
}