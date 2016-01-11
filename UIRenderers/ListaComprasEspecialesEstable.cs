using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraEditors;

namespace UIRenderers
{
    public partial class ListaComprasEspecialesEstable : XtraForm, CommonUtils.ICajaTest
    {
        bool dataHasChanged;
        private static log4net.ILog log = log4net.LogManager.GetLogger("PnlListaComprasInsumos");
        public string MessageBarValue { get; set; }
        private Dictionary<string, object[]> dictionaryOfRows = new Dictionary<string, object[]>();
        public ListaComprasEspecialesEstable(Form parentForm)
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

        private void refreshGrid()
        {
            //muestra compras de una semana 
            string sql = "DECLARE @FromDate datetime, @ToDate datetime SET @FromDate = CONVERT(varchar, DATEADD(dd, 1 - DATEPART(dw, GETDATE()), GETDATE()), 7) SET @ToDate = CONVERT(varchar, DATEADD(dd,7 - DATEPART(dw, GETDATE()), GETDATE()), 7) " +
                " select ComprasEspecialesId, Costo,Descripcion,FechaCompra from ComprasEspeciales " +
           "WHERE ( convert(varchar,CONVERT(datetime,FechaCompraEspecialText,21),7)  BETWEEN @FromDate AND @ToDate)" +
           " ORDER BY ComprasEspeciales.FechaCompra DESC ";
            gridControlListaComprasEspeciales.DataSource = null;
            gridControlListaComprasEspeciales.DataSource = CommonUtils.ConexionBD.EjecutarConsulta(sql);
            gridViewListaComprasEspeciales.Columns[0].FieldName = "ComprasEspecialesId";
            gridViewListaComprasEspeciales.Columns[1].FieldName = "Costo";
            gridViewListaComprasEspeciales.Columns[2].FieldName = "Descripcion";
            gridViewListaComprasEspeciales.Columns[3].FieldName = "FechaCompra";
          
        }

        private Image Image
        {
            get
            {
                return imageCollectionCompasEspeciales.Images[0];
            }
        }

