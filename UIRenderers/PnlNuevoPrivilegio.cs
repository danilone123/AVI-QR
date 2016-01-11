using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Xml;
using System.IO;
using System.Security.Cryptography;
using System.Data.SqlClient;

namespace UIRenderers
{
    public partial class PnlNuevoPrivilegio : DevExpress.XtraEditors.XtraUserControl
    {
        XmlTextWriter writer; //= new XmlTextWriter(sw);
        private static log4net.ILog log = log4net.LogManager.GetLogger( "PnlNuevoPrivilegio" );
        public string MessageBarValue { get; set; }
        public string IDUsuario { get; set; }
        private string tempPasswordValue;
        private string tempLoginValue = string.Empty;
        public PnlNuevoPrivilegio( )
        {
            InitializeComponent( );
            barBtnAddUser.Enabled = true;
            barButtonGuardar.Enabled = false;
            treeView1.AfterCheck += new TreeViewEventHandler( treeView1_AfterCheck );
            LoadTreeView( );
            this.CheckAllChildNodes( treeView1.Nodes[ 0 ] , true );
            InitConexionDB( );
        }

        public PnlNuevoPrivilegio( string idUsuario , string nombre , string apellidos , string login , string psswd , string telefono, string privilegios )
        {
            InitializeComponent( );
            barBtnAddUser.Enabled = false;
            barButtonGuardar.Enabled = true;
            treeView1.AfterCheck += new TreeViewEventHandler( treeView1_AfterCheck );
            LoadTreeView( privilegios);
            LoadValues( nombre , apellidos , login , psswd , telefono );
            //SetPrivileges( );
            InitConexionDB( );
            this.IDUsuario = idUsuario;
            ValidateFields();
            
        }

        private void LoadValues( string nombre , string apellidos , string login , string psswd , string telefono )
        {
            txtApellidos.Text = apellidos;
            txtLogin.Text = login;
            txtNombre.Text = nombre;
            tempLoginValue = login;
            txtPassword.Text = psswd;
            tempPasswordValue = psswd;
            txtTelefono.Text = telefono;
        }

        private void SetPrivileges( )
        {
            foreach ( TreeNode mainNode in treeView1.Nodes )
            {
                foreach ( TreeNode node in mainNode.Nodes )
                {
                    if (node.Name == "insumoNode")
                        node.Checked = CommonUtils.Parser.ShowInsumos;
                    else if (node.Name == "recetaNode")
                        node.Checked = CommonUtils.Parser.ShowRecetas;
                    else if (node.Name == "productoNode")
                        node.Checked = CommonUtils.Parser.ShowProductos;
                    else if (node.Name == "comprasInsumosNode")
                        node.Checked = CommonUtils.Parser.ShowCompraInsumos;
                    else if (node.Name == "inventarioInsumosNode")
                        node.Checked = CommonUtils.Parser.ShowInventarioInsumos;
                    else if (node.Name == "cajaNode")
                        node.Checked = CommonUtils.Parser.ShowCaja;
                    else if (node.Name == "egresosNode")
                        node.Checked = CommonUtils.Parser.ShowIngresosEgresos;
                    else if (node.Name == "ventasReservasNode")
                        node.Checked = CommonUtils.Parser.ShowVentasReservar;
                    else if (node.Name == "privilegiosNode")
                        node.Checked = CommonUtils.Parser.ShowPrivilegios;
                    else if (node.Name == "reportesComprasEspeciales")
                        node.Checked = CommonUtils.Parser.ShowReportesComprasEspeciales;
                    else if (node.Name == "reportesComprasInsumos")
                        node.Checked = CommonUtils.Parser.ShowReportesComprasInsumos;
                    else if (node.Name == "reportesVentas")
                        node.Checked = CommonUtils.Parser.ShowReportesVentas;
                    else if (node.Name == "reportesFactura")
                        node.Checked = CommonUtils.Parser.ShowReportesFactura;

                }
            }
        }

        private void InitConexionDB( )
        {
            CommonUtils.ConexionBD.StringDeConexion = CommonUtils.ConexionBD.GetConnectionString( ); //GetConnectionString();
        }

        //will check or uncheck all the child nodes of a parent node
        void treeView1_AfterCheck( object sender , TreeViewEventArgs e )
        {
            if ( e.Action != TreeViewAction.Unknown )
            {
                if ( e.Node.Nodes.Count > 0 )
                {
                    /* Calls the CheckAllChildNodes method, passing in the current 
                    Checked value of the TreeNode whose checked state changed. */
                    this.CheckAllChildNodes( e.Node , e.Node.Checked );
                }
            }
        }

        private TreeViewCancelEventHandler checkForCheckedChildren;

