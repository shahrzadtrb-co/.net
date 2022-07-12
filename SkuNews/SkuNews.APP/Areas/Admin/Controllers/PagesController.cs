using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SkuNews.Data;
using SkuNews.Data.Services;

namespace SkuNews.APP.Areas.Admin.Controllers
{
    [Authorize]
    public class PagesController : Controller
    {
        PagesRepository pagesRepository = new PagesRepository();
        private SkuNews_DBEntities db = new SkuNews_DBEntities();

        // GET: Admin/Pages
        public ActionResult Index()
        {
            return View(pagesRepository.GetAllPage());
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: Admin/Pages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Pages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PageID,Title,Description,Text,visit,ImageName,ShowInSlider,CreateDate")] Page pages, HttpPostedFileBase imgUp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (imgUp != null)
                    {
                        pages.ImageName = Guid.NewGuid() + Path.GetExtension(imgUp.FileName);
                        imgUp.SaveAs(Server.MapPath("/PageImages/" + pages.ImageName));
                    }
                    pages.CreateDate = DateTime.Now;
                    pages.visit = 0;
                    db.Pages.Add(pages);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return RedirectToAction("Error");
            }
            return View(pages);
        }

        // GET: Admin/Pages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page pages = db.Pages.Find(id);
            if (pages == null)
            {
                return HttpNotFound();
            }
            return View(pages);
        }

        // POST: Admin/Pages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PageID,Title,Description,Text,visit,ImageName,ShowInSlider,CreateDate")] Page pages, HttpPostedFileBase imgUp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (imgUp != null)
                    {
                        if (pages.ImageName != null)
                        {
                            System.IO.File.Delete(Server.MapPath("/PageImages/" + pages.ImageName));
                        }
                        pages.ImageName = Guid.NewGuid() + Path.GetExtension(imgUp.FileName);
                        imgUp.SaveAs(Server.MapPath("/PageImages/" + pages.ImageName));
                    }
                    db.Entry(pages).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return RedirectToAction("Error");
            }
            return View(pages);
        }

        // GET: Admin/Pages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page pages = db.Pages.Find(id);
            if (pages == null)
            {
                return HttpNotFound();
            }
            return View(pages);
        }

        // POST: Admin/Pages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Page pages = db.Pages.Find(id);
            if (pages.ImageName != null)
            {
                System.IO.File.Delete(Server.MapPath("/PageImages/" + pages.ImageName));
            }
            db.Pages.Remove(pages);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
