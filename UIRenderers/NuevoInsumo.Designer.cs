namespace UIRenderers
{
    partial class NuevoInsumo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NuevoInsumo));
            this.dxErrorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.txtUnidad = new DevExpress.XtraEditors.TextEdit();
            this.txtCostoPresentacion = new DevExpress.XtraEditors.TextEdit();
            this.txtCantidad = new DevExpress.XtraEditors.TextEdit();
            this.txtNomInsumo = new DevExpress.XtraEditors.TextEdit();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.barButtonAdd = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barButtonsave = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonUpdate = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonClose = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup3 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.groupControl = new DevExpress.XtraEditors.GroupControl();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.gridControlListaInsumos = new DevExpress.XtraGrid.GridControl();
            this.gridViewListaDeInsumos = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnNombre = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnCantidad = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnUnidad = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnCostoUnidad = new DevExpress.XtraGrid.Columns.GridColumn();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.txtCostoUnit = new DevExpress.XtraEditors.TextEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMarca = new DevExpress.XtraEditors.TextEdit();
            this.txtPresentacion = new DevExpress.XtraEditors.TextEdit();
            this.lblUnidad = new System.Windows.Forms.Label();
            this.lblMarca = new System.Windows.Forms.Label();
            this.lblPresentacion = new System.Windows.Forms.Label();
            this.lblCostoUnidad = new System.Windows.Forms.Label();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.lblNomInsumo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.alertControlInsumos = new DevExpress.XtraBars.Alerter.AlertControl(this.components);
            this.imageCollectionUsers = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUnidad.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCostoPresentacion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCantidad.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNomInsumo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl)).BeginInit();
            this.groupControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlListaInsumos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewListaDeInsumos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCostoUnit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMarca.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPresentacion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollectionUsers)).BeginInit();
            this.SuspendLayout();
            // 
            // dxErrorProvider
            // 
            this.dxErrorProvider.ContainerControl = this;
            // 
            // txtUnidad
            // 
            this.dxErrorProvider.SetError(this.txtUnidad, "Este campo no puede ser vacio");
            this.dxErrorProvider.SetErrorType(this.txtUnidad, DevExpress.XtraEditors.DXErrorProvider.ErrorType.Default);
            this.txtUnidad.Location = new System.Drawing.Point(185, 103);
            this.txtUnidad.Name = "txtUnidad";
            this.txtUnidad.Size = new System.Drawing.Size(184, 20);
            this.txtUnidad.TabIndex = 28;
            this.txtUnidad.Tag = "Unidad";
            this.txtUnidad.Validating += new System.ComponentModel.CancelEventHandler(this.txtUnidad_Validating);
            // 
            // txtCostoPresentacion
            // 
            this.txtCostoPresentacion.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.dxErrorProvider.SetError(this.txtCostoPresentacion, "Porfavor, ingrese un valor mayor a ");
            this.dxErrorProvider.SetErrorType(this.txtCostoPresentacion, DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning);
            this.txtCostoPresentacion.Location = new System.Drawing.Point(183, 130);
            this.txtCostoPresentacion.Name = "txtCostoPresentacion";
            this.txtCostoPresentacion.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtCostoPresentacion.Size = new System.Drawing.Size(70, 20);
            this.txtCostoPresentacion.TabIndex = 30;
            this.txtCostoPresentacion.Tag = "Costo Presentación";
            this.txtCostoPresentacion.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.txtCostoPresentacion_EditValueChanging);
            this.txtCostoPresentacion.Validating += new System.ComponentModel.CancelEventHandler(this.txtCostoPresentacion_Validating);
            // 
            // txtCantidad
            // 
            this.txtCantidad.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.dxErrorProvider.SetError(this.txtCantidad, "Porfavor, ingrese un valor mayor a ");
            this.dxErrorProvider.SetErrorType(this.txtCantidad, DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning);
            this.txtCantidad.Location = new System.Drawing.Point(184, 77);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtCantidad.Size = new System.Drawing.Size(71, 20);
            this.txtCantidad.TabIndex = 27;
            this.txtCantidad.Tag = "Cantidad Presentación";
            this.txtCantidad.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.txtCantidad_EditValueChanging);
            this.txtCantidad.Validating += new System.ComponentModel.CancelEventHandler(this.txtCantidad_Validating);
            // 
            // txtNomInsumo
            // 
            this.txtNomInsumo.EditValue = "";
            this.dxErrorProvider.SetError(this.txtNomInsumo, "Este campo no puede ser vacio");
            this.dxErrorProvider.SetErrorType(this.txtNomInsumo, DevExpress.XtraEditors.DXErrorProvider.ErrorType.Default);
            this.txtNomInsumo.Location = new System.Drawing.Point(183, 52);
            this.txtNomInsumo.Name = "txtNomInsumo";
            this.txtNomInsumo.Size = new System.Drawing.Size(186, 20);
            this.txtNomInsumo.TabIndex = 22;
            this.txtNomInsumo.Tag = "Nombre del Insumo";
            this.txtNomInsumo.Validating += new System.ComponentModel.CancelEventHandler(this.txtNomInsumo_Validating);
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "ribbonPageGroup1";
            // 
            // barButtonAdd
            // 
            this.barButtonAdd.Caption = "barButtonItem1";
            this.barButtonAdd.Id = 0;
            this.barButtonAdd.Name = "barButtonAdd";
            this.barButtonAdd.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.Text = "ribbonPageGroup2";
            // 
            // gridView1
            // 
            this.gridView1.Name = "gridView1";
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ApplicationButtonText = null;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonsave,
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
            // barButtonsave
            // 
            this.barButtonsave.Caption = "Guardar";
            this.barButtonsave.Id = 0;
            this.barButtonsave.LargeGlyph = global::UIRenderers.Properties.Resources.guardar;
            this.barButtonsave.Name = "barButtonsave";
            this.barButtonsave.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.barButtonsave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonsave_ItemClick);
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
            this.barButtonClose.LargeGlyph = global::UIRenderers.Properties.Resources.delete;
            this.barButtonClose.Name = "barButtonClose";
            this.barButtonClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonClose_ItemClick);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup3});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "Opciones";
            // 
            // ribbonPageGroup3
            // 
            this.ribbonPageGroup3.ItemLinks.Add(this.barButtonsave);
            this.ribbonPageGroup3.ItemLinks.Add(this.barButtonUpdate);
            this.ribbonPageGroup3.ItemLinks.Add(this.barButtonClose);
            this.ribbonPageGroup3.Name = "ribbonPageGroup3";
            this.ribbonPageGroup3.Text = "Principal";
            // 
            // groupControl
            // 
            this.groupControl.Controls.Add(this.splitContainerControl1);
            this.groupControl.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupControl.Location = new System.Drawing.Point(664, 141);
            this.groupControl.Name = "groupControl";
            this.groupControl.Size = new System.Drawing.Size(350, 561);
            this.groupControl.TabIndex = 10;
            this.groupControl.Text = "Buscar Insumo";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(2, 22);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.splitContainerControl1.Panel1.Appearance.Options.UseBackColor = true;
            this.splitContainerControl1.Panel1.AppearanceCaption.BackColor = System.Drawing.Color.Yellow;
            this.splitContainerControl1.Panel1.AppearanceCaption.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.splitContainerControl1.Panel1.AppearanceCaption.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.splitContainerControl1.Panel1.AppearanceCaption.Options.UseBackColor = true;
            this.splitContainerControl1.Panel1.AppearanceCaption.Options.UseBorderColor = true;
            this.splitContainerControl1.Panel1.Controls.Add(this.gridControlListaInsumos);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(346, 537);
            this.splitContainerControl1.SplitterPosition = 312;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // gridControlListaInsumos
            // 
            this.gridControlListaInsumos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlListaInsumos.Location = new System.Drawing.Point(0, 0);
            this.gridControlListaInsumos.MainView = this.gridViewListaDeInsumos;
            this.gridControlListaInsumos.Name = "gridControlListaInsumos";
            this.gridControlListaInsumos.Size = new System.Drawing.Size(346, 312);
            this.gridControlListaInsumos.TabIndex = 0;
            this.gridControlListaInsumos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewListaDeInsumos});
            this.gridControlListaInsumos.DoubleClick += new System.EventHandler(this.gridViewListaDeInsumos_DoubleClick);
            // 
            // gridViewListaDeInsumos
            // 
            this.gridViewListaDeInsumos.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.gridViewListaDeInsumos.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnId,
            this.gridColumnNombre,
            this.gridColumnCantidad,
            this.gridColumnUnidad,
            this.gridColumnCostoUnidad});
            this.gridViewListaDeInsumos.GridControl = this.gridControlListaInsumos;
            this.gridViewListaDeInsumos.HorzScrollStep = 50;
            this.gridViewListaDeInsumos.Name = "gridViewListaDeInsumos";
            this.gridViewListaDeInsumos.OptionsView.ShowAutoFilterRow = true;
            this.gridViewListaDeInsumos.OptionsView.ShowGroupPanel = false;
            this.gridViewListaDeInsumos.VertScrollTipFieldName = "yes";
            // 
            // gridColumnId
            // 
            this.gridColumnId.Caption = "Id";
            this.gridColumnId.Name = "gridColumnId";
            this.gridColumnId.OptionsColumn.AllowEdit = false;
            this.gridColumnId.OptionsColumn.AllowFocus = false;
            this.gridColumnId.Visible = true;
            this.gridColumnId.VisibleIndex = 0;
            this.gridColumnId.Width = 37;
            // 
            // gridColumnNombre
            // 
            this.gridColumnNombre.Caption = "Nombre";
            this.gridColumnNombre.Name = "gridColumnNombre";
            this.gridColumnNombre.OptionsColumn.AllowEdit = false;
            this.gridColumnNombre.OptionsColumn.AllowFocus = false;
            this.gridColumnNombre.Visible = true;
            this.gridColumnNombre.VisibleIndex = 1;
            this.gridColumnNombre.Width = 73;
            // 
            // gridColumnCantidad
            // 
            this.gridColumnCantidad.Caption = "Cantidad";
            this.gridColumnCantidad.Name = "gridColumnCantidad";
            this.gridColumnCantidad.OptionsColumn.AllowEdit = false;
            this.gridColumnCantidad.OptionsColumn.AllowFocus = false;
            this.gridColumnCantidad.Visible = true;
            this.gridColumnCantidad.VisibleIndex = 2;
            this.gridColumnCantidad.Width = 39;
            // 
            // gridColumnUnidad
            // 
            this.gridColumnUnidad.Caption = "Unidad";
            this.gridColumnUnidad.Name = "gridColumnUnidad";
            this.gridColumnUnidad.OptionsColumn.AllowEdit = false;
            this.gridColumnUnidad.OptionsColumn.AllowFocus = false;
            this.gridColumnUnidad.Visible = true;
            this.gridColumnUnidad.VisibleIndex = 3;
            this.gridColumnUnidad.Width = 60;
            // 
            // gridColumnCostoUnidad
            // 
            this.gridColumnCostoUnidad.Caption = "Costo Unidad";
            this.gridColumnCostoUnidad.Name = "gridColumnCostoUnidad";
            this.gridColumnCostoUnidad.OptionsColumn.AllowEdit = false;
            this.gridColumnCostoUnidad.OptionsColumn.AllowFocus = false;
            this.gridColumnCostoUnidad.Visible = true;
            this.gridColumnCostoUnidad.VisibleIndex = 4;
            this.gridColumnCostoUnidad.Width = 65;
            // 
            // splitterControl1
            // 
            this.splitterControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitterControl1.Location = new System.Drawing.Point(658, 141);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(6, 561);
            this.splitterControl1.TabIndex = 11;
            this.splitterControl1.TabStop = false;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.txtCostoUnit);
            this.panelControl1.Controls.Add(this.label2);
            this.panelControl1.Controls.Add(this.txtMarca);
            this.panelControl1.Controls.Add(this.txtPresentacion);
            this.panelControl1.Controls.Add(this.txtUnidad);
            this.panelControl1.Controls.Add(this.lblUnidad);
            this.panelControl1.Controls.Add(this.txtCostoPresentacion);
            this.panelControl1.Controls.Add(this.txtCantidad);
            this.panelControl1.Controls.Add(this.lblMarca);
            this.panelControl1.Controls.Add(this.lblPresentacion);
            this.panelControl1.Controls.Add(this.lblCostoUnidad);
            this.panelControl1.Controls.Add(this.lblCantidad);
            this.panelControl1.Controls.Add(this.txtNomInsumo);
            this.panelControl1.Controls.Add(this.lblNomInsumo);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 141);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(658, 561);
            this.panelControl1.TabIndex = 22;
            // 
            // txtCostoUnit
            // 
            this.txtCostoUnit.EditValue = "0.00";
            this.txtCostoUnit.Enabled = false;
            this.txtCostoUnit.Location = new System.Drawing.Point(182, 160);
            this.txtCostoUnit.Name = "txtCostoUnit";
            this.txtCostoUnit.Properties.Mask.EditMask = "n2";
            this.txtCostoUnit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtCostoUnit.Size = new System.Drawing.Size(71, 20);
            this.txtCostoUnit.TabIndex = 34;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.label2.Location = new System.Drawing.Point(34, 165);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 15);
            this.label2.TabIndex = 33;
            this.label2.Text = "Costo Unidad";
            // 
            // txtMarca
            // 
            this.txtMarca.Location = new System.Drawing.Point(182, 222);
            this.txtMarca.Name = "txtMarca";
            this.txtMarca.Size = new System.Drawing.Size(168, 20);
            this.txtMarca.TabIndex = 32;
            // 
            // txtPresentacion
            // 
            this.txtPresentacion.Location = new System.Drawing.Point(182, 192);
            this.txtPresentacion.Name = "txtPresentacion";
            this.txtPresentacion.Size = new System.Drawing.Size(168, 20);
            this.txtPresentacion.TabIndex = 31;
            // 
            // lblUnidad
            // 
            this.lblUnidad.AutoSize = true;
            this.lblUnidad.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnidad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lblUnidad.Location = new System.Drawing.Point(33, 109);
            this.lblUnidad.Name = "lblUnidad";
            this.lblUnidad.Size = new System.Drawing.Size(47, 15);
            this.lblUnidad.TabIndex = 29;
            this.lblUnidad.Text = "Unidad";
            // 
            // lblMarca
            // 
            this.lblMarca.AutoSize = true;
            this.lblMarca.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMarca.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lblMarca.Location = new System.Drawing.Point(33, 221);
            this.lblMarca.Name = "lblMarca";
            this.lblMarca.Size = new System.Drawing.Size(40, 15);
            this.lblMarca.TabIndex = 26;
            this.lblMarca.Text = "Marca";
            // 
            // lblPresentacion
            // 
            this.lblPresentacion.AutoSize = true;
            this.lblPresentacion.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPresentacion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lblPresentacion.Location = new System.Drawing.Point(33, 195);
            this.lblPresentacion.Name = "lblPresentacion";
            this.lblPresentacion.Size = new System.Drawing.Size(80, 15);
            this.lblPresentacion.TabIndex = 25;
            this.lblPresentacion.Text = "Presentación";
            // 
            // lblCostoUnidad
            // 
            this.lblCostoUnidad.AutoSize = true;
            this.lblCostoUnidad.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCostoUnidad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lblCostoUnidad.Location = new System.Drawing.Point(33, 135);
            this.lblCostoUnidad.Name = "lblCostoUnidad";
            this.lblCostoUnidad.Size = new System.Drawing.Size(119, 15);
            this.lblCostoUnidad.TabIndex = 24;
            this.lblCostoUnidad.Text = "Costo  Presentación";
            // 
            // lblCantidad
            // 
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCantidad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lblCantidad.Location = new System.Drawing.Point(33, 82);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(133, 15);
            this.lblCantidad.TabIndex = 23;
            this.lblCantidad.Text = "Cantidad Presentación";
            // 
            // lblNomInsumo
            // 
            this.lblNomInsumo.AutoSize = true;
            this.lblNomInsumo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNomInsumo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lblNomInsumo.Location = new System.Drawing.Point(33, 55);
            this.lblNomInsumo.Name = "lblNomInsumo";
            this.lblNomInsumo.Size = new System.Drawing.Size(117, 15);
            this.lblNomInsumo.TabIndex = 21;
            this.lblNomInsumo.Text = "Nombre del Insumo";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(197)))), ((int)(((byte)(255)))));
            this.label1.Location = new System.Drawing.Point(29, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 19);
            this.label1.TabIndex = 20;
            this.label1.Text = "Básico";
            // 
            // imageCollectionUsers
            // 
            this.imageCollectionUsers.ImageSize = new System.Drawing.Size(32, 32);
            this.imageCollectionUsers.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollectionUsers.ImageStream")));
            this.imageCollectionUsers.Images.SetKeyName(0, "alertIcon.png");
            // 
            // NuevoInsumo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.splitterControl1);
            this.Controls.Add(this.groupControl);
            this.Controls.Add(this.ribbonControl1);
            this.Name = "NuevoInsumo";
            this.Size = new System.Drawing.Size(1014, 702);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUnidad.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCostoPresentacion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCantidad.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNomInsumo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl)).EndInit();
            this.groupControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlListaInsumos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewListaDeInsumos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCostoUnit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMarca.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPresentacion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollectionUsers)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.BarButtonItem barButtonAdd;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.TextEdit txtCostoUnit;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.TextEdit txtMarca;
        private DevExpress.XtraEditors.TextEdit txtPresentacion;
        private DevExpress.XtraEditors.TextEdit txtUnidad;
        private System.Windows.Forms.Label lblUnidad;
        private DevExpress.XtraEditors.TextEdit txtCostoPresentacion;
        private DevExpress.XtraEditors.TextEdit txtCantidad;
        private System.Windows.Forms.Label lblMarca;
        private System.Windows.Forms.Label lblPresentacion;
        private System.Windows.Forms.Label lblCostoUnidad;
        private System.Windows.Forms.Label lblCantidad;
        private DevExpress.XtraEditors.TextEdit txtNomInsumo;
        private System.Windows.Forms.Label lblNomInsumo;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private DevExpress.XtraEditors.GroupControl groupControl;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraGrid.GridControl gridControlListaInsumos;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewListaDeInsumos;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnId;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNombre;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnCantidad;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnUnidad;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnCostoUnidad;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup3;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraBars.BarButtonItem barButtonsave;
        private DevExpress.XtraBars.BarButtonItem barButtonUpdate;
        private DevExpress.XtraBars.BarButtonItem barButtonClose;
        private DevExpress.XtraBars.Alerter.AlertControl alertControlInsumos;
        private DevExpress.Utils.ImageCollection imageCollectionUsers;

    }
}
