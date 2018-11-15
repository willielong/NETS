using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Elasticsearch.Repository.IBase
{
    public interface IBaseRepository<T> where T : class, new()
    {
        #region 创建-索引

        /// <summary>
        /// 创建默认的索引
        /// </summary>
        /// <returns></returns>
        bool CreateIndex(T data = null);

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
        bool CreateIndex(string _aliasesName, T data = null);

        /// <summary>
        /// 创建索引-其他类型
        /// <param name="_aliasesName">别名</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        bool CreateIndex<Tother>(string _aliasesName, T data = null) where Tother : class, new();

        /// <summary>
        /// 创建索引
        /// </summary>
        /// <param name="_NumberOfReplicas">副本数</param>
        /// <param name="_NumberOfShards">分片</param>
        /// <param name="_aliasesName">索引副本</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        bool CreateIndex(int? _NumberOfReplicas, int _NumberOfShards, string _aliasesName, T data = null);

        /// <summary>
        /// 创建索引-其他类型
        /// </summary>
        /// <param name="_NumberOfReplicas">副本数</param>
        /// <param name="_NumberOfShards">分片</param>
        /// <param name="_aliasesName">索引副本</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        bool CreateIndex<Tother>(int? _NumberOfReplicas, int _NumberOfShards, string _aliasesName, Tother data = null) where Tother : class, new();

        /// <summary>
        /// 重新配置字段
        /// </summary>
        /// <param name="indexName"></param>
        /// <param name="fileds"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        bool RestMapping(string indexName, string fileds, string type);

        /// <summary>
        /// 重新配置字段-其他类型
        /// </summary>
        /// <param name="indexName"></param>
        /// <param name="fileds"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        bool RestMapping<Tother>(string indexName, string fileds, string type) where Tother : class, new();

        #endregion

        #region 删除-索引

        /// <summary>
        /// 删除索引
        /// </summary>
        /// <returns></returns>
        bool DeleteIndex();

        /// <summary>
        /// 删除索引
        /// </summary>
        /// <returns></returns>
        bool DeleteIndex<Tother>() where Tother : class, new();


        #endregion

        #region 进行数据操作
        /// <summary>
        /// 批量添加数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Index(List<T> data);

        /// <summary>
        /// 批量添加数据-其他类型
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Index<Tother>(List<Tother> data) where Tother : class, new();

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Index(T data);

        /// <summary>
        /// 添加数据-其他类型
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Index<Tother>(Tother data) where Tother : class, new();

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(T data, string elasticsearchKey = null);

        /// <summary>
        /// 根据条件进行编辑数据-其他类型
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        bool UpdateQuery<TOther>(Func<UpdateByQueryDescriptor<TOther>, IUpdateByQueryRequest> selector) where TOther : class, new();
        /// <summary>
        /// 更新数据-其他类型
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update<TOther>(TOther data, string elasticsearchKey = null) where TOther : class, new();

        /// <summary>
        /// 根据条件进行编辑数据
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        bool UpdateQuery(Func<UpdateByQueryDescriptor<T>, IUpdateByQueryRequest> selector);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="prkey">要删除数据的键值</param>
        /// <returns></returns>
        bool Delete(object prkey);

        /// <summary>
        /// 根据条件完成删除
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        bool DeleteByQuery(Func<DeleteByQueryDescriptor<T>, IDeleteByQueryRequest> func);

        /// <summary>
        /// 删除数据-其他类型
        /// </summary>
        /// <param name="prkey">要删除数据的键值</param>
        /// <returns></returns>
        bool Delete<TOther>(object prkey) where TOther : class, new();

        /// <summary>
        /// 根据条件完成删除
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        bool DeleteByQuery<TOther>(Func<DeleteByQueryDescriptor<TOther>, IDeleteByQueryRequest> func) where TOther : class, new();

        #endregion

        #region 检查elastic属性是否存在的方法块

        /// <summary>
        /// 判断索引是否存在
        /// </summary>
        /// <returns></returns>
        bool IndexExist();

        /// <summary>
        /// 判断索引是否存在-其他类型
        /// </summary>
        /// <returns></returns>
        bool IndexExist<Tother>() where Tother : class, new();

        /// <summary>
        /// 判断索引中文档的类型名称
        /// </summary>
        /// <returns></returns>
        bool TypeExist();
        /// <summary>
        /// 判断索引中文档的类型名称-其他类型
        /// </summary>
        /// <returns></returns>
        bool TypeExist<Tother>() where Tother : class, new();

        /// <summary>
        /// 判断别名是否存在
        /// </summary>
        /// <param name="aliasName"></param>
        /// <returns></returns>
        bool AliasExists(string aliasName);

        /// <summary>
        /// 判断文档是否存在
        /// </summary>
        /// <param name="prekey"></param>
        /// <returns></returns>
        bool DocumentExists(object prekey);

        /// <summary>
        /// 判断文档源文件是否存在
        /// </summary>
        /// <param name="prekey"></param>
        /// <returns></returns>
        bool SourceExists(object prekey);

        #endregion
    }
}
