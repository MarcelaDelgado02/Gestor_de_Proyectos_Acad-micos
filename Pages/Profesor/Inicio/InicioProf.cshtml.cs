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

        // 🧾 Propiedades del usuario y vista
        [BindProperty]
        public Proyecto NuevoProyecto { get; set; } = new Proyecto();
        public string CedulaUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public List<Proyecto> Proyectos { get; set; } = new List<Proyecto>();
        public string Mensaje { get; set; } = string.Empty;
        public bool TieneProyectos => Proyectos != null && Proyectos.Any();

        // ✅ Cargar proyectos al entrar a la página
        public void OnGet()
        {
            CedulaUsuario = HttpContext.Session.GetString("Cedula");
            NombreUsuario = HttpContext.Session.GetString("Nombre");

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
                return Page();
            }

            try
            {
                proyectoBLL.CrearProyecto(CedulaUsuario, NuevoProyecto);
                Mensaje = "Proyecto creado correctamente.";

                // Limpiar campos y recargar lista
                NuevoProyecto = new Proyecto();
                Proyectos = proyectoBLL.ObtenerProyectos(CedulaUsuario);
                PrepararDatosVista();
            }
            catch (Exception ex)
            {
                Mensaje = $"Error al crear el proyecto: {ex.Message}";
            }

            return Page();
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
    }
}

