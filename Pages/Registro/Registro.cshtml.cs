using Gestor_de_Proyectos_Académicos.BLL;
using Gestor_de_Proyectos_Académicos.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Gestor_de_Proyectos_Académicos.Pages.Registro
{
    public class RegistroModel : PageModel
    {
        private readonly UsuarioBLL usuarioBLL; // 

        public RegistroModel()
        {
            usuarioBLL = new UsuarioBLL();
        }

        [BindProperty]
        public Usuario NuevoUsuario { get; set; } = new Usuario();

        public string Mensaje { get; set; } = string.Empty;
        public string Error { get; set; } = string.Empty;

        public void OnGet()
        {
            
        }

        public IActionResult OnPost() 
        {
            if (!ModelState.IsValid)
            {
                Error = "Por favor, complete todos los campos correctamente.";
                return Page(); 
            }

            try
            {
                var (exito, mensaje) = usuarioBLL.RegistrarUsuario(NuevoUsuario);

                if (exito)
                {
                    // Limpiar el formulario
                    NuevoUsuario = new Usuario();
                    ModelState.Clear();

                    // Redirigir al login con mensaje de éxito
                    TempData["RegistroExitoso"] = " Usuario registrado correctamente. Ahora puede iniciar sesión.";
                    return RedirectToPage("/Login/Login");
                }
                else
                {
                    Error = mensaje;
                    return Page(); //  Mostrar error en la misma página
                }
            }
            catch (Exception ex)
            {
                Error = $" Error al registrar: {ex.Message}";
                return Page(); //  Mostrar error en la misma página
            }
        }
    }
}