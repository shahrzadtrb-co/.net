using SkuNews.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SkuNews.APP.Controllers
{
    public class SearchController : Controller
    {
        public PagesRepository pageRepository = new PagesRepository(); // نمونه سازی از درگاه ارتباطی با بانک اطلاعاتی

        public ActionResult Index(string q) // اکشن مربوط به قسمت جستجو
        {
            ViewBag.Name = q; // ارسال عبارت مورد نظر به ویو
            return View(pageRepository.SearchPage(q)); // نمایش نتایج جستجو
        }
    }
}