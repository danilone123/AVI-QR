using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using DevExpress.XtraEditors;

namespace UIRenderers
{
    public partial class ListaComprasEspeciales : XtraForm,CommonUtils.ICaja
    {
        private Dictionary<string, object[]> dictionaryOfRows = new Dictionary<string, object[]>();
        bool dataHasChanged;
        public ListaComprasEspeciales(Form parentForm)
        {
            InitializeComponent();        
            InitConexionDB();
            refreshGrid();
            if (parentForm != null)
            {                
                Left = parentForm.Left + (parentForm.Width - Width) / 2;
                Top = parentForm.Top + (parentForm.Height - Height) / 2;
            }
        }
              
        private void InitConexionDB()
        {
            CommonUtils.ConexionBD.StringDeConexion = CommonUtils.ConexionBD.GetConnectionString(); //GetConnectionString();
        }

        private static string GetConnectionString()
        {
            return @"Data Source=Marcelo-PC\SQLEXPRESS;Initial Catalog=Capresso;Integrated Security=SSPI;";
        }

        private void refreshGrid()
        {           
            //muestra compras de una semana 
            string sql = "select ComprasEspecialesId, Costo,Descripcion,FechaCompra from ComprasEspeciales "+
            "WHERE (ComprasEspeciales.FechaCompra BETWEEN CONVERT(varchar, DATEADD(dd, 1 - DATEPART(dw, GETDATE()), GETDATE()), 7) AND CONVERT(varchar, DATEADD(dd," +
            " 7 - DATEPART(dw, GETDATE()), GETDATE()), 7))" +
            " ORDER BY ComprasEspeciales.FechaCompra DESC ";
            gridControlListaComprasEspeciales.DataSource = null;
            gridControlListaComprasEspeciales.DataSource = CommonUtils.ConexionBD.EjecutarConsulta(sql);
            gridViewListaComprasEspeciales.Columns[0].FieldName = "ComprasEspecialesId";
            gridViewListaComprasEspeciales.Columns[1].FieldName = "Costo";
            gridViewListaComprasEspeciales.Columns[2].FieldName = "Descripcion";
            gridViewListaComprasEspeciales.Columns[3].FieldName = "FechaCompra";
            gridViewListaComprasEspeciales.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(gridViewListaComprasEspeciales_ValidateRow);
            gridViewListaComprasEspeciales.ValidatingEditor += new DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventHandler(gridViewListaComprasEspeciales_ValidatingEditor);
            gridViewListaComprasEspeciales.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(gridViewListaComprasEspeciales_RowUpdated);
        }

        void gridViewListaComprasEspeciales_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
                
        }

