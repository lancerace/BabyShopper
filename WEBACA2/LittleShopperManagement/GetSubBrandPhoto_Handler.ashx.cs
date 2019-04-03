using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using WEBACA2.Classes;

namespace WEBACA2.LittleShopperManagement
{
    /// <summary>
    /// Summary description for GetSubBrandPhoto
    /// </summary>
    public class GetSubBrandPhoto : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {


            SubBrandManager subBrandManager = new SubBrandManager();
            SubBrandImage subBrandImage = new SubBrandImage();
            //get querystring from client request

            if (string.IsNullOrEmpty(context.Request.QueryString["id"]))
            {
                context.Response.Write("No Image found!");
            }
            else
            {

                string collectedSubBrandImageID = context.Request.QueryString["id"];

                //get 1 subBrandImage
                subBrandImage = subBrandManager.getOneSubBrandImage(collectedSubBrandImageID);

                //convert byte to memorystream
                //store in image so i can scale the image      
                ////binary write to response
                MemoryStream ms = new MemoryStream(subBrandImage.SubBrandImageData);
                Image returnImage = Image.FromStream(ms);

                //set image height to 50
                ScaleImage(returnImage, 50);


                //after that convert it back to byte again and do binarywrite() to sent it to response
                ImageConverter imageConverter = new ImageConverter();
                byte[] imageByte = (byte[])imageConverter.ConvertTo(returnImage, typeof(byte[]));
               
                context.Response.BinaryWrite(imageByte);

            }


        }

        public static System.Drawing.Image ScaleImage(System.Drawing.Image image, int maxHeight)
        {
            var ratio = (double)maxHeight / image.Height;
            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);
            var newImage = new Bitmap(newWidth, newHeight);
            using (var g = Graphics.FromImage(newImage))
            {
                g.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
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