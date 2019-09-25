using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Workflow.Core.Config;

namespace Nest.Elasticsearch.Core
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();

            ///接口上注册
            config.ApiBootstrpper();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
