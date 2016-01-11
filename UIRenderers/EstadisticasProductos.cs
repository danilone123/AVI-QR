using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using DevExpress.XtraCharts;
using System.Diagnostics;

namespace UIRenderers
{
    public partial class EstadisticasProductos : UserControl
    {
        ProgressForm progressForm;

        public EstadisticasProductos( )
        {
            InitializeComponent( );
            InitConexionDB( );
            InitDataSource( );
        }

        #region Methods
        private void InitConexionDB( )
        {
            CommonUtils.ConexionBD.StringDeConexion = CommonUtils.ConexionBD.GetConnectionString( );
        }

        private void InitDataSource( )
        {
            CommonUtils.ConexionBD.AbrirConexion( );
            ChartTitle chartTitle = new ChartTitle( );
            chartTitle.Text = "Productos por tipo";
            chartControl.Titles.Add( chartTitle );
            //chartControl.SeriesTemplate.ValueDataMembersSerializable = "Tipo";

            string sql = "SELECT Categoria.Tipo, COUNT(Categoria.Tipo) AS Amount FROM Categoria INNER JOIN  Producto ON Categoria.CategoriaId = Producto.CategoriaId " +
                "GROUP BY Categoria.Tipo HAVING (Categoria.Tipo = Categoria.Tipo)";
            //DataTable table = CommonUtils.ConexionBD.EjecutarConsulta( sql );

            SqlCommand cmdProduct = new SqlCommand( sql , CommonUtils.ConexionBD.Conexion );
            DataSet dataSet = new DataSet( );
            dataSet.Load( cmdProduct.ExecuteReader( ) , LoadOption.OverwriteChanges , "Producto" );
            BindingSource bs = new BindingSource( );
            bs.DataSource = dataSet.Tables[ 0 ];
            chartControl.DataSource = bs;
            chartControl.SeriesDataMember = "Tipo";
            chartControl.SeriesTemplate.ArgumentDataMember = "Amount";
            //chartControl.SeriesTemplate.ValueDataMembersSerializable = "Amount";
            chartControl.SeriesTemplate.ValueDataMembers.AddRange( new string[ ] { "Amount" } );

            CommonUtils.ConexionBD.CerrarConexion( );
        }

        #endregion

        #region Events & Handlers
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

        private void barButtonPreview_ItemClick( object sender , DevExpress.XtraBars.ItemClickEventArgs e )
        {
            chartControl.ShowPrintPreview( );
        }

        private void barButtonItem1_ItemClick( object sender , DevExpress.XtraBars.ItemClickEventArgs e )
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog( );
            saveFileDialog.InitialDirectory = Environment.GetFolderPath( Environment.SpecialFolder.MyDocuments );
            saveFileDialog.Filter = "Archivo PDF (*.pdf)|*.pdf";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.FileName = "Productos.pdf";
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
                    chartControl.ExportToPdf( saveFileDialog.FileName );
                    ps.AfterChange -= new DevExpress.XtraPrinting.ChangeEventHandler( Export_ProgressEx );
                    Cursor.Current = currentCursor;

                    EndExport( );

                    Process.Start( saveFileDialog.FileName );
                }
                catch ( Exception ex )
                {
                    MessageBox.Show( "Error: No se pudo guardar a la ubicación. Original error: " + ex.Message );
                }
            }
        }

        private void barButtonItemClose_ItemClick( object sender , DevExpress.XtraBars.ItemClickEventArgs e )
        {
            ( this.ParentForm as mainForm ).ContextControls.Remove( ( this.ParentForm as mainForm ).xtraTabControl.SelectedTabPage.Controls[ 0 ].Name );
            ( this.ParentForm as mainForm ).xtraTabControl.TabPages.RemoveAt( ( this.ParentForm as mainForm ).xtraTabControl.SelectedTabPageIndex );
        }
        #endregion
    }
}
