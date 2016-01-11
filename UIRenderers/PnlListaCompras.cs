using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using DevExpress.XtraEditors.Repository;
using System.Collections;

namespace UIRenderers
{
    public partial class PnlListaCompras : DevExpress.XtraEditors.XtraUserControl,CommonUtils.ICaja,CommonUtils.ActualizarGrids
    {
        private ArrayList comprasToUpdate = new ArrayList();
        private DataTable userTable;
        private SqlDataAdapter adapter;
        private DataSet ds;
        private SqlCommand command;
        private SqlCommandBuilder builder;
        private string sqlSelectQuery = string.Empty;
        
        private Dictionary<string, object[]> dictionaryOfRows = new Dictionary<string, object[]>();
        int numberOfRows = -1;
        public PnlListaCompras()
        {
            InitializeComponent();                                    
            InitConexionDB();            
            ds = new DataSet("MainDataSet");
            sqlSelectQuery = "SELECT  Compras.ComprasId, Compras.FechaCompra, Compras.Cantidad, Compras.CostoUnidad, Compras.Total, Compras.Descripcion, Insumo.Nombre,Insumo.InsumoId FROM Compras INNER JOIN Insumo ON Insumo.InsumoId = Compras.InsumoId";//"select * from Compras";
            command = new SqlCommand(sqlSelectQuery, CommonUtils.ConexionBD.getConnection());
            adapter = new SqlDataAdapter(command);
            builder = new SqlCommandBuilder(adapter);
            initializeGridComponent();
            if (numberOfRows == -1)
            {
                numberOfRows = 0;
            }
        }

        private void InitConexionDB()
        {
            CommonUtils.ConexionBD.StringDeConexion = GetConnectionString();
        }

        private static string GetConnectionString()
        {
            return @"Data Source=Marcelo-PC\SQLEXPRESS;Initial Catalog=Capresso;Integrated Security=SSPI;";
        }


        private void initializeGridComponent()
        {
            adapter.Fill(ds, "Compras");
            userTable = ds.Tables[0];
            gridControlComprarLista.DataSource = null;
            gridControlComprarLista.DataSource = userTable.DefaultView;//ds.Tables[0];
         //   numberOfRows = gridViewComprarlista.RowCount;
            ((DevExpress.XtraGrid.Views.Grid.GridView)gridControlComprarLista.Views[0]).Columns[1].FieldName = "ComprasId";
            ((DevExpress.XtraGrid.Views.Grid.GridView)gridControlComprarLista.Views[0]).Columns[2].FieldName = "InsumoId";
            ((DevExpress.XtraGrid.Views.Grid.GridView)gridControlComprarLista.Views[0]).Columns[3].FieldName = "Cantidad"; 
            ((DevExpress.XtraGrid.Views.Grid.GridView)gridControlComprarLista.Views[0]).Columns[5].FieldName = "Total";//"Total";
            ((DevExpress.XtraGrid.Views.Grid.GridView)gridControlComprarLista.Views[0]).Columns[4].FieldName = "CostoUnidad";// "Descripcion";
            ((DevExpress.XtraGrid.Views.Grid.GridView)gridControlComprarLista.Views[0]).Columns[6].FieldName = "Descripcion";
            ((DevExpress.XtraGrid.Views.Grid.GridView)gridControlComprarLista.Views[0]).Columns[7].FieldName = "FechaCompra";
            //     ((DevExpress.XtraGrid.Views.Grid.GridView)gridControlComprarLista.Views[0]).Columns[8].FieldName = "InsumoId";

           RepositoryItemCheckEdit checkEdit = gridControlComprarLista.RepositoryItems.Add("CheckEdit") as RepositoryItemCheckEdit;         
            checkEdit.PictureChecked = imageList1.Images[0];// Image.FromFile("C:\\Users\\Daniel\\Downloads\\iInventory\\iInventory\\iInventory\\Resources\\bbsave.ico");
            checkEdit.PictureUnchecked = imageList1.Images[0];//Image.FromFile("C:\\Users\\Daniel\\Downloads\\iInventory\\iInventory\\iInventory\\Resources\\bbsave.ico");
            checkEdit.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.UserDefined;
            gridViewComprarlista.Columns[0].ColumnEdit = checkEdit;
            gridViewComprarlista.Columns[0].Visible = false;
            gridViewComprarlista.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(gridViewCompras_ValidateRow);
            gridViewComprarlista.ValidatingEditor += new DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventHandler(gridViewComprarlista_ValidatingEditor);
            loadInsumosComponent();
            checkEdit.Click += new EventHandler(checkEdit_Click);
                        
        }

