using CapaEntidad;
using CapaNeogcio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class frmNegocio : Form
    {
        public frmNegocio()
        {
            InitializeComponent();
        }


        public Image ByteImage(byte[] imageBytes)
        {
            MemoryStream ms = new MemoryStream();
            ms.Write(imageBytes, 0,imageBytes.Length);
            Image image = new Bitmap(ms);

            return image;

        }



        private void frmNegocio_Load(object sender, EventArgs e)
        {
            bool obtenido = true;
            byte[] image = new CN_Negocio().ObtenerLogo(out obtenido);

            if(obtenido)
            {
                picLogo.Image = ByteImage(image);
            }

            Negocio datos = new CN_Negocio().ObtenerDatos();

            txtNombreNegocio.Text = datos.NombreNegocio;
            txtDireccion.Text = datos.Direccion;
            txtRuc.Text = datos.Ruc;


        }

        private void btnUpload_Click(object sender, EventArgs e)
        {

            string mensaje = string.Empty;

            OpenFileDialog ofd = new OpenFileDialog();

            ofd.FileName = "Files | *.jpg;*.jpeg;*.png";

            if(ofd.ShowDialog() == DialogResult.OK)
            {
                byte[] byteImage = File.ReadAllBytes(ofd.FileName);
                bool respuesta = new CN_Negocio().ActualizarLogo(byteImage, out mensaje);

                if (respuesta)
                {
                    picLogo.Image = ByteImage(byteImage);
                }
                else
                {
                    MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }

            



        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            Negocio obj = new Negocio()
            {

                NombreNegocio = txtNombreNegocio.Text,
                Ruc = txtRuc.Text,
                Direccion = txtDireccion.Text
            };

            bool respuesta = new CN_Negocio().GuardarDatos(obj, out mensaje);

            if (respuesta)
            {
                MessageBox.Show("Los cambios fueron guardados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Los cambios no pudieron guardarse", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
    }
}
