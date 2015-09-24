using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMK.Database
{
    class User
    {
        //names must be exactly like the database request
        //ID is incremental
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string user_Email { get; set; }

    }


}