using Nest.Elasticsearch.Api.Entity;
using Nest.Elasticsearch.Repository.Imp.Base;
using Nest.Elasticserarh.Api.Client.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Elasticsearch.Test
{

    class Program
    {
        //public static ElasticClient elasticClient = NestApiConnectionPool.Intence.elasticClient;
        //public static BaseRepository<Company> repository = BaseRepository<Company>.Intences;
        static void Main(string[] args)
        {
            //repository.Update();
            new test1().sage<DeviceType>(1);
        }
    }

    public class test1
    {

        public void sage<T>(int? sum) 
        {
            var type = typeof(T);
            var ts = type.ToString();
          var ss=  Enum.GetValues(typeof(T)).Cast<T>().ToList();
        }
    }
    public enum EnumAppendItemType
    {
        None = -999,
        [Description("--所有--")]
        All = 1,
        [Description("--请选择--")]
        Select = 2,
    }
    ///
    　　/// 访问设备
    　　 ///
    public enum DeviceType
    {
        [Description("PC")]
        PC = 1,
        [Description("移动端")]
        Mobile = 2
    }
}
