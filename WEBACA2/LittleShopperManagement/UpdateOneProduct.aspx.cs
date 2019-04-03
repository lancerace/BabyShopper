using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEBACA2.Classes;

namespace WEBACA2.LittleShopperManagement
{
    public partial class UpdateOneProduct : System.Web.UI.Page
    {
        //protected void Page_Load(object sender, EventArgs e)
        //{

        //}


        [WebMethod]
        public static object GetAllCategory()
        {
            CategoryManager categoryManager = new CategoryManager();
            object response = categoryManager.GetAllCategory();


            //return list of category to ajax data.d
            return response;

        }

        [WebMethod]

        public static object GetAllSubCategory()
        {

            SubCategoryManager subCategoryManager = new SubCategoryManager();
            object response = subCategoryManager.GetAllSubCategory();
            return response;
        }

        [WebMethod]
        public static object GetAllBrand()
        {
            BrandManager brandManager = new BrandManager();

            object response = brandManager.GetAllBrand();


            return response;
        }


        [WebMethod]
        public static object GetAllSubBrand()
        {

            SubBrandManager subbrandManager = new SubBrandManager();

            object response = subbrandManager.GetAllSubBrand();
            return response;

        }


        [WebMethod]
        public static object UpdateProduct(string WebFormDataParameter)
        {
            object response = new object();
            dynamic webFormData = JsonConvert.DeserializeObject<dynamic>(WebFormDataParameter);
            ProductManager productManager = new ProductManager();
            bool[] check1;
            //collected field
            string collectedProductID = webFormData[0].productID.Value;
            string collectedproductNameText = webFormData[0].productNameText.Value;
            string collectedproductCodeText = webFormData[0].productCodeText.Value;
            string collectedproductPriceText = webFormData[0].productPriceText.Value;
            string collectedproductPointsEarned = webFormData[0].productPointsEarned.Value;
            string collectedproductPointsNeeded = webFormData[0].productPointsNeeded.Value;
            string collecteddescription = webFormData[0].description.Value;
            string collectedStockQuantities = webFormData[0].StockQuantities.Value.ToString();
            string collectedStockAvailability = webFormData[0].StockAvailability.Value.ToString();
            string collectedAlertOutOfStock = webFormData[0].AlertOutOfStock.Value.ToString();
            string collectedViewableByPublic = webFormData[0].ViewableByPublic.Value.ToString();

            //UpdateOneProduct return row affect and uniqueconstraint check
            check1 = productManager.UpdateOneProduct(collectedProductID, collectedproductNameText, collectedproductCodeText, collecteddescription, collectedViewableByPublic,
                collectedAlertOutOfStock);

            bool rowAffected = check1[0];
            bool constraint = check1[1];

            if (rowAffected == false)
                response = new
                {
                    status = "fail",
                    message = "Product not Added!"
                };
            else if (constraint == true)
            {
                response = new
                {
                    status = "constraint",
                    message = "Product Not Added. Name Already Exist. Please use a different Product Name"
                };

            }

            if ((rowAffected || !constraint)) //if no constraint and rowaffected > 0 for .UpdateOneProduct() execute this
            {
                bool checkStock = false, checkPrice = false, checkPoint = false, checkCategory = false, checkSubCategory=false;
                checkStock = productManager.UpdateProductStock(collectedStockQuantities, collectedStockAvailability, collectedAlertOutOfStock, collectedProductID);
                checkPrice = productManager.UpdateProductPrice(collectedProductID, collectedproductPriceText, "");
                checkPoint = productManager.UpdateProductPoint(collectedProductID, collectedproductPointsEarned, collectedproductPointsNeeded);
                // retrieve collectedCheckSubCategoryIDarray from client side
                //convert Newtonsoft.Json.Linq.JArray to int[]
                string[] items = webFormData[1].ToObject<string[]>();

                int countSub = 0;
                int countCateSub = 0;
                foreach (string s in items)
                {
                    if (s.Substring(0, 3) == "Sub")
                        countCateSub++;
                    else
                        countSub++;
                }
                //store subcategories ID
                int[] subcategories = new int[countCateSub];
                //store categories ID
                int[] categories = new int[countSub];
                    int subcount = 0;
                    int count = 0;
                foreach (string s in items)
                {
              
                    if (s.Substring(0, 3) == "Sub")
                    {
                        string temp = s.Substring(12, 1);//
                        subcategories[subcount] = int.Parse(temp);
                        subcount++;
                    }
                    else
                    {
                        string temp = s.Substring(9, 1);//
                        categories[count] = int.Parse(temp);
                        count++;
                    }

                }
                
                //do the update
                checkCategory = productManager.UpdateProductCategory(collectedProductID, categories);
                checkSubCategory = productManager.UpdateProductSubCategory(collectedProductID, subcategories);
                
                string message = "";
                string status = "fail";

                if (!checkPoint)
                    message += "Point Not Updated.\n";
                if (!checkPrice)
                    message += "Price Not Updated.\n";
                if (!checkStock)
                    message += "Stock Not Updated.\n";
                if (!checkCategory)
                    message += "Category Not Updated\n";
                if (!checkSubCategory)
                    message += "SubCategory Not Updated\n";

                if ((checkPoint && checkPrice && checkStock && checkCategory && checkSubCategory))//if all successfully updated,status =true
                
                    status = "success";
                
                response = new
                {
                    status,
                    message
                };
              
            }

            return response;
        }

        [WebMethod]
        public static object GetOneProduct(string inProductID)
        {

            ProductManager productManager = new ProductManager();
            object response = productManager.getOneProduct(inProductID);
            return response;
        }


        [WebMethod]
        public static object getAllImagesByProductID(string inProductID)
        {
            ProductManager productManager = new ProductManager();
            object response = new object();

            //sent response of object that include byte[] of image produced error.....
            //error : during serialization or deserialization using the JSON JavaScriptSerializer. The length of the string exceeds the value set on the maxJsonLength property.
            //there is a limitation of string that can be sent via json to client? 
            //resolved : dont send byte of imagedata to client,no point. ashx will handle the image processing         
            response = productManager.getAllImagesByProductID(inProductID);
            return response;
        }
    }
}