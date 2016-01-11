using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraTab;
using System.Data.SqlClient;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using HFUtils.Impuestos.Facturacion.Cliente;
namespace UIRenderers
{
    public partial class PnlVentas : UserControl,CommonUtils.ActualizarGrids
    {
        private Clientes clientes;
        private GridProductos gridProducto;
        private string sql = string.Empty;
        private DataTable dataTable = new DataTable( );
        private const int CANTIDAD_MINIMA = 1;
        private string pedidoId;
        private bool llamadoDePnlReservas;
        private CommonUtils.Cliente cliente;
        private DevExpress.XtraEditors.CheckEdit chbtn;
        private PnlListaReservas reservasPanel;
        private static log4net.ILog log = log4net.LogManager.GetLogger("PnlVentas");
        public string MessageBarValue { get; set; }

        public PnlListaReservas ReservasPanel
        {
            get { return reservasPanel; }
            set { reservasPanel = value; }
        }

        private Image Image
        {
            get
            {
                return imageCollectionVentas.Images[0];
            }
        }
        public DevExpress.XtraEditors.CheckEdit Chbtn
        {
            get { return chbtn; }
            set { chbtn = value; }
        }

        public CommonUtils.Cliente Cliente
        {
            get { return cliente; }
            set { cliente = value; }
        }

        public bool LlamadoDePnlReservas
        {
            get { return llamadoDePnlReservas; }
            set { llamadoDePnlReservas = value; }
        }

        public DataTable DataTable
        {
            get { return dataTable; }
            set { dataTable = value; }
        }
        
        public string PedidoId
        {
            get { return pedidoId; }
            set { pedidoId = value; }
        }
   
        public PnlVentas( )
        {
            InitializeComponent( );
            updateClientesFromDB();
            InitUI( );
            InitializeDataTable();
            LoadDefaultView();
        }
        
        private void InitUI( )
        {
            dateEdit.DateTime = DateTime.Now;
            CommonUtils.ConexionBD.StringDeConexion = CommonUtils.ConexionBD.GetConnectionString( );
            txtNumPedido.Text = this.calcularNumeroPedido().ToString();
           // InitializeMRUFields( );
        }

        private void updateClientesFromDB()
        {
             clientes = new Clientes();
             #region autocomplete settings
             txtNIT.AutoCompleteCustomSource.Clear();
             txtCliente.AutoCompleteCustomSource.Clear();
             #endregion
             string sqlQueryCliente = "SELECT Nombre,NIT from Cliente";
             string sqlQueryNIT = "SELECT NIT from Cliente";

             CommonUtils.ConexionBD.AbrirConexion();

             SqlCommand cmdCliente = new SqlCommand(sqlQueryCliente, CommonUtils.ConexionBD.Conexion);
             SqlCommand cmdNIT = new SqlCommand(sqlQueryNIT, CommonUtils.ConexionBD.Conexion);

             DataSet dataSet = new DataSet();
             dataSet.Load(cmdCliente.ExecuteReader(), LoadOption.OverwriteChanges, "Clientes");
             dataSet.Load(cmdNIT.ExecuteReader(), LoadOption.OverwriteChanges, "NIT");

             List<DataRow> listaClientes = dataSet.Tables["Clientes"].AsEnumerable().ToList();
             //List<DataRow> listaNIT = dataSet.Tables["NIT"].AsEnumerable().ToList();
             foreach (DataRow clienteTemp in listaClientes)
             {
                 Cliente newClient = new Cliente((String)clienteTemp[1], (String)clienteTemp[0]);
                 clientes.addCliente(newClient);
                 txtNIT.AutoCompleteCustomSource.Add(newClient.NIT);
                 txtCliente.AutoCompleteCustomSource.Add(newClient.Nombre);
             }    

             CommonUtils.ConexionBD.CerrarConexion();
        }

