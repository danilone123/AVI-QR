namespace UIRenderers
{
    partial class PnlVentaProducto
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose( );
            }
            base.Dispose( disposing );
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent( )
        {
            this.panelControlVentas = new DevExpress.XtraEditors.PanelControl();
            this.grpBoxProductos = new System.Windows.Forms.GroupBox();
            this.gridVentas = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColArticulo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemGridLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.repositoryItemGridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColCantidad = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColPrecio = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColdescuento = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColSubtotal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.repositoryItemLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.grpBoxCliente = new System.Windows.Forms.GroupBox();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.chkBxEntregado = new DevExpress.XtraEditors.CheckEdit();
            this.lblNumPedido = new System.Windows.Forms.Label();
            this.txtNumPedido = new DevExpress.XtraEditors.TextEdit();
            this.lblTipo = new System.Windows.Forms.Label();
            this.lblFechaPedido = new System.Windows.Forms.Label();
            this.dateEdit = new DevExpress.XtraEditors.DateEdit();
            this.lblEstadoPedido = new System.Windows.Forms.Label();
            this.txtEntregado = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtEstadoPedido = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtNomCliente = new DevExpress.XtraEditors.TextEdit();
            this.lblTelefono = new System.Windows.Forms.Label();
            this.lblDireccion = new System.Windows.Forms.Label();
            this.lblNIT = new System.Windows.Forms.Label();
            this.lblNombreCliente = new System.Windows.Forms.Label();
            this.txtTelefono = new DevExpress.XtraEditors.TextEdit();
            this.txtNIT = new DevExpress.XtraEditors.TextEdit();
            this.txtDireccion = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlVentas)).BeginInit();
            this.panelControlVentas.SuspendLayout();
            this.grpBoxProductos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridVentas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).BeginInit();
            this.grpBoxCliente.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkBxEntregado.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumPedido.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEntregado.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEstadoPedido.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNomCliente.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTelefono.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNIT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDireccion.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControlVentas
            // 
            this.panelControlVentas.Appearance.BackColor = System.Drawing.Color.White;
            this.panelControlVentas.Appearance.Options.UseBackColor = true;
            this.panelControlVentas.Controls.Add(this.grpBoxProductos);
            this.panelControlVentas.Controls.Add(this.grpBoxCliente);
            this.panelControlVentas.Controls.Add(this.ribbonControl1);
            this.panelControlVentas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControlVentas.Location = new System.Drawing.Point(0, 0);
            this.panelControlVentas.MinimumSize = new System.Drawing.Size(1030, 0);
            this.panelControlVentas.Name = "panelControlVentas";
            this.panelControlVentas.Size = new System.Drawing.Size(1030, 750);
            this.panelControlVentas.TabIndex = 0;
            // 
            // grpBoxProductos
            // 
            this.grpBoxProductos.Controls.Add(this.gridVentas);
            this.grpBoxProductos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpBoxProductos.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBoxProductos.ForeColor = System.Drawing.Color.Black;
            this.grpBoxProductos.Location = new System.Drawing.Point(2, 321);
            this.grpBoxProductos.Name = "grpBoxProductos";
            this.grpBoxProductos.Size = new System.Drawing.Size(1026, 427);
            this.grpBoxProductos.TabIndex = 2;
            this.grpBoxProductos.TabStop = false;
            this.grpBoxProductos.Text = "Artículos";
            // 
            // gridVentas
            // 
            this.gridVentas.Dock = System.Windows.Forms.DockStyle.Top;
            this.gridVentas.Location = new System.Drawing.Point(3, 18);
            this.gridVentas.MainView = this.gridView1;
            this.gridVentas.MenuManager = this.ribbonControl1;
            this.gridVentas.Name = "gridVentas";
            this.gridVentas.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemGridLookUpEdit1,
            this.repositoryItemLookUpEdit1});
            this.gridVentas.Size = new System.Drawing.Size(1020, 316);
            this.gridVentas.TabIndex = 0;
            this.gridVentas.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColArticulo,
            this.gridColCantidad,
            this.gridColPrecio,
            this.gridColdescuento,
            this.gridColSubtotal});
            this.gridView1.GridControl = this.gridVentas;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.gridView1.OptionsView.ShowFooter = true;
            // 
            // gridColArticulo
            // 
            this.gridColArticulo.Caption = "Artículo";
            this.gridColArticulo.ColumnEdit = this.repositoryItemGridLookUpEdit1;
            this.gridColArticulo.FieldName = "Articulo";
            this.gridColArticulo.Name = "gridColArticulo";
            this.gridColArticulo.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.gridColArticulo.Visible = true;
            this.gridColArticulo.VisibleIndex = 0;
            this.gridColArticulo.Width = 197;
            // 
            // repositoryItemGridLookUpEdit1
            // 
            this.repositoryItemGridLookUpEdit1.AutoHeight = false;
            this.repositoryItemGridLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemGridLookUpEdit1.Name = "repositoryItemGridLookUpEdit1";
            this.repositoryItemGridLookUpEdit1.NullText = "";
            this.repositoryItemGridLookUpEdit1.View = this.repositoryItemGridLookUpEdit1View;
            // 
            // repositoryItemGridLookUpEdit1View
            // 
            this.repositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemGridLookUpEdit1View.Name = "repositoryItemGridLookUpEdit1View";
            this.repositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.repositoryItemGridLookUpEdit1View.RowHeight = 50;
            // 
            // gridColCantidad
            // 
            this.gridColCantidad.Caption = "Cantidad";
            this.gridColCantidad.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColCantidad.FieldName = "Cantidad";
            this.gridColCantidad.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColCantidad.Name = "gridColCantidad";
            this.gridColCantidad.Visible = true;
            this.gridColCantidad.VisibleIndex = 1;
            this.gridColCantidad.Width = 198;
            // 
            // gridColPrecio
            // 
            this.gridColPrecio.Caption = "Precio unitario";
            this.gridColPrecio.FieldName = "Preciounitario";
            this.gridColPrecio.Name = "gridColPrecio";
            this.gridColPrecio.OptionsColumn.AllowEdit = false;
            this.gridColPrecio.Visible = true;
            this.gridColPrecio.VisibleIndex = 2;
            this.gridColPrecio.Width = 198;
            // 
            // gridColdescuento
            // 
            this.gridColdescuento.Caption = "Descuento";
            this.gridColdescuento.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColdescuento.FieldName = "Descuento";
            this.gridColdescuento.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColdescuento.Name = "gridColdescuento";
            this.gridColdescuento.Visible = true;
            this.gridColdescuento.VisibleIndex = 3;
            this.gridColdescuento.Width = 198;
            // 
            // gridColSubtotal
            // 
            this.gridColSubtotal.Caption = "Subtotal";
            this.gridColSubtotal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColSubtotal.FieldName = "Subtotal";
            this.gridColSubtotal.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColSubtotal.Name = "gridColSubtotal";
            this.gridColSubtotal.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridColSubtotal.Visible = true;
            this.gridColSubtotal.VisibleIndex = 4;
            this.gridColSubtotal.Width = 208;
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ApplicationButtonText = null;
            this.ribbonControl1.Location = new System.Drawing.Point(2, 2);
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbonControl1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2010;
            this.ribbonControl1.SelectedPage = this.ribbonPage1;
            this.ribbonControl1.Size = new System.Drawing.Size(1026, 141);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "ribbonPage1";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "ribbonPageGroup1";
            // 
            // repositoryItemLookUpEdit1
            // 
            this.repositoryItemLookUpEdit1.AutoHeight = false;
            this.repositoryItemLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit1.Name = "repositoryItemLookUpEdit1";
            this.repositoryItemLookUpEdit1.NullText = "";
            // 
            // grpBoxCliente
            // 
            this.grpBoxCliente.Controls.Add(this.panelControl1);
            this.grpBoxCliente.Controls.Add(this.txtNomCliente);
            this.grpBoxCliente.Controls.Add(this.lblTelefono);
            this.grpBoxCliente.Controls.Add(this.lblDireccion);
            this.grpBoxCliente.Controls.Add(this.lblNIT);
            this.grpBoxCliente.Controls.Add(this.lblNombreCliente);
            this.grpBoxCliente.Controls.Add(this.txtTelefono);
            this.grpBoxCliente.Controls.Add(this.txtNIT);
            this.grpBoxCliente.Controls.Add(this.txtDireccion);
            this.grpBoxCliente.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpBoxCliente.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBoxCliente.ForeColor = System.Drawing.Color.Black;
            this.grpBoxCliente.Location = new System.Drawing.Point(2, 143);
            this.grpBoxCliente.Name = "grpBoxCliente";
            this.grpBoxCliente.Size = new System.Drawing.Size(1026, 178);
            this.grpBoxCliente.TabIndex = 1;
            this.grpBoxCliente.TabStop = false;
            this.grpBoxCliente.Text = "Datos del cliente";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.chkBxEntregado);
            this.panelControl1.Controls.Add(this.lblNumPedido);
            this.panelControl1.Controls.Add(this.txtNumPedido);
            this.panelControl1.Controls.Add(this.lblTipo);
            this.panelControl1.Controls.Add(this.lblFechaPedido);
            this.panelControl1.Controls.Add(this.dateEdit);
            this.panelControl1.Controls.Add(this.lblEstadoPedido);
            this.panelControl1.Controls.Add(this.txtEntregado);
            this.panelControl1.Controls.Add(this.txtEstadoPedido);
            this.panelControl1.Location = new System.Drawing.Point(848, 21);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(172, 134);
            this.panelControl1.TabIndex = 42;
            // 
            // chkBxEntregado
            // 
            this.chkBxEntregado.EditValue = true;
            this.chkBxEntregado.Location = new System.Drawing.Point(5, 114);
            this.chkBxEntregado.MenuManager = this.ribbonControl1;
            this.chkBxEntregado.Name = "chkBxEntregado";
            this.chkBxEntregado.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBxEntregado.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.chkBxEntregado.Properties.Appearance.Options.UseFont = true;
            this.chkBxEntregado.Properties.Appearance.Options.UseForeColor = true;
            this.chkBxEntregado.Properties.Caption = "Entregado";
            this.chkBxEntregado.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.chkBxEntregado.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkBxEntregado.Size = new System.Drawing.Size(86, 20);
            this.chkBxEntregado.TabIndex = 43;
            // 
            // lblNumPedido
            // 
            this.lblNumPedido.AutoSize = true;
            this.lblNumPedido.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumPedido.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lblNumPedido.Location = new System.Drawing.Point(5, 6);
            this.lblNumPedido.Name = "lblNumPedido";
            this.lblNumPedido.Size = new System.Drawing.Size(62, 15);
            this.lblNumPedido.TabIndex = 33;
            this.lblNumPedido.Text = "Pedido N°";
            // 
            // txtNumPedido
            // 
            this.txtNumPedido.EnterMoveNextControl = true;
            this.txtNumPedido.Location = new System.Drawing.Point(73, 3);
            this.txtNumPedido.Name = "txtNumPedido";
            this.txtNumPedido.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumPedido.Properties.Appearance.Options.UseFont = true;
            this.txtNumPedido.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.txtNumPedido.Size = new System.Drawing.Size(94, 21);
            this.txtNumPedido.TabIndex = 34;
            this.txtNumPedido.Tag = "Nombre del producto";
            // 
            // lblTipo
            // 
            this.lblTipo.AutoSize = true;
            this.lblTipo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lblTipo.Location = new System.Drawing.Point(5, 58);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.Size = new System.Drawing.Size(31, 15);
            this.lblTipo.TabIndex = 40;
            this.lblTipo.Text = "Tipo";
            // 
            // lblFechaPedido
            // 
            this.lblFechaPedido.AutoSize = true;
            this.lblFechaPedido.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaPedido.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lblFechaPedido.Location = new System.Drawing.Point(5, 31);
            this.lblFechaPedido.Name = "lblFechaPedido";
            this.lblFechaPedido.Size = new System.Drawing.Size(41, 15);
            this.lblFechaPedido.TabIndex = 35;
            this.lblFechaPedido.Text = "Fecha";
            // 
            // dateEdit
            // 
            this.dateEdit.EditValue = new System.DateTime(((long)(0)));
            this.dateEdit.Location = new System.Drawing.Point(73, 31);
            this.dateEdit.MenuManager = this.ribbonControl1;
            this.dateEdit.Name = "dateEdit";
            this.dateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEdit.Size = new System.Drawing.Size(94, 20);
            this.dateEdit.TabIndex = 39;
            // 
            // lblEstadoPedido
            // 
            this.lblEstadoPedido.AutoSize = true;
            this.lblEstadoPedido.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstadoPedido.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lblEstadoPedido.Location = new System.Drawing.Point(5, 89);
            this.lblEstadoPedido.Name = "lblEstadoPedido";
            this.lblEstadoPedido.Size = new System.Drawing.Size(46, 15);
            this.lblEstadoPedido.TabIndex = 37;
            this.lblEstadoPedido.Text = "Estado";
            // 
            // txtEntregado
            // 
            this.txtEntregado.EditValue = "Inmediato";
            this.txtEntregado.Location = new System.Drawing.Point(73, 57);
            this.txtEntregado.Name = "txtEntregado";
            this.txtEntregado.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEntregado.Properties.Appearance.Options.UseFont = true;
            this.txtEntregado.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.txtEntregado.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtEntregado.Properties.Items.AddRange(new object[] {
            "Inmediato",
            "Futuro"});
            this.txtEntregado.Size = new System.Drawing.Size(94, 21);
            this.txtEntregado.TabIndex = 41;
            this.txtEntregado.TabStop = false;
            this.txtEntregado.Tag = "Nombre del producto";
            // 
            // txtEstadoPedido
            // 
            this.txtEstadoPedido.EditValue = "Pagado";
            this.txtEstadoPedido.Location = new System.Drawing.Point(73, 88);
            this.txtEstadoPedido.Name = "txtEstadoPedido";
            this.txtEstadoPedido.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEstadoPedido.Properties.Appearance.Options.UseFont = true;
            this.txtEstadoPedido.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.txtEstadoPedido.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtEstadoPedido.Properties.Items.AddRange(new object[] {
            "Pagado",
            "Sin pagar"});
            this.txtEstadoPedido.Size = new System.Drawing.Size(94, 21);
            this.txtEstadoPedido.TabIndex = 38;
            this.txtEstadoPedido.TabStop = false;
            this.txtEstadoPedido.Tag = "Nombre del producto";
            // 
            // txtNomCliente
            // 
            this.txtNomCliente.EnterMoveNextControl = true;
            this.txtNomCliente.Location = new System.Drawing.Point(88, 21);
            this.txtNomCliente.Name = "txtNomCliente";
            this.txtNomCliente.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNomCliente.Properties.Appearance.Options.UseFont = true;
            this.txtNomCliente.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.txtNomCliente.Size = new System.Drawing.Size(186, 21);
            this.txtNomCliente.TabIndex = 28;
            this.txtNomCliente.Tag = "Nombre del producto";
            // 
            // lblTelefono
            // 
            this.lblTelefono.AutoSize = true;
            this.lblTelefono.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTelefono.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lblTelefono.Location = new System.Drawing.Point(19, 78);
            this.lblTelefono.Name = "lblTelefono";
            this.lblTelefono.Size = new System.Drawing.Size(54, 15);
            this.lblTelefono.TabIndex = 27;
            this.lblTelefono.Text = "Teléfono";
            // 
            // lblDireccion
            // 
            this.lblDireccion.AutoSize = true;
            this.lblDireccion.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDireccion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lblDireccion.Location = new System.Drawing.Point(19, 110);
            this.lblDireccion.Name = "lblDireccion";
            this.lblDireccion.Size = new System.Drawing.Size(59, 15);
            this.lblDireccion.TabIndex = 25;
            this.lblDireccion.Text = "Dirección";
            // 
            // lblNIT
            // 
            this.lblNIT.AutoSize = true;
            this.lblNIT.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNIT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lblNIT.Location = new System.Drawing.Point(19, 51);
            this.lblNIT.Name = "lblNIT";
            this.lblNIT.Size = new System.Drawing.Size(26, 15);
            this.lblNIT.TabIndex = 24;
            this.lblNIT.Text = "NIT";
            // 
            // lblNombreCliente
            // 
            this.lblNombreCliente.AutoSize = true;
            this.lblNombreCliente.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombreCliente.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lblNombreCliente.Location = new System.Drawing.Point(19, 24);
            this.lblNombreCliente.Name = "lblNombreCliente";
            this.lblNombreCliente.Size = new System.Drawing.Size(46, 15);
            this.lblNombreCliente.TabIndex = 23;
            this.lblNombreCliente.Text = "Cliente";
            // 
            // txtTelefono
            // 
            this.txtTelefono.Location = new System.Drawing.Point(88, 75);
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTelefono.Properties.Appearance.Options.UseFont = true;
            this.txtTelefono.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.txtTelefono.Size = new System.Drawing.Size(186, 21);
            this.txtTelefono.TabIndex = 30;
            this.txtTelefono.TabStop = false;
            this.txtTelefono.Tag = "Nombre del producto";
            // 
            // txtNIT
            // 
            this.txtNIT.Location = new System.Drawing.Point(88, 48);
            this.txtNIT.Name = "txtNIT";
            this.txtNIT.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNIT.Properties.Appearance.Options.UseFont = true;
            this.txtNIT.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.txtNIT.Size = new System.Drawing.Size(186, 21);
            this.txtNIT.TabIndex = 29;
            this.txtNIT.TabStop = false;
            this.txtNIT.Tag = "Nombre del producto";
            // 
            // txtDireccion
            // 
            this.txtDireccion.Location = new System.Drawing.Point(88, 102);
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDireccion.Properties.Appearance.Options.UseFont = true;
            this.txtDireccion.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.txtDireccion.Size = new System.Drawing.Size(186, 69);
            this.txtDireccion.TabIndex = 32;
            this.txtDireccion.TabStop = false;
            this.txtDireccion.Tag = "Nombre del producto";
            // 
            // PnlVentaProducto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panelControlVentas);
            this.Name = "PnlVentaProducto";
            this.Size = new System.Drawing.Size(1030, 750);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlVentas)).EndInit();
            this.panelControlVentas.ResumeLayout(false);
            this.grpBoxProductos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridVentas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).EndInit();
            this.grpBoxCliente.ResumeLayout(false);
            this.grpBoxCliente.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkBxEntregado.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumPedido.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEntregado.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEstadoPedido.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNomCliente.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTelefono.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNIT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDireccion.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControlVentas;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private System.Windows.Forms.GroupBox grpBoxProductos;
        private System.Windows.Forms.GroupBox grpBoxCliente;
        private System.Windows.Forms.Label lblTelefono;
        private System.Windows.Forms.Label lblDireccion;
        private System.Windows.Forms.Label lblNIT;
        private System.Windows.Forms.Label lblNombreCliente;
        private DevExpress.XtraEditors.TextEdit txtNomCliente;
        private DevExpress.XtraEditors.TextEdit txtTelefono;
        private DevExpress.XtraEditors.TextEdit txtNIT;
        private DevExpress.XtraEditors.MemoEdit txtDireccion;
        private DevExpress.XtraEditors.DateEdit dateEdit;
        private System.Windows.Forms.Label lblEstadoPedido;
        private System.Windows.Forms.Label lblFechaPedido;
        private DevExpress.XtraEditors.TextEdit txtNumPedido;
        private System.Windows.Forms.Label lblNumPedido;
        private System.Windows.Forms.Label lblTipo;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit txtEntregado;
        private DevExpress.XtraEditors.ComboBoxEdit txtEstadoPedido;
        private DevExpress.XtraEditors.CheckEdit chkBxEntregado;
        private DevExpress.XtraGrid.GridControl gridVentas;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColArticulo;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit repositoryItemGridLookUpEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemGridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColCantidad;
        private DevExpress.XtraGrid.Columns.GridColumn gridColPrecio;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColdescuento;
        private DevExpress.XtraGrid.Columns.GridColumn gridColSubtotal;
    }
}
