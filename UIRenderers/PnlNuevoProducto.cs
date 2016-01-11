using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using DevExpress.XtraEditors.DXErrorProvider;
using System.Data.SqlClient;
using System.Drawing.Drawing2D;
using CommonUtils;
using System.Linq;
using System.Globalization;

namespace UIRenderers
{
    public partial class PnlNuevoProducto : DevExpress.XtraEditors.XtraUserControl
    {
        private const int MAX_IMAGE_WIDTH_SIZE = 285;
        private const int MAX_IMAGE_HEIGHT_SIZE = 160;
        private DataTable dataTable = new DataTable( );
        private static log4net.ILog log = log4net.LogManager.GetLogger( "PnlNuevoProducto" );
        
        public string MessageBarValue { get; set; }
        public Producto Producto { get; set; }
        public bool IsEditing { get; set; }
        Dictionary<int,int> lasValueIntableCombo=new Dictionary<int,int>();//variable used in order to avoid problems creating or modyfing combos
        Dictionary<int, bool> lastValueInTableForCheckedState = new Dictionary<int, bool>();// variable used in order to avoid problems with combo, when the focus is on check value position

        public PnlNuevoProducto( )
        {
            InitializeComponent( );
            
            log4net.Config.XmlConfigurator.Configure( );
            InitConexionDB( );
            InitDataTable( );
            this.IsEditing = false;
        }

        public PnlNuevoProducto( Producto producto )
        {
            InitializeComponent( );
            InitConexionDB( );
            InitDataTable( );

            this.Producto = producto;

            this.txtNomProducto.EditValue = Producto.Nombre;
            this.txtNomProducto.Text = Producto.Nombre;
            this.txtDescripcion.Text = Producto.Descripcion;
            this.txtPrice.EditValue = Producto.PrecioVenta;
            this.checkBoxVisible.Checked = this.Producto.Visible;

            ValidateFields( );

            MemoryStream ms = new MemoryStream( Producto.Imagen );
            Bitmap imagen = new Bitmap( ms );
            picBoxImagen.Image = imagen;

            this.IsEditing = true;
        }

        private Image Image
        {
            get
            {
                return imageCollectionProducto.Images[0];
            }
        }

        #region Methods
        private void  InitConexionDB()
        {
            CommonUtils.ConexionBD.StringDeConexion = CommonUtils.ConexionBD.GetConnectionString( );
        }

        private void InitDataTable( )
        {
            this.dataTable.Columns.Add( "Seleccionado" , typeof( bool ) );
            this.dataTable.Columns.Add( "Producto ID" , typeof( int ) );
            this.dataTable.Columns.Add( "Articulo" , typeof( string ) );
            this.dataTable.Columns.Add( "Cantidad" , typeof( int ) );

          //  this.gridColCantidad.View.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler( View_CellValueChanging );
        }

        private void View_CellValueChanging( object sender , DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e )
        {
            
        }

        private void InitFields( )
        {
            txtNomProducto.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            groupBoxCombos.Visible = false;

            InitializeDropDowns( );
            InitializeGrid( );
            InitializeCombos( );
        }

