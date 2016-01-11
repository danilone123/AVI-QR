using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Odbc;
using System.Data.SqlClient;

namespace CommonUtils
{
    public class Usuario
    {
        #region Atributos
        private int _IDUsuario;
        private string _nombre;
        private string _primerApellido;
        private string _segundoApellido;
        private string _otb;
        private string _direccion;
        private string _observaciones;
        private int _nitci;        
        private int _IDPagos;
        private string _Mensaje;
        private bool nuevo;
        public bool Existe;
        private bool _Guardado;
        #endregion

        #region Propiedades

        public int nitci
        {
            get { return _nitci; }
            set { _nitci = value; }
        }

        public string observaciones
        {
            get { return _observaciones; }
            set { _observaciones = value; }
        }

        public bool Guardado
        {
            get { return _Guardado; }
            set { _Guardado = value; }
        }


        public string Mensaje
        {
            get { return _Mensaje; }
            private set { _Mensaje = value; }
        }


        public int IDPagos
        {
            get { return _IDPagos; }
            set { _IDPagos = value; }
        }

        public string direccion
        {
            get { return _direccion; }
            set { _direccion = value; }
        }

        public string otb
        {
            get { return _otb; }
            set { _otb = value; }
        }

        public string segundoApellido
        {
            get { return _segundoApellido; }
            set { _segundoApellido = value; }
        }

        public string primerApellido
        {
            get { return _primerApellido; }
            set { _primerApellido = value; }
        }

        public string nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }


        public int IDUsuario
        {
            get { return _IDUsuario; }
            set { _IDUsuario = value; }
        }
        #endregion

        public Usuario()
        {
            nuevo = true;
        }
        public Usuario(int IDUsuario)
        {
            string sql = "SELECT * FROM usuario"
            + " WHERE usuario.IDUsuario='" + IDUsuario + "'";
            ConexionBD.AbrirConexion();
            SqlDataReader reader = ConexionBD.EjecutarConsultaReader(sql);

            if (reader.Read())
            {
                Existe = true;
                IDUsuario = reader.GetInt32(0);
                nombre = reader.GetString(1);
                primerApellido= reader.GetString(2);
                segundoApellido = reader.GetString(3);
                otb = reader.GetString(4);
                direccion= reader.GetString(5);
                reader.Close();
                ConexionBD.CerrarConexion();
            }
            else
            {
                ConexionBD.CerrarConexion();
            } 
        }

        public void GuardarClienteAP(string sql)
        {
            Mensaje = "";
            Guardado = false;
            if (nuevo)
            {
                if (Duplicado())
                {
                    Guardado = false;
                    Mensaje = "El usuario ya está registrado";
                    return;
                }
            }
            try
            {
                ConexionBD.Actualizar(sql);
                Guardado = true;
            }
            catch (OdbcException e)
            {
                // TODO: Put the exception message on a windows notification.
                Mensaje = "Error con la Base de Datos, este usuario ya existe";
                Guardado = false;
            }
            catch (Exception ee)
            {
                // TODO: Put the exception message on a windows notification.
                Guardado = false;
            }

        }

        private bool Duplicado()
        {
            bool Resultado = false;
            string Sql = "select * from usuario where IDUsuario='" + IDUsuario + "'";
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

    }
}
