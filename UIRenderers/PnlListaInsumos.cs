using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Collections;

namespace UIRenderers
{
    public partial class PnlListaInsumos : UserControl,CommonUtils.ActualizarGrids
    {
        ProgressForm progressForm;
        private static log4net.ILog log = log4net.LogManager.GetLogger("NuevoInsumo");
        public string MessageBarValue { get; set; }
        public PnlListaInsumos()
        {
            InitializeComponent();
            InitConexionDB();
            InitializeGrid();
        }
        private Image Image
        {
            get
            {
                return imageCollectionUsers.Images[0];
            }
        }

        private void InitializeGrid()
        {
            ArrayList listaInsumos = new ArrayList();
            try
            {
                CommonUtils.ConexionBD.AbrirConexion();
                string sqlData = "SELECT InsumoId,Cantidad, CostoUnidad,Nombre,Presentacion , Marca,Unidad,CostoPresentacion FROM Insumo";
                SqlDataReader reader = CommonUtils.ConexionBD.EjecutarConsultaReader(sqlData);
                if (reader.HasRows)
                {
                    CommonUtils.Insumo insumoAMostrar;
                    while (reader.Read())
                    {
                        insumoAMostrar = new CommonUtils.Insumo();
                        insumoAMostrar.idInsumo = (int)reader.GetDecimal(0);
                        insumoAMostrar.cantidad = (float)reader.GetDouble(1);
                        insumoAMostrar.costoPresentacion = (float)reader.GetDouble(7);
                        insumoAMostrar.nombre = reader.GetString(3);
                        insumoAMostrar.presentacion = reader.GetString(4);
                        insumoAMostrar.marca = reader.GetString(5);
                        insumoAMostrar.unidad = reader.GetString(6);
                        insumoAMostrar.costoUnidad = (float)reader.GetDouble(2);
                        listaInsumos.Add(insumoAMostrar);
                    }
                }
                gridControlListaInsumos.DataSource = null;
                gridControlListaInsumos.DataSource = listaInsumos;
                gridViewListaInsumos.Columns[0].FieldName = "idInsumo";
                gridViewListaInsumos.Columns[1].FieldName = "nombre";
                gridViewListaInsumos.Columns[2].FieldName = "cantidad";
                gridViewListaInsumos.Columns[3].FieldName = "costoUnidad";
                gridViewListaInsumos.Columns[4].FieldName = "costoPresentacion";
                gridViewListaInsumos.Columns[5].FieldName = "unidad";
                gridViewListaInsumos.Columns[6].FieldName = "marca";
                gridViewListaInsumos.Columns[7].FieldName = "presentacion";

                CommonUtils.ConexionBD.CerrarConexion();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                MessageBarValue = "No se pudo mostrar lista de insumos. Hubo un error: " + ex.Message;
                alertControlListaInsumos.Show(this.FindForm(), "Lista Insumos.", MessageBarValue, Image);
            }
            finally
            {
               
            }
        }

        private void InitConexionDB()
        {
            CommonUtils.ConexionBD.StringDeConexion = CommonUtils.ConexionBD.GetConnectionString();//GetConnectionString();
        }
      
        #region events

        private void barButtonUp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            barButtonDown.Enabled = true;

            if (gridViewListaInsumos.FocusedRowHandle > 1)
                gridViewListaInsumos.MovePrev();
            else
            {
                gridViewListaInsumos.MovePrev();
                barButtonUp.Enabled = false;
            }
        }

        private void barButtonDown_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            barButtonUp.Enabled = true;

