using Gestor_de_Proyectos_Académicos.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Gestor_de_Proyectos_Académicos.ViewComponents
{
    public class UserInfoViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var usuario = new Usuario
            {
                IdUsuario = HttpContext.Session.GetInt32("IdUsuario") ?? 0,
                cedulaUsuario = HttpContext.Session.GetString("Cedula") ?? "",
                nombreUsuario = HttpContext.Session.GetString("Nombre") ?? "",
                correoUsuario = HttpContext.Session.GetString("Correo") ?? "",
                rolUsuario = HttpContext.Session.GetInt32("Rol") ?? 0

            };

            return View(usuario);
        }
    }
}
