using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BlogPost
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                         name: "Blog",
                         url: "Register",
                         defaults: new { controller = "Home", action = "Register", id = UrlParameter.Optional }
                     );


            routes.MapRoute(
                       name: "Login",
                       url: "Login",
                       defaults: new { controller = "Home", action = "Login" }
                   );

            routes.MapRoute(
                   name: "Logout",
                   url: "Logout",
                   defaults: new { controller = "Home", action = "Logout" }
               );

            routes.MapRoute(
                 name: "BlogPost",
                 url: "Blog",
                 defaults: new { controller = "Home", action = "BlogPost" }
             );

            routes.MapRoute(
              name: "AllBlogs",
              url: "Blogs",
              defaults: new { controller = "Home", action = "AllBlogs" }
          );

            routes.MapRoute(
           name: "ForgotPassword",
           url: "forgotpassword",
           defaults: new { controller = "Home", action = "ForgotPassword" }
       );

            routes.MapRoute(
       name: "UserList",
       url: "UserList",
       defaults: new { controller = "Home", action = "UserList" }
   );
            routes.MapRoute(
      name: "TreeView",
      url: "TreeView",
      defaults: new { controller = "Home", action = "TreeView" }
  );



            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "AllBlogs", id = UrlParameter.Optional }
            );

        



        }
    }
}
