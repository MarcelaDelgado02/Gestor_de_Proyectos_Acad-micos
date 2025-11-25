using Gestor_de_Proyectos_Académicos.BLL;
using Gestor_de_Proyectos_Académicos.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
namespace Gestor_de_Proyectos_Académicos.Pages.Estudiante.Tareas
{
    public class TareasEstModel : PageModel
    {
        private readonly TareaBLL tareaBLL;

        // Constructor con inyección de dependencias
        public TareasEstModel(TareaBLL tareaBLL)
        {
            tareaBLL = tareaBLL;
        }

        [BindProperty(SupportsGet = true)]
        public int IdProyecto { get; set; }

        [BindProperty]
        public string Titulo { get; set; } = string.Empty;

        [BindProperty]
        public string Descripcion { get; set; } = string.Empty;

        [BindProperty]
        public DateTime FechaLimite { get; set; } = DateTime.Now.AddDays(7);

        public List<Tarea> Tareas { get; set; } = new();

        [TempData]
        public string Mensaje { get; set; } = string.Empty;
        public string MensajeError { get; set; } = string.Empty;

        public void OnGet()
        {
            int idUsuario = HttpContext.Session.GetInt32("IdUsuario") ?? 0;

            if (idUsuario == 0)
            {
                MensajeError = "La sesión expiró. Inicie sesión nuevamente.";
                return;
            }

            try
            {
                if (IdProyecto <= 0)
                {
                    MensajeError = "Proyecto no válido.";
                    return;
                }

                Tareas = tareaBLL.ObtenerTareasPorProyecto(IdProyecto);

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
            int idUsuario = HttpContext.Session.GetInt32("IdUsuario") ?? 0;
            string cedulaUsuario = HttpContext.Session.GetString("Cedula") ?? "";

            if (idUsuario == 0 || string.IsNullOrEmpty(cedulaUsuario))
            {
                MensajeError = "La sesión expiró. Inicie sesión nuevamente.";
                return Page();
            }

            try
            {
                if (IdProyecto <= 0)
                {
                    MensajeError = "Proyecto inválido.";
                    return Page();
                }

                var nuevaTarea = new Tarea
                {
                    IDProyecto = IdProyecto,
                    TituloTarea = Titulo,
                    DescripcionTarea = Descripcion,
                    IDUsuario = idUsuario, // El estudiante se asigna a sí mismo
                    FechaLimiteTarea = FechaLimite,
                    EstadoTarea = "Pendiente"
                };

                tareaBLL.CrearTarea(nuevaTarea, cedulaUsuario);

                Mensaje = "Tarea creada exitosamente";
                TempData["Mensaje"] = Mensaje;

                // Limpiar el formulario
                Titulo = string.Empty;
                Descripcion = string.Empty;
                FechaLimite = DateTime.Now.AddDays(7);

                return RedirectToPage(new { IdProyecto });
            }
            catch (Exception ex)
            {
                MensajeError = ex.Message;
                return Page();
            }
        }

    }
}
