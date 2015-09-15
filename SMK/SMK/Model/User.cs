using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMK.Model
{
    class User
    {
        public String user_Email { get; set; }
        public String user_Password { get; set; }
        public User(String user_Email,String user_Password)
        {
            this.user_Email = user_Email;
            this.user_Password = user_Password;
        }
    }
}
