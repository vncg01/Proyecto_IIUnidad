using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Datos
{
    public class UsuarioDatos
    {
        //Crear metodos que nos permitan acceder a los datos de la tabla USUARIO
        //Metodo asincrono que nos permita validar si el usuario ingreso correctamente usuario y contraseña
        public async Task<bool> LoginAsync(string codigo, string clave)
        {
            bool valido = false;
            try
            {
                //Nos permite consultar en nuestra base de datos el usuario y contraseña 
                string sql = "SELECT 1 FROM usuarios WHERE Codigo=@Codigo AND Clave=@Clave;";

                //Utilizamos la cadena de conexion de mysql
                using (MySqlConnection _conexion = new MySqlConnection(CadenaConexion.Cadena))
                {
                    await _conexion.OpenAsync();
                    using (MySqlCommand comando = new MySqlCommand(sql, _conexion))
                    {
                        comando.CommandType = System.Data.CommandType.Text;
                        comando.Parameters.Add("@Codigo", MySqlDbType.VarChar, 20).Value = codigo;
                        comando.Parameters.Add("@Clave", MySqlDbType.VarChar, 120).Value = clave;

                        valido = Convert.ToBoolean(await comando.ExecuteScalarAsync());
                    }
                }
            }
            catch (Exception ex)
            { }
            return valido;
        }

        public async Task<DataTable> DevolverListaAsync()
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT * FROM usuarios";
                using (MySqlConnection _conexion = new MySqlConnection(CadenaConexion.Cadena))
                {
                    await _conexion.OpenAsync();
                    using (MySqlCommand comando = new MySqlCommand(sql, _conexion))
                    {
                        comando.CommandType = System.Data.CommandType.Text;

                        MySqlDataReader dr = (MySqlDataReader)await comando.ExecuteReaderAsync();
                        dt.Load(dr);
                    }
                }
            }
            catch (Exception ex)
            {}
            return dt;
        }

        public async Task<bool> InsertarAsync(Usuarios usuario)
        {
            bool inserto = false;
            try
            {
                string sql = "INSERT INTO usuarios VALUES (@Codigo, @Nombre, @Clave, @Correo, @Rol, @EstaActivo)";
                using (MySqlConnection _conexion = new MySqlConnection(CadenaConexion.Cadena))
                {
                    await _conexion.OpenAsync();
                    using (MySqlCommand comando = new MySqlCommand(sql, _conexion))
                    {
                        comando.CommandType = System.Data.CommandType.Text;
                        comando.Parameters.Add("@Codigo", MySqlDbType.VarChar, 20).Value = usuario.Codigo;
                        comando.Parameters.Add("@Nombre", MySqlDbType.VarChar, 50).Value = usuario.Nombre;
                        comando.Parameters.Add("@Clave", MySqlDbType.VarChar, 120).Value = usuario.Clave;
                        comando.Parameters.Add("@Correo",MySqlDbType.VarChar, 45).Value = usuario.Correo;
                        comando.Parameters.Add("@Rol", MySqlDbType.VarChar, 20).Value = usuario.Rol;
                        comando.Parameters.Add("@EstaActivo", MySqlDbType.Bit, 20).Value = usuario.EstaActivo;

                        await comando.ExecuteNonQueryAsync();
                        inserto = true;
                    }
                }
            }
            catch (Exception ex)
            { }
            return inserto;
        }

        public async Task<bool> ActualizarAsync(Usuarios usuario)
        {
            bool actualizar = false;
            try
            {
                string sql = "UPDATE usuarios SET Nombre=@Nombre, Clave=@Clave, Correo=@Correo, Rol=@Rol, EstaActivo=@EstaActivo WHERE Codigo=@Codigo";
                using (MySqlConnection _conexion = new MySqlConnection(CadenaConexion.Cadena))
                {
                    await _conexion.OpenAsync();
                    using (MySqlCommand comando = new MySqlCommand(sql, _conexion))
                    {
                        comando.CommandType = System.Data.CommandType.Text;
                        comando.Parameters.Add("@Codigo", MySqlDbType.VarChar, 20).Value = usuario.Codigo;
                        comando.Parameters.Add("@Nombre", MySqlDbType.VarChar, 50).Value = usuario.Nombre;
                        comando.Parameters.Add("@Clave", MySqlDbType.VarChar, 120).Value = usuario.Clave;
                        comando.Parameters.Add("@Correo", MySqlDbType.VarChar, 45).Value = usuario.Correo;
                        comando.Parameters.Add("@Rol", MySqlDbType.VarChar, 20).Value = usuario.Rol;
                        comando.Parameters.Add("@EstaActivo", MySqlDbType.Bit, 20).Value = usuario.EstaActivo;

                        await comando.ExecuteNonQueryAsync();
                        actualizar = true;
                    }
                }
            }
            catch (Exception ex)
            { }
            return actualizar;
        }

        public async Task<bool> EliminarAsync(string codigo)
        {
            bool eliminar = false;
            try
            {
                string sql = "DELETE FROM usuarios WHERE Codigo=@Codigo";
                using (MySqlConnection _conexion = new MySqlConnection(CadenaConexion.Cadena))
                {
                    await _conexion.OpenAsync();
                    using (MySqlCommand comando = new MySqlCommand(sql, _conexion))
                    {
                        comando.CommandType = System.Data.CommandType.Text;
                        comando.Parameters.Add("@Codigo", MySqlDbType.VarChar, 20).Value = codigo;

                        await comando.ExecuteNonQueryAsync();
                        eliminar = true;
                    }
                }
            }
            catch (Exception ex)
            {  }
            return eliminar;
        }
    }
}
