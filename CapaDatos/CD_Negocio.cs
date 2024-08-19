using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Negocio
    {
        public Negocio ObtenerDatos()
        {

            Negocio negocio = new Negocio();

            try
            {

                using(SqlConnection conn = new SqlConnection(Conexion.cadena))
                {
                    conn.Open();

                    string query = "select IdNegocio, Nombre, RUC, Direccion from NEGOCIO where IdNegocio = 1"; 

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = CommandType.Text;

                    using(SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while(dr.Read())
                        {
                            negocio = new Negocio()
                            {
                                IdNegocio = int.Parse(dr["IdNegocio"].ToString()),
                                NombreNegocio = dr["Nombre"].ToString(),
                                Ruc = dr["RUC"].ToString(),
                                Direccion = dr["Direccion"].ToString()
                            };

                        }


                    }
               
                }



            }catch
            {
                negocio = new Negocio();

            }




            return negocio;
        }



        public bool GuardarDatos(Negocio neg, out string mensaje)
        {

            mensaje = string.Empty;
            bool respuesta = true;

            try
            {

                using (SqlConnection conn = new SqlConnection(Conexion.cadena))
                {
                    conn.Open();

                    

                    StringBuilder query = new StringBuilder();
                    query.AppendLine("update NEGOCIO set Nombre = @NombreNegocio,");
                    query.AppendLine("RUC = @Ruc,");
                    query.AppendLine("Direccion = @Direccion,");
                    query.AppendLine("where IdNegocio = 1;");

                    SqlCommand cmd = new SqlCommand(query.ToString(), conn);
                    cmd.Parameters.AddWithValue("@NombreNegocio", neg.NombreNegocio.ToString());
                    cmd.Parameters.AddWithValue("@Ruc", neg.Ruc.ToString());
                    cmd.Parameters.AddWithValue("@Direccion", neg.Direccion.ToString());
                    cmd.CommandType = CommandType.Text;

                    if(cmd.ExecuteNonQuery() < 1)
                    {
                        mensaje = "No se pudieron guardar los datos";
                        respuesta = false;
                    }
                   
                  

                }



            }
            catch(Exception e) { 
                
                mensaje = e.Message;
                respuesta = false;

            }




            return respuesta;

        }


        public byte[] ObtenerLogo(out bool obtenido)
        {

            obtenido = true;
            byte[] logoBytes = new byte[0];

            try
            {

                using (SqlConnection conn = new SqlConnection(Conexion.cadena))
                {
                    conn.Open();

                    string query = "Select Logo from NEGOCIO where IdNegocio = 1;";


                    SqlCommand cmd = new SqlCommand(query, conn);
                   
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            logoBytes = (byte[])dr["Logo"];

                        }


                    }

                }

            }
            catch (Exception e)
            {

                obtenido = false;
                logoBytes = new byte[0];

            }




            return logoBytes;

        }


        public bool ActualizarLogo(byte[] image,out string mensaje)
        {
            mensaje = string.Empty;
            bool respuesta = true;

            try
            {

                using (SqlConnection conn = new SqlConnection(Conexion.cadena))
                {
                    conn.Open();



                    StringBuilder query = new StringBuilder();
                    query.AppendLine("update NEGOCIO set Logo = @image");
                    query.AppendLine("where IdNegocio = 1;");

                    SqlCommand cmd = new SqlCommand(query.ToString(), conn);
                    cmd.Parameters.AddWithValue("@image", image);

                    cmd.CommandType = CommandType.Text;

                    if (cmd.ExecuteNonQuery() < 1)
                    {
                        mensaje = "No se pudo actualizar el logo";
                        respuesta = false;
                    }



                }



            }
            catch (Exception e)
            {

                mensaje = e.Message;
                respuesta = false;

            }




            return respuesta;
        }




    }
}