        void checkEdit_Click(object sender, EventArgs e)
        {      
            DataRow dr = gridViewComprarlista.GetDataRow(gridViewComprarlista.FocusedRowHandle);
            if(gridViewComprarlista.FocusedRowHandle>=0)// in order to avoid a click in the edit image of the search filter
            manageUpdateFeature(dr);
            /*
             UPDATE Insumo
             SET  CantidadInsumoEnIventario =(SELECT 0 -(SELECT Cantidad FROM Compras WHERE (ComprasId = 16)) + (SELECT CantidadInsumoEnIventario FROM Insumo AS Insumo_1 WHERE (InsumoId = 26)) AS Expr1) WHERE        (InsumoId = 26)
             */
        }

        //called in order to make an update in "Compra" there are special cases.
        private void manageUpdateFeature(DataRow dr)
        {
            string cantidadUnidadesCompradas = string.Empty;
            string sqlUpdate = ""; 
            CommonUtils.ConexionBD.AbrirConexion();
            SqlDataReader reader = CommonUtils.ConexionBD.EjecutarConsultaReader("Select InsumoId from Compras where ComprasId="+dr[0].ToString());
            int idInsumo = 0;
            while (reader.Read())
            {
                idInsumo=(int) reader.GetDecimal(0);
            }             
            string costoTotal = (float.Parse(dr[3].ToString()) * float.Parse(dr[2].ToString())).ToString();
            
            CommonUtils.ConexionBD.CerrarConexion();
            if (dr[7].ToString() == idInsumo.ToString())// insumo was not changed
            {     
                //SELECT  Compras.Cantidad * Insumo.Cantidad AS Expr1 FROM Compras INNER JOIN Insumo ON Compras.InsumoId = Insumo.InsumoId WHERE (Compras.ComprasId = 23)
               // sqlUpdate = "UPDATE Insumo  SET  CantidadInsumoEnIventario =(SELECT " + dr[2].ToString() + "*(Select Cantidad from Insumo where InsumoId="+dr[7].ToString()+") -(SELECT Compras.Cantidad*Insumo.Cantidad FROM Compras INNER JOIN Insumo on Compras.InsumoId=Insumo.InsumoId WHERE (ComprasId = " + dr[0].ToString() + ")) + (SELECT CantidadInsumoEnIventario FROM Insumo AS Insumo_1 WHERE (InsumoId = '" + dr[7].ToString() + "')) AS Expr1) WHERE (InsumoId =" + dr[7].ToString() + ")";
                sqlUpdate = "UPDATE Insumo  SET  CantidadInsumoEnIventario =(SELECT " + dr[2].ToString() + "*(Select Cantidad from Insumo where InsumoId=" + dr[7].ToString() + ") -(SELECT CantidadUnidadesCompradas FROM Compras WHERE (ComprasId = " + dr[0].ToString() + ")) + (SELECT CantidadInsumoEnIventario FROM Insumo AS Insumo_1 WHERE (InsumoId = '" + dr[7].ToString() + "')) AS Expr1) WHERE (InsumoId =" + dr[7].ToString() + ")";
                CommonUtils.ConexionBD.Actualizar(sqlUpdate);
                cantidadUnidadesCompradas = (float.Parse(dr[2].ToString()) * float.Parse(CommonUtils.ConexionBD.EjecutarConsultaEspecial("select Cantidad from Insumo where InsumoId=" + dr[7].ToString()))).ToString();
                sqlUpdate = "UPDATE Compras set Descripcion='" + dr[5].ToString() + "',Cantidad='" + dr[2].ToString() + "',Total='" + costoTotal + "', InsumoId='" + dr[7].ToString() + "',CostoUnidad='" + dr[3].ToString() + "', CantidadUnidadesCompradas='" +cantidadUnidadesCompradas+ "'  where ComprasId=" + dr[0].ToString();
                CommonUtils.ConexionBD.Actualizar(sqlUpdate);
            }
            else //insumo was changed
            {
                //sqlUpdate = "Update Insumo SeT CantidadInsumoEnIventario =((select CantidadInsumoEnIventario from Insumo WHERE InsumoId=" + idInsumo + ")-("+dr[2].ToString()+"*(select Cantidad from Insumo where InsumoId="+idInsumo+"))) where InsumoId="+idInsumo;
                sqlUpdate = "Update Insumo SET CantidadInsumoEnIventario =((select CantidadInsumoEnIventario from Insumo WHERE InsumoId=" + idInsumo + ")-(select CantidadUnidadesCompradas from Compras WHERE ComprasId=" + dr[0].ToString() + ")) where InsumoId=" + idInsumo;
                CommonUtils.ConexionBD.Actualizar(sqlUpdate);
                sqlUpdate = "UPDATE Insumo Set CantidadInsumoEnIventario =((select CantidadInsumoEnIventario from Insumo WHERE InsumoId="+dr[7].ToString()+") +("+dr[2].ToString()+"*(select Cantidad from Insumo where InsumoId="+dr[7].ToString()+"))) where InsumoId="+dr[7].ToString();
                CommonUtils.ConexionBD.Actualizar(sqlUpdate);
                cantidadUnidadesCompradas=(float.Parse(dr[2].ToString())*float.Parse(CommonUtils.ConexionBD.EjecutarConsultaEspecial("select Cantidad from Insumo where InsumoId="+dr[7].ToString()))).ToString();
                sqlUpdate = "UPDATE Compras set Descripcion='" + dr[5].ToString() + "',Cantidad='" + dr[2].ToString() + "',Total='" + costoTotal + "', InsumoId='" + dr[7].ToString() + "',CostoUnidad='" + dr[3].ToString() + "', CantidadUnidadesCompradas='" + cantidadUnidadesCompradas + "'  where ComprasId=" + dr[0].ToString();//*(select Cantidad from Insumo where InsumoId="+dr[7].ToString()+")
                CommonUtils.ConexionBD.Actualizar(sqlUpdate);
            }
            dr[4] = costoTotal;
           // userTable.Rows[gridViewComprarlista.FocusedRowHandle][3]=costoTotal;
            gridViewComprarlista.RefreshRow(gridViewComprarlista.FocusedRowHandle);
           
        }

