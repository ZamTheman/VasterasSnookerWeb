using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace VästeråsSnooker.Helpers
{
    public static class ImageProcessing
    {
        /// <summary>
        /// From Stack Overflow.
        /// Takes an Image of any size and resize it to requested width and height.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns>Resized Image as Bitmap</returns>
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }
            return destImage;
        }

        /// <summary>
        /// Takes an Image and converts it to a base64 String for db storage
        /// </summary>
        /// <param name="imageIn"></param>
        /// <returns></returns>
        public static string imageToBase64(Image imageIn, ImageFormat imgFormat)
        {
            byte[] imageInBytes;
            using (MemoryStream ms = new MemoryStream())
            {
                imageIn.Save(ms, imgFormat);
                imageInBytes = ms.ToArray();
            }
            
            return Convert.ToBase64String(imageInBytes);
        }

        /// <summary>
        /// Takes a an base64 String and converts it to an Image
        /// </summary>
        /// <param name="imageAsBase64"></param>
        /// <returns></returns>
        public static Image Base64ToImage(string imageAsBase64, string imageType)
        {
            byte[] imageAsByteArray = Convert.FromBase64String(imageAsBase64);
            Image image;
            using (MemoryStream ms = new MemoryStream(imageAsByteArray))
            {
                image = Image.FromStream(ms);
            }

            return image;
        }
    }
}