        public void LoadTreeView( )
        {
            checkForCheckedChildren = new TreeViewCancelEventHandler( CheckForCheckedChildrenHandler );

            treeView1.Anchor = AnchorStyles.Top | AnchorStyles.Left |
               AnchorStyles.Bottom | AnchorStyles.Right;
            treeView1.CheckBoxes = true;

            TreeNode mainNode = new TreeNode( );
            this.treeView1.CheckBoxes = true;
            mainNode.Name = "privilegiosMainNode";
            mainNode.Checked = true;
            mainNode.Text = "Privilegios";

            this.treeView1.Nodes.Add( mainNode );

            //node insumo
            TreeNode insumoNode = new TreeNode( );
            insumoNode.Name = "insumoNode";
            insumoNode.Text = "Insumos";
            mainNode.Nodes.Add( insumoNode );

            //node Recetas
            TreeNode recetaNode = new TreeNode( );
            recetaNode.Name = "recetaNode";
            recetaNode.Text = "Recetas";
            mainNode.Nodes.Add( recetaNode );

            //node Producto
            TreeNode productoNode = new TreeNode( );
            productoNode.Name = "productoNode";
            productoNode.Text = "Productos";
            mainNode.Nodes.Add( productoNode );

            //node compras
            TreeNode comprasInsumosNode = new TreeNode( );
            comprasInsumosNode.Text = "Compras Insumos";
            comprasInsumosNode.Name = "comprasInsumosNode";
            mainNode.Nodes.Add( comprasInsumosNode );

            //Inventario
            TreeNode inventarioInsumos = new TreeNode( );
            inventarioInsumos.Text = "Inventario Insumos";
            inventarioInsumos.Name = "inventarioInsumosNode";
            mainNode.Nodes.Add( inventarioInsumos );

            //caja
            TreeNode cajaNode = new TreeNode( );
            cajaNode.Text = "Caja";
            cajaNode.Name = "cajaNode";
            mainNode.Nodes.Add( cajaNode );

            //egresos ingresos
            TreeNode egresosNode = new TreeNode( );
            egresosNode.Text = "Egresos/Ingresos";
            egresosNode.Name = "egresosNode";
            mainNode.Nodes.Add( egresosNode );

            //ventas y reservas ambas estan ligadas
            TreeNode ventasReservasNode = new TreeNode( );
            ventasReservasNode.Text = "Ventas y Reservas";
            ventasReservasNode.Name = "ventasReservasNode";
            mainNode.Nodes.Add( ventasReservasNode );

            //reportes compras especiales
            TreeNode reportesComprasEspeciales = new TreeNode( );
            reportesComprasEspeciales.Text = "Reportes Compras Especiales";
            reportesComprasEspeciales.Name = "reportesComprasEspeciales";
            mainNode.Nodes.Add( reportesComprasEspeciales );

            //reportes compras de insumos
            TreeNode reportesComprasInsumos = new TreeNode( );
            reportesComprasInsumos.Text = "Reportes Compras Insumos";
            reportesComprasInsumos.Name = "reportesComprasInsumos";
            mainNode.Nodes.Add( reportesComprasInsumos );

            //reportes ventas
            TreeNode reportesVentas = new TreeNode( );
            reportesVentas.Text = "Reportes Ventas";
            reportesVentas.Name = "reportesVentas";
            mainNode.Nodes.Add( reportesVentas );

            //reportes facturas
            TreeNode reportesFactura = new TreeNode();
            reportesFactura.Text = "Reportes Factura";
            reportesFactura.Name = "reportesFactura";
            reportesFactura.Checked = false;
            mainNode.Nodes.Add(reportesFactura);

            //privilegios
            TreeNode privilegiosNode = new TreeNode( );
            privilegiosNode.Text = "Privilegios";
            privilegiosNode.Name = "privilegiosNode";
            mainNode.Nodes.Add( privilegiosNode );
        }

