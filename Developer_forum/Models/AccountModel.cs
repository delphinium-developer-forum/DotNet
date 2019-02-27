using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Developer_forum.Models
{
    public class AccountModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string oldPassword { get; set; }
        public string Password { get; set; }
        public string newPassword { get; set; }
        public string name { get; set; }
        public string imageUrl { get; set; }
        public string LoggedOn { get; set; }
        public string PhoneNumber { get; set; }
    }
}