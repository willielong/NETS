using Nest.Elasticsearch.Api.Entity;
using Nest.Elasticsearch.Repository.Imp.Base;
using Nest.Elasticserarh.Api.Client.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Elasticsearch.Test
{
 
    class Program
    {
        public static ElasticClient elasticClient = NestApiConnectionPool.Intence.elasticClient;
        public static BaseRepository<Company> repository = BaseRepository<Company>.Intences;
        static void Main(string[] args)
        {
            //repository.Update();
        }
    }
}
