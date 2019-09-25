using Nest.Elasticsearch.Api.Entity;
using Nest.Elasticsearch.Repository.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Elasticsearch.Services.Imp
{
    public class CmopanyServices : ICompanyServices
    {

        public ICompanyRepository repository { get; set; }

        public bool CreateIndex<Tother>(Tother data = null) where Tother : class, new()
        {
            return repository.CreateIndex<Tother>(data);
        }

        public bool CreateIndex(string _aliasesName, Company data = null)
        {
            return repository.CreateIndex(_aliasesName, data);
        }

        //public CmopanyServices(ICompanyRepository _repository)
        //{
        //    repository = _repository;
        //}

        public bool IndexExist()
        {
            return repository.IndexExist();
        }
    }
}
