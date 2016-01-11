using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonUtils
{
   public class Insumo
    {
        #region Atributos
        private float _Cantidad;
        private float _CostoUnidad;
        private string _Nombre;
        private string _Presentacion;
        private string _Marca;
        private string _Unidad;
        private int _IdInsumo;
        private float _CostoPresentacion;
        private float _CantidadInsumoEnIventario;
        #endregion

        #region Propiedades

        public float cantidadInsumoIventario 
        {
            get { return _CantidadInsumoEnIventario; }
            set { _CantidadInsumoEnIventario = value; }
        }

        public float costoPresentacion
        {
            get { return _CostoPresentacion; }
            set { _CostoPresentacion = value; }
        }

        public float cantidad 
        {
            get { return _Cantidad; }
            set { _Cantidad = value; }
        }

        public float costoUnidad
        {
            get { return _CostoUnidad; }
            set { _CostoUnidad = value; }
        }

        public string nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        public string presentacion 
        {
            get { return _Presentacion; }
            set { _Presentacion = value; }
        }

        public string marca
        {
            get { return _Marca; }
            set { _Marca = value; }
        }

        public string unidad 
        {
            get { return _Unidad; }
            set { _Unidad = value; }
        }

        public int idInsumo 
        {
            get { return _IdInsumo; }
            set { _IdInsumo = value; }
        }
        
        #endregion
        public Insumo()
        {
 
        }
        public Insumo(int idInsumo,string nombre, string marca, string unidad, string presentacion, float costoUnidad, float cantidad)
        {
            this.cantidad = cantidad;
            this.costoUnidad = costoUnidad;
            this.idInsumo = idInsumo;
            this.nombre = nombre;
            this.marca = marca;
            this.presentacion = presentacion;
            this.unidad = unidad;

        }
      
    }
}
