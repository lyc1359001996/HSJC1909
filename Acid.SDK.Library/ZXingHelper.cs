using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using ZXing.QrCode;

namespace Acid.SDK.Library
{
    public class ZXingHelper
    {
        /// <summary>
        /// 解码二维码
        /// </summary>
        /// <param name="barcodeBitmap">待解码的二维码图片</param>
        /// <returns>扫码结果</returns>
        public static string DecodeQrCode(Bitmap barcodeBitmap)
        {
            BarcodeReader reader = new BarcodeReader();
            reader.Options.CharacterSet = "UTF-8";
            var result = reader.Decode(barcodeBitmap);
            return (result == null) ? null : result.Text;
        }
        /// <summary>
        /// 生成条码
        /// </summary>
        /// <param name="text">内容</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns></returns>
        public static Bitmap Generate2DBarcode(string text, int width, int height)
        {
            BarcodeWriter writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.CODE_128;
            QrCodeEncodingOptions options = new QrCodeEncodingOptions()
            {
                DisableECI = true,//设置内容编码
                CharacterSet = "UTF-8",  //设置二维码的宽度和高度
                Width = width,
                Height = height,
                Margin = 1//设置二维码的边距,单位不是固定像素
            };
            writer.Options = options;
            Bitmap map = writer.Write(text);
            return map;
        }

        public static MemoryStream BitmapToMemoryStream(Bitmap bitmap)
        {
            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            //BitmapImage bit3 = new BitmapImage();
            //bit3.BeginInit();
            //bit3.StreamSource = ms;
            //bit3.EndInit();
            return ms;
        }
    }
}