        private void manageUpdateFeature(object[] dr)
        {
            string cantidadUnidadesCompradas = string.Empty;
            string sqlUpdate = "";
            CommonUtils.ConexionBD.AbrirConexion();
            SqlDataReader reader = CommonUtils.ConexionBD.EjecutarConsultaReader("Select InsumoId from Compras where ComprasId=" + dr[0].ToString());
            int idInsumo = 0;
            while (reader.Read())
            {
                idInsumo = (int)reader.GetDecimal(0);
            }
            string costoTotal = (float.Parse(dr[3].ToString()) * float.Parse(dr[2].ToString())).ToString();

            CommonUtils.ConexionBD.CerrarConexion();
            if (dr[1].ToString() == idInsumo.ToString())// insumo was not changed
            {
                //SELECT  Compras.Cantidad * Insumo.Cantidad AS Expr1 FROM Compras INNER JOIN Insumo ON Compras.InsumoId = Insumo.InsumoId WHERE (Compras.ComprasId = 23)
                // sqlUpdate = "UPDATE Insumo  SET  CantidadInsumoEnIventario =(SELECT " + dr[2].ToString() + "*(Select Cantidad from Insumo where InsumoId="+dr[7].ToString()+") -(SELECT Compras.Cantidad*Insumo.Cantidad FROM Compras INNER JOIN Insumo on Compras.InsumoId=Insumo.InsumoId WHERE (ComprasId = " + dr[0].ToString() + ")) + (SELECT CantidadInsumoEnIventario FROM Insumo AS Insumo_1 WHERE (InsumoId = '" + dr[7].ToString() + "')) AS Expr1) WHERE (InsumoId =" + dr[7].ToString() + ")";
                sqlUpdate = "UPDATE Insumo  SET  CantidadInsumoEnIventario =(SELECT " + dr[2].ToString() + "*(Select Cantidad from Insumo where InsumoId=" + dr[1].ToString() + ") -(SELECT CantidadUnidadesCompradas FROM Compras WHERE (ComprasId = " + dr[0].ToString() + ")) + (SELECT CantidadInsumoEnIventario FROM Insumo AS Insumo_1 WHERE (InsumoId = '" + dr[1].ToString() + "')) AS Expr1) WHERE (InsumoId =" + dr[1].ToString() + ")";
                CommonUtils.ConexionBD.Actualizar(sqlUpdate);
                cantidadUnidadesCompradas = (float.Parse(dr[2].ToString()) * float.Parse(CommonUtils.ConexionBD.EjecutarConsultaEspecial("select Cantidad from Insumo where InsumoId=" + dr[1].ToString()))).ToString();
                sqlUpdate = "UPDATE Compras set Descripcion='" + dr[4].ToString() + "',Cantidad='" + dr[2].ToString() + "',Total='" + costoTotal + "', InsumoId='" + dr[1].ToString() + "',CostoUnidad='" + dr[3].ToString() + "', CantidadUnidadesCompradas='" + cantidadUnidadesCompradas + "'  where ComprasId=" + dr[0].ToString();
                CommonUtils.ConexionBD.Actualizar(sqlUpdate);
            }
            else //insumo was changed
            {
                //sqlUpdate = "Update Insumo SeT CantidadInsumoEnIventario =((select CantidadInsumoEnIventario from Insumo WHERE InsumoId=" + idInsumo + ")-("+dr[2].ToString()+"*(select Cantidad from Insumo where InsumoId="+idInsumo+"))) where InsumoId="+idInsumo;
                sqlUpdate = "Update Insumo SET CantidadInsumoEnIventario =((select CantidadInsumoEnIventario from Insumo WHERE InsumoId=" + idInsumo + ")-(select CantidadUnidadesCompradas from Compras WHERE ComprasId=" + dr[0].ToString() + ")) where InsumoId=" + idInsumo;
                CommonUtils.ConexionBD.Actualizar(sqlUpdate);
                sqlUpdate = "UPDATE Insumo Set CantidadInsumoEnIventario =((select CantidadInsumoEnIventario from Insumo WHERE InsumoId=" + dr[1].ToString() + ") +(" + dr[2].ToString() + "*(select Cantidad from Insumo where InsumoId=" + dr[1].ToString() + "))) where InsumoId=" + dr[1].ToString();
                CommonUtils.ConexionBD.Actualizar(sqlUpdate);
                cantidadUnidadesCompradas = (float.Parse(dr[2].ToString()) * float.Parse(CommonUtils.ConexionBD.EjecutarConsultaEspecial("select Cantidad from Insumo where InsumoId=" + dr[1].ToString()))).ToString();
                sqlUpdate = "UPDATE Compras set Descripcion='" + dr[4].ToString() + "',Cantidad='" + dr[2].ToString() + "',Total='" + costoTotal + "', InsumoId='" + dr[1].ToString() + "',CostoUnidad='" + dr[3].ToString() + "', CantidadUnidadesCompradas='" + cantidadUnidadesCompradas + "'  where ComprasId=" + dr[0].ToString();//*(select Cantidad from Insumo where InsumoId="+dr[7].ToString()+")
                CommonUtils.ConexionBD.Actualizar(sqlUpdate);
            }
           
           // dr[4] = costoTotal;
            // userTable.Rows[gridViewComprarlista.FocusedRowHandle][3]=costoTotal;
           // gridViewComprarlista.RefreshRow(gridViewComprarlista.FocusedRowHandle);

        }

