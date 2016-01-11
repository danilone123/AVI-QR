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
using System.Data.SqlClient;
using System.Collections;
using System.Xml;

namespace UIRenderers
{
    public partial class ReporteFacturas : DevExpress.XtraEditors.XtraUserControl
    {         
        private static log4net.ILog log = log4net.LogManager.GetLogger("ReporteFacturas");
        public string MessageBarValue { get; set; }
        ProgressForm progressForm;
        public ReporteFacturas()
        {
            InitializeComponent();
            InitConection();
            this.dateEditInit.Properties.MinValue = DateTime.Now.AddYears(-1);
        }

        private void InitConection()
        {
            CommonUtils.ConexionBD.StringDeConexion = CommonUtils.ConexionBD.GetConnectionString();
        }

        private string getNitValue(string xmlValue)
        {
            XmlReader reader = XmlReader.Create(new System.IO.StringReader(xmlValue));
            while ( reader.Read( ) )
            {
                if (reader.NodeType == XmlNodeType.Element && reader.LocalName == "ClienteNit")
                {
                    return reader.ReadInnerXml();
                }
            }
            return xmlValue;
        }

        private string getNombreValue(string xmlValue)
        {
            XmlReader reader = XmlReader.Create(new System.IO.StringReader(xmlValue));
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.LocalName == "Cliente")
                {
                    return reader.ReadInnerXml();
                }
            }
            return xmlValue;
        }

        private string getMontoValue(string xmlValue)
        {
            XmlReader reader = XmlReader.Create(new System.IO.StringReader(xmlValue));
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.LocalName == "Total")
                {
                    return reader.ReadInnerXml();

                }
            }
            return xmlValue;
        }

        private delegate void LoadGrid(ArrayList s);

        private void loadDataGridReportes(ArrayList tableReportes)
        {
            try
            {
                if ( gridControlFactura.InvokeRequired )
                {
                    LoadGrid da = new LoadGrid( loadDataGridReportes );
                    this.Invoke( da , new object[ ] { tableReportes } );
                }
                else
                {
                    gridControlFactura.DataSource = null;
                    gridControlFactura.DataSource = tableReportes;

                    gridViewFacturas.Columns[ 0 ].FieldName = "FacturaId";
                    gridViewFacturas.Columns[ 1 ].FieldName = "FechaTransaccion";//"FechaTransaccion";FacturaId
                    gridViewFacturas.Columns[ 2 ].FieldName = "FacturaNit";
                    gridViewFacturas.Columns[ 3 ].FieldName = "FacturaNombre";
                    gridViewFacturas.Columns[ 4 ].FieldName = "FacturaNro";
                    gridViewFacturas.Columns[ 5 ].FieldName = "FacturaAutorizacion";
                    gridViewFacturas.Columns[ 6 ].FieldName = "FacturaMontoParcial";
                    gridViewFacturas.Columns[ 7 ].FieldName = "FacturaMontoTotal";
                    gridViewFacturas.Columns[ 8 ].FieldName = "FacturaCodigoControl";

                    gridViewFacturas.BestFitColumns( );
                    // gridViewFacturas.SetRowCellValue(6, "Monto").ToString();
                    //gridViewFacturas.SetRowCellValue(6, "Monto", "test");
                    this.loadingQueryFinished( );
                }
            }
            catch ( Exception  ex )
            {
                log.Error( ex.Message , ex );
            }
        }

        private void RefreshGrid()
        {
            try
            {
                CommonUtils.ConexionBD.AbrirConexion();
                string sqlQuery = " Select FacturaId,FacturaAutorizacion,FacturaNro,FacturaContenido, FacturaCodigoControl, FacturaAnulada, FechaTransaccion, MontoTotal from Factura WHERE Factura.FechaTransaccion BETWEEN CONVERT(VARCHAR,'" + dateEditInit.DateTime.ToString( "yyyy-MM-dd H:mm:ss.fff" ) + "',7) AND CONVERT(varchar,'" + dateEditFinal.DateTime.ToString( "yyyy-MM-dd H:mm:ss.fff" ) + "',7) order by Factura.FechaTransaccion";
               // DataTable t = CommonUtils.ConexionBD.EjecutarConsulta(sqlQuery);
                //this.loadDataGridReportes(t);
                SqlDataReader reader = CommonUtils.ConexionBD.EjecutarConsultaReader(sqlQuery);
                ArrayList listaFacturas = new ArrayList();

                if (reader.HasRows)
                {
                    CommonUtils.FacturaReporte fact;
                    while (reader.Read())
                    {
                        fact = new CommonUtils.FacturaReporte();
                        fact.FacturaId = (int)reader.GetDecimal(0);
                        fact.FacturaAutorizacion = Convert.ToDecimal( reader.GetString( 1 ) );
                        fact.FacturaNro = Convert.ToInt64( reader.GetString( 2 ) );
                        fact.FacturaNit = Convert.ToUInt64( this.getNitValue( reader.GetString( 3 ) ) );
                        fact.FacturaCodigoControl =  reader.GetString(4);
                        fact.FacturaAnulada = reader.GetString(5);
                        fact.FechaTransaccion =  reader.GetDateTime(6);//.ToString();
                        fact.FacturaNombre = this.getNombreValue(reader.GetString(3));
                        fact.FacturaMontoParcial = Convert.ToDecimal( this.getMontoValue( reader.GetString( 3 ) ) );
                        fact.FacturaMontoTotal = Convert.ToDecimal( !reader.IsDBNull( 7 ) ? reader.GetDouble( 7 ).ToString( ) : "0" );
                        listaFacturas.Add(fact);                      
                    }
                }

                CommonUtils.ConexionBD.CerrarConexion();
                this.loadDataGridReportes(listaFacturas);
               
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

        #region add and remove, indicator when reportes button is pressed
        private void loadingQueryFinished()
        {
            panel2.Visible = false;
            panel2.SendToBack();
            panel2.Enabled = false;
            this.BringToFront();
            ribbonControl.Enabled = true;
            gridControlFactura.Enabled = true;
            (this.ParentForm as mainForm).xtraTabControl.Enabled = true;
        }

        private void loadingQueryStarted()
        {

            panel2.BringToFront();
            panel2.Enabled = true;
            this.SendToBack();
            ribbonControl.Enabled = false;
            gridControlFactura.Enabled = false;
            (this.ParentForm as mainForm).xtraTabControl.Enabled = false;
            panel2.Location = new System.Drawing.Point((this.Size.Width / 2) - panel2.Size.Width / 2,
               (this.Size.Height / 2) - panel2.Size.Height / 2);
            panel2.Visible = true;

        }
        #endregion

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
            alertControl.Show(this.FindForm(), "Falló al ver reportes.", " Por favor, intente nuevamente.", GetImage());
            MessageBox.Show(this, (string)controlErrors[0].Tag, dxErrorProviderReportes.GetError(controlErrors[0]), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private Image GetImage()
        {
            return imageCollection.Images[0];
        }
        #endregion

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

        #region Eventos

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
                    MessageBox.Show(this, "Fecha Inicio no puede ser mayor que Fecha Final", "Reportes Facturas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                this.GetErrorProviderMessages();
            }
        }


        private void barButtonPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridControlFactura.ShowPrintPreview();
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

            if (gridViewFacturas.FocusedRowHandle > 1)
                gridViewFacturas.MovePrev();
            else
            {
                gridViewFacturas.MovePrev();
                barButtonUp.Enabled = false;
            }
        }

        private void barButtonDown_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            barButtonUp.Enabled = true;

            if (gridViewFacturas.FocusedRowHandle + 1 < gridViewFacturas.RowCount - 1)
                gridViewFacturas.MoveNext();
            else
            {
                gridViewFacturas.MoveNext();
                barButtonDown.Enabled = false;
            }
        }

        private void barButtonPdf_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog.Filter = "Archivo PDF (*.pdf)|*.pdf";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.FileName = "ReportesFactura.pdf";
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
                    gridControlFactura.ExportToPdf(saveFileDialog.FileName);

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
            saveFileDialog.FileName = "ReportesFactura.xls";
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

                    gridControlFactura.ExportToXls( saveFileDialog.FileName );

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
            saveFileDialog.FileName = "ReportesFactura.xlsx";
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

                    gridControlFactura.ExportToXlsx(saveFileDialog.FileName);

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

        #endregion

        protected virtual void Export_ProgressEx(object sender, DevExpress.XtraPrinting.ChangeEventArgs e)
        {
            if (e.EventName == DevExpress.XtraPrinting.SR.ProgressPositionChanged)
            {
                int pos = (int)e.ValueOf(DevExpress.XtraPrinting.SR.ProgressPosition);
                progressForm.SetProgressValue(pos);
            }
        }

       
      
    }
}