        public void InitializeDataTable()
        {
            this.dataTable.Columns.Add("Articulo", typeof(string));
            this.dataTable.Columns.Add("Descripcion", typeof(string));
            this.dataTable.Columns.Add("Cantidad", typeof(double));
            this.dataTable.Columns.Add("Precio", typeof(double));
            this.dataTable.Columns.Add("Subtotal", typeof(double));
            this.dataTable.Columns.Add("Id", typeof(int));

            this.gridItems.DataSource = this.dataTable;

            this.columnCantidad.View.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(View_CellValueChanging);

        }

        private void InitializeMRUFields( )
        {
            string sqlQueryCliente = "SELECT Nombre from Cliente";
            string sqlQueryNIT = "SELECT NIT from Cliente";

            CommonUtils.ConexionBD.AbrirConexion( );

            SqlCommand cmdCliente = new SqlCommand( sqlQueryCliente , CommonUtils.ConexionBD.Conexion );
            SqlCommand cmdNIT = new SqlCommand( sqlQueryNIT , CommonUtils.ConexionBD.Conexion );

            DataSet dataSet = new DataSet( );
            dataSet.Load( cmdCliente.ExecuteReader( ) , LoadOption.OverwriteChanges , "Clientes" );
            dataSet.Load( cmdNIT.ExecuteReader( ) , LoadOption.OverwriteChanges , "NIT" );

            List<DataRow> listaClientes = dataSet.Tables[ "Clientes" ].AsEnumerable( ).ToList( );
            List<DataRow> listaNIT = dataSet.Tables[ "NIT" ].AsEnumerable( ).ToList( );

            // TODO: Esto se puede mejorar
            foreach ( DataRow item in listaClientes )
            {
                var items = item.ItemArray;
                mruCliente.Properties.Items.AddRange( items );
                break;
            }

            foreach ( DataRow item in listaNIT )
            {
                var items = item.ItemArray;
                mruNIT.Properties.Items.AddRange( items );
                break;
            }

            CommonUtils.ConexionBD.CerrarConexion( );
        }

        protected override void OnVisibleChanged( EventArgs e )
        {
            if ( Visible )
            {
               
            }
            base.OnVisibleChanged( e );
        }

        public void disabledButtons()
        {
            dateEdit.Enabled = false;
            txtTipoPedido.Enabled = false;
            txtEstadoPedido.Enabled = false;
            dateEditFechaEntrega.Enabled = false;
        }

        public void StartButtonsReserva(string[] listaDatos)
        {
            dateEditFechaEntrega.Enabled = false;
            mruCliente.Enabled = false;
            mruNIT.Enabled = false;
            txtNIT.Enabled = false;
            txtCliente.Enabled = false;
            txtDireccion.Enabled = false;
            txtTelefono.Enabled = false;
            txtEstadoPedido.Enabled = false;
            txtTipoPedido.Enabled = false;
            txtTelefono.Visible = true;
            txtDireccion.Visible = true;            
            txtEstadoPedido.Visible = true;
            lblEstadoPedido.Visible = true;
            lblTelefono.Visible = true;
            lblDireccion.Visible = true;
            dateEditFechaEntrega.Visible = true;
            labelFechaEntrega.Visible = true;
            dateEditFechaEntrega.Text = listaDatos[2];
            txtTelefono.Text = listaDatos[0];
            txtDireccion.Text = listaDatos[1];
            dateEdit.Text = listaDatos[3];
            ValidateEmptyStringRule(txtTelefono);
            ValidateEmptyStringRule(txtDireccion);
            ValidateEmptyStringRule(dateEditFechaEntrega);
            txtNumPedido.Text = this.cliente.NumeroPedido.ToString();
            txtNIT.Text = this.cliente.NIT;
            txtCliente.Text = this.cliente.Nombre;

        }
        
