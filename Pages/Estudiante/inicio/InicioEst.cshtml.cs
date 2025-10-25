using Gestor_de_Proyectos_Académicos.BLL;
using Gestor_de_Proyectos_Académicos.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Gestor_de_Proyectos_Académicos.Pages.Estudiante.inicio
{
    public class InicioEstModel : PageModel
    {



        private readonly ProyectoBLL proyectoBLL;

        public InicioEstModel()
        {

            proyectoBLL = new ProyectoBLL();

        }
        public string CedulaUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public List<Proyecto> Proyectos { get; set; }
        public string Mensaje { get; set; }
        public bool TieneProyectos => Proyectos != null && Proyectos.Any();
        public void OnGet()
        {
            CedulaUsuario = HttpContext.Session.GetString("Cedula");
            NombreUsuario = HttpContext.Session.GetString("Nombre");

            try
            {
                if (string.IsNullOrEmpty(CedulaUsuario))
                {
                    Mensaje = "no se encontro la cedula";
                    return;


                }

                Proyectos = proyectoBLL.ObtenerProyectos(CedulaUsuario);
             
                if (Proyectos.Any())
                {
                    Mensaje = "no esta tiene tareas encargadas";

                }
            }
            catch (Exception ex)
            {

                Mensaje = $"Error al cargar proyectos: {ex.Message}";
                Proyectos = new List<Proyecto>();
            }
        }
        private void PrepararDatosVista() 
        {
            foreach (var proyecto in Proyectos) 
            {
                proyecto.ColorEstado = ObtenerColorEstado(proyecto.EstadoProyecto);
            }
        
        }

        private string ObtenerColorEstado(string estado)
        {
            if (string.IsNullOrEmpty(estado))
                return "secondary";

            return estado.ToLower() switch
            {
                "activo" => "success",
                "en progreso" or "enprogreso" => "warning",
                "completado" or "finalizado" => "primary",
                _ => "secondary"

            };
        }

    }
}
