using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using CapaEntidad;
using System.Collections;

namespace CapaDatos
{
    public class CD_Usuario
    {
        public List<Usuario> Listar()
        {
            List<Usuario> Lista = new List<Usuario>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("Select u.IdUsuario, u.Documento, u.NombreCompleto, u.Correo,u.Clave,u.Estado, r.IdRol, r.Descripcion from USUARIO u");
                    query.AppendLine("inner join ROL r on r.IdRol = u.IdRol;");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oConexion);
                    cmd.CommandType = CommandType.Text;
                    oConexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            Lista.Add(new Usuario()
                            {
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                Documento = dr["Documento"].ToString(),
                                NombreCompleto = dr["NombreCompleto"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                Clave = dr["Clave"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"]),
                                oRol = new Rol() { IdRol = Convert.ToInt32(dr["IdRol"]), Descripcion = dr["Descripcion"].ToString()}

                            });


                        }
                    }
                }
                catch (Exception ex)
                {
                    Lista = new List<Usuario>();
                };
            };

            return Lista;
        }


        public int Register(Usuario objUsuario, out string Mensaje)
        {
            int idUsuarioGenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_REGISTRAR_USUARIO", oConexion);
                    cmd.Parameters.AddWithValue("Documento", objUsuario.Documento);
                    cmd.Parameters.AddWithValue("NombreCompleto", objUsuario.NombreCompleto);
                    cmd.Parameters.AddWithValue("Correo", objUsuario.Correo);
                    cmd.Parameters.AddWithValue("Clave", objUsuario.Clave);
                    cmd.Parameters.AddWithValue("idRol", objUsuario.oRol.IdRol);
                    cmd.Parameters.AddWithValue("Estado", objUsuario.Estado);
                    cmd.Parameters.Add("IdUsuarioResultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;
                    oConexion.Open();
                    cmd.ExecuteNonQuery();

                    idUsuarioGenerado = Convert.ToInt32(cmd.Parameters["IdUsuarioResultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }



            }
            catch (Exception ex)
            {
                idUsuarioGenerado = 0;
                Mensaje = ex.Message;

            }


            return idUsuarioGenerado;
        }


        public bool Edit(Usuario objUsuario, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_EDITAR_USUARIO", oConexion);
                    cmd.Parameters.AddWithValue("idUsuario", objUsuario.Documento);
                    cmd.Parameters.AddWithValue("Documento", objUsuario.Documento);
                    cmd.Parameters.AddWithValue("NombreCompleto", objUsuario.NombreCompleto);
                    cmd.Parameters.AddWithValue("Correo", objUsuario.Correo);
                    cmd.Parameters.AddWithValue("Clave", objUsuario.Clave);
                    cmd.Parameters.AddWithValue("idRol", objUsuario.oRol.IdRol);
                    cmd.Parameters.AddWithValue("Estado", objUsuario.Estado);
                    cmd.Parameters.Add("Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;
                    oConexion.Open();
                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Respuesta"].Value);
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


        public bool Delete(Usuario objUsuario, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_ELIMINAR_USUARIO", oConexion);
                    cmd.Parameters.AddWithValue("idUsuario", objUsuario.IdUsuario);
                    cmd.Parameters.Add("Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;
                    oConexion.Open();
                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Respuesta"].Value);
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
