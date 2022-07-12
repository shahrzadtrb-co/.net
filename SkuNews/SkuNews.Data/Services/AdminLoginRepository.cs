using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkuNews.Data.Services
{
    public class AdminLoginRepository
    {
        SkuNews_DBEntities db = new SkuNews_DBEntities(); // نمونه از بانک اطلاعاتی

        public bool IsExistUser(string username, string password) // بررسی وجود کاربر
        {
            return db.AdminLogins.Any(u => u.UserName == username && u.Password == password);
        }
    }
}
