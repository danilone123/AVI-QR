using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonUtils
{
    public class Dosificacion
    {
        #region Propiedades
        public string FechaLimite { get; set; }
        public int Activa { get; set; }
        public string Llave { get; set; }
        public string Autorizacion { get; set; }
        public string NroFactura { get; set; }
        public int DosificacionID { get; set; }
        #endregion

        public Dosificacion( )
        {

        }
    }
}
