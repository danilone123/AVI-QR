using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using DevExpress.XtraGrid.Views.Base;
using System.Drawing.Printing;
using DevExpress.XtraPrinting;
using System.Diagnostics;
using System.Drawing;
using CommonUtils;

namespace UIRenderers
{
    public partial class PnlListaProductos : UserControl
    {
        ProgressForm progressForm;
        private static log4net.ILog log = log4net.LogManager.GetLogger( "PnlListaProductos" );

        public string MessageBarValue { get; set; }

        #region Init UI
        public PnlListaProductos( )
        {
            InitializeComponent( );
            log4net.Config.XmlConfigurator.Configure( );
            InitConexionDB( );
        }
        #endregion

        #region Methods
        private void InitConexionDB( )
        {
            CommonUtils.ConexionBD.StringDeConexion = CommonUtils.ConexionBD.GetConnectionString();//GetConnectionString( );
        }

        private void InitializeGrid( )
        {
            CommonUtils.ConexionBD.AbrirConexion( );

            string sqlProductData = "SELECT Producto.ProductoId, Producto.Imagen, Producto.Nombre, Producto.Descripcion, Producto.Tipo, " +
                "Categoria.Tipo AS [Categoria], Producto.Tamano AS Tamaño, Producto.PrecioVenta AS [Precio de Venta], Producto.RecetaId, Producto.Mostrar AS [Visible] From Producto, Categoria "+
                "WHERE Producto.CategoriaId=Categoria.CategoriaId";
            
            string sqlProductCombo = "SELECT Producto.Nombre, Producto_Producto.ComboId FROM Producto INNER JOIN Producto_Producto ON Producto.ProductoId = Producto_Producto.ProductoId";

            try
            {
                SqlCommand cmdProduct = new SqlCommand( sqlProductData , CommonUtils.ConexionBD.Conexion );
                SqlCommand cmdCombo = new SqlCommand( sqlProductCombo , CommonUtils.ConexionBD.Conexion );

                DataSet dataSet = new DataSet( );
                dataSet.Load( cmdProduct.ExecuteReader( ) , LoadOption.OverwriteChanges , "Producto" );
                dataSet.Load( cmdCombo.ExecuteReader( ) , LoadOption.OverwriteChanges , "Combo" );

                dataSet.Relations.Add( "Combo" ,
                    dataSet.Tables[ "Producto" ].Columns[ "ProductoId" ] ,
                    dataSet.Tables[ "Combo" ].Columns[ "ComboId" ] , false );

                gridProductos.DataSource = dataSet.Tables[ "Producto" ];

                ColumnView view = this.gridProductos.MainView as ColumnView;
                view.Columns.Add( );
                view.ActiveFilterEnabled = true;
                DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit rPic = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit( );
                rPic.NullText = "No Image";
                rPic.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
                view.Columns[ 1 ].ColumnEdit = rPic;
                view.Columns[ 1 ].GetBestWidth( );
                view.Columns[ 8 ].Visible = false;
                view.Columns[9].Visible = true;
                
                this.gridProductos.RepositoryItems.Add( view.Columns[ 1 ].ColumnEdit );

                MessageBarValue = dataSet.Tables[ "Producto" ].Rows.Count.ToString( ) + " Items";
            }
            catch ( Exception ex )
            {
                log.Error( ex.Message , ex );
                MessageBarValue = "No se pudieron listar los productos. Hubo un error: " + ex.Message;
            }
            finally
            {
                barItem.Caption = MessageBarValue;
            }
            
            CommonUtils.ConexionBD.CerrarConexion( );
        }

        private void InitializeGridImages( )
        {
           //
        }

        private static string GetConnectionString( )
        {
            return @"Data Source=Marcelo-PC\SQLEXPRESS;Database=Capresso;Initial Catalog=Capresso;Integrated Security=SSPI;";
        }

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
        #endregion

        #region Events & Handlers
        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad( e );
            
            InitializeGrid( );
            InitializeGridImages( );
        }

        private void chkGridView_ItemClick( object sender , DevExpress.XtraBars.ItemClickEventArgs e )
        {
            chkGridView.Checked = true;
            chkCardView.Checked = false;

            gridProductos.MainView = gridViewProductos;
        }

        private void chkCardView_ItemClick( object sender , DevExpress.XtraBars.ItemClickEventArgs e )
        {
            chkCardView.Checked = true;
            chkGridView.Checked = false;

            gridProductos.MainView = cardView;
            InitializeGrid( );
        }

