using Gestor_de_Proyectos_Académicos.BLL;
using Gestor_de_Proyectos_Académicos.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Gestor_de_Proyectos_Académicos.Pages.Estudiante.Tareas
{
    public class TareasEstModel : PageModel
    {
        private readonly TareaBLL tareaBLL;

        public TareasEstModel()
        {
            tareaBLL = new TareaBLL();
        }

        // Recibe por GET y POST
        [BindProperty(SupportsGet = true)]
        public int IdProyecto { get; set; }

        // Campos para crear tarea
        [BindProperty] public string Titulo { get; set; }
        [BindProperty] public string Descripcion { get; set; }
        [BindProperty] public DateTime FechaLimite { get; set; }

        public List<Tarea> Tareas { get; set; } = new();
        public string Mensaje { get; set; } = "";

        public void OnGet()
        {
            try
            {
                if (IdProyecto <= 0)
                {
                    Mensaje = "Proyecto no válido.";
                    return;
                }

                Tareas = tareaBLL.ObtenerTareasPorProyecto(IdProyecto);

                if (Tareas == null || Tareas.Count == 0)
                {
                    Mensaje = "Aún no se han asignado tareas para este proyecto.";
                }
            }
            catch (Exception ex)
            {
                Mensaje = $"Error al cargar tareas: {ex.Message}";
            }
        }

        public IActionResult OnPostCrear()
        {
            try
            {
                if (IdProyecto <= 0)
                {
                    Mensaje = "Proyecto inválido.";
                    return Page();
                }

                string cedula = HttpContext.Session.GetString("Cedula");

                var nuevaTarea = new Tarea
                {
                    IDProyecto = IdProyecto,
                    IDUsuario = 0, // estudiante se asigna a sí mismo (SP valida)
                    tituloTarea = Titulo,
                    despripcionTarea = Descripcion,
                    fechaLimite = FechaLimite,
                    estadoTarea = "Pendiente"
                };

                tareaBLL.CrearTarea(nuevaTarea, cedula);

                // Actualizar tabla
                return RedirectToPage(new { IdProyecto = IdProyecto });
            }
            catch (Exception ex)
            {
                Mensaje = ex.Message;
                OnGet();
                return Page();
            }
        }
    }
}
