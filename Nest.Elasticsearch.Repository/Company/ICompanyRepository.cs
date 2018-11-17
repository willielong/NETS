
using Nest.Elasticsearch.Repository.IBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Elasticsearch.Repository.Company
{
    using Nest.Elasticsearch.Api.Entity;

    public interface ICompanyRepository:IBaseRepository<Company>
    {
    }
}