        // handler used in order to validate a change on a cell
        void gridViewListaComprasEspeciales_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            DataRow rowSelected = gridViewListaComprasEspeciales.GetDataRow(gridViewListaComprasEspeciales.FocusedRowHandle);
            if (rowSelected == null)
            {
                return;
            }
            if (gridViewListaComprasEspeciales.FocusedColumn.FieldName == "Costo")
            {
                if (e.Value == null || e.Value.ToString().Trim().Length == 0)
                {
                    e.Valid = false;
                    e.ErrorText = "Columna Costos no puede estar vacia";
                }
            }
        }

        //handler used in order to validate a new row
        void gridViewListaComprasEspeciales_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {

            DevExpress.XtraGrid.Views.Base.ColumnView view = sender as DevExpress.XtraGrid.Views.Base.ColumnView;
            DevExpress.XtraGrid.Columns.GridColumn costosColumn = view.Columns[1];
            if (view.GetRowCellValue(e.RowHandle, costosColumn).ToString() == null || view.GetRowCellValue(e.RowHandle, costosColumn).ToString().Trim().Length == 0)
            {
                e.Valid = false;
                view.SetColumnError(costosColumn, "Columna Costos no puede estar vacia", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical);
            }
        }

        private void gridViewListaComprasEspeciales_RowCountChanged(object sender, EventArgs e)
        {
             DataRow row = gridViewListaComprasEspeciales.GetDataRow(gridViewListaComprasEspeciales.RowCount - 1);

             if (row == null || row[0].ToString() != string.Empty)
             {
                   return;
             }
             string costos = row[1].ToString();
             string descripcion = row[2].ToString();       
             SqlCommand cmd = new SqlCommand();
             cmd.Connection = CommonUtils.ConexionBD.getConnection();
             string sqlQuery = "INSERT INTO ComprasEspeciales(Costo,Descripcion,FechaCompra,EsModificable) values('"+costos+"','"+descripcion+"','"+DateTime.Now+"','True')";//"insert into Compras (FechaCompra,Cantidad,CostoUnidad,Total,InsumoId,Descripcion,CantidadUnidadesCompradas,EsModificable) values('" + DateTime.Now + "','" + cantidad + "','" + costoUnidad + "','" + costoTotal + "','" + idInsumo + "','" + descripcion + "','" + cantidadUnidadesCompradas + "','True')";
             row[0] = CommonUtils.ConexionBD.ActualizarRetornandoId(sqlQuery);
             row[3] = DateTime.Now;               
             gridViewListaComprasEspeciales.RefreshRow(gridViewListaComprasEspeciales.RowCount - 1);
             insertCaja(row);                                 
        }

        private void manageUpdateFeature(object[] dr)
        {
            string sqlQuery = "Update ComprasEspeciales set Costo='"+dr[1].ToString()+"', Descripcion='"+dr[2].ToString()+"' where ComprasEspecialesId="+dr[0].ToString();
            CommonUtils.ConexionBD.Actualizar(sqlQuery);
        }

        private void barButtonEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            foreach (object[] obj in dictionaryOfRows.Values)
            {
                update(obj);
                manageUpdateFeature(obj);
            }
            refreshGrid();
            dictionaryOfRows.Clear();
            dataHasChanged = false;
        }

        private void gridViewListaComprasEspeciales_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DataRow row;
            row = gridViewListaComprasEspeciales.GetDataRow(e.RowHandle);

            if (row == null || row[0].ToString() == string.Empty)
            {
                return;
            }
            if ((e.Value.ToString().Trim() == string.Empty) && gridViewListaComprasEspeciales.FocusedColumn.FieldName != "Descripcion")
            {
                ribbonControl.Enabled = false;
                return;
            }
            else
            {
                ribbonControl.Enabled = true;
            }
            dataHasChanged = true;
            object[] array = new object[3];
            array[0] = row[0].ToString();//id
            array[1] = row[1].ToString();//costos
            array[2] = row[2].ToString();//descripcion
           
            if (gridViewListaComprasEspeciales.FocusedColumn.FieldName == "Costo")
            {
                array[1] = e.Value.ToString();
            }
            else if (gridViewListaComprasEspeciales.FocusedColumn.FieldName == "Descripcion")
            {
                array[2] = e.Value.ToString();
            }      

            if (dictionaryOfRows.ContainsKey(row[0].ToString()))
            {
                dictionaryOfRows.Remove(row[0].ToString());
            }
            dictionaryOfRows.Add
                (row[0].ToString(), array);
        }
        
        //event called when a end-user clicks the view
        private void gridControlListaComprasEspeciales_ProcessGridKey(object sender, KeyEventArgs e)
        {
           if (e.KeyCode == Keys.Escape)
            {
                /*DataRow r = gridViewListaComprasEspeciales.GetDataRow(gridViewListaComprasEspeciales.FocusedRowHandle);
                if (dictionaryOfRows.ContainsKey(r[0].ToString()))
                {
                    dictionaryOfRows.Remove(r[0].ToString());
                    object[] array = new object[3];
                    array[0] = r[0].ToString();
                    array[1] = r[1].ToString();
                    array[2] = r[2].ToString();
                    dictionaryOfRows.Add(r[0].ToString(),array);
                }*/
                ribbonControl.Enabled = true;
            }
        }

        //only deletes a row. If a row is not selected nothing will happend
        private void barButtonDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataRow rowSelected = gridViewListaComprasEspeciales.GetDataRow(gridViewListaComprasEspeciales.FocusedRowHandle);
            if (rowSelected == null || rowSelected[0].ToString() == string.Empty )
            {
                MessageBox.Show(this, "Seleccione una fila", "Compras especiales", MessageBoxButtons.OK);
                return;
            } 
            string sqlQuery ="SELECT EsModificable from ComprasEspeciales where ComprasEspecialesId="+rowSelected[0].ToString();//"DELETE FROM ComprasEspeciales WHERE ComprasEspecialesId="+rowSelected[0].ToString();
            bool esModificable = Boolean.Parse(CommonUtils.ConexionBD.EjecutarConsultaEspecial(sqlQuery));
            if (!esModificable)
            {
                MessageBox.Show(this, "No se puede borrar el registro ya que no es modificable", "Compras especiales", MessageBoxButtons.OK);
                return; 
            }
          /*  sqlQuery = "SELECT FechaCompra from ComprasEspeciales where ComprasEspecialesId="+rowSelected[0].ToString();
            DateTime dateTimeBD =DateTime.Parse( CommonUtils.ConexionBD.EjecutarConsultaEspecial(sqlQuery));
            if (DateTime.Now.Date > dateTimeBD.Date)
            {
                MessageBox.Show(this, "No se puede borrar el registro debido a la fecha", "Compras especiales", MessageBoxButtons.OK);
                return;
            }*/
            if (MessageBox.Show(this, "Desea borrar compra?", "Compras especiales", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            else
            {
                deleteValueCaja(rowSelected);
                sqlQuery = "DELETE FROM ComprasEspeciales WHERE ComprasEspecialesId=" + rowSelected[0].ToString();
                CommonUtils.ConexionBD.Actualizar(sqlQuery);
                gridViewListaComprasEspeciales.DeleteRow(gridViewListaComprasEspeciales.FocusedRowHandle);
            }
        }

        #region ICaja Members

        public void update(object[] row)
        {
            string newCosto = row[1].ToString();
            string sqlQuery = "Update Caja set DineroSistema=((select DineroSistema from Caja where (CajaId = (SELECT MAX(CajaId) FROM Caja)))-(select " + newCosto + " - Costo from ComprasEspeciales where ComprasEspecialesId=" + row[0].ToString() + "))where (CajaId = (SELECT MAX(CajaId) FROM Caja))";
            CommonUtils.ConexionBD.Actualizar(sqlQuery);
        }

        public void insertCaja(DataRow row)
        {
            string costo = row[1].ToString();
            string sqlQuery = "SELECT esModificable FROM Caja WHERE (CajaId = (SELECT MAX(CajaId) FROM Caja))";
            bool esModificable= Boolean.Parse(CommonUtils.ConexionBD.EjecutarConsultaEspecial(sqlQuery));
            if (esModificable)
            {
                sqlQuery = "select DineroReal -" + costo + " from Caja WHERE (CajaId = (SELECT MAX(CajaId) FROM Caja))";
                string dineroActualCaja = CommonUtils.ConexionBD.EjecutarConsultaEspecial(sqlQuery);
                sqlQuery = "INSERT INTO Caja(DineroSistema,DineroReal,Diferencia,Descripcion,Fecha,esModificable) VALUES(" + dineroActualCaja + ",0,0,'nuevo valor','" + DateTime.Now + "','False')";
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
                    sqlQuery = "UPDATE Caja set Fecha='" + DateTime.Now + "', DineroSistema="+ dineroSistema+" WHERE (CajaId =  (SELECT  MAX(CajaId) AS Expr1  FROM  Caja AS Caja_1))";
                    CommonUtils.ConexionBD.Actualizar(sqlQuery);
                }
                else
                {
                    sqlQuery = "select DineroSistema -" + costo + " from Caja WHERE (CajaId = (SELECT MAX(CajaId) FROM Caja))";
                    string dineroActualCaja = CommonUtils.ConexionBD.EjecutarConsultaEspecial(sqlQuery);
                    sqlQuery = sqlQuery = "INSERT INTO Caja(DineroSistema,DineroReal,Diferencia,Descripcion,Fecha,esModificable) VALUES(" + dineroActualCaja + ",0,0,'nuevo valor','" + DateTime.Now + "','False')";
                    CommonUtils.ConexionBD.Actualizar(sqlQuery);
                }
            }
        }

        public void deleteValueCaja(DataRow row)
        {
            string sqlQuery = "Update Caja set DineroSistema=((select DineroSistema from Caja where (CajaId = (SELECT MAX(CajaId) FROM Caja)))+(select Costo From ComprasEspeciales where ComprasEspecialesId=" + row[0].ToString() + ")) where (CajaId = (SELECT MAX(CajaId) FROM Caja))";
            CommonUtils.ConexionBD.Actualizar(sqlQuery);
        }

        #endregion

        private void gridViewListaComprasEspeciales_ShowingEditor(object sender, CancelEventArgs e)
        {
            DataRow r = gridViewListaComprasEspeciales.GetDataRow(gridViewListaComprasEspeciales.FocusedRowHandle);
            if (r == null || r[0].ToString() == string.Empty)
            {
                return;
            }
            string sqlQuery = "select EsModificable from ComprasEspeciales where ComprasEspecialesId=" + r[0].ToString();
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

        private void ListaComprasEspeciales_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dataHasChanged)
            {
                if (MessageBox.Show(this, "Existen cambios en la tabla, desea salir sin guardar los cambios? ", "Compras Especiales", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
