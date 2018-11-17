using Nest.Elasticsearch.Api.Entity;
using Nest.Elasticsearch.Services;
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
        //private readonly ElasticClient elasticClient = NestApiConnectionPool.Intence.elasticClient;

        public ICompanyServices services { get; set; }
        //public ValuesController(IBaseRepository<Company> _baseRepository)
        //{
        //    baseRepository = _baseRepository;
        //}
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

        [HttpGet]
        public bool Put1()
        {
            Company company = new Company()
            {
                prekey = 3,
                enable = 1,
                caretor = "(helie)",
                crateDate = DateTime.Now,
                modifier = "(helie)",
                modifierDate = DateTime.Now,
                c_head = "(till)",
                head = "(gill)",
                isTree = true,
                ognId = Guid.NewGuid().ToString(),
                parentId = "0",
                ognName = "测试公司03",
                virOgn = 0,
                sort = 2
            };
            // bool b= BaseRepository<Company>.Intences.CreateIndex(company);
            //bool b = baseRepository.Index<Company>(company);
            //b = baseRepository.SourceExists("1HlCv2YBNcxjthOD1TMi");
            //b = baseRepository.DocumentExists("1HlCv2YBNcxjthOD1TMi");
            return true;
        }
    }
}
