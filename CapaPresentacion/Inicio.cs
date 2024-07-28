using CapaEntidad;
using CapaNeogcio;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class Inicio : Form
    {


        private static IconMenuItem menuActivo = null;
        private static Form formularioActivo = null;
        private static Usuario usuarioActual = null;

        public Inicio(Usuario u)
        {
            usuarioActual = u;
            InitializeComponent();
        }

        private void iconMenuItem1_Click(object sender, EventArgs e)
        {
            
        }

        private void Inicio_Load(object sender, EventArgs e)
        {
            List<Permiso> listaPermiso = new CN_Permiso().listar(usuarioActual.IdUsuario);


            foreach(IconMenuItem i in Menu.Items)
            {
                bool encontrado = listaPermiso.Any(m => m.NombreMenu == i.Name);

                if(encontrado == false)
                {
                    i.Visible = false;
                }


            }



            lblUsuario.Text = usuarioActual.NombreCompleto.ToString();

        }


        private void abrirFormulario(IconMenuItem menu, Form formulario)
        {
            if (menuActivo != null)
            {
                menuActivo.BackColor = Color.White;
            }

            menu.BackColor = Color.Silver;
            menuActivo = menu;
        
            if(formularioActivo != null)
            {
                formularioActivo.Close();
            }

            formularioActivo = formulario;
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            formulario.BackColor = Color.SteelBlue;

            contenedor.Controls.Add(formulario);
            formulario.Show();


            
        }


        private void menuUsuario_Click(object sender, EventArgs e)
        {

            abrirFormulario((IconMenuItem)sender, new frmUsuarios());

        }

        private void subMenuCategoria_Click(object sender, EventArgs e)
        {
            abrirFormulario(menuConfiguracion, new frmCategoria());
        }

        private void subMenuProducto_Click(object sender, EventArgs e)
        {
            abrirFormulario(menuConfiguracion, new frmProducto());
        }

        private void subMenuRegistrarVenta_Click(object sender, EventArgs e)
        {

            abrirFormulario(menuVentas, new frmVentas());

        }

        private void subMenuVerDetalleVenta_Click(object sender, EventArgs e)
        {
            abrirFormulario(menuVentas, new frmDetalleVenta());
        }

        private void subMenuRegistrarCompra_Click(object sender, EventArgs e)
        {

            abrirFormulario(menuCompras, new frmCompras());

        }

        private void subMenuVerDetalleCompra_Click(object sender, EventArgs e)
        {

            abrirFormulario(menuCompras, new frmDetalleCompra());

        }

        private void menuClientes_Click(object sender, EventArgs e)
        {

            abrirFormulario((IconMenuItem)sender, new frmClientes());

        }

        private void menuProovedores_Click(object sender, EventArgs e)
        {

            abrirFormulario((IconMenuItem)sender, new frmProovedores());

        }

        private void menuReportes_Click(object sender, EventArgs e)
        {

            abrirFormulario((IconMenuItem)sender, new frmReportes());

        }
    }
}
