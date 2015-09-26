using System.Diagnostics;
using System.Dynamic;

namespace SMK.DataAccess
{
    public class DataAccessHandler
    {
        /// <summary>
        /// List the available Interfaces for Dataaccess
        /// </summary>
        public enum InterfaceType
        {
            MySqlPhp
        }

        public static IDataAccess DataAccess { get; private set; }

        /// <summary>
        /// Sets the Dataacces. Easy to switch between different implementations of Dataaccess logic
        /// </summary>
        /// <param name="interfaceType"></param>
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