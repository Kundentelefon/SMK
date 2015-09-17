using SMK.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SMK.Database
{
    class DatabaseDialog
    {

        public bool PasswordAccept(string pwHashed)
        {
            string pw = PasswordHash(pwHashed);

            if (pw.Equals(pwHashed))
                return true;
            else
                return false;
        }
        public string PasswordHash(string pw)
        {
            String hashResult = DependencyService.Get<IHash>().SHA512StringHash(pw);
            return hashResult;
        } 
    }
}
