using WEBACA2.Classes2;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace WEBACA2.LittleShopperManagement
{
    /// <summary>
    /// Summary description for DeleteOneBrandImage
    /// </summary>
    public class DeleteOneBrandImage : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string brandPhotoId = "";
            brandPhotoId = context.Request.Form["key"].ToString();
            BrandManager brandManager = new BrandManager();

            if (brandManager.DeleteBrandPhoto(brandPhotoId))
            {
                var successResponse = new
                {
                    status = "success",
                    message = "Deleted one brand photo."
                };
                context.Response.ContentType = "application/json";
                context.Response.Write(JsonConvert.SerializeObject(successResponse));

            }
            else
            {
                var successResponse = new
                {
                    status = "fail",
                    message = "Unable to deleted one brand photo."
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


        //http://www.emoticode.net/c-sharp/resize-images-keeping-aspect-ratio.html
        /// <summary>
        /// Allows for image resizing. if AllowLargerImageCreation = true 
        /// you want to increase the size of the image
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="NewWidth"></param>
        /// <param name="MaxHeight"></param>
        /// <param name="AllowLargerImageCreation"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static byte[] ResizeImage(byte[] bytes, int NewWidth, int MaxHeight, bool AllowLargerImageCreation)
        {

            Image FullsizeImage = null;
            Image ResizedImage = null;
            //Cast bytes to an image
            FullsizeImage = byteArrayToImage(bytes);

            // Prevent using images internal thumbnail
            FullsizeImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);
            FullsizeImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);
            // Dae: 31/May/2015 10:10AM
            // Ah Tan Note: There is a slight logical bug here. I modified a bit from the original code
            // so that if the image requires a larger resize from the original, it will work.
            // If we are re sizing upwards to a bigger size
            if (AllowLargerImageCreation == false)
            {
                if (FullsizeImage.Width <= NewWidth)
                {
                    NewWidth = FullsizeImage.Width;
                }

            }

            //Keep aspect ratio
            int NewHeight = FullsizeImage.Height * NewWidth / FullsizeImage.Width;
            if (NewHeight > MaxHeight)
            {
                // Resize with height instead
                NewWidth = FullsizeImage.Width * MaxHeight / FullsizeImage.Height;
                NewHeight = MaxHeight;
            }

            ResizedImage = FullsizeImage.GetThumbnailImage(NewWidth, NewHeight, null, IntPtr.Zero);

            // Clear handle to original file so that we can overwrite it if necessary
            FullsizeImage.Dispose();

            return imageToByteArray(ResizedImage);
        }


        /// <summary>
        /// convert image to byte array
        /// </summary>
        /// <param name="imageIn"></param>
        /// <returns></returns>
        private static byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }


        /// <summary>
        /// Convert a byte array to an image
        /// </summary>
        /// <remarks></remarks>
        public static Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
    }
}