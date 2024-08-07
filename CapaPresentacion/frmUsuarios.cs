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
using System.Windows.Controls;
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

            //Muestra los datos en el panel
            List<Usuario> listausuarios = new CN_Usuario().listar();

            foreach (Usuario user in listausuarios)
            {
                dataGridUsers.Rows.Add(new object[]
          {
                "",
                user.IdUsuario,
                user.Documento,
                user.NombreCompleto,
                user.Correo,
                user.Clave,
                user.oRol.IdRol,
                user.oRol.Descripcion,
                user.Estado == true? 1 : 0,
                user.Estado == true? "Activo" : "No Activo"

          });
            }


        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            string Mensaje = string.Empty;

            Usuario usuario = new Usuario()
            {
                IdUsuario = Convert.ToInt32(txtId.Text),
                Documento = txtDocumento.Text,
                NombreCompleto = txtNombre.Text,
                Correo = txtCorreo.Text,
                Clave = txtClave.Text,
                oRol = new Rol() { IdRol = Convert.ToInt32(((OpcionCombo)cboRol.SelectedItem).Valor) },
                Estado = Convert.ToInt32(((OpcionCombo)cboEstado.SelectedItem).Valor) == 1? true : false
            };


            if(usuario.IdUsuario == 0)
            {

            int idUsuarioGenerado = new CN_Usuario().Register(usuario, out Mensaje);


            if(idUsuarioGenerado != 0)
            {

            dataGridUsers.Rows.Add(new object[]
            {
                "",
                idUsuarioGenerado,
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
            else
            {
                MessageBox.Show(Mensaje);
            }

            }
            else
            {

                bool resultado = new CN_Usuario().Edit(usuario, out Mensaje);


                if(resultado)
                {
                    DataGridViewRow row = dataGridUsers.Rows[Convert.ToInt32(txtIndice.Text)];
                    row.Cells["IdUsuario"].Value = txtId.Text;
                    row.Cells["Documento"].Value = txtDocumento.Text;
                    row.Cells["NombreCompleto"].Value = txtNombre.Text;
                    row.Cells["Correo"].Value = txtCorreo.Text;
                    row.Cells["Clave"].Value = txtClave.Text;
                    row.Cells["IdRol"].Value = ((OpcionCombo)cboRol.SelectedItem).Valor.ToString();
                    row.Cells["Rol"].Value = ((OpcionCombo)cboRol.SelectedItem).Texto.ToString();
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
            txtClave.Text = "";
            txtConfirmClave.Text = "";
            cboEstado.SelectedIndex = 0;
            cboRol.SelectedIndex = 0;
            txtDocumento.Select();


        }

        private void dataGridUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dataGridUsers.Columns[e.ColumnIndex].Name == "btnSeleccionar")
            {

                int index = e.RowIndex;


                if(index >= 0)
                {
                    txtIndice.Text = index.ToString();
                    txtId.Text = dataGridUsers.Rows[index].Cells["IdUsuario"].Value.ToString();
                    txtDocumento.Text = dataGridUsers.Rows[index].Cells["Documento"].Value.ToString();
                    txtNombre.Text = dataGridUsers.Rows[index].Cells["NombreCompleto"].Value.ToString();
                    txtCorreo.Text = dataGridUsers.Rows[index].Cells["Correo"].Value.ToString();
                    txtClave.Text = dataGridUsers.Rows[index].Cells["Clave"].Value.ToString();
                    txtConfirmClave.Text = dataGridUsers.Rows[index].Cells["Clave"].Value.ToString();
                    
                    foreach(OpcionCombo oc in cboRol.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dataGridUsers.Rows[index].Cells["Idrol"].Value)){

                            int index_combo = cboRol.Items.IndexOf(oc);
                            cboRol.SelectedIndex = index_combo;
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
                if (MessageBox.Show("¿Desea eliminar el usuario?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string Mensaje = string.Empty;

                    Usuario usuario = new Usuario()
                    {
                        IdUsuario = Convert.ToInt32(txtId.Text)
                    };


                    bool respuesta = new CN_Usuario().Delete(usuario, out Mensaje);


                    if (respuesta)
                    {
                        dataGridUsers.Rows.RemoveAt(Convert.ToInt32(txtIndice.Text));
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

            if(dataGridUsers.Rows.Count > 0)
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
    }
    }

