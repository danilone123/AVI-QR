using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HFUtils.Impuestos.Facturacion.Factura;
using HFUtils.Core.TextTools;
using System.IO;
namespace CommonUtils
{
    public class FormatImpresion
    {
        StringFormatter strFormat;
        public FormatImpresion()
        {
            strFormat = new StringFormatter();    
        }

        #region function pedido detalle
        public void fillDetalle(ref StreamWriter streamToFill, Int16 maxTextWidth, Factura factura, string numeroPedido)
        {
            streamToFill.WriteLine(strFormat.formatString("Nro Pedido: "+numeroPedido,Align.Left,maxTextWidth));
            streamToFill.WriteLine();
            streamToFill.WriteLine(strFormat.fillString('-', maxTextWidth));
            streamToFill.WriteLine();
            streamToFill.WriteLine(strFormat.formatString("CANT", Align.Left, 4) +
                                   strFormat.formatString("CONCEPTO", Align.Center, 16) +
                                   strFormat.formatString("PRECIO", Align.Right, 8)                                   
                                   );
            streamToFill.WriteLine();
            streamToFill.WriteLine(strFormat.fillString('-', maxTextWidth));

            foreach (ItemFactura item in factura.Items)
            {
                streamToFill.WriteLine();
                streamToFill.WriteLine(strFormat.formatString(item.ItemCantidad.ToString(), Align.Center, 4) +
                                   strFormat.formatString(item.ItemNombre, Align.Left, 16) +
                                   strFormat.formatString(item.ItemPrecioUnitario.ToString(), Align.Right, 8));
            }

            streamToFill.WriteLine( );
            streamToFill.WriteLine( strFormat.formatString( "-" , Align.Center , maxTextWidth ) );
            streamToFill.WriteLine( );
        }
        public void fillDetalleWithDescription( ref StreamWriter streamToFill , Int16 maxTextWidth , List<System.Data.DataRow> listaPedidos , string numeroPedido )
        {
            List<string> items;
            int idx;

            streamToFill.WriteLine( strFormat.formatString( "Nro Pedido: " + numeroPedido , Align.Left , maxTextWidth ) );
            streamToFill.WriteLine( );
            streamToFill.WriteLine( strFormat.fillString( '-' , maxTextWidth ) );
            streamToFill.WriteLine( );
            streamToFill.WriteLine( strFormat.formatString( "CANT" , Align.Left , 4 ) +
                                   strFormat.formatString( "CONCEPTO" , Align.Center , 16 ) 
                                   );
            streamToFill.WriteLine( );
            streamToFill.WriteLine( strFormat.fillString( '-' , maxTextWidth ) );

            foreach ( System.Data.DataRow item in listaPedidos )
            {
                streamToFill.WriteLine( );
                items = TextWrapper( item[ "Articulo" ].ToString( ) , 16 );
                idx = 0;

                // If we wrapped the text and it contains more than one line, we need to procees one by one.
                if ( items.Count > 1 )
                {
                    foreach ( var itemNames in items )
                    {
                        if ( idx == 0 )
                        {
                            streamToFill.WriteLine( strFormat.formatString( item[ "Cantidad" ].ToString( ) , Align.Center , 4 ) +
                                strFormat.formatString( itemNames , Align.Left , 16 ) );
                        }
                        else
                        {
                            streamToFill.WriteLine( );
                            streamToFill.WriteLine( strFormat.formatString( " " , Align.Center , 4 ) + strFormat.formatString( itemNames , Align.Left , 16 ) );
                        }

                        idx++;
                    }
                }
                else
                {
                    streamToFill.WriteLine( strFormat.formatString( item[ "Cantidad" ].ToString( ) , Align.Center , 4 ) +
                   strFormat.formatString( item[ "Articulo" ].ToString( ) , Align.Left , 16 ) );
                }

                streamToFill.WriteLine( );

                if ( !string.IsNullOrEmpty( item[ "Descripcion" ].ToString( ) ) )
                {
                    items = TextWrapper( item[ "Descripcion" ].ToString( ) , 16 );
                    idx = 0;

                    // If we wrapped the text and it contains more than one line, we need to procees one by one.
                    if ( items.Count > 1 )
                    {
                        foreach ( var itemNames in items )
                        {
                            if ( idx == 0 )
                            {
                                streamToFill.WriteLine( strFormat.formatString( " " , Align.Center , 4 ) + strFormat.formatString( itemNames , Align.Left , 16 ) );
                            }
                            else
                            {
                                streamToFill.WriteLine( );
                                streamToFill.WriteLine( strFormat.formatString( " " , Align.Center , 4 ) + strFormat.formatString( itemNames , Align.Left , 16 ) );
                            }

                            idx++;
                        }
                    }
                    else
                    {
                        streamToFill.WriteLine( strFormat.formatString( item[ "Descripcion" ].ToString( ) , Align.Center , 16 ) );
                    }

                    streamToFill.WriteLine( );
                }
            }

            streamToFill.WriteLine( );
            streamToFill.WriteLine( strFormat.formatString( "-" , Align.Center , maxTextWidth ) );
            streamToFill.WriteLine( );
        }
        #endregion

