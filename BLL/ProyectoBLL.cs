using Gestor_de_Proyectos_Académicos.DAL;
using Gestor_de_Proyectos_Académicos.Entidades;

namespace Gestor_de_Proyectos_Académicos.BLL
{
    public class ProyectoBLL
    {

        private readonly ProyectoDAL proyectoDAL;

        public ProyectoBLL()
        {
            proyectoDAL = new ProyectoDAL();

        }

        public List<Proyecto> ObtenerProyectos(string cedulaUsuario)
        {
            if (string.IsNullOrWhiteSpace(cedulaUsuario))
            {
                Console.WriteLine("ALGO PASO");

            }
            return proyectoDAL.ObtenerProyectos(cedulaUsuario);



        }

        public bool TieneProyectosAsignados(string cedulaUsuario)
        {
            var proyectos = ObtenerProyectos(cedulaUsuario);
            return proyectos.Any();


        }

        public void CrearProyecto(string cedulaUsuario, Proyecto nuevoProyecto) {

            if (string.IsNullOrWhiteSpace(nuevoProyecto.NombreProyecto))
                throw new ArgumentException("El nombre del proyecto no puede estar vacío.");

            if (nuevoProyecto.FechaInicioProyecto > nuevoProyecto.FechaFinalProyecto)
                throw new ArgumentException("La fecha de inicio no puede ser posterior a la fecha final.");

            if (nuevoProyecto.FechaFinalProyecto < DateTime.Now)
                throw new ArgumentException("La fecha de finalización no puede ser anterior a la fecha actual.");

            
            if (string.IsNullOrWhiteSpace(nuevoProyecto.EstadoProyecto))
                nuevoProyecto.EstadoProyecto = "Activo";

            var exito = proyectoDAL.CrearProyecto(cedulaUsuario, nuevoProyecto);
            if (exito) { 
                throw new Exception("No se pudo crear el proyecto en la base de datos.");

            }
        }


        public void EditarProyecto(string cedulaUsuario, Proyecto proyecto) {

            if (proyecto == null)
                throw new ArgumentException("El proyecto no puede ser nulo.");

            if (string.IsNullOrWhiteSpace(proyecto.NombreProyecto))
                throw new ArgumentException("El nombre del proyecto no puede estar vacío.");

            if (proyecto.FechaInicioProyecto > proyecto.FechaFinalProyecto)
                throw new ArgumentException("La fecha de inicio no puede ser posterior a la fecha final.");

            if (proyecto.FechaFinalProyecto < DateTime.Now)
                throw new ArgumentException("La fecha final no puede ser anterior a la fecha actual.");

            if (string.IsNullOrWhiteSpace(proyecto.EstadoProyecto))
                proyecto.EstadoProyecto = "Activo";

            var exito = proyectoDAL.EditarProyecto(cedulaUsuario, proyecto);
            if (!exito)
                throw new Exception("No se pudo modificar el proyecto en la base de datos.");

        }

        public void EliminarProyecto(string cedulaUsuario, int idProyecto)
        {
            if (idProyecto <= 0)
                throw new ArgumentException("El ID del proyecto no es válido.");

            var exito = proyectoDAL.EliminarProyecto(cedulaUsuario, idProyecto);
            if (!exito)
                throw new Exception("No se pudo eliminar el proyecto.");
        }
    }
}
