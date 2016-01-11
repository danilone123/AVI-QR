using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonUtils
{
    public class FacturaReporte
    {
        private int _FacturaId;
        private decimal _FacturaAutorizacion;
        private long _FacturaNro;
        private string _FacturaCodigoControl;
        private string _FacturaAnulada;
        private DateTime _FechaTransaccion;
        private ulong _FacturaNit;
        private string _FacturaNombre;
        private decimal _FacturaMontoParcial;
        private decimal _FacturaTotal;

        public DateTime FechaTransaccion
        {
            get { return _FechaTransaccion; }
            set { _FechaTransaccion = value; }
        }


        public int FacturaId {
            get { return _FacturaId; }
            set {  _FacturaId = value; }
        }

        public decimal FacturaAutorizacion {
            get { return _FacturaAutorizacion; }
            set { _FacturaAutorizacion=value; }
        }

        public long FacturaNro {
            get { return _FacturaNro; }
            set { _FacturaNro = value; }
        }

        public string FacturaCodigoControl {
            get { return _FacturaCodigoControl; }
            set { _FacturaCodigoControl = value; }
        }

        public string FacturaAnulada
        {
            get { return _FacturaAnulada; }
            set { _FacturaAnulada = value; }
        }

        public decimal FacturaMontoParcial
        {
            get { return _FacturaMontoParcial; }
            set { _FacturaMontoParcial = value; }
        }

        public decimal FacturaMontoTotal
        {
            get { return _FacturaTotal; }
            set { _FacturaTotal = value; }
        }

        public string FacturaNombre
        {
            get { return _FacturaNombre; }
            set { _FacturaNombre = value; }
        }

        public ulong FacturaNit
        {
            get { return _FacturaNit; }
            set { _FacturaNit = value; }
        }

          public FacturaReporte()
        {
 
        }
    }
}
