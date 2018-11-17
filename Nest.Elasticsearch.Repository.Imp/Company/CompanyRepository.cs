using Nest.Elasticsearch.Repository.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Elasticsearch.Repository.Imp.Company
{
    using Nest.Elasticsearch.Api.Entity;
    public class CompanyRepository : Base.BaseRepository<Company>, ICompanyRepository
    {
    }
}
