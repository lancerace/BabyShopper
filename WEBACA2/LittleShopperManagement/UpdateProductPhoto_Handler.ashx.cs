using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WEBACA2.Classes;
namespace WEBACA2.LittleShopperManagement
{
    /// <summary>
    /// Summary description for UpdateProductPhoto_Handler
    /// </summary>
    public class UpdateProductPhoto_Handler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            ProductManager productManager = new ProductManager();
            //this is for uploading new image 
            try
            {
                //check if there is file from client in httpRequest
                if (HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    int numOfFiles = HttpContext.Current.Request.Files.Count;
                    // Get the uploaded image from the Files collection
                    //for each loop get file uploaded by client from httpRequest object, explicity convert to HttpPostedFile
                    for (int index = 0; index < numOfFiles; index++)
                    {
                        //use HttpPostedFile to get individual file uploaded by client

                        HttpPostedFile httpPostedFile = HttpContext.Current.Request.Files[index] as HttpPostedFile;

                        //if there is file in httpRequest object
                        if (httpPostedFile != null)
                        {

                            //convert file to byte array 
                            using (var binaryReader = new BinaryReader(httpPostedFile.InputStream))
                            {
                                string collectedProductID = HttpContext.Current.Request.Form["ProductID"].ToString();
                                //size of file : httpPostedFile.ContentLength
                                //write file to byte array with correct size
                                Byte[] imageByte = binaryReader.ReadBytes(httpPostedFile.ContentLength);
                                try //check adding image
                                {
                                    productManager.AddImageToOneProduct(httpPostedFile.FileName, imageByte, collectedProductID);
               
                                }
                                catch (Exception ex) //if there is error,return failResponse
                                {
                                    var failResponse = new
                                    {
                                        status = "error",
                                        message = "Unable to add photo. " +
                                        "If problem persist, contact administrator"
                                    };
                                    context.Response.ContentType = "application/json"; //send to client in json format
                                    context.Response.Write(JsonConvert.SerializeObject(failResponse));
                                    return;
                                }//end try catch
                            }







                        }//end if (httpPostedFile != null)
                    }//end for   

                    var successResponse = new
                    {
                        status = "success",
                        message = "Created " + numOfFiles + " photos."
                    };

                    context.Response.ContentType = "application/json";
                    context.Response.Write(JsonConvert.SerializeObject(successResponse));


                }//end if (HttpContext.Current.Request.Files.AllKeys.Any())



                //error handling
            }
            catch (Exception ex)
            {
                context.Response.Write(new KeyValuePair<bool, string>(false, "An error occurred while uploading the file. Error Message: " + ex.Message));
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