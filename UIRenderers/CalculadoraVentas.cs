using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using System.Linq;
using HFUtils.Impuestos.CodigoControl;
using HFUtils.Impuestos.Facturacion;
using HFUtils.Impuestos.Facturacion.Factura;
using HFUtils.Core.TextTools;
using System.IO;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;
//using MessagingToolkit.QRCode.Codec;
//using MessagingToolkit.QRCode.Codec;
//using MessagingToolkit.QRCode.Codec;

//using messaging

namespace UIRenderers
{
    public partial class CalculadoraVentas : XtraForm
    {
        System.Drawing.Font printFont;
        private DataTable dataTable;
        private double total;
        private bool pagado;
        private bool esReserva;
        private CommonUtils.Cliente cliente;
        private static log4net.ILog log = log4net.LogManager.GetLogger("CalculadoraVentas");
        public string MessageBarValue { get; set; }
        public string RazonSocialEmpresa { get; set; }
        public string NombreEmpresa { get; set; }
        public string DireccionEmpresa { get; set; }
        public string NITEmpresa { get; set; }
        public int SucursalNroEmpresa { get; set; }
        private string sucursalMessage;
        public string TelefonoEmpresa { get; set; }
        public string Rubro { get; set; }
        private string fechaEntrega = string.Empty;
        private string fechaEntregaNoFormat = string.Empty;
        private string pedidoId = string.Empty;
        private CommonUtils.ActualizarGrids UpdateGrids;
        private DevExpress.XtraEditors.CheckEdit chBtn;
     
        private CodigoControlV70 genCodControl;
        private Factura factura;
        private string codigoControlValue;
        private string nroAutorizacionValue;
        private string literalValue;
        CommonUtils.DataBaseLog dbLog;

        #region Print Properties
        protected int pageNumber;
        protected string buffer;
        protected const int TABLEN = 8;
        #endregion

        #region Datos Requeridos para codigo control
        Int64 dosifNroAutorizacion;
        String dosifLlave;
        
        String telecentroNit;
        String dosifFechaLimite;

        int facturaNro;
        int facturaNitCliente;
        String facturaFechaTransaccion;
        Int64 facturaMontoTotal;
        #endregion 
        
        //private bool changeStateButton = false;
        private Form ventas; // necesitamos referencia al form que se abre al pagar en la lista de reservas
        //para luego se pueda cerrar automaticamente;

        public Form Ventas 
        {
            get { return ventas; }
            set { ventas = value; }
        }
        
        public string FechaEntrega 
        {
            get { return fechaEntrega; }
            set { fechaEntrega = value; }
        }
        public string FechaEntregaNoFormat
        {
            get { return fechaEntregaNoFormat; }
            set { fechaEntregaNoFormat = value; }
        }
        public string PedidoId
        {
            get { return pedidoId; }
            set { pedidoId = value; }
        }
        public DevExpress.XtraEditors.CheckEdit checkBtn
        {
            get { return chBtn; }
            set { chBtn = value; }
        }

        private Image Image
        {
            get
            {
                return imageCollectionCalculadora.Images[0];
            }
        }

        public CalculadoraVentas(CommonUtils.ActualizarGrids IUpdateGrids,DataTable table,Form parentForm,double totalDinero,bool reserva,bool sePago,CommonUtils.Cliente _client)
        {
            InitializeComponent();
            InitializePrintSettings( );
            dbLog = new CommonUtils.DataBaseLog();
            log4net.Config.XmlConfigurator.Configure( );
            dataTable = table;
            total = totalDinero;
            txtCuenta.Text = total.ToString();
            txtTotal.Text = total.ToString();
            pagado = sePago;
            esReserva = reserva;
            cliente = _client;
           // ventasPanel = ventasPnl;
            UpdateGrids = IUpdateGrids;

            InitConexionDB();
            

            if (parentForm != null)
            {
                Left = parentForm.Left + (parentForm.Width - Width) / 2;
                Top = parentForm.Top + (parentForm.Height - Height) / 2;
            }
        }

        private void InitializePrintSettings( )
        {
            pageNumber = 0;
            buffer = string.Empty;
        }

        private void InitConexionDB()
        {
            CommonUtils.ConexionBD.StringDeConexion = CommonUtils.ConexionBD.GetConnectionString();
        }

        private void radioBtnSinDescuento_Click(object sender, EventArgs e)
        {
            txtEfectivo.Enabled = false;
            txtPorcentaje.Enabled = false;
            txtEfectivo.Text = "";
            txtPorcentaje.Text = "";
            txtTotal.Text = total.ToString();
            if (txtEfectivoSinDescuento.Text == string.Empty || txtTotal.Text == string.Empty) 
            {
                return;
            }
            txtCambio.Text = (float.Parse(txtEfectivoSinDescuento.Text) - float.Parse(txtTotal.Text)).ToString();

        }

        private void radioBtnEfectivo_Click(object sender, EventArgs e)
        {
            txtPorcentaje.Enabled = false;
            txtEfectivo.Enabled = true;
            txtPorcentaje.Text = "";
        }

        private void radioBtnPorcentaje_Click(object sender, EventArgs e)
        {
            txtEfectivo.Enabled = false;
            txtPorcentaje.Enabled = true;
            txtEfectivo.Text = "";
        }

