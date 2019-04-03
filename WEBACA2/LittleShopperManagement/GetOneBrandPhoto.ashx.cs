using WEBACA2.Classes2;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace WEBACA2.LittleShopperManagement
{
    /// <summary>
    /// Summary description for GetOneBrandPhoto
    /// </summary>
    public class GetOneBrandPhoto : IHttpHandler
    {
        int width = 0;
        int height = 0;
        public void ProcessRequest(HttpContext context)
        {
            string brandPhotoId = "";
            brandPhotoId = context.Request.QueryString["id"];
            if(brandPhotoId =="")
            {
                brandPhotoId = "207";
            }
            BrandManager bm = new BrandManager();
            BrandImage bi = new BrandImage();
            if(context.Request.QueryString["height"]!=null)
            {
                try
                {
                    height = int.Parse(context.Request.QueryString["height"]);
                }
                catch
                {
                    height = 0;
                }
            }

            if (context.Request.QueryString["width"] != null)
            {
                try
                {
                    width = int.Parse(context.Request.QueryString["width"]);
                }
                catch
                {
                    width = 0;
                }
            }

            if (width <= 0 && height <= 0)
            {

                context.Response.Clear();
                bi = bm.GetOneBrandImage(brandPhotoId);
                context.Response.AddHeader("Content-Disposition", "attachment; filename=" + bi.BrandImageFileName);
                context.Response.ContentType = bi.BrandImageContentType;
                context.Response.BinaryWrite(bi.Photo);
                context.Response.End();

            }
            else
            {
                context.Response.Clear();
                bi = bm.GetOneBrandImage(brandPhotoId);
                context.Response.ContentType =
                                        bi.BrandImageContentType;
                context.Response.AddHeader("Content-Disposition", "attachment; filename=" + bi.BrandImageFileName);
                byte[] buffer =
                ResizeImage(bi.Photo, width, height, true);
                context.Response.OutputStream.Write
                (buffer, 0, buffer.Length);
                context.Response.End();
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
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
        private static byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }
        public static Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
    }
}