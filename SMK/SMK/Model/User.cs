using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMK.Model
{
    //[Preserve(AllMembers = true)]
    public class User
    {
        public User(string email, string password)
        {
            user_ID = -1;
            user_Email = email;
            user_Password = password;
        }

        public int user_ID { get; set; }
        public String user_Email { get; set; }
        public String user_Password { get; set; }

    }
}
