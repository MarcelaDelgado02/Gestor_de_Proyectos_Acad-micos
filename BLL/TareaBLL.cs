using Gestor_de_Proyectos_Académicos.DAL;
using Gestor_de_Proyectos_Académicos.Entidades;

namespace Gestor_de_Proyectos_Académicos.BLL
{
    public class TareaBLL
    {
        private readonly TareaDAL tareaDAL;

        public TareaBLL()
        {
            tareaDAL = new TareaDAL();
        }

        public List<Tarea> ObtenerTareasPorProyecto(int idProyecto)
        {
            return tareaDAL.ObtenerTareasPorProyecto(idProyecto);
        }

        public void CrearTarea(Tarea tarea, string cedulaUsuario)
        {
            if (string.IsNullOrWhiteSpace(tarea.TituloTarea))
                throw new Exception("El título de la tarea es obligatorio.");

            if (tarea.FechaLimiteTarea == DateTime.MinValue) // 👈 CAMBIADO: Validar fecha mínima
                throw new Exception("Debe indicar una fecha límite.");

            if (tarea.IDUsuario <= 0) // 👈 CAMBIADO: Usar IDUsuario en lugar de IdAsignado
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
    }

   
}

