using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SkuNews.Data.Services
{
    public class PagesRepository
    {
        SkuNews_DBEntities db= new SkuNews_DBEntities(); // نمونه از بانک اطلاعاتی

        public IEnumerable<Page> GetAllPage() // نمایش همه ی خبر ها
        {
            return db.Pages;
        }

        public Page GetPageById(int pageId) // پیدا کردن یک خبر با شناسه ی مشخص
        {
            return db.Pages.Find(pageId);
        }

        public bool InsertPage(Page page) // اضافه کردن خبر
        {
            try
            {
                db.Pages.Add(page);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        public bool UpdatePage(Page page) // ویرایش یک خبر
        {
            try
            {
                db.Entry(page).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        public bool DeletePage(Page page) // حذف یک خبر 
        {
            try
            {
                db.Entry(page).State = EntityState.Deleted;
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        public bool DeletePage(int pageId) // حذف یک خبر با استفاده از شناسه
        {
            try
            {
                var page = GetPageById(pageId);
                DeletePage(page);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        public void Save() // ذخیره ی تغییرات
        {
            db.SaveChanges();
        }

        public IEnumerable<Page> TopNews(int take = 4) // پربازدید ترین مطالب
        {
            return db.Pages.OrderByDescending(p => p.visit).Take(take);
        }

        public IEnumerable<Page> PagesInSlider() // نمایش در اسلایدر
        {
            return db.Pages.Where((System.Linq.Expressions.Expression<Func<Page, bool>>)(p => p.ShowInSlider == true));
        }

        //public IEnumerable<Page> LastNews(int take = 4) // نمایش آخرین اخبار
        //{
        //    return db.Pages.OrderByDescending(p => p.CreateDate).Take(take);
        //}

        public IEnumerable<Page> SearchPage(string search) // جستجو
        {
            return db.Pages.Where(p => p.Title.Contains(search) || p.Description.Contains(search) || p.Text.Contains(search)).Distinct();
        }
    }
}
