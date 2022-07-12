using SkuNews.Data;
using SkuNews.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SkuNews.APP.Controllers
{
    public class HomeController : Controller
    {
        public PagesRepository pageRepository = new PagesRepository(); // نمونه سازی از درگاه ارتباطی با بانک اطلاعاتی

        public ActionResult Index() // اکشن مربوط به ساخت صفحه ی اصلی سایت
        {
            return View();
        }

        public ActionResult Slider() // اکشن مربوط به ساختن اسلایدر سایت
        {
            return PartialView(pageRepository.PagesInSlider());
        }

    }
}