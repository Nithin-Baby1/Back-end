using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webAPI2.Models
{
    public class ApplicationUserModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Stream { get; set; }
        public int RollNo { get; set; }
        public string UserId { get; set; }


        public string Password { get; set; }
        public string Role { get; set; }

    }
}
