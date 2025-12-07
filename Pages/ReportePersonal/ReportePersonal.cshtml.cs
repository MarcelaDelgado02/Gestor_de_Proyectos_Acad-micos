using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Gestor_de_Proyectos_Académicos.BLL;

namespace Gestor_de_Proyectos_Académicos.Pages.ReportePersonal
{
    public class ReportePersonalModel : PageModel
    {
        private readonly ReporteriaBLL _bll = new ReporteriaBLL();

        public IActionResult Index(int estudianteId)
        {
            var resultado = _bll.ObtenerReportePersonal(estudianteId);
            return View(resultado);
        }
    }
}
