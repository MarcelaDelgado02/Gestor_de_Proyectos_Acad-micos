using Gestor_de_Proyectos_Académicos.BLL;
using Gestor_de_Proyectos_Académicos.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Gestor_de_Proyectos_Académicos.Pages.ReporteEstudiantes
{
    public class ReportePersonalModel : PageModel
    {
        private readonly ReporteriaBLL reporteriaBLL = new ReporteriaBLL();

        public List<Reporte> Estudiantes { get; set; } = new();

        public void OnGet(int? proyectoId)
        {
            if (proyectoId.HasValue)
                Estudiantes = reporteriaBLL.ObtenerReporteEstudiantesProyecto(proyectoId.Value);
        }
    }
}