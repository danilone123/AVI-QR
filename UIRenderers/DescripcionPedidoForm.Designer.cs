namespace UIRenderers
{
    partial class DescripcionPedidoForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DescripcionPedidoForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblValueTipoPedido = new DevExpress.XtraEditors.LabelControl();
            this.lblValueNumeroPedido = new DevExpress.XtraEditors.LabelControl();
            this.lblNombreValue = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lblNumeroPedido = new DevExpress.XtraEditors.LabelControl();
            this.lblNombre = new DevExpress.XtraEditors.LabelControl();
            this.gridControlDescripcion = new DevExpress.XtraGrid.GridControl();
            this.gridViewDescripcionPedido = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.columnArticulo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.columnCantidad = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCantidad = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.columnPrecio = new DevExpress.XtraGrid.Columns.GridColumn();
            this.columnTotal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnDireccion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDescripcion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDescripcionPedido)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCantidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblValueTipoPedido);
            this.panel1.Controls.Add(this.lblValueNumeroPedido);
            this.panel1.Controls.Add(this.lblNombreValue);
            this.panel1.Controls.Add(this.labelControl1);
            this.panel1.Controls.Add(this.lblNumeroPedido);
            this.panel1.Controls.Add(this.lblNombre);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(570, 88);
            this.panel1.TabIndex = 0;
            // 
            // lblValueTipoPedido
            // 
            this.lblValueTipoPedido.Location = new System.Drawing.Point(420, 51);
            this.lblValueTipoPedido.Name = "lblValueTipoPedido";
            this.lblValueTipoPedido.Size = new System.Drawing.Size(63, 13);
            this.lblValueTipoPedido.TabIndex = 5;
            this.lblValueTipoPedido.Text = "labelControl2";
            // 
            // lblValueNumeroPedido
            // 
            this.lblValueNumeroPedido.Location = new System.Drawing.Point(150, 51);
            this.lblValueNumeroPedido.Name = "lblValueNumeroPedido";
            this.lblValueNumeroPedido.Size = new System.Drawing.Size(63, 13);
            this.lblValueNumeroPedido.TabIndex = 4;
            this.lblValueNumeroPedido.Text = "labelControl2";
            // 
            // lblNombreValue
            // 
            this.lblNombreValue.Location = new System.Drawing.Point(83, 28);
            this.lblNombreValue.Name = "lblNombreValue";
            this.lblNombreValue.Size = new System.Drawing.Size(63, 13);
            this.lblNombreValue.TabIndex = 3;
            this.lblNombreValue.Text = "labelControl2";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(315, 50);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(98, 16);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Tipo de Pedido:";
            // 
            // lblNumeroPedido
            // 
            this.lblNumeroPedido.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblNumeroPedido.Appearance.Options.UseFont = true;
            this.lblNumeroPedido.Location = new System.Drawing.Point(22, 50);
            this.lblNumeroPedido.Name = "lblNumeroPedido";
            this.lblNumeroPedido.Size = new System.Drawing.Size(121, 16);
            this.lblNumeroPedido.TabIndex = 1;
            this.lblNumeroPedido.Text = "Numero de Pedido:";
            // 
            // lblNombre
            // 
            this.lblNombre.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblNombre.Appearance.Options.UseFont = true;
            this.lblNombre.Location = new System.Drawing.Point(22, 28);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(54, 16);
            this.lblNombre.TabIndex = 0;
            this.lblNombre.Text = "Nombre:";
            // 
            // gridControlDescripcion
            // 
            this.gridControlDescripcion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlDescripcion.Location = new System.Drawing.Point(0, 88);
            this.gridControlDescripcion.MainView = this.gridViewDescripcionPedido;
            this.gridControlDescripcion.Name = "gridControlDescripcion";
            this.gridControlDescripcion.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCantidad,
            this.repositoryItemTextEdit1});
            this.gridControlDescripcion.Size = new System.Drawing.Size(570, 260);
            this.gridControlDescripcion.TabIndex = 55;
            this.gridControlDescripcion.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewDescripcionPedido});
            // 
            // gridViewDescripcionPedido
            // 
            this.gridViewDescripcionPedido.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.columnArticulo,
            this.columnCantidad,
            this.columnPrecio,
            this.columnTotal,
            this.gridColumnDireccion});
            this.gridViewDescripcionPedido.GridControl = this.gridControlDescripcion;
            this.gridViewDescripcionPedido.Name = "gridViewDescripcionPedido";
            this.gridViewDescripcionPedido.OptionsCustomization.AllowGroup = false;
            this.gridViewDescripcionPedido.OptionsView.ShowFooter = true;
            this.gridViewDescripcionPedido.OptionsView.ShowGroupPanel = false;
            // 
            // columnArticulo
            // 
            this.columnArticulo.Caption = "Artículo";
            this.columnArticulo.FieldName = "Articulo";
            this.columnArticulo.Name = "columnArticulo";
            this.columnArticulo.OptionsColumn.AllowEdit = false;
            this.columnArticulo.Visible = true;
            this.columnArticulo.VisibleIndex = 0;
            // 
            // columnCantidad
            // 
            this.columnCantidad.Caption = "Cantidad";
            this.columnCantidad.ColumnEdit = this.repositoryItemCantidad;
            this.columnCantidad.FieldName = "Cantidad";
            this.columnCantidad.Name = "columnCantidad";
            this.columnCantidad.OptionsColumn.AllowEdit = false;
            this.columnCantidad.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            this.columnCantidad.Visible = true;
            this.columnCantidad.VisibleIndex = 2;
            // 
            // repositoryItemCantidad
            // 
            this.repositoryItemCantidad.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.repositoryItemCantidad.AutoHeight = false;
            this.repositoryItemCantidad.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemCantidad.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemCantidad.Mask.BeepOnError = true;
            this.repositoryItemCantidad.Mask.EditMask = "([1-9][0-9]*)";
            this.repositoryItemCantidad.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.repositoryItemCantidad.Mask.UseMaskAsDisplayFormat = true;
            this.repositoryItemCantidad.Name = "repositoryItemCantidad";
            // 
            // columnPrecio
            // 
            this.columnPrecio.Caption = "Precio Unitario";
            this.columnPrecio.FieldName = "Precio";
            this.columnPrecio.Name = "columnPrecio";
            this.columnPrecio.OptionsColumn.AllowEdit = false;
            this.columnPrecio.Visible = true;
            this.columnPrecio.VisibleIndex = 3;
            // 
            // columnTotal
            // 
            this.columnTotal.Caption = "Subtotal";
            this.columnTotal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.columnTotal.FieldName = "Subtotal";
            this.columnTotal.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.columnTotal.Name = "columnTotal";
            this.columnTotal.OptionsColumn.AllowEdit = false;
            this.columnTotal.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.columnTotal.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.columnTotal.Visible = true;
            this.columnTotal.VisibleIndex = 4;
            // 
            // gridColumnDireccion
            // 
            this.gridColumnDireccion.Caption = "Descripcion";
            this.gridColumnDireccion.FieldName = "Descripcion";
            this.gridColumnDireccion.Name = "gridColumnDireccion";
            this.gridColumnDireccion.OptionsColumn.AllowEdit = false;
            this.gridColumnDireccion.Visible = true;
            this.gridColumnDireccion.VisibleIndex = 1;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // DescripcionPedidoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 348);
            this.Controls.Add(this.gridControlDescripcion);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DescripcionPedidoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Descripción del Pedido";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDescripcion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDescripcionPedido)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCantidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.LabelControl lblValueNumeroPedido;
        private DevExpress.XtraEditors.LabelControl lblNombreValue;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl lblNumeroPedido;
        private DevExpress.XtraEditors.LabelControl lblNombre;
        private DevExpress.XtraEditors.LabelControl lblValueTipoPedido;
        private DevExpress.XtraGrid.GridControl gridControlDescripcion;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewDescripcionPedido;
        private DevExpress.XtraGrid.Columns.GridColumn columnArticulo;
        private DevExpress.XtraGrid.Columns.GridColumn columnCantidad;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemCantidad;
        private DevExpress.XtraGrid.Columns.GridColumn columnPrecio;
        private DevExpress.XtraGrid.Columns.GridColumn columnTotal;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDireccion;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
    }
}