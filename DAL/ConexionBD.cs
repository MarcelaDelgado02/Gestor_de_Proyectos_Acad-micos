using Microsoft.Data.SqlClient;

namespace Gestor_de_Proyectos_Académicos.DAL
{
    public class ConexionBD
    {
        private readonly string connectionString; 

        public ConexionBD()
        {
            connectionString = "Server=localhost;Database=GestordeProyectosAcademicos_db;Integrated Security=true;TrustServerCertificate=true;Encrypt=false";
        }

        public SqlConnection AbrirConexion()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            return conn;
        }
    }
}