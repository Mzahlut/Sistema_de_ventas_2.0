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
    public partial class frmClientes : Form
    {
        public frmClientes()
        {
            InitializeComponent();
        }

        private void frmClientes_Load(object sender, EventArgs e)
        {
            cboEstado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Activo" });
            cboEstado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "No Activo" });
            cboEstado.DisplayMember = "Texto";
            cboEstado.ValueMember = "Valor";
            cboEstado.SelectedIndex = 0;


            foreach (DataGridViewColumn column in dataGridClient.Columns)
            {

                if (column.Visible == true && column.Name != "btnSeleccionar")
                {
                    cboSearch.Items.Add(new OpcionCombo() { Valor = column.Name, Texto = column.HeaderText });
                }

                cboSearch.DisplayMember = "Texto";
                cboSearch.ValueMember = "Valor";



            }

            //Muestra los datos en el panel
            List<Cliente> listaCliente = new CN_Cliente().listar();

            foreach (Cliente user in listaCliente)
            {
                dataGridClient.Rows.Add(new object[]
          {
                "",
                user.IdCliente,
                user.Documento,
                user.NombreCompleto,
                user.Correo,
                user.Telefono,
                user.Estado == true? 1 : 0,
                user.Estado == true? "Activo" : "No Activo"

          });
            }
        

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            string Mensaje = string.Empty;

            Cliente cliente = new Cliente()
            {
                IdCliente = Convert.ToInt32(txtId.Text),
                Documento = txtDocumento.Text,
                NombreCompleto = txtNombre.Text,
                Correo = txtCorreo.Text,
                Telefono = txtTelefono.Text,
                Estado = Convert.ToInt32(((OpcionCombo)cboEstado.SelectedItem).Valor) == 1 ? true : false
            };


            if (cliente.IdCliente == 0)
            {

                int idGenerado = new CN_Cliente().Register(cliente, out Mensaje);


                if (idGenerado != 0)
                {

                    dataGridClient.Rows.Add(new object[]
                    {
                "",
                idGenerado,
                txtDocumento.Text,
                txtNombre.Text,
                txtCorreo.Text,
                txtTelefono.Text,
                ((OpcionCombo)cboEstado.SelectedItem).Valor.ToString(),
                ((OpcionCombo)cboEstado.SelectedItem).Texto.ToString()


                    });
                    clear();
                }
                else
                {
                    MessageBox.Show(Mensaje);
                }

            }
            else
            {

                bool resultado = new CN_Cliente().Edit(cliente, out Mensaje);


                if (resultado)
                {
                    DataGridViewRow row = dataGridClient.Rows[Convert.ToInt32(txtIndice.Text)];
                    row.Cells["Id"].Value = txtId.Text;
                    row.Cells["Documento"].Value = txtDocumento.Text;
                    row.Cells["NombreCompleto"].Value = txtNombre.Text;
                    row.Cells["Correo"].Value = txtCorreo.Text;
                    row.Cells["Telefono"].Value = txtTelefono.Text;
                    row.Cells["EstadoValor"].Value = ((OpcionCombo)cboEstado.SelectedItem).Valor.ToString();
                    row.Cells["Estado"].Value = ((OpcionCombo)cboEstado.SelectedItem).Texto.ToString();

                    clear();
                }
                else
                {
                    MessageBox.Show(Mensaje);
                }


            }


        }

        private void clear()
        {
            txtIndice.Text = "-1";
            txtId.Text = "0";
            txtDocumento.Text = "";
            txtNombre.Text = "";
            txtCorreo.Text = "";
            txtTelefono.Text = "";
            cboEstado.SelectedIndex = 0;
            txtDocumento.Select();


        }

        private void dataGridUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dataGridClient.Columns[e.ColumnIndex].Name == "btnSeleccionar")
            {

                int index = e.RowIndex;


                if (index >= 0)
                {
                    txtIndice.Text = index.ToString();
                    txtId.Text = dataGridClient.Rows[index].Cells["Id"].Value.ToString();
                    txtDocumento.Text = dataGridClient.Rows[index].Cells["Documento"].Value.ToString();
                    txtNombre.Text = dataGridClient.Rows[index].Cells["NombreCompleto"].Value.ToString();
                    txtCorreo.Text = dataGridClient.Rows[index].Cells["Correo"].Value.ToString();
                    txtTelefono.Text = dataGridClient.Rows[index].Cells["Telefono"].Value.ToString();
                

                    foreach (OpcionCombo oc in cboEstado.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dataGridClient.Rows[index].Cells["EstadoValor"].Value))
                        {

                            int index_combo = cboEstado.Items.IndexOf(oc);
                            cboEstado.SelectedIndex = index_combo;
                            break;

                        }
                    }


                }


            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtId.Text) != 0)
            {
                if (MessageBox.Show("¿Desea eliminar el CLIENTE?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string Mensaje = string.Empty;

                    Cliente cliente  = new Cliente()
                    {
                        IdCliente = Convert.ToInt32(txtId.Text)
                    };


                    bool respuesta = new CN_Cliente().Delete(cliente, out Mensaje);


                    if (respuesta)
                    {
                        dataGridClient.Rows.RemoveAt(Convert.ToInt32(txtIndice.Text));
                    }
                    else
                    {
                        MessageBox.Show(Mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    };

                }
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string columnFilter = ((OpcionCombo)cboSearch.SelectedItem).Valor.ToString();

            if (dataGridClient.Rows.Count > 0)
            {

                foreach (DataGridViewRow row in dataGridClient.Rows)
                {
                    if (row.Cells[columnFilter].Value.ToString().Trim().ToUpper().Contains(txtSearch.Text.Trim().ToUpper()))
                    {

                        row.Visible = true;

                    }
                    else

                        row.Visible = false;

                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";

            foreach (DataGridViewRow row in dataGridClient.Rows)
            {
                row.Visible = true;
            }
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            clear();
        }




        //ACA TERMINA LA CLASE
    }
}
