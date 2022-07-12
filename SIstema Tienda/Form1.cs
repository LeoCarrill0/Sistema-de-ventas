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
    }
}