        //loads the main and child nodes
        public void LoadTreeView( string privilegios )
        {
            checkForCheckedChildren =new TreeViewCancelEventHandler( CheckForCheckedChildrenHandler );

            treeView1.Anchor = AnchorStyles.Top | AnchorStyles.Left |
               AnchorStyles.Bottom | AnchorStyles.Right;
            treeView1.CheckBoxes = true;

            TreeNode mainNode = new TreeNode( );
            this.treeView1.CheckBoxes = true;
            mainNode.Name = "privilegiosMainNode";
            mainNode.Checked = true;
            mainNode.Text = "Privilegios";

            this.treeView1.Nodes.Add( mainNode );

            //node insumo
            TreeNode insumoNode = new TreeNode( );
            insumoNode.Name = "insumoNode";
            insumoNode.Text = "Insumos";
            insumoNode.Checked = false;
            mainNode.Nodes.Add( insumoNode );

            //node Recetas
            TreeNode recetaNode = new TreeNode( );
            recetaNode.Name = "recetaNode";
            recetaNode.Text = "Recetas";
            recetaNode.Checked = false;
            mainNode.Nodes.Add( recetaNode );

            //node Producto
            TreeNode productoNode = new TreeNode( );
            productoNode.Name = "productoNode";
            productoNode.Text = "Productos";
            productoNode.Checked = false;
            mainNode.Nodes.Add( productoNode );

            //node compras
            TreeNode comprasInsumosNode = new TreeNode( );
            comprasInsumosNode.Text = "Compras Insumos";
            comprasInsumosNode.Name = "comprasInsumosNode";
            comprasInsumosNode.Checked = false;
            mainNode.Nodes.Add( comprasInsumosNode );

            //Inventario
            TreeNode inventarioInsumos = new TreeNode( );
            inventarioInsumos.Text = "Inventario Insumos";
            inventarioInsumos.Name = "inventarioInsumosNode";
            inventarioInsumos.Checked = false;
            mainNode.Nodes.Add( inventarioInsumos );

            //caja
            TreeNode cajaNode = new TreeNode( );
            cajaNode.Text = "Caja";
            cajaNode.Name = "cajaNode";
            cajaNode.Checked = false;
            mainNode.Nodes.Add( cajaNode );

            //egresos ingresos
            TreeNode egresosNode = new TreeNode( );
            egresosNode.Text = "Egresos/Ingresos";
            egresosNode.Name = "egresosNode";
            egresosNode.Checked = false;
            mainNode.Nodes.Add( egresosNode );

            //ventas y reservas ambas estan ligadas
            TreeNode ventasReservasNode = new TreeNode( );
            ventasReservasNode.Text = "Ventas y Reservas";
            ventasReservasNode.Name = "ventasReservasNode";
            ventasReservasNode.Checked = false;
            mainNode.Nodes.Add( ventasReservasNode );

            //reportes compras especiales
            TreeNode reportesComprasEspeciales = new TreeNode( );
            reportesComprasEspeciales.Text = "Reportes Compras Especiales";
            reportesComprasEspeciales.Name = "reportesComprasEspeciales";
            reportesComprasEspeciales.Checked = false;
            mainNode.Nodes.Add( reportesComprasEspeciales );

            //reportes compras de insumos
            TreeNode reportesComprasInsumos = new TreeNode( );
            reportesComprasInsumos.Text = "Reportes Compras Insumos";
            reportesComprasInsumos.Name = "reportesComprasInsumos";
            reportesComprasInsumos.Checked = false;
            mainNode.Nodes.Add( reportesComprasInsumos );

            //reportes ventas
            TreeNode reportesVentas = new TreeNode( );
            reportesVentas.Text = "Reportes Ventas";
            reportesVentas.Name = "reportesVentas";
            reportesVentas.Checked = false;
            mainNode.Nodes.Add( reportesVentas );

            //reportes facturas
            TreeNode reportesFactura = new TreeNode();
            reportesFactura.Text = "Reportes Factura";
            reportesFactura.Name = "reportesFactura";
            reportesFactura.Checked = false;
            mainNode.Nodes.Add(reportesFactura);

            //privilegios
            TreeNode privilegiosNode = new TreeNode( );
            privilegiosNode.Text = "Privilegios";
            privilegiosNode.Name = "privilegiosNode";
            privilegiosNode.Checked = false;
            mainNode.Nodes.Add( privilegiosNode );

            XmlReader reader = XmlReader.Create( new System.IO.StringReader( privilegios ) );
            while ( reader.Read( ) )
            {
                if ( reader.NodeType == XmlNodeType.Element && reader.LocalName == "ListaInsumos" )
                {
                    if ( reader.GetAttribute( "checked" ) == "True" )
                        insumoNode.Checked = true;
                }
                else if ( reader.NodeType == XmlNodeType.Element && reader.LocalName == "ListaRecetas" )
                {
                    if ( reader.GetAttribute( "checked" ) == "True" )
                        recetaNode.Checked = true;
                }
                else if ( reader.NodeType == XmlNodeType.Element && reader.LocalName == "ListaProductos" )
                {
                    if ( reader.GetAttribute( "checked" ) == "True" )
                        productoNode.Checked = true;
                }
                else if ( reader.NodeType == XmlNodeType.Element && reader.LocalName == "ListaComprasInsumos" )
                {
                    if ( reader.GetAttribute( "checked" ) == "True" )
                        comprasInsumosNode.Checked = true;
                }
                else if ( reader.NodeType == XmlNodeType.Element && reader.LocalName == "ListaInventarioInsumos" )
                {
                    if ( reader.GetAttribute( "checked" ) == "True" )
                        inventarioInsumos.Checked = true;
                }
                else if ( reader.NodeType == XmlNodeType.Element && reader.LocalName == "Caja" )
                {
                    if ( reader.GetAttribute( "checked" ) == "True" )
                        cajaNode.Checked = true;
                }
                else if ( reader.NodeType == XmlNodeType.Element && reader.LocalName == "ListaEgresosIngresos" )
                {
                    if ( reader.GetAttribute( "checked" ) == "True" )
                        egresosNode.Checked = true;
                }
                else if ( reader.NodeType == XmlNodeType.Element && reader.LocalName == "VentasyReservas" )
                {
                    if ( reader.GetAttribute( "checked" ) == "True" )
                        ventasReservasNode.Checked = true;
                }
                else if ( reader.NodeType == XmlNodeType.Element && reader.LocalName == "ListaPrivilegios" )
                {
                    if ( reader.GetAttribute( "checked" ) == "True" )
                        privilegiosNode.Checked = true;
                }
                else if ( reader.NodeType == XmlNodeType.Element && reader.LocalName == "ListaReportesVentas" )
                {
                    if ( reader.GetAttribute( "checked" ) == "True" )
                        reportesVentas.Checked = true;
                }
                else if (reader.NodeType == XmlNodeType.Element && reader.LocalName == "ListaReportesComprasInsumos")//ListaReportesComprasInsumos//ListaReportesComprasEspeciales
                {
                    if (reader.GetAttribute("checked") == "True")
                    {
                        //comprasInsumosNode.Checked = true;
                        reportesComprasInsumos.Checked = true;
                    }
                }
                else if ( reader.NodeType == XmlNodeType.Element && reader.LocalName == "ListaReportesComprasEspeciales" )
                {
                    if ( reader.GetAttribute( "checked" ) == "True" )
                        reportesComprasEspeciales.Checked = true;
                }
                else if (reader.NodeType == XmlNodeType.Element && reader.LocalName == "ListaReportesFactura")
                {
                    if (reader.GetAttribute("checked") == "True")
                        reportesFactura.Checked = true;
                }
            }
        }

