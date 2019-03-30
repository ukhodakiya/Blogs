using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogPost.Models
{
    public class LoginModel
    {
        [Required]
        [DataType(DataType.EmailAddress,ErrorMessage ="Email is wrong")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

    }
}