        private void barButtonPdf_ItemClick( object sender , DevExpress.XtraBars.ItemClickEventArgs e )
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog( );
            saveFileDialog.InitialDirectory = Environment.GetFolderPath( Environment.SpecialFolder.MyDocuments );
            saveFileDialog.Filter = "Archivo PDF (*.pdf)|*.pdf";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.FileName = "Productos.pdf";
            saveFileDialog.Title = "Guardar como";
            saveFileDialog.RestoreDirectory = true;

            if ( saveFileDialog.ShowDialog( ) == DialogResult.OK || saveFileDialog.ShowDialog( ) == DialogResult.Yes )
            {
                try
                {
                    StartExport();

                    Cursor currentCursor = Cursor.Current;
                    Cursor.Current = Cursors.WaitCursor;
                    DevExpress.XtraPrinting.IPrintingSystem ps = DevExpress.XtraPrinting.PrintHelper.GetCurrentPS();
                    ps.AfterChange += new DevExpress.XtraPrinting.ChangeEventHandler(Export_ProgressEx);
                    gridProductos.ExportToPdf( saveFileDialog.FileName );
                    ps.AfterChange -= new DevExpress.XtraPrinting.ChangeEventHandler(Export_ProgressEx);
                    Cursor.Current = currentCursor;

                    EndExport();
                    
                    
                    Process.Start( saveFileDialog.FileName );
                    MessageBarValue = string.Empty;
                }
                catch ( Exception ex )
                {
                    log.Error( ex.Message , ex );
                    MessageBarValue = " No se pudo guardar a la ubicación. " + ex.Message;
                }
                finally
                {
                    barItem.Caption = MessageBarValue;
                }
            }
        }

        private void barButtonXls_ItemClick( object sender , DevExpress.XtraBars.ItemClickEventArgs e )
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog( );
            saveFileDialog.InitialDirectory = Environment.GetFolderPath( Environment.SpecialFolder.MyDocuments );
            saveFileDialog.Filter = "Archivo XLS (*.xls)|*.xls";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.FileName = "Productos.xls";
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
                    
                    gridProductos.ExportToXls( saveFileDialog.FileName );
                    
                    ps.AfterChange -= new DevExpress.XtraPrinting.ChangeEventHandler( Export_ProgressEx );
                    Cursor.Current = currentCursor;

                    EndExport( );
                    
                    Process.Start( saveFileDialog.FileName );
                    MessageBarValue = string.Empty;
                }
                catch ( Exception ex )
                {
                    log.Error( ex.Message , ex );
                    MessageBarValue = " No se pudo guardar a la ubicación. " + ex.Message;
                }
                finally
                {
                    barItem.Caption = MessageBarValue;
                }
            }
        }

        private void barButtonXlsx_ItemClick( object sender , DevExpress.XtraBars.ItemClickEventArgs e )
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog( );
            saveFileDialog.InitialDirectory = Environment.GetFolderPath( Environment.SpecialFolder.MyDocuments );
            saveFileDialog.Filter = "Archivo XLSX (*.xlsx)|*.xlsx";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.FileName = "Productos.xlsx";
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

                    gridProductos.ExportToXlsx( saveFileDialog.FileName );

                    ps.AfterChange -= new DevExpress.XtraPrinting.ChangeEventHandler( Export_ProgressEx );
                    Cursor.Current = currentCursor;

                    EndExport( );

                    Process.Start( saveFileDialog.FileName );
                    MessageBarValue = string.Empty;
                }
                catch ( Exception ex )
                {
                    log.Error( ex.Message , ex );
                    MessageBarValue = " No se pudo guardar a la ubicación. " + ex.Message;
                }
                finally
                {
                    barItem.Caption = MessageBarValue;
                }
            }
        }

        private void barButtonUp_ItemClick( object sender , DevExpress.XtraBars.ItemClickEventArgs e )
        {
            barButtonDown.Enabled = true;

            if ( gridViewProductos.FocusedRowHandle > 1 )
                gridViewProductos.MovePrev( );
            else
            {
                gridViewProductos.MovePrev( );
                barButtonUp.Enabled = false;
            }
        }

        private void barButtonDown_ItemClick( object sender , DevExpress.XtraBars.ItemClickEventArgs e )
        {
            barButtonUp.Enabled = true;

            if ( gridViewProductos.FocusedRowHandle + 1 < gridViewProductos.RowCount - 1 )
                gridViewProductos.MoveNext( );
            else
            {
                gridViewProductos.MoveNext( );
                barButtonDown.Enabled = false;
            }
        }

        private void barButtonPrint_ItemClick( object sender , DevExpress.XtraBars.ItemClickEventArgs e )
        {
            gridProductos.ShowPrintPreview( );
        }

        private void barButtonAdd_ItemClick( object sender , DevExpress.XtraBars.ItemClickEventArgs e )
        {
            //if ( !( this.ParentForm as mainForm ).ContextControls.ContainsKey( "PnlNuevoProducto" ) )
            {
                PnlNuevoProducto pnlProducto = new PnlNuevoProducto( );
                pnlProducto.Dock = DockStyle.Fill;
                DevExpress.XtraTab.XtraTabPage tabItem = new DevExpress.XtraTab.XtraTabPage( );
                tabItem.Controls.Add( pnlProducto );
                tabItem.Text = "Nuevo Producto";
                ( this.ParentForm as mainForm ).xtraTabControl.TabPages.Add( tabItem );
                ( this.ParentForm as mainForm ).xtraTabControl.SelectedTabPage = tabItem;

               // ( this.ParentForm as mainForm ).ContextControls.Add( "PnlNuevoProducto" , pnlProducto );
            }
        }

        private void barButtonEdit_ItemClick( object sender , DevExpress.XtraBars.ItemClickEventArgs e )
        {
            DataRowView selectedRow;

            if ( chkGridView.Checked )
                selectedRow = ( DataRowView ) gridViewProductos.GetFocusedRow( );
            else
                selectedRow = ( DataRowView ) cardView.GetFocusedRow( );

            if ( selectedRow != null )
            {
                CommonUtils.Producto producto = new CommonUtils.Producto( );
                producto.ProductoID = Convert.ToInt32( selectedRow.Row.ItemArray[ 0 ] );
                producto.Imagen = ( byte[ ] ) selectedRow.Row.ItemArray[ 1 ];
                producto.Nombre = ( string ) selectedRow.Row.ItemArray[ 2 ];
                producto.Descripcion = ( string ) selectedRow.Row.ItemArray[ 3 ];
                producto.Tipo = ( string ) selectedRow.Row.ItemArray[ 4 ];
                producto.Categoria = ( string ) selectedRow.Row.ItemArray[ 5 ];
                producto.Size = ( string ) selectedRow.Row.ItemArray[ 6 ];
                producto.PrecioVenta = Convert.ToDecimal( selectedRow.Row.ItemArray[ 7 ] );
                producto.RecetaID = Convert.ToInt32( selectedRow.Row.ItemArray[ 8 ] );
                producto.Visible = selectedRow.Row.ItemArray[9]==null?false:(Boolean)selectedRow.Row.ItemArray[9];

                //if ( !( this.ParentForm as mainForm ).ContextControls.ContainsKey( "PnlNuevoProducto" ) )
                if (!(this.ParentForm as mainForm).ContextControlsForProductos.ContainsKey(producto.ProductoID.ToString()))
                {
                    PnlNuevoProducto pnlProducto = new PnlNuevoProducto( producto );
                    (this.ParentForm as mainForm).ContextControlsForProductos.Add(producto.ProductoID.ToString(), pnlProducto);

                    pnlProducto.Dock = DockStyle.Fill;
                    DevExpress.XtraTab.XtraTabPage tabItem = new DevExpress.XtraTab.XtraTabPage( );
                    tabItem.Controls.Add( pnlProducto );
                    tabItem.Text = producto.Nombre;
                    ( this.ParentForm as mainForm ).xtraTabControl.TabPages.Add( tabItem );
                    ( this.ParentForm as mainForm ).xtraTabControl.SelectedTabPage = tabItem;

                  //  ( this.ParentForm as mainForm ).ContextControls.Add( "PnlNuevoProducto" , pnlProducto );
                }
            }
        }

        private void barButtonDelete_ItemClick( object sender , DevExpress.XtraBars.ItemClickEventArgs e )
        {
            DataRowView selectedRow;

            if ( chkGridView.Checked )
                selectedRow = ( DataRowView ) gridViewProductos.GetFocusedRow( );
            else
                selectedRow = ( DataRowView ) cardView.GetFocusedRow( );

            string sqlQuery;

            if ( selectedRow != null )
            {
                Producto producto = new Producto( );
                producto.ProductoID = Convert.ToInt32( selectedRow.Row.ItemArray[ 0 ] );
                producto.Nombre = ( string ) selectedRow.Row.ItemArray[ 2 ];
                producto.Categoria = ( string ) selectedRow.Row.ItemArray[ 5 ];
                //sqlQuery = "Select * from Ventas where InsumoId=" + insumoABorrar.idInsumo;
                //if ( CommonUtils.ConexionBD.EjecutarConsulta( sqlQuery ).Rows.Count > 0 || CommonUtils.ConexionBD.EjecutarConsulta( sqlQueryCompras ).Rows.Count > 0 )
                //{
                //    MessageBox.Show( this , "el insumo " + insumoABorrar.nombre + " no se puede borrar debido a que se encuentra asociado a otros datos." , "Insumos" , MessageBoxButtons.OK );
                //    return;
                //}
                sqlQuery = "select * from Pedido_Producto where ProductoId=" + producto.ProductoID;
                if (CommonUtils.ConexionBD.EjecutarConsulta(sqlQuery).Rows.Count > 0)
                {
                    MessageBox.Show(this, "el producto " + producto.Nombre + " no se puede borrar debido a que se encuentra asociado a otros datos.", "Productos", MessageBoxButtons.OK);
                        return; 
                }

                if ( MessageBox.Show( this , "Desea eliminar " + producto.Nombre + " ?" , "Productos" , MessageBoxButtons.YesNo ) == DialogResult.No )
                {
                    return;
                }
                else
                {
                    try
                    {
                        if ( producto.Categoria == "Combo" )
                        {
                            sqlQuery = "Delete from Producto_Producto where ComboId=" + producto.ProductoID;
                            CommonUtils.ConexionBD.Actualizar( sqlQuery );

                            sqlQuery = "Delete from Producto where ProductoId=" + producto.ProductoID;
                            CommonUtils.ConexionBD.Actualizar( sqlQuery );
                        }
                        else
                        {
                            sqlQuery = "Delete from Producto where ProductoId=" + producto.ProductoID;
                            CommonUtils.ConexionBD.Actualizar( sqlQuery );
                        }

                        MessageBarValue = producto.Nombre + " se elimino con éxito.";
                    }
                    catch ( Exception ex )
                    {
                        log.Error( ex.Message , ex );
                        MessageBarValue = " No se pudo completar la eliminación. " + ex.Message;
                    }
                    finally
                    {
                        barItemRight.Caption = MessageBarValue;
                    }
                }

                InitializeGrid( );
            }
        }

        private void gridProductos_MouseDoubleClick( object sender , MouseEventArgs e )
        {
            DataRowView selectedRow;

            if ( chkGridView.Checked )
                selectedRow = ( DataRowView ) gridViewProductos.GetFocusedRow( );
            else
                selectedRow = ( DataRowView ) cardView.GetFocusedRow( );

            if ( selectedRow != null )
            {
                CommonUtils.Producto producto = new CommonUtils.Producto( );
                producto.ProductoID = Convert.ToInt32( selectedRow.Row.ItemArray[ 0 ] );
                producto.Imagen = ( byte[ ] ) selectedRow.Row.ItemArray[ 1 ];
                producto.Nombre = ( string ) selectedRow.Row.ItemArray[ 2 ];
                producto.Descripcion = ( string ) selectedRow.Row.ItemArray[ 3 ];
                producto.Tipo = ( string ) selectedRow.Row.ItemArray[ 4 ];
                producto.Categoria = ( string ) selectedRow.Row.ItemArray[ 5 ];
                producto.Size = ( string ) selectedRow.Row.ItemArray[ 6 ];
                producto.PrecioVenta = Convert.ToDecimal( selectedRow.Row.ItemArray[ 7 ] );
                producto.RecetaID = Convert.ToInt32( selectedRow.Row.ItemArray[ 8 ] );
                producto.Visible = selectedRow.Row.ItemArray[9] == null ? false : (Boolean)selectedRow.Row.ItemArray[9];

                if ( !( this.ParentForm as mainForm ).ContextControls.ContainsKey( "PnlNuevoProducto" ) )
                {
                    PnlNuevoProducto pnlProducto = new PnlNuevoProducto( producto );
                    pnlProducto.Dock = DockStyle.Fill;
                    DevExpress.XtraTab.XtraTabPage tabItem = new DevExpress.XtraTab.XtraTabPage( );
                    tabItem.Controls.Add( pnlProducto );
                    tabItem.Text = "Nuevo Producto";
                    ( this.ParentForm as mainForm ).xtraTabControl.TabPages.Add( tabItem );
                    ( this.ParentForm as mainForm ).xtraTabControl.SelectedTabPage = tabItem;

                    ( this.ParentForm as mainForm ).ContextControls.Add( "PnlNuevoProducto" , pnlProducto );
                }
            }
        }

        private void gridProductos_KeyDown( object sender , KeyEventArgs e )
        {
            if ( e.KeyData == Keys.Delete )
            {
                barButtonDelete_ItemClick( null , null );
            }
            else if ( e.KeyData == Keys.Insert )
            {
                barButtonAdd_ItemClick( null , null );
            }
        }
        #endregion

        private void barButtonRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InitializeGrid();
        }
    }
}
