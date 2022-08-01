using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIstema_Tienda.Clases
{
    class ConexionDB
    {
        public MySqlConnection Conexion()
        {
            string servidor = "localhost";
            string db = "sistema_tienda";
            string usuario = "root";
            string password = "123456";

            string cadenaConexion = "Database=" + db + "; Data Source=" + servidor + "; User Id=" + usuario + "; Password=" + password+ "";

            try
            {

                MySqlConnection conexionDBS = new MySqlConnection(cadenaConexion);
                return(conexionDBS);

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }

        }
    }
}
