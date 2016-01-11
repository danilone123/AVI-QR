using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;


namespace UIRenderers
{
    public partial class PnlNuevaReceta : UserControl,CommonUtils.ActualizarGrids
    {
        CommonUtils.Receta receta;
        string currentValueCantidad = string.Empty;
        Dictionary<int,CommonUtils.Insumo> listaInsumosReceta = new Dictionary<int, CommonUtils.Insumo>();
        System.Collections.ArrayList arrayInsumos = new System.Collections.ArrayList();
        int rowPosition;
        float cantidadInsumo = -1;
        private static log4net.ILog log = log4net.LogManager.GetLogger("PnlNuevaReceta");
        public string MessageBarValue { get; set; }
        public CommonUtils.Receta Receta
        {
            get { return receta; }
            set { receta = value; }
        }

        private Image Image
        {
            get
            {
                return imageCollectionReceta.Images[0];
            }
        }
        public PnlNuevaReceta()
        {            
            InitializeComponent();
            InitConexionDB();
            CargarListaInsumos();
            CargarDataGridControlListaRecetas();
         //   dataGridViewListInsumoReceta.EditingControlShowing+=new DataGridViewEditingControlShowingEventHandler(dataGridViewListInsumoReceta_EditingControlShowing
                
            //    );
           // dataGridViewListInsumoReceta.CurrentCellDirtyStateChanged += new EventHandler(dataGridViewListInsumoReceta_CurrentCellDirtyStateChanged);
            // gridControl1.DataSource = CommonUtils.ConexionBD.EjecutarConsulta("select * from Insumo");
           // ((DevExpress.XtraGrid.Views.Grid.GridView)gridControl1.Views[0]).Columns[0].FieldName = "Cantidad";
                                
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            ((TextBox)e.Control).TextChanged += new EventHandler(Textbox_TextChanged);
        }
        private void Textbox_TextChanged(object sender, EventArgs e)
        {
            int row = this.dataGridViewListInsumoReceta.CurrentCell.RowIndex;
            int column = this.dataGridViewListInsumoReceta.CurrentCell.ColumnIndex;
            this.dataGridViewListInsumoReceta.Rows[row].Cells[column + 1].Value = ((TextBox)sender).Text; ;
        } 


        public PnlNuevaReceta(CommonUtils.Receta recetaAModificar)
        {
            InitializeComponent();
            InitConexionDB();            
            this.receta = recetaAModificar;
            CargarDatosDeReceta();
            CargarDataGridControlListaRecetas();
        }

        private void InitConexionDB()
        {
            CommonUtils.ConexionBD.StringDeConexion = CommonUtils.ConexionBD.GetConnectionString();
        }

        private void CargarDatosDeReceta()
        {
            txtNombreReceta.Text = receta.nombre;
            ValidateEmptyStringRule(txtNombreReceta);
            txtCostoReceta.Text = receta.costoReceta.ToString();
            listaInsumosReceta.Clear();
            for (int i = 0; i < receta.listaInsumos.Count; i++)
            {
                CommonUtils.Insumo insumo = ((CommonUtils.RecetaInsumo)receta.listaInsumos[i]).insumo;
                listaInsumosReceta.Add(insumo.idInsumo, insumo);
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridViewListInsumoReceta);                
                row.SetValues(insumo.idInsumo, insumo.nombre, insumo.costoUnidad.ToString(), ((CommonUtils.RecetaInsumo)receta.listaInsumos[i]).cantidad.ToString(), insumo.unidad);
                             
                dataGridViewListInsumoReceta.Rows.Add(row);                
            }
            CargarListaInsumos();
        }