        //based on the nodes clicked it will build an xml strucuture
        private void buildXml( TreeNode treeNode )
        {
            foreach ( TreeNode node in treeNode.Nodes )
            {
                if (node.Checked)
                {
                    if ( node.Text == "Insumos" )
                    {
                        writer.WriteStartElement( "ListaInsumos" );
                        writer.WriteAttributeString( "checked" , node.Checked.ToString( ) );
                        writer.WriteAttributeString( "name" , "Insumos" );
                        if ( node.Nodes.Count > 0 )
                        {
                            this.buildXml( node );
                        }
                        writer.WriteEndElement( );
                        // writer.WriteElementString("ListaInsumos", "");

                    }
                    else if ( node.Text == "Recetas" )
                    {
                        writer.WriteStartElement( "ListaRecetas" );
                        writer.WriteAttributeString( "checked" , node.Checked.ToString( ) );
                        writer.WriteAttributeString( "name" , "Recetas" );
                        if ( node.Nodes.Count > 0 )
                        {
                            this.buildXml( node );
                        }
                        writer.WriteEndElement( );
                    }
                    else if ( node.Text == "Productos" )
                    {
                        writer.WriteStartElement( "ListaProductos" );
                        writer.WriteAttributeString( "checked" , node.Checked.ToString( ) );
                        writer.WriteAttributeString( "name" , "Productos" );
                        if ( node.Nodes.Count > 0 )
                        {
                            this.buildXml( node );
                        }
                        writer.WriteEndElement( );
                    }
                    else if ( node.Text == "Compras Insumos" )
                    {
                        writer.WriteStartElement( "ListaComprasInsumos" );
                        writer.WriteAttributeString( "checked" , node.Checked.ToString( ) );
                        writer.WriteAttributeString( "name" , "Compras Insumos" );
                        if ( node.Nodes.Count > 0 )
                        {
                            this.buildXml( node );
                        }
                        writer.WriteEndElement( );

                    }
                    else if ( node.Text == "Inventario Insumos" )
                    {
                        writer.WriteStartElement( "ListaInventarioInsumos" );
                        writer.WriteAttributeString( "checked" , node.Checked.ToString( ) );
                        writer.WriteAttributeString( "name" , "Inventario Insumos" );
                        if ( node.Nodes.Count > 0 )
                        {
                            this.buildXml( node );
                        }
                        writer.WriteEndElement( );
                    }
                    else if ( node.Text == "Caja" )
                    {
                        writer.WriteStartElement( "Caja" );
                        writer.WriteAttributeString( "checked" , node.Checked.ToString( ) );
                        writer.WriteAttributeString( "name" , "Caja" );
                        if ( node.Nodes.Count > 0 )
                        {
                            this.buildXml( node );
                        }
                        writer.WriteEndElement( );
                    }
                    else if ( node.Text == "Egresos/Ingresos" )
                    {
                        writer.WriteStartElement( "ListaEgresosIngresos" );
                        writer.WriteAttributeString( "checked" , node.Checked.ToString( ) );
                        writer.WriteAttributeString( "name" , "Egresos/Ingresos" );
                        if ( node.Nodes.Count > 0 )
                        {
                            this.buildXml( node );
                        }
                        writer.WriteEndElement( );
                    }
                    else if ( node.Text == "Ventas y Reservas" )
                    {
                        writer.WriteStartElement( "VentasyReservas" );
                        writer.WriteAttributeString( "checked" , node.Checked.ToString( ) );
                        writer.WriteAttributeString( "name" , "Ventas y Reservas" );
                        if ( node.Nodes.Count > 0 )
                        {
                            this.buildXml( node );
                        }
                        //writer.WriteEndAttribute();
                        writer.WriteEndElement( );
                    }
                    else if ( node.Text == "Reportes Compras Especiales" )
                    {
                        writer.WriteStartElement( "ListaReportesComprasEspeciales" );
                        writer.WriteAttributeString( "checked" , node.Checked.ToString( ) );
                        writer.WriteAttributeString( "name" , "Reportes Compras Especiales" );
                        if ( node.Nodes.Count > 0 )
                        {
                            this.buildXml( node );
                        }
                        //writer.WriteEndAttribute();
                        writer.WriteEndElement( );
                    }
                    else if ( node.Text == "Reportes Compras Insumos" )
                    {
                        writer.WriteStartElement( "ListaReportesComprasInsumos" );
                        writer.WriteAttributeString( "checked" , node.Checked.ToString( ) );
                        writer.WriteAttributeString( "name" , "Reportes Compras Insumos" );
                        if ( node.Nodes.Count > 0 )
                        {
                            this.buildXml( node );
                        }
                        //writer.WriteEndAttribute();
                        writer.WriteEndElement( );
                    }
                    else if ( node.Text == "Reportes Ventas" )
                    {
                        writer.WriteStartElement( "ListaReportesVentas" );
                        writer.WriteAttributeString( "checked" , node.Checked.ToString( ) );
                        writer.WriteAttributeString( "name" , "Reportes Ventas" );
                        if ( node.Nodes.Count > 0 )
                        {
                            this.buildXml( node );
                        }
                        //writer.WriteEndAttribute();
                        writer.WriteEndElement( );
                    }
                    else if ( node.Text == "Privilegios" )
                    {
                        writer.WriteStartElement( "ListaPrivilegios" );
                        writer.WriteAttributeString( "checked" , node.Checked.ToString( ) );
                        writer.WriteAttributeString( "name" , "Privilegios" );
                        if ( node.Nodes.Count > 0 )
                        {
                            this.buildXml( node );
                        }
                        //writer.WriteEndAttribute();
                        writer.WriteEndElement( );
                    }
                    else if (node.Text == "Reportes Factura")
                    {
                        writer.WriteStartElement("ListaReportesFactura");
                        writer.WriteAttributeString( "checked" , node.Checked.ToString( ) );
                        writer.WriteAttributeString( "name" , "Privilegios" );
                        if ( node.Nodes.Count > 0 )
                        {
                            this.buildXml( node );
                        }
                        //writer.WriteEndAttribute();
                        writer.WriteEndElement( ); 
                    }
                    else if (node.Text == "insumo Node 1")
                    {
                        // if (node.Parent.Checked)
                        {
                            writer.WriteStartElement("ListaInsumosNode1");
                            if (node.Checked)
                            {
                                writer.WriteAttributeString("checked", this.checkParentOfNode(node) == 1 ? "True" : "False");
                            }
                            else
                            {
                                writer.WriteAttributeString("checked", node.Checked.ToString());
                            }
                            writer.WriteAttributeString("name", "Insumo Node 1");
                            if (node.Nodes.Count > 0)
                            {
                                this.buildXml(node);
                            }
                            writer.WriteEndElement();
                        }
                    }
                    else if (node.Text == "insumo node 1 a 1")
                    {
                        //if (node.Parent.Checked)
                        {
                            writer.WriteStartElement("ListaInsumosNode1a1");
                            if (node.Checked)
                            {
                                writer.WriteAttributeString("checked", this.checkParentOfNode(node) == 1 ? "True" : "False");
                            }
                            else
                            {
                                writer.WriteAttributeString("checked", node.Checked.ToString());
                            }
                            writer.WriteAttributeString("name", "insumo node 1 a 1");
                            if (node.Nodes.Count > 0)
                            {
                                this.buildXml(node);
                            }
                            writer.WriteEndElement();
                        }
                    }
                    else if (node.Text == "insumo node 1 a 1 a 1")
                    {
                        writer.WriteStartElement("ListaInsumosNode1a1a1");
                        if (node.Checked)
                        {
                            writer.WriteAttributeString("checked", this.checkParentOfNode(node) == 1 ? "True" : "False");
                        }
                        else
                        {
                            writer.WriteAttributeString("checked", node.Checked.ToString());
                        }
                        writer.WriteAttributeString("name", "insumo node 1 a 1 a 1");
                        if (node.Nodes.Count > 0)
                        {
                            this.buildXml(node);
                        }
                        writer.WriteEndElement();

                    }
                    else if (node.Text == "Layla")
                    {
                        writer.WriteStartElement("Layla");
                        if (node.Checked)
                        {
                            writer.WriteAttributeString("checked", this.checkParentOfNode(node) == 1 ? "True" : "False");
                        }
                        else
                        {
                            writer.WriteAttributeString("checked", node.Checked.ToString());
                        }
                        writer.WriteAttributeString("name", "Layla");
                        if (node.Nodes.Count > 0)
                        {
                            this.buildXml(node);
                        }
                        writer.WriteEndElement();

                    }
                    /*  else if (node.Text == "insumo node 2 a 2")
                      {
                          if (node.Parent.Checked)
                          {
                              writer.WriteStartElement("ListaInsumosNode2a2");
                              if (node.Nodes.Count > 0)
                              {
                                  this.buildXml(node);
                              }
                              writer.WriteEndElement();
                          }
                      }
                      else if (node.Text == "insumo node 2")
                      {
                          if (node.Parent.Checked)
                          {
                              writer.WriteStartElement("ListaInsumosNode2");
                              if (node.Nodes.Count > 0)
                              {
                                  this.buildXml(node);
                              }
                              writer.WriteEndElement();
                          }
                      }*/

                }
                //  this.buildXml(node);
            }
        }

