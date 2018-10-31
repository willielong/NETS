
using Elasticsearch.Net;
using Nest;
using Nest.Elasticsearch.Api.Entity;
using Nest.Elasticsearch.Repository.Imp.Base;
using Nest.Elasticserarh.Api.Client.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Net.Elatsticsearc.Demo
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("open....");
            ElasticClient elasticClient = NestApiConnectionPool.Intence.elasticClient;

            var person = new Person
            {
                FirstName = "Martijn",
                LastName = "Laarman"
            };
            //var ss = elasticClient.Search<Company>(s => s.Index(typeof(Company).Name.ToLower()).Type(typeof(Company).Name.ToLower()).Version());

            //if (!elasticClient.IndexExists("db_test").Exists)
            //{
            //    ICreateIndexResponse response = elasticClient.CreateIndex("db_test", p => p.InitializeUsing(new IndexState()
            //    {
            //        Settings = new IndexSettings()
            //        {
            //            NumberOfReplicas = 1,
            //            NumberOfShards = 5
            //        }
            //    }));
            //    Console.WriteLine(response.Acknowledged);

            //}
            //else
            //{
            //    IDeleteIndexResponse response = elasticClient.DeleteIndex("db_test");
            //    Console.WriteLine(response.Acknowledged);
            //}
            Company company = new Company()
            {
                prekey = 2,
                enable = 1,
                caretor = "文龙(willie)",
                crateDate = DateTime.Now,
                modifier = "文龙(willie)",
                modifierDate = DateTime.Now,
                c_head = "向东(tom)",
                head = "邵嵩(hill)",
                isTree = true,
                ognId = Guid.NewGuid().ToString(),
                parentId = "0",
                ognName = "测试公司01",
                virOgn = 0,
                sort = 1
            };
            // bool b= BaseRepository<Company>.Intences.CreateIndex(company);
            bool b = BaseRepository<Company>.Intences.Index<Company>(company);
            b = BaseRepository<Company>.Intences.SourceExists("1HlCv2YBNcxjthOD1TMi");
            b = BaseRepository<Company>.Intences.DocumentExists("1HlCv2YBNcxjthOD1TMi");
            Console.WriteLine(b);
            Console.ReadKey();
        }
    }

    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
