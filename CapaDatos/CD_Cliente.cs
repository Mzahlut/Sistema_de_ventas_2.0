using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Cliente
    {

        public List<Cliente> Listar()
        {
            List<Cliente> Lista = new List<Cliente>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select IdCliente, Documento, NombreCompleto, Correo, Telefono, Estado from CLIENTE");
 
                    SqlCommand cmd = new SqlCommand(query.ToString(), oConexion);
                    cmd.CommandType = CommandType.Text;
                    oConexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            Lista.Add(new Cliente()
                            {
                                IdCliente = Convert.ToInt32(dr["IdCliente"]),
                                Documento = dr["Documento"].ToString(),
                                NombreCompleto = dr["NombreCompleto"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                Telefono = dr["Telefono"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"]),
                   

                            });


                        }
                    }
                }
                catch (Exception ex)
                {
                    Lista = new List<Cliente>();
                };
            };

            return Lista;
        }


        public int Register(Cliente objCliente, out string Mensaje)
        {
            int idClienteGenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_REGISTRAR_CLIENTE", oConexion);
                    cmd.Parameters.AddWithValue("Documento", objCliente.Documento);
                    cmd.Parameters.AddWithValue("NombreCompleto", objCliente.NombreCompleto);
                    cmd.Parameters.AddWithValue("Correo", objCliente.Correo);
                    cmd.Parameters.AddWithValue("Telefono", objCliente.Telefono);
                    cmd.Parameters.AddWithValue("Estado", objCliente.Estado);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;
                    oConexion.Open();
                    cmd.ExecuteNonQuery();

                    idClienteGenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }



            }
            catch (Exception ex)
            {
                idClienteGenerado = 0;
                Mensaje = ex.Message;

            }


            return idClienteGenerado;
        }


        public bool Edit(Cliente objCliente, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_MODIFICAR_CLIENTE", oConexion);
                    cmd.Parameters.AddWithValue("idCliente", objCliente.Documento);
                    cmd.Parameters.AddWithValue("Documento", objCliente.Documento);
                    cmd.Parameters.AddWithValue("NombreCompleto", objCliente.NombreCompleto);
                    cmd.Parameters.AddWithValue("Correo", objCliente.Correo);
                    cmd.Parameters.AddWithValue("Telefono", objCliente.Telefono);
                    cmd.Parameters.AddWithValue("Estado", objCliente.Estado);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;
                    oConexion.Open();
                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }



            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = ex.Message;

            }


            return respuesta;
        }


        public bool Delete(Cliente objCliente, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand cmd = new SqlCommand("Delete from CLIENTE where IdCliente = @Id", oConexion);
                    cmd.Parameters.AddWithValue("@Id", objCliente.IdCliente);
                    cmd.CommandType = CommandType.Text;
                    oConexion.Open();

                    respuesta = cmd.ExecuteNonQuery() > 0? true : false;

                    

                }



            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = ex.Message;

            }


            return respuesta;
        }





    }
}
