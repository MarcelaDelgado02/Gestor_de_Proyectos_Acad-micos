using Gestor_de_Proyectos_Académicos.BLL;
using Gestor_de_Proyectos_Académicos.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Gestor_de_Proyectos_Académicos.DAL;

namespace Gestor_de_Proyectos_Académicos.Pages.Profesor.Inicio
{
    public class InicioProfModel : PageModel
    {
        private readonly ProyectoBLL proyectoBLL;
        private readonly UsuarioBLL usuarioBLL;

        public InicioProfModel()
        {
            proyectoBLL = new ProyectoBLL();
            usuarioBLL = new UsuarioBLL();
        }

        [BindProperty]
        public Proyecto NuevoProyecto { get; set; } = new Proyecto();
        [BindProperty]
        public List<int> EstudiantesSeleccionados { get; set; } = new List<int>();

        public List<Usuario> EstudiantesDisponibles { get; set; } = new List<Usuario>();
        public string CedulaUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public List<Proyecto> Proyectos { get; set; } = new List<Proyecto>();
        public string Mensaje { get; set; } = string.Empty;
        public bool TieneProyectos => Proyectos != null && Proyectos.Any();

        public void OnGet()
        {
            
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

                EstudiantesDisponibles = usuarioBLL.ObtenerEstudiantesAsignarProyecto(0);

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
                // 1. Crear proyecto y obtener el ID
                int idProyectoCreado = proyectoBLL.CrearProyecto(CedulaUsuario, NuevoProyecto);

                if (idProyectoCreado <= 0)
                {
                    TempData["Mensaje"] = "No se pudo crear el proyecto.";
                    return RedirectToPage();
                }

                // 2. Asignar estudiantes si hay seleccionados
                if (EstudiantesSeleccionados != null && EstudiantesSeleccionados.Any())
                {
                    usuarioBLL.AsignarEstudiantesAProyecto(idProyectoCreado, EstudiantesSeleccionados, CedulaUsuario);
                }

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
