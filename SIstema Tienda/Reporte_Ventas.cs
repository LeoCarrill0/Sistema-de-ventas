using MySql.Data.MySqlClient;
using SIstema_Tienda.Clases;
using Sistema_Tienda.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Sistema_Tienda
{
    public partial class Reporte_Ventas : Form
    {
        public Reporte_Ventas()
        {
            InitializeComponent();
        }

        private void Reporte_Ventas_Load(object sender, EventArgs e)
        {

        }

        private void mostrarTodosLosProductosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imprimir();
        }
        private void imprimir()
        {

            MySqlDataReader reader = null;

            string sql = "SELECT Id, Nombre, Precio, Fecha, Cantidad, Codigo, Precio_Compra FROM productos";

            ConexionDB conexionDB = new ConexionDB();
            MySqlConnection conexionBD = conexionDB.Conexion();
            conexionBD.Open();

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    listView1.Items.Clear();
                    while (reader.Read())
                    {
                        ListViewItem lista = lista = new ListViewItem(reader.GetString(0));
                        lista.SubItems.Add(reader.GetString(5));
                        lista.SubItems.Add(reader.GetString(1));
                        lista.SubItems.Add(reader.GetString(6));
                        lista.SubItems.Add(reader.GetString(2));
                        lista.SubItems.Add(reader.GetString(4));
                        double a = reader.GetDouble(4);
                        double T_p_v = reader.GetDouble(2) * reader.GetDouble(4);
                        double T_p_c = reader.GetDouble(6) * reader.GetDouble(4);
                        lista.SubItems.Add(T_p_c.ToString());
                        lista.SubItems.Add(T_p_v.ToString());
                        listView1.Items.Add(lista);

                    }
                }
                else
                {

                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conexionBD.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form menu = new Menu();
            menu.ShowDialog();
            this.Close();
        }
    }
}
