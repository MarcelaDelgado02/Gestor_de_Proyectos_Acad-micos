using System;

namespace Gestor_de_Proyectos_Académicos.Entidades
{
    /// <summary>
    /// Representa un usuario del sistema con todos los campos necesarios para login y registro
    /// </summary>
    public class Usuario
    {
        public string cedulaUsuario { get; set; } = string.Empty;
        public string nombreUsuario { get; set; } = string.Empty;
        public string correoUsuario { get; set; } = string.Empty;
        public string telefonoUsuario { get; set; } = string.Empty;      
        public string contrasenaUsuario { get; set; } = string.Empty;    
        public int rolUsuario { get; set; }

        public Usuario() { }

        // Constructor para login
        public Usuario(string cedula, string nombre, string correo, int rol)
        {
            cedulaUsuario = cedula;
            nombreUsuario = nombre;
            correoUsuario = correo;
            rolUsuario = rol;
        }

        // Constructor completo para registro
        public Usuario(string cedula, string nombre, string correo, string telefono, string contrasena, int rol)
        {
            cedulaUsuario = cedula;
            nombreUsuario = nombre;
            correoUsuario = correo;
            telefonoUsuario = telefono;
            contrasenaUsuario = contrasena;
            rolUsuario = rol;
        }

        public override string ToString()
        {
            string rolTexto = rolUsuario == 1 ? "Profesor" :
                             rolUsuario == 2 ? "Estudiante" : "Sin rol";
            return $"Usuario: {nombreUsuario} ({cedulaUsuario}) - {rolTexto}";
        }
    }
}