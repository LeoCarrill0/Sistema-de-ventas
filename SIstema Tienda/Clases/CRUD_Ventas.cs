using MySql.Data.MySqlClient;
using SIstema_Tienda.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Tienda.Clases
{
    class CRUD_Ventas
    {
        public static void Create_DB(string sql)
        {
            ConexionDB conexionDB = new ConexionDB();
            MySqlConnection conexionBD = conexionDB.Conexion();
            conexionBD.Open();

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                comando.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);
            }
            finally
            {
                conexionBD.Close();
            }
        }

        public void Search_DB(string sql)
        {

        }
    }
}
