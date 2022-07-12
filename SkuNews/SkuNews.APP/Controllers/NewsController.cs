using SkuNews.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SkuNews.APP.Controllers
{
    public class NewsController : Controller
    {
        public PagesRepository pageRepository = new PagesRepository(); // نمونه سازی از درگاه ارتباطی با بانک اطلاعاتی

        public ActionResult TopNews() // اکشن مربوط به پر بازدید ترین اخبار
        {
            return PartialView(pageRepository.TopNews());
        }

        [Route("Archive")]
        public ActionResult ArchiveNews() // اکشن مربوط به ساخت آرشیو اخبار
        {
            return View(pageRepository.GetAllPage());
        }

        [Route("News/{id}")]
        public ActionResult ShowNews(int id) // اکشن مربوط به نمایش خبر بر اساس شناسه
        {
            var news = pageRepository.GetPageById(id); // پیدا کردن خبر بر اساس شناسه

            if (news == null) // بررسی وجود خبر
            {
                return HttpNotFound();
            }

            news.visit += 1; // اضافه کردن بازدید خبر
            pageRepository.UpdatePage(news); // ویرایش خبر
            pageRepository.Save(); // ذخیره تغییرات

            return View(news); // نمایش خبر
        }
    }
}