        #region function datos A empresa
        private void fillDatosAEmpresa( ref StreamWriter streamToFill ,
                                       Int16 maxTextWidth ,
                                       String nombreEmpresa ,
                                       String razonSocial ,
                                       String sucursal ,
                                       String direccion ,
                                       String telefono ,
                                       String departamento )
        {
            List<string> items;
            
            items = TextWrapper( nombreEmpresa , 38 );
            this.PrintInLines( streamToFill , maxTextWidth , nombreEmpresa , items );

            items = TextWrapper( razonSocial , 38 );
            this.PrintInLines( streamToFill , maxTextWidth , razonSocial , items );

            streamToFill.WriteLine( strFormat.formatString( sucursal , Align.Center , maxTextWidth ) );

            items = TextWrapper( direccion , 38 );
            this.PrintInLines( streamToFill , maxTextWidth , direccion , items );

            streamToFill.WriteLine( strFormat.formatString( telefono , Align.Center , maxTextWidth ) );
            streamToFill.WriteLine( strFormat.formatString( departamento , Align.Center , maxTextWidth ) );
            streamToFill.WriteLine( );
        }
        #endregion 

        #region function datos A facturacion
        private void fillDatosAFacturacion(ref StreamWriter streamToFill,
                                           Int16 maxTextWidth,
                                           String nit,
                                           String facturaNro,
                                           String autNro)
        {
            streamToFill.WriteLine(strFormat.formatString("NIT: " + nit, Align.Center, maxTextWidth));
            streamToFill.WriteLine(strFormat.formatString("FACTURA Nro: " + facturaNro, Align.Center, maxTextWidth));
            streamToFill.WriteLine(strFormat.formatString("AUTORIZACION Nro: " + autNro, Align.Center, maxTextWidth));
        }
        #endregion 

        #region function datos A Cliente
        //rubro
        private void fillRubroEmpresa(ref StreamWriter streamToFill, String actividadEconomica, Int16 maxTextWidth)
        {
            int idx = 0;
            List<string> items;
            //checking if the name is too long, 20 value can be changed for 18 in case the string does not fit the space
            items = TextWrapper(actividadEconomica, 20);

            if (items.Count > 1)
            {
                foreach (var itemNames in items)
                {
                    if (idx == 0)
                    {
                        streamToFill.WriteLine("Actividad Económica: " + itemNames);
                        streamToFill.WriteLine();
                    }
                    else
                    {
                        //streamToFill.WriteLine();
                        streamToFill.WriteLine(strFormat.formatString(" ", Align.Center, 13) + itemNames); 
                        streamToFill.WriteLine();
                    }

                    idx++;
                }
            }
            else
            {
                streamToFill.WriteLine(strFormat.formatString("Actividad Económica: " + actividadEconomica, Align.Left, maxTextWidth));
                streamToFill.WriteLine();
            }
        }

