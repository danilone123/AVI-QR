using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Card;

namespace UIRenderers
{
    public partial class GridProductos : UserControl
    {
        private string sqlQuery;
        internal CommonUtils.Ventas ventas;

        public GridProductos( )
        {
        }

        public GridProductos( string sql )
        {
            InitializeComponent( );
            InitConexionDB( );

            // TODO: Complete member initialization
            this.sqlQuery = sql;

            CommonUtils.ConexionBD.AbrirConexion( );
           
            gridControl.MainView = cardView;
            SqlCommand cmdProduct = new SqlCommand( this.sqlQuery , CommonUtils.ConexionBD.Conexion );
            DataSet dataSet = new DataSet( );
            dataSet.Load( cmdProduct.ExecuteReader( ) , LoadOption.OverwriteChanges , "Producto" );
           
            gridControl.DataSource = dataSet.Tables[ "Producto" ];

            ColumnView view = this.gridControl.MainView as ColumnView;
            view.Columns.Add( );
            DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit rPic = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit( );
            rPic.NullText = "No Image";
            rPic.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            view.Columns[ 1 ].ColumnEdit = rPic;
            view.Columns[ 1 ].GetBestWidth( );

            /*
             * Make just the Default columns visible. Imagen and Precio de Venta.
             */
            view.Columns[0].Visible = false;
            view.Columns[2].Visible = false;
            view.Columns[3].Visible = false;
            view.Columns[4].Visible = false;
            view.Columns[5].Visible = false;
            view.Columns[6].Visible = false;
            view.Columns[8].Visible = false;
            /*
             * The user can make them visible if needed.
             */
            this.gridControl.RepositoryItems.Add( view.Columns[ 1 ].ColumnEdit );

            CommonUtils.ConexionBD.CerrarConexion( );
        }

        private void InitConexionDB( )
        {
            CommonUtils.ConexionBD.StringDeConexion = CommonUtils.ConexionBD.GetConnectionString( );
        }

        private void cardView_DoubleClick( object sender , EventArgs e )
        {
            CardView cardView = ( sender as CardView );

            if ( cardView == null )
                return;

            DataRow dataRow = cardView.GetFocusedDataRow( );

            ventas = new CommonUtils.Ventas( );
            ventas.ProductoID = Convert.ToInt32( dataRow.ItemArray[ 0 ] );
            ventas.Articulo = Convert.ToString( dataRow.ItemArray[ 2 ] );
            ventas.Precio = Convert.ToDouble( dataRow.ItemArray[ 7 ] );
            this.Process( );
        }

        // Define a delegate named LogHandler, which will encapsulate
        // any method that takes a string as the parameter and returns no value
        public delegate void SalesHandler(GridProductos gridProductos);

        // Define an Event based on the above Delegate
        public event SalesHandler Sales;

        // Instead of having the Process() function take a delegate
        // as a parameter, we've declared a Log event. Call the Event,
        // using the OnXXXX Method, where XXXX is the name of the Event.
        public void Process( )
        {
            OnLog( this );
        }

        private void OnLog(GridProductos gridProductos)
        {
            if (Sales != null)
            {
                Sales(gridProductos);
            }
        }

        private void cardView_CustomDrawCardCaption(object sender, CardCaptionCustomDrawEventArgs e)
        {
            DevExpress.XtraGrid.Views.Card.CardView view = sender as DevExpress.XtraGrid.Views.Card.CardView;
            (e.CardInfo as DevExpress.XtraGrid.Views.Card.ViewInfo.CardInfo).CaptionInfo.CardCaption = view.GetRowCellDisplayText(e.RowHandle, view.Columns["Nombre"]);
        }

        private void cardView_CustomDrawCardFieldCaption(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.DisplayText.ToString().Contains("Imagen"))
                e.DisplayText = string.Empty;
        }
    }
}
