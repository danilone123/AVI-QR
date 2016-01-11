using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using System.Threading;

namespace UIRenderers
{
    public partial class ReporteVentasPorFecha : DevExpress.XtraEditors.XtraUserControl
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger("ReportesVentasPorFecha");
        public string MessageBarValue { get; set; }
        ProgressForm progressForm;
        private delegate void LoadGrid(DataTable s);
        public ReporteVentasPorFecha()
        {
            InitializeComponent();
            InitConection();
            this.dateEditInit.Properties.MinValue = DateTime.Now.AddYears(-1);
            panel2.Visible = false;
            panel2.Enabled = false;
            panel2.SendToBack();
        }

        private void InitConection()
        {
            CommonUtils.ConexionBD.StringDeConexion = CommonUtils.ConexionBD.GetConnectionString();
        }

        private void loadDataGridReportes(DataTable tableReportes)
        {
            if (gridComprasConFecha.InvokeRequired)
            {
                LoadGrid da = new LoadGrid(loadDataGridReportes);
                this.Invoke(da, new object[] { tableReportes });
            }
            else
            {
                gridComprasConFecha.DataSource = null;
                gridComprasConFecha.DataSource = tableReportes;
                gridViewComprasConFecha.Columns[0].FieldName = "ComprasId";
                gridViewComprasConFecha.Columns[1].FieldName = "Nombre";
                gridViewComprasConFecha.Columns[2].FieldName = "Cantidad";
                gridViewComprasConFecha.Columns[3].FieldName = "CostoUnidad";
                gridViewComprasConFecha.Columns[4].FieldName = "Total";
                gridViewComprasConFecha.Columns[5].FieldName = "FechaCompra";
                gridViewComprasConFecha.Columns[6].FieldName = "Descripcion";
                this.loadingQueryFinished();
            }

        }
        private void RefreshGrid()
        {
            try
            {
                string sqlQuery = "Select Compras.ComprasId,Compras.Descripcion,( CAST (Compras.FechaCompraText AS Date ) ) AS FechaCompra, " +
                    "Insumo.Nombre,Compras.Cantidad,Compras.CostoUnidad,Compras.Total  FROM Compras INNER JOIN  Insumo ON Compras.InsumoId = Insumo.InsumoId " + 
                    "WHERE CAST (Compras.FechaCompraText as Date) BETWEEN '" + dateEditInit.DateTime.ToString( "yyyy-MM-dd" ) + "' AND '" 
                    + dateEditFinal.DateTime.ToString( "yyyy-MM-dd" ) + "' order by Insumo.Nombre"; 
                          //Function1('" + dateEditInit.DateTime.ToString("yyyyMMdd") + "','" + dateEditFinal.DateTime.ToString("yyyyMMdd") + "'),Producto where Producto.ProductoId=Function1.ProductoId";//"SELECT Producto.Nombre, ViewPedidos.Costo, ViewPedidos.Precio, ViewPedidos.Cantidad, ViewPedidos.Total FROM  ViewPedidos INNER JOIN  Producto ON ViewPedidos.ProductoId = Producto.ProductoId";
               
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

        #region progressform
        protected virtual void EndExport()
        {
            progressForm.Dispose();
            progressForm = null;
        }

        protected virtual void StartExport()
        {
            if (this.ParentForm != null)
                this.ParentForm.Update();

            progressForm = new ProgressForm(this.ParentForm);
            progressForm.Show();
            progressForm.Refresh();
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
        #region Validar formulario
        private void ValidateEmptyStringRule(BaseEdit control)
        {
            if (control.Text == null || control.Text.Trim().Length == 0)
                dxErrorProviderReport.SetError(control, "Este campo no puede ser vacio", ErrorType.Critical);
            else
                dxErrorProviderReport.SetError(control, "");
        }

        private void GetErrorProviderMessages()
        {
            IList<Control> controlErrors = dxErrorProviderReport.GetControlsWithError();
            //    controlErrors.OrderBy<>;
            alertControl.Show(this.FindForm(), "Falló al ver reportes.", "Los cambios NO se guardaron.  Por favor, intente nuevamente.", GetImage());
            MessageBox.Show(this, (string)controlErrors[0].Tag, dxErrorProviderReport.GetError(controlErrors[0]), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void dateEditInit_Validating(object sender, CancelEventArgs e)
        {
            this.ValidateEmptyStringRule(sender as BaseEdit);
        }

        private void dateEditFinal_Validating(object sender, CancelEventArgs e)
        {
            this.ValidateEmptyStringRule(sender as BaseEdit);
        }

        private Image GetImage()
        {
            return imageCollection.Images[0];
        }
        #endregion
        #region eventos
        private void barButtonPdf_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog.Filter = "Archivo PDF (*.pdf)|*.pdf";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.FileName = "Productos.pdf";
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
                    gridComprasConFecha.ExportToPdf(saveFileDialog.FileName);

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
            saveFileDialog.FileName = "Productos.xls";
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

                    gridViewComprasConFecha.BestFitColumns();
                    gridComprasConFecha.ExportToXls(saveFileDialog.FileName);

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
            saveFileDialog.FileName = "Productos.xlsx";
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

                    gridComprasConFecha.ExportToXlsx(saveFileDialog.FileName);

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
            gridComprasConFecha.ShowPrintPreview();
        }
       
        private void barButtonDown_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            barButtonUp.Enabled = true;

            if (gridViewComprasConFecha.FocusedRowHandle + 1 < gridViewComprasConFecha.RowCount - 1)
                gridViewComprasConFecha.MoveNext();
            else
            {
                gridViewComprasConFecha.MoveNext();
                barButtonDown.Enabled = false;
            }
        }
        private void barButtonUp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            barButtonDown.Enabled = true;

            if (gridViewComprasConFecha.FocusedRowHandle > 1)
                gridViewComprasConFecha.MovePrev();
            else
            {
                gridViewComprasConFecha.MovePrev();
                barButtonUp.Enabled = false;
            }
        }
        private void btnGenerarReporte_Click(object sender, EventArgs e)
        {
            if (!dxErrorProviderReport.HasErrors)
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

        #endregion 

        #region Loading indicator
        private void loadingQueryStarted()
        {
           
            panel2.BringToFront();
            panel2.Enabled = true;
            this.SendToBack();
            ribbonControl.Enabled = false;
            gridComprasConFecha.Enabled = false;
            (this.ParentForm as mainForm).xtraTabControl.Enabled = false;
            panel2.Location = new System.Drawing.Point((this.Size.Width/2)-panel2.Size.Width/2, 
               ( this.Size.Height/2)-panel2.Size.Height/2);
            panel2.Visible = true;

        }

        private void loadingQueryFinished()
        {
            panel2.Visible = false;
            panel2.SendToBack();
            panel2.Enabled = false;
            this.BringToFront();
            ribbonControl.Enabled = true;
            gridComprasConFecha.Enabled = true;
            (this.ParentForm as mainForm).xtraTabControl.Enabled = true;
        }

        #endregion

       

    }
}
