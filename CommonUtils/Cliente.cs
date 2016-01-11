using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonUtils
{
    public class Cliente
    {
        #region Atributos

        private int _IdCliente;
        private string _Nombre;
        private string _Telefono;
        private string _Nit;
        private string _Direccion;
        private int _NumeroPedido;
        #endregion

        #region propiedades
        public int IdCliente
        {
            get { return _IdCliente; }
            set { _IdCliente = value; }
        }

        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        public string Telefono
        {
            get { return _Telefono; }
            set { _Telefono = value; }
        }

        public string NIT
        {
            get { return _Nit; }
            set { _Nit = value; }
        }

        public string Direccion
        {
            get { return _Direccion; }
            set { _Direccion = value; }
        }

        public int NumeroPedido
        {
            get { return _NumeroPedido; }
            set { _NumeroPedido = value; }
        }

        #endregion 

        public Cliente(int id,string nombre, string telefono,string nit,string direccion,int numeroPedido)
        {
            _IdCliente = id;
            _Nombre = nombre;
            _Telefono = telefono;
            _Nit = nit;
            _Direccion = direccion;
            _NumeroPedido = numeroPedido;
        }
    }
}
