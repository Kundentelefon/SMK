using System.Diagnostics;
using System.Dynamic;

namespace SMK.DataAccess
{
    public class DataAccessHandler
    {
        public enum InterfaceType
        {
            MySqlPhp
        }

        public static IDataAccess DataAccess { get; private set; }

        public static void InitDataAccess(InterfaceType interfaceType)
        {
            switch (interfaceType)
            {
                case InterfaceType.MySqlPhp:
                    DataAccess = new DataAccessMySqlPhp();
                    break;
            }
        }
    }
}