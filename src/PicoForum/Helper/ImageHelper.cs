
using System.Drawing;
using System.Drawing.Imaging;

namespace PicoForum.Helper
{
    public static class ImageHelper
    {
        public static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        public static void SaveImageWithQuality(MemoryStream ms, string filePath, long quality)
        {
            using (Image image = Image.FromStream(ms))
            {
                // 获取JPEG编码器信息
                ImageCodecInfo jpegEncoder = GetEncoder(ImageFormat.Jpeg);

                // 设置保存参数
                EncoderParameters encoderParameters = new EncoderParameters(1);
                encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, quality);

                // 保存图像
                image.Save(filePath, jpegEncoder, encoderParameters);
            }
        }
    }
}