        //handler used in order to validate a change on a cell
        void gridViewComprarlista_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            DataRow rowSelected = gridViewComprarlista.GetDataRow(gridViewComprarlista.FocusedRowHandle);
            if (rowSelected == null)//to avoid validation in the row filter
            {
                return;
            }
            if (gridViewComprarlista.FocusedColumn.FieldName == "Cantidad")
            {
                if (e.Value== null || e.Value.ToString().Trim().Length == 0)
                {
                    e.Valid = false;
                    e.ErrorText = "cantidad no puede ser un campo vacio";
                }
            }
            else if (gridViewComprarlista.FocusedColumn.FieldName == "CostoUnidad")
            {
                if (e.Value == null || e.Value.ToString().Trim().Length == 0)
                {
                    e.Valid = false;
                    e.ErrorText = "Costo Unidad no puede ser un campo vacio";
                }
            }
        }

        //handler used in order to validate a new row
        void gridViewCompras_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            DevExpress.XtraGrid.Views.Base.ColumnView view = sender as DevExpress.XtraGrid.Views.Base.ColumnView;
            DevExpress.XtraGrid.Columns.GridColumn column1 = view.Columns[3];
            DevExpress.XtraGrid.Columns.GridColumn columnCostoUnidad = view.Columns[4];
            DevExpress.XtraGrid.Columns.GridColumn columnInsumo = view.Columns[2];       
            if (view.GetRowCellValue(e.RowHandle, column1).ToString() == null || view.GetRowCellValue(e.RowHandle, column1).ToString().Trim().Length == 0)
            {
                e.Valid = false;
                view.SetColumnError(column1, "Cantidad no puede ser vacio", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical);
            }
            else if (view.GetRowCellValue(e.RowHandle, columnCostoUnidad).ToString() == null || view.GetRowCellValue(e.RowHandle, columnCostoUnidad).ToString().Trim().Length == 0)
            {
                e.Valid = false;
                view.SetColumnError(columnCostoUnidad, "Costo Unidad no puede ser vacio", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical);
            }
            else if (view.GetRowCellValue(e.RowHandle, columnInsumo).ToString() == null || view.GetRowCellValue(e.RowHandle, columnInsumo).ToString().Trim().Length == 0)
            {
                e.Valid = false;
                view.SetColumnError(columnInsumo, "Debe seleccionar insumo", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical);
            }
        }

        private void loadInsumosComponent()
        {
            string sql = "select Nombre,InsumoId as Id from Insumo";
            CommonUtils.ConexionBD.AbrirConexion();
            repositoryItemGridLookUpEdit1.DataSource = null;
            repositoryItemGridLookUpEdit1.DataSource = CommonUtils.ConexionBD.EjecutarConsulta(sql);
            CommonUtils.ConexionBD.CerrarConexion();
            repositoryItemGridLookUpEdit1.ValueMember = "Id";
            repositoryItemGridLookUpEdit1.DisplayMember = "Nombre";
            repositoryItemGridLookUpEdit1.View.ActiveFilterEnabled = true;
         
           //repositoryItemGridLookUpEdit1.View.
         
            // repositoryItemGridLookUpEdit1.EditValueChanged += new EventHandler(repositoryItemGridLookUpEdit1_EditValueChanged);
                /*
            SqlDataReader reader=CommonUtils.ConexionBD.EjecutarConsultaReader(sql);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                   
                    
                   // repositoryItemComboBox2.Items.Add(reader.GetString(3)+" "+((int)reader.GetDecimal(0)).ToString());
                }
            }
            CommonUtils.ConexionBD.CerrarConexion();*/
        }

        private void gridViewComprarlista_RowCountChanged(object sender, EventArgs e)
        {
            /*if (numberOfRows > gridViewComprarlista.RowCount)
            {
                numberOfRows = gridViewComprarlista.RowCount;
            }
            
            if (numberOfRows == -1)//first call
            {
                numberOfRows = gridViewComprarlista.RowCount;                
            }
            else
            if(numberOfRows<gridViewComprarlista.RowCount)*/ //if a new row was added
            {
                
                numberOfRows = gridViewComprarlista.RowCount;
                DataRow row = gridViewComprarlista.GetDataRow(gridViewComprarlista.RowCount-1);
               
                if ( row==null || row[1].ToString() != string.Empty)
                {
                    return;
                }
                string  cantidad = row["Cantidad"].ToString();
                string costoUnidad = row[3].ToString();
                string costoTotal = (float.Parse(costoUnidad) * float.Parse(cantidad)).ToString();
                string descripcion = row["Descripcion"].ToString();
                string idInsumo = row[7].ToString();
                string cantidadUnidadesCompradas = string.Empty;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = CommonUtils.ConexionBD.getConnection();
                cantidadUnidadesCompradas = (float.Parse(cantidad) * float.Parse(CommonUtils.ConexionBD.EjecutarConsultaEspecial("select Cantidad from Insumo where InsumoId=" + idInsumo  ))).ToString();
                string sqlInsertCompras = "insert into Compras (FechaCompra,Cantidad,CostoUnidad,Total,InsumoId,Descripcion,CantidadUnidadesCompradas,EsModificable) values('" + DateTime.Now + "','" + cantidad + "','" + costoUnidad + "','" + costoTotal+ "','" + idInsumo + "','" + descripcion + "','"+cantidadUnidadesCompradas+"','True')";
               //we add cantidad to the current amount of an specific insumo
                string sql=" UPDATE Insumo SET CantidadInsumoEnIventario = ("+cantidad+"*(SELECT Cantidad from Insumo where InsumoId="+idInsumo+") ) +(SELECT CantidadInsumoEnIventario FROM Insumo AS Insumo_1 WHERE (InsumoId = "+idInsumo+")) WHERE (InsumoId ="+idInsumo+")";
                CommonUtils.ConexionBD.Actualizar(sql);
             //   adapter.InsertCommand = cmd;            
               // adapter.Update(userTable);
              /*  cmd.CommandText = "select @@IDENTITY";
                CommonUtils.ConexionBD.AbrirConexion();
                decimal newId=(decimal)cmd.ExecuteNonQuery();
                CommonUtils.ConexionBD.CerrarConexion();*/

                row[0] = CommonUtils.ConexionBD.ActualizarRetornandoId(sqlInsertCompras);
                row[4] = costoTotal;
                row[1] = DateTime.Now;
                gridViewComprarlista.RefreshRow(gridViewComprarlista.RowCount - 1);
                insertCaja(row);
                //refresh the table with the new insert data
               /* command.CommandText = sqlSelectQuery;
                adapter.SelectCommand = command;
                userTable.Clear();
                adapter.Fill(ds, "Compras");
                userTable = ds.Tables[0];
                gridControlComprarLista.DataSource = null;
                gridControlComprarLista.DataSource = userTable.DefaultView;*/
                
            }                       
        }

      
        #region ActualizarGrids Members

        public void actualizarGrid()
        {
            userTable.Clear();
            initializeGridComponent();
            loadInsumosComponent();
            
        }

        #endregion

        private void barButtonDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataRow rowSelected = gridViewComprarlista.GetDataRow(gridViewComprarlista.FocusedRowHandle);
            if (rowSelected == null || rowSelected[0].ToString() == string.Empty)
            {
                MessageBox.Show(this, "Seleccione una fila", "Compras Insumos", MessageBoxButtons.OK);
                return;
            }
            string sqlQuery = "SELECT EsModificable from Compras where ComprasId=" + rowSelected[0].ToString();//"DELETE FROM ComprasEspeciales WHERE ComprasEspecialesId="+rowSelected[0].ToString();
            bool esModificable = Boolean.Parse(CommonUtils.ConexionBD.EjecutarConsultaEspecial(sqlQuery));
            if (!esModificable)
            {
                MessageBox.Show(this, "No se puede borrar el registro ya que no es modificable", "Compras Insumos", MessageBoxButtons.OK);
                return;
            }
            sqlQuery = "SELECT FechaCompra from Compras where ComprasId=" + rowSelected[0].ToString();
            DateTime dateTimeBD = DateTime.Parse(CommonUtils.ConexionBD.EjecutarConsultaEspecial(sqlQuery));
            if (DateTime.Now.Date > dateTimeBD.Date)
            {
                MessageBox.Show(this, "No se puede borrar el registro debido a la fecha", "Compras Insumos", MessageBoxButtons.OK);
                return;
            }

            deleteValueCaja(rowSelected);
            sqlQuery = "DELETE FROM Compras WHERE ComprasId=" + rowSelected[0].ToString();
            CommonUtils.ConexionBD.Actualizar(sqlQuery);
            gridViewComprarlista.DeleteRow(gridViewComprarlista.FocusedRowHandle);

        }

        private void gridViewComprarlista_CellValueChanging( object sender , DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e )
        {
            DevExpress.XtraGrid.Views.Base.ColumnView view = sender as DevExpress.XtraGrid.Views.Base.ColumnView;
            // TODO: columnCantidad is declared but is never used. Use this local or remove it please.
            // TODO: columnCostoUnidad is declared but is never used. Use this local or remove it please.
            DevExpress.XtraGrid.Columns.GridColumn columnCantidad = view.Columns[ 3 ];
            DevExpress.XtraGrid.Columns.GridColumn columnCostoUnidad = view.Columns[ 4 ];

            DataRow row;
            row = gridViewComprarlista.GetDataRow( e.RowHandle );

            if ( row == null )//|| row[0].ToString() == string.Empty)
            {
                // TODO: lote is declared but is never used. Use this local or remove it please.
                string lote = "select Cantidad from Insumo where InsumoId=" + e.Value.ToString( );
                //     lote = CommonUtils.ConexionBD.EjecutarConsultaEspecial(lote);
                //     gridViewComprarlista.SetRowCellValue(gridViewComprarlista.FocusedRowHandle, gridViewComprarlista.Columns["Cantidad"], lote);

                return;
            }
            else if ( row[ 0 ].ToString( ) == string.Empty )
            {
                //
                return;

            }

            if ( ( e.Value.ToString( ).Trim( ) == string.Empty ) && gridViewComprarlista.FocusedColumn.FieldName != "Descripcion" )
            {
                ribbonControl.Enabled = false;
                return;
            }
            else
            {
                ribbonControl.Enabled = true;
            }

            object[ ] array = new object[ 5 ];
            array[ 0 ] = row[ 0 ].ToString( );//id
            array[ 1 ] = row[ 7 ].ToString( );//insumo id
            array[ 2 ] = int.Parse( row[ 2 ].ToString( ) ).ToString( );//cantidad
            array[ 3 ] = row[ 3 ].ToString( );//costo unidad
            array[ 4 ] = row[ 5 ].ToString( );//descripcion

            if ( gridViewComprarlista.FocusedColumn.FieldName == "Cantidad" )
            {
                if ( e.Value.ToString( ) != string.Empty )
                {
                    string a = e.Value.ToString( ).Replace( "." , "" );
                    array[ 2 ] = int.Parse( a );
                }
            }
            else if ( gridViewComprarlista.FocusedColumn.FieldName == "CostoUnidad" )
            {
                array[ 3 ] = e.Value.ToString( );
            }
            else if ( gridViewComprarlista.FocusedColumn.FieldName == "InsumoId" )
            {
                array[ 1 ] = e.Value.ToString( );
            }
            else if ( gridViewComprarlista.FocusedColumn.FieldName == "Descripcion" )
            {
                array[ 4 ] = e.Value.ToString( );
            }

            if ( dictionaryOfRows.ContainsKey( row[ 0 ].ToString( ) ) )
            {
                dictionaryOfRows.Remove( row[ 0 ].ToString( ) );
            }
            dictionaryOfRows.Add
                ( row[ 0 ].ToString( ) , array );

        }           

        private void gridControlComprarLista_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
               /* DataRow r = gridViewComprarlista.GetDataRow(gridViewComprarlista.FocusedRowHandle);
                if (dictionaryOfRows.ContainsKey(r[0].ToString()))
                {
                    dictionaryOfRows.Remove(r[0].ToString());
                    object[] array = new object[5];                    
                    array[0] = r[0].ToString();//id
                    array[1] = r[7].ToString();//insumo id
                    array[2] = int.Parse(r[2].ToString()).ToString();//cantidad
                    array[3] = r[3].ToString();//costo unidad
                    array[4] = r[5].ToString();//descripcion
                    dictionaryOfRows.Add(r[0].ToString(), array);
                    
                }*/
                ribbonControl.Enabled = true;
            }
        }

        private void gridViewComprarlista_ShowingEditor(object sender, CancelEventArgs e)
        {
           DataRow r=gridViewComprarlista.GetDataRow(gridViewComprarlista.FocusedRowHandle);
           if (r == null || r[0].ToString() == string.Empty)
           {
               return;
           }
           string sqlQuery = "select EsModificable from Compras where ComprasId="+r[0].ToString();
           string value= CommonUtils.ConexionBD.EjecutarConsultaEspecial(sqlQuery);
           if (value == "True")
           {
               e.Cancel = false;
           }
           else
           {
               e.Cancel = true;
           }
        }

        private void barButtonRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            actualizarGrid();

        }

        private void barButtonEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            foreach (object[] obj in dictionaryOfRows.Values)
            {
                update(obj);
                manageUpdateFeature(obj);
            }
            userTable.Clear();
            initializeGridComponent();
            dictionaryOfRows.Clear();
        }

        private void barBtnComprasEspeciales_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ListaComprasEspeciales compras = new ListaComprasEspeciales(this.ParentForm);
            compras.ShowDialog();
            compras.Refresh();
        }

        private void barButtonIventario_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ListaIventarios iventario = new ListaIventarios(this.ParentForm);
            iventario.ShowDialog();
            iventario.Refresh();
        }

        #region ICaja Members

        public void update(object[] row)
        {
            string costoTotal = (float.Parse(row[3].ToString()) * float.Parse(row[2].ToString())).ToString();
            string sqlQuery = "Update Caja set DineroSistema=((select DineroSistema from Caja where (CajaId = (SELECT MAX(CajaId) FROM Caja)))-(select " + costoTotal + " - Total from Compras where ComprasId=" + row[0].ToString() + "))where (CajaId = (SELECT MAX(CajaId) FROM Caja))";
            CommonUtils.ConexionBD.Actualizar(sqlQuery);
        }

        public void insertCaja(DataRow row)
        {
            string cantidad = row["Cantidad"].ToString();
            string costoUnidad = row[3].ToString();
            string costo = (float.Parse(costoUnidad) * float.Parse(cantidad)).ToString();
            string sqlQuery = "SELECT esModificable FROM Caja WHERE (CajaId = (SELECT MAX(CajaId) FROM Caja))";
            bool esModificable = Boolean.Parse(CommonUtils.ConexionBD.EjecutarConsultaEspecial(sqlQuery));
            if (esModificable)
            {
                sqlQuery = "select DineroReal -" + costo + " from Caja WHERE (CajaId = (SELECT MAX(CajaId) FROM Caja))";
                string dineroActualCaja = CommonUtils.ConexionBD.EjecutarConsultaEspecial(sqlQuery);
                sqlQuery = "INSERT INTO Caja(DineroSistema,DineroReal,Diferencia,Descripcion,Fecha,esModificable) VALUES(" + dineroActualCaja + ",0,0,'nuevo valor','" + DateTime.Now + "','False')";
                CommonUtils.ConexionBD.Actualizar(sqlQuery);
            }
            else
            {
                sqlQuery = "Select Fecha from Caja WHERE (CajaId = (SELECT MAX(CajaId) FROM Caja))";
                DateTime lastTime = DateTime.Parse(CommonUtils.ConexionBD.EjecutarConsultaEspecial(sqlQuery));
                if (lastTime.Date == DateTime.Now.Date)
                {
                    sqlQuery = "select DineroSistema - " + costo + " from Caja  WHERE (CajaId = (SELECT MAX(CajaId) FROM Caja))";
                    string dineroSistema = CommonUtils.ConexionBD.EjecutarConsultaEspecial(sqlQuery);
                    sqlQuery = "UPDATE Caja set Fecha='" + DateTime.Now + "', DineroSistema=" + dineroSistema + " WHERE (CajaId =  (SELECT  MAX(CajaId) AS Expr1  FROM  Caja AS Caja_1))";
                    CommonUtils.ConexionBD.Actualizar(sqlQuery);
                }
                else
                {
                    sqlQuery = "select DineroReal -" + costo + " from Caja WHERE (CajaId = (SELECT MAX(CajaId) FROM Caja))";
                    string dineroActualCaja = CommonUtils.ConexionBD.EjecutarConsultaEspecial(sqlQuery);
                    sqlQuery = sqlQuery = "INSERT INTO Caja(DineroSistema,DineroReal,Diferencia,Descripcion,Fecha,esModificable) VALUES(" + dineroActualCaja + ",0,0,'nuevo valor','" + DateTime.Now + "','False')";
                    CommonUtils.ConexionBD.Actualizar(sqlQuery);
                }
            }
        }

        public void deleteValueCaja(DataRow row)
        {
            throw new NotImplementedException();
        }

        #endregion

        private void gridViewComprarlista_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            // TODO: This method is still needed?
            int i = (e.RowHandle);
        }

      
    }
}