        private void ResetFields()
        {
            dataTable.Clear();
            txtDireccion.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            mruCliente.Text = string.Empty;
            mruNIT.Text = string.Empty;
            txtCliente.Text = string.Empty;
            txtNIT.Text = string.Empty;
            dateEditFechaEntrega.Text = string.Empty;
            txtDireccion.Visible = false;
            txtTelefono.Visible = false;
            dateEditFechaEntrega.Visible = false;
            labelFechaEntrega.Visible = false;
            lblDireccion.Visible = false;
            lblTelefono.Visible = false;
            txtEstadoPedido.Visible = false;
            lblEstadoPedido.Visible = false;
            txtTipoPedido.SelectedIndex = 0;
            txtEstadoPedido.SelectedIndex = 0;
            btnVenta.Text = "Vender";
            ValidateEmptyStringRule(txtTelefono);
            ValidateEmptyStringRule(dateEditFechaEntrega);
            ValidateEmptyStringRule(txtDireccion);
            txtNumPedido.Text = this.calcularNumeroPedido().ToString();
            
        }
                
        private void LoadDefaultView( )
        {//masas
            sql = "SELECT Producto.ProductoId, Producto.Imagen, Producto.Nombre, Producto.Descripcion, Producto.Tipo, " +
                "Categoria.Tipo AS [Categoria], Producto.Tamano AS Tamaño, Producto.PrecioVenta AS [Precio] From Producto, Categoria " +
                "WHERE Producto.CategoriaId=Categoria.CategoriaId AND Producto.CategoriaId=3 AND Producto.Mostrar ='True'";
            gridProducto = new GridProductos( sql );
            gridProducto.Sales += new GridProductos.SalesHandler( gridProducto_Sales );
            gridProducto.Dock = DockStyle.Fill;
            xtraTabCategorias.SelectedTabPage.Controls.Add( gridProducto );
        }
      
        private void xtraTabCategorias_SelectedPageChanged( object sender , DevExpress.XtraTab.TabPageChangedEventArgs e )
        {
            switch ( e.Page.TabControl.SelectedTabPageIndex )
            {
                case 1://sandwich
                    sql = "SELECT Producto.ProductoId, Producto.Imagen, Producto.Nombre, Producto.Descripcion, Producto.Tipo, " +
                "Categoria.Tipo AS [Categoria], Producto.Tamano AS Tamaño, Producto.PrecioVenta AS [Precio] From Producto, Categoria " +
                "WHERE Producto.CategoriaId=Categoria.CategoriaId AND Producto.CategoriaId=4 AND Producto.Mostrar = 'True'";
                    gridProducto = new GridProductos( sql );
                    gridProducto.Sales += new GridProductos.SalesHandler( gridProducto_Sales );
                    gridProducto.Dock = DockStyle.Fill;
                    e.Page.Controls.Add( gridProducto );
                    break;
                case 2:
                    sql = "SELECT Producto.ProductoId, Producto.Imagen, Producto.Nombre, Producto.Descripcion, Producto.Tipo, " +
                "Categoria.Tipo AS [Categoria], Producto.Tamano AS Tamaño, Producto.PrecioVenta AS [Precio] From Producto, Categoria " +
                "WHERE Producto.CategoriaId=Categoria.CategoriaId AND Producto.Tipo='Frio' AND Producto.Mostrar = 'True'";
                    gridProducto = new GridProductos( sql );
                    gridProducto.Sales += new GridProductos.SalesHandler( gridProducto_Sales );
                    gridProducto.Dock = DockStyle.Fill;
                    e.Page.Controls.Add( gridProducto );
                    break;
                case 3:
                    sql = "SELECT Producto.ProductoId, Producto.Imagen, Producto.Nombre, Producto.Descripcion, Producto.Tipo, " +
                "Categoria.Tipo AS [Categoria], Producto.Tamano AS Tamaño, Producto.PrecioVenta AS [Precio] From Producto, Categoria " +
                "WHERE Producto.CategoriaId=Categoria.CategoriaId AND Producto.Tipo='Caliente' AND Producto.Mostrar = 'True'";
                    gridProducto = new GridProductos( sql );
                    gridProducto.Sales += new GridProductos.SalesHandler( gridProducto_Sales );
                    gridProducto.Dock = DockStyle.Fill;
                    e.Page.Controls.Add( gridProducto );
                    break;
                case 4:
                    sql = "SELECT Producto.ProductoId, Producto.Imagen, Producto.Nombre, Producto.Descripcion, Producto.Tipo, " +
                "Categoria.Tipo AS [Categoria], Producto.Tamano AS Tamaño, Producto.PrecioVenta AS [Precio] From Producto, Categoria " +
                "WHERE Producto.CategoriaId=Categoria.CategoriaId AND Producto.CategoriaId=5 AND Producto.Mostrar = 'True'";
                    gridProducto = new GridProductos( sql );
                    gridProducto.Sales += new GridProductos.SalesHandler( gridProducto_Sales );
                    gridProducto.Dock = DockStyle.Fill;
                    e.Page.Controls.Add( gridProducto );
                    break;
                case 5:
                    sql = "SELECT Producto.ProductoId, Producto.Imagen, Producto.Nombre, Producto.Descripcion, Producto.Tipo, " +
                "Categoria.Tipo AS [Categoria], Producto.Tamano AS Tamaño, Producto.PrecioVenta AS [Precio] From Producto, Categoria " +
                "WHERE Producto.CategoriaId=Categoria.CategoriaId AND Producto.CategoriaId=6 AND Producto.Mostrar= 'True'";
                    gridProducto = new GridProductos( sql );
                    gridProducto.Sales += new GridProductos.SalesHandler( gridProducto_Sales );
                    gridProducto.Dock = DockStyle.Fill;
                    e.Page.Controls.Add( gridProducto );
                    break;
                default:
                    this.LoadDefaultView( );
                    break;
            }
        }

