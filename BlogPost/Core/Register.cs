using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogPost.Core
{
    public class Register : BaseEntity
    {

        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        

    }
}