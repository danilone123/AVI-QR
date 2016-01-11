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

namespace UIRenderers
{
    public partial class PnlListaReservas : DevExpress.XtraEditors.XtraUserControl,CommonUtils.ActualizarGrids
    {
        private string reservarName = "Reserva";
        private Form formReservasPagar;
        private static log4net.ILog log = log4net.LogManager.GetLogger("PnlNuevoPrivilegio");
        public string MessageBarValue { get; set; }
       // private List<DataRow> listRowsChecked = new List<DataRow>();
        private Dictionary<string,List< string>> listRowsChecked = new Dictionary<string,List<string>>();

        public Form FormReservasPagar
        {
            get { return formReservasPagar; }
            set { formReservasPagar = value; }
        }
        private Image Image
        {
            get
            {
                return imageCollection1.Images[2];
            }
        }
        public PnlListaReservas()
        {
            InitializeComponent();
            InitConexionDB();
            RefreshGridReservas();

            // this 3 lines of code should only be called once will make the event gets called more thant
            //1 time!!!
            this.gridViewReservas.ShowingEditor += new CancelEventHandler(gridViewReservas_ShowingEditor);
            //this.repositoryItemCheckEdit2.Click += new EventHandler(repositoryItemCheckEdit2_Click);
            this.repositoryItemCheckEdit2.CheckedChanged += new EventHandler(repositoryItemCheckEdit2_CheckedChanged);
            this.repositoryItemCheckEdit1.CheckedChanged += new EventHandler(repositoryItemCheckEdit1_CheckedChanged);

        }
        private void RefreshGridReservas()
        {// "DECLARE @FromDate datetime, @ToDate datetime SET @FromDate = CONVERT(varchar, DATEADD(dd, 1 - DATEPART(dw, GETDATE()), GETDATE()), 7) SET @ToDate = CONVERT(varchar, DATEADD(dd,7 - DATEPART(dw, GETDATE()), GETDATE()), 7) " +

            string sqlQuery="DECLARE @FromDate datetime, @ToDate datetime SET @FromDate = CONVERT(varchar, DATEADD(dd, 1 - DATEPART(dw, GETDATE()), GETDATE()), 7)  SET @ToDate = CONVERT(varchar, DATEADD(dd, 30, GETDATE()), 7) "+
                " SELECT PedidoId, TipoPedido, Pagado, FechaEntrega, Entregado, FechaPedido, NombreCliente, Direccion, Telefono, NIT, ClienteId, TotalVenta, NumeroPedido"
                + " FROM  Pedido WHERE (Entregado = 0) AND (TipoPedido ='"+reservarName+"')" +
            " AND ( convert(varchar,CONVERT(datetime,FechaEntregaText,21),7) BETWEEN @FromDate and @ToDate"
                + " ) ORDER BY FechaEntrega DESC";
////
           // string sqlQuery = "Select PedidoId, TipoPedido,Pagado,FechaEntrega,Entregado,FechaPedido,NombreCliente,Direccion,Telefono,NIT,ClienteId,TotalVenta,NumeroPedido from Pedido WHERE Entregado=0 and TipoPedido='"+reservarName+"'";
            gridControlReservas.DataSource = null;
            gridControlReservas.DataSource = CommonUtils.ConexionBD.EjecutarConsulta(sqlQuery);
            gridViewReservas.Columns[0].FieldName = "PedidoId";
            gridViewReservas.Columns[1].FieldName = "NombreCliente";
            gridViewReservas.Columns[2].FieldName = "Telefono";
            gridViewReservas.Columns[3].FieldName = "Direccion";
            gridViewReservas.Columns[4].FieldName = "FechaPedido";
            gridViewReservas.Columns[5].FieldName = "FechaEntrega";
            gridViewReservas.Columns[6].FieldName = "Pagado";
            gridViewReservas.Columns[7].FieldName = "Entregado";
          //  this.gridColumnEntregado.View.ShowingEditor+=new CancelEventHandler(ColumnaEntregado_ShowingEditor);
      //      repositoryItemCheckEdit1.PictureChecked = Image.FromFile(@"C:\\Users\\Daniel\\Downloads\\iInventory22September\\iInventory\\Resources\\down.png");
         }

