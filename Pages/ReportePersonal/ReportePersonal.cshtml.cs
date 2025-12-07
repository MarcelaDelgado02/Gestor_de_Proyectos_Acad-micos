using Gestor_de_Proyectos_Académicos.BLL;
using Gestor_de_Proyectos_Académicos.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Gestor_de_Proyectos_Académicos.Pages.ReportePersonal
{
    public class ReportePersonalModel : PageModel
    {
        private readonly ReporteriaBLL reporteriaBLL = new ReporteriaBLL();



        public List<Reporte> ReporteEstudiante { get; set; } = new();
        public int IdEstudiante { get; set; }

        public void OnGet()
        {
            IdEstudiante = HttpContext.Session.GetInt32("IdUsuario") ?? 0;

            if (IdEstudiante == 0)
            {
                TempData["Error"] = "No hay sesión activa.";
                return;
            }

            ReporteEstudiante = reporteriaBLL.ObtenerReportePersonal(IdEstudiante);
        }
    }
}
