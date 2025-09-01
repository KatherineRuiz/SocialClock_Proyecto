using Modelos;
using Modelos.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Vistas.Formularios
{
    public partial class frmListaProyectos : Form
    {
        int proyectoseleccionado;
        public frmListaProyectos()
        {
            InitializeComponent();
            RedondearPanel(pnlEncabezado, 40);
            RedondearPanel(pnlListadoProyectos, 40);
            MostrarProyecto();
            lblEstado.Visible = false;
            rbnActivo.Visible = false;
            rbnInactivo.Visible = false;
            tpEstudiantesProyecto.Visible = false;
            btnRegresarEstudiantes.Visible = false;
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
       
        private void MostrarProyecto()
        {
            dgvContenido.DataSource = null;
            dgvContenido.DataSource = Proyecto.CargarTodosProyectos();
        }

        private void MostrarBitacora(int idEstudiante)
        {
            dgvBitacoraEstudiantes.DataSource = null;
            dgvBitacoraEstudiantes.DataSource = BitacoraSocial.CargarBitacoraSocial(idEstudiante);
            btnRegresarEstudiantes.Visible = true;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Proyecto p = new Proyecto();
            p.NombreProyecto = txtNombreProyecto.Text;
            if (rbnActivo.Checked == true)
            {
                p.EstadoProyecto = false;
            }
            else
            {
                p.EstadoProyecto = true;

            }

            p.InsertarProyecto();
            MessageBox.Show("Exito", "Datos ingresados correctamente", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MostrarProyecto();

        }


        // Un clic para rellenar los datos
        private void dgvContenido_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtNombreProyecto.Text = dgvContenido.CurrentRow.Cells[1].Value.ToString();
            if (dgvContenido.CurrentRow.Cells[2].Value.ToString() == "ACTIVO")
            {
                rbnActivo.Checked = true;
            }
            else
            {
                rbnInactivo.Checked = true;

            }
            lblEstado.Visible = true;
            rbnActivo.Visible = true;
            rbnInactivo.Visible = true;
        }

        // Doble clic para ir a la otra pestaña
        private void dgvContenido_DoubleClick(object sender, EventArgs e)
        {
            tpEstudiantesProyecto.Visible = true;
            tcProyectos.SelectedTab = tpEstudiantesProyecto;
            try
            {
                dgvBitacoraEstudiantes.DataSource = null;
                dgvBitacoraEstudiantes.DataSource = Estudiante.CargarEstudianteProyecto(Convert.ToInt32(dgvContenido.CurrentRow.Cells[0].Value.ToString()));
                proyectoseleccionado = Convert.ToInt32(dgvContenido.CurrentRow.Cells[0].Value.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void btnAgregar_Click_1(object sender, EventArgs e)
        {
            Proyecto p = new Proyecto();
            p.NombreProyecto = txtNombreProyecto.Text;
            if (rbnActivo.Checked == true)
            {
                p.EstadoProyecto = false;
            }
            else
            {
                p.EstadoProyecto = true;

            }

            p.InsertarProyecto();
            MessageBox.Show("Exito", "Datos ingresados correctamente", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MostrarProyecto();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Proyecto p = new Proyecto();
            p.NombreProyecto = txtNombreProyecto.Text;
            if (rbnActivo.Checked == true)
            {
                p.EstadoProyecto = false;
            }
            else
            {
                p.EstadoProyecto = true;

            }
            p.IdProyecto = int.Parse(dgvContenido.CurrentRow.Cells[0].Value.ToString());
            if(p.ActualizarProyectos()==true)
            {
                MessageBox.Show("Datos Actualizados correctamente","Exito");
                MostrarProyecto();
            }
            else
            {
                MessageBox.Show("Hubo un error , intenta de nuevo.","Error");
            }
        }


        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNombreProyecto.Text = "";
            lblEstado.Visible = false;
            rbnActivo.Visible = false;
            rbnInactivo.Visible = false;
        }

        private void frmListaProyectos_Load(object sender, EventArgs e)
        {
            tpEstudiantesProyecto.Visible = false;
            btnRegresarEstudiantes.Visible = false;
        }

        private void txtBuscar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
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


        private void btnLimpiarBitacora_Click(object sender, EventArgs e)
        {
            txtNumEstudiante.Text = "";
            txtHoras.Text = "";
            txtActvidad.Text = "";
        }

        private void btnEliminarProyecto_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (dgvContenido.CurrentRow == null)
                {
                    MessageBox.Show("Asegúrese de seleccionar un registro", "Advertencia");
                    return;
                }

                Proyecto pr = new Proyecto();
                int id = int.Parse(dgvContenido.CurrentRow.Cells[0].Value.ToString());

                string registroEliminar = dgvContenido.CurrentRow.Cells[1].Value?.ToString();
                DialogResult respuesta = MessageBox.Show("¿Quieres eliminar este registro?\n" + registroEliminar,
                                                         "Eliminar registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    if (pr.EliminarProyectos(id) == true)
                    {
                        MessageBox.Show("Registro eliminado\n" + registroEliminar, "Eliminando", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        MostrarProyecto();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return;
            }
            
        }


        private void btnRegresarEstudiantes_Click(object sender, EventArgs e)
        {
            tcProyectos.SelectedTab = tpListadoProyectos;
            dgvBitacoraEstudiantes.DataSource = null;
            dgvBitacoraEstudiantes.DataSource = Estudiante.CargarEstudianteProyecto(proyectoseleccionado);
            btnRegresarEstudiantes.Visible = false;
        }


        private void btnVerBitacora_Click(object sender, EventArgs e)
        {
            if (dgvBitacoraEstudiantes.CurrentRow == null || btnRegresarEstudiantes.Visible == true)
            {
                MessageBox.Show("Asegúrese de seleccionar un registro de estudiante", "Advertencia");
                return;
            }
            else
            {
                int idEstudiante = int.Parse(dgvBitacoraEstudiantes.CurrentRow.Cells[0].Value.ToString());
                MostrarBitacora(idEstudiante);
            }
        }

        private void btnBusqueda_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                try
                {
                    dgvContenido.DataSource = null;
                    dgvContenido.DataSource = Proyecto.BuscarProyecto(txtBuscar.Text);
                }
                catch (Exception)
                {
                    return;
                }
            }
            else
            {
                MessageBox.Show("Asegurese de ingresar un valor en el campo de búsqueda", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvBitacoraEstudiantes_DoubleClick(object sender, EventArgs e)
        {
            if (btnRegresarEstudiantes.Visible == false)
            {
                txtNumEstudiante.Text = dgvBitacoraEstudiantes.CurrentRow.Cells[0].Value.ToString();
                
            }
            else
            {
                txtNumEstudiante.Text = dgvBitacoraEstudiantes.CurrentRow.Cells[4].Value.ToString();
                txtActvidad.Text = dgvBitacoraEstudiantes.CurrentRow.Cells[2].Value.ToString();
                txtHoras.Text = dgvBitacoraEstudiantes.CurrentRow.Cells[1].Value.ToString();

            }
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

                if (dgvBitacoraEstudiantes.CurrentRow == null)
                {
                    MessageBox.Show("Asegúrese de seleccionar un registro", "Advertencia");
                    return;
                }
                else
                {
                    bi.IdBitacora = int.Parse(dgvBitacoraEstudiantes.CurrentRow.Cells[0].Value.ToString());
                }

                string registroEditar = dgvBitacoraEstudiantes.CurrentRow.Cells[2].Value?.ToString();
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
                if (dgvBitacoraEstudiantes.CurrentRow == null || btnRegresarEstudiantes.Visible == false)
                {
                    MessageBox.Show("Asegúrese de seleccionar un registro de bitacora", "Advertencia");
                    return;
                }

                BitacoraSocial bi = new BitacoraSocial();
                int id = int.Parse(dgvBitacoraEstudiantes.CurrentRow.Cells[0].Value.ToString());

                string registroEliminar = dgvBitacoraEstudiantes.CurrentRow.Cells[2].Value?.ToString();
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

        private void btnBuscarEstudiante_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                try
                {
                    dgvBitacoraEstudiantes.DataSource = null;
                    dgvBitacoraEstudiantes.DataSource = Estudiante.BuscarEstudianteProyecto(txtBuscar.Text, proyectoseleccionado);
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
    }
}
