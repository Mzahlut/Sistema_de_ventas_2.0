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
    public class CD_Producto
    {

            public List<Producto> Listar()
            {
                List<Producto> Lista = new List<Producto>();
                using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
                {
                    try
                    {
                        StringBuilder query = new StringBuilder();
                        query.AppendLine("select IdProducto, Codigo, Nombre, p.Descripcion, c.IdCategoria,c.Descripcion[Descrip], Stock, PrecioCompra, PrecioVenta, p.Estado from PRODUCTO p");
                        query.AppendLine("inner join CATEGORIA c on c.IdCategoria = p.IdCategoria");

                        SqlCommand cmd = new SqlCommand(query.ToString(), oConexion);
                        cmd.CommandType = CommandType.Text;
                        oConexion.Open();

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {

                            while (dr.Read())
                            {
                                Lista.Add(new Producto()
                                {
                                    IdProducto = Convert.ToInt32(dr["IdProducto"]),
                                    Codigo = dr["Codigo"].ToString(),
                                    Nombre = dr["Nombre"].ToString(),
                                    Descripcion = dr["Descripcion"].ToString(),
                                    oCategoria = new Categoria() { IdCategoria = Convert.ToInt32(dr["IdCategoria"]), Descripcion = dr["Descrip"].ToString() },
                                    Stock = Convert.ToInt32(dr["Stock"]),
                                    PrecioCompra = Convert.ToDecimal(dr["PrecioCompra"]),
                                    PrecioVenta = Convert.ToDecimal(dr["PrecioVenta"]),
                                    Estado = Convert.ToBoolean(dr["Estado"])


                                });;


                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Lista = new List<Producto>();
                    };
                };

                return Lista;
            }


            public int Register(Producto objProducto, out string Mensaje)
            {
                int idProductoGenerado = 0;
                Mensaje = string.Empty;

                try
                {
                    using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
                    {

                        SqlCommand cmd = new SqlCommand("SP_REGISTRAR_PRODUCTO", oConexion);
                        cmd.Parameters.AddWithValue("Codigo", objProducto.Codigo);
                        cmd.Parameters.AddWithValue("Nombre", objProducto.Nombre);
                        cmd.Parameters.AddWithValue("Descripcion", objProducto.Descripcion);
                        cmd.Parameters.AddWithValue("IdCategoria", objProducto.oCategoria.IdCategoria);
                        cmd.Parameters.AddWithValue("Estado", objProducto.Estado);
                        cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                        cmd.CommandType = CommandType.StoredProcedure;
                        oConexion.Open();
                        cmd.ExecuteNonQuery();

                        idProductoGenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                        Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                    }



                }
                catch (Exception ex)
                {
                    idProductoGenerado = 0;
                    Mensaje = ex.Message;

                }


                return idProductoGenerado;
            }


            public bool Edit(Producto objProducto, out string Mensaje)
            {
                bool respuesta = false;
                Mensaje = string.Empty;

                try
                {
                    using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
                    {

                        SqlCommand cmd = new SqlCommand("SP_EDITAR_PRODUCTO", oConexion);
                        cmd.Parameters.AddWithValue("IdProducto", objProducto.IdProducto);
                        cmd.Parameters.AddWithValue("Codigo", objProducto.Codigo);
                        cmd.Parameters.AddWithValue("Nombre", objProducto.Nombre);
                        cmd.Parameters.AddWithValue("Descripcion", objProducto.Descripcion);
                        cmd.Parameters.AddWithValue("IdCategoria", objProducto.oCategoria.IdCategoria);
                        cmd.Parameters.AddWithValue("Estado", objProducto.Estado);
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


            public bool Delete(Producto objProducto, out string Mensaje)
            {
                bool respuesta = false;
                Mensaje = string.Empty;

                try
                {
                    using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
                    {

                        SqlCommand cmd = new SqlCommand("SP_ELIMINAR_PRODUCTO", oConexion);
                        cmd.Parameters.AddWithValue("IdProducto", objProducto.IdProducto);
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