        void repositoryItemCheckEdit1_CheckedChanged(object sender, EventArgs e)
        {
            DataRow row = gridViewReservas.GetDataRow(gridViewReservas.FocusedRowHandle);
            if (row == null || row[0].ToString() == string.Empty)
            {
                //MessageBox.Show(this, "Seleccione una fila", "Reservas", MessageBoxButtons.OK);
                return;
            }
            string sqlQuery = "Select ProductoId,Cantidad From Pedido_Producto where PedidoId="+row[0].ToString();
            try
            {
                DevExpress.XtraEditors.CheckEdit chBtn = (sender as DevExpress.XtraEditors.CheckEdit);
                if (chBtn.Checked)
                {
                    List<string> pedidoProducto = new List<string>();
                    CommonUtils.ConexionBD.AbrirConexion();
                    SqlDataReader reader = CommonUtils.ConexionBD.EjecutarConsultaReader(sqlQuery);
                    while (reader.Read())
                    {
                        pedidoProducto.Add(reader.GetDecimal(0).ToString() + "," + reader.GetInt32(1).ToString());
                        //   pedidoProducto += reader.GetDecimal(0).ToString() + "," + reader.GetInt32(1).ToString() + "|";
                    }
                    CommonUtils.ConexionBD.CerrarConexion();
                    if (!listRowsChecked.ContainsKey(row[0].ToString()))
                    {
                        listRowsChecked.Add(row[0].ToString(), pedidoProducto);
                    }
                }
                else
                {
                    listRowsChecked.Remove(row[0].ToString());
                }

            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                MessageBarValue = "Error al check campo entregar " + ex.Message;
                alertControlReservas.Show(this.FindForm(),
                      "Reserva", MessageBarValue
                      , Image);

            }
            finally
            {

            }           
        }
       
        //for pagar column
        void repositoryItemCheckEdit2_CheckedChanged(object sender, EventArgs e)
        {
            DataRow r = gridViewReservas.GetDataRow(gridViewReservas.FocusedRowHandle);
            DevExpress.XtraEditors.CheckEdit chBtn = (sender as DevExpress.XtraEditors.CheckEdit);
            if (chBtn.Checked)
            {
                formReservasPagar = new Form();
                
                PnlVentas ventas = new PnlVentas();
              //  ventas.InitializeDataTable();
                CommonUtils.Cliente cliente = new CommonUtils.Cliente(int.Parse(r[10].ToString()), r[6].ToString(), r[8].ToString(), r[9].ToString(), r[7].ToString(), int.Parse(r[12].ToString()));
                ventas.Cliente = cliente;
                ventas.StartButtonsReserva(new string[] { r[8].ToString(), r[7].ToString(), r[3].ToString(), r[5].ToString() });
                chBtn.Checked = false;
                ventas.PedidoId = r[0].ToString();
                ventas.ReservasPanel = this;
               
                ventas.Chbtn = chBtn;
                ventas.LlamadoDePnlReservas = true;
                formReservasPagar.Size=new Size(1046, 738);
                getPedidosDescripcion(r[0].ToString(),ventas);
                ventas.Dock = DockStyle.Fill;
                formReservasPagar.Controls.Add(ventas);
                formReservasPagar.MaximizeBox = false;               
                formReservasPagar.Text = "Pagar reserva";
                formReservasPagar.ShowDialog();
                formReservasPagar.Refresh();
              
            }
           // chBtn.Checked = false;
        }

        private void getPedidosDescripcion(string pedidoId,PnlVentas ventas)
        {
            string sql = "Select Producto.Nombre,Pedido_Producto.Descripcion,Pedido_Producto.Cantidad,Pedido_Producto.PrecioProducto,Pedido_Producto.Total,Pedido_Producto.ProductoId from Pedido_Producto,Producto where Pedido_Producto.ProductoId=Producto.ProductoId and Pedido_Producto.PedidoId=" + pedidoId;
            CommonUtils.ConexionBD.AbrirConexion();
            SqlDataReader reader = CommonUtils.ConexionBD.EjecutarConsultaReader(sql);
            while (reader.Read())
            {               // (new object[] { articulo, "", CANTIDAD_MINIMA, gridProducto.ventas.Precio, (CANTIDAD_MINIMA * gridProducto.ventas.Precio), gridProducto.ventas.ProductoID });
                object descripcion= reader.GetValue(1);
                ventas.DataTable.Rows.Add(new object[]{reader.GetString(0),descripcion.ToString(),reader.GetInt32(2),(float)reader.GetDouble(3),reader.GetDouble(4),(int)reader.GetDecimal(5)}); 
            }
            CommonUtils.ConexionBD.CerrarConexion();    
          
            //gridProducto.ventas.Articulo , "",CANTIDAD_MINIMA , gridProducto.ventas.Precio , ( CANTIDAD_MINIMA * gridProducto.ventas.Precio ),gridProducto.ventas.ProductoID }
        }

        void gridViewReservas_ShowingEditor(object sender, CancelEventArgs e)
        {
            DataRow r = gridViewReservas.GetDataRow(gridViewReservas.FocusedRowHandle);
            DevExpress.XtraGrid.Views.Grid.GridView c = (sender as DevExpress.XtraGrid.Views.Grid.GridView);
            string nameColumn = c.FocusedColumn.Caption;
            if (r == null || r[0].ToString() == string.Empty)
            {
                return;
            }

            if (nameColumn == "Pagar" )
            {
                if (Boolean.Parse(r[2].ToString()))
                {
                    e.Cancel = true;
                }
                else
                {
                    e.Cancel = false;
                }
            }
            
            if (nameColumn == "Entregar")
            {
                if (Boolean.Parse(r[2].ToString()))
                {
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }
            }

        }

