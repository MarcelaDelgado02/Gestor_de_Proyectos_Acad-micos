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
      
    }
}