        #region Validation
        private void GetErrorProviderMessages()
        {
            IList<Control> controlErrors = dxErrorProviderComprasEspeciales.GetControlsWithError();
            //    controlErrors.OrderBy<>;
          //  (this.ParentForm as mainForm).MessageBar.Show();
           // (this.ParentForm as mainForm).MessageBar.Items[0].Text = "  Falló al guardar.  Los cambios NO se guardaron.  Por favor, intente nuevamente.";
            MessageBox.Show(this, (string)controlErrors[0].Tag, dxErrorProviderComprasEspeciales.GetError(controlErrors[0]), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ValidateEmptyStringRule(BaseEdit control)
        {
            if (control.EditValue == null || control.EditValue.ToString().Trim().Length == 0)
                dxErrorProviderComprasEspeciales.SetError(control, "Este campo no puede ser vacio", ErrorType.Critical);
            else
                dxErrorProviderComprasEspeciales.SetError(control, "");
        }

        private void txtCostos_Validating(object sender, CancelEventArgs e)
        {
            ValidateEmptyStringRule(sender as BaseEdit);
        }
        #endregion


        #region ICaja Members

        public void update(object[] row)
        {
            string newCosto = row[1].ToString();
            string sqlQuery = "Update Caja set DineroSistema=((select DineroSistema from Caja where (CajaId = (SELECT MAX(CajaId) FROM Caja)))-(select " + newCosto + " - Costo from ComprasEspeciales where ComprasEspecialesId=" + row[0].ToString() + "))where (CajaId = (SELECT MAX(CajaId) FROM Caja))";
            CommonUtils.ConexionBD.Actualizar(sqlQuery);
        }

        public void insertCaja(object[] row)
        {
            string costo = row[1].ToString();
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
            string sqlQuery = "Update Caja set DineroSistema=((select DineroSistema from Caja where (CajaId = (SELECT MAX(CajaId) FROM Caja)))+(select Costo From ComprasEspeciales where ComprasEspecialesId=" + row[0].ToString() + ")) where (CajaId = (SELECT MAX(CajaId) FROM Caja))";
            CommonUtils.ConexionBD.Actualizar(sqlQuery);
        }

        #endregion

        //only deletes a row. If a row is not selected nothing will happend
        private void barButtonDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataRow rowSelected = gridViewListaComprasEspeciales.GetDataRow(gridViewListaComprasEspeciales.FocusedRowHandle);
            if (rowSelected == null || rowSelected[0].ToString() == string.Empty)
            {
                MessageBox.Show(this, "Seleccione una fila", "Compras especiales", MessageBoxButtons.OK);
                return;
            }
            string sqlQuery = "SELECT EsModificable from ComprasEspeciales where ComprasEspecialesId=" + rowSelected[0].ToString();//"DELETE FROM ComprasEspeciales WHERE ComprasEspecialesId="+rowSelected[0].ToString();
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

        private void barButtonEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                foreach (object[] obj in dictionaryOfRows.Values)
                {
                    update(obj);
                    manageUpdateFeature(obj);
                }
                refreshGrid();
                dictionaryOfRows.Clear();
                dataHasChanged = false;
                MessageBarValue="Compras especiales fueron modificadas satisfactoriamente.";
                

            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                MessageBarValue = "No se pudo modificar Compra Especial. Hubo un error: " + ex.Message;

            }
            finally
            {
                alertControlComprasEspeciales.Show(this.FindForm(), "Cambios compras especiales", MessageBarValue, Image);
            }
        }
        private void btnAnadir_Click(object sender, EventArgs e)
        {
            if (!dxErrorProviderComprasEspeciales.HasErrors)
            {
                try
                {
                    string costos = txtCostos.Text;
                    string descripcion = textEditDescripcion.Text;

                    //  cmd.Connection = CommonUtils.ConexionBD.getConnection();
                    string sqlQuery = "INSERT INTO ComprasEspeciales(FechaCompraEspecialText,Costo,Descripcion,FechaCompra,EsModificable) values('" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss.fff") + "','" + costos + "','" + descripcion + "','" + DateTime.Now + "','True')";//"insert into Compras (FechaCompra,Cantidad,CostoUnidad,Total,InsumoId,Descripcion,CantidadUnidadesCompradas,EsModificable) values('" + DateTime.Now + "','" + cantidad + "','" + costoUnidad + "','" + costoTotal + "','" + idInsumo + "','" + descripcion + "','" + cantidadUnidadesCompradas + "','True')";
                    object[] row = new object[4];
                    row[0] = CommonUtils.ConexionBD.ActualizarRetornandoId(sqlQuery);
                    row[1] = txtCostos.Text;
                    row[2] = textEditDescripcion.Text;
                    row[3] = DateTime.Now;
                    refreshGrid();
                    gridViewListaComprasEspeciales.RefreshRow(gridViewListaComprasEspeciales.RowCount - 1);
                    insertCaja(row);
                    MessageBarValue ="Compra especial fue creada satisfactoriamente.";                   
                    refreshAddRow();
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message, ex);
                    MessageBarValue = "No se pudo realizar Compra Especial. Hubo un error: " + ex.Message;
                }
                finally
                {
                    alertControlComprasEspeciales.Show(this.FindForm(), "Realizar compra", MessageBarValue, Image);
                }
            }
            else
            {
                GetErrorProviderMessages();
            }
        }

        private void refreshAddRow()
        {
            txtCostos.Text = "";
            textEditDescripcion.Text = "";
            ValidateEmptyStringRule(txtCostos);
        }
        // handler used in order to validate a change on a cell
        private void gridViewListaComprasEspeciales_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
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

        private void gridControlListaComprasEspeciales_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                ribbonControl.Enabled = true;
            }
        }               

        private void manageUpdateFeature(object[] dr)
        {
            string sqlQuery = "Update ComprasEspeciales set Costo='" + dr[1].ToString() + "', Descripcion='" + dr[2].ToString() + "' where ComprasEspecialesId=" + dr[0].ToString();
            CommonUtils.ConexionBD.Actualizar(sqlQuery);
        }

        private void ListaComprasEspecialesEstable_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dataHasChanged)
            {
                if (MessageBox.Show(this, "Existen cambios en la tabla, desea salir sin guardar los cambios? ", "Compras Especiales", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void barButtonUp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            barButtonDown.Enabled = true;

            if (gridViewListaComprasEspeciales.FocusedRowHandle > 1)
                gridViewListaComprasEspeciales.MovePrev();
            else
            {
                gridViewListaComprasEspeciales.MovePrev();
                barButtonUp.Enabled = false;
            }
        }

        private void barButtonDown_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            barButtonUp.Enabled = true;

            if (gridViewListaComprasEspeciales.FocusedRowHandle + 1 < gridViewListaComprasEspeciales.RowCount - 1)
                gridViewListaComprasEspeciales.MoveNext();
            else
            {
                gridViewListaComprasEspeciales.MoveNext();
                barButtonDown.Enabled = false;
            }
        }
    }
}
