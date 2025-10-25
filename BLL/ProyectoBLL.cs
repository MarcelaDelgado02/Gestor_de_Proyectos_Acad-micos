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
    }
}
