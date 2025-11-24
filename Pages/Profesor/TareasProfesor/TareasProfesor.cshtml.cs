using Gestor_de_Proyectos_Académicos.BLL;
using Gestor_de_Proyectos_Académicos.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace Gestor_de_Proyectos_Académicos.Pages.Profesor.TareasProfesor
{
    public class TareasProfesorModel : PageModel
    {

        private readonly TareaBLL tareaBLL;
        private readonly UsuarioBLL usuarioBLL;

       

        [BindProperty(SupportsGet = true)]
        public int IdProyecto { get; set; }

        [BindProperty]
        public int IdTarea { get; set; }

        [BindProperty]
        public string Titulo { get; set; }

        [BindProperty]
        public string Descripcion { get; set; }

        [BindProperty]
        public int IdAsignado { get; set; }

        [BindProperty]
        public DateTime FechaLimite { get; set; }

        [BindProperty]
        public string Estado { get; set; }

        public List<Tarea> Tareas { get; set; } = new();

        public string Mensaje { get; set; } = "";

        public TareasProfesorModel() { 
            tareaBLL = new TareaBLL();
            usuarioBLL = new UsuarioBLL();
        
        }

        

        public void OnGet()
        {
            string cedula = HttpContext.Session.GetString("Cedula") ?? "";
            int idUsuario = HttpContext.Session.GetInt32("IdUsuario") ?? 0;

            if (idUsuario == 0)
            {
                Mensaje = "La sesión expiró.";
                return;
            }

            try
            {
                Tareas = tareaBLL.ObtenerTareasPorProyecto(IdProyecto);

                if (!Tareas.Any())
                    Mensaje = "No hay tareas para este proyecto.";
            }
            catch (Exception ex)
            {
                Mensaje = $"Error al cargar tareas: {ex.Message}";
            }
        }

        public IActionResult OnPostCrear()
        {
            string cedula = HttpContext.Session.GetString("Cedula") ?? "";

            try
            {
                var nueva = new Tarea
                {
                    IDProyecto = IdProyecto,
                    IdAsignado = IdAsignado,   // profesor asigna a cualquier estudiante con un checkbox
                    tituloTarea = Titulo,
                    despripcionTarea = Descripcion,
                    fechaLimite = FechaLimite,
                    estadoTarea = "Pendiente",
                };
                tareaBLL.CrearTarea(nueva, cedula);
                return RedirectToPage(new { IdProyecto = IdProyecto });

            }
            catch (Exception ex)
            {

                Mensaje = ex.Message;
                return Page();
            }
        }   

            public IActionResult OnPostEditar() {
            string cedula = HttpContext.Session.GetString("Cedula") ?? "";

            try
            {
                var tarea = new Tarea
                {
                    idTarea = IdTarea,
                    tituloTarea = Titulo,
                    despripcionTarea = Descripcion,
                    fechaLimite = FechaLimite,
                    estadoTarea = Estado
                };
                tareaBLL.ModificarTarea(cedula, tarea);
                return RedirectToPage(new { IdProyecto = IdProyecto });

            }
            catch (Exception ex)
            {

                Mensaje = ex.Message;
                return Page();
            }


            }

        public IActionResult OnPostEliminar(int id) {

            string cedula = HttpContext.Session.GetString("Cedula") ?? "";

            try
            {
                tareaBLL.EliminarTarea(cedula, id);
                return RedirectToPage(new { IdProyecto = IdProyecto });
            }
            catch (Exception ex)
            {
                Mensaje = ex.Message;
                return Page();
            }
        }

    }
}
