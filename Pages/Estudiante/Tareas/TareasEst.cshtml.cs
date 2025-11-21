using Gestor_de_Proyectos_Académicos.BLL;
using Gestor_de_Proyectos_Académicos.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;

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
        [BindProperty]
        public string Titulo { get; set; }

        [BindProperty]
        public string Descripcion { get; set; }

        [BindProperty]
        public DateTime FechaLimite { get; set; }

        public List<Tarea> Tareas { get; set; } = new();

        public string Mensaje { get; set; } = "";

        public void OnGet()
        {
            int idUsuario = HttpContext.Session.GetInt32("IdUsuario") ?? 0;


            if (idUsuario == 0)
            {
                Mensaje = "La sesión expiró. Inicie sesión nuevamente.";
                return;
            }

            try
            {
                if (IdProyecto <= 0)
                {
                    Mensaje = "Proyecto no válido.";
                    return;
                }

                Tareas = tareaBLL.ObtenerTareasPorProyecto(IdProyecto);

                if (Tareas == null || !Tareas.Any())
                    Mensaje = "Aún no se han asignado tareas para este proyecto.";
            }
            catch (Exception ex)
            {
                Mensaje = $"Error al cargar tareas: {ex.Message}";
            }
        }

        public IActionResult OnPostCrear()
        {
            int idUsuario = HttpContext.Session.GetInt32("IdUsuario") ?? 0;
            string cedulaUsuario = HttpContext.Session.GetString("Cedula") ?? "";

            if (idUsuario == 0 || string.IsNullOrEmpty(cedulaUsuario))
            {
                Mensaje = "La sesión expiró. Inicie sesión nuevamente.";
                return Page();
            }

            try
            {
                if (IdProyecto <= 0)
                {
                    Mensaje = "Proyecto inválido.";
                    return Page();
                }

                var nuevaTarea = new Tarea
                {
                    IDProyecto = IdProyecto,
                    IDUsuario = idUsuario,
                    IdAsignado = idUsuario,
                    tituloTarea = Titulo,
                    despripcionTarea = Descripcion,
                    fechaLimite = FechaLimite,
                    estadoTarea = "Pendiente"
                };

                // Pasar la cédula, no el ID
                tareaBLL.CrearTarea(nuevaTarea, cedulaUsuario);

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
