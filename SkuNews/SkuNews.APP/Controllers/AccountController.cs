using SkuNews.APP.Models;
using SkuNews.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SkuNews.APP.Controllers
{
    public class AccountController : Controller
    {
        AdminLoginRepository adminLoginRepository = new AdminLoginRepository(); // نمونه سازی از درگاه ارتباطی با بانک اطلاعاتی

        public ActionResult Login() // اکشن مربوط به صفحه ی لاگین (get)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(AdminLoginViewModel login, string ReturnUrl = "/") // اکشن مربوط به لاگین (post)
        {
            if (ModelState.IsValid) // بررسی وارد کردن اطلاعات
            {
                if (adminLoginRepository.IsExistUser(login.UserName, login.Password)) // بررسی وجود کاربر با توجه به اطلاعات ورودی کاربر
                {
                    FormsAuthentication.SetAuthCookie(login.UserName, login.RememberMe); // احراز هویت و لاگین کاربر
                    return Redirect(ReturnUrl); // بازگشت به صفحه ی مورد نظر
                }
                else
                {
                    ModelState.AddModelError("UserName", "کاربری یافت نشد"); // در صورتی که کاربری با اطلاعات وارد شده پیدا نشود
                }
            }
            return View(login); // در صورتی که کاربر اطلاعات را وارد نکند
        }

        public ActionResult SignOut() // اکشن مربوط با خروج از حساب کاربری
        {
            FormsAuthentication.SignOut();
            return Redirect("/"); // بازشگت به صفحه ی اصلی
        }
    }
}