        private void gridProducto_Sales(GridProductos gridProducto)
        {
            // TODO: rowNumber is declared but is never used. Use this local or remove it please.
            string rowNumber = Convert.ToString( this.dataTable.Rows.Count + 1 ); 
            this.dataTable.Rows.Add( new object[ ] { gridProducto.ventas.Articulo , "",CANTIDAD_MINIMA , gridProducto.ventas.Precio , ( CANTIDAD_MINIMA * gridProducto.ventas.Precio ),gridProducto.ventas.ProductoID } );
        }

        private void View_CellValueChanging( object sender , DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e )
        {
            double value = 0.0;
            if ( e.Column.Caption != "Descripcion" )
            {
                if ( !( double.TryParse( Convert.ToString( e.Value ) , out value ) ) || string.IsNullOrEmpty( ( string ) e.Value ) || Convert.ToDecimal( value ) < 1 )
                {
                    this.gridViewItems.SetRowCellValue( e.RowHandle , this.gridViewItems.Columns[ 1 ].FieldName , CANTIDAD_MINIMA );
                    return;
                }

                double subTotal = Convert.ToDouble( e.Value ) * Convert.ToDouble( gridViewItems.GetDataRow( e.RowHandle )[ "Precio" ] );
                this.gridViewItems.SetRowCellValue( e.RowHandle , this.gridViewItems.Columns[ 3 ].FieldName , subTotal );

                gridViewItems.UpdateTotalSummary( );
            }
        }
       /* private void View_CellValueChanging( object sender , DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e )
        {
           DataRow row=gridViewItems.GetFocusedDataRow();
            if (e.Column.Caption != "Descripcion")
            {
                if (Convert.ToDecimal(e.Value) < 1)
                {
                    this.gridViewItems.SetRowCellValue(e.RowHandle, this.gridViewItems.Columns[1].FieldName, CANTIDAD_MINIMA);
                    return;
                }

                double subTotal = Convert.ToDouble(e.Value) * Convert.ToDouble(row[3].ToString());// gridProducto.ventas.Precio;
                this.gridViewItems.SetRowCellValue(e.RowHandle, this.gridViewItems.Columns[3].FieldName, subTotal);

                gridViewItems.UpdateTotalSummary();
            }
        }
        */
        private void btnVenta_Click( object sender , EventArgs e )
        {
            this.calcularNumeroPedido();
        //    string sql = "INSERT INTO Cliente (Nombre,NIT,Telefono,Direccion)  VALUES ('" +
      //                 mruCliente.Text + "','" + mruNIT.Text + "','" +CommonUtils.StringUtils.EscapeSQL( txtTelefono.Text) + "','" +CommonUtils.StringUtils.EscapeSQL( txtDireccion.Text) + "')";
       //     CommonUtils.ConexionBD.Actualizar( sql );
           
            if (dataTable.Rows.Count == 0)
            {
                //no hay nada para vender
                return;
            }
            else if (llamadoDePnlReservas)
            {
                CalculadoraVentas calculadora = new CalculadoraVentas(reservasPanel, dataTable, this.ParentForm, float.Parse(columnTotal.SummaryItem.SummaryValue.ToString()), true, false, cliente);
                calculadora.PedidoId = pedidoId;
                calculadora.Ventas = reservasPanel.FormReservasPagar;
                calculadora.checkBtn = this.chbtn;
                calculadora.ShowDialog();
                calculadora.Refresh();
            }
            else if (txtTipoPedido.SelectedItem.ToString() == "Inmediato")
            {
                CommonUtils.Cliente cliente = new CommonUtils.Cliente(0, /*mruCliente.Text*/txtCliente.Text, txtTelefono.Text, txtNIT.Text, txtDireccion.Text,int.Parse(txtNumPedido.Text));
                
                CalculadoraVentas calculadora = new CalculadoraVentas(this, dataTable, this.ParentForm, double.Parse(columnTotal.SummaryItem.SummaryValue.ToString()) , false, true, cliente);
                calculadora.ShowDialog();
                calculadora.Refresh();

            }
            else if (txtEstadoPedido.SelectedItem.ToString() == "Pagado" && txtTipoPedido.SelectedItem.ToString() == "Reserva")//mostramos calculadora
            {
                if (!dxErrorProvider.HasErrors)
                {
                    CommonUtils.Cliente cliente = new CommonUtils.Cliente(0, txtCliente.Text, txtTelefono.Text, txtNIT.Text, txtDireccion.Text,int.Parse(txtNumPedido.Text));
                    CalculadoraVentas calculadora = new CalculadoraVentas(this, dataTable, this.ParentForm, float.Parse(columnTotal.SummaryItem.SummaryValue.ToString()), true, true, cliente);
                    calculadora.FechaEntrega = dateEditFechaEntrega.DateTime.ToString("yyyy-MM-dd H:mm:ss.fff");
                    calculadora.FechaEntregaNoFormat = dateEditFechaEntrega.DateTime.ToString( );
                    calculadora.ShowDialog();
                    calculadora.Refresh();
                }
                else
                {
                    this.GetErrorProviderMessages();
                }
            }
            else //hacemos reserva directamente.
            {
                if (!dxErrorProvider.HasErrors)
                {
                    try
                    {
                        CommonUtils.Cliente client = new CommonUtils.Cliente(0, txtCliente.Text, txtTelefono.Text, txtNIT.Text, txtDireccion.Text, int.Parse(txtNumPedido.Text));
                        this.registrarReserva(client);
                        this.ResetFields();
                        MessageBarValue="La reserva fue creada satisfactoriamente.";
                        
                       // MessageBox.Show(this, "Reserva Realizada correctamente", "Reserva ", MessageBoxButtons.OK);
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex.Message, ex);
                         MessageBarValue = "No se pudo registrar reserva sin pagar. Hubo un error: " + ex.Message;
                    }
                    finally
                    {
                        alertControlPnlVentas.Show(this.FindForm(), "Reserva.", MessageBarValue, Image);
                    }                    
                }
                else
                {
                    this.GetErrorProviderMessages();
                }
            }
            
        }
       
