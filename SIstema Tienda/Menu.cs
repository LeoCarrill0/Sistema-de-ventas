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
    public partial class Menu : Form
    {
        
        public Menu()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form Ventas_FRM = new Ventas();
            Ventas_FRM.ShowDialog();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                String Codigo = textBox1.Text;
                String Nombre = textBox2.Text;
                double Precio = double.Parse(textBox3.Text);
                int Existencias = int.Parse(textBox4.Text);
                if (Codigo != "" && Nombre != "" && Precio > 0 && Existencias > 0)
                {
                    string sql = "INSERT INTO productos (Nombre, Precio, Cantidad, Codigo) VALUES ('" + Nombre + "','"
                    + Precio + "','" + Existencias + "','" + Codigo + "')";

                    MySqlConnection conexionBD = ConexionDB.Conexion();
                    conexionBD.Open();

                    try
                    {
                        MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                        comando.ExecuteNonQuery(); ;
                        label7.Text = "Producto guardados";
                        limpiar();
                    }
                    catch (MySqlException ex)
                    {
                        label7.Text = "Error al guardar: " + ex.Message;
                    }
                    finally
                    {
                        conexionBD.Close();
                    }
                }
                else
                {
                    label7.Text = "Debe completar los campos";
                }
            }
            catch (FormatException fex)
            {
                label7.Text = "Datos incorectos : " + fex.Message;
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            limpiar();
            label7.Text = "";
        }
        string Id_1;
        private void button1_Click(object sender, EventArgs e)
        {
            string codigo = textBox1.Text;
            MySqlDataReader reader = null;
            if (codigo != "")
            {

                string sql = "SELECT Id, Nombre, Precio, Fecha, Cantidad, Codigo FROM productos WHERE Codigo LIKE '"
                + codigo + "' LIMIT 1";

                MySqlConnection conexionBD = ConexionDB.Conexion();
                conexionBD.Open();

                try
                {
                    MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                    reader = comando.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Id_1 = reader.GetString(0);
                            textBox2.Text = reader.GetString(1);
                            textBox3.Text = reader.GetString(2);
                            textBox4.Text = reader.GetString(4);
                            label6.Text = reader.GetString(3);
                            label7.Text = "";
                        }
                    }
                    else
                    {
                        label7.Text = "No se encontraron registros";
                        limpiar();
                    }
                }
                catch (MySqlException ex)
                {
                    label7.Text = "Error: " + ex.Message;
                }
                finally
                {
                    conexionBD.Close();
                }
            }
            else
            {
                label7.Text = "Debe completar los campos";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                String id = Id_1;
                String Codigo = textBox1.Text;
                String Nombre = textBox2.Text;
                double Precio = double.Parse(textBox3.Text);
                int Existencias = int.Parse(textBox4.Text);

                if (Codigo != "" && Nombre != "" && Precio > 0 && Existencias > 0)
                {

                    string sql = "UPDATE productos SET Codigo='" + Codigo + "', Nombre='" + Nombre + "', Precio='"
                    + Precio + "', Cantidad='" + Existencias + "' WHERE Id='" + id + "'";

                    MySqlConnection conexionBD = ConexionDB.Conexion();
                    conexionBD.Open();

                    try
                    {
                        MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                        comando.ExecuteNonQuery();
                        label7.Text = "Producto modificado";
                        limpiar();

                    }
                    catch (MySqlException ex)
                    {
                        label7.Text = "Error al modificar: " + ex.Message;
                    }
                    finally
                    {
                        conexionBD.Close();
                    }
                }
                else
                {
                    label7.Text = "Debe completar los campos";
                }
            }
            catch (FormatException fex)
            {
                label7.Text = "Datos incorectos : " + fex.Message;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                String id = Id_1;
                String Codigo = textBox1.Text;
                String Nombre = textBox2.Text;
                double Precio = double.Parse(textBox3.Text);
                int Existencias = int.Parse(textBox4.Text);

                if (Codigo != "" && Nombre != "" && Precio > 0 && Existencias > 0)
                {

                    string sql = "DELETE FROM productos WHERE Id='" + id + "'";

                    MySqlConnection conexionBD = ConexionDB.Conexion();
                    conexionBD.Open();

                    try
                    {
                        MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                        comando.ExecuteNonQuery();

                        comando.CommandText = "ALTER TABLE productos DROP Id";
                        comando.ExecuteNonQuery();
                        comando.CommandText = "ALTER TABLE productos AUTO_INCREMENT = 1";
                        comando.ExecuteNonQuery();
                        comando.CommandText = "ALTER TABLE productos ADD Id int(4) NOT NULL AUTO_INCREMENT PRIMARY KEY FIRST";
                        comando.ExecuteNonQuery();

                        label7.Text = "Producto eliminado";

                        limpiar();

                    }
                    catch (MySqlException ex)
                    {
                        label7.Text = "Error al eliminar: " + ex.Message;
                    }
                    finally
                    {
                        conexionBD.Close();
                        Id_1 = null;
                    }
                }
                else
                {
                    label7.Text = "Producto no encontrado";
                }
            }
            catch (FormatException fex)
            {
                label7.Text = "Datos incorectos : " + fex.Message;
            }

        }
        private void limpiar()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            label6.Text = "";
            Id_1 = null;
        }
    }
}
