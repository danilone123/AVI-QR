using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;

namespace CommonUtils
{
    public class ConexionBD
    {

        private static string _StringDeConexion = string.Empty;
        private static Usuarios _UsuarioActual = null;
        public static SqlConnection Conexion = new SqlConnection( );
        private static bool Conectado = false;
        private static Usuario _IDActual;

        public static Usuario  IDActual
        {
            get { return _IDActual; }
            set { _IDActual = value; }
        }

        #region Propiedades       
        public static string StringDeConexion
        {
            get { return ConexionBD._StringDeConexion; }
            set
            {
                ConexionBD._StringDeConexion = value;
                Conexion.ConnectionString = StringDeConexion;
            }
        }

        public static string GetConnectionString( )
        {
            //return @"Data Source=localhost\SQLEXPRESS;Database=jam;Initial Catalog=jam;Integrated Security=SSPI;";
            return @"Data Source=localhost\SQLEXPRESS;Database=aviador;Initial Catalog=aviador;Integrated Security=SSPI;";
            //return @"Data Source=localhost\SQLEXPRESS;Initial Catalog=Capresso;User Id='sa';Password='llajta'";
        }
        
        public static Usuarios UsuarioActual
        {
            get { return _UsuarioActual; }
            set
            {
                _UsuarioActual = value;
            }
        }
        #endregion

        #region Metodos Publicos
        public static void AbrirConexion( )
        {
            if ( !Conectado )
            {                
                Conexion.Open( );
                Conectado = true;
            }
        }

        public static void CerrarConexion( )
        {
            if ( Conectado )
            {
                Conexion.Close( );
                Conectado = false;
            }
        }
        
        public static void Actualizar( string SQL )
        {
            AbrirConexion( );
            SqlCommand Comando = Conexion.CreateCommand( );
            Comando.CommandText = SQL;
            Comando.ExecuteNonQuery( );
            CerrarConexion( );            
        }

        public static decimal ActualizarRetornandoId( string SQL )
        {
            AbrirConexion( );
            SqlCommand Comando = Conexion.CreateCommand( );
            Comando.CommandText = SQL;
            Comando.ExecuteNonQuery( );
            Comando.CommandText = "SELECT @@IDENTITY";
            decimal newID = ( decimal ) ( Comando.ExecuteScalar( ) );
            CerrarConexion( );
            return newID;
        }

        public static SqlDataReader EjecutarConsultaReader( string ConsultaSQL )
        {
            SqlCommand Comando = Conexion.CreateCommand( );
            Comando.CommandText = ConsultaSQL;
            SqlDataReader DReader = Comando.ExecuteReader( );
            return DReader;            
        }

        public static DataTable TraerTabla( string StrTabla )
        {
            string ConsultaSQL = "SELECT * FROM " + StrTabla;
            DataTable DTabla = EjecutarConsulta( ConsultaSQL );
            DTabla.TableName = StrTabla;
            return DTabla;
        }

        
        public static DataTable EjecutarConsulta( string ConsultaSQL )
        {
            SqlCommand Comando = Conexion.CreateCommand( );
            Comando.CommandText = ConsultaSQL;
            SqlDataAdapter Adaptador = new SqlDataAdapter( Comando );
            DataTable Tabla = new DataTable( );
            Adaptador.Fill( Tabla );
            return Tabla;            
        }

        public static string EjecutarConsultaEspecial(string ConsultaSQL)
        {
            Conexion.Open();
            SqlCommand cmd = Conexion.CreateCommand();
            cmd.CommandText = ConsultaSQL;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            adapter.Fill(table);
            
            Conexion.Close();
            return table.Rows[0][0].ToString();
        }

        public static SqlConnection getConnection()
        {
            return Conexion;
        }

        #endregion
    }
}
