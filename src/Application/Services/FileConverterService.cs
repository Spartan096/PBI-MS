using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace Application
{
    public class FileConverterService
    {
        public static string PlaceHolder = "data:image;base64,iVBORw0KGgoAAAANSUhEUgAAAGQAAABkCAYAAABw4pVUAAAABmJLR0QA/wD/AP+gvaeT...";

        public static string ConvertToBase64(Stream file, int width = 256)
        {
            if (file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    ms.Position = 0; // Reiniciar el stream antes de procesarlo

                    using (var resizedMs = ResizeImage(ms, width))
                    {
                        var fileBytes = resizedMs.ToArray();
                        return "data:image/png;base64," + Convert.ToBase64String(fileBytes);
                    }
                }
            }
            else
            {
                throw new FileLoadException();
            }
        }

        private static MemoryStream ResizeImage(Stream originalStream, int width)
        {
            using (var img = Image.FromStream(originalStream))
            {
                int height = Convert.ToInt32(width * img.Height / img.Width);
                using (var resizedImg = new Bitmap(width, height))
                {
                    using (var graphics = Graphics.FromImage(resizedImg))
                    {
                        graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        graphics.DrawImage(img, 0, 0, width, height);
                    }

                    var resizedMs = new MemoryStream();
                    resizedImg.Save(resizedMs, ImageFormat.Png);
                    resizedMs.Position = 0;
                    return resizedMs;
                }
            }
        }
    }
}
