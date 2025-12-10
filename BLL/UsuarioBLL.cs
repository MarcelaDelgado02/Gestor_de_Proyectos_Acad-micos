using System;
using System.Security.Cryptography;
using System.Text;
using System.Net.Mail;
using Gestor_de_Proyectos_Académicos.DAL;
using Gestor_de_Proyectos_Académicos.Entidades;

namespace Gestor_de_Proyectos_Académicos.BLL
{
    public class UsuarioBLL
    {
        private UsuarioDAL usuarioDAL = new UsuarioDAL();

       
        public Usuario? IniciarSesion(string correo, string contrasena)
        {
            if (string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(contrasena))
            {
                throw new ArgumentException("Correo y contraseña son obligatorios");
            }

            byte[] passwordHash = HashPassword(contrasena);
            return usuarioDAL.Login(correo, passwordHash);
        }

       
        public (bool exito, string mensaje) RegistrarUsuario(Usuario nuevoUsuario)
        {
           
            if (string.IsNullOrWhiteSpace(nuevoUsuario.cedulaUsuario) ||
                string.IsNullOrWhiteSpace(nuevoUsuario.nombreUsuario) ||
                string.IsNullOrWhiteSpace(nuevoUsuario.correoUsuario) ||
                string.IsNullOrWhiteSpace(nuevoUsuario.telefonoUsuario) ||
                string.IsNullOrWhiteSpace(nuevoUsuario.contrasenaUsuario))
            {
                return (false, "Todos los campos son obligatorios");
            }

           
            if (nuevoUsuario.rolUsuario != 1 && nuevoUsuario.rolUsuario != 2)
            {
                return (false, "Rol debe ser 1 (Profesor) o 2 (Estudiante)");
            }

            
            if (!IsValidEmail(nuevoUsuario.correoUsuario))
            {
                return (false, "El formato del correo electrónico no es válido");
            }

            

           

           

            try
            {
                // Hashear contraseña y registrar
                byte[] contrasenaHash = HashPassword(nuevoUsuario.contrasenaUsuario);
                var resultado = usuarioDAL.RegistrarUsuario(nuevoUsuario, contrasenaHash);
                return resultado;
            }
            catch (Exception ex)
            {
                return (false, $"Error al registrar usuario: {ex.Message}");
            }
        }

        
        public bool CorreoExiste(string correo)
        {
            return usuarioDAL.ExisteCorreo(correo);
        }

       
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

       
        private byte[] HashPassword(string password)
        {
            using (SHA512 sha512 = SHA512.Create())
            {
                return sha512.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public byte[] GenerarHashContrasena(string contrasena)
        {
            return HashPassword(contrasena);
        }

        //  MÉTODO DE PRUEBA
        public void ProbarHash(string contrasena)
        {
            byte[] hash = HashPassword(contrasena);
            Console.WriteLine($"PRUEBA DE HASH:");
            Console.WriteLine($"Texto: {contrasena}");
            Console.WriteLine($"Longitud hash: {hash.Length} bytes");
            Console.WriteLine($"Hash (hex): {BitConverter.ToString(hash).Replace("-", "")}");
        }


        public List<Usuario> ObtenerEstudiantesPorProyecto(int idProyecto) {
            return usuarioDAL.ObtenerEstudiantesPorProyecto(idProyecto);

        }

        public List<Usuario> ObtenerEstudiantesAsignarProyecto(int idProyecto)
        {
            return usuarioDAL.ObtenerEstudiantesAsignarProyecto(idProyecto);
        }

        public void AsignarEstudiantesAProyecto(int idProyecto, List<int> idsEstudiantes, string cedulaProfesor)
        {
            foreach (var idEstudiante in idsEstudiantes)
            {
                usuarioDAL.AsignarEstudianteAProyecto(idProyecto, idEstudiante, cedulaProfesor);
            }
        }


    }
}