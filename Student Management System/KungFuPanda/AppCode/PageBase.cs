using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KungFuPanda
{
    public class PageBase: System.Web.UI.Page
    {
        public static string getServerURL()
        {
            return HttpContext.Current.Request.Url.Authority;
            
        }
    }
}