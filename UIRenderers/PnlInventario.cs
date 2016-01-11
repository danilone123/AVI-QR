using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace UIRenderers
{
    public partial class PnlInventario : DevExpress.XtraEditors.XtraUserControl
    {
        ProgressForm progressForm;
        private Dictionary<string, object[]> dictionaryOfRows = new Dictionary<string, object[]>();
        bool dataHasChanged;
        private static log4net.ILog log = log4net.LogManager.GetLogger("PnlInventario");
        public string MessageBarValue { get; set; }
        public PnlInventario()
        {        
            InitializeComponent();
            InitConexionDB();
            RefreshGrid();
        }
        #region Propiedades
        private Image Image
        {
            get
            {
                return imageCollectionUsers.Images[0];
            }
        }

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

        private void RefreshGrid()
        {
            string sqlQuery = "SELECT InsumoId,Nombre,Presentacion,Marca,Unidad,CantidadInsumoEnIventario FROM Insumo";
            gridControlListaIventarios.DataSource = null;
            gridControlListaIventarios.DataSource = CommonUtils.ConexionBD.EjecutarConsulta(sqlQuery);
            gridViewListaIventarios.Columns[0].FieldName = "InsumoId";
            gridViewListaIventarios.Columns[1].FieldName = "Nombre";
            gridViewListaIventarios.Columns[2].FieldName = "Presentacion";
            gridViewListaIventarios.Columns[3].FieldName = "Marca";
            gridViewListaIventarios.Columns[4].FieldName = "Unidad";
            gridViewListaIventarios.Columns[5].FieldName = "CantidadInsumoEnIventario";

        }

        private void manageUpdateFeature(object[] dr)
        {
            string sqlQuery = "UPDATE Insumo set CantidadInsumoEnIventario=" + dr[1].ToString() + " WHERE InsumoId=" + dr[0].ToString();
            CommonUtils.ConexionBD.Actualizar(sqlQuery);
        }

        private void barButtonEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                foreach (object[] obj in dictionaryOfRows.Values)
                {
                    manageUpdateFeature(obj);
                }
                RefreshGrid();
                dictionaryOfRows.Clear();
                dataHasChanged = false;
                MessageBarValue = "Cambios en inventario realizados satisfactoriamente";
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                MessageBarValue = " No se pudo realizar cambios en inventario hubo un error. " + ex.Message;
            }
            finally
            {
                alertControlInventario.Show(this.FindForm(), "Inventario", MessageBarValue, Image);
            }
        }

        private void barButtonUp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            barButtonDown.Enabled = true;

            if (gridViewListaIventarios.FocusedRowHandle > 1)
                gridViewListaIventarios.MovePrev();
            else
            {
                gridViewListaIventarios.MovePrev();
                barButtonUp.Enabled = false;
            }
        }

        private void barButtonDown_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            barButtonUp.Enabled = true;

            if (gridViewListaIventarios.FocusedRowHandle + 1 < gridViewListaIventarios.RowCount - 1)
                gridViewListaIventarios.MoveNext();
            else
            {
                gridViewListaIventarios.MoveNext();
                barButtonDown.Enabled = false;
            }
        }

        private void gridViewListaIventarios_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DataRow row;
            row = gridViewListaIventarios.GetDataRow(e.RowHandle);

            if (row == null || row[0].ToString() == string.Empty)
            {
                return;
            }
            if ((e.Value.ToString().Trim() == string.Empty))
            {
                ribbonControl.Enabled = false;
                return;
            }
            else
            {
                ribbonControl.Enabled = true;
            }
            dataHasChanged = true;
            object[] array = new object[2];
            array[0] = row[0].ToString();//id
            array[1] = row[5].ToString();//cantidadInsumoEnIventario         

            if (gridViewListaIventarios.FocusedColumn.FieldName == "CantidadInsumoEnIventario")
            {
                array[1] = e.Value.ToString();
            }

            if (dictionaryOfRows.ContainsKey(row[0].ToString()))
            {
                dictionaryOfRows.Remove(row[0].ToString());
            }
            dictionaryOfRows.Add
                (row[0].ToString(), array);
        }

        private void gridControlListaIventarios_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DataRow r = gridViewListaIventarios.GetDataRow(gridViewListaIventarios.FocusedRowHandle);
                if (dictionaryOfRows.ContainsKey(r[0].ToString()))
                {
                    dictionaryOfRows.Remove(r[0].ToString());
                    object[] array = new object[2];
                    array[0] = r[0].ToString();
                    array[1] = r[5].ToString();
                    dictionaryOfRows.Add(r[0].ToString(), array);
                }
                ribbonControl.Enabled = true;
            }
        }

        private void gridViewListaIventarios_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            DataRow rowSelected = gridViewListaIventarios.GetDataRow(gridViewListaIventarios.FocusedRowHandle);
            if (rowSelected == null)
            {
                return;
            }
            if (gridViewListaIventarios.FocusedColumn.FieldName == "CantidadInsumoEnIventario")
            {
                if (e.Value == null || e.Value.ToString().Trim().Length == 0)
                {
                    e.Valid = false;
                    e.ErrorText = "Columna Cantidad en Almacen no puede estar vacia";
                }
            }
        }

        private void barButtonItemUpdate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.RefreshGrid();
        }

        #region export data
        protected virtual void StartExport()
        {
            if (this.ParentForm != null)
                this.ParentForm.Update();

            progressForm = new ProgressForm(this.ParentForm);
            progressForm.Show();
            progressForm.Refresh();
        }
        protected virtual void EndExport()
        {
            progressForm.Dispose();
            progressForm = null;
        }

        protected virtual void Export_ProgressEx(object sender, DevExpress.XtraPrinting.ChangeEventArgs e)
        {
            if (e.EventName == DevExpress.XtraPrinting.SR.ProgressPositionChanged)
            {
                int pos = (int)e.ValueOf(DevExpress.XtraPrinting.SR.ProgressPosition);
                progressForm.SetProgressValue(pos);
            }
        }

        #endregion

        private void barButtonPdf_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog.Filter = "Archivo PDF (*.pdf)|*.pdf";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.FileName = "Inventario.pdf";
            saveFileDialog.Title = "Guardar como";
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StartExport();

                    Cursor currentCursor = Cursor.Current;
                    Cursor.Current = Cursors.WaitCursor;
                    DevExpress.XtraPrinting.IPrintingSystem ps = DevExpress.XtraPrinting.PrintHelper.GetCurrentPS();
                    ps.AfterChange += new DevExpress.XtraPrinting.ChangeEventHandler(Export_ProgressEx);
                    gridControlListaIventarios.ExportToPdf(saveFileDialog.FileName);

                    ps.AfterChange -= new DevExpress.XtraPrinting.ChangeEventHandler(Export_ProgressEx);
                    Cursor.Current = currentCursor;

                    EndExport();

                    System.Diagnostics.Process.Start(saveFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: No se pudo guardar a la ubicación. Original error: " + ex.Message);
                }
            }
        }

        private void barButtonXls_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog.Filter = "Archivo XLS (*.xls)|*.xls";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.FileName = "Inventario.xls";
            saveFileDialog.Title = "Guardar como";
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StartExport();

                    Cursor currentCursor = Cursor.Current;
                    Cursor.Current = Cursors.WaitCursor;
                    DevExpress.XtraPrinting.IPrintingSystem ps = DevExpress.XtraPrinting.PrintHelper.GetCurrentPS();
                    ps.AfterChange += new DevExpress.XtraPrinting.ChangeEventHandler(Export_ProgressEx);

                    gridControlListaIventarios.ExportToXls(saveFileDialog.FileName);

                    ps.AfterChange -= new DevExpress.XtraPrinting.ChangeEventHandler(Export_ProgressEx);
                    Cursor.Current = currentCursor;

                    EndExport();

                    System.Diagnostics.Process.Start(saveFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: No se pudo guardar a la ubicación. Original error: " + ex.Message);
                }
            }


        }

        private void barButtonXlsx_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog.Filter = "Archivo XLSX (*.xlsx)|*.xlsx";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.FileName = "Inventario.xlsx";
            saveFileDialog.Title = "Guardar como";
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StartExport();

                    Cursor currentCursor = Cursor.Current;
                    Cursor.Current = Cursors.WaitCursor;
                    DevExpress.XtraPrinting.IPrintingSystem ps = DevExpress.XtraPrinting.PrintHelper.GetCurrentPS();
                    ps.AfterChange += new DevExpress.XtraPrinting.ChangeEventHandler(Export_ProgressEx);

                    gridControlListaIventarios.ExportToXlsx(saveFileDialog.FileName);

                    ps.AfterChange -= new DevExpress.XtraPrinting.ChangeEventHandler(Export_ProgressEx);
                    Cursor.Current = currentCursor;

                    EndExport();
                    System.Diagnostics.Process.Start(saveFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: No se pudo guardar a la ubicación. Original error: " + ex.Message);
                }
            }
        }
    }
}
