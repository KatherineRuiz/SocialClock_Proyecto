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
    public partial class frmResultado_Colaborador : Form
    {
        private string carnet;
        public frmResultado_Colaborador(string carnetBusqueda)
        {
            InitializeComponent();
            mostrarNivelAcademico();
            mostrarProyecto();
            mostrarSeccion();
            mostrarEspecialidad();
            carnet = carnetBusqueda;

            dtpFechaBitacora.MinDate = DateTime.Today;
            dtpFechaBitacora.MaxDate = DateTime.Today;
            dtpFechaBitacora.Value = DateTime.Today;
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
        private void MostrarBusqueda()
        {
            dgvEstudianteEncontrado.DataSource = null;
            dgvEstudianteEncontrado.DataSource = Estudiante.CargarBusqueda(txtBuscar.Text);
        }

        private void MostrarBitacora(int idEstudiante)
        {
            dgvEstudianteEncontrado.DataSource = null;
            dgvEstudianteEncontrado.DataSource = BitacoraSocial.CargarBitacoraSocial(idEstudiante);
            btnRegresar.Visible = true;
        }

        #region Metodos para mostrar las opciones disponibles de los combo box
        private void mostrarNivelAcademico()
        {
            cbNivelAcademico.DataSource = null;
            cbNivelAcademico.DataSource = NivelAcademico.CargarNivelAcademico();
            cbNivelAcademico.DisplayMember = "nombreNivel";
            cbNivelAcademico.ValueMember = "idNivelAcademico";
            cbNivelAcademico.SelectedIndex = -1;
        }

        private void mostrarEspecialidad()
        {
            cbEspecialidad.DataSource = null;
            cbEspecialidad.DataSource = Especialidad.CargarEspecialidad();
            cbEspecialidad.DisplayMember = "nombreEspecialidad";
            cbEspecialidad.ValueMember = "idEspecialidad";
            cbEspecialidad.SelectedIndex = -1;
        }

        private void mostrarSeccion()
        {
            cbSeccion.DataSource = null;
            cbSeccion.DataSource = Seccion.CargarSeccion();
            cbSeccion.DisplayMember = "nombreSeccion";
            cbSeccion.ValueMember = "idSeccion";
            cbSeccion.SelectedIndex = -1;
        }

        private void mostrarProyecto()
        {
            cbProyecto.DataSource = null;
            cbProyecto.DataSource = Proyecto.CargarProyecto();
            cbProyecto.DisplayMember = "nombreProyecto";
            cbProyecto.ValueMember = "idProyecto";
            cbProyecto.SelectedIndex = -1;
        }

        #endregion
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                try
                {
                    dgvEstudianteEncontrado.DataSource = null;
                    dgvEstudianteEncontrado.DataSource = Estudiante.CargarBusqueda(txtBuscar.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Asegurese de ingresar un valor en el campo de búsqueda", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void txtBuscar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
        }

        private void frmResultado_Colaborador_Load(object sender, EventArgs e)
        {
            txtBuscar.Text = carnet;
            MostrarBusqueda();
            btnRegresar.Visible = false;
            RedondearPanel(pnlBienvenida, 40);

            dtpFechaBitacora.MinDate = DateTime.Today;
            dtpFechaBitacora.MaxDate = DateTime.Today;
            dtpFechaBitacora.Value = DateTime.Today;
        }

        private void btnLimpiarBitacora_Click(object sender, EventArgs e)
        {
            txtNumEstudiante.Text = "";
            txtHoras.Text = "";
            txtActvidad.Text = "";
            dtpFechaBitacora.Value = DateTime.Today;
        }

        private void btnLimpiarDatos_Click(object sender, EventArgs e)
        {
            txtNombre.Text = "";
            txtCarnet.Text = "";
            txtNie.Text = "";
            cbEspecialidad.Text = "";
            cbNivelAcademico.Text = "";
            cbSeccion.Text = "";
            cbProyecto.Text = "";
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (cbEspecialidad.SelectedIndex != -1 && cbNivelAcademico.SelectedIndex != -1 && cbSeccion.SelectedIndex != -1 && cbProyecto.SelectedIndex != -1 &&
                !string.IsNullOrWhiteSpace(txtCarnet.Text) && !string.IsNullOrWhiteSpace(txtNombre.Text))
            {

                Estudiante es = new Estudiante();
                es.Carnet = txtCarnet.Text;
                es.NombreEstudiante = txtNombre.Text;
                es.Nie = txtNie.Text;
                es.Especialidad = Convert.ToInt32(cbEspecialidad.SelectedValue);
                es.NivelAcademico = Convert.ToInt32(cbNivelAcademico.SelectedValue);
                es.Seccion = Convert.ToInt32(cbSeccion.SelectedValue);
                es.Proyecto = Convert.ToInt32(cbProyecto.SelectedValue);
                es.Estado = false;

                string registroEditar = "";

                if (dgvEstudianteEncontrado.CurrentRow == null)
                {
                    MessageBox.Show("Asegúrese de seleccionar un registro", "Advertencia");
                    return;
                }
                else
                {
                    es.Id = int.Parse(dgvEstudianteEncontrado.CurrentRow.Cells[0].Value.ToString());
                }
                DialogResult respuesta = MessageBox.Show("¿Quieres editar este registro?\n" + registroEditar,
                                                         "Editar registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    if (es.ActualizarEstudiantes() == true)
                    {
                        MostrarBusqueda();
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
            else
            {
                MessageBox.Show("Todos los campos, excepto <Nie>, necesitan ingresarse obligatoriamente ", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtHoras.Text) && !string.IsNullOrWhiteSpace(txtActvidad.Text))
            {
                //Creamos un objeto Bitacora
                BitacoraSocial bi = new BitacoraSocial();
                bi.RegistroHoras = Convert.ToInt32(txtHoras.Text);
                bi.Descripcion = txtActvidad.Text;
                bi.FechaBitacora = dtpFechaBitacora.Value;
                bi.IdEstudiante = Convert.ToInt32(txtNumEstudiante.Text);

                try
                {
                    bi.InsertarBitacoraSocial();

                    MostrarBitacora(Convert.ToInt32(txtNumEstudiante.Text));
                }
                catch (Exception)
                {

                    MessageBox.Show("Error al insgresar datos", "Advertencia");
                }
            }
            else
            {
                MessageBox.Show("Asegurese de llenar todos los campos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnVerBitacora_Click(object sender, EventArgs e)
        {
            if (dgvEstudianteEncontrado.CurrentRow == null || btnRegresar.Visible == true)
            {
                MessageBox.Show("Asegúrese de seleccionar un registro de estudiante", "Advertencia");
                return;
            }
            else
            {
                int idEstudiante = int.Parse(dgvEstudianteEncontrado.CurrentRow.Cells[0].Value.ToString());
                MostrarBitacora(idEstudiante);
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            MostrarBusqueda();
            btnRegresar.Visible = false;
        }

        private void btnEditarBitacora_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtHoras.Text) && !string.IsNullOrWhiteSpace(txtActvidad.Text))
            {
                BitacoraSocial bi = new BitacoraSocial();
                bi.RegistroHoras = Convert.ToInt32(txtHoras.Text);
                bi.Descripcion = txtActvidad.Text;
                bi.FechaBitacora = dtpFechaBitacora.Value;
                bi.IdEstudiante = Convert.ToInt32(txtNumEstudiante.Text);

                if (dgvEstudianteEncontrado.CurrentRow == null)
                {
                    MessageBox.Show("Asegúrese de seleccionar un registro", "Advertencia");
                    return;
                }
                else
                {
                    bi.IdBitacora = int.Parse(dgvEstudianteEncontrado.CurrentRow.Cells[0].Value.ToString());
                }

                string registroEditar = dgvEstudianteEncontrado.CurrentRow.Cells[2].Value?.ToString();
                DialogResult respuesta = MessageBox.Show("¿Quieres editar este registro?\n" + registroEditar,
                                                          "Editar registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    if (bi.ActualizarBitacora() == true)
                    {
                        MostrarBitacora(Convert.ToInt32(txtNumEstudiante.Text));
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
            else
            {
                MessageBox.Show("Asegurese de llenar todos los campos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEliminarBitacora_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvEstudianteEncontrado.CurrentRow == null || btnRegresar.Visible == false)
                {
                    MessageBox.Show("Asegúrese de seleccionar un registro", "Advertencia");
                    return;
                }

                BitacoraSocial bi = new BitacoraSocial();
                int id = int.Parse(dgvEstudianteEncontrado.CurrentRow.Cells[0].Value.ToString());

                string registroEliminar = dgvEstudianteEncontrado.CurrentRow.Cells[2].Value?.ToString();
                DialogResult respuesta = MessageBox.Show("¿Quieres eliminar este registro?\n" + registroEliminar,
                                                         "Eliminar registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    if (bi.EliminarBitacora(id) == true)
                    {
                        MessageBox.Show("Registro eliminado\n" + registroEliminar, "Eliminando", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        MostrarBitacora(Convert.ToInt32(txtNumEstudiante.Text));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return;
            }
        }

        #region "Método para cargar datos que se actualizaran"
        private void dgvEstudianteEncontrado_DoubleClick(object sender, EventArgs e)
        {
            if (btnRegresar.Visible == false)
            {
                txtNumEstudiante.Text = dgvEstudianteEncontrado.CurrentRow.Cells[0].Value.ToString();
                txtCarnet.Text = dgvEstudianteEncontrado.CurrentRow.Cells[1].Value.ToString();
                txtNombre.Text = dgvEstudianteEncontrado.CurrentRow.Cells[2].Value.ToString();
                txtNie.Text = dgvEstudianteEncontrado.CurrentRow.Cells[6].Value.ToString();
                cbEspecialidad.Text = dgvEstudianteEncontrado.CurrentRow.Cells[3].Value.ToString();
                cbNivelAcademico.Text = dgvEstudianteEncontrado.CurrentRow.Cells[4].Value.ToString();
                cbSeccion.Text = dgvEstudianteEncontrado.CurrentRow.Cells[5].Value.ToString();
                cbProyecto.Text = dgvEstudianteEncontrado.CurrentRow.Cells[8].Value.ToString();


            }
            else
            {
                txtNumEstudiante.Text = dgvEstudianteEncontrado.CurrentRow.Cells[4].Value.ToString();
                txtActvidad.Text = dgvEstudianteEncontrado.CurrentRow.Cells[2].Value.ToString();
                txtHoras.Text = dgvEstudianteEncontrado.CurrentRow.Cells[1].Value.ToString();

            }
        }
        #endregion

        #region Validaciones
        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Space))
            {
                MessageBox.Show("Solo permite letras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true;
                return;
            }
        }

        private void txtCarnet_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo permite números", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true;
                return;
            }
        }

        private void txtNie_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo permite números", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true;
                return;
            }
        }

        private void txtHoras_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo permite números", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true;
                return;
            }
        }

        private void txtActvidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Space))
            {
                MessageBox.Show("Solo permite letras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true;
                return;
            }
        }
        #endregion
    }
}
