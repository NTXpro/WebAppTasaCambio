using System.Configuration;

namespace Infrastructure.Utilities
{
    class Globals
    {

        public abstract class ConnectionAccess
        {
            /// <summary>
            /// Get the connection string
            /// Busca la cadena de conexion
            /// </summary>
            protected string ConnectionString
            {
                get
                {
                    string str = System.Configuration.ConfigurationManager.ConnectionStrings["MyConn"].ConnectionString;
                    return str;
                }
            }
        }
    }
}
