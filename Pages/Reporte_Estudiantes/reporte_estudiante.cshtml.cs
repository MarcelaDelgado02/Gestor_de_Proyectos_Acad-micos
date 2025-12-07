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

    }
    
}
