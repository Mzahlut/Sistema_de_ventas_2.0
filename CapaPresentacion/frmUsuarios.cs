using CapaEntidad;
using CapaNeogcio;
using CapaPresentacion.Utilities;
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
    public partial class frmUsuarios : Form
    {
        public frmUsuarios()
        {
            InitializeComponent();
        }

        private void frmUsuarios_Load(object sender, EventArgs e)
        {
            //Poniendo los dos estados en el formulario
            cboEstado.Items.Add(new OpcionCombo() {Valor = 1, Texto = "Activo" });
            cboEstado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "No Activo" });
            cboEstado.DisplayMember = "Texto";
            cboEstado.ValueMember = "Valor";
            cboEstado.SelectedIndex = 0;


            List<Rol> listaRoles = new CN_Rol().listar();    

            foreach (Rol role in listaRoles)
            {
                cboRol.Items.Add(new OpcionCombo() { Valor = role.IdRol, Texto = role.Descripcion });
            }

            cboRol.DisplayMember = "Texto";
            cboRol.ValueMember = "Valor";
            cboRol.SelectedIndex = 0;

            foreach (DataGridViewColumn column in dataGridUsers.Columns)
            {

                if (column.Visible == true && column.Name != "btnSeleccionar")
                {
                    cboSearch.Items.Add(new OpcionCombo() { Valor = column.Name, Texto = column.HeaderText });
                }

                cboSearch.DisplayMember = "Texto";
                cboSearch.ValueMember = "Valor";
               


            }


        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            dataGridUsers.Rows.Add(new object[]
            {
                "",
                txtId.Text,
                txtDocumento.Text,
                txtNombre.Text,
                txtCorreo.Text,
                txtClave.Text,
                ((OpcionCombo)cboRol.SelectedItem).Valor.ToString(),
                ((OpcionCombo)cboRol.SelectedItem).Texto.ToString(),
                ((OpcionCombo)cboEstado.SelectedItem).Valor.ToString(),
                ((OpcionCombo)cboEstado.SelectedItem).Texto.ToString()

            });

            clear();

        }


        private void clear()
        {
            txtId.Text = "0";
            txtDocumento.Text = "";
            txtNombre.Text = "";
            txtCorreo.Text = "";
            txtClave.Text = "";
            txtConfirmClave.Text = "";
            cboEstado.SelectedIndex = 0;
            cboRol.SelectedIndex = 0;


        }

    }
}
