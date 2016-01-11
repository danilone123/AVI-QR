using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Data.Odbc;
using System.Security.Cryptography;
using System.Data.SqlClient;


namespace CommonUtils
{
    public class Usuarios
    {
        public DataTable TablaMenu;
        private static log4net.ILog log = log4net.LogManager.GetLogger( "Usuarios" );

        #region Atributos 

        private bool nuevo;
        public bool Existe;

        #endregion               

        #region Propiedades

        public string Password { get; set; }

        public bool Guardado { get; set; }

        public int UsuarioID { get; set; }

        public string Nombre { get; set; }

        public string Apellidos{ get; set; }

        public string Login{ get; set; }
        
        public string Mensaje{ get; set; }

        public string Privilegios { get; set; }
       
        public string NombreCompleto
        {
            get { return Nombre + " " + Apellidos; }
        }
        #endregion

        #region Constructores
        public Usuarios()
        {
            nuevo = true;
        }
        public Usuarios( string login , string pass )
        {
            pass = MD5_ComputeHexaHash(pass);
            string sql = "SELECT UsuariosSistema.UsuarioId, UsuariosSistema.Nombre, UsuariosSistema.Apellidos, UsuariosSistema.PrivilegiosXML "
            + "FROM UsuariosSistema "
            + "WHERE UsuariosSistema.Login='" + login + "' "
            + "and UsuariosSistema.Password='" + pass + "'";
            ConexionBD.AbrirConexion( );
            SqlDataReader reader = ConexionBD.EjecutarConsultaReader( sql );

            if ( reader.Read( ) )
            {
                Existe = true;
                UsuarioID = Convert.ToInt32( reader.GetValue( 0 ) );
                Nombre = reader.GetString( 1 );
                Apellidos = reader.GetString( 2 );
                Privilegios = reader.GetString( 3 );
                reader.Close( );
                ConexionBD.CerrarConexion( );
            }
            else
            {
                ConexionBD.CerrarConexion( );                
            }
        }

        private string MD5_ComputeHexaHash(string text)
        {
            // Gets the MD5 hash for text
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] data = Encoding.Default.GetBytes(text);
            byte[] hash = md5.ComputeHash(data);
            // Transforms as hexa
            string hexaHash = "";

            foreach ( byte b in hash )
                hexaHash += String.Format( "{0:x2}" , b );

            // Returns MD5 hexa hash
            return hexaHash;
        }
        public void GuardarUsuario(string sql)
        {
            Mensaje = "";
            Guardado = false;
            if (nuevo)
            {
                if (Duplicado())
                {
                    Guardado = false;
                    Mensaje = "Repetido";
                    return;
                }

            }
            try
            {                
                ConexionBD.Actualizar(sql);
                Guardado = true;
            }
            catch (OdbcException oex)
            {
                log.Error( oex.Message , oex );
                Mensaje = "Error con la BD, este Usuario ya existe";
                Guardado = false;
            }
            catch (Exception ex)
            {
                log.Error( ex.Message , ex );
                Guardado = false;
            }          
        }
        public void GuardarRoles(string sql)
        {
            ConexionBD.Actualizar(sql);
        }

        private bool Duplicado()
        {
            bool Resultado = false;
            string Sql = "select * from Usuarios where IDUsuario='" + UsuarioID + "'";
            /*
             * cuando trabajamos con OleDbDataReader, es necesario siempre abrir
             * y cerrar la conexion
             */
            ConexionBD.AbrirConexion();
            SqlDataReader reader = ConexionBD.EjecutarConsultaReader(Sql);
            if (reader.HasRows)
            {
                Resultado = true;
            }

            ConexionBD.CerrarConexion();
            return Resultado;
        }
        #endregion
    }
}
