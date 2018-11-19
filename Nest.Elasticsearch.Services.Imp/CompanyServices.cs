using Nest.Elasticsearch.Repository.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Elasticsearch.Services.Imp
{
    public class CmopanyServices: ICompanyServices
    {

        public ICompanyRepository repository { get; set; }
        //public CmopanyServices(ICompanyRepository _repository)
        //{
        //    repository = _repository;
        //}

        public bool IndexExist()
        {
          return  repository.IndexExist();
        }
    }
}
