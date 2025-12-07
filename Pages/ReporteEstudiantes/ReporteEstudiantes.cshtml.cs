using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Gestor_de_Proyectos_Académicos.BLL;

namespace Gestor_de_Proyectos_Académicos.Pages.ReporteEstudiantes
{
    public class ReportePersonalModel : PageModel
    {
        private readonly ReporteriaBLL _bll = new ReporteriaBLL();

        public IActionResult Index(int proyectoId)
        {
            var lista = _bll.ObtenerReporteEstudiantesProyecto(proyectoId);
            return View(lista);

        }
    }
}