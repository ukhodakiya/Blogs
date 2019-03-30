using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogPost.Core
{
    public  class UserAuthentication
    {
        private readonly HttpContextBase _HtttpContextbase;

        public UserAuthentication(HttpContextBase HtttpContextbase)
        {
            this._HtttpContextbase = HtttpContextbase;
        }

        public  bool Authentication()
        {
            BlogPostContext _context = new BlogPostContext();

            bool isAuthenticated = false;

            //var User = HttpContext.Current.Request.Cookies["UserId"];

            //if (User != null)
            //{
            //    if(!string.IsNullOrEmpty(User.Value))
            //    {
            //        var ExistUser = _context.User.Find(Convert.ToInt32(User.Value));

            //        if (ExistUser != null)
            //            isAuthenticated = true;
            //    }            
            //}

            if(_HtttpContextbase.Session["USerId"] != null)
            {
                if(!string.IsNullOrEmpty(_HtttpContextbase.Session["USerId"].ToString()))
                {
                    var ExistUser = _context.User.Find(Convert.ToInt32(_HtttpContextbase.Session["USerId"]));

                    if (ExistUser != null)
                        isAuthenticated = true;
                }        
            }
            return isAuthenticated;
        }


        public void AuthenticationExpire()
        {
            BlogPostContext _context = new BlogPostContext();

        

            var User = HttpContext.Current.Request.Cookies["UserId"];

            if (User != null)
            {
                HttpCookie myCookie = new HttpCookie("UserId");
                //myCookie.Expires = DateTime.Now.AddDays(-1d);
                HttpContext.Current.Response.Cookies.Remove("UserId");
                myCookie.Expires = DateTime.Now.AddDays(-1);
                //HttpContext.Current.Response.Cookies["UserId"].Expires = DateTime.Now.AddDays(-1);
            }


           
        }

    }
}