        private void fillDatosACliente(ref StreamWriter streamToFill,
                                           Int16 maxTextWidth,
                                           String fecha,
                                           String clienteNombre,
                                           String clienteNit)
        {
            List<string> items;
            int idx;

            streamToFill.WriteLine(strFormat.formatString("FECHA: " + fecha, Align.Left, maxTextWidth));
            streamToFill.WriteLine( );
            
            // Check if the name is too long.
            items = TextWrapper( clienteNombre , 30 );
            idx = 0;

            // If we wrapped the literal text and it contains more than one line, we need to procees it one by one.
            if ( items.Count > 1 )
            {
                foreach ( var itemNames in items )
                {
                    if ( idx == 0 )
                        streamToFill.WriteLine( "SR(ES): " + itemNames );
                    else
                    {
                        streamToFill.WriteLine( );
                        streamToFill.WriteLine( strFormat.formatString( " " , Align.Center , 4 ) + itemNames );
                    }

                    idx++;
                }
            }
            else
            {
                streamToFill.WriteLine( strFormat.formatString( "SR(ES): " + clienteNombre , Align.Left , maxTextWidth ) );
            }
            //

            streamToFill.WriteLine( );
            streamToFill.WriteLine(strFormat.formatString("NIT/CI: " + clienteNit, Align.Left, maxTextWidth));
            streamToFill.WriteLine( );
        }
        #endregion 

        #region function fillFacturaItems

        public void fillFacturaItems(ref StreamWriter streamToFill, Int16 maxTextWidth, Factura factura, String literal, double descuento)
        {
            List<string> items;
            int idx;
            streamToFill.WriteLine(strFormat.fillString('-', maxTextWidth));
            streamToFill.WriteLine();
            streamToFill.WriteLine(strFormat.formatString("CANT", Align.Left, 4) +
                                   strFormat.formatString("CONCEPTO", Align.Center, 16) +
                                   strFormat.formatString("PRECIO", Align.Right, 8) +
                                   strFormat.formatString("TOTAL", Align.Right, 8));
            streamToFill.WriteLine();
            streamToFill.WriteLine(strFormat.fillString('-', maxTextWidth));

            foreach (ItemFactura item in factura.Items)
            {
                streamToFill.WriteLine();
                items = TextWrapper( item.ItemNombre , 17 );
                idx = 0;

                // If we wrapped the text and it contains more than one line, we need to procees one by one.
                if ( items.Count > 1 )
                {
                    foreach ( var itemNames in items )
                    {
                        if ( idx == 0 )
                        {
                            streamToFill.WriteLine( strFormat.formatString( item.ItemCantidad.ToString( ) , Align.Center , 4 ) +
                                    strFormat.formatString( itemNames , Align.Left , 17 ) +
                                    strFormat.formatString( item.ItemPrecioUnitario.ToString( ) , Align.Right , 8 ) +
                                    strFormat.formatString( item.ItemImporte.ToString( ) , Align.Right , 8 ) );
                        }
                        else
                        {
                            streamToFill.WriteLine( );
                            streamToFill.WriteLine( strFormat.formatString( " " , Align.Center , 4 ) + strFormat.formatString( itemNames , Align.Left , 17 ) );
                        }

                        idx++;
                    }
                }
                else
                {
                    streamToFill.WriteLine( strFormat.formatString( item.ItemCantidad.ToString( ) , Align.Center , 4 ) +
                                      strFormat.formatString( item.ItemNombre , Align.Left , 17 ) +
                                      strFormat.formatString( item.ItemPrecioUnitario.ToString( ) , Align.Right , 8 ) +
                                      strFormat.formatString( item.ItemImporte.ToString( ) , Align.Right , 8 ) );
                }

            }
            streamToFill.WriteLine();
            streamToFill.WriteLine(strFormat.fillString('-', maxTextWidth));
            streamToFill.WriteLine();
            streamToFill.WriteLine(strFormat.formatString("MONTO PARCIAL BS.:" +
                                                          strFormat.formatString(factura.TotalFactura.ToString(), Align.Right, 9) + " "
                                                          , Align.Right, maxTextWidth));
            streamToFill.WriteLine( );
            streamToFill.WriteLine(strFormat.formatString("DESCUENTO BS.:" +
                                                         strFormat.formatString(descuento.ToString(), Align.Right, 9) + " "
                                                         , Align.Right, maxTextWidth));
            streamToFill.WriteLine( );
            streamToFill.WriteLine(strFormat.formatString("TOTAL Bs.:" +
                                                          strFormat.formatString((factura.TotalFactura-descuento).ToString(), Align.Right, 9) + " "
                                                          , Align.Right, maxTextWidth));
            streamToFill.WriteLine( );

            items = TextWrapper( literal , 35 );
            idx = 0;

            // If we wrapped the literal text and it contains more than one line, we need to procees it one by one.
            if ( items.Count > 1 )
            {
                foreach ( var itemNames in items )
                {
                    if ( idx == 0 )
                        streamToFill.WriteLine( "SON: " + itemNames );
                    else
                    {
                        streamToFill.WriteLine( );
                        streamToFill.WriteLine( strFormat.formatString( " " , Align.Center , 4 ) + itemNames );
                    }

                    idx++;
                }
            }
            else
            {
                streamToFill.WriteLine( "SON: " + literal );
            }

            streamToFill.WriteLine( );
        }

