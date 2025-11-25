using Gestor_de_Proyectos_Académicos.Entidades;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Gestor_de_Proyectos_Académicos.DAL
{


    public class ReporteriaDAL
    {

        private readonly string _connectionString;
        private object proyectoId;

        public ReporteriaDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<ReporteFiltros> ObtenerReporteProyectos(RepoteFiltros filtros)

        {
            var reporte = new List<ReporteFiltros>();

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_ReporteProyectos", connection))

                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (filtros.FechaInicio.HasValue)
                        command.Parameters.AddWithValue("@FechaInicio", filtros.FechaInicio);


                    if (filtros.FechaFin.HasValue)
                        command.Parameters.AddWithValue("@FechaFin", filtros.FechaFin);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            reporte.Add(new ReporteFiltros
                            {
                                EstudianteId = reader.GetInt32("EstudianteId"),
                                NombreEstudiate = reader.GetString("NombreEstudiante"),
                                TotalTareas = reader.GetInt32("TotalTareas"),
                                TareasCompletadasEstudiante = reader.GetInt32("TareasCompletadas"),
                                PorcentajeAvance = reader.GetDecimal("PorcentajeAvancePersonal")
                            });


                        }

                    }
                }
            }
            return reporte;
        }

        //Reporte de estudiantes en proyecto 
        public List<ReporteFiltros> ObtenerReporteEstudiantesProyecto(int proyectoId)
        {

            var reportes = new List<ReporteFiltros>();

            using (var connection = new SqlConnection(_connectionString))

            {
                using (var command = new SqlCommand("SP_ReporteEstudiantesProyecto", connection))

                {

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ProyectoId", proyectoId);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            reportes.Add(new ReporteFiltros
                            {

                                EstudianteId = reader.GetInt32("EstudianteId"),
                                NombreEstudiate = reader.GetString("NombreEstudiante"),
                                TareasCompletadasEstudiante = reader.GetInt32("TareasCompletadas"),
                                ProyectoId = proyectoId,
                                NombreProyecto = reader.GetString("NombreProyecto")

                            });
                        }

                    }

                }

                return reportes;
            }
        }
        // Reporte de estudiantes en proyecto
        private List<ReporteFiltros> ObtenerReportePersonal(int estudianteId)
        {

            var reportes = new List<ReporteFiltros>();


            using (var connection = new SqlConnection(_connectionString))

            {

                using (var command = new SqlCommand("SP_ReportePersonalEstudiante", connection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EstudianteId", estudianteId);


                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            reportes.Add(new ReporteFiltros
                            {
                                ProyectoId = reader.GetInt32("ProyectoId"),
                                NombreProyecto = reader.GetString("NombreProyecto"),
                                TotalTareas = reader.GetInt32("TotalTareasPersonales"),
                                TareasCompletadasEstudiante = reader.GetInt32("TareasCompletadasPersonales"),
                                PorcentajeAvance = reader.GetDecimal("PorcentajeAvancePersonal")

                            });

                        }

                    }
                }

                return reportes;
            }
        }
    }
}
    
            

          