using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonUtils
{
    public class Empresa
    {
        #region Propiedades
        public string RazonSocial { get; set; }
        public string NombreEmpresa { get; set; }
        public string Direccion { get; set; }
        public string NIT { get; set; }
        public int SucursalNro { get; set; }
        public string Telefono { get; set; }
        public string Rubro { get; set; }
        #endregion

        public Empresa( )
        {

        }
    }
}
