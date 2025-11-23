using Gestor_de_Proyectos_Académicos.Entidades;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
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

        public List<Reporte> ObtenerReporteProyectos(RepoteFiltros filtros)

        {
            var reporte = new List<Reporte>();

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_ReporteProyectos",connection))

                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ProyectosId", proyectoId);


                    connection.Open();
                    using(var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            reporte.Add(new Reporte
                            {
                                EstudianteId = reader.GetInt32("EstudianteId"),
                                NombreEstudiate = reader.GetString("NombreEstudiante"),
                                TareasCompletadasEstudiante = reader.GetInt32("TareasCompletadas"),
                                PorcentajeAvance = reader.GetDecimal("PorcentajeAvancePersonal")
                            });


                        }

                    }
                }
            }
            return reporte;
        }
    }
}
