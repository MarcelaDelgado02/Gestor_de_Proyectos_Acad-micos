// Entidades/Proyecto.cs
namespace Gestor_de_Proyectos_Académicos.Entidades
{
    public class Proyecto
    {
        public int IdProyecto { get; set; }
        public string NombreProyecto { get; set; } = string.Empty;
        public string DescripcionProyecto { get; set; } = string.Empty;
        public DateTime FechaInicioProyecto { get; set; }
        public DateTime FechaFinalProyecto { get; set; }
        public string EstadoProyecto { get; set; } = string.Empty;

        // Propiedad para el color del estado 
        public string ColorEstado { get; set; } = string.Empty;

        public bool EstaPorFinalizar => FechaFinalProyecto.Date <= DateTime.Now.AddDays(5).Date
                             && FechaFinalProyecto.Date >= DateTime.Now.Date;
        public int DiasRestantes =>
        (FechaFinalProyecto.Date - DateTime.Now.Date).Days;

        public bool EstaVencido => FechaFinalProyecto.Date < DateTime.Now.Date;

        public string ColorAviso =>
        EstaVencido ? "danger" :
        EstaPorFinalizar ? "warning" :
        "secondary";


    }


}