        private void InitializeDropDowns( )
        {
            GetCategories( );
            GetRecipes( );
        }
        /*
        private void InitializeCombos( )
        {
            if ( IsEditing )
            {
                try
                {
                    string sql = "SELECT Producto.ProductoId, Producto.Nombre, Producto_Producto.Cantidad FROM Producto INNER JOIN "
                        + "Producto_Producto ON Producto.ProductoId = Producto_Producto.ProductoId "
                        + "WHERE (Producto_Producto.ComboId = " + Producto.ProductoID + ")";

                    CommonUtils.ConexionBD.AbrirConexion( );
                    DataRow dataRow;
                    SqlDataReader reader = CommonUtils.ConexionBD.EjecutarConsultaReader( sql );
                    Dictionary<int, string> arrayTemp = new Dictionary<int, string>();
                    if ( reader.HasRows )
                    {
                        while ( reader.Read( ) )
                        {
                            dataRow = dataTable.NewRow( );
                            dataRow.SetField( "Seleccionado" , true );
                            dataRow.SetField( "Producto ID" , reader.GetValue( 0 ) );
                            dataRow.SetField( "Articulo" , reader.GetValue( 1 ) );
                            dataRow.SetField( "Cantidad" , reader.GetValue( 2 ) );
                            dataTable.Rows.Add( dataRow );
                        }
                    }

                    reader.Close( );

                    sql = "SELECT ProductoId, Nombre From Producto WHERE CategoriaId != 5 AND ProductoId!=" + Producto.ProductoID;
                    CommonUtils.ConexionBD.AbrirConexion( );
                    reader = CommonUtils.ConexionBD.EjecutarConsultaReader( sql );

                    if ( reader.HasRows )
                    {
                        while ( reader.Read( ) )
                        {
                            dataRow = dataTable.NewRow( );
                            dataRow.SetField( "Seleccionado" , false );
                            dataRow.SetField( "Producto ID" , reader.GetValue( 0 ) );
                            dataRow.SetField( "Articulo" , reader.GetValue( 1 ) );
                            dataRow.SetField( "Cantidad" , 1 );
                            dataTable.Rows.Add( dataRow );
                        }
                    }
                }
                catch ( Exception ex )
                {
                    log.Error( ex.Message , ex );
                    MessageBarValue = " No se pudo cargar la tabla de Combos. " + ex.Message;
                }
                finally
                {
                    barItem.Caption = MessageBarValue;
                }
            }
            else
            {
                try
                {
                    string sql = "SELECT ProductoId, Nombre From Producto WHERE CategoriaId != 5";
                    CommonUtils.ConexionBD.AbrirConexion( );
                    DataRow dataRow;
                    SqlDataReader reader = CommonUtils.ConexionBD.EjecutarConsultaReader( sql );

                    if ( reader.HasRows )
                    {
                        while ( reader.Read( ) )
                        {
                            dataRow = dataTable.NewRow( );
                            dataRow.SetField( "Seleccionado" , false );
                            dataRow.SetField( "Producto ID" , reader.GetValue( 0 ) );
                            dataRow.SetField( "Articulo" , reader.GetValue( 1 ) );
                            dataRow.SetField( "Cantidad" , 1 );
                            dataTable.Rows.Add( dataRow );
                        }
                    }
                }
                catch ( Exception ex )
                {
                    log.Error( ex.Message , ex );
                    MessageBarValue = " No se pudo cargar la tabla de Combos. " + ex.Message;
                }
                finally
                {
                    barItem.Caption = MessageBarValue;
                }
            }

            gridCombos.DataSource = this.dataTable;

            CommonUtils.ConexionBD.CerrarConexion( );

        }
        */

        private void InitializeCombos()
        {
            if (IsEditing)
            {
                try
                {
                    string sql = "SELECT Producto.ProductoId, Producto.Nombre,Producto_Producto.Cantidad FROM Producto INNER JOIN "
                        + "Producto_Producto ON Producto.ProductoId = Producto_Producto.ProductoId "
                        + "WHERE (Producto_Producto.ComboId = " + Producto.ProductoID + ")";

                    CommonUtils.ConexionBD.AbrirConexion();
                    DataRow dataRow;
                    SqlDataReader reader = CommonUtils.ConexionBD.EjecutarConsultaReader(sql);
                    Dictionary<int, string> arrayTemp = new Dictionary<int, string>();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {/*
                            dataRow = dataTable.NewRow( );
                            dataRow.SetField( "Seleccionado" , true );
                            dataRow.SetField( "Producto ID" , reader.GetValue( 0 ) );
                            dataRow.SetField( "Articulo" , reader.GetValue( 1 ) );
                            dataRow.SetField( "Cantidad" , 1 );
                            dataTable.Rows.Add( dataRow );*/
                            arrayTemp.Add((int)reader.GetDecimal(0), reader.GetInt32(2).ToString());
                        }
                    }
                    reader.Close();
                    sql = "SELECT ProductoId, Nombre From Producto WHERE CategoriaId != 5 AND CategoriaId != 6 AND ProductoId!=" + Producto.ProductoID;
                    SqlDataReader readerFull = CommonUtils.ConexionBD.EjecutarConsultaReader(sql);
                    if (readerFull.HasRows)
                    {
                        while (readerFull.Read())
                        {

                            dataRow = dataTable.NewRow();
                            if (arrayTemp.ContainsKey((int)readerFull.GetDecimal(0)))
                            {
                                dataRow.SetField("Seleccionado", true);
                                dataRow.SetField("Cantidad", int.Parse(arrayTemp[(int)readerFull.GetDecimal(0)]));
                            }
                            else
                            {
                                dataRow.SetField("Seleccionado", false);
                                dataRow.SetField("Cantidad", 1);
                            }
                            //dataRow.SetField("Seleccionado", false);
                            dataRow.SetField("Producto ID", readerFull.GetValue(0));
                            dataRow.SetField("Articulo", readerFull.GetValue(1));
                            //  dataRow.SetField("Cantidad", );
                            dataTable.Rows.Add(dataRow);

                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message, ex);
                    MessageBarValue = " No se pudo cargar la tabla de Combos. " + ex.Message;
                }
                finally
                {
                    barItem.Caption = MessageBarValue;
                }
            }
            else
            {
                try
                {
                    // No Mostramos tipo: Combos & Otros
                    string sql = "SELECT ProductoId, Nombre From Producto WHERE CategoriaId != 5 AND CategoriaId != 6";
                    CommonUtils.ConexionBD.AbrirConexion();
                    DataRow dataRow;
                    SqlDataReader reader = CommonUtils.ConexionBD.EjecutarConsultaReader(sql);

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            dataRow = dataTable.NewRow();
                            dataRow.SetField("Seleccionado", false);
                            dataRow.SetField("Producto ID", reader.GetValue(0));
                            dataRow.SetField("Articulo", reader.GetValue(1));
                            dataRow.SetField("Cantidad", 1);
                            dataTable.Rows.Add(dataRow);

                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message, ex);
                    MessageBarValue = " No se pudo cargar la tabla de Combos. " + ex.Message;
                }
                finally
                {
                    barItem.Caption = MessageBarValue;
                }
            }

