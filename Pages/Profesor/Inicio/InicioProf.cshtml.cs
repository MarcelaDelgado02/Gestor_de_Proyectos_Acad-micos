using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Gestor_de_Proyectos_Académicos.Pages.Profesor.Inicio
{
    public class InicioProfModel : PageModel
    {
        public string CedulaUsuario { get; set; } = string.Empty;

        public void OnGet()
        {
            CedulaUsuario = HttpContext.Session.GetString("Cedula") ?? string.Empty;
        }
    }
}
