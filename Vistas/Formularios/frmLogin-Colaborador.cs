using Modelos.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
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
            RedondearPanel(pnlLogin, 40);
        }
        private void RedondearPanel(Panel panel, int radio)
        {
            //Creamos un objeto de tipo GraphicsPath
            GraphicsPath path = new GraphicsPath();
            //Dibujamos la figura
            path.StartFigure();
            path.AddArc(new Rectangle(0, 0, radio, radio), 180, 90);
            path.AddArc(new Rectangle(panel.Width - radio, 0, radio, radio), 270, 90);
            path.AddArc(new Rectangle(panel.Width - radio, panel.Height - radio, radio, radio), 0, 90);
            path.AddArc(new Rectangle(0, panel.Height - radio, radio, radio), 90, 90);
            path.CloseFigure();
            panel.Region = new Region(path);
        }
        private void btnIngresar_Click_1(object sender, EventArgs e)
        {
            // Validar que los campos no estén vacíos

            if (!(string.IsNullOrEmpty(txtUsuario.Text) || string.IsNullOrEmpty(txtClave.Text)))
            {
                string clave = txtClave.Text;
                string nombreUsuario = txtUsuario.Text;

                Usuario usuario = new Usuario();

                if (usuario.VerificarLoginColaborador(nombreUsuario, clave))
                {
                    MessageBox.Show("Inicio de sesión exitoso");

                    frmSocialClock_Colaborador fe = new frmSocialClock_Colaborador();
                    fe.Show();
                    this.Hide();

                }
                else
                {
                    MessageBox.Show("Usuario y/o clave no coinciden", "Datos incorrectos");
                }
            }
            else
            {
                MessageBox.Show("Por favor, ingrese el usuario y la contraseña.", "Campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            frmSeleccionDeRol fe = new frmSeleccionDeRol();
            fe.Show();
            this.Hide();
        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;

            if (!char.IsLetter(c) && c != '@' && c != '_' && c != '.' && c != ' ' && c != (char)Keys.Back)
            {
                MessageBox.Show("Solo se permiten letras, @, guion bajo, punto y espacio", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true;
            }
        }

        private void txtClave_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Space) && !(char.IsNumber(e.KeyChar)))
            {
                MessageBox.Show("Solo permite números y letras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true;
                return;
            }
        }
    }
}
