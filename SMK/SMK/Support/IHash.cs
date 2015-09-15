using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMK.Support
{
    public interface IHash
    {
        String SHA512StringHash(String inputString); 
    }
}