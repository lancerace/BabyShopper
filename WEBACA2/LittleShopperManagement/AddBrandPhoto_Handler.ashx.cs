using WEBACA2.Classes2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using System.IO;
using Newtonsoft.Json;

namespace BootstrapWithMyLittleShopper.admin
{
    /// <summary>
    /// Summary description for AddBrandPhoto_Handler
    /// </summary>
    public class AddBrandPhoto_Handler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                if (HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    BrandManager brandManager = new BrandManager();
                    BrandImage brandImage = new BrandImage();
                    int numOfFiles = HttpContext.Current.Request.Files.Count;
                    // Get the uploaded image from the Files collection
                    for (int index = 0; index < numOfFiles; index++)
                    {

                        HttpPostedFile httpPostedFile =
                            HttpContext.Current.Request.Files[index] as HttpPostedFile;

                        if (httpPostedFile != null)
                        {
                            //Converting posted file into a byte array

                            using (var binaryReader = new BinaryReader(httpPostedFile.InputStream))
                            {
                                brandImage = new BrandImage();



                                brandImage.BrandID = Int32.Parse(HttpContext.Current.Request.Form["BrandId"].ToString());
                                brandImage.BrandImageFileName = httpPostedFile.FileName;
                                brandImage.BrandImageContentLength = httpPostedFile.ContentLength;
                                brandImage.BrandImageContentType = httpPostedFile.ContentType;
                                //Call the ReadBytes method of the binaryReader (which has the file information)
                                //to begin writing all the file data into a byte array with the correct size (I used the content length info)
                                brandImage.Photo = binaryReader.ReadBytes(brandImage.BrandImageContentLength);
                            }

                            try
                            {
                                brandManager.AddBrandPhoto(brandImage);
                            }
                            catch (Exception ex)
                            {
                                var failResponse = new
                                {
                                    status = "error",
                                    message = "Unable to add photo. " +
                                    "Keep calm. Try again. " +
                                    "If problem persist, contact adminstrator"
                                };
                                context.Response.ContentType = "application/json";
                                context.Response.Write(JsonConvert.SerializeObject(failResponse));
                                return;
                            }

                        }
                    }//end of foreach block to save each product photo
                    var successResponse = new
                    {
                        status = "success",
                        message = "Created " + numOfFiles + " photos."
                    };
                    context.Response.ContentType = "application/json";
                    context.Response.Write(JsonConvert.SerializeObject(successResponse));


                }


            }
            catch (Exception ex)
            {
                context.Response.Write(new KeyValuePair<bool, string>(false, "An error occurred while uploading the file. Error Message: " + ex.Message));
            }

        }//End of ProcessRequest method


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}