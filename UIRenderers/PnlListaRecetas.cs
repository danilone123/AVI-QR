using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace UIRenderers
{
    public partial class PnlListaRecetas : UserControl,CommonUtils.ActualizarGrids
    {
        public PnlListaRecetas()
        {
            InitializeComponent();
            InitConexionDB();
            InitializeGrid();
        }

        private void InitConexionDB()
        {
            CommonUtils.ConexionBD.StringDeConexion = CommonUtils.ConexionBD.GetConnectionString();//GetConnectionString();
        }

        private static string GetConnectionString()
        {
            return @"Data Source=Marcelo-PC\SQLEXPRESS;Initial Catalog=Capresso;Integrated Security=SSPI;";//@"Data Source=Marcelo-PC\SQLEXPRESS;Database=Capresso;Initial Catalog=Capresso;Integrated Security=SSPI;";//@"Data Source=MARCELO-PC\SQLEXPRESS;Database=Capresso;Initial Catalog=Capresso;Integrated Security=SSPI;";
        }

        private void InitializeGrid()
        {             
            System.Collections.ArrayList list = new System.Collections.ArrayList();
            CommonUtils.ConexionBD.AbrirConexion();
            string sql="SELECT  Insumo.InsumoId, Insumo.Cantidad, Insumo.CostoUnidad, Insumo.Nombre, Insumo.Presentacion, Insumo.Marca, Insumo.Unidad,Receta_Insumo.Cantidad AS Expr1, Receta.RecetaId, Receta.Nombre AS Expr2, Receta.CostoReceta FROM Receta_Insumo INNER JOIN Insumo ON Insumo.InsumoId = Receta_Insumo.InsumoId INNER JOIN Receta ON Receta.RecetaId = Receta_Insumo.RecetaId ORDER BY Receta.RecetaId";
             
            SqlDataReader reader = CommonUtils.ConexionBD.EjecutarConsultaReader(sql);
            if (reader.HasRows)
            {
                CommonUtils.Receta receta=new CommonUtils.Receta();
                CommonUtils.RecetaInsumo recetaInsumo;
                int idReceta =-1;
                while (reader.Read())
                {
                    int idActual = (int)reader.GetDecimal(8);
                    if (idActual != idReceta)
                    {
                        if (idReceta != -1)
                        {
                            receta = new CommonUtils.Receta();
                        }
                        receta.idReceta = idActual;
                        receta.nombre = reader.GetString(9);
                        receta.costoReceta = (float)reader.GetDouble(10);
                        list.Add(receta);
                        idReceta=idActual;
                    }
                    CommonUtils.Insumo insumo = new CommonUtils.Insumo();
                    recetaInsumo = new CommonUtils.RecetaInsumo();
                    insumo.idInsumo = (int)reader.GetDecimal(0);
                    insumo.cantidad = (float)reader.GetDouble(1);
                    insumo.costoUnidad = (float)reader.GetDouble(2);
                    insumo.nombre = reader.GetString(3);
                    insumo.presentacion = reader.GetString(4);
                    insumo.marca = reader.GetString(5);
                    insumo.unidad = reader.GetString(6);
                    recetaInsumo.insumo = insumo;
                    recetaInsumo.Nombre = insumo.nombre;
                    recetaInsumo.cantidad = (float)reader.GetDouble(7);
                    receta.listaInsumos.Add(recetaInsumo);
                    gridViewListaRecetas.SetMasterRowExpanded(0, false);                    
                }
            }
            gridControlListaRecetas.ShowOnlyPredefinedDetails = true;
          // gridControlListaRecetas.MainView. ColumnViewOptionsBehavior.AutoPopulateColumns 
            gridControlListaRecetas.DataSource = null;
            gridControlListaRecetas.DataSource = list;//CommonUtils.ConexionBD.EjecutarConsulta("Select * from Receta");
            ((DevExpress.XtraGrid.Views.Grid.GridView)gridControlListaRecetas.Views[0]).Columns[0].FieldName = "idReceta";
            ((DevExpress.XtraGrid.Views.Grid.GridView)gridControlListaRecetas.Views[0]).Columns[1].FieldName = "nombre";
            ((DevExpress.XtraGrid.Views.Grid.GridView)gridControlListaRecetas.Views[0]).Columns[2].FieldName = "costoReceta";
            CommonUtils.ConexionBD.CerrarConexion();
        }

        private void barButtonUp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            barButtonDown.Enabled = true;

            if (gridViewListaRecetas.FocusedRowHandle > 1)
                gridViewListaRecetas.MovePrev();
            else
            {
                gridViewListaRecetas.MovePrev();
                barButtonUp.Enabled = false;
            }
        }

        private void barButtonDown_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            barButtonUp.Enabled = true;

            if (gridViewListaRecetas.FocusedRowHandle + 1 < gridViewListaRecetas.RowCount - 1)
                gridViewListaRecetas.MoveNext();
            else
            {
                gridViewListaRecetas.MoveNext();
                barButtonDown.Enabled = false;
            }
        }

        private void barButtonAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if ( !(this.ParentForm as mainForm).ContextControls.ContainsKey( "PnlNuevaReceta" ) )
            {
                PnlNuevaReceta pnlRecet = new PnlNuevaReceta( );
                pnlRecet.Dock = DockStyle.Fill;
                DevExpress.XtraTab.XtraTabPage tabItem = new DevExpress.XtraTab.XtraTabPage( );
                tabItem.Controls.Add(pnlRecet);
                tabItem.Text = "Nueva Receta";
                ( this.ParentForm as mainForm ).xtraTabControl.TabPages.Add( tabItem );
                ( this.ParentForm as mainForm ).xtraTabControl.SelectedTabPage = tabItem;

              // (this.ParentForm as mainForm).ContextControls.Add( "PnlNuevaReceta" , pnlInsumo );
            }
        }

        private void barButtonPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridControlListaRecetas.ShowPrintPreview();
        }

        private void barButtonEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            CommonUtils.Receta recipe = (CommonUtils.Receta)gridViewListaRecetas.GetFocusedRow();
            //PnlNuevaReceta pnlReceta = new PnlNuevaReceta((CommonUtils.Receta)((DevExpress.XtraGrid.Views.Grid.GridView)gridControlListaRecetas.Views[0]).GetFocusedRow());
            if (recipe != null)
            {
                if (!(this.ParentForm as mainForm).ContextControlsForRecetas.ContainsKey(recipe.idReceta.ToString()))
                {
                    PnlNuevaReceta pnlReceta = new PnlNuevaReceta( recipe );
                    (this.ParentForm as mainForm).ContextControlsForRecetas.Add(recipe.idReceta.ToString(), pnlReceta);

                    pnlReceta.Dock = DockStyle.Fill;
                    DevExpress.XtraTab.XtraTabPage tabItem = new DevExpress.XtraTab.XtraTabPage( );
                    tabItem.Controls.Add( pnlReceta );
                    tabItem.Text = pnlReceta.Receta.nombre;
                    ( this.ParentForm as mainForm ).xtraTabControl.TabPages.Add( tabItem );
                    ( this.ParentForm as mainForm ).xtraTabControl.SelectedTabPage = tabItem;

                   // ( this.ParentForm as mainForm ).ContextControls.Add( "PnlNuevaReceta" , pnlReceta );
                }
            }
        }

        private void barButtonRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            actualizarGrid();
        }

        #region ActualizarGrids Members

        public void actualizarGrid()
        {
            InitializeGrid();
        }

        #endregion


        //wont delete if the recipe is related with producto
        private void barButtonDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CommonUtils.Receta recipe = (CommonUtils.Receta)gridViewListaRecetas.GetFocusedRow();
            //PnlNuevaReceta pnlReceta = new PnlNuevaReceta((CommonUtils.Receta)((DevExpress.XtraGrid.Views.Grid.GridView)gridControlListaRecetas.Views[0]).GetFocusedRow());
      
            if (recipe == null)
            {
                MessageBox.Show(this, "Seleccione una fila para borrar", "Recetas", MessageBoxButtons.OK);
                return;
            }
            
            if (!esPosibleBorrarReceta(recipe))
            {
                MessageBox.Show(this, "la receta " + recipe.nombre + " no se puede borrar debido a que se encuentra asociado a otros datos.", "Recetas", MessageBoxButtons.OK);
                return;
            }

            if (MessageBox.Show(this, "Desea borrar la receta " + recipe.nombre + " ?", "Recetas", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            else
            {
                string sqlQuery = "Delete from Receta_Insumo where RecetaId="+ recipe.idReceta;
                CommonUtils.ConexionBD.Actualizar(sqlQuery);
                sqlQuery = "Delete from Receta where RecetaId=" + recipe.idReceta;
                CommonUtils.ConexionBD.Actualizar(sqlQuery);
                //gridViewListaComprasEspeciales.DeleteRow(gridViewListaComprasEspeciales.FocusedRowHandle);
                gridViewListaRecetas.DeleteRow(gridViewListaRecetas.FocusedRowHandle);
                //this.actualizarGrid();
            }
        }

        //categoria 5 es para combo
        private bool esPosibleBorrarReceta(CommonUtils.Receta recipe)
        {
            //string sqlQuery="select RecetaId from Producto where ProductoId in (Select ProductoId from Producto_Producto where ComboId=12) "
            string sqlQuery="select RecetaId from Receta where RecetaId="+recipe.idReceta+" and RecetaId in (select RecetaId from Producto where CategoriaId!=5)";
            if (CommonUtils.ConexionBD.EjecutarConsulta(sqlQuery).Rows.Count > 0)
            {
                return false;
            }
            sqlQuery = "Select RecetaId from Producto where RecetaId="+recipe.idReceta+" and ProductoId in( Select ProductoId from Producto_Producto where ComboId in (Select ProductoId from Producto where CategoriaId=5))";

            if (CommonUtils.ConexionBD.EjecutarConsulta(sqlQuery).Rows.Count > 0)
            {
                return false;
            }
            return true;
        }
    }
}
