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
    public partial class frmCategoria : Form
    {
        public frmCategoria()
        {
            InitializeComponent();
        }

        private void frmCategoria_Load(object sender, EventArgs e)
        {

            // Poniendo los dos estados en el formulario
            cboEstado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Activo" });
            cboEstado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "No Activo" });
            cboEstado.DisplayMember = "Texto";
            cboEstado.ValueMember = "Valor";
            cboEstado.SelectedIndex = 0;



            foreach (DataGridViewColumn column in dataGridUsers.Columns)
            {

                if (column.Visible == true && column.Name != "btnSeleccionar")
                {
                    cboSearch.Items.Add(new OpcionCombo() { Valor = column.Name, Texto = column.HeaderText });
                }

                cboSearch.DisplayMember = "Texto";
                cboSearch.ValueMember = "Valor";



            }

            //Muestra los datos en el panel
            List<Categoria> listaCategoria = new CN_Categoria().listar();

            foreach (Categoria categoria in listaCategoria)
            {
                dataGridUsers.Rows.Add(new object[]
          {
                "",
                categoria.IdCategoria,
                categoria.Descripcion,
                categoria.Estado == true? "Activo" : "No Activo"

          });
            }


        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string Mensaje = string.Empty;

            Categoria categoria = new Categoria()
            {
                IdCategoria = Convert.ToInt32(txtId.Text),
                Descripcion = txtDescripcion.Text,
                Estado = Convert.ToInt32(((OpcionCombo)cboEstado.SelectedItem).Valor) == 1 ? true : false
            };


            if (categoria.IdCategoria == 0)
            {

                int idGenerado = new CN_Categoria().Register(categoria, out Mensaje);


                if (idGenerado != 0)
                {

                    dataGridUsers.Rows.Add(new object[]
                    {
                "",
                idGenerado,
                txtDescripcion.Text,
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

                bool resultado = new CN_Categoria().Edit(categoria, out Mensaje);


                if (resultado)
                {
                    DataGridViewRow row = dataGridUsers.Rows[Convert.ToInt32(txtIndice.Text)];
                    row.Cells["IdCategoria"].Value = txtId.Text;
                    row.Cells["Descripcion"].Value = txtDescripcion.Text;
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
            txtDescripcion.Text = "";
            cboEstado.SelectedIndex = 0;
            txtDescripcion.Select();
        }

        private void dataGridUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dataGridUsers.Columns[e.ColumnIndex].Name == "btnSeleccionar")
            {

                int index = e.RowIndex;


                if (index >= 0)
                {
                    txtIndice.Text = index.ToString();
                    txtId.Text = dataGridUsers.Rows[index].Cells["IdCategoria"].Value.ToString();
                    txtDescripcion.Text = dataGridUsers.Rows[index].Cells["Descripcion"].Value.ToString();

                    foreach (OpcionCombo oc in cboEstado.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dataGridUsers.Rows[index].Cells["EstadoValor"].Value))
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
                if (MessageBox.Show("¿Desea eliminar la categoria?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string Mensaje = string.Empty;

                    Categoria categoria = new Categoria()
                    {
                        IdCategoria = Convert.ToInt32(txtId.Text)
                    };


                    bool respuesta = new CN_Categoria().Delete(categoria, out Mensaje);


                    if (respuesta)
                    {
                        dataGridUsers.Rows.RemoveAt(Convert.ToInt32(txtIndice.Text));

                        clear();
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

           


            if (dataGridUsers.Rows.Count > 0)
            {

                foreach (DataGridViewRow row in dataGridUsers.Rows)
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

            foreach (DataGridViewRow row in dataGridUsers.Rows)
            {
                row.Visible = true;
            }
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            clear();
        }
    }
}
