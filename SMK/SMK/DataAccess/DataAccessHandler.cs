using System.Diagnostics;
using System.Dynamic;

namespace SMK.DataAccess
{
    public class DataAccessHandler
    {
        /// <summary>
        /// List of the available Interfaces for Dataaccess
        /// </summary>
        public enum InterfaceType
        {
            MySqlPhp
        }

        public static IDataAccess DataAccess { get; private set; }

        public string ServerAdress { get; private set; } = "192.168.2.100";
        public string FtpName { get; private set; } = "SMKFTPUser";
        public string FtpPassword { get; private set; } = "";


        /// <summary>
        /// Sets the DataAcces. Easy to switch between different implementations of the DataAccess logic
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