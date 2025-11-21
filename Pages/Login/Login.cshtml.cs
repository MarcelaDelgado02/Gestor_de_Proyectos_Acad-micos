using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Gestor_de_Proyectos_Académicos.BLL;
using Gestor_de_Proyectos_Académicos.Entidades;

namespace Gestor_de_Proyectos_Académicos.Pages.Login
{
    public class LoginModel : PageModel
    {
        private readonly UsuarioBLL usuarioBLL;

        public LoginModel()
        {
            usuarioBLL = new UsuarioBLL();
        }

        //  PROPIEDADES ENLAZADAS 
        [BindProperty]
        public string CorreoUsuario { get; set; } = string.Empty;

        [BindProperty]
        public string ContrasenaUsuario { get; set; } = string.Empty;

        public string Mensaje { get; set; } = string.Empty;

        public void OnGet()
        {
            HttpContext.Session.Clear();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Mensaje = "Por favor, complete todos los campos correctamente.";
                return Page();
            }

            try
            {
                //  AUTENTICAR 
                Usuario usuario = usuarioBLL.IniciarSesion(CorreoUsuario, ContrasenaUsuario);

                if (usuario != null)
                {
                    // LOGIN EXITOSO
                    HttpContext.Session.SetString("Cedula", usuario.cedulaUsuario);
                    HttpContext.Session.SetString("Nombre", usuario.nombreUsuario);
                    HttpContext.Session.SetString("Correo", usuario.correoUsuario);
                    HttpContext.Session.SetInt32("Rol", usuario.rolUsuario);
                    HttpContext.Session.SetInt32("IdUsuario", usuario.IdUsuario);


                    if (usuario.rolUsuario == 1)
                        return RedirectToPage("/Profesor/Inicio/InicioProf");
                    else
                        return RedirectToPage("/Estudiante/Inicio/InicioEst");
                }
                else
                {
                    // CREDENCIALES INCORRECTAS
                    Mensaje = "Correo o contraseña incorrectos.";
                    return Page();
                }
            }
            catch (Exception ex)
            {
                Mensaje = $"Error en el sistema: {ex.Message}";
                return Page();
            }
        }
    }
}