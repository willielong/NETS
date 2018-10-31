using Nest.Elasticsearch.Api.Entity;
using Nest.Elasticsearch.Repository.Imp.Base;
using Nest.Elasticserarh.Api.Client.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Nest.Elasticsearch.Core.Controllers
{
    public class ValuesController : ApiController
    {
       private readonly ElasticClient elasticClient = NestApiConnectionPool.Intence.elasticClient;
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
      
        public void Put()
        {
            Company company = new Company()
            {
                prekey = 2,
                enable = 1,
                caretor = "(willie)",
                crateDate = DateTime.Now,
                modifier = "(willie)",
                modifierDate = DateTime.Now,
                c_head = "(till)",
                head = "(gill)",
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
        }
    }
}
