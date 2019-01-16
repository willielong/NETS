using com.google.zxing;
using com.google.zxing.common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace 二维码
{
    public partial class QR2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CreateQrcode("http://www.baidu.com");
        }
        private void CreateQrcode(string nr)
        {
            try
            {
                int QSize = 200;//二维码大小
                ByteMatrix byteMatrix = new MultiFormatWriter().encode(nr, BarcodeFormat.QR_CODE, QSize, QSize);
                Bitmap bt = toBitmap(byteMatrix, "#000000", "#ffffff");
                using (var ms = new System.IO.MemoryStream())
                {
                    bt.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    string base64 = Convert.ToBase64String(ms.ToArray());
                    //imgShow.Attributes.Add("src", $"data:image/png;base64,{base64}");//用来在html img控件显示bitmap用法
                    Image1.ImageUrl = string.Format("data:image/png;base64,{0}", base64);//asp.net控件用法
                }
            }
            catch (Exception ex)
            {

            }
        }

        public Bitmap toBitmap(ByteMatrix matrix, string scolor, string qcolor)
        {
            int width = matrix.Width;
            int height = matrix.Height;
            Bitmap bmap = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    bmap.SetPixel(x, y, matrix.get_Renamed(x, y) != -1 ? ColorTranslator.FromHtml(scolor) : ColorTranslator.FromHtml(qcolor));
                }
            }

            return bmap;
        }
    }
}