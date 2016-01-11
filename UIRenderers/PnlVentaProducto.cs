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
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors;

namespace UIRenderers
{
    public partial class PnlVentaProducto : UserControl
    {
        private decimal costoUnitario;

        public PnlVentaProducto( )
        {
            InitializeComponent( );
            InitConexionDB( );
            InitLookUpEdit( );
            InitLookUpEvents( );
        }

        private void InitLookUpEvents( )
        {
            this.gridColArticulo.ColumnEdit.EditValueChanged += new EventHandler( ColumnEdit_EditValueChanged );
            this.gridColCantidad.View.CellValueChanging += new CellValueChangedEventHandler( View_CellValueChanging );
        }

        void View_CellValueChanging( object sender , CellValueChangedEventArgs e )
        {
            if ( Convert.ToDecimal( e.Value ) == 1 )
                return;

            if ( Convert.ToDecimal( e.Value ) < 1 )
            {
                this.gridView1.SetRowCellValue( -2147483647 , this.gridView1.Columns[ 1 ].FieldName , 1 );
                return;
            }

            decimal subTotal = Convert.ToDecimal( e.Value ) * costoUnitario;
            this.gridView1.SetRowCellValue( -2147483647 , this.gridView1.Columns[ 4 ].FieldName , subTotal );
        }

        void ColumnEdit_EditValueChanged( object sender , EventArgs e )
        {
            GridLookUpEdit gle = ( sender as GridLookUpEdit );

            if ( string.IsNullOrEmpty( gle.Text ) )
                return;

            SetPrecioUnitario( gle );
        }

        private void SetPrecioUnitario( GridLookUpEdit gle )
        {
            CommonUtils.ConexionBD.AbrirConexion( );

            string sqlVentas = "SELECT PrecioVenta FROM Producto WHERE ProductoId =" + gle.EditValue.ToString();

            SqlDataReader reader = CommonUtils.ConexionBD.EjecutarConsultaReader( sqlVentas );
            
            if ( reader.HasRows )
            {
                while ( reader.Read( ) )
                {
                    costoUnitario = Convert.ToDecimal( reader.GetValue( 0 ) );

                    this.gridView1.SetRowCellValue( -2147483647 , this.gridView1.Columns[ 2 ].FieldName , reader.GetValue( 0 ) );
                    this.gridView1.SetRowCellValue( -2147483647 , this.gridView1.Columns[ 1 ].FieldName , 1 );
                    this.gridView1.SetRowCellValue( -2147483647 , this.gridView1.Columns[ 4 ].FieldName , reader.GetValue( 0 ) );
                }
            }
            CommonUtils.ConexionBD.CerrarConexion( );
        }

        private void InitConexionDB( )
        {
            CommonUtils.ConexionBD.StringDeConexion = CommonUtils.ConexionBD.GetConnectionString( );
        }

        private void InitLookUpEdit( )
        {
            CommonUtils.ConexionBD.AbrirConexion( );

            string sqlProductData = "SELECT Producto.Imagen, Producto.Nombre, Producto.ProductoId " +
               "From Producto";
            
            string sqlVentas = "SELECT Articulo, Cantidad, Preciounitario , Descuento, Subtotal FROM Ventas";

            SqlCommand cmdProduct = new SqlCommand( sqlProductData , CommonUtils.ConexionBD.Conexion );
            SqlCommand cmdVentas = new SqlCommand( sqlVentas , CommonUtils.ConexionBD.Conexion );

            DataSet dataSet = new DataSet( );
            dataSet.Load( cmdProduct.ExecuteReader( ) , LoadOption.OverwriteChanges , "Producto" );
            dataSet.Load( cmdVentas.ExecuteReader( ) , LoadOption.OverwriteChanges , "Ventas" );

            this.gridVentas.DataSource = dataSet.Tables[ "Ventas" ];
            this.repositoryItemGridLookUpEdit1.DisplayMember = "Nombre";
            this.repositoryItemGridLookUpEdit1.DataSource = dataSet.Tables[ "Producto" ];
            this.repositoryItemGridLookUpEdit1.ValueMember = "ProductoId";

            ColumnView view = this.repositoryItemGridLookUpEdit1.View as ColumnView;
            view.Columns.Add( );
            DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit rPic = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit( );
            rPic.NullText = "No Image";
            rPic.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            view.Columns[ 0 ].ColumnEdit = rPic;
            view.DetailHeight = 150;
            view.Columns[ 0 ].GetBestWidth( );

            this.repositoryItemGridLookUpEdit1.RepositoryItems.Add( view.Columns[ 0 ].ColumnEdit );
            
            // No queremos que la columna ProductoId sea visieble, la necesitamos para ejecutar un Query;
            this.repositoryItemGridLookUpEdit1.View.Columns[ "ProductoId" ].Visible = false;

            CommonUtils.ConexionBD.CerrarConexion( );
        }
    }
}
