using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonUtils
{
    public class RecetaInsumo
    {
        private CommonUtils.Insumo _Insumo;
        private float _Cantidad;
        private string _Nombre;

        #region Propiedades

        public CommonUtils.Insumo insumo 
        {
            get { return _Insumo; }
            set { _Insumo = value; }
        }

        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        public float cantidad 
        {
            get { return _Cantidad; }
            set { _Cantidad = value; }
        }
        #endregion 
    }
}