        private void registrarReserva(CommonUtils.Cliente cliente1)
        {                  
            string entregado = "False";
            string tipoPedido = "Reserva";
            string pagado = "False";
            string fechaEntrega = dateEditFechaEntrega.DateTime.ToString();
            double total = double.Parse(columnTotal.SummaryItem.SummaryValue.ToString());
            this.manageClient(cliente1);
            string sqlQuery = "Insert into Pedido(FechaPedidoText,FechaEntregaText,Descuento,TipoPedido,Pagado,FechaEntrega,Entregado,ClienteId,TotalVenta,FechaPedido,NombreCliente,Direccion,Telefono,NIT,NumeroPedido) values('" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss.fff") + "','" + dateEditFechaEntrega.DateTime.ToString("yyyy-MM-dd H:mm:ss.fff") + "','" + 0 + "','" + tipoPedido + "','" + pagado + "','" + fechaEntrega + "','" + entregado + "','" + cliente1.IdCliente + "','" + total + "','" + DateTime.Now + "','" + cliente1.Nombre + "','" + cliente1.Direccion + "','" + cliente1.Telefono + "','" + cliente1.NIT + "','" + cliente1.NumeroPedido + "')";
                decimal idPedido = CommonUtils.ConexionBD.ActualizarRetornandoId(sqlQuery);
                List<DataRow> listaPedidos = dataTable.AsEnumerable().ToList();
                foreach (DataRow item in listaPedidos)
                {
                    string costoReceta = CalculadoraVentas.obtenerCostoReceta(item);//this.obtenerCostoReceta(item);
                    sqlQuery = "insert into Pedido_Producto(PedidoId,ProductoId,Cantidad,Total,CostoReceta,PrecioProducto,Descripcion) values('" + idPedido + "','" + item["Id"] + "','" + item["Cantidad"] + "','" + item["Subtotal"] + "','" + costoReceta + "','" + item["Precio"] + "','" + item["Descripcion"] + "')";
                    CommonUtils.ConexionBD.Actualizar(sqlQuery);
                }
        }

