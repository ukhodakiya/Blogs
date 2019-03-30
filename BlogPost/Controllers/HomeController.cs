using BlogPost.Core;
using BlogPost.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace BlogPost.Controllers
{
    public class HomeController : Controller
    {
        

        BlogPostContext _context;

        public HomeController()
        {
            _context = new BlogPostContext();
        }

        public ActionResult Index()
        { 
            if (Session["UserId"] == null)
                return RedirectToRoute("Login");
            

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        [HttpGet]
        public ActionResult Register()
        {
            RegisterModel model = new RegisterModel();

            return View(model);
        }


        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            var IsExistData = _context.User.ToList().Where(b => b.Email.Trim() == model.Email.Trim()).FirstOrDefault();

            if (IsExistData != null)
            {
                ModelState.AddModelError(string.Empty, "Email is already exist");
                return View(model);
            }

            if (ModelState.IsValid)
            {


                Register register = new Register()
                {
                    Email = model.Email,
                    Name = model.Name,
                    Password = model.Password
                };


                _context.User.Add(register);

                _context.SaveChanges();

                model.IsregisterSuccess = true;
            }

            return View(model);
        }



        [HttpGet]
        public ActionResult Login()
        {
            LoginModel model = new LoginModel();

            return View(model);
        }


        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            var Users = _context.User.ToList();

            if (Users.Count() > 0)
            {
                var userExist = Users.Where(a => a.Email.Trim() == model.Email.Trim()).FirstOrDefault();

                if (userExist == null)
                    ModelState.AddModelError(string.Empty, "Email is not exist");
                else
                {
                    if (userExist.Password.Trim() != model.Password.Trim())
                        ModelState.AddModelError(string.Empty, "Password is wrong");
                    else
                    {                       
                        Session["UserId"] = userExist.Id.ToString();
                        return RedirectToAction("AllBlogs");
                    }
                }
            }

            if (ModelState.Any())
            {
                return View(model);
            }


            return View(model);
        }


        public ActionResult Logout()
        {

            Session["UserId"] = null;

            return RedirectToRoute("Login");
        }

        [HttpGet]
        public ActionResult BlogPost()
        {
            if (Session["UserId"] == null)
                return RedirectToRoute("Login");

            BlogPostModel model = new BlogPostModel();
            ViewBag.success = false;
            return View(model);
        }


        [HttpPost]
        public ActionResult BlogPost(BlogPostModel model)
        {

            var IsExistData = _context.Blog.ToList().Where(b => b.Name.Trim() == model.Name.Trim()).FirstOrDefault();

            if (IsExistData != null)
            {
                ModelState.AddModelError(string.Empty, "Blog name is already exist");
                ViewBag.success = false;
                return View(model);
            }


            BlogPost.Core.BlogPost post = new BlogPost.Core.BlogPost()
            {
                Description = model.Description,
                IsPrivate = model.IsPrivate,
                Name = model.Name,
                UserId = Convert.ToInt32(Session["UserId"])
            };

            _context.Blog.Add(post);

            _context.SaveChanges();

            ViewBag.success = true;


            return View(model);
        }



        [HttpGet]
        public ActionResult AllBlogs()
        {
            AllBlogsModel model = new AllBlogsModel();
            var UserId = 0;

            if(Session["UserId"] != null)
            {
                UserId = Convert.ToInt32(Session["UserId"]);
            }


            var Allblog = _context.Blog.Where(a=>a.IsPrivate == false).ToList();
            PrepareBlogpostModel(model, Allblog);

            if (UserId > 0)
            {
                var CurrentUserBlog = _context.Blog.Where(c => c.IsPrivate == true && c.UserId == UserId).ToList();
                PrepareBlogpostModel(model, CurrentUserBlog);
            }
            

            return View(model);
        }



        [HttpGet]
        public ActionResult ForgotPassword()
        {
            ForgotPasswordModel model = new ForgotPasswordModel();
           

            return View(model);
        }


        public ActionResult PasswordRecovery(string email)
        {
            ForgotPasswordModel model = new ForgotPasswordModel();
             var ExistUser = _context.User.Where(a => a.Email.Trim() == email.Trim()).FirstOrDefault();

            if(ExistUser != null)
            {
                try
                {
                    var newPassword = GenerateRandomPassword();

                    var client = new SmtpClient("smtp.gmail.com", 587)
                    {
                        Credentials = new NetworkCredential("smitenkalathiya94@gmail.com", "sk5202@123#"),
                        EnableSsl = true
                    };
                    client.Send("smitenkalathiya94@gmail.com", ExistUser.Email, "Forgot Password from " + Request.Url.Host, "Your new password is :- " + newPassword);


                    ExistUser.Password = newPassword;

                    _context.SaveChanges();

                    model.Success = true;
                    model.message = "Password has been send to " + ExistUser.Email;
                }
                catch(Exception Ex)
                {
                    model.Success = false;
                    model.message = Ex.Message;
                }



               
            }
            else
            {
                model.Success = false;
                model.message = "Email is not register";
            }


            return Json(model, JsonRequestBehavior.AllowGet);
        }


        public ActionResult UserList()
        {
            UsersModel model = new UsersModel();

            var AllUser = _context.User.ToList();

            foreach(var a in AllUser)
            {
                RegisterModel m = new RegisterModel
                {
                    Email = a.Email,
                    Name = a.Name,
                    
                };

                model.RegisterUser.Add(m);
            }


            return View(model);
        }




        #region utilities
        public void PrepareBlogpostModel(AllBlogsModel model, List<BlogPost.Core.BlogPost> post)
        {
            foreach (var item in post)
            {
                BlogPostModel posts = new BlogPostModel()
                {
                    Description = item.Description,
                    IsPrivate = item.IsPrivate,
                    Name = item.Name,
                    UserName = _context.User.Where(a => a.Id == item.UserId).FirstOrDefault() == null ? "" : _context.User.Where(a => a.Id == item.UserId).FirstOrDefault().Name
                };
                model.AllBLogs.Add(posts);


            }
        }

        public string GenerateRandomPassword()
        {
            Random generator = new Random();
            String r = generator.Next(0, 999999).ToString("D6");

            return r;
        }

        #endregion




        //do not validate request token (XSRF)
        //for some reasons it does not work with "filtering" support
        [HttpPost]
        public virtual ActionResult AllBlogs(DataSourceRequest command, BlogPostModel model)
        {

            var AllBlogs = GetAllBlog(command.Page - 1, command.PageSize);

            var Blogs = AllBlogs.ToList()
                .Select(x =>
                {
                  
                    var settingModel = new BlogPostModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        UserId = x.UserId,
                        UserName =_context.User.Where(a=>a.Id == x.UserId).FirstOrDefault() == null ?"": _context.User.Where(a => a.Id == x.UserId).FirstOrDefault().Name

                       
                    };
                    return settingModel;
                })
                .AsQueryable();

            var gridModel = new DataSourceResult
            {
                Data = Blogs,
                Total = AllBlogs.TotalCount
            };

            return Json(gridModel);
        }

        [HttpPost]
        public virtual ActionResult AllBlogsAdd(BlogPostModel model)
        {

            var blog = new BlogPost.Core.BlogPost()
            {
                Description = model.Description,
                Name = model.Name,
                IsPrivate = false,
                UserId = model.UserId
            };

            _context.Blog.Add(blog);
            _context.SaveChanges();

            return Json(model,JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public virtual ActionResult AllBlogsDelete(int id)
        {

            var blog = _context.Blog.Where(a => a.Id == id).FirstOrDefault();

            if(blog != null)
            {
                _context.Blog.Remove(blog);

                _context.SaveChanges();
            }

            return Json(blog, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public virtual ActionResult AllBlogsUpdate(BlogPostModel model)
        {

            var blog = _context.Blog.Where(a => a.Id == model.Id).FirstOrDefault();

            if (blog != null)
            {
                blog.Name = model.Name;
                blog.Description = model.Description;
                blog.IsPrivate = model.IsPrivate;
                blog.UserId = model.UserId;

                _context.SaveChanges();

                

                
            }

            return Json(blog, JsonRequestBehavior.AllowGet);
        }

        public virtual IPagedList<BlogPost.Core.BlogPost> GetAllBlog(
           int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _context.Blog.ToList();
          

            var Blogs = new PagedList<BlogPost.Core.BlogPost>(query, pageIndex, pageSize);
            return Blogs;
        }

       

        public ActionResult TreeView()
        {


            return View();
        }


    }
}