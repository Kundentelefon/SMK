using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMK_Database
{
    //Dataaccess handler
    //Dataaccess interface
    public interface Dataaccess
    {
        void Database_handler();

        bool PasswordAccept();

        String PasswordHash(String inputString);
    }
}