            gridCombos.DataSource = this.dataTable;

            CommonUtils.ConexionBD.CerrarConexion();

        }

        private void GetRecipes( )//cuando  combo no tenga receta este metodo se tiene q cambiar! FIXME
        {
            /*
             * Inicializando el DropDown de Recetas
             */
            CommonUtils.ConexionBD.AbrirConexion( );
            string sql = "SELECT RecetaId, Nombre From Receta";
            DataTable tablaReceta = CommonUtils.ConexionBD.EjecutarConsulta( sql );
            int rowIndex = 1;
            if (IsEditing)
            {
                DataRow[] selectedRow = tablaReceta.Select("RecetaId=" + Producto.RecetaID);// --> Con esta linea y la de abajo obtenemos el index que queremos para el lookupreceta
                
                rowIndex = tablaReceta.Rows.IndexOf(selectedRow[0]);
                
            }
           
            lookUpReceta.Properties.DataSource = tablaReceta;
            lookUpReceta.Properties.DisplayMember = "Nombre";
            lookUpReceta.Properties.ValueMember = "RecetaId";
            lookUpReceta.ItemIndex = rowIndex;



            CommonUtils.ConexionBD.CerrarConexion( );
        }


       private void GetRecipes1( )
        {
            /*
             * Inicializando el DropDown de Recetas
             */
            
            CommonUtils.ConexionBD.AbrirConexion( );
            string sql = "SELECT RecetaId, Nombre From Receta";
            DataTable tablaReceta = CommonUtils.ConexionBD.EjecutarConsulta( sql );
            

            lookUpReceta.Properties.DataSource = tablaReceta;
            lookUpReceta.Properties.DisplayMember = "Nombre";
            lookUpReceta.Properties.ValueMember = "RecetaId";
            lookUpReceta.ItemIndex = 1;

            CommonUtils.ConexionBD.CerrarConexion( );
             
        }
        
        private void GetCategories( )
        {
            /*
             * Inicializando el DropDown de Categorias
             */
            CommonUtils.ConexionBD.AbrirConexion( );
            string sql = "SELECT CategoriaId, Tipo From Categoria";
            DataTable tablaCategoria = CommonUtils.ConexionBD.EjecutarConsulta( sql );

            lookUpCategoria.Properties.DataSource = tablaCategoria;
            lookUpCategoria.Properties.DisplayMember = "Tipo";
            lookUpCategoria.Properties.ValueMember = "CategoriaId";

            if ( IsEditing )
            {
                string expression;
                expression = "Tipo = '" + Producto.Categoria + "'";
                DataRow[ ] foundRows;
                // Use the Select method to find all rows matching the filter.
                foundRows = tablaCategoria.Select( expression );
                lookUpCategoria.ItemIndex = Convert.ToInt32( foundRows[ 0 ].ItemArray[ 0 ] ) - 2;
            }
            else
                lookUpCategoria.ItemIndex = 0;

            CommonUtils.ConexionBD.CerrarConexion( );
        }

        private void InitializeGrid( )
        {
            CommonUtils.ConexionBD.AbrirConexion( );
            string sql = "SELECT Categoria.Tipo as Categoria, Producto.Nombre as Artículo From Producto, Categoria WHERE Producto.CategoriaId = Categoria.CategoriaId";
            DataTable table = CommonUtils.ConexionBD.EjecutarConsulta( sql );
            gridControl.DataSource = table;

            CommonUtils.ConexionBD.CerrarConexion( );
        }

