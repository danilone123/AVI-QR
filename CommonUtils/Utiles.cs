using System;
using System.Collections.Generic;
using System.Text;

namespace CommonUtils
{
    public class Utiles
    {
        private static string _UltimoMensaje;

        private static bool _ExisteError;

        public static bool ExisteError
        {
            get { return _ExisteError; }
            private set { _ExisteError = value; }
        }


        public static string UltimoMensaje
        {
            get { return _UltimoMensaje; }
            private set { _UltimoMensaje = value; }
        }

        private static void SetError( string StrMesg )
        {
            ExisteError = true;
            UltimoMensaje = StrMesg;
        }

        private static void LimpiarEstado( )
        {
            UltimoMensaje = "";
            ExisteError = false;
        }
    }
}