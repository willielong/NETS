
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using ZXing.QrCode;

namespace Nest.Elasticsearch.Test
{

    class Program
    {
        //public static ElasticClient elasticClient = NestApiConnectionPool.Intence.elasticClient;
        //public static BaseRepository<Company> repository = BaseRepository<Company>.Intences;
        static void Main(string[] args)
        {
            //repository.Update();
           // new test1().sage<DeviceType>(1);
            Generate1("https://www.baidu.com");
        }
        /// <summary>
        /// 生成二维码,保存成图片
        /// </summary>
        static void Generate1(string text)
        {
            BarcodeWriter writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            QrCodeEncodingOptions options = new QrCodeEncodingOptions();
            options.DisableECI = true;
            //设置内容编码
            options.CharacterSet = "UTF-8";
            //设置二维码的宽度和高度
            options.Width = 500;
            options.Height = 500;
            //设置二维码的边距,单位不是固定像素
            options.Margin = 1;
            writer.Options = options;

            Bitmap map = writer.Write(text);
            string filename = @"C:\Users\wenlli\Desktop\generate1.png";
            map.Save(filename, ImageFormat.Png);
            map.Dispose();
        }
    }

    public class test1
    {

        public void sage<T>(int? sum) 
        {
            var type = typeof(T);
            var ts = type.ToString();
          var ss=  Enum.GetValues(typeof(T)).Cast<T>().ToList();
        }
    }
    public enum EnumAppendItemType
    {
        None = -999,
        [Description("--所有--")]
        All = 1,
        [Description("--请选择--")]
        Select = 2,
    }
    ///
    　　/// 访问设备
    　　 ///
    public enum DeviceType
    {
        [Description("PC")]
        PC = 1,
        [Description("移动端")]
        Mobile = 2
    }
}
