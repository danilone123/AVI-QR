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
    public partial class ReportesComprasEspeciales : DevExpress.XtraEditors.XtraUserControl
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger("ReportesComprasEspeciales");
        public string MessageBarValue { get; set; }
        ProgressForm progressForm;
        public ReportesComprasEspeciales()
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
            if (gridControlListaComprasEspeciales.InvokeRequired)
            {
                LoadGrid da = new LoadGrid(loadDataGridReportes);
                this.Invoke(da, new object[] { tableReportes });
            }
            else
            {
                gridControlListaComprasEspeciales.DataSource = null;
                gridControlListaComprasEspeciales.DataSource = tableReportes;
                
                gridViewListaComprasEspeciales.Columns[0].FieldName = "ComprasEspecialesId";
                gridViewListaComprasEspeciales.Columns[1].FieldName = "Costo";
                gridViewListaComprasEspeciales.Columns[2].FieldName = "Descripcion";
                gridViewListaComprasEspeciales.Columns[3].FieldName = "FechaCompra";
               

                this.loadingQueryFinished();
            }

        }
        private void RefreshGrid()
        {
            try
            {
                string sqlQuery = " Select ComprasEspecialesId,Costo,Descripcion, ( CAST (FechaCompraEspecialText AS Date ) ) AS FechaCompra from ComprasEspeciales WHERE CAST(FechaCompraEspecialText AS Date) BETWEEN '" + dateEditInit.DateTime.ToString( "yyyy-MM-dd" ) + "' AND '" + dateEditFinal.DateTime.ToString( "yyyy-MM-dd" ) + "' ";
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

        private void dateEditInit_Validating(object sender, CancelEventArgs e)
        {
            this.ValidateEmptyStringRule(sender as BaseEdit);
        }

        #region add and remove, indicator when reportes button is pressed
        private void loadingQueryFinished()
        {
            panel2.Visible = false;
            panel2.SendToBack();
            panel2.Enabled = false;
            this.BringToFront();
            ribbonControl.Enabled = true;
            gridControlListaComprasEspeciales.Enabled = true;
            (this.ParentForm as mainForm).xtraTabControl.Enabled = true;
        }

        private void loadingQueryStarted()
        {

            panel2.BringToFront();
            panel2.Enabled = true;
            this.SendToBack();
            ribbonControl.Enabled = false;
            gridControlListaComprasEspeciales.Enabled = false;
            (this.ParentForm as mainForm).xtraTabControl.Enabled = false;
            panel2.Location = new System.Drawing.Point((this.Size.Width / 2) - panel2.Size.Width / 2,
               (this.Size.Height / 2) - panel2.Size.Height / 2);
            panel2.Visible = true;

        }
        #endregion
        
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

        private void dateEditFinal_Validating(object sender, CancelEventArgs e)
        {
            this.ValidateEmptyStringRule(sender as BaseEdit);

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
        #endregion

        private void barButtonPdf_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog.Filter = "Archivo PDF (*.pdf)|*.pdf";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.FileName = "ComprasEspeciales.pdf";
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
                    gridControlListaComprasEspeciales.ExportToPdf(saveFileDialog.FileName);

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
            saveFileDialog.FileName = "ComprasEspeciales.xls";
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

                    gridControlListaComprasEspeciales.ExportToXls(saveFileDialog.FileName);

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
            saveFileDialog.FileName = "ComprasEspeciales.xlsx";
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

                    gridControlListaComprasEspeciales.ExportToXlsx(saveFileDialog.FileName);

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

        protected virtual void Export_ProgressEx(object sender, DevExpress.XtraPrinting.ChangeEventArgs e)
        {
            if (e.EventName == DevExpress.XtraPrinting.SR.ProgressPositionChanged)
            {
                int pos = (int)e.ValueOf(DevExpress.XtraPrinting.SR.ProgressPosition);
                progressForm.SetProgressValue(pos);
            }
        }

        private void barButtonPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridControlListaComprasEspeciales.ShowPrintPreview();
        }

    }
}
