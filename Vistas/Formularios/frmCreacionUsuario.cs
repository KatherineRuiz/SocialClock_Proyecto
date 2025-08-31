using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Modelos;
using Modelos.Entidades;

namespace Vistas.Formularios
{
    public partial class frmCreacionUsuario : Form
    {
        public frmCreacionUsuario()
        {
            InitializeComponent();
            MostrarUsuario();
            MostrarRol();
            dgvUsuarios.ScrollBars = ScrollBars.Both;
        }

        private void lblBienvenida_Click(object sender, EventArgs e)
        {

        }

        private void btnEditarPrimero_Click(object sender, EventArgs e)
        {

        }

        private void rbnActivo_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void frmCreacionUsuario_Load(object sender, EventArgs e)
        {
            MostrarUsuario();
        }
        private void MostrarUsuario()
        {
            dgvUsuarios.DataSource = null;
            dgvUsuarios.DataSource = Usuario.CargarUsuario();
        }


        //Metodo para mostrar las opciones del comboBox Rol\
        private void MostrarRol ()
        {
            cmbRol.DataSource = null;
            cmbRol.DataSource = Rol.CargarRol();
            cmbRol.DisplayMember = "nombreRol";
            cmbRol.ValueMember = "idRol";
            cmbRol.SelectedIndex = -1;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Usuario p = new Usuario();
            p.NombreUsuario = txtUsuario.Text;
            p.Clave = txtClave.Text;
            if (rbnActivo.Checked == true)
            {
                p.EstadoUsuario = false;
            }
            else
            {
                p.EstadoUsuario = true;
            }
            p.Id_Rol = Convert.ToInt32(cmbRol.SelectedValue);
            p.InsertarUsuario();
            MessageBox.Show("Exito", "Datos ingresados correctamente", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MostrarUsuario();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

        }
    }
}
