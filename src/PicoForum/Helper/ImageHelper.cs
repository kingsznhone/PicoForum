using SkiaSharp;

namespace PicoForum.Helper
{
    public static class ImageHelper
    {
        public static void SaveImageWithQuality(MemoryStream ms, string filePath, int quality)
        {
            SKBitmap skBitmap = SKBitmap.Decode(ms.ToArray());
            using (FileStream fileStream = File.OpenWrite(filePath))
            {
                skBitmap.Encode(fileStream, SKEncodedImageFormat.Jpeg, (int)quality);
            } 
        }
    }
}