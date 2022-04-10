using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;

namespace Lab03
{
    public partial class Login : Form
    {
        SqlConnection conn;
        public Login(SqlConnection conn)
        {
            this.conn = conn;
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
 
            if (conn.State == ConnectionState.Open)
            {
                String sql = "SELECT usuario_nombre, usuario_password " +
                    "FROM tbl_usuario WHERE usuario_nombre = @vUsuario " +
                    "AND  usuario_password = @vPassword";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@vUsuario", txtUsuario.Text);
                cmd.Parameters.AddWithValue("@vPassword", txtPassword.Text);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read()) {
                    Persona persona = new Persona(conn);
                    persona.Show();
                    this.Hide();
                    reader.Close();
                }

                else {
                    MessageBox.Show("Error en los datos");
                    reader.Close();
                }

            }
            else
            {
                MessageBox.Show("La conexión esta cerrada");
            }


        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
