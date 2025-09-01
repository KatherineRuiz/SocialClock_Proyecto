using Modelos;
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
    public partial class frmCreacionUsuario : Form
    {
        public frmCreacionUsuario()
        {
            InitializeComponent();
            MostrarUsuario();
            MostrarRol();
            RedondearPanel(pnlBienvenida, 40);
            dgvUsuarios.ScrollBars = ScrollBars.Both;
            lblEstadoUsuario.Visible = false;
            rbnActivo.Visible = false;
            rbnActivo.Checked = true;
            rbnInactivo.Visible = false;
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
        private void MostrarRol()
        {
            cmbRol.DataSource = null;
            cmbRol.DataSource = Rol.CargarRol();
            cmbRol.DisplayMember = "nombreRol";
            cmbRol.ValueMember = "idRol";
            cmbRol.SelectedIndex = -1;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cmbRol.SelectedIndex != -1 && !string.IsNullOrWhiteSpace(txtUsuario.Text) && !string.IsNullOrWhiteSpace(txtClave.Text))
            {
                Usuario user = new Usuario();
                user.NombreUsuario = txtUsuario.Text;
                user.Clave = BCrypt.Net.BCrypt.HashPassword(txtClave.Text);

                if (rbnActivo.Checked == true)
                {
                    user.EstadoUsuario = false;
                }
                else
                {
                    user.EstadoUsuario = true;
                }
                user.Id_Rol = Convert.ToInt32(cmbRol.SelectedValue);

                if (user.InsertarUsuario() == true)
                {

                    MessageBox.Show("Exito", "Datos ingresados correctamente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MostrarUsuario();
                }
            }
            else
            {
                MessageBox.Show("Por favor, asegurese de llenar todos los campos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtUsuario.Text = "";
            txtClave.Text = "";
            txtBuscar.Text = "";
            cmbRol.SelectedIndex = -1;
            lblEstadoUsuario.Visible = false;
            rbnActivo.Visible = false;
            rbnActivo.Checked = true;
            rbnInactivo.Visible = false;
            txtClave.ReadOnly = false;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario();
            int id = int.Parse(dgvUsuarios.CurrentRow.Cells[0].Value.ToString());
            string registroEliminar = dgvUsuarios.CurrentRow.Cells[1].Value.ToString();
            DialogResult respuesta = MessageBox.Show("¿Quieres eliminar este registro?\n"
                + registroEliminar, "Eliminando un registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta == DialogResult.Yes)
            {
                if (usuario.EliminarUsuario(id) == true)
                {
                    MessageBox.Show("Registro Eliminado\n" + registroEliminar, "Eliminado",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MostrarUsuario();
                }
            }
            else
            {
                MessageBox.Show("Registro no eliminado", "No seleccionado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvUsuarios_DoubleClick(object sender, EventArgs e)
        {
            txtUsuario.Text = dgvUsuarios.CurrentRow.Cells[1].Value.ToString();
            cmbRol.Text = dgvUsuarios.CurrentRow.Cells[2].Value.ToString();
            txtClave.Text = dgvUsuarios.CurrentRow.Cells[3].Value.ToString();
            lblEstadoUsuario.Visible = true;
            rbnActivo.Visible = true;
            rbnInactivo.Visible = true;
            if (dgvUsuarios.CurrentRow.Cells[4].Value.ToString() == "ACTIVO")
            {
                rbnActivo.Checked = true;
            }
            else
            {
                rbnInactivo.Checked = true;
            }

            txtClave.ReadOnly = true;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (cmbRol.SelectedIndex != -1 && !string.IsNullOrWhiteSpace(txtUsuario.Text) && !string.IsNullOrWhiteSpace(txtClave.Text))
            {
                Usuario usuario = new Usuario();
                usuario.NombreUsuario = txtUsuario.Text;
                usuario.Id_Rol = Convert.ToInt32(cmbRol.SelectedValue);
                usuario.Clave = txtClave.Text;
                
                if (rbnActivo.Checked == true)
                {
                    usuario.EstadoUsuario = false;
                }
                else
                {
                    usuario.EstadoUsuario = true;
                }
                if (dgvUsuarios.CurrentRow == null)
                {
                    MessageBox.Show("Asegúrese de seleccionar un registro", "Advertencia");
                    return;
                }
                else
                {
                    usuario.IdUsuario = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells[0].Value.ToString());
                }

                string registroEditar = dgvUsuarios.CurrentRow.Cells[1].Value?.ToString();
                DialogResult respuesta = MessageBox.Show("¿Quieres editar este registro?\n" + registroEditar,
                                                          "Editar registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    if (usuario.ActualizarUsuario() == true)
                    {
                        MostrarUsuario();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo editar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    return;
                }
                
            }
                
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                dgvUsuarios.DataSource = null;
                dgvUsuarios.DataSource = Usuario.BuscarUsuario(txtBuscar.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtBuscar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            txtBuscar.ForeColor = Color.Black;
        }

        #region Validaciones
        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;

            if (!char.IsLetter(c) && c != '@' && c != '_' && c != '.' && c != ' ' && c != (char)Keys.Back)
            {
                MessageBox.Show("Solo se permiten letras, @, guion bajo, punto y espacio", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true;
            }
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
        #endregion

    }
}
