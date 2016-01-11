using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HFUtils.Impuestos.Facturacion.Factura;
namespace CommonUtils
{
    public class DataBaseLog
    {
        private String formatElem(String elementName, String elementValue)
        {
            return "<" + elementName + ">" + elementValue + "</" + elementName + ">";
        }

        public void RecordOperationInDataBase(String nombreEmpresa,
                              String razonSocial,
                              String sucursal,
                              String direccion,
                              String rubro,
                              String telefono,
                              String departamento,
                              String nit,
                              String facturaNro,
                              String autorizacionNro,
                              String fecha,
                              String clienteNombre,
                              String clienteNit,
                              Factura factura,
                              String literal,
                              String codigoControl,
                              String fechalimite,
                              double montoTotal)
        {
            String datosEmp = formatElem("DatosEmpresa",
                              formatElem("Nombre", nombreEmpresa) +
                              formatElem("RazonSocial", razonSocial) +
                              formatElem("Sucursal", sucursal) +
                              formatElem("Direccion", direccion) +
                              formatElem("Rubro", rubro) +
                              formatElem("Telefono", telefono) +
                              formatElem("Departamento", departamento));

            String datosFacturacion = formatElem("DatosFacturacion",
                                      formatElem("Nit", nit) +
                                      formatElem("FacturaNro", facturaNro) +
                                      formatElem("AutorizacionNro", autorizacionNro) +
                                      formatElem("LimiteEmision", fechalimite));

            String datosFactura = formatElem("DatosFactura",
                formatElem("Fecha", fecha) +
                formatElem("Cliente", clienteNombre) +
                formatElem("ClienteNit", clienteNit) +
                formatElem("FacturaDetalle", factura.toXmlString()) +
                formatElem( "Total" , montoTotal.ToString( ) ) +
                formatElem("TotalLiteral", literal) +
                formatElem("CodigoControl", codigoControl));

            String facturaXml = formatElem("Factura", datosEmp + datosFacturacion + datosFactura);

            String sql = "insert into Factura (FacturaAutorizacion,FacturaNro,FacturaContenido,FacturaCodigoControl,FacturaAnulada,FechaTransaccion,MontoTotal)" +
                " values ('" + autorizacionNro + "','" + facturaNro + "','" + facturaXml + "','" + codigoControl + "','0','" + fecha + "','" + montoTotal + "')";
            CommonUtils.ConexionBD.Actualizar(sql);
            
        }
    }
}
