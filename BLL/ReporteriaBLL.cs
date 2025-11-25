using System;
using Gestor_de_Proyectos_Académicos.Entidades;
using Gestor_de_Proyectos_Académicos.DAL;
using Gestor_de_Proyectos_Académicos.Pages.Reporte_Proyecto;


namespace Gestor_de_Proyectos_Académicos.BLL
{
    public class ReporteriaBLL
    {

        private readonly ReporteriaDAL reporteriaDAL;
        private ReporteriaDAL _reporteriaDal;

        public ReporteriaBLL(string connectionString)
        {
            _reporteriaDal = new ReporteriaDAL(connectionString);
        }

        public List<ReporteFiltros> GenerarReporteProyectos(RepoteFiltros filtros)
        {
            try
            {
                return _reporteriaDal.ObtenerReporteProyectos(filtros);
            }

            catch (Exception ex)
            {
                throw new Exception($"Error al generar reporte de proyectos: { ex.Message}");
            }
        }
            
        public List<ReporteFiltros> GenerarReporteEstudiantesProyecto(int proyectoId)
        {

            if (proyectoId < 0)
                throw new ArgumentOutOfRangeException("El ID de; proyecto es inválido");

            try
            {

                return _reporteriaDal.ObtenerReporteEstudiantesProyecto(proyectoId);
            }

            catch (Exception ex)
            {
                throw new Exception($"Error al generar reporte de estudiantes: {ex.Message}");  

            }
            
        }
       
        public List<ReporteFiltros> GenerarReportePersonal(int estudianteId)
        {

            if (estudianteId < 0)
                throw new ArgumentOutOfRangeException("El ID del estudiante en inválido");

            try
            {
                return _reporteriaDal.ObtenerReportePersonal(estudianteId);
            }

            catch (Exception ex)
            {
                throw new Exception($"Error al generar reporte personal :{ ex.Message}");   

            }
        }

        internal List<Reportes> GenerarReporteEstudiantesProyecto(ReporteFiltros filtros)
        {
            throw new NotImplementedException();
        }
    }
}