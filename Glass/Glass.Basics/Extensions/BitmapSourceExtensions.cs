using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Glass.Basics.Wpf.Extensions
{
    public static class BitmapSourceExtensions
    {
        public static byte[] ToArray(this BitmapSource source)
        {
            byte[] data;
            using (var ms = new MemoryStream())
            {
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(source));
                encoder.Save(ms);
                data = ms.ToArray();
            }

            return data;
        }

        public static ImageSource FromArray(byte[] bytes)
        {
            using (var memoryStream = new MemoryStream(bytes))
            {
                var decoder = new PngBitmapDecoder(memoryStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
                BitmapSource bitmapFrame = decoder.Frames[0];                            
                return bitmapFrame;
            }
        }
    }
}