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
    public partial class frmSocialClock_Colaborador : Form
    {
        public frmSocialClock_Colaborador()
        {
            InitializeComponent();
        }

        private void frmSocialClock_Colaborador_Load(object sender, EventArgs e)
        {
            RedondearPanel(pnlContenido, 50);
            abrirForm(new frmInicio_Colaborador());
        }
        //Metodo para redondear las esquinas de los paneles
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



        #region "Metodo para pintar formularios"
        //Creamos un atributo
        private Form activarForm = null;

        private void abrirForm(Form formularioPintar)
        {
            if (activarForm != null)
            //Si existe un formulario abierto, se cerrará
            {
                activarForm.Close();
            }
            //Le damos todos los permisos que tiene la clase form
            activarForm = formularioPintar;
            //Convertimos a un hijo de tipo de form
            formularioPintar.TopLevel = false;
            //Quitamos los bordes
            formularioPintar.FormBorderStyle = FormBorderStyle.None;
            formularioPintar.Dock = DockStyle.Fill;

            pnlContenido.Controls.Add(formularioPintar);
            formularioPintar.BringToFront();
            formularioPintar.Show();
        }
        #endregion
        private void btnInicio_Click(object sender, EventArgs e)
        {
            abrirForm(new frmInicio_Colaborador());
        }


        private void btnSalir_Click_1(object sender, EventArgs e)
        {
            frmSeleccionDeRol se = new frmSeleccionDeRol();
            se.Show();
            this.Close();
        }

        private void btnProyecto_Click(object sender, EventArgs e)
        {
            abrirForm(new frmProyectos_Colaborador());
        }
    }
}
