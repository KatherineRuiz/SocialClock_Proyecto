using Modelos.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vistas.Formularios
{
    public partial class frmLogin_Colaborador : Form
    {
        public frmLogin_Colaborador()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            Usuario usr = new Usuario();

            string claveBD = "";
            claveBD = usr.ConsultarClaveColaborador(txtUsuario.Text);

            //MessageBox.Show("claveBD " + claveBD, "Error");
            if (claveBD != null)
            {
                if (txtClave.Text == claveBD)
                {
                    frmSocialClock_Colaborador fe = new frmSocialClock_Colaborador();
                    fe.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Usuario y/o clave no coinciden", "Error");
                }
            }
            else
            {
                MessageBox.Show("Usuario y/o clave no coinciden", "Error");
            }
        }
    }
}