        private int checkParentOfNode( TreeNode node )
        {
            if ( node.Parent.Text == "Privilegios" )
            {
                return 1;
            }
            if ( !node.Parent.Checked )
            {
                return 0;
            }
            return 1 * checkParentOfNode( node.Parent );


        }

        private void treeViewTestMethod( )
        {
            // Disable redrawing of treeView1 to prevent flickering 
            // while changes are made.
            treeView1.BeginUpdate( );

            // Collapse all nodes of treeView1.
            treeView1.CollapseAll( );

            // Add the checkForCheckedChildren event handler to the BeforeExpand event.
            treeView1.BeforeExpand += checkForCheckedChildren;


            // Expand all nodes of treeView1. Nodes without checked children are 
            // prevented from expanding by the checkForCheckedChildren event handler.
            treeView1.ExpandAll( );

            // Remove the checkForCheckedChildren event handler from the BeforeExpand 
            // event so manual node expansion will work correctly.
            treeView1.BeforeExpand -= checkForCheckedChildren;

            // Enable redrawing of treeView1.
            treeView1.EndUpdate( );
        }

        // Prevent expansion of a node that does not have any checked child nodes.
        private void CheckForCheckedChildrenHandler( object sender ,
            TreeViewCancelEventArgs e )
        {
            if ( !HasCheckedChildNodes( e.Node ) ) e.Cancel = true;
        }

