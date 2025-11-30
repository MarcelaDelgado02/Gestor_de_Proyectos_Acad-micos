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
                    var tarea = new Tarea
                    {
                        IdTarea = lector.GetInt32(lector.GetOrdinal("IdTarea")),
                        TituloTarea = lector.GetString(lector.GetOrdinal("TituloTarea")),
                        DescripcionTarea = lector.GetString(lector.GetOrdinal("DescripcionTarea")),
                        IDUsuario = lector.GetInt32(lector.GetOrdinal("IDUsuario")),
                        CedulaEstudiante = lector.GetString(lector.GetOrdinal("CedulaEstudiante")),
                        NombreEstudiante = lector.GetString(lector.GetOrdinal("NombreEstudiante")),
                        IDUsuarioCreador = lector.GetInt32(lector.GetOrdinal("IDUsuarioCreador")),
                        FechaCreacion = lector.GetDateTime(lector.GetOrdinal("FechaCreacion")),
                        FechaLimiteTarea = lector.GetDateTime(lector.GetOrdinal("FechaLimiteTarea")),
                        EstadoTarea = lector.GetString(lector.GetOrdinal("EstadoTarea")),
                        IDProyecto = idProyecto
                    };

                    tareas.Add(tarea);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener tareas: {ex.Message}", ex);
            }

            return tareas;
        }


        public void CrearTarea(Tarea tarea, string cedulaUsuario)
        {
            try
            {
                using var conexion = new ConexionBD().AbrirConexion();
                using var cmd = new SqlCommand("spCrearTarea", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CedulaUsuario", cedulaUsuario);
                cmd.Parameters.AddWithValue("@IdProyecto", tarea.IDProyecto);
                cmd.Parameters.AddWithValue("@IdAsignado", tarea.IDUsuario); // el asignado
                cmd.Parameters.AddWithValue("@Titulo", tarea.TituloTarea);
                cmd.Parameters.AddWithValue("@Descripcion", tarea.DescripcionTarea); 
                cmd.Parameters.AddWithValue("@FechaLimite", tarea.FechaLimiteTarea); 
                cmd.Parameters.AddWithValue("@Estado", tarea.EstadoTarea);

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

        public void ModificarTarea(string cedulaUsuario, Tarea tarea)
        {
            try
            {
                using var conexion = new ConexionBD().AbrirConexion();
                using var cmd = new SqlCommand("spModificarTarea", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CedulaUsuario", cedulaUsuario);
                cmd.Parameters.AddWithValue("@IdTarea", tarea.IdTarea);
                cmd.Parameters.AddWithValue("@Titulo", (object?)tarea.TituloTarea ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Descripcion", (object?)tarea.DescripcionTarea ?? DBNull.Value); 
                cmd.Parameters.AddWithValue("@FechaLimite", (object?)tarea.FechaLimiteTarea ?? DBNull.Value); 
                cmd.Parameters.AddWithValue("@Estado", (object?)tarea.EstadoTarea ?? DBNull.Value);

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
        public void EliminarTarea(string cedulaUsuario, int idTarea)
        {
            try
            {
                using var conexion = new ConexionBD().AbrirConexion();
                using var cmd = new SqlCommand("spEliminarTarea", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CedulaUsuario", cedulaUsuario);
                cmd.Parameters.AddWithValue("@IdTarea", idTarea);


                var rolSalida = new SqlParameter("@RespuestaRol", SqlDbType.Int)
                {
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

        public void ActualizarEstadoTareaEstudiante(int idTarea, string cedulaEstudiante, string nuevoEstado)
        {
            try
            {
                using var conexion = new ConexionBD().AbrirConexion();
                using var cmd = new SqlCommand("sp_ActualizarEstadoTareaEstudiante", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id_tarea", idTarea);
                cmd.Parameters.AddWithValue("@cedula_estudiante", cedulaEstudiante);
                cmd.Parameters.AddWithValue("@nuevo_estado", nuevoEstado);

                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error SQL al actualizar estado de tarea: {ex.Message}", ex);
            }
        }

    }
}

