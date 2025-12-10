using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Gestor_de_Proyectos_Académicos.Pages.Logout
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            // Limpiar toda la sesión
            HttpContext.Session.Clear();

            // Limpiar TempData
            TempData.Clear();

            // Redirigir al login con un parámetro para indicar que fue logout
            return RedirectToPage("/Login/Login", new { logout = true });
        }
    }
}
