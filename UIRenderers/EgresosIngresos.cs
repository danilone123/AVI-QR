using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using CommonUtils;

namespace UIRenderers
{
    public partial class EgresosIngresos : DevExpress.XtraEditors.XtraUserControl, CommonUtils.ICajaTest,CommonUtils.ActualizarGrids
    {
        private Dictionary<string, object[]> dictionaryOfRows = new Dictionary<string, object[]>();
        bool dataHasChanged ;
        private static log4net.ILog log = log4net.LogManager.GetLogger("EgresosIngresos");

        public string MessageBarValue { get; set; }

        public EgresosIngresos()
        {
            InitializeComponent();
            InitConexionDB();
            RefreshGridIngresosEgresos();
            dataHasChanged = false;
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
            CommonUtils.ConexionBD.StringDeConexion = CommonUtils.ConexionBD.GetConnectionString();//GetConnectionString();
        }

        private void RefreshGridIngresosEgresos()
        {
            string sql = "select IngresosEgresosId,Monto,Concepto, (cast(FechaText as date)) AS Fecha from IngresosEgresos Order by cast (FechaText as Date) Desc";
            gridControlIngresosEgresos.DataSource = null;
            gridControlIngresosEgresos.DataSource = CommonUtils.ConexionBD.EjecutarConsulta(sql);
            gridViewIngresosEgresos.Columns[0].FieldName = "IngresosEgresosId";
            gridViewIngresosEgresos.Columns[1].FieldName = "Monto";
            gridViewIngresosEgresos.Columns[2].FieldName = "Concepto";
            gridViewIngresosEgresos.Columns[3].FieldName = "Fecha";
            gridViewIngresosEgresos.ValidatingEditor += new DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventHandler(gridViewIngresosEgresos_ValidatingEditor);           
        }

        void gridViewIngresosEgresos_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            DataRow rowSelected = gridViewIngresosEgresos.GetDataRow(gridViewIngresosEgresos.FocusedRowHandle);
            if (rowSelected == null)
            {
                return;
            }
            if (gridViewIngresosEgresos.FocusedColumn.FieldName == "Monto")
            {
                if (e.Value == null || e.Value.ToString().Trim().Length == 0)
                {
                    e.Valid = false;
                    e.ErrorText = "Columna Monto no puede estar vacia";
                }
            }
        }

        
        private void ValidateEmptyStringRule(BaseEdit control)
        {
            if (control.EditValue == null || control.EditValue.ToString().Trim().Length == 0)
                dxErrorProviderEgresosingresos.SetError(control, "Este campo no puede ser nulo", ErrorType.Critical);
            else
                dxErrorProviderEgresosingresos.SetError(control, "");
        }

