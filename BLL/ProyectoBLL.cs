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

        public int CrearProyecto(string cedulaUsuario, Proyecto nuevoProyecto)
        {
            int respuestaRol;

            int idProyecto = proyectoDAL.CrearProyecto(cedulaUsuario, nuevoProyecto, out respuestaRol);

            if (respuestaRol != 1)
                throw new Exception("Solo los profesores pueden crear proyectos.");

            if (idProyecto <= 0)
                throw new Exception("No se pudo obtener el Id del proyecto creado.");

            return idProyecto;
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

        public List<Proyecto> ObtenerProyectosValidos(string cedula, int diasAviso)
        {
            var proyectos = proyectoDAL.ObtenerProyectos(cedula);

            DateTime hoy = DateTime.Now.Date;

            return proyectos
                .Where(p =>
                    p.EstadoProyecto.ToLower() != "completado" &&     
                    p.FechaFinalProyecto.Date >= hoy &&               
                    (p.FechaFinalProyecto.Date - hoy).TotalDays <= diasAviso
                )
                .OrderBy(p => p.FechaFinalProyecto)
                .ToList();
        }

    }

}
