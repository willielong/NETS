using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Elasticsearch.Api.Entity
{
    public interface IEntiytBase
    { /// <summary>
      /// 主键
      /// </summary>
        [Number(NumberType.Long, Name = "prekey")]
        long prekey { get; set; }

        /// <summary>
        /// 是否禁用和启用
        /// </summary>
        [Number(NumberType.Integer, Name = "enable")]
        int enable { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [Text(Name = "caretor", Index = true, Analyzer = "ik_max_word")]
        string caretor { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Date(Name = "crateDate")]
        DateTime crateDate { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        [Text(Name = "modifier", Index = true, Analyzer = "ik_max_word")]
        string modifier { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [Date(Name = "modifierDate")]
        DateTime modifierDate { get; set; }
    }
}
