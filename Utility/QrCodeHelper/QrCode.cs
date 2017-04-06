using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.IO;
using ThoughtWorks.QRCode.Codec;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;


namespace Utility.QrCodeHelper
{
    public class QrCode
    {
        #region 生成二维码START
        /// <summary>
        /// 生成二维码（返回二维码路径）
        /// </summary>
        /// <param name="Data">二维码数据</param>
        /// <param name="Scale">二维码大小默认：5</param>
        /// <returns>String</returns>
        public static string CreateCode_Simple(string Data, int Scale = 5)
        {
            try
            {
                QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                //设置大小
                qrCodeEncoder.QRCodeScale = Scale;
                qrCodeEncoder.QRCodeVersion = 8;
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                System.Drawing.Image image = qrCodeEncoder.Encode(Data);
                string filename = "LookQRCode" + ".jpg";
                //如果文件夹不存在，则创建
                if (!Directory.Exists(HttpContext.Current.Server.MapPath("/QRCode/")))
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/QRCode/"));

                if (File.Exists(HttpContext.Current.Server.MapPath("/QRCode/" + filename)))
                    File.Delete(HttpContext.Current.Server.MapPath("/QRCode/" + filename));

                string filepath = HttpContext.Current.Server.MapPath(@"~\QRCode") + "\\" + filename;

                //去掉这个文件夹的读写属性
                System.IO.DirectoryInfo DirInfo = new DirectoryInfo(HttpContext.Current.Server.MapPath("/QRCode/"));
                DirInfo.Attributes = FileAttributes.Normal & FileAttributes.Directory;

                System.IO.FileStream fs = new System.IO.FileStream(filepath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);
                image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
                fs.Close();
                image.Dispose();
                return "/QRCode/" + filename;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        /// <summary>
        /// 生成二维码（返回二维码路径）针对数据过长，导致报错的情况
        /// </summary>
        /// <param name="Data"></param>
        /// <param name="Version"></param>
        /// <param name="Scale"></param>
        /// <returns></returns>
        public static string CreateCode_Simple(string Data, int Version, int Scale = 5)
        {
            try
            {
                QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                //设置大小
                qrCodeEncoder.QRCodeScale = Scale;
                qrCodeEncoder.QRCodeVersion = Version;
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                System.Drawing.Image image = qrCodeEncoder.Encode(Data);
                string filename = "LookQRCode" + ".jpg";
                //如果文件夹不存在，则创建
                if (!Directory.Exists(HttpContext.Current.Server.MapPath("/QRCode/")))
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/QRCode/"));

                if (File.Exists(HttpContext.Current.Server.MapPath("/QRCode/" + filename)))
                    File.Delete(HttpContext.Current.Server.MapPath("/QRCode/" + filename));

                string filepath = HttpContext.Current.Server.MapPath(@"~\QRCode") + "\\" + filename;

                //去掉这个文件夹的读写属性
                System.IO.DirectoryInfo DirInfo = new DirectoryInfo(HttpContext.Current.Server.MapPath("/QRCode/"));
                DirInfo.Attributes = FileAttributes.Normal & FileAttributes.Directory;

                System.IO.FileStream fs = new System.IO.FileStream(filepath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);
                image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
                fs.Close();
                image.Dispose();
                return "/QRCode/" + filename;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        /// <summary>
        /// 生成二维码（中心带有图片）
        /// </summary>
        /// <param name="Data">二维码数据</param>
        /// <param name="CenterImageUrl">中心图片的地址</param>
        /// <param name="Scale">二维码大小，默认：5</param>
        /// <param name="Width">中心图片宽，默认：70</param>
        /// <param name="Height">中心图片高，默认：70</param>
        /// <returns>String</returns>
        public static string CreateCode_Simple(string Data, string CenterImageUrl, int Scale = 5, int Width = 70, int Height = 70)
        {
            try
            {
                QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                //设置大小
                qrCodeEncoder.QRCodeScale = Scale;
                qrCodeEncoder.QRCodeVersion = 8;
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                System.Drawing.Image image = qrCodeEncoder.Encode(Data);
                string filename = "LookQRCode" + ".jpg";

                //如果文件夹不存在，则创建
                if (!Directory.Exists(HttpContext.Current.Server.MapPath("/QRCode/")))
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/QRCode/"));

                if (File.Exists(HttpContext.Current.Server.MapPath("/QRCode/" + filename)))
                    File.Delete(HttpContext.Current.Server.MapPath("/QRCode/" + filename));

                string filepath = HttpContext.Current.Server.MapPath(@"~\QRCode") + "\\" + filename;

                //去掉这个文件夹的读写属性
                System.IO.DirectoryInfo DirInfo = new DirectoryInfo(HttpContext.Current.Server.MapPath("/QRCode/"));
                DirInfo.Attributes = FileAttributes.Normal & FileAttributes.Directory;

                System.IO.FileStream fs = new System.IO.FileStream(filepath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);
                CombinImage(image, HttpContext.Current.Server.MapPath(CenterImageUrl), Height, Width).Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);//二维码中心加入图标的代码
                fs.Close();
                image.Dispose();
                return "/QRCode/" + filename;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        /// <summary>
        /// 生成二维码（返回BitMap类型）
        /// </summary>
        /// <param name="Data">二维码数据</param>
        /// <param name="Scale">二维码大小，默认：5</param>
        /// <returns>Bitmap</returns>
        public static Bitmap CreateCode(string Data, int Scale = 5, int width = 240, int height = 240)
        {
            try
            {
                QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                //设置大小
                qrCodeEncoder.QRCodeScale = Scale;
                qrCodeEncoder.QRCodeVersion = 0;
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                System.Drawing.Image image = qrCodeEncoder.Encode(Data);
                return new Bitmap(image, new Size(240, 240));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //二维码解码(解析二维码隐藏的内容)
        //public static string GetCode(string filePath)
        //{
        //    //二维码解码(解析二维码隐藏的内容)
        //    if (!System.IO.File.Exists(filePath))
        //        return null;
        //    Bitmap myBitmap = new Bitmap(System.Drawing.Image.FromFile(filePath));
        //    QRCodeDecoder decoder = new QRCodeDecoder();
        //    string decodedString = decoder.decode(new QRCodeBitmapImage(myBitmap));
        //    return decodedString;
        //}

        /// <summary>    
        /// 调用此函数后使此两种图片合并，类似相册，有个    
        /// 背景图，中间贴自己的目标图片    
        /// </summary>    
        /// <param name="imgBack">粘贴的源图片</param>    
        /// <param name="destImg">粘贴的目标图片</param>    
        public static System.Drawing.Image CombinImage(System.Drawing.Image imgBack, string destImg, int height = 100, int width = 100)
        {
            //照片图片
            System.Drawing.Image img = System.Drawing.Image.FromFile(destImg);

            if (img.Height != height || img.Width != width)
                img = KiResizeImage(img, width, height, 0);

            //如果原图片是索引像素格式之列的，则需要转换  //无法从带有索引像素格式的图像创建 Graphics 对象。
            if (IsPixelFormatIndexed(imgBack.PixelFormat))
            {
                Bitmap bmp = new Bitmap(imgBack.Width, imgBack.Height, PixelFormat.Format32bppArgb);
                Graphics g = Graphics.FromImage(bmp);
                g.DrawImage(imgBack, 0, 0, imgBack.Width, imgBack.Height);
                g.DrawImage(img, imgBack.Width / 2 - img.Width / 2, imgBack.Width / 2 - img.Width / 2, img.Width, img.Height);
                GC.Collect();
                g.Dispose();
                return bmp;
            }
            else
            {
                Graphics g = Graphics.FromImage(imgBack);
                g.DrawImage(imgBack, 0, 0, imgBack.Width, imgBack.Height);
                g.DrawImage(img, imgBack.Width / 2 - img.Width / 2, imgBack.Width / 2 - img.Width / 2, img.Width, img.Height);
                GC.Collect();
                return imgBack;
            }
        }

        /// <summary>    
        /// Resize图片    
        /// </summary>    
        /// <param name="bmp">原始Bitmap</param>    
        /// <param name="newW">新的宽度</param>    
        /// <param name="newH">新的高度</param>    
        /// <param name="Mode">保留着，暂时未用</param>    
        /// <returns>处理以后的图片</returns>    
        protected static System.Drawing.Image KiResizeImage(System.Drawing.Image bmp, int newW, int newH, int Mode)
        {
            try
            {
                System.Drawing.Image b = new Bitmap(newW, newH);
                Graphics g = Graphics.FromImage(b);
                // 插值算法的质量    
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(bmp, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                g.Dispose();
                return b;
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// 会产生graphics异常的PixelFormat
        /// </summary>
        private static PixelFormat[] indexedPixelFormats = { PixelFormat.Undefined, PixelFormat.DontCare,
PixelFormat.Format16bppArgb1555, PixelFormat.Format1bppIndexed, PixelFormat.Format4bppIndexed,
PixelFormat.Format8bppIndexed
    };
        /// <summary>
        /// 判断图片的PixelFormat 是否在 引发异常的 PixelFormat 之中
        /// </summary>
        /// <param name="imgPixelFormat">原图片的PixelFormat</param>
        /// <returns></returns>
        private static bool IsPixelFormatIndexed(PixelFormat imgPixelFormat)
        {
            foreach (PixelFormat pf in indexedPixelFormats)
            {
                if (pf.Equals(imgPixelFormat)) return true;
            }

            return false;
        }

        #endregion 生成二维码END
    }
}
