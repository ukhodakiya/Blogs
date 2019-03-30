using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogPost.Models
{
    public class ForgotPasswordModel
    {
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is wrong")]
        public string Email { get; set; }

        public bool Success { get; set; }

        public string message { get; set; }

    }
}