using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace CommonUtils
{
    public class Receta
    {
        #region atributos 
        private string _Nombre;
        private float _CostoReceta;
        private int _IdReceta;
        private ArrayList _ListaInsumos= new ArrayList();
        #endregion
        
        #region Propiedades

        public int idReceta 
        {
            get { return _IdReceta; }
            set { _IdReceta = value; }
        }
        public string nombre 
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        public float costoReceta
        {
            get { return _CostoReceta; }
            set { _CostoReceta = value; }
        }

        public ArrayList listaInsumos
        {
            get { return _ListaInsumos; }
            set { _ListaInsumos = value; }
        }

        #endregion
    }
}
