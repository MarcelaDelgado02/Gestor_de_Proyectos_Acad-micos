using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Gestor_de_Proyectos_Académicos.BLL;
using Gestor_de_Proyectos_Académicos.Entidades;
using Microsoft.AspNetCore.Authorization;
using Gestor_de_Proyectos_Académicos.Pages.Reporte_Proyecto;
using Gestor_de_Proyectos_Académicos.Pages.Reporte_Estudiantes;

namespace Gestor_de_Proyectos_Académicos.Pages.Reporte_Personal
{
    [Authorize(Roles = "Estudiante")]
    public class reporte_personalModel : PageModel
    {
        private readonly ReporteriaBLL _reporteriaBLL;

        public reporte_personalModel(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaulConnection");

            _reporteriaBLL = new ReporteriaBLL(connectionString);   
        }

        public List<ReporteFiltros> Reporte { get; private set; }

        public void OnGet()
        {
            var estudianteId = int.Parse(User.FindFirst("UsuarioId").Value);
            Reporte = _reporteriaBLL.GenerarReportePersonal(estudianteId);

        }
    }
}