        private void ValidateFields( )
        {
            ValidateEmptyStringRule( txtNomProducto );
            ValidateLessThanMinRule( txtPrice , Decimal.Zero );
        }

        private void ValidateEmptyStringRule( BaseEdit control )
        {
            if ( control.Text == null || control.Text.Trim( ).Length == 0 )
                dxErrorProvider.SetError( control , "Este campo no puede ser nulo" , ErrorType.Critical );
            else
                dxErrorProvider.SetError( control , "" );
        }

        private void ValidateLessThanMinRule( BaseEdit control , Decimal min )
        {
            if ( !( control.EditValue is Decimal ) )
                return;

            if ( ( Decimal ) control.EditValue <= min )
                dxErrorProvider.SetError( control , "Porfavor, ingrese un valor mayor a " + min.ToString( ) , ErrorType.Warning );
            else
                dxErrorProvider.SetError( control , "" );
        }

        private void GetErrorProviderMessages( )
        {
            IList<Control> controlErrors = dxErrorProvider.GetControlsWithError( );
            MessageBox.Show( this , ( string ) controlErrors[ 0 ].Tag , dxErrorProvider.GetError( controlErrors[ 0 ] ) , MessageBoxButtons.OK , MessageBoxIcon.Error );
        }

         private void SaveProduct( )
        {
            CommonUtils.ConexionBD.AbrirConexion( );

            var tipo = cmboxTipo.SelectedItem;
            var tamanio = cmboxTamanio.SelectedItem;

            if ( lookUpCategoria.Text.ToLower( CultureInfo.InvariantCulture ) != "bebida" )
            {
                tipo = DBNull.Value;
                tamanio = DBNull.Value;
            }

            if ( IsEditing )
            {
                try
                {
                    string sql = "UPDATE Producto SET Nombre='" + StringUtils.EscapeSQL( txtNomProducto.Text ) + "', Descripcion='" +
                    StringUtils.EscapeSQL( txtDescripcion.Text ) + "', Tipo='" + tipo + "', Tamano='" +
                    tamanio + "', PrecioVenta='" + txtPrice.EditValue.ToString( ) + "', Imagen=" + "@pic" + ", CategoriaId='" +
                     ( lookUpCategoria.GetSelectedDataRow( ) as DataRowView ).Row.ItemArray[ 0 ] + "', RecetaId='" +
                     (lookUpReceta.GetSelectedDataRow() as DataRowView).Row.ItemArray[0] + "', FechaCreacion='" + DateTime.Now + "', FechaModificada='" + DateTime.Now + "', Mostrar='" + checkBoxVisible.Checked + "' WHERE " +
                     " ProductoId=" + Producto.ProductoID;

                    MemoryStream ms = new MemoryStream( );
                    picBoxImagen.Image = this.ImageResize( picBoxImagen.Image );
                    picBoxImagen.Image.Save( ms , System.Drawing.Imaging.ImageFormat.Jpeg );

                    byte[ ] imageAsBytes = ms.ToArray( );
                    ms.Close( );

                    SqlParameter picparameter = new SqlParameter( );
                    picparameter.SqlDbType = SqlDbType.Image;
                    picparameter.ParameterName = "pic";
                    picparameter.Value = imageAsBytes;

                    SqlCommand cmd = new SqlCommand( sql , CommonUtils.ConexionBD.Conexion );
                    cmd.Parameters.Add( picparameter );
                    cmd.ExecuteNonQuery( );

                    CommonUtils.ConexionBD.CerrarConexion( );
                    sql = "delete from Producto_Producto where ComboId='" + Producto.ProductoID+"'";
                    CommonUtils.ConexionBD.Actualizar(sql);
                    if (groupBoxCombos.Visible)
                    {
                        foreach (DataRow item in dataTable.Rows)
                        {
                            bool isSelect = (bool)item.ItemArray[0];
                            if (lastValueInTableForCheckedState.ContainsKey(int.Parse(item.ItemArray[1].ToString())))
                            {
                                isSelect=lastValueInTableForCheckedState[int.Parse(item.ItemArray[1].ToString())];
                            }
                            if (isSelect == true)
                            {/*
                                sql = "UPDATE Producto_Producto SET ComboId='" + Producto.ProductoID + "',ProductoId='" + item.ItemArray[ 1 ] + "',Cantidad='" + item.ItemArray[ 3 ] +
                                    "'  WHERE ComboId=" + Producto.ProductoID;
                                CommonUtils.ConexionBD.Actualizar( sql );*/
                                int cantidadProducto =int.Parse( item.ItemArray[3].ToString());
                                if (lasValueIntableCombo.ContainsKey(int.Parse(item.ItemArray[1].ToString()))) {

                                    cantidadProducto = lasValueIntableCombo[int.Parse(item.ItemArray[1].ToString())];
                                }
                                sql = "INSERT INTO Producto_Producto (ComboId,ProductoId,Cantidad)  VALUES ('" +
                                  Producto.ProductoID + "','" + item.ItemArray[1] + "','" + cantidadProducto + "')";
                                CommonUtils.ConexionBD.Actualizar(sql);
                            }
                        }
                    }
                    alertControlProducto.Show(this.FindForm(), "Actualización satisfactoria.", "El producto " + txtNomProducto.Text +
                  " fue modificada (a) satisfactoriamente.", Image);
                 //   barButtonCerrar_ItemClick( null , null );
                }
                catch ( Exception ex )
                {
                    log.Error( ex.Message , ex );
                    MessageBarValue = " No se pudo actualizar el producto. " + ex.Message;
                }
                finally
                {
                    barItem.Caption = MessageBarValue;
                }
            }
            else
            {
                try
                {
                    string sql = "INSERT into Producto (Nombre, Descripcion, Tipo, Tamano, PrecioVenta, Imagen, CategoriaId, RecetaId, FechaCreacion, FechaModificada, Mostrar)"
                    + " values ('{0}','{1}','{2}','{3}','{4}',{5},'{6}','{7}','{8}','{9}','{10}');";

                    sql = string.Format( sql , StringUtils.EscapeSQL( txtNomProducto.Text ) , StringUtils.EscapeSQL( txtDescripcion.Text ) ,tipo ,
                        tamanio , txtPrice.EditValue.ToString( ) , "@pic" , ( lookUpCategoria.GetSelectedDataRow( ) as DataRowView ).Row.ItemArray[ 0 ] ,
                     (lookUpReceta.GetSelectedDataRow() as DataRowView).Row.ItemArray[0], DateTime.Now, DateTime.Now,checkBoxVisible.Checked);

                    MemoryStream ms = new MemoryStream( );
                    picBoxImagen.Image = this.ImageResize( picBoxImagen.Image );
                    picBoxImagen.Image.Save( ms , System.Drawing.Imaging.ImageFormat.Jpeg );

                    byte[ ] imageAsBytes = ms.ToArray( );
                    ms.Close( );

                    SqlParameter picparameter = new SqlParameter( );
                    picparameter.SqlDbType = SqlDbType.Image;
                    picparameter.ParameterName = "pic";
                    picparameter.Value = imageAsBytes;

                    SqlCommand cmd = new SqlCommand( sql , CommonUtils.ConexionBD.Conexion );
                    cmd.Parameters.Add( picparameter );
                    cmd.ExecuteNonQuery( );
                    cmd.CommandText = "SELECT @@IDENTITY";
                    decimal productoId = ( decimal ) ( cmd.ExecuteScalar( ) );

                    CommonUtils.ConexionBD.CerrarConexion( );

                    if ( groupBoxCombos.Visible )
                    {
                        foreach ( DataRow item in dataTable.Rows )
                        {
                         //   if ( ( bool ) item.ItemArray[ 0 ] == true )
                            bool isSelect = (bool)item.ItemArray[0];
                            if (lastValueInTableForCheckedState.ContainsKey(int.Parse(item.ItemArray[1].ToString())))
                            {
                                isSelect = lastValueInTableForCheckedState[int.Parse(item.ItemArray[1].ToString())];
                            }
                            if (isSelect == true)
                            {
                                int cantidadProducto = int.Parse(item.ItemArray[3].ToString());
                                if (lasValueIntableCombo.ContainsKey(int.Parse(item.ItemArray[1].ToString())))
                                {

                                    cantidadProducto = lasValueIntableCombo[int.Parse(item.ItemArray[1].ToString())];
                                }
                                sql = "INSERT INTO Producto_Producto (ComboId,ProductoId,Cantidad)  VALUES ('" +
                                   ( int ) productoId + "','" + item.ItemArray[ 1 ] + "','" + cantidadProducto + "')";
                                CommonUtils.ConexionBD.Actualizar( sql );
                            }
                        }
                    }
                    alertControlProducto.Show(this.FindForm(), "Creación satisfactoria.", "El producto" + txtNomProducto.Text +
                  " fue creado (a) satisfactoriamente.", Image);
                    this.ResetFields( );
                }
                catch ( Exception ex )
                {
                    log.Error( ex.Message , ex );
                    MessageBarValue = " No se pudo guardar el producto. " + ex.Message;
                }
                finally
                {
                    barItem.Caption = MessageBarValue;
                }
            }
        }
              
