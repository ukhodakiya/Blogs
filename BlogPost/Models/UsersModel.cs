using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogPost.Models
{
    public class UsersModel
    {
        public UsersModel()
        {
            RegisterUser = new List<RegisterModel>();
        }

        public IList<RegisterModel> RegisterUser { get; set; }

    }
}