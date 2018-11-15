using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nest.Elasticsearch.Api.Entity;
using Nest.Elasticsearch.Repository.IBase;

namespace Nest.Elasticsearch.Core.Controllers
{
    public class HomeController : Controller
    {
        public IBaseRepository<Company> baseRepository { get; set; }
        //public HomeController()
        //{
        //    //baseRepository = _baseRepository;
        //}
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return View();
        }
    }
}
