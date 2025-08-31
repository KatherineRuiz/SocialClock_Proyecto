namespace Vistas.Formularios
{
    partial class frmEstudiante
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnEstadisticas = new System.Windows.Forms.Button();
            this.btnProyectos = new System.Windows.Forms.Button();
            this.btnHome = new System.Windows.Forms.Button();
            this.btnAjustes = new System.Windows.Forms.Button();
            this.pnlBusqueda = new System.Windows.Forms.Panel();
            this.pnlContenido = new System.Windows.Forms.Panel();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlButtons
            // 
            this.pnlButtons.BackgroundImage = global::Vistas.Properties.Resources.Captura_de_pantalla_2025_07_03_114908;
            this.pnlButtons.Controls.Add(this.btnEstadisticas);
            this.pnlButtons.Controls.Add(this.btnProyectos);
            this.pnlButtons.Controls.Add(this.btnHome);
            this.pnlButtons.Controls.Add(this.btnAjustes);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlButtons.Location = new System.Drawing.Point(0, 0);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(246, 1030);
            this.pnlButtons.TabIndex = 0;
            // 
            // btnEstadisticas
            // 
            this.btnEstadisticas.BackgroundImage = global::Vistas.Properties.Resources.Captura_de_pantalla_2025_07_03_114908;
            this.btnEstadisticas.FlatAppearance.BorderSize = 0;
            this.btnEstadisticas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEstadisticas.Image = global::Vistas.Properties.Resources.graficos;
            this.btnEstadisticas.Location = new System.Drawing.Point(44, 804);
            this.btnEstadisticas.Name = "btnEstadisticas";
            this.btnEstadisticas.Size = new System.Drawing.Size(101, 85);
            this.btnEstadisticas.TabIndex = 3;
            this.btnEstadisticas.UseVisualStyleBackColor = true;
            // 
            // btnProyectos
            // 
            this.btnProyectos.BackgroundImage = global::Vistas.Properties.Resources.Captura_de_pantalla_2025_07_03_114908;
            this.btnProyectos.FlatAppearance.BorderSize = 0;
            this.btnProyectos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProyectos.Image = global::Vistas.Properties.Resources.proyectos;
            this.btnProyectos.Location = new System.Drawing.Point(44, 567);
            this.btnProyectos.Name = "btnProyectos";
            this.btnProyectos.Size = new System.Drawing.Size(118, 120);
            this.btnProyectos.TabIndex = 2;
            this.btnProyectos.UseVisualStyleBackColor = true;
            // 
            // btnHome
            // 
            this.btnHome.BackgroundImage = global::Vistas.Properties.Resources.Captura_de_pantalla_2025_07_03_114908;
            this.btnHome.FlatAppearance.BorderSize = 0;
            this.btnHome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHome.Image = global::Vistas.Properties.Resources.home_;
            this.btnHome.Location = new System.Drawing.Point(27, 277);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(135, 141);
            this.btnHome.TabIndex = 1;
            this.btnHome.UseVisualStyleBackColor = true;
            // 
            // btnAjustes
            // 
            this.btnAjustes.BackgroundImage = global::Vistas.Properties.Resources.Captura_de_pantalla_2025_07_03_114908;
            this.btnAjustes.FlatAppearance.BorderSize = 0;
            this.btnAjustes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAjustes.Image = global::Vistas.Properties.Resources.ajustes;
            this.btnAjustes.Location = new System.Drawing.Point(44, 68);
            this.btnAjustes.Name = "btnAjustes";
            this.btnAjustes.Size = new System.Drawing.Size(108, 113);
            this.btnAjustes.TabIndex = 0;
            this.btnAjustes.UseVisualStyleBackColor = true;
            // 
            // pnlBusqueda
            // 
            this.pnlBusqueda.BackgroundImage = global::Vistas.Properties.Resources.Captura_de_pantalla_2025_07_03_114908;
            this.pnlBusqueda.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBusqueda.Location = new System.Drawing.Point(246, 0);
            this.pnlBusqueda.Name = "pnlBusqueda";
            this.pnlBusqueda.Size = new System.Drawing.Size(1018, 42);
            this.pnlBusqueda.TabIndex = 1;
            // 
            // pnlContenido
            // 
            this.pnlContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContenido.Location = new System.Drawing.Point(246, 42);
            this.pnlContenido.Name = "pnlContenido";
            this.pnlContenido.Size = new System.Drawing.Size(1018, 988);
            this.pnlContenido.TabIndex = 2;
            // 
            // frmEstudiante
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Vistas.Properties.Resources.Captura_de_pantalla_2025_07_03_114908;
            this.ClientSize = new System.Drawing.Size(1264, 1030);
            this.Controls.Add(this.pnlContenido);
            this.Controls.Add(this.pnlBusqueda);
            this.Controls.Add(this.pnlButtons);
            this.Name = "frmEstudiante";
            this.Text = "frmEstudiante";
            this.Load += new System.EventHandler(this.frmEstudiante_Load);
            this.pnlButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnAjustes;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.Button btnProyectos;
        private System.Windows.Forms.Button btnEstadisticas;
        private System.Windows.Forms.Panel pnlBusqueda;
        private System.Windows.Forms.Panel pnlContenido;
    }
}