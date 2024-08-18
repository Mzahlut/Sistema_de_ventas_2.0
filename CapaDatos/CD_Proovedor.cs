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
    public class CD_Proovedor
    {

        public List<Proovedor> Listar()
        {
            List<Proovedor> Lista = new List<Proovedor>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select IdProovedor, Documento, RazonSocial, Correo, Telefono, Estado from PROOVEDOR");
             
                    SqlCommand cmd = new SqlCommand(query.ToString(), oConexion);
                    cmd.CommandType = CommandType.Text;
                    oConexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            Lista.Add(new Proovedor()
                            {
                                IdProovedor = Convert.ToInt32(dr["IdProovedor"]),
                                Documento = dr["Documento"].ToString(),
                                RazonSocial = dr["RazonSocial"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                Telefono = dr["Telefono"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"]),
                             

                            });


                        }
                    }
                }
                catch (Exception ex)
                {
                    Lista = new List<Proovedor>();
                };
            };

            return Lista;
        }


        public int Register(Proovedor objProovedor, out string Mensaje)
        {
            int idProovedorGenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_REGISTRAR_PROVEEDOR", oConexion);
                    cmd.Parameters.AddWithValue("Documento", objProovedor.Documento);
                    cmd.Parameters.AddWithValue("RazonSocial", objProovedor.RazonSocial);
                    cmd.Parameters.AddWithValue("Correo", objProovedor.Correo);
                    cmd.Parameters.AddWithValue("Telefono", objProovedor.Telefono);
                    cmd.Parameters.AddWithValue("Estado", objProovedor.Estado);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;
                    oConexion.Open();
                    cmd.ExecuteNonQuery();

                    idProovedorGenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }



            }
            catch (Exception ex)
            {
                idProovedorGenerado = 0;
                Mensaje = ex.Message;

            }


            return idProovedorGenerado;
        }


        public bool Edit(Proovedor objProovedor, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_EDITAR_PROOVEDOR", oConexion);
                    cmd.Parameters.AddWithValue("idProovedor", objProovedor.Documento);
                    cmd.Parameters.AddWithValue("Documento", objProovedor.Documento);
                    cmd.Parameters.AddWithValue("RazonSocial", objProovedor.RazonSocial);
                    cmd.Parameters.AddWithValue("Correo", objProovedor.Correo);
                    cmd.Parameters.AddWithValue("Telefono", objProovedor.Telefono);
                    cmd.Parameters.AddWithValue("Estado", objProovedor.Estado);
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


        public bool Delete(Proovedor objProovedor, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_ELIMINAR_PROOVEDOR", oConexion);
                    cmd.Parameters.AddWithValue("idProovedor", objProovedor.IdProovedor);
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



    }
}
