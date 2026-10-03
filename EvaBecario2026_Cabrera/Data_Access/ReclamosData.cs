using EvaBecario2026_Cabrera.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EvaBecario2026_Cabrera.Data_Access
{
    public class ReclamosData
    {
        // Cadena de coneccion 
        private readonly string _cadena = ConfigurationManager.ConnectionStrings["CadenaConexion"].ConnectionString;

        // Metodos
        public List<ReclamosModel> Listar()
        {
            var lista = new List<ReclamosModel>();

            using (SqlConnection con = new SqlConnection(_cadena))
            {
                string query = @"   SELECT idReclamo, nombreProveedor, direccionProveedor, nombresConsumidor, apellidosConsumidor, DUI, detalleReclamo, montoReclamado, telefono, fechaIngreso 
                                    FROM t_reclamos 
                                    ORDER BY idReclamo";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new ReclamosModel
                            {
                                IdReclamo = Convert.ToInt32(dr["idReclamo"]),
                                NombreProveedor = dr["nombreProveedor"].ToString(),
                                DireccionProveedor = dr["direccionProveedor"] != DBNull.Value ? dr["direccionProveedor"].ToString() : string.Empty,
                                NombresConsumidor = dr["nombresConsumidor"].ToString(),
                                ApellidosConsumidor = dr["apellidosConsumidor"].ToString(),
                                DUI = dr["DUI"].ToString(),
                                DetalleReclamo = dr["detalleReclamo"].ToString(),
                                MontoReclamado = Convert.ToDecimal(dr["montoReclamado"]),
                                Telefono = dr["telefono"] != DBNull.Value ? dr["telefono"].ToString() : string.Empty,
                                FechaIngreso = Convert.ToDateTime(dr["fechaIngreso"])
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public ReclamosModel ObtenerPorId(int id)
        {
            ReclamosModel reclamo = null;

            using (SqlConnection con = new SqlConnection(_cadena))
            {
                string query = "SELECT * FROM t_reclamos WHERE idReclamo = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            reclamo = new ReclamosModel
                            {
                                IdReclamo = Convert.ToInt32(dr["idReclamo"]),
                                NombreProveedor = dr["nombreProveedor"].ToString(),
                                DireccionProveedor = dr["direccionProveedor"] != DBNull.Value ? dr["direccionProveedor"].ToString() : string.Empty,
                                NombresConsumidor = dr["nombresConsumidor"].ToString(),
                                ApellidosConsumidor = dr["apellidosConsumidor"].ToString(),
                                DUI = dr["DUI"].ToString(),
                                DetalleReclamo = dr["detalleReclamo"].ToString(),
                                MontoReclamado = Convert.ToDecimal(dr["montoReclamado"]),
                                Telefono = dr["telefono"] != DBNull.Value ? dr["telefono"].ToString() : string.Empty,
                                FechaIngreso = Convert.ToDateTime(dr["fechaIngreso"])
                            };
                        }
                    }
                }
            }
            return reclamo;
        }

        public bool ExisteDUI(string dui, int idExcluir = 0)
        {
            using (SqlConnection con = new SqlConnection(_cadena))
            {
                string query = "SELECT COUNT(1) FROM t_reclamos WHERE DUI = @dui AND idReclamo != @idExcluir";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@dui", dui);
                    cmd.Parameters.AddWithValue("@idExcluir", idExcluir);
                    con.Open();
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        public bool Agregar(ReclamosModel r)
        {
            using (SqlConnection con = new SqlConnection(_cadena))
            {
                string query = @"INSERT INTO t_reclamos 
                                 (nombreProveedor, direccionProveedor, nombresConsumidor, apellidosConsumidor, DUI, detalleReclamo, montoReclamado, telefono, fechaIngreso) 
                                 VALUES 
                                 (@nombreProv, @dirProv, @nombresCons, @apellidosCons, @dui, @detalle, @monto, @tel, GETDATE())";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@nombreProv", r.NombreProveedor);
                    cmd.Parameters.AddWithValue("@dirProv", string.IsNullOrEmpty(r.DireccionProveedor) ? (object)DBNull.Value : r.DireccionProveedor);
                    cmd.Parameters.AddWithValue("@nombresCons", r.NombresConsumidor);
                    cmd.Parameters.AddWithValue("@apellidosCons", r.ApellidosConsumidor);
                    cmd.Parameters.AddWithValue("@dui", r.DUI);
                    cmd.Parameters.AddWithValue("@detalle", r.DetalleReclamo);
                    cmd.Parameters.AddWithValue("@monto", r.MontoReclamado);
                    cmd.Parameters.AddWithValue("@tel", string.IsNullOrEmpty(r.Telefono) ? (object)DBNull.Value : r.Telefono);

                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Editar(ReclamosModel r)
        {
            using (SqlConnection con = new SqlConnection(_cadena))
            {
                string query = @"UPDATE t_reclamos SET 
                                 nombreProveedor = @nombreProv, 
                                 direccionProveedor = @dirProv, 
                                 nombresConsumidor = @nombresCons, 
                                 apellidosConsumidor = @apellidosCons, 
                                 DUI = @dui, 
                                 detalleReclamo = @detalle, 
                                 montoReclamado = @monto, 
                                 telefono = @tel 
                                 WHERE idReclamo = @id";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", r.IdReclamo);
                    cmd.Parameters.AddWithValue("@nombreProv", r.NombreProveedor);
                    cmd.Parameters.AddWithValue("@dirProv", string.IsNullOrEmpty(r.DireccionProveedor) ? (object)DBNull.Value : r.DireccionProveedor);
                    cmd.Parameters.AddWithValue("@nombresCons", r.NombresConsumidor);
                    cmd.Parameters.AddWithValue("@apellidosCons", r.ApellidosConsumidor);
                    cmd.Parameters.AddWithValue("@dui", r.DUI);
                    cmd.Parameters.AddWithValue("@detalle", r.DetalleReclamo);
                    cmd.Parameters.AddWithValue("@monto", r.MontoReclamado);
                    cmd.Parameters.AddWithValue("@tel", string.IsNullOrEmpty(r.Telefono) ? (object)DBNull.Value : r.Telefono);

                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Eliminar(int id)
        {
            using (SqlConnection con = new SqlConnection(_cadena))
            {
                string query = "DELETE FROM t_reclamos WHERE idReclamo = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }

}
