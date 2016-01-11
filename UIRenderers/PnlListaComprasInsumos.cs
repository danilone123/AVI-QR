using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections;
using System.Data.SqlClient;
using DevExpress.XtraEditors.DXErrorProvider;
using System.Diagnostics;

namespace UIRenderers
{
    public partial class PnlListaComprasInsumos : DevExpress.XtraEditors.XtraUserControl,CommonUtils.ICajaTest,CommonUtils.ActualizarGrids
    {
        ProgressForm progressForm;
        private ArrayList comprasToUpdate = new ArrayList();
        private DataTable userTable;
        private SqlDataAdapter adapter;
        private DataSet ds;
        private SqlCommand command;
        private SqlCommandBuilder builder;
        private string sqlSelectQuery = string.Empty;
        private Dictionary<string, object[]> dictionaryOfRows = new Dictionary<string, object[]>();
        bool dataHasChanged;
        private static log4net.ILog log = log4net.LogManager.GetLogger("PnlListaComprasInsumos");
        public string MessageBarValue { get; set; }
        
        public PnlListaComprasInsumos()
        {
            InitializeComponent();
            InitConexionDB();
            InitializeLookupEdit();
            loadInsumosComponent();            
            ds = new DataSet("MainDataSet");
            //will show compras from domingo to sabado
            sqlSelectQuery = "DECLARE @FromDate datetime, @ToDate datetime SET @FromDate = CONVERT(varchar, DATEADD(dd, 1 - DATEPART(dw, GETDATE()), GETDATE()), 7) SET @ToDate = CONVERT(varchar, DATEADD(dd,7 - DATEPART(dw, GETDATE()), GETDATE()), 7)" +
                "SELECT Compras.ComprasId, Compras.FechaCompra, Compras.Cantidad, Compras.CostoUnidad, Compras.Total, Compras.Descripcion, Insumo.Nombre," +
            " Insumo.InsumoId, Insumo.Cantidad AS Lote,Insumo.Unidad" +
            " FROM  Compras INNER JOIN " +
            " Insumo ON Insumo.InsumoId = Compras.InsumoId " +
            " WHERE (convert(varchar,CONVERT(datetime,Compras.FechaCompraText,21),7) BETWEEN @FromDate AND @ToDate)" +
            " ORDER BY Compras.FechaCompra DESC";
            //"SELECT  Compras.ComprasId, Compras.FechaCompra, Compras.Cantidad, Compras.CostoUnidad, Compras.Total, Compras.Descripcion, Insumo.Nombre,Insumo.InsumoId,Insumo.Cantidad as Lote FROM Compras INNER JOIN Insumo ON Insumo.InsumoId = Compras.InsumoId Order by Compras.FechaCompra";//"select * from Compras";
            command = new SqlCommand(sqlSelectQuery, CommonUtils.ConexionBD.getConnection());
            adapter = new SqlDataAdapter(command);
            builder = new SqlCommandBuilder(adapter);            
           InitializeGridComponent();
        }

        #region Propiedades
        public bool DataHasChanged
        {
            get { return dataHasChanged; }
            set { dataHasChanged = value; }
        }
        #endregion

        private void InitConexionDB()
        {
            CommonUtils.ConexionBD.StringDeConexion = CommonUtils.ConexionBD.GetConnectionString(); //GetConnectionString();
        }

        private Image Image
        {
            get
            {
                return imageCollectionCompasInsumos.Images[0];
            }
        }

        private void InitializeGridComponent()
        {
            adapter.Fill(ds, "Compras");
            userTable = ds.Tables[0];
            gridControlComprasInsumos.DataSource = null;
            gridControlComprasInsumos.DataSource = userTable.DefaultView;//ds.Tables[0];
            //   numberOfRows = gridViewComprarlista.RowCount;
            ((DevExpress.XtraGrid.Views.Grid.GridView)gridControlComprasInsumos.Views[0]).Columns[0].FieldName = "ComprasId";
            ((DevExpress.XtraGrid.Views.Grid.GridView)gridControlComprasInsumos.Views[0]).Columns[1].FieldName = "InsumoId";
            ((DevExpress.XtraGrid.Views.Grid.GridView)gridControlComprasInsumos.Views[0]).Columns[2].FieldName = "Cantidad";
            ((DevExpress.XtraGrid.Views.Grid.GridView)gridControlComprasInsumos.Views[0]).Columns[4].FieldName = "Total";//"Total";
            ((DevExpress.XtraGrid.Views.Grid.GridView)gridControlComprasInsumos.Views[0]).Columns[3].FieldName = "CostoUnidad";// "Descripcion";
            ((DevExpress.XtraGrid.Views.Grid.GridView)gridControlComprasInsumos.Views[0]).Columns[5].FieldName = "Descripcion";
            ((DevExpress.XtraGrid.Views.Grid.GridView)gridControlComprasInsumos.Views[0]).Columns[6].FieldName = "FechaCompra";
            gridViewComprasInsumos.Columns[7].FieldName = "Lote";
            gridViewComprasInsumos.Columns[8].FieldName = "Unidad";
            //     ((DevExpress.XtraGrid.Views.Grid.GridView)gridControlComprarLista.Views[0]).Columns[8].FieldName = "InsumoId";
           
        }

