using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogPost.Models
{
    public class BlogPostModel 
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public bool IsPrivate { get; set; }


        public int UserId { get; set; }

        public string UserName { get; set; }


    }
}