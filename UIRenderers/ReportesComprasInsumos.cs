using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Threading;
using DevExpress.XtraEditors.DXErrorProvider;

namespace UIRenderers
{
    public partial class ReportesComprasInsumos : DevExpress.XtraEditors.XtraUserControl
    {
        ProgressForm progressForm;
        private static log4net.ILog log = log4net.LogManager.GetLogger("ReportesComprasInsumos");
        public string MessageBarValue { get; set; }
        public ReportesComprasInsumos()
        {
            InitializeComponent();
            InitConection();
            this.dateEditInit.Properties.MinValue = DateTime.Now.AddYears(-1);
        }

        private void InitConection()
        {
            CommonUtils.ConexionBD.StringDeConexion = CommonUtils.ConexionBD.GetConnectionString();
        }

        private delegate void LoadGrid(DataTable s);

        private void loadDataGridReportes(DataTable tableReportes)
        {
            if (gridControlReporteVentas.InvokeRequired)
            {
                LoadGrid da = new LoadGrid(loadDataGridReportes);
                this.Invoke(da, new object[] { tableReportes });
            }
            else
            {
                gridControlReporteVentas.DataSource = null;
                gridControlReporteVentas.DataSource = tableReportes;
                gridViewReporteVentas.Columns[0].FieldName = "InsumoId";
                gridViewReporteVentas.Columns[1].FieldName = "Nombre";
                gridViewReporteVentas.Columns[2].FieldName = "CostoLote";
                gridViewReporteVentas.Columns[3].FieldName = "CantidadLotes";
                gridViewReporteVentas.Columns[4].FieldName = "Total";
                
                this.loadingQueryFinished();
            }

        }
        private void RefreshGrid()
        {
            try
            {
                string sqlQuery = "Select FunctionComprasInsumos.InsumoId,Insumo.Nombre ,FunctionComprasInsumos.CantidadLotes,FunctionComprasInsumos.Total, FunctionComprasInsumos.CostoLote from FunctionComprasInsumos('" + dateEditInit.DateTime.ToString( "yyyyMMdd" ) + "','" + dateEditFinal.DateTime.ToString( "yyyyMMdd" ) + "'),Insumo where Insumo.InsumoId=FunctionComprasInsumos.InsumoId order by Insumo.Nombre";//"SELECT Producto.Nombre, ViewPedidos.Costo, ViewPedidos.Precio, ViewPedidos.Cantidad, ViewPedidos.Total FROM  ViewPedidos INNER JOIN  Producto ON ViewPedidos.ProductoId = Producto.ProductoId";
                //Thread.Sleep(1);
                DataTable t = CommonUtils.ConexionBD.EjecutarConsulta(sqlQuery);
                this.loadDataGridReportes(t);
                MessageBarValue = "El reporte fue generado satisfactoriamente.";
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                MessageBarValue = "No se pudo generar reporte. Hubo un error: " + ex.Message;
            }
            finally 
            {
                alertControl.Show(this.FindForm(), 
                        "Reportes.", 
                        MessageBarValue, this.GetImage());
                
            }          
        }


        #region Validar formulario
        private void ValidateEmptyStringRule(BaseEdit control)
        {
            if (control.Text == null || control.Text.Trim().Length == 0)
                dxErrorProviderReportes.SetError(control, "Este campo no puede ser vacio", ErrorType.Critical);
            else
                dxErrorProviderReportes.SetError(control, "");
        }

        private void GetErrorProviderMessages()
        {
            IList<Control> controlErrors = dxErrorProviderReportes.GetControlsWithError();
            //    controlErrors.OrderBy<>;
            alertControl.Show( this.FindForm( ) , "Falló al ver reportes." , " Por favor, intente nuevamente." , GetImage( ) );
            MessageBox.Show(this, (string)controlErrors[0].Tag, dxErrorProviderReportes.GetError(controlErrors[0]), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private Image GetImage( )
        {
            return imageCollection.Images[ 0 ];
        }
        #endregion
        #region add and remove, indicator when reportes button is pressed
        private void loadingQueryFinished()
        {
            panel2.Visible = false;
            panel2.SendToBack();
            panel2.Enabled = false;
            this.BringToFront();
            ribbonControl.Enabled = true;
            gridControlReporteVentas.Enabled = true;
            (this.ParentForm as mainForm).xtraTabControl.Enabled = true;
        }

        private void loadingQueryStarted()
        {

            panel2.BringToFront();
            panel2.Enabled = true;
            this.SendToBack();
            ribbonControl.Enabled = false;
            gridControlReporteVentas.Enabled = false;
            (this.ParentForm as mainForm).xtraTabControl.Enabled = false;
            panel2.Location = new System.Drawing.Point((this.Size.Width / 2) - panel2.Size.Width / 2,
               (this.Size.Height / 2) - panel2.Size.Height / 2);
            panel2.Visible = true;

        }
        #endregion

        protected virtual void Export_ProgressEx(object sender, DevExpress.XtraPrinting.ChangeEventArgs e)
        {
            if (e.EventName == DevExpress.XtraPrinting.SR.ProgressPositionChanged)
            {
                int pos = (int)e.ValueOf(DevExpress.XtraPrinting.SR.ProgressPosition);
                progressForm.SetProgressValue(pos);
            }
        }

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

        private void btnGenerarReporte_Click(object sender, EventArgs e)
        {
            if (!dxErrorProviderReportes.HasErrors)
            {
                if (dateEditInit.DateTime <= dateEditFinal.DateTime)
                {
                    CommonUtils.ReportesUtils reportesHilos = new CommonUtils.ReportesUtils(new CommonUtils.ReportesUtils.ResultDelegate(this.RefreshGrid));
                    Thread t = new Thread(new ThreadStart(reportesHilos.ThreadProc));
                                      loadingQueryStarted();
                    t.Start();
                }
                else
                {
                    MessageBox.Show(this, "Fecha Inicio no puede ser mayor que Fecha Final", "Reportes de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                this.GetErrorProviderMessages();
            }
        }

        private void dateEditInit_Validating(object sender, CancelEventArgs e)
        {
            this.ValidateEmptyStringRule(sender as BaseEdit);
        }

        private void dateEditFinal_Validating(object sender, CancelEventArgs e)
        {
            this.ValidateEmptyStringRule(sender as BaseEdit);
        }

        private void barButtonUp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            barButtonDown.Enabled = true;

            if (gridViewReporteVentas.FocusedRowHandle > 1)
                gridViewReporteVentas.MovePrev();
            else
            {
                gridViewReporteVentas.MovePrev();
                barButtonUp.Enabled = false;
            }
        }

        private void barButtonDown_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            barButtonUp.Enabled = true;

            if (gridViewReporteVentas.FocusedRowHandle + 1 < gridViewReporteVentas.RowCount - 1)
                gridViewReporteVentas.MoveNext();
            else
            {
                gridViewReporteVentas.MoveNext();
                barButtonDown.Enabled = false;
            }
        }

        private void barButtonPdf_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog.Filter = "Archivo PDF (*.pdf)|*.pdf";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.FileName = "ComprasInsumos.pdf";
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
                    gridControlReporteVentas.ExportToPdf(saveFileDialog.FileName);

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
            saveFileDialog.FileName = "ComprasInsumos.xls";
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

                    gridControlReporteVentas.ExportToXls(saveFileDialog.FileName);

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
            saveFileDialog.FileName = "ComprasInsumos.xlsx";
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

                    gridControlReporteVentas.ExportToXlsx(saveFileDialog.FileName);

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

        private void barButtonPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridControlReporteVentas.ShowPrintPreview();
        }
    }
}
