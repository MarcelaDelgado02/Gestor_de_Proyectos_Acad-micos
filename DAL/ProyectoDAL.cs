
using Gestor_de_Proyectos_Académicos.Entidades;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection.PortableExecutable;


namespace Gestor_de_Proyectos_Académicos.DAL

{
    public class ProyectoDAL
    {
        public List<Proyecto> ObtenerProyectos(string cedulaUsuario) 
        {
            var proyectos = new List<Proyecto>();

            try
            {
                using var conexion = new ConexionBD().AbrirConexion();
                using var cmd = new SqlCommand("spMostarProyecto",conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CedulaUsuario", cedulaUsuario);


                using var lector = cmd.ExecuteReader();
                while (lector.Read()) {

                    var proyecto = new Proyecto() {

                        IdProyecto = lector.GetInt32("IdProyecto"),
                        NombreProyecto = lector.GetString("NombreProyecto"),
                        DescripcionProyecto = lector.IsDBNull("DescripcionProyecto") ? "" : lector.GetString("DescripcionProyecto"),
                        FechaInicioProyecto = lector.GetDateTime("FechaInicioProyecto"),
                        FechaFinalProyecto = lector.GetDateTime("FechaFinalProyecto"),
                        EstadoProyecto = lector.GetString("EstadoProyecto")

                    };

                    proyectos.Add(proyecto);    


                
                
                }
            }
            catch (SqlException ex) when (ex.Message.Contains("No hay proyectos en los que participa"))
            {
                // No hay proyectos, retornamos lista vacía
                return proyectos;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener proyectos: {ex.Message}", ex);
            }

            return proyectos;
        }

        public bool CrearProyecto(string cedulaUsuario, Proyecto nuevoProyecto)
        {
            try
            {
                using var conexion = new ConexionBD().AbrirConexion();
                using var cmd = new SqlCommand("spCrearProyecto", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CedulaUsuario", cedulaUsuario);
                cmd.Parameters.AddWithValue("@NombreProyecto", nuevoProyecto.NombreProyecto);
                cmd.Parameters.AddWithValue("@DescripcionProyecto", nuevoProyecto.DescripcionProyecto ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@FechaInicioProyecto", nuevoProyecto.FechaInicioProyecto);
                cmd.Parameters.AddWithValue("@FechaFinalProyecto", nuevoProyecto.FechaFinalProyecto);
                cmd.Parameters.AddWithValue("@EstadoProyecto", nuevoProyecto.EstadoProyecto ?? "Activo");


                var respuestaRol = new SqlParameter("@RespuestaRolP", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                cmd.Parameters.Add(respuestaRol);
                cmd.ExecuteNonQuery();
                return true;

            }
            catch (SqlException ex)
            {


                throw new Exception($"Error SQL al crear el proyecto: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error general al crear el proyecto: {ex.Message}", ex);
            }


        }

        public bool EditarProyecto(string cedulaUsuario, Proyecto proyecto) {
            try
            {
                using var conexion = new ConexionBD().AbrirConexion();
                using var cmd = new SqlCommand("spModificarProyecto", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CedulaUsuario", cedulaUsuario);
                cmd.Parameters.AddWithValue("@IdProyecto", proyecto.IdProyecto);
                cmd.Parameters.AddWithValue("@NombreProyecto", proyecto.NombreProyecto);
                cmd.Parameters.AddWithValue("@DescripcionProyecto", proyecto.DescripcionProyecto);
                cmd.Parameters.AddWithValue("@FechaInicioProyecto", proyecto.FechaInicioProyecto);
                cmd.Parameters.AddWithValue("@FechaFinalProyecto", proyecto.FechaFinalProyecto);
                cmd.Parameters.AddWithValue("@EstadoProyecto", proyecto.EstadoProyecto);

                var respuestaRol = new SqlParameter("@RespuestaRolP", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(respuestaRol);
                cmd.ExecuteNonQuery();
                return true;


            }
            catch (SqlException ex) {

                throw new Exception($"Error SQL al modificar el proyecto: {ex.Message}", ex);

            }
            catch (Exception ex)
            {

                throw new Exception($"Error general al modificar el proyecto: {ex.Message}", ex);
            }
        
        }

        public bool EliminarProyecto(string cedulaUsuario, int idProyecto) {

            try
            {
                using var conexion = new ConexionBD().AbrirConexion();
                using var cmd = new SqlCommand("spEliminarProyecto", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CedulaUsuario", cedulaUsuario);
                cmd.Parameters.AddWithValue("@IdProyecto", idProyecto);

                var respuestaRol = new SqlParameter("@RespuestaRolP", SqlDbType.Int)
                {

                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(respuestaRol);
                cmd.ExecuteNonQuery();

                return true;

            }
            catch (SqlException ex)
            {
                throw new Exception($"Error SQL al eliminar el proyecto: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error general al eliminar el proyecto: {ex.Message}", ex);
            }
        }



    }
}


        


