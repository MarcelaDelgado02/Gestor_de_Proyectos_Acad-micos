using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Gestor_de_Proyectos_Académicos.BLL;
using Gestor_de_Proyectos_Académicos.Entidades;
using Microsoft.AspNetCore.Authorization;



namespace Gestor_de_Proyectos_Académicos.Pages.Reporte_Estudiantes
{
    [Authorize(Roles = "Profesor")]
    public class reporte_estudianteModel : PageModel
    {

        private readonly ReporteriaBLL _reporteriaBLL;


        public EstudianteProyectoModel(IConfiguration configuration)
        {

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            _reporteriaBLL = new ReporteriaBLL(connectionString);
        }

        public List<Reporte> Reportes { get; set; } = new List<Reporte>(); 

        public string NombreProyecto { get; set; }
        public List<ReporteFiltros> Reporte { get; private set; }

        public IActionResult OnGet(int id)
        {
            Reporte = _reporteriaBLL.GenerarReporteEstudiantesProyecto(id);

            if (Reporte.Any())
                NombreProyecto = Reporte.First().NombreProyecto;

            else
                NombreProyecto = "Proyecto no encontrado";

            return Page();

        }
    }
    
}
