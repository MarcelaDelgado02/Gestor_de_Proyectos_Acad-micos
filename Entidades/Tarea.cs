namespace Gestor_de_Proyectos_Académicos.Entidades
{
    public class Tarea
    {
        public int idTarea { get; set; } 
        public string tituloTarea { get; set;} 
        public string despripcionTarea { get; set; } 
        public DateTime? fechaLimite { get; set; } 
        public string estadoTarea { get; set; } 
        public int IDProyecto { get; set; } 
        public int IDUsuario { get; set; }
    }
}
