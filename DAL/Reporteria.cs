using Gestor_de_Proyectos_Académicos.Entidades;
using Microsoft.Identity.Client;

namespace Gestor_de_Proyectos_Académicos.DAL
{
    public class Reporteria
    {

        private readonly ConexionBD_conexion;

            public List<Reporte> ObtenerReporteProyecto(int? profesorid = null)
        { 
         
            var query = @"SELECT p.Nombre as NombreProyecto,
                             COUNT(t.Id) as TotalTareas,
                             SUM(CASE WHEN t.Estado = 'Completado' THEN 1 ELSE 0 END) as TareasCompletadas
                      FROM Proyectos p
                      LEFT JOIN Tareas t ON p.Id = t.ProyectoId
                      WHERE (@profesorId IS NULL OR p.ProfesorId = @profesorId)
                      GROUP BY p.Id, p.Nombre";

        }

        public List<ReporteEstudianteProyecto>  ObtenerReporteEstudianteProyectos(int proyectoId)

        {
          public List<ReportePersonal> ObtenerReortePersonal(int estudianteId)

    }
    }
}
