using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Gestor_de_Proyectos_Académicos.Pages.Logout
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
           
            HttpContext.Session.Clear();

          
            TempData.Clear();

            
            return RedirectToPage("/Login/Login", new { logout = true });
        }
    }
}
