using Elasticsearch.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Net;

namespace Nest.Elasticserarh.Api.Client.Helper
{
    public class NestApiConnectionPool
    {
        ///单列模式进行处理
        public static NestApiConnectionPool Intence = new NestApiConnectionPool();

        /// <summary>
        /// 初始化 elasticsearch 连接池
        /// </summary>
        public StaticConnectionPool newstPool { get; set; }
        public ElasticClient elasticClient { get; set; }
        public NestApiConnectionPool()
        {
            InitPool();
            InitClient();
        }

        /// <summary>
        /// 初始化连接数据
        /// </summary>
        private void InitPool()
        {
            ///ssl 的不安全认证（不需要安装证书）
            SetCertificatePolicy();

            ///获取Uri地址
            string appSettings = ConfigurationManager.AppSettings["elasticsearchUri"] == null ? "https://localhost:9200" : ConfigurationManager.AppSettings["elasticsearchUri"];

            ///进行分割
            var NodesUri = appSettings.Split(';');

            List<Uri> uris = new List<Uri>();

            ///进行Node组装
            for (int i = 0; i < NodesUri.Length; i++)
            {
                uris.Add(new Uri(NodesUri[i]));
            }
            
            ///进行连接池实例
            newstPool = new StaticConnectionPool(uris);

        }

        /// <summary>        
        /// Sets the cert policy.     
        /// </summary>  
        public  void SetCertificatePolicy()
        {
            ServicePointManager.ServerCertificateValidationCallback += RemoteCertificateValidate;
        }

        /// <summary>         
        /// Remotes the certificate validate.         
        /// </summary>        
        private  bool RemoteCertificateValidate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors error)
        {
            // trust any certificate!!!          
            System.Console.WriteLine("Warning, trust any certificate");
            return true;
        }

        /// <summary>
        /// 初始化API接口
        /// </summary>
        private void InitClient() {
            ConnectionSettings settings = new ConnectionSettings(newstPool);
            settings.DefaultIndex(ConfigurationManager.AppSettings["defaultIndex"].ToString());
            settings.BasicAuthentication(ConfigurationManager.AppSettings["user"].ToString(), ConfigurationManager.AppSettings["pwd"].ToString());
            elasticClient = new ElasticClient(settings);
        }
    }
}
