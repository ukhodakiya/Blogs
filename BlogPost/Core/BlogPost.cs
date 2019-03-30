using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogPost.Core
{
    public class BlogPost : BaseEntity
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPrivate { get; set; }
        public int UserId { get; set; }
    }
}