        private void CargarDataGridControlListaRecetas()
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
                    recetaInsumo.cantidad = (float)reader.GetDouble(7);
                    receta.listaInsumos.Add(recetaInsumo);
                }
            }
            gridControlListaRecetas.ShowOnlyPredefinedDetails = true;
            gridControlListaRecetas.DataSource = null;
            gridControlListaRecetas.DataSource = list;//CommonUtils.ConexionBD.EjecutarConsulta("Select * from Receta");
            ((DevExpress.XtraGrid.Views.Grid.GridView)gridControlListaRecetas.Views[0]).Columns[0].FieldName = "idReceta";
            ((DevExpress.XtraGrid.Views.Grid.GridView)gridControlListaRecetas.Views[0]).Columns[1].FieldName = "nombre";
            ((DevExpress.XtraGrid.Views.Grid.GridView)gridControlListaRecetas.Views[0]).Columns[2].FieldName = "costoReceta";
            CommonUtils.ConexionBD.CerrarConexion();

        }

        
        private void CargarListaInsumos()
        {
            CommonUtils.ConexionBD.AbrirConexion();
            string sql = "Select InsumoId,Cantidad,CostoUnidad,Nombre,Presentacion,Marca,Unidad,CostoPresentacion from Insumo";
            SqlDataReader reader = CommonUtils.ConexionBD.EjecutarConsultaReader(sql);
            arrayInsumos.Clear();
            if (reader.HasRows)
            {
                CommonUtils.Insumo insumo;
                while (reader.Read())
                {
                    insumo = new CommonUtils.Insumo();
                    insumo.idInsumo = (int)reader.GetDecimal(0);
                    insumo.cantidad = (float)reader.GetDouble(1);
                    insumo.costoUnidad = (float)reader.GetDouble(2);
                    insumo.nombre = reader.GetString(3);
                    insumo.presentacion = reader.GetString(4);
                    insumo.marca = reader.GetString(5);
                    insumo.unidad = reader.GetString(6);
                    insumo.costoPresentacion = (float)reader.GetDouble(7);
                    arrayInsumos.Add(insumo);
                }
                loadListInsumos();
            }
            CommonUtils.ConexionBD.CerrarConexion();
        }

        private void ValidateEmptyStringRule(BaseEdit control)
        {
            if (control.Text == null || control.Text.Trim().Length == 0)
                dxErrorProvider.SetError(control, "Este campo no puede ser nulo", ErrorType.Critical);
            else
                dxErrorProvider.SetError(control, "");
        }

        private string GetConnectionString()
        {
            return @"Data Source=Marcelo-PC\SQLEXPRESS;Initial Catalog=Capresso;Integrated Security=SSPI;";//@"Data Source=Marcelo-PC\SQLEXPRESS;Database=Capresso;Initial Catalog=Capresso;Integrated Security=SSPI;";
        }

        private void loadListInsumos()
        {            
            //dataGridViewListInsumos.AutoGenerateColumns = false;
            dataGridViewListInsumos.DataSource = null;
            dataGridViewListInsumos.DataSource = arrayInsumos;
            ((DevExpress.XtraGrid.Views.Grid.GridView)dataGridViewListInsumos.Views[0]).Columns[0].FieldName="idInsumo";
            ((DevExpress.XtraGrid.Views.Grid.GridView)dataGridViewListInsumos.Views[0]).Columns[1].FieldName = "nombre";
            ((DevExpress.XtraGrid.Views.Grid.GridView)dataGridViewListInsumos.Views[0]).Columns[2].FieldName = "unidad";
                        /*dataGridViewListInsumos.Columns[0].DataPropertyName = "idInsumo";
            dataGridViewListInsumos.Columns[1].DataPropertyName = "nombre";
            dataGridViewListInsumos.Columns[2].DataPropertyName = "unidad";    */                     
        }       

        private Boolean isInsumoAlreadyInListRecetaInsumo()
        {
            Boolean isInsumoInList = false;
            int cellIdListInsumo =((CommonUtils.Insumo) ((DevExpress.XtraGrid.Views.Grid.GridView)dataGridViewListInsumos.Views[0]).GetFocusedRow()).idInsumo;//(Int32)dataGridViewListInsumos1.SelectedRows[0].Cells[0].Value;
            if (listaInsumosReceta.ContainsKey(cellIdListInsumo))
            {
                isInsumoInList = true;
            }
            
            return isInsumoInList;
        }

        private void SaveProduct() 
        {

            if (receta == null)
            {
                string Sql = "INSERT INTO Receta( " +
                       " Nombre,CostoReceta)" +
                       " VALUES ('" +CommonUtils.StringUtils.EscapeSQL( txtNombreReceta.Text )+ "', " + txtCostoReceta.Text + " ) ";
                try
                {
                    decimal value = CommonUtils.ConexionBD.ActualizarRetornandoId(Sql);// CommonUtils.ConexionBD.ObtenerUltimoId("Select @@IDENTITY");
                    receta = new CommonUtils.Receta();
                    receta.idReceta = (int)value;
                    receta.nombre = txtNombreReceta.Text;
                    for (int i = 0; i < dataGridViewListInsumoReceta.Rows.Count; i++)
                    {
                        string cantidad = dataGridViewListInsumoReceta.Rows[i].Cells[3].Value.ToString();
                        if (rowPosition == i && cantidadInsumo != -1)
                        {
                            cantidad = cantidadInsumo.ToString();
                        }
                        Sql = "INSERT INTO Receta_Insumo (RecetaId,InsumoId,Cantidad)  VALUES ('" +
                            value + "','" + dataGridViewListInsumoReceta.Rows[i].Cells[0].Value.ToString() + "','" + /*dataGridViewListInsumoReceta.Rows[i].Cells[3].Value.ToString()*/cantidad + "')";
                        CommonUtils.ConexionBD.Actualizar(Sql);
                        CommonUtils.RecetaInsumo recetaInsumo = new CommonUtils.RecetaInsumo();
                        recetaInsumo.insumo = listaInsumosReceta[int.Parse(dataGridViewListInsumoReceta.Rows[i].Cells[0].Value.ToString())];
                        recetaInsumo.cantidad = float.Parse(dataGridViewListInsumoReceta.Rows[i].Cells[3].Value.ToString());
                        receta.listaInsumos.Add(recetaInsumo);
                    }
                    MessageBarValue = "La receta " + txtNombreReceta.Text +
                                      " fue creado (a) satisfactoriamente.";
                    this.ResetFields();
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message, ex);
                    MessageBarValue = "No se pudo Crear la receta. Hubo un error: " + ex.Message;
                }
                finally
                {
                    alertControlNuevaReceta.Show(this.FindForm(), "Creación Receta.",MessageBarValue , Image);
                }
            }
            else 
            {
                try
                {
                    string Sql = "UPDATE Receta set Nombre='" + CommonUtils.StringUtils.EscapeSQL(txtNombreReceta.Text) + "',CostoReceta=" + txtCostoReceta.Text + " where RecetaId=" + receta.idReceta;
                    CommonUtils.ConexionBD.Actualizar(Sql);
                    Sql = "DELETE from Receta_Insumo WHERE RecetaId=" + receta.idReceta;
                    CommonUtils.ConexionBD.Actualizar(Sql);
                    for (int i = 0; i < dataGridViewListInsumoReceta.Rows.Count; i++)
                    {
                        string cantidad = dataGridViewListInsumoReceta.Rows[i].Cells[3].Value.ToString();
                        if (rowPosition == i && cantidadInsumo != -1)
                        {
                            cantidad = cantidadInsumo.ToString();
                        }
                        Sql = "INSERT INTO Receta_Insumo (RecetaId,InsumoId,Cantidad)  VALUES ('" +
                            receta.idReceta + "','" + dataGridViewListInsumoReceta.Rows[i].Cells[0].Value.ToString() + "','" + /*dataGridViewListInsumoReceta.Rows[i].Cells[3].Value.ToString()*/cantidad + "')";
                        CommonUtils.ConexionBD.Actualizar(Sql);
                    }
                    MessageBarValue ="La receta " + txtNombreReceta.Text +
                 " fue modificada (a) satisfactoriamente.";
                    actualizarGrid();
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message, ex);
                    MessageBarValue = "No se pudo Modificar la receta. Hubo un error: " + ex.Message;
                }
                finally
                {
                    alertControlNuevaReceta.Show(this.FindForm(), "Actualización Receta.",MessageBarValue , Image); 
                }
            
            }
         
        }

        private void ResetFields()
        {
            int selectedTabIndex = (this.ParentForm as mainForm).xtraTabControl.SelectedTabPageIndex;

            PnlNuevaReceta pnlRecet = new PnlNuevaReceta();
            pnlRecet.Dock = DockStyle.Fill;
            DevExpress.XtraTab.XtraTabPage tabItem = new DevExpress.XtraTab.XtraTabPage();
            tabItem.Controls.Add(pnlRecet);
            tabItem.Text = "nueva receta";
            (this.ParentForm as mainForm).xtraTabControl.TabPages.Add(tabItem);
            (this.ParentForm as mainForm).xtraTabControl.SelectedTabPage = tabItem;
           
            (this.ParentForm as mainForm).xtraTabControl.TabPages.RemoveAt(selectedTabIndex);
        }
        #region Events
        /*
        private void btnBuscarInsumos_click(object sender, EventArgs e)
        {
            CommonUtils.ConexionBD.AbrirConexion();
            string sql = "Select InsumoId,Cantidad,CostoUnidad,Nombre,Presentacion,Marca,Unidad from Insumo where Nombre  like '%" + txtBuscarInsumo.Text + "%'";
            SqlDataReader reader = CommonUtils.ConexionBD.EjecutarConsultaReader(sql);
            arrayInsumos.Clear();
            dataGridViewListInsumos1.DataSource = null;
            // dataGridViewListInsumos.Rows.Clear();
            if (reader.HasRows)
            {
                CommonUtils.Insumo insumo;
                while (reader.Read())
                {
                    insumo = new CommonUtils.Insumo();
                    insumo.idInsumo = (int)reader.GetDecimal(0);
                    insumo.cantidad = (float)reader.GetDouble(1);
                    insumo.costoUnidad = (float)reader.GetDouble(2);
                    insumo.nombre = reader.GetString(3);
                    insumo.presentacion = reader.GetString(4);
                    insumo.marca = reader.GetString(5);
                    insumo.unidad = reader.GetString(6);                    
                    arrayInsumos.Add(insumo);
                }
                loadListInsumos();
            }
            CommonUtils.ConexionBD.CerrarConexion();
        }
        */

        private void bttAddInsumo_click(object sender, EventArgs e)
        {            
            //if (dataGridViewListInsumos1.SelectedRows.Count <= 0)
            if ((CommonUtils.Insumo)((DevExpress.XtraGrid.Views.Grid.GridView)dataGridViewListInsumos.Views[0]).GetFocusedRow() == null)//to avoid problem when no insumo is selected
            {
                return;
            }


            if(dataGridViewListInsumos.Views[0].RowCount<=0)
            {
                return;
            }
            else if (isInsumoAlreadyInListRecetaInsumo())
            {
                return;
            }
            else 
            {
                CommonUtils.Insumo insumo = (CommonUtils.Insumo)((DevExpress.XtraGrid.Views.Grid.GridView)dataGridViewListInsumos.Views[0]).GetFocusedRow();
                
                listaInsumosReceta.Add(insumo.idInsumo, insumo);
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridViewListInsumoReceta);                             
                row.SetValues(insumo.idInsumo, insumo.nombre, insumo.costoUnidad.ToString(), "0.00", insumo.unidad);
                dataGridViewListInsumoReceta.Rows.Add(row);
                   /* int cellId = (Int32)dataGridViewListInsumos1.SelectedRows[0].Cells[0].Value;                
                CommonUtils.Insumo insumo = dataGridViewListInsumos1.SelectedRows[0].DataBoundItem as CommonUtils.Insumo;
              //  string cellInsumoNombre = (string)dataGridViewListInsumos.SelectedRows[0].Cells[1].Value;
                listaInsumosReceta.Add(cellId,insumo);
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridViewListInsumoReceta);
                row.SetValues(cellId, insumo.nombre,insumo.costoUnidad.ToString() ,"0.00",insumo.unidad);
                dataGridViewListInsumoReceta.Rows.Add(row);
                */
               
            }

        }

       
        private void cb_KeyUp(object sender, EventArgs e) 
        {
            System.Windows.Forms.ComboBox cb = (System.Windows.Forms.ComboBox)sender;
           // string c =(string)e.KeyChar ;
            CommonUtils.ConexionBD.AbrirConexion();
            cb.DataSource = CommonUtils.ConexionBD.EjecutarConsulta("Select * from Insumo  where Nombre like '%" + cb.SelectedText + "%'");
            CommonUtils.ConexionBD.CerrarConexion();
         
            // comboBox.DataPropertyName = "Nombre";
            cb.ValueMember = "Nombre";
            cb.SelectedIndex = -1;                       
        }
        
             

        private void dataGridViewListInsumoReceta_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (e.Row.Index>= 0)//check if a row is selected
            {
                int cellId = (Int32)dataGridViewListInsumoReceta.SelectedRows[0].Cells[0].Value;
                 listaInsumosReceta.Remove(cellId);
                 float valueDeleted = float.Parse((string)dataGridViewListInsumoReceta.SelectedRows[0].Cells[3].Value) * float.Parse((string)dataGridViewListInsumoReceta.SelectedRows[0].Cells[2].Value);
                txtCostoReceta.Text= (float.Parse(txtCostoReceta.Text) - valueDeleted).ToString();
                // dataGridViewListInsumoReceta.Rows.Remove(dataGridViewListInsumoReceta.Rows[e.Row.Index]);
            }
        }
       
        private void txtNombreReceta_Validating(object sender, CancelEventArgs e)
        {
            ValidateEmptyStringRule(sender as BaseEdit);
        }       

        private void dataGridViewListInsumoReceta_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {            
            cantidadInsumo = -1;
           
        }
       

        //event handler used in order to avoid a bug with the calculation of the price
        private void dataGridViewListInsumoReceta_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            rowPosition = e.RowIndex;
            //cantidadInsumo = float.Parse((string)dataGridViewListInsumoReceta.Rows[e.RowIndex].Cells[3].Value);//(string)dataGridViewListInsumoReceta.Rows[e.RowIndex].Cells[3].Value;

            currentValueCantidad = (float.Parse((string)dataGridViewListInsumoReceta.Rows[e.RowIndex].Cells[3].Value) * float.Parse((string)dataGridViewListInsumoReceta.Rows[e.RowIndex].Cells[2].Value)).ToString();//(string)dataGridViewListInsumoReceta.Rows[e.RowIndex].Cells[3].Value;
        }

        //will open an edit receta for the table in the right side
        private void gridViewListaDeRecetas_DoubleClick(object sender, EventArgs e)
        {
      
        }

        //
        private void dataGridViewListInsumoReceta_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if ((this.dataGridViewListInsumoReceta.CurrentCell.ColumnIndex == 3) && !(e.Control == null))
            {
                TextBox tb = (TextBox)e.Control;                
                tb.KeyPress += new KeyPressEventHandler(tb_KeyPress);
                tb.TextChanged += new EventHandler(tb_TextChanged);
            }
        }

        void tb_TextChanged(object sender, EventArgs e)
        {
            TextBox txtBox = sender as TextBox;
            cantidadInsumo = float.Parse(txtBox.Text == string.Empty ? "0" : txtBox.Text);
            float cantidadValue = float.Parse(txtBox.Text == string.Empty ? "0" : txtBox.Text) * float.Parse((string)dataGridViewListInsumoReceta.Rows[dataGridViewListInsumoReceta.CurrentRow.Index].Cells[2].Value);
            float cantidadValueTotal = cantidadValue + float.Parse(txtCostoReceta.Text) - (float.Parse(currentValueCantidad));
            txtCostoReceta.Text = cantidadValueTotal.ToString(); //String.Format("{0:0.00}", cantidadValueTotal);
            //  cantidadValueTotal.ToString();
            currentValueCantidad = cantidadValue.ToString();
        }

        //method created in order to validate if cantidad is a correct value.   
        void tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox tb=(TextBox )sender;
            if (tb.Text == string.Empty && e.KeyChar == '.')
            {
                e.Handled = true;
                return;
            }
            //if textbox already has a decimal point
            if (tb.Text.Contains('.') && e.KeyChar == '.')
            {
                e.Handled = true;
                return;
            }
            //if the key pressed is not a valid decimal number
            if(!((char.IsDigit(e.KeyChar)) || char.IsControl(e.KeyChar)|| (e.KeyChar=='.')))
            {
                e.Handled = true;
            }
        }

        #endregion


        #region ActualizarGrids Members

        //this method will be called after saving a receta or when the end user clicks de update button
        public void actualizarGrid()
        {
            CargarDataGridControlListaRecetas();
            CargarListaInsumos();
            ActualizarListaDeInsumosEnReceta();
            //fixme it is necesary to see what happend with the datagridviewListInsumos when an update happens
        }

        #endregion
       
        //if an insumo was changed the list of insumos that the recipe has will change in order
        // to get the new values from insumos
        public void ActualizarListaDeInsumosEnReceta()
        {
            //dataGridViewListInsumoReceta.
            float costoReceta = 0.0f;
            for (int i = 0; i < dataGridViewListInsumoReceta.RowCount; i++)
                {                   
                DataGridViewRow r = dataGridViewListInsumoReceta.Rows[i];
                string cantidad = r.Cells[3].Value.ToString();
                if (rowPosition == i && cantidadInsumo != -1)
                {
                    cantidad = cantidadInsumo.ToString();
                }
                int idInsumo = int.Parse(r.Cells[0].Value.ToString());
                string query = "select CostoUnidad from Insumo where InsumoId=" + idInsumo;
                CommonUtils.ConexionBD.AbrirConexion();
                SqlDataReader reader = CommonUtils.ConexionBD.EjecutarConsultaReader(query);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        r.Cells[2].Value = ((float)reader.GetDouble(0)).ToString();
                        costoReceta = costoReceta + (float.Parse(cantidad/*r.Cells[3].Value.ToString()*/) * float.Parse(r.Cells[2].Value.ToString()));
                       
                    }
                }
                CommonUtils.ConexionBD.CerrarConexion();
            }
            txtCostoReceta.Text = costoReceta.ToString();
        }

        private void barButtonSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ValidateEmptyStringRule(txtNombreReceta);
            if (listaInsumosReceta.Count <= 0)
            {
                MessageBox.Show(this, "Debe seleccionar insumo", "Recetas", MessageBoxButtons.OK);

            }
            else if (!dxErrorProvider.HasErrors)
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

        private void GetErrorProviderMessages()
        {
            IList<Control> controlErrors = dxErrorProvider.GetControlsWithError();
            //    controlErrors.OrderBy<>;
            alertControlNuevaReceta.Show( this.FindForm( ) , "Falló al guardar." , "Los cambios NO se guardaron.  Por favor, intente nuevamente." , Image );
            MessageBox.Show(this, (string)controlErrors[0].Tag, dxErrorProvider.GetError(controlErrors[0]), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void barButtonClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if ((this.ParentForm as mainForm).ContextControlsForRecetas.ContainsValue((this.ParentForm as mainForm).xtraTabControl.SelectedTabPage.Controls[0]))
            {
                (this.ParentForm as mainForm).ContextControlsForRecetas.Remove(receta.idReceta.ToString());
            }
            (this.ParentForm as mainForm).xtraTabControl.TabPages.RemoveAt((this.ParentForm as mainForm).xtraTabControl.SelectedTabPageIndex);

        }

        }
}
