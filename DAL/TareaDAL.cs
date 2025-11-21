using Gestor_de_Proyectos_Académicos.Entidades;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Gestor_de_Proyectos_Académicos.DAL
{
    public class TareaDAL
    {
        public List<Tarea> ObtenerTareasPorProyecto(int idProyecto)
        {
            var tareas = new List<Tarea>();

            try
            {
                using var conexion = new ConexionBD().AbrirConexion();
                using var cmd = new SqlCommand("sp_ObtenerTareasPorProyecto", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_proyecto", idProyecto);

                using var lector = cmd.ExecuteReader();
                while (lector.Read())
                {
                
                    if (lector.FieldCount == 1 && lector.GetName(0) == "Mensaje")
                        break;

                    var tarea = new Tarea
                    {
                        idTarea = lector.GetInt32(lector.GetOrdinal("IdTarea")),
                        tituloTarea = lector.GetString(lector.GetOrdinal("TituloTarea")),
                        despripcionTarea = lector.IsDBNull(lector.GetOrdinal("DescripcionTarea")) ? "" : lector.GetString(lector.GetOrdinal("DescripcionTarea")),
                        IDUsuario = lector.GetInt32(lector.GetOrdinal("IDUsuario")),
                        fechaLimite = lector.IsDBNull(lector.GetOrdinal("FechaLimiteTarea")) ? null : lector.GetDateTime(lector.GetOrdinal("FechaLimiteTarea")),
                        estadoTarea = lector.IsDBNull(lector.GetOrdinal("EstadoTarea")) ? "Sin estado" : lector.GetString(lector.GetOrdinal("EstadoTarea")),
                        IDProyecto = idProyecto
                    };

                    tareas.Add(tarea);
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error SQL al obtener tareas: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error general al obtener tareas: {ex.Message}", ex);
            }

            return tareas;
        }

        public void CrearTarea(Tarea tarea, string cedulaUsuario) // Cambiar a string
        {
            try
            {
                using var conexion = new ConexionBD().AbrirConexion();
                using var cmd = new SqlCommand("spCrearTarea", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                // Pasar la cédula como string
                cmd.Parameters.AddWithValue("@CedulaUsuario", cedulaUsuario);
                cmd.Parameters.AddWithValue("@IdProyecto", tarea.IDProyecto);
                cmd.Parameters.AddWithValue("@IdAsignado", tarea.IdAsignado);
                cmd.Parameters.AddWithValue("@Titulo", tarea.tituloTarea);
                cmd.Parameters.AddWithValue("@Descripcion", tarea.despripcionTarea);
                cmd.Parameters.AddWithValue("@FechaLimite", tarea.fechaLimite);
                cmd.Parameters.AddWithValue("@Estado", tarea.estadoTarea);

                var rolSalida = new SqlParameter("@RespuestaRol", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                cmd.Parameters.Add(rolSalida);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error SQL al crear la tarea: {ex.Message}", ex);
            }
        }
        public void EliminarTarea(string cedulaUsuario, int idTarea) {
            try
            {
                using var conexion = new ConexionBD().AbrirConexion();
                using var cmd = new SqlCommand("spEliminarTarea", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CedulaUsuario", cedulaUsuario);
                cmd.Parameters.AddWithValue("@IdTarea", idTarea);


                var rolSalida = new SqlParameter ("@RespuestaRol", SqlDbType.Int){
                    Direction = ParameterDirection.Output

                };
                cmd.Parameters.Add(rolSalida);

                cmd.ExecuteNonQuery();
                

            }
            catch (SqlException ex)
            {
                throw new Exception($"Error SQL al eliminar la tarea: {ex.Message}", ex);
            }



        }

        public void ModificarTarea(string cedulaUsuario, Tarea tarea)
        {
            try
            {
                using var conexion = new ConexionBD().AbrirConexion();
                using var cmd = new SqlCommand("spModificarTarea", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CedulaUsuario", cedulaUsuario);
                cmd.Parameters.AddWithValue("@IdTarea", tarea.idTarea);
                cmd.Parameters.AddWithValue("@Titulo", (object?)tarea.tituloTarea ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Descripcion", (object?)tarea.despripcionTarea ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@FechaLimite", (object?)tarea.fechaLimite ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Estado", (object?)tarea.estadoTarea ?? DBNull.Value);

                var rolOut = new SqlParameter("@RespuestaRol", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(rolOut);

                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error SQL al modificar la tarea: {ex.Message}", ex);
            }
        }

    }
}

