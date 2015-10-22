using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMK.Support
{
    /// <summary>
    /// input a String and get the Hash of this String as String 
    /// use this:
    /// String hashResult=DependencyService.Get<IHash>().SHA512StringHash(" Inputstring" );
    /// </summary>
    public interface IHash
    {
        String Sha512StringHash(String inputString);
    }
}