        // Returns a value indicating whether the specified 
        // TreeNode has checked child nodes.
        private bool HasCheckedChildNodes( TreeNode node )
        {
            if ( node.Nodes.Count == 0 ) return false;
            foreach ( TreeNode childNode in node.Nodes )
            {
                if ( childNode.Checked ) return true;
                // Recursively check the children of the current child node.
                if ( HasCheckedChildNodes( childNode ) ) return true;
            }
            return false;
        }

        private void CheckAllChildNodes( TreeNode treeNode , bool nodeChecked )
        {
            foreach ( TreeNode node in treeNode.Nodes )
            {
                node.Checked = nodeChecked;
                if ( node.Nodes.Count > 0 )
                {
                    // If the current node has child nodes, call the CheckAllChildsNodes method recursively.
                    this.CheckAllChildNodes( node , nodeChecked );
                }
            }
        }

        private void BtnAdd_Click( object sender , EventArgs e )
        {
            StringWriter sw = new StringWriter( );
            writer = new XmlTextWriter( sw );
            writer.WriteStartElement( "Privilegios" );
            this.buildXml( treeView1.Nodes[ 0 ] );
            writer.WriteEndElement( );
            string xmlValue = sw.ToString( );

            if ( !dxErrorProviderPrivilegios.HasErrors )
            {

                if (UsuarioRegistrado() && (tempLoginValue != txtLogin.Text))
                {
                    dxErrorProviderPrivilegios.SetError( txtLogin , "El usuario " + txtLogin.Text + " ya existe." , DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning );
                    return;
                }
                if ( !IsEditing( ) )
                {
                    txtPassword.Text = MD5_ComputeHexaHash( txtPassword.Text );

                    string sqlQuery = "Insert into UsuariosSistema(Login,Password,Telefono,PrivilegiosXML,Privilegios, Nombre, Apellidos) values('"
                        + txtLogin.Text + "','" + txtPassword.Text + "','" + txtTelefono.Text + "','" + xmlValue + "','" + xmlValue
                        + "','" + txtNombre.Text + "','" + txtApellidos.Text + "')";
                    try
                    {
                        CommonUtils.ConexionBD.Actualizar( sqlQuery );
                        alertControlUsers.Show( this.FindForm( ) , "Creación satisfactoria." , "El usuario " + txtNombre.Text + " " + txtApellidos.Text +
                        " fue creado (a) satisfactoriamente." , Image );
                        CloseUI( );
                    }
                    catch ( Exception ex )
                    {
                        log.Error( ex.Message , ex );
                        MessageBarValue = "No se pudo Registrar al usuario. Hubo un error: " + ex.Message;
                    }
                    finally
                    {
                        barItem.Caption = MessageBarValue;
                        this.Process( );
                    }
                }
                else
                {
                    if (!tempPasswordValue.Equals(txtPassword.Text)) {
                        txtPassword.Text = MD5_ComputeHexaHash(txtPassword.Text);
                    }
                  

                    string sqlQuery = "UPDATE UsuariosSistema set Login='" + CommonUtils.StringUtils.EscapeSQL( txtLogin.Text ) + "',Password='" + txtPassword.Text +
                        "',Telefono='" + CommonUtils.StringUtils.EscapeSQL( txtTelefono.Text ) + "',PrivilegiosXML='" + xmlValue + "',Privilegios='" + xmlValue +
                         "',Nombre='" + CommonUtils.StringUtils.EscapeSQL( txtNombre.Text ) + "',Apellidos='" + CommonUtils.StringUtils.EscapeSQL( txtApellidos.Text ) + "' where UsuarioId=" + this.IDUsuario;
                    try
                    {
                        CommonUtils.ConexionBD.Actualizar( sqlQuery );
                        
                        alertControlUsers.Show( this.FindForm( ) , "Actualización satisfactoria." , "El usuario " + txtNombre.Text + " " + txtApellidos.Text +
                        " fue actualizado (a) satisfactoriamente." , Image );
                        CloseUI( );
                    }
                    catch ( Exception ex )
                    {
                        log.Error( ex.Message , ex );
                        MessageBarValue = "No se pudo Registrar al usuario. Hubo un error: " + ex.Message;
                    }
                    finally
                    {
                        barItem.Caption = MessageBarValue;
                        this.Process( );
                    }
                }
            }
            else
            {
                GetErrorProviderMessages( );
            }
        }

