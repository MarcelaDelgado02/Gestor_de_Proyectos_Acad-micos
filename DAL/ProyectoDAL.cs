
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
    }
}


        


