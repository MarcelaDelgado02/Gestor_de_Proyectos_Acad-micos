using System;
using Gestor_de_Proyectos_Académicos.Entidades;
using Gestor_de_Proyectos_Académicos.DAL;
using Gestor_de_Proyectos_Académicos.Pages.ReporteProyectos;


namespace Gestor_de_Proyectos_Académicos.BLL
{
    public class ReporteriaBLL
    {
        private readonly ReporteriaDAL reporteriaDAL = new ReporteriaDAL();

        public List<Reporte> ObtenerReporteProyectos(Reporte filtros) {

            try
            {
                return reporteriaDAL.ObtenerReporteProyectos(filtros);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el reporte general de proyectos.", ex);
            }

        }

        public List<Reporte> ObtenerReportePersonal(int estudianteId) {
            try
            {
                return reporteriaDAL.ObtenerReportePersonal(estudianteId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el reporte personal del estudiante.", ex);
            }

        }

        public List<Reporte> ObtenerReporteEstudiantesProyecto(int proyectoId) {

            try
            {
                return reporteriaDAL.ObtenerReporteEstudiantesProyecto(proyectoId);

            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el reporte de estudiantes por proyecto.", ex);
            }
        }
    }
}