        // Define a delegate named LogHandler, which will encapsulate
        // any method that takes a string as the parameter and returns no value
        public delegate void PrivilegesHandler( PnlNuevoPrivilegio Privilegio );

        // Define an Event based on the above Delegate
        public event PrivilegesHandler Privilege;

        // Instead of having the Process() function take a delegate
        // as a parameter, we've declared a Log event. Call the Event,
        // using the OnXXXX Method, where XXXX is the name of the Event.
        public void Process( )
        {
            OnChange( this );
        }

        private void OnChange( PnlNuevoPrivilegio Privilegio )
        {
            if ( Privilege != null )
            {
                Privilege( Privilegio );
            }
        }

        private Image Image
        {
            get
            {
                return imageCollectionUsers.Images[ 0 ];
            }
        }

        private void CloseUI( )
        {
            ( this.ParentForm as mainForm ).ContextControls.Remove( ( this.ParentForm as mainForm ).xtraTabControl.SelectedTabPage.Controls[ 0 ].Name );
            ( this.ParentForm as mainForm ).xtraTabControl.TabPages.RemoveAt( ( this.ParentForm as mainForm ).xtraTabControl.SelectedTabPageIndex );
        }

        private string MD5_ComputeHexaHash( string text )
        {
            // Gets the MD5 hash for text
            MD5 md5 = new MD5CryptoServiceProvider( );
            byte[ ] data = Encoding.Default.GetBytes( text );
            byte[ ] hash = md5.ComputeHash( data );
            // Transforms as hexa
            string hexaHash = "";
            foreach ( byte b in hash )
            {
                hexaHash += String.Format( "{0:x2}" , b );
            }
            // Returns MD5 hexa hash
            return hexaHash;
        }

