using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Gestor_de_Proyectos_Académicos.Entidades;

namespace Gestor_de_Proyectos_Académicos.DAL
{
    public class UsuarioDAL
    {
        private ConexionBD conexion = new ConexionBD();


        public Usuario? Login(string correo, byte[] contrasenaHash)
        {
            using (SqlConnection conn = conexion.AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand("spLogin", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                // PARÁMETROS DE ENTRADA
                cmd.Parameters.AddWithValue("@CorreoUsuario", correo);
                cmd.Parameters.AddWithValue("@ContrasenaUsuario", contrasenaHash);

                // PARÁMETROS DE SALIDA
                cmd.Parameters.Add("@NombreUsuario", SqlDbType.NVarChar, 30).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ResultadoLogin", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@TomaCedula", SqlDbType.NVarChar, 12).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@RolUsuario", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@TomaIdUsuario", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                bool loginCorrecto = Convert.ToBoolean(cmd.Parameters["@ResultadoLogin"].Value);

                if (loginCorrecto)
                {
                    return new Usuario
                    {
                        IdUsuario = Convert.ToInt32(cmd.Parameters["@TomaIdUsuario"].Value),
                        cedulaUsuario = cmd.Parameters["@TomaCedula"].Value?.ToString() ?? "",
                        nombreUsuario = cmd.Parameters["@NombreUsuario"].Value?.ToString() ?? "",
                        correoUsuario = correo,
                        rolUsuario = Convert.ToInt32(cmd.Parameters["@RolUsuario"].Value)
                    };
                }
                return null;
            }
        }

        public bool ExisteCedula(string cedula) {
            using (SqlConnection conn = conexion.AbrirConexion()) {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Usuarios WHERE CedulaUsuario = @Cedula", conn);
                cmd.Parameters.AddWithValue("@Cedula", cedula);

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        
        }
        public bool ExisteCorreo(string correo)
        {
            using (SqlConnection conn = conexion.AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Usuarios WHERE CorreoUsuario = @Correo", conn);
                cmd.Parameters.AddWithValue("@Correo", correo);

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }

        public (bool exito, string errorMensaje) RegistrarUsuario(Usuario nuevoUsuario, byte[] contrasenaHash)
        {
            using (SqlConnection conn = conexion.AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand("spRegistrarUsuario", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CedulaUsuario", nuevoUsuario.cedulaUsuario);
                cmd.Parameters.AddWithValue("@NombreUsuario", nuevoUsuario.nombreUsuario);
                cmd.Parameters.AddWithValue("@ContrasenaUsuario", contrasenaHash);
                cmd.Parameters.AddWithValue("@CorreoUsuario", nuevoUsuario.correoUsuario);
                cmd.Parameters.AddWithValue("@TelefonoUsuario", nuevoUsuario.telefonoUsuario);
                cmd.Parameters.AddWithValue("@IDRolUsuario", nuevoUsuario.rolUsuario);

                try
                {
                    cmd.ExecuteNonQuery();
                    return (true, "Usuario registrado exitosamente");
                }
                catch (SqlException ex)
                {
                    // Capturar errores específicos del SQL (se debe cambiar en el futuro)
                    string errorMensaje = ex.Message;

                    if (errorMensaje.Contains("cédula ya existe"))
                        return (false, "La cédula ya está registrada en el sistema");
                    else if (errorMensaje.Contains("correo electrónico ya está registrado"))
                        return (false, "El correo electrónico ya está registrado");
                    else
                        return (false, $"Error al registrar: {errorMensaje}");
                }
                catch (Exception ex)
                {
                    return (false, $"Error general: {ex.Message}");
                }
            }
        }

    }

}