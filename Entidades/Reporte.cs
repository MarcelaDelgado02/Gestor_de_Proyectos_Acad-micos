using System.ComponentModel;
using System.Data;

namespace Gestor_de_Proyectos_Académicos.Entidades
{
    public class Reporte
    {
        public int ProyectoId { get; set; }
        public string NombreProyecto { get; set; }
        public int TotalTareas { get; set; }
        public int TareasCompletas { get; set; }
        public decimal PorcentajeAvance { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int EstudianteId { get; set; }
        public string NombreEstudiante { get; set; }
        public int TareasCompletadasEstudiante { get; set; }

        public List<Tarea> TareasPersonales { get; set; } = new List<Tarea>();
    }
}
    
    