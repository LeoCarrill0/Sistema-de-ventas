using MySql.Data.MySqlClient;
using SIstema_Tienda.Clases;
using System.Timers;

namespace SIstema_Tienda
{
    public partial class Ventas : Form
    {
        public Ventas()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            Finalizar();
            textBox1.Text = "";
            Guardar_venta();
            Reiniciar();
            this.ActiveControl = textBox1;

        }


        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void inicioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form Menu_FRM = new Reporte_Ventas();
            Menu_FRM.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            

        }

        int[] Id_A0 = new int[1];
        string[] Nombre_A1 = new string[1];
        double[] Precio_A2 = new double[1];
        
        string[] Fecha_A3 = new string[1];
        int[] Cantidad_A4 = new int[1];
        string[] Codigo_A5 = new string[1];
        double[] Precio_compra_A6 = new double[1];

        string lista_productos_g = "";
        int i = 0;
        double precio_total = 0;
        double precio_total_compra = 0;
        bool flag = false;
        private void Guardar_venta()
        {
            try
            {

                if (lista_productos_g != "")
                {
                    string sql = "INSERT INTO ventas (Productos, Total, Total_Compra) VALUES ('" + lista_productos_g + "','"
                            + precio_total + "', '" + precio_total_compra + "')";
                    
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
                else
                {
                    label1.Text = "Debe completar los campos";
                }
            }

            catch (FormatException fex)
            {
                label1.Text = "Datos incorectos : " + fex.Message;
            }
        }

        private void Finalizar()
        {
            try
            {
                string codigo = "";
                int Existencias = 0;
                string Nombre_prod = "";
                MySqlDataReader reader = null;
                ConexionDB conexionDB = new ConexionDB();
                MySqlConnection conexionBD = conexionDB.Conexion();
                conexionBD.Open();

                try
                {
                    if (Codigo_A5.Length != 0)
                    {
                        MySqlCommand comando = new MySqlCommand();
                        comando.Connection = conexionBD;
                        for (int x = 0; x <= i; x++)
                        {
                            codigo = Codigo_A5[x];

                            string sql_buscar = "SELECT Cantidad, Nombre, Codigo, Precio FROM productos WHERE Codigo LIKE '" + codigo + "' LIMIT 1";

                            comando.CommandText = sql_buscar;
                            reader = comando.ExecuteReader();

                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Existencias = reader.GetInt16(0);
                                    Nombre_prod = reader.GetString(1);
                                }

                                conexionBD.Close();

                                if (Existencias >= 1)
                                {
                                    Existencias--;
                                    string sql_actualizar = "UPDATE productos SET Cantidad='" + Existencias + "' WHERE Codigo='" + codigo + "'";
                                    conexionBD.Open();
                                    comando.CommandText = sql_actualizar;
                                    lista_productos_g += x + "-\t" + Nombre_prod + "\n";
                                    comando.ExecuteNonQuery();
                                }
                                else
                                {
                                    MessageBox.Show("El producto " + Nombre_prod + " ya no existe");
                                    break;
                                }

                            }
                            else
                            {
                                label1.Text = ("No hay productos agregados");
                                break;
                            }
                        }

                        textBox1.Text = "";
                        MessageBox.Show(lista_productos_g + "Total\t" + precio_total);
                    }

                }
                catch (MySqlException ex)
                {
                    label1.Text = ("Error: " + ex.Message);
                }
                finally
                {
                    conexionBD.Close();
                }

            }
            catch (FormatException fex)
            {
                label1.Text = ("Datos incorectos : " + fex.Message);
            }
        }

        private void Eliminar()
        {

            if (i > 0)
            {
                precio_total = precio_total - Precio_A2[i];
                precio_total_compra = precio_total_compra - Precio_compra_A6[i];
                label1.Text = "El producto " + Nombre_A1[i] + " se ha eliminado";

                Array.Resize(ref Id_A0, Id_A0.Length - 1);
                Array.Resize(ref Nombre_A1, Nombre_A1.Length - 1);
                Array.Resize(ref Precio_A2, Precio_A2.Length - 1);
                Array.Resize(ref Fecha_A3, Fecha_A3.Length - 1);
                Array.Resize(ref Cantidad_A4, Cantidad_A4.Length - 1);
                Array.Resize(ref Codigo_A5, Codigo_A5.Length - 1);
                Array.Resize(ref Precio_compra_A6, Precio_compra_A6.Length - 1);
                i--;
                imprimir();
            }
            else
            {
                Reiniciar();
            }
        }

        private void Reiniciar()
        {
            Array.Resize(ref Id_A0, 0);
            Array.Resize(ref Nombre_A1, 0);
            Array.Resize(ref Precio_A2, 0);
            Array.Resize(ref Precio_compra_A6, 0);
            Array.Resize(ref Fecha_A3, 0);
            Array.Resize(ref Cantidad_A4, 0);
            Array.Resize(ref Codigo_A5, 0);

            precio_total = 0;
            precio_total_compra = 0;
            lista_productos_g = "";
            i = 0;
            flag = false;
            listView1.Items.Clear();
            label1.Text = ("");
        }

        private void Agregar()
        {

            string codigo = textBox1.Text;
            MySqlDataReader reader = null;
            if (codigo != "")
            {

                string sql = "SELECT Id, Nombre, Precio, Fecha, Cantidad, Codigo,Precio_Compra FROM productos WHERE Codigo LIKE '"
                + codigo + "' LIMIT 1";
                ConexionDB conexionDB = new ConexionDB();
                MySqlConnection conexionBD = conexionDB.Conexion();
                conexionBD.Open();

                try
                {
                    MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                    reader = comando.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (flag == false)
                            {
                                Array.Resize(ref Id_A0, 1);
                                Array.Resize(ref Nombre_A1, 1);
                                Array.Resize(ref Precio_A2, 1);
                                Array.Resize(ref Precio_compra_A6, 1);
                                Array.Resize(ref Fecha_A3, 1);
                                Array.Resize(ref Cantidad_A4, 1);
                                Array.Resize(ref Codigo_A5, 1);

                                if (reader.GetInt16(4) > 0)
                                {
                                    flag = true;
                                    Id_A0[i] = reader.GetInt16(0);
                                    Nombre_A1[i] = reader.GetString(1);
                                    Precio_A2[i] = reader.GetDouble(2);
                                    Fecha_A3[i] = reader.GetString(3);
                                    Cantidad_A4[i] = reader.GetInt16(4);
                                    Codigo_A5[i] = reader.GetString(5);
                                    Precio_compra_A6[i] = reader.GetDouble(6);

                                    precio_total = precio_total + Precio_A2[i];
                                    precio_total_compra = precio_total_compra + Precio_compra_A6[i];
                                    label1.Text = "Producto " + Nombre_A1[i] + " agregado";
                                    imprimir();
                                }
                                else
                                {
                                    MessageBox.Show("El producto " + reader.GetString(1) + " ya no existe");
                                }
                            }
                            else
                            {
                                if (reader.GetInt16(4) > 0)
                                {
                                    i++;

                                    Array.Resize(ref Id_A0, Id_A0.Length + 1);
                                    Array.Resize(ref Nombre_A1, Nombre_A1.Length + 1);
                                    Array.Resize(ref Precio_A2, Precio_A2.Length + 1);
                                    Array.Resize(ref Precio_compra_A6, Precio_compra_A6.Length + 1);
                                    Array.Resize(ref Fecha_A3, Fecha_A3.Length + 1);
                                    Array.Resize(ref Cantidad_A4, Cantidad_A4.Length + 1);
                                    Array.Resize(ref Codigo_A5, Codigo_A5.Length + 1);

                                    Id_A0[i] = reader.GetInt16(0);
                                    Nombre_A1[i] = reader.GetString(1);
                                    Precio_A2[i] = reader.GetDouble(2);
                                    Fecha_A3[i] = reader.GetString(3);
                                    Cantidad_A4[i] = reader.GetInt16(4);
                                    Codigo_A5[i] = reader.GetString(5);
                                    Precio_compra_A6[i] = reader.GetDouble(6);

                                    precio_total = precio_total + Precio_A2[i];
                                    precio_total_compra = precio_total_compra + Precio_compra_A6[i];
                                    label1.Text = "Producto " + Nombre_A1[i] + " agregado";
                                    imprimir();
                                }
                                else
                                {
                                    MessageBox.Show("El producto " + reader.GetString(1) + " ya no existe");
                                }
                            }
                        }
                    }
                    else
                    {
                        label1.Text = "No existe el producto";
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
                label1.Text = "Debe Ingresar el codigo";
            }


        }

        private void imprimir()
        {
            try
            {
                listView1.Items.Clear();
                for (int x = 0; x <= i; x++)
                {
                    int x_1 = x + 1;
                    ListViewItem lista = lista = new ListViewItem(x_1.ToString());
                    lista.SubItems.Add(Codigo_A5[x].ToString());
                    lista.SubItems.Add(Nombre_A1[x]);
                    lista.SubItems.Add(Precio_A2[x].ToString());

                    if (x == i)
                    {
                        lista.SubItems.Add(precio_total.ToString());
                        listView1.Items.Add(lista);
                    }
                    else
                    {
                        listView1.Items.Add(lista);
                    }

                }
            }
            catch (Exception ex)
            {
                label1.Text = "Error" + ex.Message;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Eliminar();
            this.ActiveControl = textBox1;
        }

        private void button4_Click(object sender, EventArgs e)
        {

            listView1.Items.Clear();
            Reiniciar();
            this.ActiveControl = textBox1;
        }

        private async void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                Agregar();
                textBox1.Text = "";
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}