        private void GetErrorProviderMessages()
        {
            IList<Control> controlErrors = dxErrorProviderEgresosingresos.GetControlsWithError();
            //    controlErrors.OrderBy<>;
            alertControl.Show( this.FindForm( ) , "Falló al guardar." , "Los cambios NO se guardaron.  Por favor, intente nuevamente." , GetImage( ) );
            MessageBox.Show(this, (string)controlErrors[0].Tag, dxErrorProviderEgresosingresos.GetError(controlErrors[0]), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private Image GetImage( )
        {
            return imageCollection.Images[ 0 ];
        }

        private void txtMonto_Validating(object sender, CancelEventArgs e)
        {
            ValidateEmptyStringRule(sender as BaseEdit);
        }

        private void btnAnadir_Click(object sender, EventArgs e)
        {
            if (!dxErrorProviderEgresosingresos.HasErrors)
            {
                try
                {
                    string sql = "INSERT INTO IngresosEgresos(FechaText,Monto,Concepto,Fecha,EsModificable) values('" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss.fff") + "','" + txtMonto.Text + "','" + StringUtils.EscapeSQL(txtConcepto.Text) + "','" + DateTime.Now + "','True')";
                    object[] row = new object[4];
                    row[0] = CommonUtils.ConexionBD.ActualizarRetornandoId(sql);
                    row[1] = txtMonto.Text;
                    row[2] = txtConcepto.Text;
                    row[3] = DateTime.Now;
                    actualizarGrid();
                    insertCaja(row);
                    refreshTextFields();
                    MessageBarValue = "Ingresos/Egresos creados satisfactoriamente.";

                }
                catch (Exception ex)
                {
                    log.Error(ex.Message, ex);
                    MessageBarValue = "No se pudo realizar el ingreso. Hubo un error: " + ex.Message;
                }
                finally
                {
                    alertControl.Show(this.FindForm(), "Ingresos/Egresos", MessageBarValue, GetImage());                       
                }

            }
            else
            {
                GetErrorProviderMessages();
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
                //refreshGrid();
                dictionaryOfRows.Clear();
                dataHasChanged = false;
                MessageBarValue = "Cambios modificados correctamente";
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                MessageBarValue = "No se pudo modificar los cambios. Hubo un error: " + ex.Message;
            }
            finally
            {
                alertControl.Show(this.FindForm(), "Ingresos/Egresos ", MessageBarValue, GetImage());  
            }
        }
        //only deletes a row. If a row is not selected nothing will happend
        private void barButtonDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {                    
            DataRow rowSelected = gridViewIngresosEgresos.GetDataRow(gridViewIngresosEgresos.FocusedRowHandle);
            if (rowSelected == null || rowSelected[0].ToString() == string.Empty )
            {
                MessageBox.Show(this, "Seleccione una fila", "Ingresos/Egresos", MessageBoxButtons.OK);
                return;
            } 
            string sqlQuery ="SELECT EsModificable from IngresosEgresos where IngresosEgresosId="+rowSelected[0].ToString();//"DELETE FROM ComprasEspeciales WHERE ComprasEspecialesId="+rowSelected[0].ToString();
            bool esModificable = Boolean.Parse(CommonUtils.ConexionBD.EjecutarConsultaEspecial(sqlQuery));
            if (!esModificable)
            {
                MessageBox.Show(this, "No se puede borrar el registro ya que no es modificable", "Ingresos/Egresos", MessageBoxButtons.OK);
                return; 
            }            
            if (MessageBox.Show(this, "Desea borrar el ingreso/egreso?", "Ingresos/Egresos", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            else
            {
                deleteValueCaja(rowSelected);
                sqlQuery = "DELETE FROM IngresosEgresos WHERE IngresosEgresosId=" + rowSelected[0].ToString();
                CommonUtils.ConexionBD.Actualizar(sqlQuery);
                gridViewIngresosEgresos.DeleteRow(gridViewIngresosEgresos.FocusedRowHandle);
            }
        
        }

        private void refreshTextFields()
        {
            txtMonto.Text = "";
            txtConcepto.Text = "";
            ValidateEmptyStringRule(txtMonto);
        }

        private void manageUpdateFeature(object[] dr)
        {
            string sqlQuery = "Update IngresosEgresos set Monto='" + dr[1].ToString() + "', Concepto='" +StringUtils.EscapeSQL(dr[2].ToString()) + "' where IngresosEgresosId=" + dr[0].ToString();
            CommonUtils.ConexionBD.Actualizar(sqlQuery);
        }

        #region ICajaTest Members

        public void update(object[] row)
        {
            string newCosto = row[1].ToString();
            string sqlQuery = "Update Caja set DineroSistema=((select DineroSistema from Caja where (CajaId = (SELECT MAX(CajaId) FROM Caja)))+(select " + newCosto + " - Monto from IngresosEgresos where IngresosEgresosId=" + row[0].ToString() + "))where (CajaId = (SELECT MAX(CajaId) FROM Caja))";
            CommonUtils.ConexionBD.Actualizar(sqlQuery);
        }

        public void insertCaja(object[] row)
        {
            string costo = row[1].ToString();
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
                    sqlQuery = "UPDATE Caja set FechaText='" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss.fff") + "', Fecha='" + DateTime.Now + "', DineroSistema=" + dineroSistema + " WHERE (CajaId =  (SELECT  MAX(CajaId) AS Expr1  FROM  Caja AS Caja_1))";
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

        public void deleteValueCaja(DataRow row)
        {
            string sqlQuery = "Update Caja set DineroSistema=((select DineroSistema from Caja where (CajaId = (SELECT MAX(CajaId) FROM Caja)))-(select Monto From IngresosEgresos where IngresosEgresosId=" + row[0].ToString() + ")) where (CajaId = (SELECT MAX(CajaId) FROM Caja))";
            CommonUtils.ConexionBD.Actualizar(sqlQuery);
        }

        #endregion

        #region ActualizarGrids Members

        public void actualizarGrid()
        {
            RefreshGridIngresosEgresos();
        }

        #endregion

        private void gridViewIngresosEgresos_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DataRow row;
            row = gridViewIngresosEgresos.GetDataRow(e.RowHandle);

            if (row == null || row[0].ToString() == string.Empty)
            {
                return;
            }
            if ((e.Value.ToString().Trim() == string.Empty) && gridViewIngresosEgresos.FocusedColumn.FieldName != "Concepto")
            {
                ribbonControl.Enabled = false;
                return;
            }
            else
            {
                ribbonControl.Enabled = true;
            }

            //to show a message
            dataHasChanged = true;

            object[] array = new object[3];
            array[0] = row[0].ToString();//id
            array[1] = row[1].ToString();//monto
            array[2] = row[2].ToString();//concepto

            if (gridViewIngresosEgresos.FocusedColumn.FieldName == "Monto")
            {
                array[1] = e.Value.ToString();
            }
            else if (gridViewIngresosEgresos.FocusedColumn.FieldName == "Concepto")
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

        private void gridViewIngresosEgresos_ShowingEditor(object sender, CancelEventArgs e)
        {
            DataRow r = gridViewIngresosEgresos.GetDataRow(gridViewIngresosEgresos.FocusedRowHandle);
            if (r == null || r[0].ToString() == string.Empty)
            {
                return;
            }
            string sqlQuery = "select EsModificable from IngresosEgresos where IngresosEgresosId=" + r[0].ToString();
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

        private void gridControlIngresosEgresos_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
               ribbonControl.Enabled = true;
            }
        }
                    
    }
}
