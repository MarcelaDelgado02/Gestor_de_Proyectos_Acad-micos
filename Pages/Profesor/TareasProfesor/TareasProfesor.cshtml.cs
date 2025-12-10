using Gestor_de_Proyectos_Académicos.BLL;
using Gestor_de_Proyectos_Académicos.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Gestor_de_Proyectos_Académicos.Pages.Profesor.TareasProfesor
{
    public class TareasProfesorModel : PageModel
    {
        private readonly TareaBLL tareaBLL;
        private readonly UsuarioBLL usuarioBLL;


        public TareasProfesorModel(TareaBLL tareaBLL, UsuarioBLL usuarioBLL)
        {
            this.tareaBLL = tareaBLL;
            this.usuarioBLL = usuarioBLL;
        }

        [BindProperty(SupportsGet = true)]
        public int IdProyecto { get; set; }
        

        [BindProperty]
        public string Titulo { get; set; } = string.Empty;

        [BindProperty]
        public string Descripcion { get; set; } = string.Empty;

        [BindProperty]
        public int IdAsignado { get; set; }

        [BindProperty]
        public DateTime FechaLimite { get; set; } = DateTime.Now.AddDays(7);

        [BindProperty]
        public string Estado { get; set; }

        public List<Tarea> Tareas { get; set; } = new();
        public List<Usuario> Estudiantes { get; set; } = new();
        public List<SelectListItem> Estados { get; set; } = new();

        [TempData]
        public string Mensaje { get; set; } = string.Empty;
        public string MensajeError { get; set; } = string.Empty;


        public void OnGet()
        {
            try
            {
                if (IdProyecto <= 0)
                {
                    MensajeError = "Proyecto no válido.";
                    return;

                }

                string cedulaUsuario = HttpContext.Session.GetString("Cedula") ?? "";

                Tareas = tareaBLL.ObtenerTareasPorProyecto(IdProyecto, cedulaUsuario);

                Estudiantes = usuarioBLL.ObtenerEstudiantesPorProyecto(IdProyecto);
                Estados = new List<SelectListItem>
                {
                    new SelectListItem { Value = "Pendiente", Text = "Pendiente" },
                    new SelectListItem { Value = "En Progreso", Text = "En Progreso" },
                    new SelectListItem { Value = "Completada", Text = "Completada" }
                };
                if (!Tareas.Any())
                {
                    Mensaje = "Aún no se han asignado tareas para este proyecto.";
                }
            }
            catch (Exception ex)
            {
                MensajeError = $"Error al cargar tareas: {ex.Message}";
            }
        }

        public IActionResult OnPostCrear()
        {

            try
            {
                string cedulaUsuario = HttpContext.Session.GetString("Cedula") ?? "";

                if (string.IsNullOrEmpty(cedulaUsuario))
                {
                    MensajeError = "La sesión expiró. Inicie sesión nuevamente.";
                    return Page();
                }

                if (IdProyecto <= 0)
                {
                    MensajeError = $"Proyecto inválido. {IdProyecto}";
                    return Page();
                }

                var nuevaTarea = new Tarea
                {
                    IDProyecto = IdProyecto,
                    TituloTarea = Titulo,
                    DescripcionTarea = Descripcion,
                    IDUsuario = IdAsignado, // El estudiante asignado
                    FechaLimiteTarea = FechaLimite,
                    EstadoTarea = Estado
                };

                tareaBLL.CrearTarea(nuevaTarea, cedulaUsuario);
                Mensaje = "Tarea creada exitosamente";
                TempData["Mensaje"] = Mensaje;

                // Limpiar el formulario
                Titulo = string.Empty;
                Descripcion = string.Empty;
                IdAsignado = 0;
                FechaLimite = DateTime.Now.AddDays(7);
                Estado = string.Empty;

                return RedirectToPage(new { IdProyecto });
            }
            catch (Exception ex)
            {
                MensajeError = ex.Message;
                return RedirectToPage(new { IdProyecto });
            }

        }

        public IActionResult OnPostEliminar(int idTarea)
        {

            try
            {
                string cedulaUsuario = HttpContext.Session.GetString("Cedula") ?? "";

                if (string.IsNullOrEmpty(cedulaUsuario))
                {
                    MensajeError = "La sesión expiró. Inicie sesión nuevamente.";
                    return Page();
                }

                tareaBLL.EliminarTarea(cedulaUsuario, idTarea);
                Mensaje = "Tarea eliminada exitosamente";
                TempData["Mensaje"] = Mensaje;

                return RedirectToPage(new { IdProyecto });
            }
            catch (Exception ex)
            {

                MensajeError = $"Error al eliminar la tarea: {ex.Message}";
                return RedirectToPage(new { IdProyecto });
            }
        }


        public IActionResult OnPostModificarEstado(int idTarea, string nuevoEstado)
        {
            try
            {
                string cedulaUsuario = HttpContext.Session.GetString("Cedula") ?? "";

                if (string.IsNullOrEmpty(cedulaUsuario))
                {
                    MensajeError = "La sesión expiró. Inicie sesión nuevamente.";
                    return Page();
                }

                // Buscar la tarea existente


                var tareas = tareaBLL.ObtenerTareasPorProyecto(IdProyecto, cedulaUsuario);

                var tarea = tareas.FirstOrDefault(t => t.IdTarea == idTarea);

                if (tarea != null)
                {
                    tarea.EstadoTarea = nuevoEstado;
                    tareaBLL.ModificarTarea(cedulaUsuario, tarea);

                    Mensaje = "Estado de tarea actualizado exitosamente";
                    TempData["Mensaje"] = Mensaje;
                }

                return RedirectToPage(new { IdProyecto });
            }
            catch (Exception ex)
            {
                MensajeError = $"Error al modificar el estado: {ex.Message}";
                return Page();
            }
        }
        public IActionResult OnPostModificar(int IdTarea, string Titulo, string Descripcion, DateTime FechaLimite, string Estado)
        {
            try
            {
                string cedulaUsuario = HttpContext.Session.GetString("Cedula") ?? "";

                if (string.IsNullOrEmpty(cedulaUsuario))
                {
                    MensajeError = "La sesión expiró. Inicie sesión nuevamente.";
                    return Page();
                }

                var tareaModificada = new Tarea
                {
                    IdTarea = IdTarea,
                    TituloTarea = Titulo,
                    DescripcionTarea = Descripcion,
                    FechaLimiteTarea = FechaLimite,
                    EstadoTarea = Estado
                };

                tareaBLL.ModificarTarea(cedulaUsuario, tareaModificada);

                TempData["Mensaje"] = "Tarea modificada exitosamente";

                return RedirectToPage(new { IdProyecto });
            }
            catch (Exception ex)
            {
                TempData["MensajeError"] = $"Error al modificar: {ex.Message}";
                return RedirectToPage(new { IdProyecto });
            }
        }


    }
}
