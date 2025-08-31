using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Vistas.Clases
{
    public class RedondearBoton : Button
    {
        //Declarar campos para la apariencia y asignar sus valores predeterminados 
        private int borderSize = 0;
        private int borderRadius = 30;
        private Color borderColor = Color.Aquamarine;

        //Cada vez que se asigne un valor a la propiedad vamos a redibujar el botón 
        public int BorderSize
        {
            get
            {
                return borderSize;
            }
            set
            {
                borderSize = value;
                this.Invalidate();
            }
        }
        public int BorderRadius
        {
            get
            {
                return borderRadius;

            }
            set
            {
                borderRadius = value;
                this.Invalidate();
            }
        }
        public Color BorderColor
        {
            get
            {
                return borderColor;
            }
            set
            {
                borderColor = value;
                this.Invalidate();
            }
        }


        //Crear un constructor 
        public RedondearBoton()
        {
        }

        //Creamos un metodo privado para obtener la ruta de graficos para el diseño del boton con bordes redondeados
        private GraphicsPath GetFigurePath(RectangleF rect, float radius)
        {
            //Creamos un objeto Ruta de Graficos
            GraphicsPath path = new GraphicsPath();
            //Iniciamos la figura de la ruta 
            path.StartFigure();
            //Agregamos un arco en el eje inicial del rectangulo igual al radio 
            //Comenzamos en un ángulo de 180 grados con angulo de recorrido de 90 grados 
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            //Agregamos otro arco en la esquina superior derecha 
            path.AddArc(rect.Width - radius, rect.Y, radius, radius, 270, 90);
            //Agregamos otro arco en la esquina inferior derecha 
            path.AddArc(rect.Width - radius, rect.Height - radius, radius, radius, 0, 90);
            //Agregamos otro arco en la esquina inferior izquierda 
            path.AddArc(rect.X, rect.Height - radius, radius, radius, 90, 90);
            //Finalizamos la figura y retornamos la ruta de grafios 
            path.CloseFigure();
            return path;

        }
        //Anulamos el evento OnPaint del boton para ampliar la funcionalidad
        protected override void OnPaint(PaintEventArgs pevent)
        {
            //Establecemos el modo de suavizado de la pintura en AntiAlias
            base.OnPaint(pevent);
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            //Creamos un objeto rectangulo de valor flotante para la superficie del boton
            RectangleF rectSurface = new RectangleF(0, 0, this.Width, this.Height);
            RectangleF rectBorder = new RectangleF(1, 1, this.Width - 0.8F, this.Height - 1);

            //Si el radio del boton es mayor que dos será un boton con esquinas redondeadas
            if (BorderRadius > 2)
            {
                using (GraphicsPath pathSurface = GetFigurePath(rectSurface, BorderRadius))
                using (GraphicsPath pathBorder = GetFigurePath(rectBorder, BorderRadius - 1F))
                using (Pen penSurface = new Pen(this.Parent.BackColor, 2))
                using (Pen penBorder = new Pen(BorderColor, BorderSize))
                {
                    //Establecemos la alineación del boligrafo borde en inset
                    penBorder.Alignment = PenAlignment.Inset;
                    //Establecemos una nueva región para el boton
                    this.Region = new Region(pathSurface);
                    //Dibujamos la superficie y los bordes del boton para una buena calidad
                    pevent.Graphics.DrawPath(penSurface, pathSurface);
                    if (BorderSize >= 1)
                    {
                        pevent.Graphics.DrawPath(penBorder, pathBorder);
                    }
                }
            }
            else
            {
            }
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            this.Parent.BackColorChanged += new EventHandler(Container_BackColorChanged);
        }
        //Declaramos una condición para que el boton cambie de color si el contenedor lo hace
        private void Container_BackColorChanged(object sender, EventArgs e)
        {
            if (this.DesignMode)
                this.Invalidate();
        }
    }
}

























