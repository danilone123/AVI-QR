using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using System.Data.SqlClient;
using System.Collections;
using CommonUtils;

namespace UIRenderers
{
    public partial class NuevoInsumo : DevExpress.XtraEditors.XtraUserControl,CommonUtils.ActualizarGrids
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger("NuevoInsumo");
        public string MessageBarValue { get; set; }
        private CommonUtils.Insumo insumo;
        public CommonUtils.Insumo Insumo 
        {
            get { return insumo; }
            set{insumo=value;}
        }
        public NuevoInsumo()
        {
            InitializeComponent();
            InitConexionDB();
            InitFields();
            CargarListaInsumos();
           
            
        }
        public NuevoInsumo(CommonUtils.Insumo insumoAModificar)
        {
            InitializeComponent();
            insumo = insumoAModificar;
            CargarDatosInsumo();
            CargarListaInsumos();            
        }

        private void InitFields()
        {
            txtNomInsumo.Text = string.Empty;
            txtMarca.Text = string.Empty;
            txtPresentacion.Text = string.Empty;
            txtUnidad.Text = string.Empty;            
        }

        private void InitConexionDB() 
        {
            CommonUtils.ConexionBD.StringDeConexion = CommonUtils.ConexionBD.GetConnectionString();//GetConnectionString();
        }       

        //called when the user wants to see an specific insumo, this method will load the values 
        //to the controls
        private void CargarDatosInsumo()
        {
            
            txtNomInsumo.Text = insumo.nombre;
            txtCostoPresentacion.EditValue = Decimal.Parse(insumo.costoPresentacion.ToString());
            txtCantidad.EditValue =Decimal.Parse(insumo.cantidad.ToString());
            txtMarca.Text = insumo.marca;
            txtPresentacion.Text = insumo.presentacion;
            txtUnidad.Text = insumo.unidad;
            txtCostoUnit.EditValue = Decimal.Parse(insumo.costoUnidad.ToString());
           
        }

        //called to load the list of the right that contains all the insumos
        private void CargarListaInsumos()
        {
            ArrayList listaInsumos = new ArrayList();
            string sql = "select * from Insumo ORDER BY InsumoId DESC";
            CommonUtils.ConexionBD.AbrirConexion();
            SqlDataReader reader = CommonUtils.ConexionBD.EjecutarConsultaReader(sql);
            if (reader.HasRows)
            {
                CommonUtils.Insumo insumoAMostrar;// = new CommonUtils.Insumo();

                while (reader.Read())
                {
                    insumoAMostrar = new CommonUtils.Insumo();
                    insumoAMostrar.idInsumo = (int)reader.GetDecimal(0);
                    insumoAMostrar.cantidad = (float)reader.GetDouble(1);
                    insumoAMostrar.costoPresentacion = (float)reader.GetDouble(7);
                    insumoAMostrar.nombre = reader.GetString(3);
                    insumoAMostrar.presentacion = reader.GetString(4);
                    insumoAMostrar.marca = reader.GetString(5);
                    insumoAMostrar.unidad = reader.GetString(6);
                    insumoAMostrar.costoUnidad = (float)reader.GetDouble(2);
                    listaInsumos.Add(insumoAMostrar);
    
                }
            }
            gridControlListaInsumos.DataSource = null;
            gridControlListaInsumos.DataSource = listaInsumos;
            ((DevExpress.XtraGrid.Views.Grid.GridView)gridControlListaInsumos.Views[0]).Columns[0].FieldName = "idInsumo";
            ((DevExpress.XtraGrid.Views.Grid.GridView)gridControlListaInsumos.Views[0]).Columns[1].FieldName = "nombre";
            ((DevExpress.XtraGrid.Views.Grid.GridView)gridControlListaInsumos.Views[0]).Columns[2].FieldName = "cantidad";
            ((DevExpress.XtraGrid.Views.Grid.GridView)gridControlListaInsumos.Views[0]).Columns[3].FieldName = "unidad";
            ((DevExpress.XtraGrid.Views.Grid.GridView)gridControlListaInsumos.Views[0]).Columns[4].FieldName = "costoUnidad";
            CommonUtils.ConexionBD.CerrarConexion();
        }

        private void ValidateEmptyStringRule(BaseEdit control)
        {
            if (control.Text == null || control.Text.Trim().Length == 0)
                dxErrorProvider.SetError(control, "Este campo no puede ser nulo", ErrorType.Critical);
            else
                dxErrorProvider.SetError(control, "");
        }

        private void ValidateLessThanMinRule(BaseEdit control, Decimal min)
        {
            /*if (!(control.EditValue is Decimal))
                return;*/
            
            if ((Decimal)control.EditValue <= min)
                dxErrorProvider.SetError(control, "Porfavor, ingrese un valor mayor a " + min.ToString(), ErrorType.Warning);
            else
                dxErrorProvider.SetError(control, "");                               
        }

