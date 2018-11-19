using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nest.Elasticsearch.Api.Entity;
using Nest.Elasticsearch.Services;

namespace Nest.Elasticsearch.Core.Controllers
{
    public class HomeController : Controller
    {
        public ICompanyServices services { get; set; }
        public IEntiytBase entiytBase { get; set; }
        //public HomeController(ICompanyServices _services)
        //{
        //    services = _services;
        //}
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            services.IndexExist();
            return View();
        }
    }
}
