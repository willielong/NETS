using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Nest.Elasticsearch.Api.Entity;
using Nest.Elasticsearch.Services;

namespace Nest.Elasticsearch.Core.Controllers
{
    public class HomeController : Controller
    {
        public ICompanyServices services { get; set; }
        public IEntiytBase entiytBase { get; set; }
        //public HomeController(ICompanyServices _services)
        //{
        //    services = _services;
        //}
        public ActionResult Index()
        {
            string HostName = string.Empty;
            string ip = string.Empty;
            string ipv4 = String.Empty;

            if (!string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"]))
                ip = Convert.ToString(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]);
            if (string.IsNullOrEmpty(ip))
                ip = Request.UserHostAddress;

            // 利用 Dns.GetHostEntry 方法，由获取的 IPv6 位址反查 DNS 纪录，<br> // 再逐一判断何者为 IPv4 协议，即可转为 IPv4 位址。
            foreach (IPAddress ipAddr in Dns.GetHostEntry(ip).AddressList)
            {
                if (ipAddr.AddressFamily.ToString() == "InterNetwork")
                {
                    ipv4 = ipAddr.ToString();
                }
            }
            string 主机名 = Server.MachineName;

            string IP地址 = Request.UserHostAddress;

            string 系统时间 = DateTime.Now.ToString();

            string 服务端口 = Request.ServerVariables["SERVER_PORT"];

            string 操作系统 = Environment.OSVersion.ToString().Remove(0, 10);

            string 环境版本 = Request.ServerVariables["SERVER_SOFTWARE"];
            HostName = "主机名: " + Dns.GetHostEntry(ip).HostName + " IP: " + ipv4 + GetWebClientIp();
            ViewBag.Title = "Home Page"+HostName;
            services.IndexExist();
            GetCPUCounter();
            GetCup();
            return View();
        }


        /// <summary>
        /// 获取web客户端ip
        /// </summary>
        /// <returns></returns>
        public static string GetWebClientIp()
        {

            string userIP = "未获取用户IP";

            try
            {
                if (System.Web.HttpContext.Current == null
                 || System.Web.HttpContext.Current.Request == null
                 || System.Web.HttpContext.Current.Request.ServerVariables == null)
                {
                    return "";
                }

                string CustomerIP = "";

                //CDN加速后取到的IP simone 090805
                CustomerIP = System.Web.HttpContext.Current.Request.Headers["Cdn-Src-Ip"];
                if (!string.IsNullOrEmpty(CustomerIP))
                {
                    return CustomerIP;
                }

                CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                if (!String.IsNullOrEmpty(CustomerIP))
                {
                    return CustomerIP;
                }

                if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
                {
                    CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                    if (CustomerIP == null)
                    {
                        CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                    }
                }
                else
                {
                    CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }

                if (string.Compare(CustomerIP, "unknown", true) == 0 || String.IsNullOrEmpty(CustomerIP))
                {
                    return System.Web.HttpContext.Current.Request.UserHostAddress;
                }
                return CustomerIP;
            }
            catch { }

            return userIP;

        }
        /// <summary>
        /// 获取CPU信息
        /// </summary>
        /// <returns></returns>
        private static object GetCPUCounter()
        {
            PerformanceCounter pc = new PerformanceCounter();
            pc.CategoryName = "Processor";
            pc.CounterName = "% Processor Time";
            pc.InstanceName = "_Total";
            dynamic Value_1 = pc.NextValue();
            System.Threading.Thread.Sleep(1000);
            dynamic Value_2 = pc.NextValue();
            return Value_2;
        }
        public static object GetCup() {
            //获取CPU序列号代码 
            string cpuInfo = " ";//cpu序列号 
            ManagementClass mc = new ManagementClass("Win32_Processor");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
            }
            moc = null;
            mc = null;
            return cpuInfo;
        }
    }
}
