using CapaEntidad;
using CapaNeogcio;
using CapaPresentacion.Utilities;
using ClosedXML.Excel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SaveFileDialog = System.Windows.Forms.SaveFileDialog;

namespace CapaPresentacion
{
    public partial class frmProducto : Form
    {
        public frmProducto()
        {
            InitializeComponent();
        }

        private void frmProducto_Load(object sender, EventArgs e)
        {


            //Poniendo los dos estados en el formulario
            cboEstado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Activo" });
            cboEstado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "No Activo" });
            cboEstado.DisplayMember = "Texto";
            cboEstado.ValueMember = "Valor";
            cboEstado.SelectedIndex = 0;
            txtSearch.Select();
            

            List<Categoria> listaCategoria = new CN_Categoria().listar();

            foreach (Categoria category in listaCategoria)
            {
                cboCategoria.Items.Add(new OpcionCombo() { Valor = category.IdCategoria, Texto = category.Descripcion });
            }

            cboCategoria.DisplayMember = "Texto";
            cboCategoria.ValueMember = "Valor";
            cboCategoria.SelectedIndex = 0;

            foreach (DataGridViewColumn column in dataGridProducts.Columns)
            {

                if (column.Visible == true && column.Name != "btnSeleccionar")
                {
                    cboSearch.Items.Add(new OpcionCombo() { Valor = column.Name, Texto = column.HeaderText });
                }

                cboSearch.DisplayMember = "Texto";
                cboSearch.ValueMember = "Valor";



            }

            //Muestra los datos en el panel
            List<Producto> listaProductos = new CN_Producto().listar();

            foreach (Producto product in listaProductos)
            {
                dataGridProducts.Rows.Add(new object[]
          {
                "",
                product.IdProducto,
                product.Codigo,
                product.Nombre,
                product.Descripcion,
                product.oCategoria.IdCategoria,
                product.oCategoria.Descripcion,
                product.Stock,
                product.PrecioCompra,
                product.PrecioVenta,
                product.Estado == true? 1 : 0,
                product.Estado == true? "Activo" : "No Activo"

          });
            }



        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string Mensaje = string.Empty;

            Producto producto = new Producto()
            {
                IdProducto = Convert.ToInt32(txtId.Text),
                Codigo = txtCodigo.Text,
                Nombre = txtNombre.Text,
                Descripcion = txtDescripcion.Text,
                oCategoria = new Categoria() { IdCategoria = Convert.ToInt32(((OpcionCombo)cboCategoria.SelectedItem).Valor) },
                Estado = Convert.ToInt32(((OpcionCombo)cboEstado.SelectedItem).Valor) == 1 ? true : false
            };


            if (producto.IdProducto == 0)
            {

                int IdProducto = new CN_Producto().Register(producto, out Mensaje);


                if (IdProducto != 0)
                {

                    dataGridProducts.Rows.Add(new object[]
                    {
                "",
                IdProducto,
                txtCodigo.Text,
                txtNombre.Text,
                txtDescripcion.Text,
                ((OpcionCombo)cboCategoria.SelectedItem).Valor.ToString(),
                ((OpcionCombo)cboCategoria.SelectedItem).Texto.ToString(),
                "0",
                "0,00",
                "0,00",
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

                bool resultado = new CN_Producto().Edit(producto, out Mensaje);


                if (resultado)
                {
                    DataGridViewRow row = dataGridProducts.Rows[Convert.ToInt32(txtIndice.Text)];
                    row.Cells["IdProducto"].Value = txtId.Text;
                    row.Cells["Codigo"].Value = txtCodigo.Text;
                    row.Cells["Nombre"].Value = txtNombre.Text;
                    row.Cells["Descripcion"].Value = txtDescripcion.Text;   
                    row.Cells["IdCategoria"].Value = ((OpcionCombo)cboCategoria.SelectedItem).Valor.ToString();
                    row.Cells["Categoria"].Value = ((OpcionCombo)cboCategoria.SelectedItem).Texto.ToString();
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
            txtCodigo.Text = "";
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            cboEstado.SelectedIndex = 0;
            cboCategoria.SelectedIndex = 0;
            txtCodigo.Select();
        }

        private void dataGridProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridProducts.Columns[e.ColumnIndex].Name == "btnSeleccionar")
            {

                int index = e.RowIndex;


                if (index >= 0)
                {
                    txtIndice.Text = index.ToString();
                    txtId.Text = dataGridProducts.Rows[index].Cells["IdProducto"].Value.ToString();
                    txtCodigo.Text = dataGridProducts.Rows[index].Cells["Codigo"].Value.ToString();
                    txtNombre.Text = dataGridProducts.Rows[index].Cells["Nombre"].Value.ToString();
                    txtDescripcion.Text = dataGridProducts.Rows[index].Cells["Descripcion"].Value.ToString();


                    foreach (OpcionCombo oc in cboCategoria.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dataGridProducts.Rows[index].Cells["IdCategoria"].Value))
                        {

                            int index_combo = cboCategoria.Items.IndexOf(oc);
                            cboCategoria.SelectedIndex = index_combo;
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
                if (MessageBox.Show("¿Desea eliminar el producto?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string Mensaje = string.Empty;

                    Producto producto = new Producto()
                    {
                        IdProducto = Convert.ToInt32(txtId.Text)
                    };


                    bool respuesta = new CN_Producto().Delete(producto, out Mensaje);


                    if (respuesta)
                    {
                        dataGridProducts.Rows.RemoveAt(Convert.ToInt32(txtIndice.Text));

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

            if (dataGridProducts.Rows.Count > 0)
            {

                foreach (DataGridViewRow row in dataGridProducts.Rows)
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

            foreach (DataGridViewRow row in dataGridProducts.Rows)
            {
                row.Visible = true;
            }
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {

            if(dataGridProducts.Rows.Count < 1)
            {

                MessageBox.Show("No hay registros que descargar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            else
            {

                DataTable dt = new DataTable();

                foreach(DataGridViewColumn column in dataGridProducts.Columns)
                {
                    if(column.HeaderText != "" && column.Visible)
                    {
                        dt.Columns.Add(column.HeaderText, typeof(string));
                    }
                }
                foreach (DataGridViewRow row in dataGridProducts.Rows)
                    {
                        if (row.Visible)
                        {
                            dt.Rows.Add(
                                new object[]
                                {
                                    row.Cells[2].Value.ToString(),
                                    row.Cells[3].Value.ToString(),
                                    row.Cells[4].Value.ToString(),
                                    row.Cells[6].Value.ToString(),
                                    row.Cells[7].Value.ToString(),
                                    row.Cells[8].Value.ToString(),
                                    row.Cells[9].Value.ToString(),
                                    row.Cells[11].Value.ToString(),
                                    
                                }


                                ) ;
                        }
                            
                    }

            SaveFileDialog saveFile = new SaveFileDialog();

            saveFile.FileName = string.Format("Reporte_productos_{0}.xlsx", DateTime.Now.ToString("ddMMyyyyHHmmss"));
            saveFile.Filter = "Excel Files | *.xlsx";

            if(saveFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    XLWorkbook wb = new XLWorkbook();
                    var hoja = wb.Worksheets.Add(dt, "Informe");
                    hoja.ColumnsUsed().AdjustToContents();
                    wb.SaveAs(saveFile.FileName);

                    MessageBox.Show("Reporte generado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    }
                    catch
                        {

                        MessageBox.Show("Error al generar el reporte", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        }
            }


            }


        }
    }

}