        private void txtEfectivoSinDescuento_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (e.NewValue.ToString() == string.Empty)
            {
                txtCambio.Text = "0.00";
                return;
            }
          
          //  txtTotal.Text = (total - descuento).ToString();
            txtCambio.Text=(float.Parse(e.NewValue.ToString())-float.Parse(txtTotal.Text)).ToString();
        }

        private void txtEfectivo_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (e.NewValue.ToString() == string.Empty)
            {
                txtTotal.Text = total.ToString();
                return;
            }
            txtTotal.Text = (float.Parse(txtCuenta.Text) - float.Parse(e.NewValue.ToString())).ToString();
            if(txtEfectivoSinDescuento.Text!=string.Empty)
            txtCambio.Text = (float.Parse(txtEfectivoSinDescuento.Text) - float.Parse(txtTotal.Text)).ToString();

        }

        private void txtPorcentaje_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (e.NewValue.ToString() == string.Empty)
            {
                txtTotal.Text = total.ToString();
                return;
            }
            float descuento=float.Parse(e.NewValue.ToString()) * float.Parse(txtCuenta.Text) / 100;
            txtTotal.Text = (float.Parse(txtCuenta.Text) - descuento).ToString();
            if(txtEfectivoSinDescuento.Text!=string.Empty)
            txtCambio.Text = (float.Parse(txtEfectivoSinDescuento.Text) - float.Parse(txtTotal.Text)).ToString();

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {   /* 
            CommonUtils.ConexionBD.AbrirConexion();
            DataTable read = CommonUtils.ConexionBD.TraerTabla("Pedido");//("SELECT COUNT(*) FROM  Pedido");
           
           int numberOfRows = read.Rows.Count;
           CommonUtils.ConexionBD.CerrarConexion();
           if (numberOfRows > 100)
           {
               string message = "El sistema es una version demo y no podra usar a partir de ahora";
               string caption = "Demo";
               MessageBoxButtons buttons = MessageBoxButtons.OK;
               DialogResult result;Fff

               // Displays the MessageBox.

               result = MessageBox.Show(message, caption, buttons);

               if (result == System.Windows.Forms.DialogResult.OK)
               {
                   Application.Exit();
                   return;
               }
           }*/
            if (!esReserva)
            {
                try
                {
                    this.descontar();
                   // this.guardarDineroEnCaja();
                   // this.registrarPedidoInmediato();
                    if (this.isFacturaAvailable())
                    {
                        this.generateFactura();
                        //timer = new System.Threading.Timer( obj => { this.generateAndPrintDetalle( ); } , null , 5000 , System.Threading.Timeout.Infinite );
                        this.generateAndPrintDetalle();
                    }
                    else
                    {
                        this.generateAndPrintDetalle();
                    }
                    UpdateGrids.actualizarGrid();
                    MessageBarValue = "El pedido fue creado satisfactoriamente.";
                    alertControlCalculadoraVentas.Show(this.FindForm(), "Pedido", MessageBarValue, Image);
                   
                    this.Close();
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message, ex);
                    MessageBarValue = "No se pudo Crear el pedido. Hubo un error: " + ex.Message;
                    alertControlCalculadoraVentas.Show(this.FindForm(), "Pedido", MessageBarValue, Image);
                    //MessageBox.Show(this, "No se pudo Crear el pedido.", "Pedido ", MessageBoxButtons.OK);

                }
               
            }
            else if (esReserva && pagado)
            {
                try
                {
                    this.guardarDineroEnCaja();
                    this.RegistrarReserva();
                    if (this.isFacturaAvailable())
                    {
                        this.generateFactura();
                        this.generateAndPrintDetalle();
                    }
                    else
                    {
                        this.generateAndPrintDetalle();
                    }
                    // this.ventasPanel.cleanPanelVentas();
                    UpdateGrids.actualizarGrid();
                    MessageBarValue="La reserva pagada fue creada satisfactoriamente.";
                    alertControlCalculadoraVentas.Show(this.FindForm(), "Reserva",MessageBarValue , Image);
                    this.Close();
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message, ex);
                    MessageBarValue = "No se pudo Crear la reserva pagada. Hubo un error: " + ex.Message;
                    //MessageBox.Show(this, "No se pudo Crear la reserva pagada.", "Reserva ", MessageBoxButtons.OK);
                    alertControlCalculadoraVentas.Show(this.FindForm(), "Reserva", MessageBarValue, Image);
                }
               
            }
            else if (esReserva && !pagado)
            {
                try
                {
                    this.guardarDineroEnCaja();
                    this.modificarPedido();
                    if (this.isFacturaAvailable())
                    {
                        this.generateFactura();
                        this.generateAndPrintDetalle();
                    }
                    else 
                    {
                        this.generateAndPrintDetalle();
                    }
                    UpdateGrids.actualizarGrid();
                    // changeStateButton = true;
                    chBtn.Checked = true;
                    ventas.Close();// this will close the form that pnllistaReservas carga. 
                    alertControlCalculadoraVentas.Show(this.FindForm(), "Reserva modificada.", "La reserva fue pagada satisfactoriamente.", Image);
                    this.Close();
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message, ex);
                    MessageBarValue = "No se pudo pagar la reserva. Hubo un error: " + ex.Message;
                    //MessageBox.Show(this, "El pago de la reserva no pudo ser realizado", "Reserva ", MessageBoxButtons.OK);
                    alertControlCalculadoraVentas.Show(this.FindForm(), "Reserva modificada.", MessageBarValue, Image);
  
                }                      
            }          
        }

        private void modificarPedido()
        {
            string descuento = (double.Parse(txtCuenta.Text) - double.Parse(txtTotal.Text)).ToString();
            string sqlQuery = "Update Pedido set Pagado='True',Descuento='"+ descuento+"',TotalVenta='"+total+"' where PedidoId=" + pedidoId;
            CommonUtils.ConexionBD.Actualizar(sqlQuery);
            sqlQuery = "Delete from Pedido_Producto where PedidoId=" + pedidoId;
            CommonUtils.ConexionBD.Actualizar(sqlQuery);
            List<DataRow> listaPedidos = dataTable.AsEnumerable().ToList();
            foreach (DataRow item in listaPedidos)
            {
                string costoReceta = CalculadoraVentas.obtenerCostoReceta(item);//this.obtenerCostoReceta(item);
                sqlQuery = "insert into Pedido_Producto(PedidoId,ProductoId,Cantidad,Total,CostoReceta,PrecioProducto,Descripcion) values('" + pedidoId + "','" + item["Id"] + "','" + item["Cantidad"] + "','" + item["Subtotal"] + "','" + costoReceta + "','" + item["Precio"] + "','" + item["Descripcion"] + "')";
                CommonUtils.ConexionBD.Actualizar(sqlQuery);
            }  
        }
       
        //categoria 5 es para combo
        private void descontar()
        {
            
            List<DataRow> listaCompras = dataTable.AsEnumerable().ToList();
            string sql = string.Empty;
            foreach(DataRow dtRow in listaCompras)
            {
                string idProducto = dtRow["Id"].ToString();
                sql = "Select CategoriaId from Producto where ProductoId=" + idProducto;
                int categoriaId = int.Parse(CommonUtils.ConexionBD.EjecutarConsultaEspecial(sql));
                if (categoriaId != 5)//no es combo
                {
                    
                    this.descontarUnProducto(idProducto, dtRow["Cantidad"].ToString());
                  
                }
                else //es combo
                {
                   // CommonUtils.ConexionBD.AbrirConexion();
                    sql = "(Select ProductoId,Cantidad from Producto_Producto where ComboId=" + dtRow["Id"]+")";
                    CommonUtils.ConexionBD.AbrirConexion();
                    DataTable reader = CommonUtils.ConexionBD.EjecutarConsulta(sql);
                    CommonUtils.ConexionBD.CerrarConexion();
                   for (int i = 0; i < reader.Rows.Count; i++)
                   {
                       string id = reader.Rows[i][0].ToString();
                       
                       this.descontarUnProducto(id, (float.Parse(reader.Rows[i][1].ToString())*float.Parse(dtRow["Cantidad"].ToString())).ToString());
                   }
                      
                      /* {
                           int idPro = (int)reader.GetDecimal(0);
                           float cantidad = (float)reader.GetInt32(1) * );
                         
                       }*/
                    
                }
               /* sql = "SELECT Receta_Insumo.Cantidad, Receta_Insumo.InsumoId, Insumo.CantidadInsumoEnIventario, Insumo.Nombre FROM Receta_Insumo INNER JOIN Insumo ON Receta_Insumo.InsumoId = Insumo.InsumoId WHERE (Receta_Insumo.RecetaId = (SELECT RecetaId FROM Producto WHERE (ProductoId = "+idProducto+")))";
                //SqlCommand cmdInsumosReceta= new SqlCommand(sql, CommonUtils.ConexionBD.Conexion);
                DataTable temp = CommonUtils.ConexionBD.EjecutarConsulta(sql);
                            // DataSet dataSet = new DataSet();
               // dataSet.Load(cmdInsumosReceta.ExecuteReader(), LoadOption.OverwriteChanges, "InsumosReceta");
               // cmdInsumosReceta.Connection.Close();
                List<DataRow> insumosReceta = temp.AsEnumerable().ToList();// dataSet.Tables["InsumosReceta"].AsEnumerable().ToList();
                foreach (DataRow item in insumosReceta)
                {
                    string cantidadNuevadeInsumo = (float.Parse(item[2].ToString())-(float.Parse(dtRow["Cantidad"].ToString()) * float.Parse(item[0].ToString()))).ToString();
                    sql = "update Insumo set CantidadInsumoEnIventario="+cantidadNuevadeInsumo+" where InsumoId="+item[1].ToString();
                    CommonUtils.ConexionBD.Actualizar(sql);
                }*/
            } 
        }

        private void descontarUnProducto(string idProducto,string cantidad)
        {
           // string idProducto = dtRow["Id"].ToString();
            CommonUtils.ConexionBD.AbrirConexion();
            string sql = "SELECT Receta_Insumo.Cantidad, Receta_Insumo.InsumoId, Insumo.CantidadInsumoEnIventario, Insumo.Nombre FROM Receta_Insumo INNER JOIN Insumo ON Receta_Insumo.InsumoId = Insumo.InsumoId WHERE (Receta_Insumo.RecetaId = (SELECT RecetaId FROM Producto WHERE (ProductoId = " + idProducto + ")))";
            DataTable temp = CommonUtils.ConexionBD.EjecutarConsulta(sql);
            CommonUtils.ConexionBD.CerrarConexion();
           
            List<DataRow> insumosReceta = temp.AsEnumerable().ToList();// dataSet.Tables["InsumosReceta"].AsEnumerable().ToList();
            foreach (DataRow item in insumosReceta)
            {
                string cantidadNuevadeInsumo = (float.Parse(item[2].ToString()) - (float.Parse(cantidad) * float.Parse(item[0].ToString()))).ToString();
                sql = "update Insumo set CantidadInsumoEnIventario=" + cantidadNuevadeInsumo + " where InsumoId=" + item[1].ToString();
                CommonUtils.ConexionBD.Actualizar(sql);
            }
        }

        private void guardarDineroEnCaja()
        {
            string costo = txtTotal.Text;
            string sqlQuery = "SELECT esModificable FROM Caja WHERE (CajaId = (SELECT MAX(CajaId) FROM Caja))";
            bool esModificable = Boolean.Parse(CommonUtils.ConexionBD.EjecutarConsultaEspecial(sqlQuery));
            if (esModificable)
            {
                sqlQuery = "select DineroReal +" + costo + " from Caja WHERE (CajaId = (SELECT MAX(CajaId) FROM Caja))";
                string dineroActualCaja = CommonUtils.ConexionBD.EjecutarConsultaEspecial(sqlQuery);
                sqlQuery = "INSERT INTO Caja(FechaText,DineroSistema,DineroReal,Diferencia,Descripcion,Fecha,esModificable) VALUES('" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss.fff") + "'," + dineroActualCaja + ",0,0,'nuevo valor','" + DateTime.Now + "','False')";
                CommonUtils.ConexionBD.Actualizar(sqlQuery);
            }
            else
            {
                sqlQuery = "Select Fecha from Caja WHERE (CajaId = (SELECT MAX(CajaId) FROM Caja))";
                DateTime lastTime = DateTime.Parse(CommonUtils.ConexionBD.EjecutarConsultaEspecial(sqlQuery));
                if (lastTime.Date == DateTime.Now.Date)
                {
                    sqlQuery = "select DineroSistema + " + costo + " from Caja  WHERE (CajaId = (SELECT MAX(CajaId) FROM Caja))";
                    string dineroSistema = CommonUtils.ConexionBD.EjecutarConsultaEspecial(sqlQuery);
                    sqlQuery = "UPDATE Caja set FechaText='"+ DateTime.Now.ToString("yyyy-MM-dd H:mm:ss.fff") +"', Fecha='" + DateTime.Now + "', DineroSistema=" + dineroSistema + " WHERE (CajaId =  (SELECT  MAX(CajaId) AS Expr1  FROM  Caja AS Caja_1))";
                    CommonUtils.ConexionBD.Actualizar(sqlQuery);
                }
                else
                {
                    sqlQuery = "select DineroSistema +" + costo + " from Caja WHERE (CajaId = (SELECT MAX(CajaId) FROM Caja))";
                    string dineroActualCaja = CommonUtils.ConexionBD.EjecutarConsultaEspecial(sqlQuery);
                    sqlQuery = sqlQuery = "INSERT INTO Caja(FechaText,DineroSistema,DineroReal,Diferencia,Descripcion,Fecha,esModificable) VALUES('" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss.fff") + "'," + dineroActualCaja + ",0,0,'nuevo valor','" + DateTime.Now + "','False')";
                    CommonUtils.ConexionBD.Actualizar(sqlQuery);
                }
            }
                        
        }

        private void manageClient()
        {
            string sql = "SELECT ClienteId from Cliente where NIT='" +cliente.NIT +"'";
            Int64 clienteNitTemp = 0;
            Int64.TryParse(cliente.NIT, out clienteNitTemp);
            if (clienteNitTemp == 0)
            {
                cliente.IdCliente = -1;
                cliente.NIT = clienteNitTemp.ToString();
                return;
            }
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
                             cliente.Nombre + "','" + cliente.NIT + "','" + CommonUtils.StringUtils.EscapeSQL(cliente.Telefono) + "','" + CommonUtils.StringUtils.EscapeSQL(cliente.Direccion) + "')";
                    // CommonUtils.ConexionBD.Actualizar(sql);
                    decimal value = CommonUtils.ConexionBD.ActualizarRetornandoId(sql);
                    cliente.IdCliente = (int)value;
                }
                else//there is a client with that nit, 
                {
                    sql = "UPDATE Cliente set Nombre='" + cliente.Nombre + "'  where clienteId='" + idClient + "'";
                    CommonUtils.ConexionBD.Actualizar(sql);
                    cliente.IdCliente = idClient;
                }
           
            
        }

        private void registrarPedidoInmediato()
        {           
            string descuento = (double.Parse(txtCuenta.Text) - double.Parse(txtTotal.Text)).ToString();
            string entregado = "True";
        
            string tipoPedido = "Inmediato";
            this.manageClient();
            int idCliente = cliente.IdCliente;
            string nit = cliente.NIT;

            string sqlQuery = "Insert into Pedido(FechaPedidoText,FechaEntregaText,Descuento,TipoPedido,Pagado,FechaEntrega,Entregado,ClienteId,TotalVenta,FechaPedido,NombreCliente,Direccion,Telefono,NIT,NumeroPedido) values('" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss.fff") + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss.fff") + "','" + descuento + "','" + tipoPedido + "','" + pagado + "','" + DateTime.Now + "','" + entregado + "','" + idCliente + "','" + total + "','" + DateTime.Now + "','" + cliente.Nombre + "','" + cliente.Direccion + "','" + cliente.Telefono + "','" + nit + "','" + cliente.NumeroPedido + "')";
            decimal idPedido = CommonUtils.ConexionBD.ActualizarRetornandoId(sqlQuery);
            List<DataRow> listaPedidos = dataTable.AsEnumerable().ToList();
            foreach (DataRow item in listaPedidos)
            {
                string costoReceta = CalculadoraVentas.obtenerCostoReceta(item);//this.obtenerCostoReceta(item);
                sqlQuery = "insert into Pedido_Producto(PedidoId,ProductoId,Cantidad,Total,CostoReceta,PrecioProducto,Descripcion) values('" + idPedido + "','" + item["Id"] + "','" + item["Cantidad"] + "','" + item["Subtotal"] + "','" + costoReceta + "','" + item["Precio"] + "','"+item["Descripcion"]+"')";
                CommonUtils.ConexionBD.Actualizar(sqlQuery);
            }                       
             
        }

        public  void RegistrarReserva()
        {
            string descuento = (double.Parse(txtCuenta.Text) - double.Parse(txtTotal.Text)).ToString();
            string entregado = "False";
            string tipoPedido = "Reserva";
            this.manageClient();

            string sqlQuery = "Insert into Pedido(FechaPedidoText,FechaEntregaText,Descuento,TipoPedido,Pagado,FechaEntrega,Entregado,ClienteId,TotalVenta,FechaPedido,NombreCliente,Direccion,Telefono,NIT,NumeroPedido) values('"
                + DateTime.Now.ToString( "yyyy-MM-dd H:mm:ss.fff" ) + "','" + this.FechaEntrega + "','" + descuento + "','" + tipoPedido + "','" + pagado + "','" + this.FechaEntregaNoFormat
                + "','" + entregado + "','" + cliente.IdCliente + "','" + total + "','" + DateTime.Now + "','" + cliente.Nombre + "','" + cliente.Direccion + "','" 
                + cliente.Telefono + "','" + cliente.NIT + "','" + cliente.NumeroPedido + "')";

            decimal idPedido = CommonUtils.ConexionBD.ActualizarRetornandoId(sqlQuery);
            List<DataRow> listaPedidos = dataTable.AsEnumerable().ToList();
            foreach (DataRow item in listaPedidos)
            {
                string costoReceta = CalculadoraVentas.obtenerCostoReceta(item);//this.obtenerCostoReceta(item);
                sqlQuery = "insert into Pedido_Producto(PedidoId,ProductoId,Cantidad,Total,CostoReceta,PrecioProducto,Descripcion) values('" + idPedido + "','" + item["Id"] + "','" + item["Cantidad"] + "','" + item["Subtotal"] + "','" + costoReceta + "','" + item["Precio"] + "','"+item["Descripcion"]+"')";
                CommonUtils.ConexionBD.Actualizar(sqlQuery);
            }             
        }

        //method 
        public static  string obtenerCostoReceta(DataRow row)
        {
            string costoReceta = string.Empty;
            string sqlQuery = string.Empty;
                        
            sqlQuery="select CategoriaId from Producto where ProductoId="+row["Id"];
            if (CommonUtils.ConexionBD.EjecutarConsultaEspecial(sqlQuery) == "5")// producto es un combo
            {
              // sqlQuery = "SELECT SUM(CostoReceta) AS Expr1 FROM Receta WHERE  (RecetaId IN  (SELECT RecetaId  FROM  Producto WHERE (ProductoId IN (SELECT ProductoId FROM Producto_Producto WHERE (ComboId = '"+row["Id"]+"')))))";
                      sqlQuery=" SELECT SUM(Receta.CostoReceta * Producto_Producto.Cantidad) AS Expr1 " + 
                    " FROM Producto_Producto INNER JOIN"+
                        " Producto ON Producto.ProductoId = Producto_Producto.ProductoId INNER JOIN " +
                        " Receta ON Receta.RecetaId = Producto.RecetaId " +
                        " WHERE        (Producto_Producto.ComboId ='" + row["Id"] + "')";
                costoReceta = CommonUtils.ConexionBD.EjecutarConsultaEspecial(sqlQuery); 
            }
            else
            {
                sqlQuery = "Select CostoReceta From Receta where  RecetaId=(select RecetaId from Producto where ProductoId='" + row["Id"] + "' )";
                costoReceta = CommonUtils.ConexionBD.EjecutarConsultaEspecial(sqlQuery);
            }
            return costoReceta; 
        }

        private void CalculadoraVentas_FormClosed(object sender, FormClosedEventArgs e)
        {
            /*if (checkBtn != null && !changeStateButton)
            {
                checkBtn.Checked = false;
            }*/
        }

        private void buildDosifData()
        {
            string sql = "Select dosificacion_nro_autorizacion,dosificacion_llave from dosificacion where dosificacion_activa=1";
            try
            {
                CommonUtils.ConexionBD.AbrirConexion();
                SqlDataReader reader = CommonUtils.ConexionBD.EjecutarConsultaReader(sql);
                if (reader.Read())
                {
                    dosifNroAutorizacion = reader.GetInt64(0);
                    dosifLlave = reader.GetString(1);
                }
                CommonUtils.ConexionBD.CerrarConexion();
            }
            catch (Exception ex)
            {
                log.Error( ex.Message , ex );
            }
 
        }
        private void buildCompanyData()
        {
            string sql="select TelecentroId,RazonSocial,NombreEmpresa,Direccion,NIT,SucursalNro,Telefono,Rubro from telecentro";
            try
            {
                CommonUtils.ConexionBD.AbrirConexion();
                SqlDataReader reader = CommonUtils.ConexionBD.EjecutarConsultaReader(sql);
                if (reader.Read())
                {
                    RazonSocialEmpresa = reader.GetString(1);
                    NombreEmpresa = reader.GetString(2);
                    DireccionEmpresa = reader.GetString(3);
                    NITEmpresa = reader.GetString(4);
                    SucursalNroEmpresa = reader.GetInt32(5);
                    TelefonoEmpresa = reader.GetString(6);
                    Rubro = reader.GetString(7);

                    switch (SucursalNroEmpresa)
                    {
                        case 0:
                            sucursalMessage = "CASA MATRIZ";
                            break;
                        default:
                            sucursalMessage = "SUCURSAL Nro " + SucursalNroEmpresa;
                            break;
                    }

                }
                CommonUtils.ConexionBD.CerrarConexion();
            }
            catch (Exception ex)
            {
                log.Error( ex.Message , ex );
            }
        }

        private void buildNroFacturaAndFechaTransaccion()
        {
            string sql = "select dosificacion_ultimo_valor from dosificacion where dosificacion_activa=1";
            string factNumero=string.Empty;
            try
            {
                factNumero = CommonUtils.ConexionBD.EjecutarConsultaEspecial(sql);
                int.TryParse(factNumero, out facturaNro);
                facturaNro = facturaNro + 1;
                DateTime currentDate = DateTime.Now;
                facturaFechaTransaccion = currentDate.Year.ToString() +
                                          formatTwoDigits(currentDate.Month.ToString()) +
                                          formatTwoDigits(currentDate.Day.ToString());
            }
            catch (Exception ex)
            {
                log.Error( ex.Message , ex );
 
            }
        }
        private void buildFechaLimite()
        {
            string sql = "select dosificacion_fechalimite from dosificacion where dosificacion_activa=1";
            try
            {
               // List<Object> dosificacionInfo = DBbasicOperations.Instance.executeReadSQL(
             //       new String[] { "fechaLimite" }, "dosificacion", "activa", "1");                
              
                string fechaLimit = CommonUtils.ConexionBD.EjecutarConsultaEspecial(sql);
                DateTime dfec;               
                DateTime.TryParse(fechaLimit, out dfec);//(DateTime)fechaLimit;
                dosifFechaLimite = formatTwoDigits(dfec.Day.ToString()) + "/"
                                 + formatTwoDigits(dfec.Month.ToString()) + "/"
                                 + dfec.Year.ToString();
            }
            catch (Exception ex)
            {
                log.Error( ex.Message , ex );
            }            
        }

        private void buildFacturaOBject()
        {
            factura = new Factura();
            
            List<DataRow> listaPedidos = dataTable.AsEnumerable().ToList();
            ItemFactura itemFactura;
            foreach (DataRow item in listaPedidos)
            {
    //            "(PedidoId,ProductoId,Cantidad,Total,CostoReceta,PrecioProducto,Descripcion) values('" + pedidoId + "','" + item["Id"] + "','" + item["Cantidad"] + "','" + item["Subtotal"] + "','" + costoReceta + "','" + item["Precio"] + "','" + item["Descripcion"] + "')";
 //('" + pedidoId + "','" + item["Id"] + "','" + item["Cantidad"] + "','" + item["Subtotal"] + "','" + "','" + item["Precio"] + "','" "')";
                double importeTotal=0.0;
                double precio = 0.0;
                int cantidadPedido = 0;
                short idItem = 0;
                double.TryParse(item["Subtotal"].ToString(),out importeTotal );
                double.TryParse(item["Precio"].ToString(), out precio);
                int.TryParse(item["Cantidad"].ToString(), out cantidadPedido);
                short.TryParse(item["Id"].ToString(), out idItem);
                itemFactura = new ItemFactura(idItem, item["Articulo"].ToString(), precio, cantidadPedido, importeTotal);
                factura.addItem(itemFactura);
            }
 
        }

        private void buildCodigoControl()
        {
            try
            {
                factura.recalculateTotal();
                genCodControl = new CodigoControlV70();
                Int64 totalInt;
                Int64 nitClienteTemp = 0;
                Int64.TryParse(cliente.NIT, out nitClienteTemp);

                double totalAccount = double.Parse(txtTotal.Text);//factura.TotalFactura - descuentoC;//we need to generate code of control based in the discounts!!!
                totalInt = (Int64)totalAccount;
                double diff = totalAccount - totalInt;

                nroAutorizacionValue = "Autorizacion Nro: " + dosifNroAutorizacion.ToString();
                
                codigoControlValue = genCodControl.generarCodControl(dosifNroAutorizacion, dosifLlave
                    , facturaNro, nitClienteTemp, facturaFechaTransaccion, diff >= 0.5 ? totalInt + 1 : totalInt );
                          
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                codigoControlValue = "";

            }
        }

        private void buildLiteral()
        {
            double totalAccount = double.Parse(txtTotal.Text);
            LiteralConverter litprov = new LiteralConverter();

            literalValue = litprov.convertToLiteral((Int32)totalAccount
)
                           + " " + toCentavos(totalAccount) + "/100 Bs."; 
        }

        private bool isFacturaAvailable()
        {
            DateTime currentDate = DateTime.Now;
            DateTime dateLimite;
            string sql="Select dosificacion_fechalimite from dosificacion where dosificacion_activa=1";
            
            CommonUtils.ConexionBD.AbrirConexion();
            SqlDataReader reader = CommonUtils.ConexionBD.EjecutarConsultaReader(sql);
            bool hasRowsValue = reader.HasRows;
            CommonUtils.ConexionBD.CerrarConexion();
            //DataTable t=CommonUtils.ConexionBD.TraerTabla(s`
            if(!hasRowsValue)
      //      if (string.IsNullOrEmpty(fechaLimiteT) || fechaLimiteT == "")//there is no information about dosificacion
            {
                return false;
            }
            else
            {
                string fechaLimiteT = CommonUtils.ConexionBD.EjecutarConsultaEspecial(sql);
                DateTime.TryParse(fechaLimiteT, out dateLimite);
                if(currentDate>dateLimite)//means is not posible to emit a factura
                {
                    return false;                    
                }

                currentDate=currentDate.AddDays(14);
                if (currentDate.Date >= dateLimite.Date)
                {
                    //show alert
                    alertControlCalculadoraVentas.Show(this.FindForm(), "Alerta fecha limite factura.", "Faltan menos de dos semanas para llegar a la fecha limite de emision de facturas", Image);
                    return true;
                }
                else
                {
                    return true;
                }               
            }            
        }

        //
        private void generateAndPrintDetalle()
        {
            StreamWriter streamTofill = new System.IO.StreamWriter(@"c:\temp\tmpfile.txt");
            List<DataRow> listaPedidos = dataTable.AsEnumerable().ToList();
            CommonUtils.FormatImpresion impresionFormat = new CommonUtils.FormatImpresion();
           
            this.buildFacturaOBject();
            //impresionFormat.fillDetalle(ref streamTofill, 35, factura, cliente.NumeroPedido.ToString());
            impresionFormat.fillDetalleWithDescription( ref streamTofill , 30 , listaPedidos , cliente.NumeroPedido.ToString( ) );
            streamTofill.Close();
            printFont = new System.Drawing.Font("Courier New", 8);
           
            printDocument.Print();
            #region print detalle
            //not implemented yet
            #endregion

        }

        private void generateFactura()
        {
            
            DateTime currentDate = DateTime.Now;
            StreamWriter streamTofill = new System.IO.StreamWriter(@"c:\temp\tmpfile.txt",false,Encoding.UTF8);
            this.buildCompanyData();
            this.buildDosifData();
            this.buildNroFacturaAndFechaTransaccion();
            this.buildFacturaOBject();
            this.buildFechaLimite();
            this.buildCodigoControl();
            this.buildLiteral();
            CommonUtils.FormatImpresion impresionFormat = new CommonUtils.FormatImpresion();
            log.Error( cliente.Nombre );
            impresionFormat.fillStreamWithData(ref streamTofill,
                40, NombreEmpresa, RazonSocialEmpresa, sucursalMessage,
                DireccionEmpresa,Rubro,TelefonoEmpresa, "Cochabamba-Bolivia", NITEmpresa, facturaNro.ToString()
                , dosifNroAutorizacion.ToString(), formatTwoDigits(currentDate.Day.ToString()) + "/" + formatTwoDigits(currentDate.Month.ToString())+ "/" + currentDate.Year.ToString(),
                                cliente.Nombre, cliente.NIT, factura, literalValue,
                codigoControlValue, dosifFechaLimite,(double.Parse(txtCuenta.Text) - double.Parse(txtTotal.Text)));

            streamTofill.Close();

            printFont = new System.Drawing.Font("Courier New", 8);
            printDocument.Print();
          
            string sql="update dosificacion set dosificacion_ultimo_valor='"+facturaNro+"'";
            CommonUtils.ConexionBD.Actualizar(sql);

            dbLog.RecordOperationInDataBase(NombreEmpresa, RazonSocialEmpresa, sucursalMessage, DireccionEmpresa, Rubro, TelefonoEmpresa
                , "Cochabamba-Bolivia", NITEmpresa, facturaNro.ToString(), dosifNroAutorizacion.ToString(),
                currentDate.ToString( "yyyy-MM-dd H:mm:ss.fff" ) , cliente.Nombre ,
                cliente.NIT, factura, literalValue, codigoControlValue, dosifFechaLimite,double.Parse(txtTotal.Text));            
            
        }
        private String toCentavos(Double monto)
        {
            Int32 Centavos = ((Int32)(monto * 1000.00) % 1000) / 10;
            String result = Centavos.ToString();
            if (result.Length == 1)
                result = "0" + result;
            return result;
        }

        private String formatTwoDigits(String value)
        {
            if (value.Length == 1)
                return "0" + value;
            else
                return value;
        }

        private void printDocument_BeginPrint( object sender , System.Drawing.Printing.PrintEventArgs e )
        {
            StreamReader textReader = null;
            string line = string.Empty;
            StringBuilder tempBuffer = new StringBuilder( );

            pageNumber = 0;

            try
            {
                textReader = new StreamReader( @"c:\temp\tmpfile.txt" );

                while ( ( line = textReader.ReadLine( ) ) != null )
                {
                    tempBuffer.Append( line );
                    tempBuffer.Append( "\n" );
                }
                buffer = tempBuffer.ToString( );
            }
            finally
            {
                if ( textReader != null )
                {
                    textReader.Close( );
                }
            }
        }

        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            string line = string.Empty;
            int lineNumber = 1;
            int linesPerPage;
            int idx = 0;
            float pos;
            bool formfeed = false;
            bool imagePrinted = false;
            float leftMargin = e.MarginBounds.Left;
            leftMargin = 0;

            if ( e.Cancel )
                return;
            
            linesPerPage = ( int ) ( e.MarginBounds.Height / printFont.GetHeight( e.Graphics ) );
            pageNumber++;

            while ( lineNumber < linesPerPage )
            {
                idx = buffer.IndexOf( '\n' );

                if ( idx == -1 )
                    break;

                int ffidx = buffer.IndexOf( '\f' , 0 , idx );

                if ( ffidx != -1 )
                {
                    formfeed = true;
                    idx = ffidx;
                }

                line = buffer.Substring( 0 , idx );
                line = ReplaceTabChar( line );
                pos = e.MarginBounds.Top + ( ( lineNumber - 1 ) * printFont.GetHeight( e.Graphics ) );
                pos = imagePrinted ? pos + 160 : pos;
                e.Graphics.DrawString( line , printFont , Brushes.Black , leftMargin , pos , new StringFormat( ) );
                
                if (line.Contains("FECHA LIMITE DE EMISION:"))
                {
                    pos = pos + 20;
                    var img = generateQRCode() as Image;
                    e.Graphics.DrawImage(img, 70, pos, 150, 150);
                    imagePrinted = true;
                }
                
                if ( formfeed && buffer[ idx + 1 ] == '\n' )
                    idx++;

                buffer = buffer.Substring( idx + 1 );
                
                if ( formfeed )
                    break;
            
                lineNumber++;
            }
            
            //int positionImage = (int)pos;
            //var img = System.Drawing.Image.FromFile(generateQRCode());
   
            if ( buffer.Length > 0 )
                e.HasMorePages = true;
            else
                e.HasMorePages = false;
         
            //float yPos = 0f;
            //int count = 0;
            //float leftMargin = e.MarginBounds.Left;
            //leftMargin = 0;
            //float topMargin = e.MarginBounds.Top;
            //string line = null;
            //float linesPerPage = e.MarginBounds.Height / printFont.GetHeight(e.Graphics);

            //StreamReader streamToRead = new StreamReader(@"c:\temp\tmpfile.txt");
            
            //while (count < linesPerPage)
            //{
            //    line = streamToRead.ReadLine();
            //    if (line == null)
            //    {
            //        break;
            //    }
            //    yPos = topMargin + count * printFont.GetHeight(e.Graphics);
            //    e.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());
            //    count++;
            //}
            //if ( line.Length > 0 )
            //{
            //    e.HasMorePages = true;
            //}
            //streamToRead.Close();

        }

        private void printDocument_EndPrint( object sender , System.Drawing.Printing.PrintEventArgs e )
        {
            File.Delete( @"C:\temp\tmpfile.txt" );
        }

        private string ReplaceTabChar( string line )
        {
            StringBuilder buffer = new StringBuilder( line.Length * TABLEN );
            int idx = line.IndexOf( '\t' );
            int start = 0;

            while ( idx != -1 )
            {
                buffer.Append( line.Substring( start , idx - start ) );
                int count = TABLEN - ( buffer.Length % TABLEN );
                buffer.Append( new string( ' ' , count ) );
                start = idx + 1;
                idx = line.IndexOf( '\t' , start );
            }
            buffer.Append( line.Substring( start ) );
            return ( buffer.ToString( ) );
        }

        //builds the string that will generate qrcode
        private string getQRString()
        {
            string clienteNIT = String.IsNullOrEmpty(cliente.NIT)? "0" : cliente.NIT;
            string nombreDelComprador = String.IsNullOrEmpty(cliente.Nombre) ? "0" : cliente.Nombre;
            DateTime currentDate = DateTime.Now;
            string qrString = string.Empty;
            string nombreEmpresa = String.IsNullOrEmpty(this.NombreEmpresa) ? this.RazonSocialEmpresa : this.NombreEmpresa;
            qrString = this.NITEmpresa + "|" + nombreEmpresa + "|" + facturaNro + "|" + this.dosifNroAutorizacion + "|"
                + currentDate.ToString("dd/MM/yyyy") + "|" + txtTotal.Text + "|" +
             codigoControlValue + "|" + dosifFechaLimite + "|" + clienteNIT + "|" + nombreDelComprador;  
            return qrString;
        }

        //generate qrcode based on the string
        private Bitmap generateQRCode()
        {
            string qrString = getQRString();
            QRCodeEncoder enc = new QRCodeEncoder();
            Bitmap qrBitmapCode = enc.Encode(qrString);
            //borrameQRCode.Save(@"C:\test1.txt");
            return qrBitmapCode;
        }
    }
}
