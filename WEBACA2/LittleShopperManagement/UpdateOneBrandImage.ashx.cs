using WEBACA2.Classes2;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WEBACA2.LittleShopperManagement
{
    /// <summary>
    /// Summary description for UpdateOneBrandImager
    /// </summary>
    public class UpdateOneBrandImager : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                BrandManager bm = new BrandManager();
                BrandImage bi = new BrandImage();
                Brand brand = new Brand();
                brand.BrandID = Int32.Parse(HttpContext.Current.Request.Form["BrandId"].ToString());
            int numOfFiles = HttpContext.Current.Request.Files.Count;
                    // Get the uploaded image from the Files collection
                    for (int index = 0; index < numOfFiles; index++)
                    {
                        HttpPostedFile httpPostedFile =
                            HttpContext.Current.Request.Files[index] as HttpPostedFile;

                        if (httpPostedFile != null)
                        {


                            //Reference: http://stackoverflow.com/questions/359894/how-to-create-byte-array-from-httppostedfile
                            //Converting posted file into a byte array

                            using (var binaryReader = new BinaryReader(httpPostedFile.InputStream))
                            {
                                bi = new BrandImage();

                                if (HttpContext.Current.Request.Form["NEW_" + index.ToString()].ToString() == "1")
                                {
                                    bi.IsPrimaryPhoto = true;
                                }
                                else
                                {
                                    bi.IsPrimaryPhoto = false;
                                }
                                bi.Brand.BrandID = brand.BrandID;
                                bi.BrandImageFileName = httpPostedFile.FileName;
                                bi.BrandImageContentLength = httpPostedFile.ContentLength;
                                bi.BrandImageContentType = httpPostedFile.ContentType;
                                //Call the ReadBytes method of the binaryReader (which has the file information)
                                //to begin writing all the file data into a byte array with the correct size (I used the content length info)
                                bi.Photo = binaryReader.ReadBytes(bi.BrandImageContentLength);
                            }

                            try
                            {
                                bm.AddBrandPhoto(bi);
                            }
                            catch (Exception ex)
                            {
                                var failResponse = new
                                {
                                    status = "error",
                                    message = "Unable to add photo. " +
                                    "Keep calm. Try again. " +
                                    "If problem persist, contact us at reachus@internmonster.com"
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
                        message = "Created a new Brand record with " + numOfFiles + " photos"
                    };
                    context.Response.ContentType = "application/json";
                    context.Response.Write(JsonConvert.SerializeObject(successResponse));


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