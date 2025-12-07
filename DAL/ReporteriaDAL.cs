using Gestor_de_Proyectos_Académicos.Entidades;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection.PortableExecutable;


namespace Gestor_de_Proyectos_Académicos.DAL
{


    public class ReporteriaDAL
    {

        private ConexionBD conexion = new ConexionBD();

        public List<Reporte> ObtenerReporteProyectos(Reporte filtros) {
            var listaReporteProyectos = new List<Reporte>();

            using (var conn = conexion.AbrirConexion())
            using (var cmd = new SqlCommand("SP_ReporteProyectos", conn)) { 
            
                cmd.CommandType = CommandType.StoredProcedure;

                if (filtros.FechaInicio.HasValue)
                    cmd.Parameters.AddWithValue("@FechaInicio", filtros.FechaInicio);

                if (filtros.FechaFin.HasValue)
                    cmd.Parameters.AddWithValue("@FechaFin", filtros.FechaFin);
                using (var lector = cmd.ExecuteReader())
                {

                    while (lector.Read())
                    {
                        listaReporteProyectos.Add(new Reporte
                        {
                            ProyectoId = lector.GetInt32(lector.GetOrdinal("IdProyecto")),
                            NombreProyecto = lector.GetString(lector.GetOrdinal("NombreProyecto")),
                            TotalTareas = lector.GetInt32(lector.GetOrdinal("TotalTareas")),
                            TareasCompletas = lector.GetInt32(lector.GetOrdinal("TareasCompletadas")),
                            PorcentajeAvance = lector.GetDecimal(lector.GetOrdinal("PorcentajeAvance")),
                            
                            
                        });
                    }
                }
                
            }
            return listaReporteProyectos;


        }
        public List<Reporte> ObtenerReportePersonal(int estudianteId) { 
        
            var litaReporteEstudiante = new List<Reporte>();

            using (var conn = conexion.AbrirConexion())
            using (var cmd = new SqlCommand("SP_ReportePersonalEstudiante", conn)) {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EstudianteId", estudianteId);

                using (var lector = cmd.ExecuteReader()) {

                    while (lector.Read()) {

                        litaReporteEstudiante.Add(new Reporte
                        {
                            ProyectoId = lector.GetInt32(lector.GetOrdinal("IdProyecto")),
                            NombreProyecto = lector.GetString(lector.GetOrdinal("NombreProyecto")),
                            TotalTareas = lector.GetInt32(lector.GetOrdinal("TotalTareasPersonales")),
                            TareasCompletas = lector.GetInt32(lector.GetOrdinal("TareasCompletadasPersonales")),
                            PorcentajeAvance = lector.GetDecimal(lector.GetOrdinal("PorcentajeAvancePersonal"))


                        });
                    }
                }
            }

            return litaReporteEstudiante;
        }

        public List<Reporte> ObtenerReporteEstudiantesProyecto(int proyectoId) { 
            var listaEstudiantesProyecto = new List<Reporte>();

            using (var conn = conexion.AbrirConexion())
            using (var cmd = new SqlCommand("SP_ReporteEstudiantesProyecto", conn)) { 
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProyectoId", proyectoId);

                using (var lector = cmd.ExecuteReader()) {

                    while (lector.Read()) {


                        listaEstudiantesProyecto.Add(new Reporte
                        {
                            EstudianteId = lector.GetInt32(lector.GetOrdinal("EstudianteId")),
                            NombreEstudiante = lector.GetString(lector.GetOrdinal("NombreEstudiante")),
                            ProyectoId = proyectoId,
                            NombreProyecto = lector.GetString(lector.GetOrdinal("NombreProyecto")),
                            TareasCompletadasEstudiante = lector.GetInt32(lector.GetOrdinal("TareasCompletadas"))



                        });
                    }
                }

            }

            return listaEstudiantesProyecto;


        }

    }
}
    
            

          