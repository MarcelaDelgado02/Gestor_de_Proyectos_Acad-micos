using Gestor_de_Proyectos_Académicos.BLL;
using Gestor_de_Proyectos_Académicos.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Gestor_de_Proyectos_Académicos.Pages.Profesor.Inicio
{
    public class InicioProfModel : PageModel
    {
        private readonly ProyectoBLL proyectoBLL;

        public InicioProfModel()
        {
            proyectoBLL = new ProyectoBLL();
        }

        [BindProperty]
        public Proyecto NuevoProyecto { get; set; } = new Proyecto();
        public string CedulaUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public List<Proyecto> Proyectos { get; set; } = new List<Proyecto>();
        public string Mensaje { get; set; } = string.Empty;
        public bool TieneProyectos => Proyectos != null && Proyectos.Any();

        public void OnGet()
        {
            // ❌ ESTA LÍNEA SE ELIMINA
            // string cedula = HttpContext.Session.GetString("Cedula");

            // ✔ ESTA ES LA ÚNICA CORRECTA
            CedulaUsuario = HttpContext.Session.GetString("Cedula");

            if (string.IsNullOrEmpty(CedulaUsuario))
            {
                Mensaje = "No se encontró la cédula del usuario.";
                return;
            }

            try
            {
                Proyectos = proyectoBLL.ObtenerProyectos(CedulaUsuario);
                PrepararDatosVista();

                if (!Proyectos.Any())
                    Mensaje = "No tiene proyectos registrados aún.";
            }
            catch (Exception ex)
            {
                Mensaje = $"Error al cargar proyectos: {ex.Message}";
            }
        }

        public IActionResult OnPostCrearProyecto()
        {
            CedulaUsuario = HttpContext.Session.GetString("Cedula");

            if (string.IsNullOrEmpty(CedulaUsuario))
            {
                Mensaje = "No se encontró la cédula del usuario.";
                return RedirectToPage();
            }

            try
            {
                proyectoBLL.CrearProyecto(CedulaUsuario, NuevoProyecto);
                TempData["Mensaje"] = "Proyecto creado correctamente.";
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = $"Error al crear el proyecto: {ex.Message}";
            }

            return RedirectToPage();
        }

        private void PrepararDatosVista()
        {
            foreach (var proyecto in Proyectos)
                proyecto.ColorEstado = ObtenerColorEstado(proyecto.EstadoProyecto);
        }

        private string ObtenerColorEstado(string estado)
        {
            if (string.IsNullOrEmpty(estado))
                return "secondary";

            return estado.ToLower() switch
            {
                "activo" => "success",
                "en progreso" or "enprogreso" => "warning",
                "finalizado" or "completado" => "primary",
                "eliminado" => "danger",
                _ => "secondary"
            };
        }

        public IActionResult OnPostEditarProyecto()
        {
            CedulaUsuario = HttpContext.Session.GetString("Cedula");

            try
            {
                proyectoBLL.EditarProyecto(CedulaUsuario, NuevoProyecto);
                TempData["Mensaje"] = "Proyecto modificado correctamente.";
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = $"Error al editar el proyecto: {ex.Message}";
            }

            return RedirectToPage();
        }

        public IActionResult OnPostEliminarProyecto(int idProyecto)
        {
            CedulaUsuario = HttpContext.Session.GetString("Cedula");

            try
            {
                proyectoBLL.EliminarProyecto(CedulaUsuario, idProyecto);
                TempData["Mensaje"] = "Proyecto eliminado correctamente.";
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = $"Error al eliminar el proyecto: {ex.Message}";
            }

            return RedirectToPage();
        }
    }
}
