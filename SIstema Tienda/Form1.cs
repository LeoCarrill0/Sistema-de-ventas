using MySql.Data.MySqlClient;
using SIstema_Tienda.Clases;

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
            Form Menu_FRM = new Acceso();
            Menu_FRM.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Agregar();
            textBox1.Text = "";

        }

        int[] Id_A0 = new int[1];
        string[] Nombre_A1 = new string[1];
        double[] Precio_A2 = new double[1];
        string[] Fecha_A3 = new string[1];
        int[] Cantidad_A4 = new int[1];
        string[] Codigo_A5 = new string[1];
        int i = 0;
        double precio_total = 0;
        string s = "E03";
        bool flag = false;

        private void Eliminar()
        {
            
            if (i > 0)
            {
                precio_total = precio_total - Precio_A2[i];
                label1.Text = "El producto " + Nombre_A1[i] + " se ha eliminado";

                Array.Resize(ref Id_A0, Id_A0.Length - 1);
                Array.Resize(ref Nombre_A1, Nombre_A1.Length - 1);
                Array.Resize(ref Precio_A2, Precio_A2.Length - 1);
                Array.Resize(ref Fecha_A3, Fecha_A3.Length - 1);
                Array.Resize(ref Cantidad_A4, Cantidad_A4.Length - 1);
                Array.Resize(ref Codigo_A5, Codigo_A5.Length - 1);
                i--;
                imprimir();
            }
            else 
            {
                Reiniciar();
            }
        }

        private void Reiniciar() {
            Array.Resize(ref Id_A0, 0);
            Array.Resize(ref Nombre_A1, 0);
            Array.Resize(ref Precio_A2, 0);
            Array.Resize(ref Fecha_A3, 0);
            Array.Resize(ref Cantidad_A4, 0);
            Array.Resize(ref Codigo_A5, 0);

            Array.Resize(ref Id_A0, 1);
            Array.Resize(ref Nombre_A1, 1);
            Array.Resize(ref Precio_A2, 1);
            Array.Resize(ref Fecha_A3, 1);
            Array.Resize(ref Cantidad_A4, 1);
            Array.Resize(ref Codigo_A5, 1);
            precio_total = 0;
            i = 0;
            flag = false;
            label1.Text = "No hay productos escaneados";
            listView1.Items.Clear();
        }

        private void Agregar(){

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
                            if (flag == false)
                            {
                                flag = true;
                                Id_A0[i] = reader.GetInt16(0);
                                Nombre_A1[i] = reader.GetString(1);
                                Precio_A2[i] = reader.GetDouble(2);
                                Fecha_A3[i] = reader.GetString(3);
                                Cantidad_A4[i] = reader.GetInt16(4);
                                Codigo_A5[i] = reader.GetString(5);

                                precio_total = precio_total + Precio_A2[i];
                                label1.Text = "Producto agregado 1";
                                imprimir();
                            }
                            else
                            {
                                i++;

                                Array.Resize(ref Id_A0, Id_A0.Length + 1);
                                Array.Resize(ref Nombre_A1, Nombre_A1.Length + 1);
                                Array.Resize(ref Precio_A2, Precio_A2.Length + 1);
                                Array.Resize(ref Fecha_A3, Fecha_A3.Length + 1);
                                Array.Resize(ref Cantidad_A4, Cantidad_A4.Length + 1);
                                Array.Resize(ref Codigo_A5, Codigo_A5.Length + 1);

                                Id_A0[i] = reader.GetInt16(0);
                                Nombre_A1[i] = reader.GetString(1);
                                Precio_A2[i] = reader.GetDouble(2);
                                Fecha_A3[i] = reader.GetString(3);
                                Cantidad_A4[i] = reader.GetInt16(4);
                                Codigo_A5[i] = reader.GetString(5);

                                precio_total = precio_total + Precio_A2[i];
                                label1.Text = "Producto agregado 2";
                                imprimir();
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
            listView1.Items.Clear();
            for (int x = 0; x <= i; x++)
            {
                ListViewItem lista = lista = new ListViewItem();
                lista.SubItems.Add(Codigo_A5[x].ToString());
                lista.SubItems.Add(Nombre_A1[x]);
                lista.SubItems.Add(Precio_A2[x].ToString());
                
                if(x == i)
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

        private void button3_Click(object sender, EventArgs e)
        {
            Eliminar();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            imprimir();
            Reiniciar();
        }
    }
}