        private void InitializeLookupEdit()
        {
            string sql = "select Nombre,InsumoId as Id from Insumo";
            CommonUtils.ConexionBD.AbrirConexion();
            gridLookUpEditInsumo.Properties.DataSource = null;
            gridLookUpEditInsumo.Properties.DataSource = CommonUtils.ConexionBD.EjecutarConsulta(sql);          
            CommonUtils.ConexionBD.CerrarConexion();
            gridLookUpEditInsumo.Properties.ValueMember = "Id";
            gridLookUpEditInsumo.Properties.DisplayMember = "Nombre";
            gridLookUpEditInsumo.Properties.View.ActiveFilterEnabled = true;
           
        }

        private void loadInsumosComponent()
        {
            string sql = "select Nombre,InsumoId as Id from Insumo";
            CommonUtils.ConexionBD.AbrirConexion();
            repositoryItemGridLookUpEdit1.DataSource = null;
            repositoryItemGridLookUpEdit1.DataSource = CommonUtils.ConexionBD.EjecutarConsulta(sql);
            CommonUtils.ConexionBD.CerrarConexion();
            repositoryItemGridLookUpEdit1.ValueMember = "Id";
            repositoryItemGridLookUpEdit1.DisplayMember = "Nombre";
            repositoryItemGridLookUpEdit1.View.ActiveFilterEnabled = true;
            repositoryItemGridLookUpEdit1.EditValueChanged += new EventHandler(repositoryItemGridLookUpEdit1_EditValueChanged);
      
        }       

        void repositoryItemGridLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
          /*  string id = repositoryItemGridLookUpEdit1.GetKeyValue(gridViewComprasInsumos.FocusedRowHandle).ToString();
            DataRow r = gridViewComprasInsumos.GetDataRow(gridViewComprasInsumos.FocusedRowHandle);
            string idInsumo = r[7].ToString();
            string sql = "select Cantidad from Insumo where InsumoId=" + idInsumo;//gridLookUpEditInsumo.Properties.ValueMember;
            gridViewComprasInsumos.SetRowCellValue(gridViewComprasInsumos.FocusedRowHandle, gridViewComprasInsumos.Columns[7], CommonUtils.ConexionBD.EjecutarConsultaEspecial(sql));
        */}

       
        private void gridLookUpEditInsumo_EditValueChanged(object sender, EventArgs e)
        {
            if (gridLookUpEditInsumo.GetSelectedDataRow() == null)
            {
                return;
            }
            string idInsumo = (gridLookUpEditInsumo.GetSelectedDataRow() as DataRowView).Row.ItemArray[1].ToString();
            string sql = "select Cantidad from Insumo where InsumoId=" + idInsumo;//gridLookUpEditInsumo.Properties.ValueMember;
            txtLote.Text = CommonUtils.ConexionBD.EjecutarConsultaEspecial(sql);
            sql = "select Unidad from Insumo where InsumoId="+idInsumo;
            txtUnidad.Text = CommonUtils.ConexionBD.EjecutarConsultaEspecial(sql);
        }

