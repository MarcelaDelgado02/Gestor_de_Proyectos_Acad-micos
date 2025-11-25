using Gestor_de_Proyectos_Académicos.DAL;
using Gestor_de_Proyectos_Académicos.Entidades;

namespace Gestor_de_Proyectos_Académicos.BLL
{
    public class TareaBLL
    {
        private readonly TareaDAL tareaDAL;
        private readonly UsuarioDAL usuarioDAL; 

        public TareaBLL()
        {
            tareaDAL = new TareaDAL();
            usuarioDAL = new UsuarioDAL();  
        }

        public List<Tarea> ObtenerTareasPorProyecto(int idProyecto, string cedulaSolicitante)
        {
            var tareas = tareaDAL.ObtenerTareasPorProyecto(idProyecto);

            // Obtener el rol del usuario
            int rol = usuarioDAL.ObtenerRolPorCedula(cedulaSolicitante);

            if (rol == 1) // PROFESOR
            {
                return tareas; // todas
            }

            if (rol == 2) // ESTUDIANTE
            {
                return tareas.Where(t => t.CedulaEstudiante == cedulaSolicitante).ToList();
            }

            return new List<Tarea>();
        }

        public void CrearTarea(Tarea tarea, string cedulaUsuario)
        {
            if (string.IsNullOrWhiteSpace(tarea.TituloTarea))
                throw new Exception("El título de la tarea es obligatorio.");

            if (tarea.FechaLimiteTarea == DateTime.MinValue) //Validar fecha mínima
                throw new Exception("Debe indicar una fecha límite.");

            if (tarea.IDUsuario <= 0) //  Usar IDUsuario en lugar de IdAsignado
                throw new Exception("Debe indicar un usuario asignado válido.");

            if (string.IsNullOrWhiteSpace(cedulaUsuario))
                throw new Exception("Cédula de usuario no válida.");

            tareaDAL.CrearTarea(tarea, cedulaUsuario);
        }

        public void EliminarTarea(string cedulaUsuario, int idTarea)
        {
            tareaDAL.EliminarTarea(cedulaUsuario, idTarea);
        }

        public void ModificarTarea(string cedulaUsuario, Tarea tarea)
        {
            tareaDAL.ModificarTarea(cedulaUsuario, tarea);
        }

        public void ActualizarEstadoTareaEstudiante(int idTarea, string cedulaEstudiante, string nuevoEstado) {
            tareaDAL.ActualizarEstadoTareaEstudiante(idTarea, cedulaEstudiante, nuevoEstado);
        }
    }


   
}