        private void manageClient(CommonUtils.Cliente newCliente)
        {
            //string sql = "SELECT ClienteId from Cliente where NIT='" + cliente.NIT + "'";
            Int64 clienteNitTemp = 0;
            Int64.TryParse(newCliente.NIT, out clienteNitTemp);
            if (clienteNitTemp == 0)
            {
                newCliente.IdCliente = -1;
                newCliente.NIT = clienteNitTemp.ToString();
                return;
            }
            string sql = "SELECT ClienteId from Cliente where NIT='" + newCliente.NIT + "'";
            CommonUtils.ConexionBD.AbrirConexion();
            SqlDataReader reader = CommonUtils.ConexionBD.EjecutarConsultaReader(sql);
            int numberOfElements = 0;
            int idClient = 0;
            while (reader.Read())
            {
                numberOfElements++;
                idClient = (int)reader.GetDecimal(0);
            }
            CommonUtils.ConexionBD.CerrarConexion();
            if (numberOfElements == 0)
            {
                sql = "INSERT INTO Cliente (Nombre,NIT,Telefono,Direccion)  VALUES (N'" +
                         newCliente.Nombre + "','" + newCliente.NIT + "','" + CommonUtils.StringUtils.EscapeSQL(newCliente.Telefono) + "','" + CommonUtils.StringUtils.EscapeSQL(newCliente.Direccion) + "')";
                // CommonUtils.ConexionBD.Actualizar(sql);
                decimal value = CommonUtils.ConexionBD.ActualizarRetornandoId(sql);
                newCliente.IdCliente = (int)value;
            }
            else//there is a client with that nit, 
            {
                sql = "UPDATE Cliente set Nombre='" + newCliente.Nombre + "'  where clienteId='" + idClient + "'";
                CommonUtils.ConexionBD.Actualizar(sql);
                newCliente.IdCliente = idClient;
            }
        }

        #region Validar formulario
        private void ValidateEmptyStringRule(BaseEdit control)
        {
            if (control.Text == null || control.Text.Trim().Length == 0)
                dxErrorProvider.SetError(control, "Este campo no puede ser vacio", ErrorType.Critical);
            else
                dxErrorProvider.SetError(control, "");
        }