        //este metodo esta incompleto!!!
        private bool IsEditing( )
        {
            bool Resultado = false;
            string Sql = "select * from UsuariosSistema where UsuarioId='" + IDUsuario + "'";
            /*
             * cuando trabajamos con OleDbDataReader, es necesario siempre abrir
             * y cerrar la conexion
             */
            CommonUtils.ConexionBD.AbrirConexion( );
            SqlDataReader reader = CommonUtils.ConexionBD.EjecutarConsultaReader( Sql );

            if ( reader.HasRows )
                Resultado = true;

            CommonUtils.ConexionBD.CerrarConexion( );

            return Resultado;
        }

        private bool UsuarioRegistrado( )
        {
            bool Resultado = false;
            string Sql = "select * from UsuariosSistema where Login='" + txtLogin.Text + "'";
            /*
             * cuando trabajamos con OleDbDataReader, es necesario siempre abrir
             * y cerrar la conexion
             */
            CommonUtils.ConexionBD.AbrirConexion( );
            SqlDataReader reader = CommonUtils.ConexionBD.EjecutarConsultaReader( Sql );

            if ( reader.HasRows )
                Resultado = true;

            CommonUtils.ConexionBD.CerrarConexion( );

            return Resultado;
        }

        #region validate region
        private void ValidateFields()
        {
            ValidateEmptyStringRule(txtPassword);
            ValidateEmptyStringRule(txtLogin);
        }
        private void ValidateEmptyStringRule( BaseEdit control )
        {
            if ( control.EditValue == null || control.EditValue.ToString( ).Trim( ).Length == 0 )
                dxErrorProviderPrivilegios.SetError( control , "Este campo no puede ser vacio" , DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical );
            else
                dxErrorProviderPrivilegios.SetError( control , "" );
        }

        private void GetErrorProviderMessages( )
        {
            IList<Control> controlErrors = dxErrorProviderPrivilegios.GetControlsWithError( );
            //    controlErrors.OrderBy<>;
            alertControlUsers.Show( this.FindForm( ) , "Falló al guardar." , "Los cambios NO se guardaron.  Por favor, intente nuevamente." , Image );
            MessageBox.Show( this , ( string ) controlErrors[ 0 ].Tag , dxErrorProviderPrivilegios.GetError( controlErrors[ 0 ] ) , MessageBoxButtons.OK , MessageBoxIcon.Error );
        }

        private void txtNombre_Validating( object sender , CancelEventArgs e )
        {
            ValidateEmptyStringRule( sender as BaseEdit );

            if (UsuarioRegistrado() && (tempLoginValue != txtLogin.Text))
            {
                dxErrorProviderPrivilegios.SetError( txtLogin , "El usuario " + txtLogin.Text + " ya existe.",DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning );
                e.Cancel = true;
            }
        }

        private void txtPassword_Validating( object sender , CancelEventArgs e )
        {
            ValidateEmptyStringRule( sender as BaseEdit );
        }

         #endregion

        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad( e );
            txtLogin.Focus( );
        }
        private void barButtonEdit_ItemClick( object sender , DevExpress.XtraBars.ItemClickEventArgs e )
        {
            ValidateFields();
            BtnAdd_Click( null , EventArgs.Empty );
        }

        private void barButtonClose_ItemClick( object sender , DevExpress.XtraBars.ItemClickEventArgs e )
        {
            if ( ( this.ParentForm as mainForm ).ContextControls.ContainsValue( ( this.ParentForm as mainForm ).xtraTabControl.SelectedTabPage.Controls[ 0 ] ) )
                ( this.ParentForm as mainForm ).ContextControls.Remove( ( this.ParentForm as mainForm ).xtraTabControl.SelectedTabPage.Controls[ 0 ].Name );
            
            ( this.ParentForm as mainForm ).xtraTabControl.TabPages.RemoveAt( ( this.ParentForm as mainForm ).xtraTabControl.SelectedTabPageIndex );

        }

        private void barBtnAddUser_ItemClick( object sender , DevExpress.XtraBars.ItemClickEventArgs e )
        {
            ValidateFields();
            BtnAdd_Click( null , EventArgs.Empty );
        }
    }
}          