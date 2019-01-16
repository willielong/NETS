using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ThoughtWorks.QRCode.Codec;

namespace 二维码
{
    public partial class QR1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Bitmap bpm = GetDimensionalCode("http://www.baidu.com");

            using (var ms = new System.IO.MemoryStream())
            {
                bpm.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                string base64 = Convert.ToBase64String(ms.ToArray());
                //imgShow.Attributes.Add("src", $"data:image/png;base64,{base64}");//用来在html img控件显示bitmap用法
                Image1.ImageUrl = string.Format("data:image/png;base64,{0}", base64);//asp.net控件用法
            }
        }
        /// <summary>
        /// 根据链接获取二维码
        /// </summary>
        /// <param name="link">链接</param>
        /// <returns>返回二维码图片</returns>
        private Bitmap GetDimensionalCode(string link)
        {

            Bitmap bmp = null;

            try
            {
                QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;//编码方式(注意：BYTE能支持中文，ALPHA_NUMERIC扫描出来的都是数字)
                qrCodeEncoder.QRCodeScale = 5;//大小(值越大生成的二维码图片像素越高)
                qrCodeEncoder.QRCodeVersion = 0;//版本(注意：设置为0主要是防止编码的字符串太长时发生错误)
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;//错误效验、错误更正(有4个等级)
                bmp = qrCodeEncoder.Encode(link);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Invalid version !");
            }
            return bmp;
        }
    }
}