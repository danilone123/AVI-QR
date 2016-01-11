using DevExpress.XtraEditors;
using System;
using System.Data.SqlClient;
using System.Data;
using DevExpress.XtraGrid.Views.Base;

namespace UIRenderers
{
    public partial class Dosificacion : XtraForm
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger( "Dosificacion" );
        public string MessageBarValue { get; set; }

        public Dosificacion( )
        {
            InitializeComponent( );
            log4net.Config.XmlConfigurator.Configure( );
            InitConexionDB( );
        }

        private void InitConexionDB( )
        {
            CommonUtils.ConexionBD.StringDeConexion = CommonUtils.ConexionBD.GetConnectionString( );//GetConnectionString( );
        }

        private void InitializeGrid( )
        {
            CommonUtils.ConexionBD.AbrirConexion( );

            string sqlDosificacion = "SELECT dosificacion_llave AS [Llave], dosificacion_nro_autorizacion as [Nro de Autorizacion], dosificacion_ultimo_valor AS [Nro de Factura], " +
                " dosificacion_fechalimite AS [Fecha Limite de Emision], dosificacion_activa AS [Activa], dosificacion_id " +
                "From dosificacion ";

            try
            {
                SqlCommand cmdDosificacion = new SqlCommand( sqlDosificacion , CommonUtils.ConexionBD.Conexion );

                DataSet dataSet = new DataSet( );
                dataSet.Load( cmdDosificacion.ExecuteReader( ) , LoadOption.OverwriteChanges , "Dosificacion" );


                gridDosificacion.DataSource = dataSet.Tables[ "Dosificacion" ];

                ColumnView view = this.gridDosificacion.MainView as ColumnView;
                view.Columns.Add( );
                view.Columns[ 5 ].Visible = false;
                this.gridDosificacion.RepositoryItems.Add( view.Columns[ 1 ].ColumnEdit );

                MessageBarValue = dataSet.Tables[ "Dosificacion" ].Rows.Count.ToString( ) + " Dosificacion";
            }
            catch ( Exception ex )
            {
                log.Error( ex.Message , ex );
                MessageBarValue = "No se pudieron listar las dosificaciones. Hubo un error: " + ex.Message;
            }
            finally
            {
                barItem.Text = MessageBarValue;
            }

            CommonUtils.ConexionBD.CerrarConexion( );
        }

        #region Events & Handlers
        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad( e );

            InitializeGrid( );
        }

        private void barLargeButtonItem1_ItemClick( object sender , DevExpress.XtraBars.ItemClickEventArgs e )
        {
            NuevaDosificacion nuevaDosificacion = new NuevaDosificacion( );
            nuevaDosificacion.ShowDialog( );
            nuevaDosificacion.Refresh( );
        }

        private void gridDosificacion_MouseDoubleClick( object sender , System.Windows.Forms.MouseEventArgs e )
        {
            DataRowView selectedRow;
            selectedRow = ( DataRowView ) gridViewDosificacion.GetFocusedRow( );

            if ( selectedRow != null )
            {
                CommonUtils.Dosificacion dosificacion = new CommonUtils.Dosificacion( );
                dosificacion.Llave = (string)( selectedRow.Row.ItemArray[ 0 ] );
                dosificacion.Autorizacion = Convert.ToString( selectedRow.Row.ItemArray[ 1 ] );
                dosificacion.NroFactura = Convert.ToString( selectedRow.Row.ItemArray[ 2 ] );
                dosificacion.FechaLimite = Convert.ToString( selectedRow.Row.ItemArray[ 3 ] );
                dosificacion.Activa = Convert.ToInt32( selectedRow.Row.ItemArray[ 4 ] );
                dosificacion.DosificacionID = Convert.ToInt32( selectedRow.Row.ItemArray[ 5 ] );

                NuevaDosificacion formDosificacion = new NuevaDosificacion( dosificacion );
                formDosificacion.Dosificaciones += new NuevaDosificacion.DosificacionHandler( formDosificacion_Dosificaciones );
                formDosificacion.ShowDialog( );
                formDosificacion.Refresh( );
            }
        }

        void formDosificacion_Dosificaciones( NuevaDosificacion Dosificacion )
        {
            InitializeGrid( );
        }

        #endregion

    }
}
