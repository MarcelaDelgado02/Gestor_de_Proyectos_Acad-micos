namespace Gestor_de_Proyectos_Académicos.Entidades
{
    public class Reporte
    {
        public string NombreProyecto { get; set; }
        public int TotalTareas { get; set; }
        public int TareasCompletadas { get; set; }
        public decimal PorcentajeAvance {  get; set; }  
           
    }

    public class ReporteEstudianteProyecto
    {
        public string NombreEstudiante { get; set; }
        public int TareasAsignadas { get; set; }
        public int TareasCompletadas { get; set; }
        public decimal PorcentajeCompletado { get; set; }


    }

    public class ReportePersonal
    {
        public string NombrePersonal { get; set; }
        public string NombreTarea { get; set; }
        public string Estado { get; set; }
        public DateTime? FechaCompletado { get; set; }
        public DateTime FechaVencimiento { get; set; }

    }

}

    
    