using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogPost.Models
{
    public class AllBlogsModel
    {

        public AllBlogsModel()
        {
            AllBLogs = new List<BlogPostModel>();
        }

        public IList<BlogPostModel> AllBLogs { get; set; }

    }
}