        private void GetErrorProviderMessages()
        {
            IList<Control> controlErrors = dxErrorProvider.GetControlsWithError();
            //    controlErrors.OrderBy<>;
            alertControlPnlVentas.Show( this.FindForm( ) , "Falló al guardar." , "Los cambios NO se guardaron.  Por favor, intente nuevamente." , Image );
            MessageBox.Show(this, (string)controlErrors[0].Tag, dxErrorProvider.GetError(controlErrors[0]), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion        

        private void txtTipoPedido_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtTipoPedido.SelectedItem.ToString() == "Inmediato")
            {
                btnVenta.Text = "Vender";
                lblEstadoPedido.Visible = false;
                txtEstadoPedido.Visible = false;
                labelFechaEntrega.Visible = false;
                dateEditFechaEntrega.Visible = false;

                //datos invisibles ya que solo se habilitan para reserva
                lblTelefono.Visible = false;
                txtTelefono.Visible = false;
                txtDireccion.Visible = false;
                lblDireccion.Visible = false;
            }
            else 
            {
                if (txtEstadoPedido.SelectedIndex == 0)
                {
                    btnVenta.Text = "Vender";

                }
                else
                {
                    btnVenta.Text = "Hacer reserva";
                }
                lblTelefono.Visible = true;
                txtTelefono.Visible = true;
                txtDireccion.Visible = true;
                lblDireccion.Visible = true;
                lblEstadoPedido.Visible = true;
                txtEstadoPedido.Visible = true;
                labelFechaEntrega.Visible = true;
                dateEditFechaEntrega.Visible = true;
             
            }
        }

        private void txtEstadoPedido_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtEstadoPedido.SelectedIndex == 1 && txtEstadoPedido.Visible)
            {
                btnVenta.Text = "Hacer reserva";
            }
            else 
            {
                btnVenta.Text = "Vender";
            }
        }

        private void dateEditFechaEntrega_Validating(object sender, CancelEventArgs e)
        {
            ValidateEmptyStringRule(sender as BaseEdit);
        }

        private void txtTelefono_Validating(object sender, CancelEventArgs e)
        {
            ValidateEmptyStringRule(sender as BaseEdit);
        }

        private void txtDireccion_Validating(object sender, CancelEventArgs e)
        {
            ValidateEmptyStringRule(sender as BaseEdit);
        }


        #region ActualizarGrids Members

        public void actualizarGrid()
        {
            //clean cliente
            //clean otros datos
            //this.gridItems.DataSource = null;
            this.ResetFields();
        }

        #endregion

        private int calcularNumeroPedido()
        {
            int numeroPedido = 1;
            string sqlQuery = "SELECT NumeroPedido,FechaPedido,FechaPedidoText FROM   Pedido WHERE (PedidoId = (SELECT  MAX(PedidoId) AS Expr1 FROM Pedido AS Pedido_1))";//"Select NumeroPedido from Pedido";
           // int rows=CommonUtils.ConexionBD.EjecutarConsulta(sqlQuery).Rows.Count;
            DataTable tableTemp = CommonUtils.ConexionBD.EjecutarConsulta(sqlQuery);
            int rows = tableTemp.Rows.Count;
            if (rows == 0)
            {
                return numeroPedido;
            }
            else
            {
                DateTime fechaPedido = DateTime.Parse(tableTemp.Rows[0][2].ToString());
                if (fechaPedido.Date == DateTime.Now.Date)
                {
                    numeroPedido = int.Parse(tableTemp.Rows[0][0].ToString());
                    numeroPedido = numeroPedido + 1;
                    return numeroPedido;
                }
                else
                {
                    return numeroPedido;
                }
            }
        }

        private void retrieveClients_name(object sender, EventArgs e)
        {
            List<Cliente> cli = clientes.clientesXNit(txtNIT.Text);
            if (cli.Count == 1)
            {
                txtCliente.Text = cli[0].Nombre;
            }
            else
            {
                   txtCliente.Text = "";
            }
        }
      
    }
}
