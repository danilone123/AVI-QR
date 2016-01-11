using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace CommonUtils
{
    public class Parser
    {
        XmlReader reader;

        public static bool ShowInsumos { get; set; }
        public static bool ShowRecetas { get; set; }
        public static bool ShowProductos { get; set; }
        public static bool ShowCompraInsumos { get; set; }
        public static bool ShowInventarioInsumos { get; set; }
        public static bool ShowCaja { get; set; }
        public static bool ShowIngresosEgresos { get; set; }
        public static bool ShowVentasReservar { get; set; }
        public static bool ShowReportesComprasEspeciales { get; set; }
        public static bool ShowReportesComprasInsumos { get; set; }
        public static bool ShowReportesVentas { get; set; }
        public static bool ShowReportesFactura { get; set; }
        public static bool ShowPrivilegios { get; set; }

        public void InitValues( string strBase )
        {
            InitProperties( );
            reader = XmlReader.Create( new System.IO.StringReader( strBase ) );

            while ( reader.Read( ) )
            {
                if ( reader.NodeType == XmlNodeType.Element && reader.LocalName == "ListaInsumos" )
                {
                    if ( reader.GetAttribute( "checked" ) == "True" )
                        ShowInsumos = true;
                }
                else if ( reader.NodeType == XmlNodeType.Element && reader.LocalName == "ListaRecetas" )
                {
                    if ( reader.GetAttribute( "checked" ) == "True" )
                        ShowRecetas = true;
                }
                else if ( reader.NodeType == XmlNodeType.Element && reader.LocalName == "ListaProductos" )
                {
                    if ( reader.GetAttribute( "checked" ) == "True" )
                        ShowProductos = true;
                }
                else if ( reader.NodeType == XmlNodeType.Element && reader.LocalName == "ListaComprasInsumos" )
                {
                    if ( reader.GetAttribute( "checked" ) == "True" )
                        ShowCompraInsumos = true;
                }
                else if ( reader.NodeType == XmlNodeType.Element && reader.LocalName == "ListaInventarioInsumos" )
                {
                    if ( reader.GetAttribute( "checked" ) == "True" )
                        ShowInventarioInsumos = true;
                }
                else if ( reader.NodeType == XmlNodeType.Element && reader.LocalName == "Caja" )
                {
                    if ( reader.GetAttribute( "checked" ) == "True" )
                        ShowCaja = true;
                }
                else if ( reader.NodeType == XmlNodeType.Element && reader.LocalName == "ListaEgresosIngresos" )
                {
                    if ( reader.GetAttribute( "checked" ) == "True" )
                        ShowIngresosEgresos = true;
                }
                else if ( reader.NodeType == XmlNodeType.Element && reader.LocalName == "VentasyReservas" )
                {
                    if ( reader.GetAttribute( "checked" ) == "True" )
                        ShowVentasReservar = true;
                }
                else if (reader.NodeType == XmlNodeType.Element && reader.LocalName == "ListaPrivilegios")
                {
                    if (reader.GetAttribute("checked") == "True")
                        ShowPrivilegios = true;
                }//ListaReportesVentas
                else if (reader.NodeType == XmlNodeType.Element && reader.LocalName == "ListaReportesVentas")
                {
                    if (reader.GetAttribute("checked") == "True")
                        ShowReportesVentas = true; 
                }
                else if (reader.NodeType == XmlNodeType.Element && reader.LocalName == "ListaReportesComprasInsumos")
                {
                    if (reader.GetAttribute("checked") == "True")
                        ShowReportesComprasInsumos = true;

                }//
                else if (reader.NodeType == XmlNodeType.Element && reader.LocalName == "ListaReportesComprasEspeciales")
                {
                    if (reader.GetAttribute("checked") == "True")
                        ShowReportesComprasEspeciales = true;

                }//
                else if (reader.NodeType == XmlNodeType.Element && reader.LocalName == "ListaReportesFactura")
                {
                    if (reader.GetAttribute("checked") == "True")
                        ShowReportesFactura = true;

                }
            }
        }

        private void InitProperties( )
        {
            ShowInsumos = false;
            ShowRecetas = false;
            ShowProductos = false;
            ShowCompraInsumos = false;
            ShowInventarioInsumos = false;
            ShowCaja = false;
            ShowIngresosEgresos = false;
            ShowVentasReservar = false;
            ShowReportesComprasEspeciales = false;
            ShowReportesComprasInsumos = false;
            ShowReportesVentas = false;
            ShowPrivilegios = false;
            ShowReportesFactura = false;
        }
    }
}