        private void PrintInLines( StreamWriter streamToFill , Int16 maxTextWidth , String longText , List<string> items )
        {
            if ( items.Count > 1 )
            {
                foreach ( var itemNames in items )
                {
                    streamToFill.WriteLine( strFormat.formatString( itemNames , Align.Center , 38 ) );
                }
            }
            else
            {
                streamToFill.WriteLine( strFormat.formatString( longText , Align.Center , maxTextWidth ) );
            }
        }

        private List<string> TextWrapper( string sentence, int limit )
        {
            // The new result, will be turned into string[]
            var newSplit = new List<string>( );
            // Split on normal chars, ie newline, space etc
            var splitted = sentence.Split( );
            // Start out with null
            string word = null;

            for ( int i = 0 ; i < splitted.Length ; i++ )
            {
                // If first word, add
                if ( word == null )
                {
                    word = splitted[ i ];
                }
                // If too long, add
                else if ( splitted[ i ].Length + 1 + word.Length > limit )
                {
                    newSplit.Add( word );
                    word = splitted[ i ];
                }
                // Else, concatenate and go again
                else
                {
                    word += " " + splitted[ i ];
                }
            }
            // Flush what we have left, ie the last word
            newSplit.Add( word );

            // Convert into List<String>
            return newSplit.ToList( );
        }
        #endregion
        #region pie de pagina
        public void fillPiePaginaLeyenda(ref StreamWriter streamToFill, String pieDePagina, Int16 maxTextWidth)
        {
            
            List<string> items;
            //checking if the name is too long, 20 value can be changed for 18 in case the string does not fit the space
            items = TextWrapper(pieDePagina, maxTextWidth);

            if (items.Count > 1)
            {
                foreach (var itemNames in items)
                {
                    streamToFill.WriteLine(/*strFormat.formatString(" ", Align.Left, 2) */ itemNames);
                    streamToFill.WriteLine();
               
                }
            }
            else
            {
                streamToFill.WriteLine(strFormat.formatString(pieDePagina, Align.Left, maxTextWidth));
                streamToFill.WriteLine();
            }
        }
        #endregion 
        public void fillStreamWithData(ref StreamWriter streamToFill,
                                      Int16 maxTextWidth,
                                      String nombreEmpresa,
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
                                        double descuento)
        {
            //streamToFill.WriteLine(strFormat.formatString(strFormat.fillString('X', 90), Align.Left, 90));             

            #region datos A Empresa
            fillDatosAEmpresa(ref streamToFill, maxTextWidth, nombreEmpresa, razonSocial, sucursal, direccion, telefono, departamento);
            #endregion

            #region Texto Estatico
            streamToFill.WriteLine(strFormat.formatString("FACTURA ORIGINAL", Align.Center, maxTextWidth));
            streamToFill.WriteLine(strFormat.formatString(strFormat.fillString('-', 34), Align.Center, maxTextWidth));
            #endregion

            #region datos A Facturacion
            fillDatosAFacturacion(ref streamToFill, maxTextWidth, nit, facturaNro, autorizacionNro);
            streamToFill.WriteLine(strFormat.formatString(strFormat.fillString('-', 34), Align.Center, maxTextWidth));
            #endregion

            #region datos A Cliente
            if (!String.IsNullOrEmpty(rubro))
            {
                fillRubroEmpresa(ref streamToFill, rubro, maxTextWidth);
                streamToFill.WriteLine();
            }
            fillDatosACliente(ref streamToFill, maxTextWidth, fecha, clienteNombre, clienteNit);
            #endregion

            #region factura Items
            fillFacturaItems(ref streamToFill, maxTextWidth, factura, literal,descuento);
            #endregion

            #region codigo Control
            streamToFill.WriteLine( );
            streamToFill.WriteLine("CODIGO DE CONTROL: " + codigoControl);
            #endregion

            #region fecha limite emision
            streamToFill.WriteLine( );
            streamToFill.WriteLine("FECHA LIMITE DE EMISION:" + fechalimite);
            #endregion

            #region pie de pagina
            streamToFill.WriteLine();

            streamToFill.WriteLine(strFormat.formatString("\"ESTA FACTURA CONTRIBUYE AL DESARROLLO", Align.Left, maxTextWidth));
           // streamToFill.WriteLine();
            streamToFill.WriteLine(strFormat.formatString("DEL PAIS.EL USO ILICITO DE ESTA SERA", Align.Left, maxTextWidth));
           // streamToFill.WriteLine();
            streamToFill.WriteLine(strFormat.formatString("SANCIONADO DE ACUERDO A LEY\"", Align.Left, maxTextWidth));
           // streamToFill.WriteLine();
            fillPiePaginaLeyenda(ref streamToFill, "Ley No. 453: Los productos deben suministrarse en condiciones de inocuidad, calidad y seguridad.", maxTextWidth);
            /*
            streamToFill.WriteLine(strFormat.formatString("\"LA ALTERACIÓN, FALSIFICACIÓN O", Align.Left, maxTextWidth));
            streamToFill.WriteLine(strFormat.formatString("COMERCIALIZACIÓN ILEGAL DE ESTE", Align.Left, maxTextWidth));
            streamToFill.WriteLine(strFormat.formatString("DOCUMENTO, TIENE CÁRCEL\"", Align.Left, maxTextWidth));
            */
 /*
            streamToFill.WriteLine(strFormat.formatString("\"La reproduccion total o parcial y/o el", Align.Center, maxTextWidth));
            streamToFill.WriteLine(strFormat.formatString("uso no autorizado de esta Nota Fiscal", Align.Center, maxTextWidth));
            streamToFill.WriteLine(strFormat.formatString("constituye un delito a ser sancionado", Align.Center, maxTextWidth));
            streamToFill.WriteLine(strFormat.formatString("conforme a la Ley\"", Align.Center, maxTextWidth));
  */
            streamToFill.WriteLine(strFormat.fillString('-', maxTextWidth));
            streamToFill.WriteLine( );
            streamToFill.WriteLine(strFormat.formatString("GRACIAS POR SU PREFERENCIA", Align.Center, maxTextWidth));
      
            #endregion
        }

    }
}