        /*
        private void SaveProduct( )
        {
            CommonUtils.ConexionBD.AbrirConexion( );

            if ( IsEditing )
            {
                try
                {
                    string sql = "UPDATE Producto SET Nombre='" + StringUtils.EscapeSQL( txtNomProducto.Text ) + "', Descripcion='" +
                    StringUtils.EscapeSQL( txtDescripcion.Text ) + "', Tipo='" + cmboxTipo.SelectedItem.ToString( ) + "', Tamano='" +
                    cmboxTamanio.SelectedItem.ToString( ) + "', PrecioVenta='" + txtPrice.EditValue.ToString()+ "', Imagen=" + "@pic" + ", CategoriaId='" +
                     ( lookUpCategoria.GetSelectedDataRow( ) as DataRowView ).Row.ItemArray[ 0 ] + "', RecetaId='" +
                     ( lookUpReceta.GetSelectedDataRow( ) as DataRowView ).Row.ItemArray[ 0 ] + "', FechaCreacion='" + DateTime.Now + "', FechaModificada='" + DateTime.Now + "' WHERE " +
                     " ProductoId=" + Producto.ProductoID;

                    MemoryStream ms = new MemoryStream( );
                    picBoxImagen.Image = this.ImageResize( picBoxImagen.Image );
                    picBoxImagen.Image.Save( ms , System.Drawing.Imaging.ImageFormat.Jpeg );

                    byte[ ] imageAsBytes = ms.ToArray( );
                    ms.Close( );

                    SqlParameter picparameter = new SqlParameter( );
                    picparameter.SqlDbType = SqlDbType.Image;
                    picparameter.ParameterName = "pic";
                    picparameter.Value = imageAsBytes;

                    SqlCommand cmd = new SqlCommand( sql , CommonUtils.ConexionBD.Conexion );
                    cmd.Parameters.Add( picparameter );
                    cmd.ExecuteNonQuery( );

                    CommonUtils.ConexionBD.CerrarConexion( );

                    sql = "delete from Producto_Producto where ComboId='" + Producto.ProductoID+"'";
                    CommonUtils.ConexionBD.Actualizar(sql);
                    if ( groupBoxCombos.Visible )
                    {
                        foreach ( DataRow item in dataTable.Rows )
                        {
                            if ( ( bool ) item.ItemArray[ 0 ] == true )
                            {
                                sql = "UPDATE Producto_Producto SET ComboId='" + Producto.ProductoID + "',ProductoId='" + item.ItemArray[ 1 ] + "',Cantidad='" + item.ItemArray[ 3 ] +
                                    "'  WHERE ComboId=" + Producto.ProductoID;
                                CommonUtils.ConexionBD.Actualizar( sql );
                            }
                        }
                    }
                    alertControlProducto.Show(this.FindForm(), "Actualización satisfactoria.", "El producto " + txtNomProducto.Text +
                  " fue modificada (a) satisfactoriamente.", Image);
                 //   barButtonCerrar_ItemClick( null , null );
                }
                catch ( Exception ex )
                {
                    log.Error( ex.Message , ex );
                    MessageBarValue = " No se pudo actualizar el producto. " + ex.Message;
                }
                finally
                {
                    barItem.Caption = MessageBarValue;
                }
            }
            else
            {
                try
                {
                    string sql = "INSERT into Producto (Nombre, Descripcion, Tipo, Tamano, PrecioVenta, Imagen, CategoriaId, RecetaId, FechaCreacion, FechaModificada)"
                    + " values ('{0}','{1}','{2}','{3}','{4}',{5},'{6}','{7}','{8}','{9}');";

                    sql = string.Format( sql , StringUtils.EscapeSQL( txtNomProducto.Text ) , StringUtils.EscapeSQL( txtDescripcion.Text ) , cmboxTipo.SelectedItem.ToString( ) ,
                        cmboxTamanio.SelectedItem.ToString( ) , txtPrice.EditValue.ToString( ) , "@pic" , ( lookUpCategoria.GetSelectedDataRow( ) as DataRowView ).Row.ItemArray[ 0 ] ,
                     ( lookUpReceta.GetSelectedDataRow( ) as DataRowView ).Row.ItemArray[ 0 ] , DateTime.Now , DateTime.Now );

                    MemoryStream ms = new MemoryStream( );
                    picBoxImagen.Image = this.ImageResize( picBoxImagen.Image );
                    picBoxImagen.Image.Save( ms , System.Drawing.Imaging.ImageFormat.Jpeg );

                    byte[ ] imageAsBytes = ms.ToArray( );
                    ms.Close( );

                    SqlParameter picparameter = new SqlParameter( );
                    picparameter.SqlDbType = SqlDbType.Image;
                    picparameter.ParameterName = "pic";
                    picparameter.Value = imageAsBytes;

                    SqlCommand cmd = new SqlCommand( sql , CommonUtils.ConexionBD.Conexion );
                    cmd.Parameters.Add( picparameter );
                    cmd.ExecuteNonQuery( );
                    cmd.CommandText = "SELECT @@IDENTITY";
                    decimal productoId = ( decimal ) ( cmd.ExecuteScalar( ) );

                    CommonUtils.ConexionBD.CerrarConexion( );

                    if ( groupBoxCombos.Visible )
                    {
                        foreach ( DataRow item in dataTable.Rows )
                        {
                            if ( ( bool ) item.ItemArray[ 0 ] == true )
                            {
                                sql = "INSERT INTO Producto_Producto (ComboId,ProductoId,Cantidad)  VALUES ('" +
                                   ( int ) productoId + "','" + item.ItemArray[ 1 ] + "','" + item.ItemArray[ 3 ] + "')";
                                CommonUtils.ConexionBD.Actualizar( sql );
                            }
                        }
                    }
                    alertControlProducto.Show(this.FindForm(), "Creación satisfactoria.", "El producto" + txtNomProducto.Text +
                  " fue creado (a) satisfactoriamente.", Image);
                    this.ResetFields( );
                }
                catch ( Exception ex )
                {
                    log.Error( ex.Message , ex );
                    MessageBarValue = " No se pudo guardar el producto. " + ex.Message;
                }
                finally
                {
                    barItem.Caption = MessageBarValue;
                }
            }
        }
        */

