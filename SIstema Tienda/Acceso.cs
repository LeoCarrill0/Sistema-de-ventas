using MySql.Data.MySqlClient;
using SIstema_Tienda.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIstema_Tienda
{
    public partial class Acceso : Form
    {
        public Acceso()
        {
            InitializeComponent();
        }
        public static void acceso() 
        { 
        
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string Usuario = textBox1.Text;
            String Pass = textBox2.Text;
            MySqlDataReader reader = null;

            if (Usuario != "" || Pass != "")
            {

                string sql = "SELECT Nombre, Pass FROM usuarios WHERE Nombre LIKE '"
                    + Usuario + "' AND Pass like '" + Pass + "'";

                ConexionDB conexionDB = new ConexionDB();
                MySqlConnection conexionBD = conexionDB.Conexion();
                conexionBD.Open();

                try
                {
                    MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                    reader = comando.ExecuteReader();
                    if (reader.HasRows)
                    {
                        label1.Text = "Accediendo";
                        this.Hide();
                        Form Menu_FRM = new Menu();
                        Menu_FRM.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        label1.Text = "Datos incorectos";
                    }
                }
                catch (MySqlException ex)
                {
                    label1.Text = "Error: " + ex.Message;
                }
                finally
                {
                    conexionBD.Close();
                }
            }
            else
            {
                label1.Text = "Debe completar los campos";
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                string Usuario = textBox1.Text;
                String Pass = textBox2.Text;
                MySqlDataReader reader = null;

                if (Usuario != "" || Pass != "")
                {

                    string sql = "SELECT Nombre, Pass FROM usuarios WHERE Nombre LIKE '"
                        + Usuario + "' AND Pass like '" + Pass + "'";

                    ConexionDB conexionDB = new ConexionDB();
                    MySqlConnection conexionBD = conexionDB.Conexion();
                    conexionBD.Open();

                    try
                    {
                        MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                        reader = comando.ExecuteReader();
                        if (reader.HasRows)
                        {
                            label1.Text = "Accediendo";
                            this.Hide();
                            Form Menu_FRM = new Menu();
                            Menu_FRM.ShowDialog();
                            this.Close();
                        }
                        else
                        {
                            label1.Text = "Datos incorectos";
                        }
                    }
                    catch (MySqlException ex)
                    {
                        label1.Text = "Error: " + ex.Message;
                    }
                    finally
                    {
                        conexionBD.Close();
                    }
                }
                else
                {
                    label1.Text = "Debe completar los campos";
                }
            }
        }
    }
}
