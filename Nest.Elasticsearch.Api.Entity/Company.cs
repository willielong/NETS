using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Elasticsearch.Api.Entity
{
    [ElasticsearchType(Name = "company")]
    public class Company : EntiytBase
    {

       
        /// <summary>
        /// 组织编号
        /// </summary>
        [Keyword(Name = "ognId", Index = true)]
        public string ognId { get; set; }

        /// <summary>
        /// 组织名称
        /// </summary>
        [Text(Name = "ognName", Index = true, Analyzer = "ik_max_word")]
        public string ognName { get; set; }

        /// <summary>
        /// 父级组织的编号
        /// </summary>
        [Keyword(Name = "parentId", Index = true)]
        public string parentId { get; set; }

        /// <summary>
        /// 单位领导
        /// </summary>
        [Text(Name = "head", Index = true, Analyzer = "ik_max_word")]
        public string head { get; set; }

        /// <summary>
        /// 单位委领导
        /// </summary>
        [Text(Name = "c_head", Index = true, Analyzer = "ik_max_word")]
        public string c_head { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Number(NumberType.Integer, Name = "sort")]
        public int sort { get; set; }

        /// <summary>
        /// 是否是虚拟组织
        /// </summary>
        [Number(NumberType.Integer, Name = "virOgn")]
        public int virOgn { get; set; }
        /// <summary>
        /// 是否是虚拟组织
        /// </summary>
        [Boolean(Name = "isTree", Index = true)]
        public bool isTree { get; set; }
    }
}
