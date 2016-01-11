using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonUtils
{
    class UsuariosSistema
    {
        #region atributos
        private int _IdUsuario;
        private string _Telefono;
        private string _Login;
        private string _Password;
        private string _XML;
        #endregion
        public int IdUser
        {
            get { return _IdUsuario; }
            set { _IdUsuario = value; }
        }
        public string Telefono {

            get { return _Telefono; }
            set { _Telefono = value; }
        }

        public string Login
        {
            get { return _Login; }
            set { _Login = value; }
        }

        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        public string XML
        {
            get { return _XML; }
            set { _XML = value; }
        }
    }
}
