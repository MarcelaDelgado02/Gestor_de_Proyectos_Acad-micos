using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Gestor_de_Proyectos_Académicos.BLL;
using Gestor_de_Proyectos_Académicos.Entidades;
using Microsoft.AspNetCore.Authorization;
using Azure;

namespace Gestor_de_Proyectos_Académicos.Pages.Reporte_Proyecto
{
    [Authorize(Roles = "Profesor")]
    public class ProyectosModel : PageModel
    {

        private readonly ReporteriaBLL _reporteriaBLL;

        public ProyectosModel(IConfiguration configuration)
        {

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            _reporteriaBLL = new ReporteriaBLL(connectionString);
        }

        [BindProperty]
        public ReporteFiltros filtros { get; set; } = new ReporteFiltros();

        public List<Reportes> Reportes { get; set; } = new List<Reportes>();

        public void OnGet()
        {
            Reportes = _reporteriaBLL.GenerarReporteEstudiantesProyecto(filtros);
        }

        public IActionResult OnPost()
        {
            Reportes = _reporteriaBLL.GenerarReporteEstudiantesProyecto(filtros);

            return Page();  
        }
    }

}