        private void ValidateFields()
        {                   // 
            ValidateLessThanMinRule(txtCantidad, Decimal.Zero);
            ValidateEmptyStringRule(txtUnidad);
            ValidateLessThanMinRule(txtCostoPresentacion,Decimal.Zero);
            ValidateEmptyStringRule(txtNomInsumo);                         
        }

        private void GetErrorProviderMessages()
        {            
            IList<Control> controlErrors = dxErrorProvider.GetControlsWithError();
        //    controlErrors.OrderBy<>;
            alertControlInsumos.Show( this.FindForm( ) , "Falló al guardar." , "Los cambios NO se guardaron.  Por favor, intente nuevamente." , Image );
            MessageBox.Show(this, (string)controlErrors[0].Tag, dxErrorProvider.GetError(controlErrors[0]), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void SaveProduct()
        {
            float cantidad=float.Parse(txtCantidad.Text);
            if (insumo == null)
            {
                string Sql = "INSERT INTO Insumo( " +
                      " Cantidad,CostoUnidad,Nombre,Presentacion,Marca,Unidad,CostoPresentacion,CantidadInsumoEnIventario)" +
                    //" VALUES ('"+TBStyleDescription.Text + "', " +Team + ", " + TBOrder.Text+" ,'"+TBFactor.Text+"' ,'"+TBStyleAbbreviation.Text+"' )" ;
                      " VALUES ('" + cantidad + "', " + txtCostoUnit.Text + ", '" + StringUtils.EscapeSQL( txtNomInsumo.Text ) + "' ,'" + StringUtils.EscapeSQL(txtPresentacion.Text) + "', '" +StringUtils.EscapeSQL( txtMarca.Text) + "','" + StringUtils.EscapeSQL(txtUnidad.Text) + "','" + txtCostoPresentacion.Text + "','0.0')";
                //CommonUtils.ConexionBD.Actualizar(Sql);
                try
                {
                    decimal idInsumo = CommonUtils.ConexionBD.ActualizarRetornandoId(Sql);
                    insumo = new CommonUtils.Insumo();
                    insumo.idInsumo = (int)idInsumo;
                    insumo.cantidad = float.Parse(txtCantidad.Text);
                    insumo.costoUnidad = float.Parse(txtCostoUnit.Text);
                    insumo.nombre = txtNomInsumo.Text;
                    insumo.presentacion = txtPresentacion.Text;
                    insumo.marca = txtMarca.Text;
                    insumo.unidad = txtUnidad.Text;
                    insumo.costoPresentacion = float.Parse(txtCostoPresentacion.Text);
                    MessageBarValue = "El insumo " + txtNomInsumo.Text +
                      " fue creado (a) satisfactoriamente.";
                   
                    this.ResetFields();
                   
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message, ex);
                    MessageBarValue = "No se pudo Crear el insumo. Hubo un error: " + ex.Message;
                }
                finally
                {
                    alertControlInsumos.Show(this.FindForm(), "Creación Insumos.", MessageBarValue, Image);
                }
            }
            else 
            {
                ArrayList listQuery = new ArrayList();
                string sqlQuery = "SELECT  Receta.RecetaId, Receta.CostoReceta, Insumo.InsumoId, Insumo.CostoUnidad," +
               " Receta_Insumo.Cantidad, Insumo.CostoUnidad * Receta_Insumo.Cantidad AS Expr1," +
                    "("+txtCostoUnit.Text+" * Receta_Insumo.Cantidad + Receta.CostoReceta)" +
                 "- Insumo.CostoUnidad * Receta_Insumo.Cantidad AS Expr2 FROM Receta INNER JOIN" +
                 " Receta_Insumo ON Receta.RecetaId = Receta_Insumo.RecetaId INNER JOIN" +
                 " Insumo ON Insumo.InsumoId = Receta_Insumo.InsumoId" +
                   " WHERE (Receta_Insumo.InsumoId = "+insumo.idInsumo+")";
                try
                {
                    CommonUtils.ConexionBD.AbrirConexion();
                    SqlDataReader reader = CommonUtils.ConexionBD.EjecutarConsultaReader(sqlQuery);

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int idReceta = (int)reader.GetDecimal(0);
                            string nuevoCostoReceta = ((float)reader.GetDouble(6)).ToString();
                            string queryUpdate = "Update Receta set CostoReceta=" + nuevoCostoReceta + " where RecetaId=" + idReceta;
                            listQuery.Add(queryUpdate);
                        }
                    }
                    CommonUtils.ConexionBD.CerrarConexion();
                    for (int i = 0; i < listQuery.Count; i++)
                    {
                        CommonUtils.ConexionBD.Actualizar((string)listQuery[i]);
                    }
                    string Sql = "update Insumo set Cantidad='" + cantidad + "',CostoUnidad='" + txtCostoUnit.Text + "', Nombre='" + StringUtils.EscapeSQL(txtNomInsumo.Text) + "', Presentacion='" + StringUtils.EscapeSQL(txtPresentacion.Text) + "', Marca='" + StringUtils.EscapeSQL(txtMarca.Text) + "', Unidad='" + StringUtils.EscapeSQL(txtUnidad.Text) + "', CostoPresentacion='" + txtCostoPresentacion.Text + "' where InsumoId='" + insumo.idInsumo + "'";
                    CommonUtils.ConexionBD.Actualizar(Sql);
                    Sql = "UPDATE Compras set EsModificable='False' where InsumoId=" + insumo.idInsumo;
                    CommonUtils.ConexionBD.Actualizar(Sql);
                    actualizarGrid();
                    MessageBarValue="El insumo " + txtNomInsumo.Text +
                     " fue modificado (a) satisfactoriamente.";
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message, ex);
                    MessageBarValue = "No se pudo Modificar el insumo. Hubo un error: " + ex.Message;
                }
                finally
                {
                    alertControlInsumos.Show(this.FindForm(), "Actualización Insumo.",MessageBarValue , Image);
                }

            }
          
        }
        private Image Image
        {
            get
            {
                return imageCollectionUsers.Images[0];
            }
        }
        //method called after a new insumo is created. If the recipe is being edited this method is not 
        //called
        private void ResetFields()
        {
            int selectedTabIndex = (this.ParentForm as mainForm).xtraTabControl.SelectedTabPageIndex;

            NuevoInsumo pnlInsumo = new NuevoInsumo();
            pnlInsumo.Dock = DockStyle.Fill;
            DevExpress.XtraTab.XtraTabPage tabItem = new DevExpress.XtraTab.XtraTabPage();
            tabItem.Controls.Add(pnlInsumo);
            tabItem.Text = "nuevo insumo";
            (this.ParentForm as mainForm).xtraTabControl.TabPages.Add(tabItem);
            (this.ParentForm as mainForm).xtraTabControl.SelectedTabPage = tabItem;
            (this.ParentForm as mainForm).xtraTabControl.TabPages.RemoveAt(selectedTabIndex);
        }
        #region Events & Handlers

        protected override void OnVisibleChanged(EventArgs e)
        {
            if (Visible)
            {
               // InitFields();
                ValidateFields();
            }
            base.OnVisibleChanged(e);
        }

        private void txtCantidad_Validating(object sender, CancelEventArgs e)
        {
            ValidateLessThanMinRule(sender as BaseEdit, Decimal.Zero);
         
        }

        private void txtNomInsumo_Validating(object sender, CancelEventArgs e)
        {
            ValidateEmptyStringRule(sender as BaseEdit);
        }

        private void txtUnidad_Validating(object sender, CancelEventArgs e)
        {
            ValidateEmptyStringRule(sender as BaseEdit);
        }

        private void txtCostoPresentacion_Validating(object sender, CancelEventArgs e)
        {
            ValidateLessThanMinRule(sender as BaseEdit, Decimal.Zero);
        }

        private void gridViewListaDeInsumos_DoubleClick(object sender, EventArgs e)
        {
            
        }
        #endregion





        #region ActualizarGrids Members

        public void actualizarGrid()
        {
            CargarListaInsumos();
        }
    
        #endregion

        private void barButtonsave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ValidateFields();
            if (!dxErrorProvider.HasErrors)
            {
                SaveProduct();
            }
            else
            {
                GetErrorProviderMessages();
            }

        }

        private void barButtonUpdate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            actualizarGrid();
        }

        private void barButtonClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if ((this.ParentForm as mainForm).ContextControlsForInsumo.ContainsValue((this.ParentForm as mainForm).xtraTabControl.SelectedTabPage.Controls[0]))
            {
                (this.ParentForm as mainForm).ContextControlsForInsumo.Remove(insumo.idInsumo.ToString());

            }
            (this.ParentForm as mainForm).xtraTabControl.TabPages.RemoveAt((this.ParentForm as mainForm).xtraTabControl.SelectedTabPageIndex);
        }

        private void txtCantidad_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (float.Parse(e.NewValue.ToString())>0 /*|| float.Parse(txtCostoPresentacion.Text) > 0*/)
            {
                float value = float.Parse(txtCostoPresentacion.EditValue.ToString()) / float.Parse(e.NewValue.ToString());
                
                txtCostoUnit.Text = value.ToString();
            }
        }

     

        private void txtCostoPresentacion_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (/*!e.NewValue.ToString().Equals("$0.00") &&*/ ((Decimal)txtCantidad.EditValue)>0 /*|| float.Parse(txtCostoPresentacion.Text) > 0*/)
            {
                float value = float.Parse(e.NewValue.ToString()) / float.Parse(txtCantidad.EditValue.ToString());
                txtCostoUnit.Text = value.ToString();
            }
        }
    }
}
