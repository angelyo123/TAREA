using System.Data;
using tarea_ANGEL_SEBASTIAN_MACA_SANCHEZ.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace tarea_ANGEL_SEBASTIAN_MACA_SANCHEZ.repositorio
{
    public class UsuarioRepository
    {
        private readonly string _connectionString;

        public UsuarioRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Usuario> ObtenerTodos()
        {
            var lista = new List<Usuario>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("sp_listar", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Usuario
                            {
                                Id = (int)reader["Id"],
                                Numero = (int)reader["Numero"],
                                FechaHora = (DateTime)reader["FechaHora"],
                                NombreApellido = reader["NombreApellido"].ToString()
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public void Insertar(Usuario usuario)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("sp_Insertar", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Numero", usuario.Numero);
                    cmd.Parameters.AddWithValue("@FechaHora", usuario.FechaHora);
                    cmd.Parameters.AddWithValue("@NombreApellido", usuario.NombreApellido);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Actualizar(Usuario usuario)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("sp_editar", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", usuario.Id);
                    cmd.Parameters.AddWithValue("@Numero", usuario.Numero);
                    cmd.Parameters.AddWithValue("@FechaHora", usuario.FechaHora);
                    cmd.Parameters.AddWithValue("@NombreApellido", usuario.NombreApellido);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Eliminar(int id)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("sp_Eliminar", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