        private void InitConexionDB()
        {
            CommonUtils.ConexionBD.StringDeConexion = CommonUtils.ConexionBD.GetConnectionString();
        }

        private void ColumnaEntregado_ShowingEditor(object sender, CancelEventArgs e)
        {   
           
                       
        }

        #region ActualizarGrids Members

        public void actualizarGrid()
        {

            this.RefreshGridReservas();            
        }

        #endregion


        //will open a form that will contain a description of the order
        private void barButtonPedido_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
             DataRow rowSelected = gridViewReservas.GetDataRow(gridViewReservas.FocusedRowHandle);
            if (rowSelected == null || rowSelected[0].ToString() == string.Empty )
            {
                MessageBox.Show(this, "Seleccione una fila", "Reservas", MessageBoxButtons.OK);
                return;
            }
            DescripcionPedidoForm formDescripcion = new DescripcionPedidoForm(this.ParentForm,rowSelected[6].ToString(), rowSelected[12].ToString(), rowSelected[1].ToString(), rowSelected[0].ToString());
            formDescripcion.ShowDialog();
            formDescripcion.Refresh();
        }

        private void btnEntregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (listRowsChecked.Count > 0)
                {
                    string sqlQuery = string.Empty;
                    foreach (string item in listRowsChecked.Keys)
                    {
                        sqlQuery = "update Pedido set Entregado='True' where PedidoId=" + item;
                        CommonUtils.ConexionBD.Actualizar(sqlQuery);
                        List<string> value = listRowsChecked[item];
                        descontar(value);
                    }
                    /* foreach (List<string> item in listRowsChecked.Values)
                     {
              
                         descontarProducto(item);
                     }*/
                    this.actualizarGrid();
                    MessageBarValue = "La reserva fue entegada satisfactoriamente.";
                    

                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                MessageBarValue = "No se pudo entregar la reserva. Hubo un error: " + ex.Message;

            }
            finally
            {
                alertControlReservas.Show(this.FindForm(),
                       "Reserva Entregada.", MessageBarValue
                       , Image);
            }
        }

        //categoria 5 es para combo
        private void descontar(List<string> listaCompras)
        {
          //  CommonUtils.ConexionBD.AbrirConexion();            
            string sql = string.Empty;
            try
            {
                foreach (string rows in listaCompras)
                {
                    string[] itemSplit = rows.Split(',');
                    string idProducto = itemSplit[0];
                    sql = "Select CategoriaId from Producto where ProductoId=" + idProducto;
                    int categoriaId = int.Parse(CommonUtils.ConexionBD.EjecutarConsultaEspecial(sql));
                    if (categoriaId != 5)//no es combo
                    {
                        this.descontarUnProducto(idProducto, itemSplit[1]);
                    }
                    else //es combo
                    {
                        //CommonUtils.ConexionBD.AbrirConexion();
                        sql = "(Select ProductoId,Cantidad from Producto_Producto where ComboId=" + idProducto + ")";
                        CommonUtils.ConexionBD.AbrirConexion();
                        DataTable reader = CommonUtils.ConexionBD.EjecutarConsulta(sql);
                        CommonUtils.ConexionBD.CerrarConexion();
                        for (int i = 0; i < reader.Rows.Count; i++)
                        {
                            string id = reader.Rows[i][0].ToString();
                            this.descontarUnProducto(id, (float.Parse(reader.Rows[i][1].ToString()) * float.Parse(itemSplit[1])).ToString());
                        }
                    }
                   
                }
            }
            catch (Exception ex)
            {

                log.Error(ex.Message, ex);
                MessageBarValue = "No se pudo descontar producto. Hubo un error: " + ex.Message;
                alertControlReservas.Show(this.FindForm(),
                      "Reserva Entregada.", MessageBarValue
                      , Image);
            }
           
            
        }

        private void descontarUnProducto(string idProducto, string cantidad)
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

        private void barButtonRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.actualizarGrid();
        }

        private void gridControlReservas_DoubleClick(object sender, EventArgs e)
        {
            DataRow rowSelected = gridViewReservas.GetDataRow(gridViewReservas.FocusedRowHandle);
            if (rowSelected == null || rowSelected[0].ToString() == string.Empty)
            {
                MessageBox.Show(this, "Seleccione una fila", "Reservas", MessageBoxButtons.OK);
                return;
            }
            DescripcionPedidoForm formDescripcion = new DescripcionPedidoForm(this.ParentForm, rowSelected[6].ToString(), rowSelected[12].ToString(), rowSelected[1].ToString(), rowSelected[0].ToString());
            formDescripcion.ShowDialog();
            formDescripcion.Refresh();
        }
    }
}
