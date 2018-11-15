
/****************************************************************************
* 类名：AutoFacConfig
* 描述：将第三方的组件接管到程序
* 创建人：Author
* 创建时间：2018.05.04 
* 修改人;Author
* 修改时间：2018.09.18
* 修改描述：1、添加反射及读写分离的数据库操作
*           2、添加身份认证-jwt授权
* **************************************************************************
*/

using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using log4net;
using log4net.Config;
using Nest.Elasticsearch.Repository.IBase;
using Nest.Elasticsearch.Repository.Imp.Base;
//using AutoMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Web.Http;
using System.Web.Mvc;
using Workflow.comm;

namespace Workflow.Core.Config
{
    public static class AutoFacConfig
    {
        /// <summary>
        /// 接口注入的方法
        /// </summary>
        /// <param name="config"></param>
        public static void ApiBootstrpper(this HttpConfiguration config)
        {
            var builder = GetContainer();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(builder.Build());
        }

        /// <summary>
        /// 一般网站接口
        /// </summary>
        public static void Bootstrpper()
        {
            var builder = GetContainer();
            builder.RegisterControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));
        }

        /// <summary>
        /// 获取容器
        /// </summary>
        /// <returns></returns>
        private static ContainerBuilder GetContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(GetAssmenBlys())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            //builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();注册api控制器
            builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>))//单个注入
   .InstancePerDependency();
            return builder;
        }
        /// <summary>
        /// 返回需要依赖注入的接口
        /// </summary>
        /// <returns></returns>
        public static Assembly[] GetAssmenBlys()
        {
            IConfigFile con = new GeneralConfFileOperator();
            Assemblys assembly = new Assemblys();
            string path = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "Config\\Assemblys.xml");
            assembly = con.ReadConfFile<Assemblys>(path, false);
            //assembly.childs.Add("12312312");
            //assembly.childs.Add("123");
            //con.WriteConfFile(assembly, path, false, false);
            List<Assembly> assemblys = new List<Assembly>();
            if (assembly.childs.Count > 0)
            {
                foreach (var item in assembly.childs)
                {
                    assemblys.Add(Assembly.Load(item));
                }

            }
            return assemblys.ToArray();
        }

        /// <summary>
        /// 读取配置文件
        /// </summary>
        /// <returns></returns>
        //public static DBHelper GetDbBaseConnection()
        //{
        //    DBHelper dbConnection = new DBHelper();
        //    IConfigFile con = new GeneralConfFileOperator();
        //    string path = Path.Combine(Directory.GetCurrentDirectory(), "Config", "DBHelper.xml");
        //    dbConnection = con.ReadConfFile<DBHelper>(path, false);
        //    return dbConnection;
        //}

        ///// <summary>
        ///// 进行数据映射的三方插件引入
        ///// </summary>
        //public static void RegisterMappings()
        //{
        //    //获取所有IProfile实现类
        //    var allType =
        //    Assembly
        //       .GetEntryAssembly()//获取默认程序集
        //       .GetReferencedAssemblies()//获取所有引用程序集
        //       .Select(Assembly.Load)
        //       .SelectMany(y => y.DefinedTypes)
        //       .Where(type => typeof(ICommProfile).GetTypeInfo().IsAssignableFrom(type.AsType()));
        //    Mapper.Initialize(y =>
        //                       {
        //                           foreach (var typeInfo in allType)
        //                           {
        //                               var type = typeInfo.AsType();
        //                               if (type.Equals(typeof(ICommProfile)))
        //                               {
        //                                   //注册映射
        //                                   y.AddProfiles(type); // Initialise each Profile classe
        //                                   ///Mapper.AddProfile(Activator.CreateInstance(type) as Profile);
        //                               }
        //                           }
        //                       });
        //}

        //public static MapperConfiguration RegisterMappingConfig()
        //{
        //    //获取所有IProfile实现类
        //    var allType =
        //    Assembly
        //       .GetEntryAssembly()//获取默认程序集
        //       .GetReferencedAssemblies()//获取所有引用程序集
        //       .Select(Assembly.Load)
        //       .SelectMany(y => y.DefinedTypes)
        //       .Where(type => typeof(ICommProfile).GetTypeInfo().IsAssignableFrom(type.AsType()));
        //    return new MapperConfiguration(cfg =>
        //    {
        //        foreach (var typeInfo in allType)
        //        {
        //            var type = typeInfo.AsType();
        //            if (type.Equals(typeof(ICommProfile)))
        //            {
        //                //注册映射
        //                cfg.AddProfiles(type); // Initialise each Profile classe
        //                                       ///Mapper.AddProfile(Activator.CreateInstance(type) as Profile);
        //            }
        //        }
        //    });
        //}

        ///// <summary>
        ///// 获取生成Token的配置
        ///// </summary>
        ///// <returns></returns>
        //public static TokenHelper GetTokenHelper()
        //{
        //    TokenHelper tokenHelper = new TokenHelper();
        //    IConfigFile con = new GeneralConfFileOperator();
        //    string path = Path.Combine(Directory.GetCurrentDirectory(), "Config", "TokenHelper.xml");
        //    tokenHelper = con.ReadConfFile<TokenHelper>(path, false);
        //    return tokenHelper;
        //}
    }
}