            if (gridViewListaInsumos.FocusedRowHandle + 1 < gridViewListaInsumos.RowCount - 1)
                gridViewListaInsumos.MoveNext();
            else
            {
                gridViewListaInsumos.MoveNext();
                barButtonDown.Enabled = false;
            }
        }

        private void barButtonEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CommonUtils.Insumo updateInsumo = (CommonUtils.Insumo)gridViewListaInsumos.GetFocusedRow();//((DevExpress.XtraGrid.Views.Grid.GridView)gridControlListaInsumos.Views[0]).GetFocusedRow();
            if (updateInsumo != null)
            {
                if ( !( this.ParentForm as mainForm ).ContextControlsForInsumo.ContainsKey( updateInsumo.idInsumo.ToString() ) )
                {
                    NuevoInsumo pnlInsumo = new NuevoInsumo( updateInsumo );
                    (this.ParentForm as mainForm).ContextControlsForInsumo.Add(updateInsumo.idInsumo.ToString(), pnlInsumo);
                    pnlInsumo.Dock = DockStyle.Fill;
                    DevExpress.XtraTab.XtraTabPage tabItem = new DevExpress.XtraTab.XtraTabPage( );
                    tabItem.Controls.Add( pnlInsumo );
                    tabItem.Text = pnlInsumo.Insumo.nombre;
                    ( this.ParentForm as mainForm ).xtraTabControl.TabPages.Add( tabItem );
                    ( this.ParentForm as mainForm ).xtraTabControl.SelectedTabPage = tabItem;

                   // ( this.ParentForm as mainForm ).ContextControls.Add( updateInsumo.idInsumo.ToString() , pnlInsumo );
                }
            }
        }

        private void barButtonAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if ( !( this.ParentForm as mainForm ).ContextControls.ContainsKey( "NuevoInsumo" ) )
            {
                NuevoInsumo pnlInsumo = new NuevoInsumo( );
                pnlInsumo.Dock = DockStyle.Fill;
                DevExpress.XtraTab.XtraTabPage tabItem = new DevExpress.XtraTab.XtraTabPage( );
                tabItem.Controls.Add( pnlInsumo );
                tabItem.Text = "nuevo insumo";
                ( this.ParentForm as mainForm ).xtraTabControl.TabPages.Add( tabItem );
                ( this.ParentForm as mainForm ).xtraTabControl.SelectedTabPage = tabItem;

            //    ( this.ParentForm as mainForm ).ContextControls.Add( "NuevoInsumo" , pnlInsumo );
            }
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

        private void barButtonPdf_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog.Filter = "Archivo PDF (*.pdf)|*.pdf";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.FileName = "Insumos.pdf";
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
                    gridViewListaInsumos.ExportToPdf(saveFileDialog.FileName);
                    ps.AfterChange -= new DevExpress.XtraPrinting.ChangeEventHandler(Export_ProgressEx);
                    Cursor.Current = currentCursor;

                    EndExport();

                    Process.Start(saveFileDialog.FileName);
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
            saveFileDialog.FileName = "Insumos.xls";
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

                    gridViewListaInsumos.ExportToXls(saveFileDialog.FileName);

                    ps.AfterChange -= new DevExpress.XtraPrinting.ChangeEventHandler(Export_ProgressEx);
                    Cursor.Current = currentCursor;

                    EndExport();

                    Process.Start(saveFileDialog.FileName);
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
            saveFileDialog.FileName = "Insumos.xlsx";
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

                    gridViewListaInsumos.ExportToXlsx(saveFileDialog.FileName);

                    ps.AfterChange -= new DevExpress.XtraPrinting.ChangeEventHandler(Export_ProgressEx);
                    Cursor.Current = currentCursor;

                    EndExport();

                    Process.Start(saveFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: No se pudo guardar a la ubicación. Original error: " + ex.Message);
                }
            }
        }

        private void barButtonPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridControlListaInsumos.ShowPrintPreview();
        }

        private void barButtonRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            actualizarGrid();
        }


        #region ActualizarGrids Members

        public void actualizarGrid()
        {
            InitializeGrid();
        }

        #endregion

        private void barButtonDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //DataRow rowSelected = gridViewListaInsumos.GetDataRow(gridViewListaInsumos.FocusedRowHandle);
           // DataRow rowSelected = gridViewListaInsumos.GetDataRow(gridViewListaInsumos.FocusedRowHandle);
            CommonUtils.Insumo insumoABorrar = (CommonUtils.Insumo)gridViewListaInsumos.GetFocusedRow();//((DevExpress.XtraGrid.Views.Grid.GridView)gridControlListaInsumos.Views[0]).GetFocusedRow();

            if (insumoABorrar == null)
            {
                MessageBox.Show(this, "Seleccione una fila para borrar", "Insumos", MessageBoxButtons.OK);
                return;
            }
            string sqlQuery = "Select * from Receta_Insumo where InsumoId="+insumoABorrar.idInsumo;
            string sqlQueryCompras = "Select * from Compras where InsumoId=" + insumoABorrar.idInsumo;
            if (CommonUtils.ConexionBD.EjecutarConsulta(sqlQuery).Rows.Count > 0 || CommonUtils.ConexionBD.EjecutarConsulta(sqlQueryCompras).Rows.Count>0)
            {
                MessageBox.Show(this, "el insumo "+insumoABorrar.nombre +" no se puede borrar debido a que se encuentra asociado a otros datos.", "Insumos", MessageBoxButtons.OK);
                return;
            }

            if (MessageBox.Show(this, "Desea borrar el insumo " + insumoABorrar.nombre + " ?", "Insumos", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            else 
            {
                sqlQuery = "Delete from Insumo where InsumoId=" + insumoABorrar.idInsumo;
                CommonUtils.ConexionBD.Actualizar(sqlQuery);
                this.actualizarGrid();
            }
        }
    }
}
