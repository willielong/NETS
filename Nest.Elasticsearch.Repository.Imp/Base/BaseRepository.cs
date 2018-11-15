using Nest.Elasticsearch.Api.Entity;
using Nest.Elasticsearch.Repository.IBase;
using Nest.Elasticserarh.Api.Client.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Elasticsearch.Repository.Imp.Base
{
    public class BaseRepository<T>: IBaseRepository<T> where T : class, new()
    {
        public IElasticClient _client = NestApiConnectionPool.Intence.elasticClient;

        #region 创建-索引

        /// <summary>
        /// 创建默认的索引
        /// </summary>
        /// <returns></returns>
        public virtual bool CreateIndex(T data = null)
        {
            return CreateIndex(1, 5, (typeof(T).Name), data);
        }

        /// <summary>
        /// 创建默认索引其他类型
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual bool CreateIndex<Tother>(Tother data = null) where Tother : class, new()
        {
            return CreateIndex<Tother>(1, 5, (typeof(Tother).Name), data);
        }

        /// <summary>
        /// 创建索引
        /// <param name="_aliasesName">别名</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public virtual bool CreateIndex(string _aliasesName, T data = null)
        {
            return CreateIndex(1, 5, _aliasesName, data);
        }

        /// <summary>
        /// 创建索引-其他类型
        /// <param name="_aliasesName">别名</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public virtual bool CreateIndex<Tother>(string _aliasesName, T data = null) where Tother : class, new()
        {
            return CreateIndex(1, 5, _aliasesName, data);
        }

        /// <summary>
        /// 创建索引
        /// </summary>
        /// <param name="_NumberOfReplicas">副本数</param>
        /// <param name="_NumberOfShards">分片</param>
        /// <param name="_aliasesName">索引副本</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public virtual bool CreateIndex(int? _NumberOfReplicas, int _NumberOfShards, string _aliasesName, T data = null)
        {
            bool b = false;
            try
            {
                if (!_client.TypeExists(typeof(T).Name.ToLower(), Types.Parse(typeof(T).Name.ToLower())).Exists)
                {
                    IndexState indexState = new IndexState
                    {
                        Settings = new IndexSettings()
                        {
                            NumberOfReplicas = _NumberOfReplicas, //副本数
                            NumberOfShards = _NumberOfShards, //分片数
                        }
                    };

                    ///进行数据创建
                    ICreateIndexResponse response = _client.CreateIndex(typeof(T).Name.ToLower(),
                         us => us.InitializeUsing(indexState).Mappings(mp => mp.Map<T>(mps => mps.AutoMap()))
                         .Aliases(Alias => Alias.Alias(_aliasesName)));
                    b = response.Acknowledged;
                    if (data != null && b)
                    {
                        b = Index(data);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return b;
        }

        /// <summary>
        /// 创建索引-其他类型
        /// </summary>
        /// <param name="_NumberOfReplicas">副本数</param>
        /// <param name="_NumberOfShards">分片</param>
        /// <param name="_aliasesName">索引副本</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public virtual bool CreateIndex<Tother>(int? _NumberOfReplicas, int _NumberOfShards, string _aliasesName, Tother data = null) where Tother : class, new()
        {
            bool b = false;
            try
            {
                if (!_client.TypeExists(typeof(Tother).Name.ToLower(), Types.Parse(typeof(Tother).Name.ToLower())).Exists)
                {
                    IndexState indexState = new IndexState
                    {
                        Settings = new IndexSettings()
                        {
                            NumberOfReplicas = _NumberOfReplicas, //副本数
                            NumberOfShards = _NumberOfShards, //分片数
                        }
                    };

                    ///进行数据创建
                    ICreateIndexResponse response = _client.CreateIndex(typeof(T).Name.ToLower(),
                         us => us.InitializeUsing(indexState).Mappings(mp => mp.Map<T>(mps => mps.AutoMap()))
                         .Aliases(Alias => Alias.Alias(_aliasesName)));
                    b = response.Acknowledged;
                    if (data != null && b)
                    {
                        b = Index(data);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return b;
        }

        /// <summary>
        /// 重新配置字段
        /// </summary>
        /// <param name="indexName"></param>
        /// <param name="fileds"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public virtual bool RestMapping(string indexName, string fileds, string type)
        {
            //新增字段
            IPutMappingResponse result = new PutMappingResponse();
            switch (type.ToLower())
            {
                case "text":
                    {
                        result = _client.Map<T>(m => m
            .Index(indexName)
            .Properties(p => p
                    .Text(s => s
                        .Name(fileds)
                        .Index(false).Analyzer("ik_max_word"))));
                    }
                    break;
                case "number":
                    {
                        result = _client.Map<T>(m => m
            .Index(indexName)
            .Properties(p => p
                    .Number(s => s
                        .Name(fileds)
                        .Index(false))));
                    }
                    break;
                case "date":
                    {
                        result = _client.Map<T>(m => m
            .Index(indexName)
            .Properties(p => p
                    .Date(s => s
                        .Name(fileds)
                        .Index(false))));
                    }
                    break;
                case "Keyword":
                    {
                        result = _client.Map<T>(m => m
           .Index(indexName)
           .Properties(p => p.Keyword(s => s
                            .Name(fileds)
                            .Index(true))));
                    }
                    break;
            }
            return result.Acknowledged;
        }

        /// <summary>
        /// 重新配置字段-其他类型
        /// </summary>
        /// <param name="indexName"></param>
        /// <param name="fileds"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public virtual bool RestMapping<Tother>(string indexName, string fileds, string type) where Tother : class, new()
        {
            //新增字段
            IPutMappingResponse result = new PutMappingResponse();
            switch (type.ToLower())
            {
                case "text":
                    {
                        result = _client.Map<Tother>(m => m
            .Index(indexName)
            .Properties(p => p
                    .Text(s => s
                        .Name(fileds)
                        .Index(false).Analyzer("ik_max_word"))));
                    }
                    break;
                case "number":
                    {
                        result = _client.Map<Tother>(m => m
            .Index(indexName)
            .Properties(p => p
                    .Number(s => s
                        .Name(fileds)
                        .Index(false))));
                    }
                    break;
                case "date":
                    {
                        result = _client.Map<Tother>(m => m
            .Index(indexName)
            .Properties(p => p
                    .Date(s => s
                        .Name(fileds)
                        .Index(false))));
                    }
                    break;
                case "Keyword":
                    {
                        result = _client.Map<Tother>(m => m
           .Index(indexName)
           .Properties(p => p.Keyword(s => s
                            .Name(fileds)
                            .Index(true))));
                    }
                    break;
            }
            return result.Acknowledged;
        }

        #endregion

        #region 删除-索引

        /// <summary>
        /// 删除索引
        /// </summary>
        /// <returns></returns>
        public virtual bool DeleteIndex()
        {

            bool b = false;
            try
            {
                IDeleteIndexResponse response = _client.DeleteIndex(typeof(T).Name.ToLower());
                b = response.ApiCall.Success;
            }
            catch (Exception)
            {
                throw;
            }
            return b;
        }

        /// <summary>
        /// 删除索引
        /// </summary>
        /// <returns></returns>
        public virtual bool DeleteIndex<Tother>() where Tother : class, new()
        {

            bool b = false;
            try
            {
                IDeleteIndexResponse response = _client.DeleteIndex(typeof(Tother).Name.ToLower());
                b = response.ApiCall.Success;
            }
            catch (Exception)
            {
                throw;
            }
            return b;
        }


        #endregion

        #region 进行数据操作
        /// <summary>
        /// 批量添加数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual bool Index(List<T> data)
        {
            bool b = false;
            try
            {
                IBulkResponse response = _client.IndexMany(data, typeof(T).Name.ToLower(), typeof(T).Name.ToLower());
                b = response.ApiCall.Success;
            }
            catch (Exception)
            {
                throw;
            }
            return b;
        }

        /// <summary>
        /// 批量添加数据-其他类型
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual bool Index<Tother>(List<Tother> data) where Tother : class, new()
        {
            bool b = false;
            try
            {
                IBulkResponse response = _client.IndexMany(data, typeof(Tother).Name.ToLower(), typeof(Tother).Name.ToLower());
                b = response.ApiCall.Success;
            }
            catch (Exception)
            {
                throw;
            }
            return b;
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual bool Index(T data)
        {
            bool b = false;
            try
            {
                IIndexResponse response = _client.Index(data, dt => dt.Index(typeof(T).Name.ToLower()).Type(typeof(T).Name.ToLower()));
                b = response.ApiCall.Success;
            }
            catch (Exception)
            {
                throw;
            }
            return b;
        }

        /// <summary>
        /// 添加数据-其他类型
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual bool Index<Tother>(Tother data) where Tother : class, new()
        {
            bool b = false;
            try
            {
                IIndexResponse response = _client.Index(data, dt => dt.Index(typeof(Tother).Name.ToLower()).Type(typeof(Tother).Name.ToLower()));
                b = response.ApiCall.Success;
            }
            catch (Exception)
            {
                throw;
            }
            return b;
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual bool Update(T data, string elasticsearchKey = null)
        {
            bool b = false;
            try
            {

                DocumentPath<T> documentPath;
                if (string.IsNullOrEmpty(elasticsearchKey))
                {
                    documentPath = new DocumentPath<T>(data);
                }
                else
                {
                    documentPath = new DocumentPath<T>(elasticsearchKey);
                }
                var updateResponse = _client.Update(documentPath, p => p.Index(typeof(T).Name).Doc(data)).ApiCall.Success;
                b = true;
            }
            catch (Exception)
            {

                throw;
            }
            return b;
        }

        /// <summary>
        /// 根据条件进行编辑数据-其他类型
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        public virtual bool UpdateQuery<TOther>(Func<UpdateByQueryDescriptor<TOther>, IUpdateByQueryRequest> selector) where TOther : class, new()
        {
            return _client.UpdateByQuery<TOther>(selector).ApiCall.Success;
        }

        /// <summary>
        /// 更新数据-其他类型
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual bool Update<TOther>(TOther data, string elasticsearchKey = null) where TOther : class, new()
        {
            bool b = false;
            try
            {

                DocumentPath<TOther> documentPath;
                if (string.IsNullOrEmpty(elasticsearchKey))
                {
                    documentPath = new DocumentPath<TOther>(data);
                }
                else
                {
                    documentPath = new DocumentPath<TOther>(elasticsearchKey);
                }
                var updateResponse = _client.Update(documentPath, p => p.Index(typeof(TOther).Name).Doc(data)).ApiCall.Success;
                b = true;
            }
            catch (Exception)
            {

                throw;
            }
            return b;
        }

        /// <summary>
        /// 根据条件进行编辑数据
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        public virtual bool UpdateQuery(Func<UpdateByQueryDescriptor<T>, IUpdateByQueryRequest> selector)
        {
            return _client.UpdateByQuery<T>(selector).ApiCall.Success;
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="prkey">要删除数据的键值</param>
        /// <returns></returns>
        public virtual bool Delete(object prkey)
        {
            bool b = false;
            try
            {
                if (DocumentExists(prkey))
                {
                    DocumentPath<T> documentPath = new DocumentPath<T>(prkey.ToString());
                    b = _client.Delete(documentPath, s => s.Index(typeof(T).Name).Type(typeof(T).Name)).ApiCall.Success;
                }
                else { b = false; }
                // b = true;
            }
            catch (Exception)
            {
                throw;
            }
            return b;
        }

        /// <summary>
        /// 根据条件完成删除
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public virtual bool DeleteByQuery(Func<DeleteByQueryDescriptor<T>, IDeleteByQueryRequest> func)
        {
            return _client.DeleteByQuery<T>(func).ApiCall.Success;
        }

        /// <summary>
        /// 删除数据-其他类型
        /// </summary>
        /// <param name="prkey">要删除数据的键值</param>
        /// <returns></returns>
        public virtual bool Delete<TOther>(object prkey) where TOther : class, new()
        {
            bool b = false;
            try
            {
                if (DocumentExists(prkey))
                {
                    DocumentPath<TOther> documentPath = new DocumentPath<TOther>(prkey.ToString());
                    b = _client.Delete(documentPath, s => s.Index(typeof(TOther).Name).Type(typeof(TOther).Name)).ApiCall.Success;
                }
                else { b = false; }
                // b = true;
            }
            catch (Exception)
            {
                throw;
            }
            return b;
        }

        /// <summary>
        /// 根据条件完成删除
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public virtual bool DeleteByQuery<TOther>(Func<DeleteByQueryDescriptor<TOther>, IDeleteByQueryRequest> func) where TOther : class, new()
        {
            return _client.DeleteByQuery<TOther>(func).ApiCall.Success;
        }

        #endregion

        #region 检查elastic属性是否存在的方法块

        /// <summary>
        /// 判断索引是否存在
        /// </summary>
        /// <returns></returns>
        public virtual bool IndexExist()
        {
            return _client.IndexExists(typeof(T).Name).Exists;
        }

        /// <summary>
        /// 判断索引是否存在-其他类型
        /// </summary>
        /// <returns></returns>
        public virtual bool IndexExist<Tother>() where Tother : class, new()
        {
            return _client.IndexExists(typeof(Tother).Name).Exists;
        }

        /// <summary>
        /// 判断索引中文档的类型名称
        /// </summary>
        /// <returns></returns>
        public virtual bool TypeExist()
        {
            return _client.TypeExists(typeof(T).Name.ToLower(), Types.Parse(typeof(T).Name.ToLower())).Exists;
        }

        /// <summary>
        /// 判断索引中文档的类型名称-其他类型
        /// </summary>
        /// <returns></returns>
        public virtual bool TypeExist<Tother>() where Tother : class, new()
        {
            return _client.TypeExists(typeof(Tother).Name.ToLower(), Types.Parse(typeof(Tother).Name.ToLower())).Exists;
        }

        /// <summary>
        /// 判断别名是否存在
        /// </summary>
        /// <param name="aliasName"></param>
        /// <returns></returns>
        public virtual bool AliasExists(string aliasName)
        {
            return _client.AliasExists(aliasName).Exists;
            //_client.DocumentExists()
        }

        /// <summary>
        /// 判断文档是否存在
        /// </summary>
        /// <param name="prekey"></param>
        /// <returns></returns>
        public virtual bool DocumentExists(object prekey)
        {
            DocumentPath<T> documentPath = new DocumentPath<T>(prekey.ToString());
            return _client.DocumentExists(documentPath, s => s.Index(typeof(T).Name)).Exists;
        }

        /// <summary>
        /// 判断文档源文件是否存在
        /// </summary>
        /// <param name="prekey"></param>
        /// <returns></returns>
        public virtual bool SourceExists(object prekey)
        {
            DocumentPath<T> documentPath = new DocumentPath<T>(prekey.ToString());
            return _client.SourceExists(documentPath, s => s.Index(typeof(T).Name)).Exists;
            //_client.UpdateByQuery()
        }

        #endregion

        #region 更新数据

        #endregion
    }
}
