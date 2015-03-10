using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace DotNetCommon.Helper
{
    public class DrawingHelper
    {
        /// <summary>
        /// 将一个流转化为图片
        /// </summary>
        /// <param name="inputStream">将一个流转化为一个图片</param>
        /// <returns>图片</returns>
        public static Image ConverStreamToImage(Stream imageStream)
        {
            return Image.FromStream(imageStream, true, true);
        }

        /// <summary>
        /// 将一个图片转化为一个byte数组
        /// </summary>
        /// <param name="image">要转化的image</param>
        /// <returns>byte数组</returns>
        public static byte[] ImageToBytes(Image image)
        {
            using (var ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// 从一个bytes数组中生成一个图片
        /// </summary>
        /// <param name="imageBytes">要转化的byte数组</param>
        /// <returns>图片</returns>
        public static Image BytesToImage(byte[] imageBytes)
        {
            using (var stream = new MemoryStream(imageBytes))
            {
                return Image.FromStream(stream);
            }
        }

        /// <summary>
        /// 生成一个图片的缩略图
        /// </summary>
        /// <param name="sourceImage">原图片</param>
        /// <param name="maxSize">要生成缩略图和大小</param>
        /// <returns>要生成的缩略图</returns>
        public static Image ThumbnailCreate(Image sourceImage, Size size)
        {
            Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(() => { return true; });
            Size calculatedSize = calculateSize(sourceImage.Size, size);
            Image thumb = sourceImage.GetThumbnailImage(calculatedSize.Width, calculatedSize.Height, myCallback, System.IntPtr.Zero);
            return thumb;
        }

        /// <summary>
        /// 生成一个图片的缩略图
        /// </summary>
        /// <param name="imageBytes">图片的字节数组</param>
        /// <param name="maxSize">要生成缩略图和大小</param>
        /// <returns>Created image</returns>
        public static Image ThumbnailCreate(byte[] imageBytes, Size size)
        {
            return ThumbnailCreate(BytesToImage(imageBytes), size);
        }

        /// <summary>
        ///  根据现在的大小和缩略图的大小，生成一个成比例的大小
        /// </summary>
        /// <param name="currentSize">原大小</param>
        /// <param name="maxSize">缩略图大小</param>
        /// <returns>生成一个成比例的缩略图的大小</returns>
        private static Size calculateSize(Size currentSize, Size maxSize)
        {
            double widthAdjustment = (double)maxSize.Width / (double)currentSize.Width;
            double heightAdjustment = (double)maxSize.Height / (double)currentSize.Height;
            double reductionAmount = (widthAdjustment < heightAdjustment) ? widthAdjustment : heightAdjustment;
            int newWidth = (int)Math.Floor(currentSize.Width * reductionAmount);
            int newHeight = (int)Math.Floor(currentSize.Height * reductionAmount);
            Size calculatedSize = new Size(newWidth, newHeight);
            return calculatedSize;
        }
    }
}
