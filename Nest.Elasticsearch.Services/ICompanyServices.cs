using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Elasticsearch.Services
{
    public interface ICompanyServices
    {

        /// <summary>
        /// 判断索引是否存在
        /// </summary>
        /// <returns></returns>
        bool IndexExist();
    }
}
