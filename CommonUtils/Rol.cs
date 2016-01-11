using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Odbc;
using System.Data.SqlClient;

namespace CommonUtils
{
    class Rol
    {
        #region Atributos
        private string _IDrol;
        private string _descripcion;
        private string _nombre;
        private bool Nuevo;
        private bool _Existe;
        private bool _Guardado;
        private string _Mensaje;
        
        #endregion

        #region Propiedades

        public string Mensaje
        {
            get { return _Mensaje; }
            private set { _Mensaje = value; }
        }

        public bool Guardado
        {
            get { return _Guardado; }
            private set { _Guardado = value; }
        }


        public bool Existe
        {
            get { return _Existe; }
            private set { _Existe = value; }
        }



        public string nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }


        public string descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }


        public string IDrol            
        {
            get { return _IDrol; }
            set { _IDrol = value; }
        }
        #endregion

        #region Constructores
        public Rol()
        {
            Nuevo = true;
        }

        public Rol(string idRol)
        {
            Nuevo = false;
            string Sql = "SELECT * From roles WHERE IDrol='" + idRol + "'";
            ConexionBD.AbrirConexion();
            SqlDataReader reader = ConexionBD.EjecutarConsultaReader(Sql);
            if (reader.HasRows)
            {
                reader.Read();
                idRol = reader.GetString(0);
                nombre = reader.GetString(1);
                descripcion= reader.GetString(2);
            }
            ConexionBD.CerrarConexion();
        }
        #endregion

        #region Métodos
        public bool Guardar(List<string> IDsPrivilegios)
        {
            bool modifica = false;
            if (IDsPrivilegios.Count == 0)
            {
                modifica = false;
            }
            else
            {
                string SqlRoles = "";
                string SqlAsignacionPrivilegios = "DELETE FROM asigmenu WHERE IDmenuItem = '{0}'";
                Guardado = false;

                if (Nuevo)
                {
                    SqlRoles = "INSERT INTO roles VALUES('{0}','{1}','{2}');";
                }
                else
                {
                    SqlRoles = "UPDATE roles SET nombre='{1}', descripcion='{2}' "
                    + "WHERE IDrol='{0}';";
                }

                SqlRoles = string.Format(SqlRoles, IDrol, nombre, descripcion);
                SqlAsignacionPrivilegios = string.Format(SqlAsignacionPrivilegios, IDrol);
                modifica = true;

                try
                {
                    ConexionBD.Actualizar(SqlRoles);
                    ConexionBD.Actualizar(SqlAsignacionPrivilegios);
                    GuardarPrivilegios(IDsPrivilegios);
                    Guardado = true;
                    Mensaje = "Se creo con éxito un nuevo Rol";
                    
                }
                catch (Exception ee)
                {
                    Mensaje = "No se pudo crear, por favor verifique";
                    Guardado = false;
                }
            }
            return modifica;
        }

        private void GuardarPrivilegios(List<string> IDsPrivilegios)
        {
            string Sql = "";
            foreach (string IDmenuItem in IDsPrivilegios)
            {
                Sql = "INSERT INTO asigmenu VALUES ('{0}','{1}');";
                Sql = string.Format(Sql, IDmenuItem, IDrol);
                ConexionBD.Actualizar(Sql);
            }
        }

    }
        #endregion
}