        private void ResetFields( )
        {
            int selectedTabIndex = ( this.ParentForm as mainForm ).xtraTabControl.SelectedTabPageIndex;

            ( this.ParentForm as mainForm ).ContextControls.Remove( ( this.ParentForm as mainForm ).xtraTabControl.SelectedTabPage.Controls[ 0 ].Name );
            ( this.ParentForm as mainForm ).InicializarNuevoProducto( );
            ( this.ParentForm as mainForm ).xtraTabControl.TabPages.RemoveAt( selectedTabIndex );
        }
        #endregion

        #region Events & Handlers
        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad( e );

            if ( IsEditing )
            {
                InitEditedFields( );
            }
            else
            {
                InitFields( );
                ValidateFields( );
            }
        }

        private void InitEditedFields( )
        {
            ShowHideControls( );
            InitializeDropDowns( );
            InitializeGrid( );
            InitializeCombos( );
        }

        private void ShowHideControls( )
        {
            if ( Producto.Categoria == "Bebida" )
            {
                groupBoxCombos.Visible = false;
            }
            else if ( Producto.Categoria == "Combo" )
            {
                groupBoxCombos.Visible = true;
                lookUpReceta.Visible = false;
                cmboxTipo.Visible = false;
                cmboxTamanio.Visible = false;
            }
            else
            {
                cmboxTipo.Visible = false;
                cmboxTamanio.Visible = false;
                groupBoxCombos.Visible = false;
            }
        }

        private void btnExaminar_Click( object sender , EventArgs e )
        {
            OpenFileDialog openFileDialog = new OpenFileDialog( );

            openFileDialog.InitialDirectory = Environment.GetFolderPath( Environment.SpecialFolder.MyPictures );
            openFileDialog.Filter = "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|GIF Files (*.gif)|*.gif";
            openFileDialog.FilterIndex = 1;
            openFileDialog.Title = "Seleccionar Imagen";
            openFileDialog.RestoreDirectory = true;

            if ( openFileDialog.ShowDialog( ) == DialogResult.OK )
            {
                try
                {
                    picBoxImagen.Image = Image.FromFile( openFileDialog.FileName );
                    picBoxImagen.ImageLocation = openFileDialog.FileName;
                }
                catch ( Exception ex )
                {
                    log.Error( ex.Message , ex );
                    MessageBarValue = " No se pudo cargar la imagen elegida. " + ex.Message;
                }
                finally
                {
                    barItem.Caption = MessageBarValue;
                }
            }
        }

        private Image ImageResize( Image img )
        {
            //get the new size based on the percentage change
            int resizedW = MAX_IMAGE_WIDTH_SIZE;
            int resizedH = MAX_IMAGE_HEIGHT_SIZE;

            //create a new Bitmap the size of the new image
            Bitmap bmp = new Bitmap( resizedW , resizedH );
            //create a new graphic from the Bitmap
            Graphics graphic = Graphics.FromImage( ( Image ) bmp );
            graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //draw the newly resized image
            graphic.DrawImage( img , 0 , 0 , resizedW , resizedH );
            //dispose and free up the resources
            graphic.Dispose( );
            //return the image
            return ( Image ) bmp;
        }

        private void txtNomProducto_Validating( object sender , CancelEventArgs e )
        {
            ValidateEmptyStringRule( sender as BaseEdit );
        }

        private void txtPrice_Validating( object sender , CancelEventArgs e )
        {
            ValidateLessThanMinRule( sender as BaseEdit , Decimal.Zero );
        }

        private void lookUpCategoria_EditValueChanged( object sender , EventArgs e )
        {
            LookUpEdit lookUpEdit = ( sender as LookUpEdit );

            if ( lookUpEdit != null )
            {
                lookUpReceta.Visible = true;
                lblReceta.Visible = true;

                if ( lookUpEdit.Text != "Bebida" )
                {
                    lblTamanio.Visible = false;
                    lblTipo.Visible = false;
                    cmboxTamanio.Visible = false;
                    cmboxTipo.Visible = false;
                }
                else
                {
                    lblTamanio.Visible = true;
                    lblTipo.Visible = true;
                    cmboxTamanio.Visible = true;
                    cmboxTipo.Visible = true;
                }

                if ( lookUpEdit.Text != "Combo" )
                {
                    groupBoxCombos.Visible = false;
                    //chkListComboProductos.Visible = false;
                }
                else
                {
                    lookUpReceta.Visible = false;
                    lblReceta.Visible = false;

                    groupBoxCombos.Visible = true;
                   // chkListComboProductos.Visible = true;
                }
            }
        }

        private void barButtonAdd_ItemClick( object sender , DevExpress.XtraBars.ItemClickEventArgs e )
        {
            if ( !dxErrorProvider.HasErrors )
            {
                SaveProduct( );
            }
            else
            {
                GetErrorProviderMessages( );
            }
        }

        private void barButtonCerrar_ItemClick( object sender , DevExpress.XtraBars.ItemClickEventArgs e )
        {
            //( this.ParentForm as mainForm ).ContextControls.Remove( ( this.ParentForm as mainForm ).xtraTabControl.SelectedTabPage.Controls[ 0 ].Name );
            //( this.ParentForm as mainForm ).xtraTabControl.TabPages.RemoveAt( ( this.ParentForm as mainForm ).xtraTabControl.SelectedTabPageIndex );
            if ((this.ParentForm as mainForm).ContextControlsForProductos.ContainsValue((this.ParentForm as mainForm).xtraTabControl.SelectedTabPage.Controls[0]))
            {
                (this.ParentForm as mainForm).ContextControlsForProductos.Remove(Producto.ProductoID.ToString());

            }
            (this.ParentForm as mainForm).xtraTabControl.TabPages.RemoveAt((this.ParentForm as mainForm).xtraTabControl.SelectedTabPageIndex);

        }

        private void barButtonRefrescar_ItemClick( object sender , DevExpress.XtraBars.ItemClickEventArgs e )
        {
            this.InitializeGrid();
        }

        private void txtPrice_TextChanged( object sender , EventArgs e )
        {
            ValidateFields( );
        }
        #endregion

        private void gridViewCombos_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DataRow row;
            row = gridViewCombos.GetDataRow(e.RowHandle);

            if (row == null || row[0].ToString() == string.Empty)
            {
                return;
            }

            if (gridViewCombos.FocusedColumn.FieldName == "Cantidad")
            {
                int idProduct = int.Parse(row[1].ToString());
                int cantidadProducto = int.Parse(row[3].ToString());
                if (e.Value.ToString() != "" && e.Value.ToString().Length != 0)
                {
                    cantidadProducto = int.Parse(e.Value.ToString());
                }

                if (lasValueIntableCombo.ContainsKey(idProduct))
                {
                    lasValueIntableCombo.Remove(idProduct);
                }
                lasValueIntableCombo.Add(idProduct, cantidadProducto);
            }
            if (gridViewCombos.FocusedColumn.FieldName == "Seleccionado")
            {
                int idProduct = int.Parse(row[1].ToString());
                bool isSelected = bool.Parse(e.Value.ToString());
                if (lastValueInTableForCheckedState.ContainsKey(idProduct))
                {
                    lastValueInTableForCheckedState.Remove(idProduct);
                }
                lastValueInTableForCheckedState.Add(idProduct, isSelected);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
