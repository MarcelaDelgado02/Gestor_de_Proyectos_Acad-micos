namespace Gestor_de_Proyectos_Académicos.Entidades
{
    public class Tarea
    {
        public int IdTarea { get; set; }
        public string TituloTarea { get; set; } = string.Empty;
        public string DescripcionTarea { get; set; } = string.Empty; 
        public DateTime FechaLimiteTarea { get; set; } 
        public string EstadoTarea { get; set; } 
        public int IDProyecto { get; set; }
        public int IDUsuario { get; set; } 
        public int IDUsuarioCreador { get; set; } 
        public DateTime FechaCreacion { get; set; } 

       
    }
}
