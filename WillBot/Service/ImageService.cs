using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;

namespace WillBot.Service
{
    public class ImageService
    {
        public Bitmap Image_String(string font, int font_size, bool font_bold, Color bgcolor, Color color)
        {
            Font f = new Font("微軟正黑體", font_size, font_bold ? FontStyle.Bold : FontStyle.Regular); //文字字型
            Brush b = new SolidBrush(color); //文字顏色

            //計算文字長寬
            int img_width = 0, img_height = 0;
            using (Graphics gr = Graphics.FromImage(new Bitmap(1, 1)))
            {
                SizeF size = gr.MeasureString(font, f);
                img_width = Convert.ToInt32(size.Width);
                img_height = Convert.ToInt32(size.Height);
                gr.Dispose();
            }

            //圖片產生
            Bitmap image = new Bitmap(img_width, img_height);

            //填滿顏色並透明
            using (Graphics g = Graphics.FromImage(image))
            {
                g.Clear(bgcolor);
                image = Image_ChangeOpacity(image, 0.5f);
                g.Dispose();
            }

            //文字寫入
            using (Graphics g = Graphics.FromImage(image))
            {
                g.DrawString(font, f, b, 0, 0);
                g.Dispose();
            }

            return image;
        }

        protected Bitmap Image_ChangeOpacity(Image img, float opacityvalue)
        {
            Bitmap bmp = new Bitmap(img.Width, img.Height);
            Graphics graphics = Graphics.FromImage(bmp);
            graphics.InterpolationMode = InterpolationMode.High; //設定高品質插值法
            graphics.SmoothingMode = SmoothingMode.HighQuality; //設定高品質,低速度呈現平滑程度
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias; //此行文字反鋸齒

            ColorMatrix colormatrix = new ColorMatrix();
            colormatrix.Matrix33 = opacityvalue;
            ImageAttributes imgAttribute = new ImageAttributes();
            imgAttribute.SetColorMatrix(colormatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            graphics.DrawImage(img, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imgAttribute);

            graphics.Dispose();
            return bmp;
        }

        //水平合併
        public Image HorizontalMergeImages(Image img1, Image img2)
        {
            Image MergedImage = default(Image);
            Int32 Wide = 0;
            Int32 High = 0;
            Wide = img1.Width + img2.Width;//設定寬度           
            if (img1.Height >= img2.Height)
            {
                High = img1.Height;
            }
            else
            {
                High = img2.Height;
            }
            Bitmap mybmp = new Bitmap(Wide, High);
            Graphics gr = Graphics.FromImage(mybmp);
            //處理第一張圖片
            gr.DrawImage(img1, 0, 0);
            //處理第二張圖片
            gr.DrawImage(img2, img1.Width, 0);
            MergedImage = mybmp;
            gr.Dispose();
            return MergedImage;
        }

        //垂直合併
        public Image verticalMergeImages(Image img1, Image img2)
        {
            Image MergedImage = default(Image);
            Int32 Wide = 0;
            Int32 High = 0;
            High = img1.Height + img2.Height;//設定高度          
            if (img1.Width >= img2.Width)
            {
                Wide = img1.Width;
            }
            else
            {
                Wide = img2.Width;
            }
            Bitmap mybmp = new Bitmap(Wide, High);
            Graphics gr = Graphics.FromImage(mybmp);
            //處理第一張圖片
            gr.DrawImage(img1, 0, 0);
            //處理第二張圖片
            gr.DrawImage(img2, 0, img1.Height);
            MergedImage = mybmp;
            gr.Dispose();
            return MergedImage;
        }

        //圖片浮水印
        public Image MarkImage(Image img1, Image img2)
        {
            Image MergedImage = default(Image);
            //設定背景圖片
            Graphics gr = Graphics.FromImage(img1);
            //新建logo浮水印圖片
            Bitmap Logo = new Bitmap(img2.Width, img2.Height);
            Graphics tgr = Graphics.FromImage(Logo);
            ColorMatrix cmatrix = new ColorMatrix();
            //設定圖片色彩(透明度)
            cmatrix.Matrix33 = 0.5F;
            ImageAttributes imgattributes = new ImageAttributes();
            imgattributes.SetColorMatrix(cmatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            tgr.DrawImage(img2, new Rectangle(0, 0, Logo.Width, Logo.Height), 0, 0, Logo.Width, Logo.Height, GraphicsUnit.Pixel, imgattributes);
            tgr.Dispose();
            //logo圖片位置
            gr.DrawImage(Logo, img1.Width / 3, 10);
            gr.Dispose();
            MergedImage = img1;
            return MergedImage;
        }
    }
}