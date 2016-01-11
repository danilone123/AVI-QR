namespace UIRenderers
{
    partial class PnlNuevaReceta
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PnlNuevaReceta));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dxErrorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barButtonSave = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonUpdate = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonClose = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.groupControl = new DevExpress.XtraEditors.GroupControl();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.gridControlListaRecetas = new DevExpress.XtraGrid.GridControl();
            this.gridViewListaDeRecetas = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnIdReceta = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnNombreReceta = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnCostoReceta = new DevExpress.XtraGrid.Columns.GridColumn();
            this.splitterControl = new DevExpress.XtraEditors.SplitterControl();
            this.alertControlNuevaReceta = new DevExpress.XtraBars.Alerter.AlertControl(this.components);
            this.imageCollectionReceta = new DevExpress.Utils.ImageCollection(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridViewListInsumos = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtNombreReceta = new DevExpress.XtraEditors.TextEdit();
            this.txtCostoReceta = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewListInsumoReceta = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAddInsumo = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl)).BeginInit();
            this.groupControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlListaRecetas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewListaDeRecetas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollectionReceta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewListInsumos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNombreReceta.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCostoReceta.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewListInsumoReceta)).BeginInit();
            this.SuspendLayout();
            // 
            // dxErrorProvider
            // 
            this.dxErrorProvider.ContainerControl = this;
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ApplicationButtonText = null;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonSave,
            this.barButtonUpdate,
            this.barButtonClose});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 3;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbonControl1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2010;
            this.ribbonControl1.SelectedPage = this.ribbonPage1;
            this.ribbonControl1.Size = new System.Drawing.Size(1014, 141);
            // 
            // barButtonSave
            // 
            this.barButtonSave.Caption = "Guardar";
            this.barButtonSave.Id = 0;
            this.barButtonSave.LargeGlyph = global::UIRenderers.Properties.Resources.guardar;
            this.barButtonSave.Name = "barButtonSave";
            this.barButtonSave.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.barButtonSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonSave_ItemClick);
            // 
            // barButtonUpdate
            // 
            this.barButtonUpdate.Caption = "Actualizar";
            this.barButtonUpdate.Id = 1;
            this.barButtonUpdate.LargeGlyph = global::UIRenderers.Properties.Resources.refresh;
            this.barButtonUpdate.Name = "barButtonUpdate";
            this.barButtonUpdate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonUpdate_ItemClick);
            // 
            // barButtonClose
            // 
            this.barButtonClose.Caption = "Cerrar";
            this.barButtonClose.Id = 2;
            this.barButtonClose.LargeGlyph = global::UIRenderers.Properties.Resources.close;
            this.barButtonClose.Name = "barButtonClose";
            this.barButtonClose.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.barButtonClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonClose_ItemClick);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "Opciones";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonSave);
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonUpdate);
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonClose);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "Principal";
            // 
            // groupControl
            // 
            this.groupControl.Appearance.BackColor = System.Drawing.Color.Aqua;
            this.groupControl.Appearance.Options.UseBackColor = true;
            this.groupControl.Controls.Add(this.splitContainerControl1);
            this.groupControl.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupControl.Location = new System.Drawing.Point(715, 141);
            this.groupControl.Name = "groupControl";
            this.groupControl.Size = new System.Drawing.Size(299, 561);
            this.groupControl.TabIndex = 25;
            this.groupControl.Text = "Busqueda de recetas";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.splitContainerControl1.Appearance.Options.UseBackColor = true;
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(2, 22);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.gridControlListaRecetas);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(295, 537);
            this.splitContainerControl1.SplitterPosition = 371;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // gridControlListaRecetas
            // 
            this.gridControlListaRecetas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlListaRecetas.Location = new System.Drawing.Point(0, 0);
            this.gridControlListaRecetas.MainView = this.gridViewListaDeRecetas;
            this.gridControlListaRecetas.Name = "gridControlListaRecetas";
            this.gridControlListaRecetas.Size = new System.Drawing.Size(295, 371);
            this.gridControlListaRecetas.TabIndex = 0;
            this.gridControlListaRecetas.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewListaDeRecetas});
            this.gridControlListaRecetas.DoubleClick += new System.EventHandler(this.gridViewListaDeRecetas_DoubleClick);
            // 
            // gridViewListaDeRecetas
            // 
            this.gridViewListaDeRecetas.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnIdReceta,
            this.gridColumnNombreReceta,
            this.gridColumnCostoReceta});
            this.gridViewListaDeRecetas.DefaultRelationIndex = 1;
            this.gridViewListaDeRecetas.GridControl = this.gridControlListaRecetas;
            this.gridViewListaDeRecetas.Name = "gridViewListaDeRecetas";
            this.gridViewListaDeRecetas.OptionsView.ShowAutoFilterRow = true;
            this.gridViewListaDeRecetas.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumnIdReceta
            // 
            this.gridColumnIdReceta.Caption = "Id";
            this.gridColumnIdReceta.Name = "gridColumnIdReceta";
            this.gridColumnIdReceta.OptionsColumn.AllowEdit = false;
            this.gridColumnIdReceta.OptionsColumn.AllowFocus = false;
            this.gridColumnIdReceta.Visible = true;
            this.gridColumnIdReceta.VisibleIndex = 0;
            this.gridColumnIdReceta.Width = 78;
            // 
            // gridColumnNombreReceta
            // 
            this.gridColumnNombreReceta.Caption = "Nombre";
            this.gridColumnNombreReceta.Name = "gridColumnNombreReceta";
            this.gridColumnNombreReceta.OptionsColumn.AllowEdit = false;
            this.gridColumnNombreReceta.OptionsColumn.AllowFocus = false;
            this.gridColumnNombreReceta.Visible = true;
            this.gridColumnNombreReceta.VisibleIndex = 1;
            this.gridColumnNombreReceta.Width = 65;
            // 
            // gridColumnCostoReceta
            // 
            this.gridColumnCostoReceta.Caption = "Costo Receta";
            this.gridColumnCostoReceta.Name = "gridColumnCostoReceta";
            this.gridColumnCostoReceta.OptionsColumn.AllowEdit = false;
            this.gridColumnCostoReceta.OptionsColumn.AllowFocus = false;
            this.gridColumnCostoReceta.Visible = true;
            this.gridColumnCostoReceta.VisibleIndex = 2;
            // 
            // splitterControl
            // 
            this.splitterControl.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitterControl.Location = new System.Drawing.Point(709, 141);
            this.splitterControl.Name = "splitterControl";
            this.splitterControl.Size = new System.Drawing.Size(6, 561);
            this.splitterControl.TabIndex = 26;
            this.splitterControl.TabStop = false;
            // 
            // imageCollectionReceta
            // 
            this.imageCollectionReceta.ImageSize = new System.Drawing.Size(32, 32);
            this.imageCollectionReceta.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollectionReceta.ImageStream")));
            this.imageCollectionReceta.Images.SetKeyName(0, "alertIcon.png");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.label3.Location = new System.Drawing.Point(7, 196);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 15);
            this.label3.TabIndex = 42;
            this.label3.Text = "Nombre de la receta";
            // 
            // dataGridViewListInsumos
            // 
            this.dataGridViewListInsumos.Location = new System.Drawing.Point(10, 343);
            this.dataGridViewListInsumos.MainView = this.gridView1;
            this.dataGridViewListInsumos.Name = "dataGridViewListInsumos";
            this.dataGridViewListInsumos.Size = new System.Drawing.Size(307, 169);
            this.dataGridViewListInsumos.TabIndex = 41;
            this.dataGridViewListInsumos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3});
            this.gridView1.GridControl = this.dataGridViewListInsumos;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsFilter.AllowFilterEditor = false;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Id";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.AllowFocus = false;
            this.gridColumn1.OptionsFilter.AllowFilter = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 40;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Insumo";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.AllowFocus = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Unidad";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.AllowFocus = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // txtNombreReceta
            // 
            this.dxErrorProvider.SetError(this.txtNombreReceta, "Este campo no puede ser vacio");
            this.dxErrorProvider.SetErrorType(this.txtNombreReceta, DevExpress.XtraEditors.DXErrorProvider.ErrorType.Default);
            this.txtNombreReceta.Location = new System.Drawing.Point(161, 192);
            this.txtNombreReceta.Name = "txtNombreReceta";
            this.txtNombreReceta.Size = new System.Drawing.Size(135, 20);
            this.txtNombreReceta.TabIndex = 40;
            this.txtNombreReceta.Tag = "Nombre de la receta";
            // 
            // txtCostoReceta
            // 
            this.txtCostoReceta.EditValue = "0.00";
            this.txtCostoReceta.Enabled = false;
            this.txtCostoReceta.Location = new System.Drawing.Point(160, 220);
            this.txtCostoReceta.Name = "txtCostoReceta";
            this.txtCostoReceta.Size = new System.Drawing.Size(77, 20);
            this.txtCostoReceta.TabIndex = 39;
            // 
            // label1
            // 
            this.label1.AutoEllipsis = true;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(197)))), ((int)(((byte)(255)))));
            this.label1.Location = new System.Drawing.Point(6, 274);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 19);
            this.label1.TabIndex = 38;
            this.label1.Text = "Insumos de la Receta";
            // 
            // dataGridViewListInsumoReceta
            // 
            this.dataGridViewListInsumoReceta.AllowUserToAddRows = false;
            this.dataGridViewListInsumoReceta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewListInsumoReceta.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5});
            this.dataGridViewListInsumoReceta.Location = new System.Drawing.Point(375, 343);
            this.dataGridViewListInsumoReceta.MultiSelect = false;
            this.dataGridViewListInsumoReceta.Name = "dataGridViewListInsumoReceta";
            this.dataGridViewListInsumoReceta.Size = new System.Drawing.Size(307, 169);
            this.dataGridViewListInsumoReceta.TabIndex = 37;
            this.dataGridViewListInsumoReceta.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewListInsumoReceta_CellEndEdit);
            this.dataGridViewListInsumoReceta.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewListInsumoReceta_CellEnter);
            this.dataGridViewListInsumoReceta.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridViewListInsumoReceta_EditingControlShowing);
            this.dataGridViewListInsumoReceta.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dataGridViewListInsumoReceta_UserDeletingRow);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Id";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 50;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Insumo";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 80;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Costo Unidad";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 70;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = "0.0";
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTextBoxColumn4.HeaderText = "Cantidad";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Unidad";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn5.Width = 50;
            // 
            // btnAddInsumo
            // 
            this.btnAddInsumo.Location = new System.Drawing.Point(327, 386);
            this.btnAddInsumo.Name = "btnAddInsumo";
            this.btnAddInsumo.Size = new System.Drawing.Size(38, 23);
            this.btnAddInsumo.TabIndex = 36;
            this.btnAddInsumo.Text = ">>";
            this.btnAddInsumo.UseVisualStyleBackColor = true;
            this.btnAddInsumo.Click += new System.EventHandler(this.bttAddInsumo_click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.label2.Location = new System.Drawing.Point(7, 225);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 15);
            this.label2.TabIndex = 35;
            this.label2.Text = "Costo de la receta";
            // 
            // label4
            // 
            this.label4.AutoEllipsis = true;
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(197)))), ((int)(((byte)(255)))));
            this.label4.Location = new System.Drawing.Point(3, 175);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 19);
            this.label4.TabIndex = 34;
            this.label4.Text = "Básico";
            // 
            // PnlNuevaReceta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dataGridViewListInsumos);
            this.Controls.Add(this.txtNombreReceta);
            this.Controls.Add(this.txtCostoReceta);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridViewListInsumoReceta);
            this.Controls.Add(this.btnAddInsumo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.splitterControl);
            this.Controls.Add(this.groupControl);
            this.Controls.Add(this.ribbonControl1);
            this.Name = "PnlNuevaReceta";
            this.Size = new System.Drawing.Size(1014, 702);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl)).EndInit();
            this.groupControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlListaRecetas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewListaDeRecetas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollectionReceta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewListInsumos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNombreReceta.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCostoReceta.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewListInsumoReceta)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider;
        private DevExpress.XtraEditors.SplitterControl splitterControl;
        private DevExpress.XtraEditors.GroupControl groupControl;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraGrid.GridControl gridControlListaRecetas;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewListaDeRecetas;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnIdReceta;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNombreReceta;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnCostoReceta;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.BarButtonItem barButtonSave;
        private DevExpress.XtraBars.BarButtonItem barButtonUpdate;
        private DevExpress.XtraBars.BarButtonItem barButtonClose;
        private DevExpress.XtraBars.Alerter.AlertControl alertControlNuevaReceta;
        private DevExpress.Utils.ImageCollection imageCollectionReceta;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraGrid.GridControl dataGridViewListInsumos;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.TextEdit txtNombreReceta;
        private DevExpress.XtraEditors.TextEdit txtCostoReceta;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridViewListInsumoReceta;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.Button btnAddInsumo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
    }
}
