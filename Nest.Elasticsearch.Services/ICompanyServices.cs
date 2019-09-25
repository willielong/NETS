
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Elasticsearch.Services
{
    using Nest.Elasticsearch.Api.Entity;

    public interface ICompanyServices
    {

        /// <summary>
        /// 判断索引是否存在
        /// </summary>
        /// <returns></returns>
        bool IndexExist();


        /// <summary>
        /// 创建默认索引其他类型
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool CreateIndex<Tother>(Tother data = null) where Tother : class, new();

        /// <summary>
        /// 创建索引
        /// <param name="_aliasesName">别名</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        bool CreateIndex(string _aliasesName, Company data = null);
    }
}
