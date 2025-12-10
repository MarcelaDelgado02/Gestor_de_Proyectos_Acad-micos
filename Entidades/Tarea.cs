namespace Gestor_de_Proyectos_Académicos.Entidades
{
    public class Tarea
    {
        public int IdTarea { get; set; }
        public string TituloTarea { get; set; } = string.Empty;
        public string DescripcionTarea { get; set; } = string.Empty;
        public DateTime FechaLimiteTarea { get; set; }
        public string EstadoTarea { get; set; } = string.Empty;

        public int IDProyecto { get; set; }

        
        public int IDUsuario { get; set; }  // FK real en la BD
        public string CedulaEstudiante { get; set; } = string.Empty;   
        public string NombreEstudiante { get; set; } = string.Empty;   

        // creador  tarea
        public int IDUsuarioCreador { get; set; }
        public DateTime FechaCreacion { get; set; }

        public bool EstaPorVencer => FechaLimiteTarea.Date <= DateTime.Now.AddDays(5).Date
                                  && FechaLimiteTarea.Date >= DateTime.Now.Date;

        public bool EstaVencida => FechaLimiteTarea.Date < DateTime.Now.Date;

        public string ColorAviso =>
            EstaVencida ? "danger" :
            EstaPorVencer ? "warning" :
            "secondary";
    }
}