        private void txtCantidad_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (e.NewValue.ToString() == string.Empty || txtCostoUnidad.Text == string.Empty)
            {
                return;
            }
            txtCostoTotal.Text=(float.Parse(e.NewValue.ToString())*float.Parse(txtCostoUnidad.Text)).ToString();
        }

        private void barBtnComprasEspeciales_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ListaComprasEspecialesEstable compras = new ListaComprasEspecialesEstable(this.ParentForm);
            compras.ShowDialog();
            compras.Refresh();
        }

        private void barButtonIventario_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ListaIventarios iventario = new ListaIventarios(this.ParentForm);
            iventario.ShowDialog();
            iventario.Refresh();
        }

        #region ICaja Members

        public void update(object[] row)
        {
            string costoTotal = (float.Parse(row[3].ToString()) * float.Parse(row[2].ToString())).ToString();
            string sqlQuery = "Update Caja set DineroSistema=((select DineroSistema from Caja where (CajaId = (SELECT MAX(CajaId) FROM Caja)))-(select " + costoTotal + " - Total from Compras where ComprasId=" + row[0].ToString() + "))where (CajaId = (SELECT MAX(CajaId) FROM Caja))";
            CommonUtils.ConexionBD.Actualizar(sqlQuery);
        }

        public void insertCaja(object[] row)
        {
            string cantidad = row[2].ToString();
            string costoUnidad = row[3].ToString();
            string costo = (float.Parse(costoUnidad) * float.Parse(cantidad)).ToString();
            string sqlQuery = "SELECT esModificable FROM Caja WHERE (CajaId = (SELECT MAX(CajaId) FROM Caja))";
            bool esModificable = Boolean.Parse(CommonUtils.ConexionBD.EjecutarConsultaEspecial(sqlQuery));
            if (esModificable)
            {
                sqlQuery = "select DineroReal -" + costo + " from Caja WHERE (CajaId = (SELECT MAX(CajaId) FROM Caja))";
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
                    sqlQuery = "select DineroSistema - " + costo + " from Caja  WHERE (CajaId = (SELECT MAX(CajaId) FROM Caja))";
                    string dineroSistema = CommonUtils.ConexionBD.EjecutarConsultaEspecial(sqlQuery);
                    sqlQuery = "UPDATE Caja set FechaText='" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss.fff") + "', Fecha='" + DateTime.Now + "', DineroSistema=" + dineroSistema + " WHERE (CajaId =  (SELECT  MAX(CajaId) AS Expr1  FROM  Caja AS Caja_1))";
                    CommonUtils.ConexionBD.Actualizar(sqlQuery);
                }
                else
                {
                    sqlQuery = "select DineroSistema -" + costo + " from Caja WHERE (CajaId = (SELECT MAX(CajaId) FROM Caja))";
                    string dineroActualCaja = CommonUtils.ConexionBD.EjecutarConsultaEspecial(sqlQuery);
                    sqlQuery = sqlQuery = "INSERT INTO Caja(FechaText,DineroSistema,DineroReal,Diferencia,Descripcion,Fecha,esModificable) VALUES('" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss.fff") + "'," + dineroActualCaja + ",0,0,'nuevo valor','" + DateTime.Now + "','False')";
                    CommonUtils.ConexionBD.Actualizar(sqlQuery);
                }
            }
        }

        public void deleteValueCaja(DataRow row)
        {
            string sqlQuery = "Update Caja set DineroSistema=((select DineroSistema from Caja where (CajaId = (SELECT MAX(CajaId) FROM Caja)))+(select Total From Compras where ComprasId=" + row[0].ToString() + ")) where (CajaId = (SELECT MAX(CajaId) FROM Caja))";
            CommonUtils.ConexionBD.Actualizar(sqlQuery);
        }

        #endregion
        private void manageUpdateFeature(object[] dr)
        {
            string cantidadUnidadesCompradas = string.Empty;
            string sqlUpdate = "";
            CommonUtils.ConexionBD.AbrirConexion();
            SqlDataReader reader = CommonUtils.ConexionBD.EjecutarConsultaReader("Select InsumoId from Compras where ComprasId=" + dr[0].ToString());
            int idInsumo = 0;
            while (reader.Read())
            {
                idInsumo = (int)reader.GetDecimal(0);
            }
            string costoTotal = (float.Parse(dr[3].ToString()) * float.Parse(dr[2].ToString())).ToString();

            CommonUtils.ConexionBD.CerrarConexion();
            if (dr[1].ToString() == idInsumo.ToString())// insumo was not changed
            {
                //SELECT  Compras.Cantidad * Insumo.Cantidad AS Expr1 FROM Compras INNER JOIN Insumo ON Compras.InsumoId = Insumo.InsumoId WHERE (Compras.ComprasId = 23)
                // sqlUpdate = "UPDATE Insumo  SET  CantidadInsumoEnIventario =(SELECT " + dr[2].ToString() + "*(Select Cantidad from Insumo where InsumoId="+dr[7].ToString()+") -(SELECT Compras.Cantidad*Insumo.Cantidad FROM Compras INNER JOIN Insumo on Compras.InsumoId=Insumo.InsumoId WHERE (ComprasId = " + dr[0].ToString() + ")) + (SELECT CantidadInsumoEnIventario FROM Insumo AS Insumo_1 WHERE (InsumoId = '" + dr[7].ToString() + "')) AS Expr1) WHERE (InsumoId =" + dr[7].ToString() + ")";
                sqlUpdate = "UPDATE Insumo  SET  CantidadInsumoEnIventario =(SELECT " + dr[2].ToString() + "*(Select Cantidad from Insumo where InsumoId=" + dr[1].ToString() + ") -(SELECT CantidadUnidadesCompradas FROM Compras WHERE (ComprasId = " + dr[0].ToString() + ")) + (SELECT CantidadInsumoEnIventario FROM Insumo AS Insumo_1 WHERE (InsumoId = '" + dr[1].ToString() + "')) AS Expr1) WHERE (InsumoId =" + dr[1].ToString() + ")";
                CommonUtils.ConexionBD.Actualizar(sqlUpdate);
                cantidadUnidadesCompradas = (float.Parse(dr[2].ToString()) * float.Parse(CommonUtils.ConexionBD.EjecutarConsultaEspecial("select Cantidad from Insumo where InsumoId=" + dr[1].ToString()))).ToString();
                sqlUpdate = "UPDATE Compras set Descripcion='" + dr[4].ToString() + "',Cantidad='" + dr[2].ToString() + "',Total='" + costoTotal + "', InsumoId='" + dr[1].ToString() + "',CostoUnidad='" + dr[3].ToString() + "', CantidadUnidadesCompradas='" + cantidadUnidadesCompradas + "'  where ComprasId=" + dr[0].ToString();
                CommonUtils.ConexionBD.Actualizar(sqlUpdate);
            }
            else //insumo was changed
            {
                //sqlUpdate = "Update Insumo SeT CantidadInsumoEnIventario =((select CantidadInsumoEnIventario from Insumo WHERE InsumoId=" + idInsumo + ")-("+dr[2].ToString()+"*(select Cantidad from Insumo where InsumoId="+idInsumo+"))) where InsumoId="+idInsumo;
                sqlUpdate = "Update Insumo SET CantidadInsumoEnIventario =((select CantidadInsumoEnIventario from Insumo WHERE InsumoId=" + idInsumo + ")-(select CantidadUnidadesCompradas from Compras WHERE ComprasId=" + dr[0].ToString() + ")) where InsumoId=" + idInsumo;
                CommonUtils.ConexionBD.Actualizar(sqlUpdate);
                sqlUpdate = "UPDATE Insumo Set CantidadInsumoEnIventario =((select CantidadInsumoEnIventario from Insumo WHERE InsumoId=" + dr[1].ToString() + ") +(" + dr[2].ToString() + "*(select Cantidad from Insumo where InsumoId=" + dr[1].ToString() + "))) where InsumoId=" + dr[1].ToString();
                CommonUtils.ConexionBD.Actualizar(sqlUpdate);
                cantidadUnidadesCompradas = (float.Parse(dr[2].ToString()) * float.Parse(CommonUtils.ConexionBD.EjecutarConsultaEspecial("select Cantidad from Insumo where InsumoId=" + dr[1].ToString()))).ToString();
                sqlUpdate = "UPDATE Compras set Descripcion='" + dr[4].ToString() + "',Cantidad='" + dr[2].ToString() + "',Total='" + costoTotal + "', InsumoId='" + dr[1].ToString() + "',CostoUnidad='" + dr[3].ToString() + "', CantidadUnidadesCompradas='" + cantidadUnidadesCompradas + "'  where ComprasId=" + dr[0].ToString();//*(select Cantidad from Insumo where InsumoId="+dr[7].ToString()+")
                CommonUtils.ConexionBD.Actualizar(sqlUpdate);
            }

            // dr[4] = costoTotal;
            // userTable.Rows[gridViewComprarlista.FocusedRowHandle][3]=costoTotal;
            // gridViewComprarlista.RefreshRow(gridViewComprarlista.FocusedRowHandle);

        }
        private void barButtonEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                foreach (object[] obj in dictionaryOfRows.Values)
                {
                    update(obj);
                    manageUpdateFeature(obj);
                }
                userTable.Clear();
                InitializeGridComponent();
                dictionaryOfRows.Clear();
                dataHasChanged = false;
                MessageBarValue = "Los insumos fueron modificados satisfactoriamente.";          
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                MessageBarValue = "No se pudo modificar los insumos. Hubo un error: " + ex.Message;
            }
            finally            
            {
                alertControlCompasInsumos.Show(this.FindForm(), "Modificar compras insumos",MessageBarValue, Image);
            }
        }

        private void barButtonRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            actualizarGrid();
        }

        #region ActualizarGrids Members

        public void actualizarGrid()
        {
            userTable.Clear();
            InitializeGridComponent();
            InitializeLookupEdit();
            loadInsumosComponent();
        }

        #endregion

        private void gridViewComprasInsumos_ShowingEditor(object sender, CancelEventArgs e)
        {
            DataRow r = gridViewComprasInsumos.GetDataRow(gridViewComprasInsumos.FocusedRowHandle);
            if (r == null || r[0].ToString() == string.Empty)
            {
                return;
            }
            string sqlQuery = "select EsModificable from Compras where ComprasId=" + r[0].ToString();
            string value = CommonUtils.ConexionBD.EjecutarConsultaEspecial(sqlQuery);
            if (value == "True")
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!dxErrorProviderCompras.HasErrors)
            {
                try
                {
                    string idInsumo = (gridLookUpEditInsumo.GetSelectedDataRow() as DataRowView).Row.ItemArray[1].ToString();

                    string cantidadUnidadesCompradas = (float.Parse(txtCantidad.Text) * float.Parse(CommonUtils.ConexionBD.EjecutarConsultaEspecial("select Cantidad from Insumo where InsumoId=" + idInsumo))).ToString();

                    string sql = "insert into Compras (FechaCompraText,FechaCompra,Cantidad,CostoUnidad,Total,InsumoId,Descripcion,CantidadUnidadesCompradas,EsModificable) values('" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss.fff") + "','" + DateTime.Now + "','" + txtCantidad.Text + "','" + txtCostoUnidad.Text + "','" + txtCostoTotal.Text + "','" + idInsumo + "','" + txtDescripcion.Text + "','" + cantidadUnidadesCompradas + "','True')";

                    //we add cantidad to the current amount of an specific insumo
                    string sqlUpdate = " UPDATE Insumo SET CantidadInsumoEnIventario = (" + txtCantidad.Text + "*(SELECT Cantidad from Insumo where InsumoId=" + idInsumo + ") ) +(SELECT CantidadInsumoEnIventario FROM Insumo AS Insumo_1 WHERE (InsumoId = " + idInsumo + ")) WHERE (InsumoId =" + idInsumo + ")";
                    CommonUtils.ConexionBD.Actualizar(sqlUpdate);
                    //   CommonUtils.ConexionBD.Actualizar(sql);
                    object[] row = new object[8];
                    row[0] = CommonUtils.ConexionBD.ActualizarRetornandoId(sql);
                    row[1] = DateTime.Now;
                    row[2] = txtCantidad.Text;
                    row[3] = txtCostoUnidad.Text;
                    row[4] = txtCostoTotal.Text;
                    row[5] = txtDescripcion.Text;
                    row[7] = idInsumo;
                    actualizarGrid();
                    insertCaja(row);
                    
                  MessageBarValue="El insumo " + gridLookUpEditInsumo.Text +
                                      " fue creado (a) satisfactoriamente.";
                    refreshAddRow();
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message, ex);
                    MessageBarValue = "No se pudo Comprar el insumo. Hubo un error: " + ex.Message;   
                }
                finally
                {
                    alertControlCompasInsumos.Show(this.FindForm(), "Compra insumo.",MessageBarValue , Image);
                }
            }
            else
            {
                GetErrorProviderMessages();
            }          
                    
           // insertCaja();
        }

        private void GetErrorProviderMessages()
        {
            IList<Control> controlErrors = dxErrorProviderCompras.GetControlsWithError();
            //    controlErrors.OrderBy<>;
            alertControlCompasInsumos.Show( this.FindForm( ) , "Falló al guardar." , "Los cambios NO se guardaron.  Por favor, intente nuevamente." , Image );
            MessageBox.Show(this, (string)controlErrors[0].Tag, dxErrorProviderCompras.GetError(controlErrors[0]), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void refreshAddRow()
        {
            gridLookUpEditInsumo.EditValue = "";
            txtCantidad.Text = "";
            txtCostoTotal.Text = "";
            txtCostoUnidad.Text = "";
            txtDescripcion.Text = "";
            txtLote.Text = "";
            txtUnidad.Text = "";
            gridLookUpEditInsumo.Text = "";
            ValidateEmptyStringRule(gridLookUpEditInsumo);
            ValidateEmptyStringRule(txtCantidad);
            ValidateEmptyStringRule(txtCostoUnidad);
        }

        private void gridViewComprasInsumos_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DevExpress.XtraGrid.Views.Base.ColumnView view = sender as DevExpress.XtraGrid.Views.Base.ColumnView;
           

            DataRow row;
            row = gridViewComprasInsumos.GetDataRow(e.RowHandle);

            if (row == null)//|| row[0].ToString() == string.Empty)
            {                
                return;
            }
            else if (row[0].ToString() == string.Empty)
            {
                //
                return;

            }

            if ((e.Value.ToString().Trim() == string.Empty) && gridViewComprasInsumos.FocusedColumn.FieldName != "Descripcion")
            {
                ribbonControl.Enabled = false;
                return;
            }
            else
            {
                ribbonControl.Enabled = true;
            }
            dataHasChanged = true;

            object[] array = new object[5];
            array[0] = row[0].ToString();//id
            array[1] = row[7].ToString();//insumo id
            array[2] = int.Parse(row[2].ToString()).ToString();//cantidad
            array[3] = row[3].ToString();//costo unidad
            array[4] = row[5].ToString();//descripcion

            if (gridViewComprasInsumos.FocusedColumn.FieldName == "Cantidad")
            {
                if (e.Value.ToString() != string.Empty)
                {
                    string a = e.Value.ToString().Replace(".", "");

                    array[2] = int.Parse(a);
                    if (row[3].ToString() != string.Empty)
                    {
                        string costoTotal = (float.Parse(a) * float.Parse(row[3].ToString())).ToString();
                        gridViewComprasInsumos.SetRowCellValue(gridViewComprasInsumos.FocusedRowHandle, gridViewComprasInsumos.Columns[4], costoTotal);
                    }
                }
            }
            else if (gridViewComprasInsumos.FocusedColumn.FieldName == "CostoUnidad")
            {
                array[3] = e.Value.ToString();
                if (row[2].ToString() != string.Empty && e.Value.ToString()!=string.Empty)
                {
                    string costoTotal = (float.Parse(e.Value.ToString()) * float.Parse(row[2].ToString())).ToString();
                    gridViewComprasInsumos.SetRowCellValue(gridViewComprasInsumos.FocusedRowHandle, gridViewComprasInsumos.Columns[4], costoTotal);
                }
            }
            else if (gridViewComprasInsumos.FocusedColumn.FieldName == "InsumoId")
            {
                array[1] = e.Value.ToString();
                gridViewComprasInsumos.SetRowCellValue(gridViewComprasInsumos.FocusedRowHandle, gridViewComprasInsumos.Columns[7], CommonUtils.ConexionBD.EjecutarConsultaEspecial("select Cantidad from Insumo where InsumoId='" + e.Value.ToString() + "' "));
                gridViewComprasInsumos.SetRowCellValue(gridViewComprasInsumos.FocusedRowHandle, gridViewComprasInsumos.Columns[8], CommonUtils.ConexionBD.EjecutarConsultaEspecial("select Unidad from Insumo where InsumoId='" + e.Value.ToString() + "' "));

            }
            else if (gridViewComprasInsumos.FocusedColumn.FieldName == "Descripcion")
            {
                array[4] = e.Value.ToString();
            }

            if (dictionaryOfRows.ContainsKey(row[0].ToString()))
            {
                dictionaryOfRows.Remove(row[0].ToString());
            }
            dictionaryOfRows.Add
                (row[0].ToString(), array);    

        }

        private void gridControlComprasInsumos_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                /* DataRow r = gridViewComprarlista.GetDataRow(gridViewComprarlista.FocusedRowHandle);
                 if (dictionaryOfRows.ContainsKey(r[0].ToString()))
                 {
                     dictionaryOfRows.Remove(r[0].ToString());
                     object[] array = new object[5];                    
                     array[0] = r[0].ToString();//id
                     array[1] = r[7].ToString();//insumo id
                     array[2] = int.Parse(r[2].ToString()).ToString();//cantidad
                     array[3] = r[3].ToString();//costo unidad
                     array[4] = r[5].ToString();//descripcion
                     dictionaryOfRows.Add(r[0].ToString(), array);
                    
                 }*/
                ribbonControl.Enabled = true;
            }
        }

        private void txtCostoUnidad_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (e.NewValue.ToString() == string.Empty || txtCantidad.Text == string.Empty)
            {
                return;
            }
            txtCostoTotal.Text = (float.Parse(e.NewValue.ToString()) * float.Parse(txtCantidad.Text)).ToString();

        }

        private void txtCantidad_Validating(object sender, CancelEventArgs e)
        {           
            ValidateEmptyStringRule(sender as BaseEdit);
        }

        private void ValidateEmptyStringRule(BaseEdit control)
        {
            if (control.EditValue == null || control.EditValue.ToString().Trim().Length == 0)
                dxErrorProviderCompras.SetError(control, "Este campo no puede ser nulo", ErrorType.Critical);
            else
                dxErrorProviderCompras.SetError(control, "");
        }

        private void txtCostoUnidad_Validating(object sender, CancelEventArgs e)
        {
            ValidateEmptyStringRule(sender as BaseEdit);
        }

        private void gridLookUpEditInsumo_Validating(object sender, CancelEventArgs e)
        {
            ValidateEmptyStringRule(sender as BaseEdit);
        }

        private void barButtonDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataRow rowSelected = gridViewComprasInsumos.GetDataRow(gridViewComprasInsumos.FocusedRowHandle);
            if (rowSelected == null || rowSelected[0].ToString() == string.Empty)
            {
                MessageBox.Show(this, "Seleccione una fila", "Compras Insumos", MessageBoxButtons.OK);
                return;
            }
            string sqlQuery = "SELECT EsModificable from Compras where ComprasId=" + rowSelected[0].ToString();//"DELETE FROM ComprasEspeciales WHERE ComprasEspecialesId="+rowSelected[0].ToString();
            bool esModificable = Boolean.Parse(CommonUtils.ConexionBD.EjecutarConsultaEspecial(sqlQuery));
            if (!esModificable)
            {
                MessageBox.Show(this, "No se puede borrar el registro ya que no es modificable", "Compras Insumos", MessageBoxButtons.OK);
                return;
            }
          /*  sqlQuery = "SELECT FechaCompra from Compras where ComprasId=" + rowSelected[0].ToString();
            DateTime dateTimeBD = DateTime.Parse(CommonUtils.ConexionBD.EjecutarConsultaEspecial(sqlQuery));
            if (DateTime.Now.Date > dateTimeBD.Date)
            {
                MessageBox.Show(this, "No se puede borrar el registro debido a la fecha", "Compras Insumos", MessageBoxButtons.OK);
                return;
            }*/
            if (MessageBox.Show(this, "Desea borrar la compra?", "Compras Insumos", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            else
            {
                deleteValueCaja(rowSelected);
                sqlQuery="update insumo set CantidadInsumoEnIventario= ("+
                          "(Select CantidadInsumoEnIventario FROM Insumo WHere(InsumoId = '" + rowSelected[7].ToString() + "'))-" +
                          "(SELECT CantidadUnidadesCompradas FROM Compras WHERE (ComprasId = " + rowSelected[0].ToString() + ")) )" +
                            "WHERE (InsumoId =" + rowSelected[7].ToString() + ")";
                CommonUtils.ConexionBD.Actualizar(sqlQuery);
                sqlQuery = "DELETE FROM Compras WHERE ComprasId=" + rowSelected[0].ToString();
                CommonUtils.ConexionBD.Actualizar(sqlQuery);
                gridViewComprasInsumos.DeleteRow(gridViewComprasInsumos.FocusedRowHandle);
            }

        }

        private void barButtonPdf_ItemClick( object sender , DevExpress.XtraBars.ItemClickEventArgs e )
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog( );
            saveFileDialog.InitialDirectory = Environment.GetFolderPath( Environment.SpecialFolder.MyDocuments );
            saveFileDialog.Filter = "Archivo PDF (*.pdf)|*.pdf";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.FileName = "Insumos.pdf";
            saveFileDialog.Title = "Guardar como";
            saveFileDialog.RestoreDirectory = true;

            if ( saveFileDialog.ShowDialog( ) == DialogResult.OK || saveFileDialog.ShowDialog( ) == DialogResult.Yes )
            {
                try
                {
                    StartExport( );

                    Cursor currentCursor = Cursor.Current;
                    Cursor.Current = Cursors.WaitCursor;
                    DevExpress.XtraPrinting.IPrintingSystem ps = DevExpress.XtraPrinting.PrintHelper.GetCurrentPS( );
                    ps.AfterChange += new DevExpress.XtraPrinting.ChangeEventHandler( Export_ProgressEx );
                    gridControlComprasInsumos.ExportToPdf( saveFileDialog.FileName );
                    ps.AfterChange -= new DevExpress.XtraPrinting.ChangeEventHandler( Export_ProgressEx );
                    Cursor.Current = currentCursor;
                    
                    EndExport( );

                    Process.Start( saveFileDialog.FileName );
                    MessageBarValue = string.Empty;
                }
                catch ( Exception ex )
                {
                    log.Error( ex.Message , ex );
                    MessageBarValue = " No se pudo guardar a la ubicación. " + ex.Message;
                }
            }
        }

        private void barButtonXlsx_ItemClick( object sender , DevExpress.XtraBars.ItemClickEventArgs e )
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog( );
            saveFileDialog.InitialDirectory = Environment.GetFolderPath( Environment.SpecialFolder.MyDocuments );
            saveFileDialog.Filter = "Archivo XLSX (*.xlsx)|*.xlsx";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.FileName = "Insumos.xlsx";
            saveFileDialog.Title = "Guardar como";
            saveFileDialog.RestoreDirectory = true;

            if ( saveFileDialog.ShowDialog( ) == DialogResult.OK )
            {
                try
                {
                    StartExport( );

                    Cursor currentCursor = Cursor.Current;
                    Cursor.Current = Cursors.WaitCursor;
                    DevExpress.XtraPrinting.IPrintingSystem ps = DevExpress.XtraPrinting.PrintHelper.GetCurrentPS( );
                    ps.AfterChange += new DevExpress.XtraPrinting.ChangeEventHandler( Export_ProgressEx );

                    gridControlComprasInsumos.ExportToXlsx( saveFileDialog.FileName );

                    ps.AfterChange -= new DevExpress.XtraPrinting.ChangeEventHandler( Export_ProgressEx );
                    Cursor.Current = currentCursor;

                    EndExport( );

                    Process.Start( saveFileDialog.FileName );
                    MessageBarValue = string.Empty;
                }
                catch ( Exception ex )
                {
                    log.Error( ex.Message , ex );
                    MessageBarValue = " No se pudo guardar a la ubicación. " + ex.Message;
                }
            }
        }

        private void barButtonXls_ItemClick( object sender , DevExpress.XtraBars.ItemClickEventArgs e )
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog( );
            saveFileDialog.InitialDirectory = Environment.GetFolderPath( Environment.SpecialFolder.MyDocuments );
            saveFileDialog.Filter = "Archivo XLS (*.xls)|*.xls";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.FileName = "Insumos.xls";
            saveFileDialog.Title = "Guardar como";
            saveFileDialog.RestoreDirectory = true;

            if ( saveFileDialog.ShowDialog( ) == DialogResult.OK )
            {
                try
                {
                    StartExport( );

                    Cursor currentCursor = Cursor.Current;
                    Cursor.Current = Cursors.WaitCursor;
                    DevExpress.XtraPrinting.IPrintingSystem ps = DevExpress.XtraPrinting.PrintHelper.GetCurrentPS( );
                    ps.AfterChange += new DevExpress.XtraPrinting.ChangeEventHandler( Export_ProgressEx );

                    gridControlComprasInsumos.ExportToXls( saveFileDialog.FileName );

                    ps.AfterChange -= new DevExpress.XtraPrinting.ChangeEventHandler( Export_ProgressEx );
                    Cursor.Current = currentCursor;

                    EndExport( );

                    Process.Start( saveFileDialog.FileName );
                    MessageBarValue = string.Empty;
                }
                catch ( Exception ex )
                {
                    log.Error( ex.Message , ex );
                    MessageBarValue = " No se pudo guardar a la ubicación. " + ex.Message;
                }
            }
        }

        protected virtual void StartExport( )
        {
            if ( this.ParentForm != null )
                this.ParentForm.Update( );

            progressForm = new ProgressForm( this.ParentForm );
            progressForm.Show( );
            progressForm.Refresh( );
        }

        protected virtual void EndExport( )
        {
            progressForm.Dispose( );
            progressForm = null;
        }

        protected virtual void Export_ProgressEx( object sender , DevExpress.XtraPrinting.ChangeEventArgs e )
        {
            if ( e.EventName == DevExpress.XtraPrinting.SR.ProgressPositionChanged )
            {
                int pos = ( int ) e.ValueOf( DevExpress.XtraPrinting.SR.ProgressPosition );
                progressForm.SetProgressValue( pos );
            }
        }
    }
}
