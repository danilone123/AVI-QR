namespace UIRenderers
{
    partial class CalculadoraVentas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CalculadoraVentas));
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip3 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem3 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem3 = new DevExpress.Utils.ToolTipItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.radioBtnSinDescuento = new System.Windows.Forms.RadioButton();
            this.txtEfectivo = new DevExpress.XtraEditors.TextEdit();
            this.txtPorcentaje = new DevExpress.XtraEditors.TextEdit();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtEfectivoSinDescuento = new DevExpress.XtraEditors.TextEdit();
            this.txtCuenta = new DevExpress.XtraEditors.TextEdit();
            this.txtTotal = new DevExpress.XtraEditors.TextEdit();
            this.txtCambio = new DevExpress.XtraEditors.TextEdit();
            this.btnAceptar = new DevExpress.XtraEditors.SimpleButton();
            this.radioBtnPorcentaje = new System.Windows.Forms.RadioButton();
            this.radioBtnEfectivo = new System.Windows.Forms.RadioButton();
            this.alertControlCalculadoraVentas = new DevExpress.XtraBars.Alerter.AlertControl(this.components);
            this.imageCollectionCalculadora = new DevExpress.Utils.ImageCollection(this.components);
            this.barItem = new DevExpress.XtraBars.BarStaticItem();
            this.ribbonPage = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.barButtonAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonRefrescar = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonCerrar = new DevExpress.XtraBars.BarButtonItem();
            this.printDocument = new System.Drawing.Printing.PrintDocument();
            ((System.ComponentModel.ISupportInitialize)(this.txtEfectivo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPorcentaje.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEfectivoSinDescuento.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCuenta.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCambio.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollectionCalculadora)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 14F);
            this.label1.Location = new System.Drawing.Point(102, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(196, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Calculadora de Ventas";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label2.Location = new System.Drawing.Point(44, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Efectivo:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label3.Location = new System.Drawing.Point(44, 144);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Cuenta:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label4.Location = new System.Drawing.Point(44, 192);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "Descuento:";
            // 
            // radioBtnSinDescuento
            // 
            this.radioBtnSinDescuento.AutoSize = true;
            this.radioBtnSinDescuento.Checked = true;
            this.radioBtnSinDescuento.ForeColor = System.Drawing.SystemColors.ControlText;
            this.radioBtnSinDescuento.Location = new System.Drawing.Point(84, 228);
            this.radioBtnSinDescuento.Name = "radioBtnSinDescuento";
            this.radioBtnSinDescuento.Size = new System.Drawing.Size(93, 17);
            this.radioBtnSinDescuento.TabIndex = 5;
            this.radioBtnSinDescuento.TabStop = true;
            this.radioBtnSinDescuento.Text = "Sin Descuento";
            this.radioBtnSinDescuento.UseVisualStyleBackColor = true;
            this.radioBtnSinDescuento.Click += new System.EventHandler(this.radioBtnSinDescuento_Click);
            // 
            // txtEfectivo
            // 
            this.txtEfectivo.Enabled = false;
            this.txtEfectivo.Location = new System.Drawing.Point(182, 260);
            this.txtEfectivo.Name = "txtEfectivo";
            this.txtEfectivo.Properties.Mask.EditMask = "f";
            this.txtEfectivo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtEfectivo.Size = new System.Drawing.Size(100, 20);
            this.txtEfectivo.TabIndex = 6;
            this.txtEfectivo.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.txtEfectivo_EditValueChanging);
            // 
            // txtPorcentaje
            // 
            this.txtPorcentaje.Enabled = false;
            this.txtPorcentaje.Location = new System.Drawing.Point(182, 298);
            this.txtPorcentaje.Name = "txtPorcentaje";
            this.txtPorcentaje.Properties.Mask.EditMask = "f";
            this.txtPorcentaje.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtPorcentaje.Size = new System.Drawing.Size(100, 20);
            this.txtPorcentaje.TabIndex = 7;
            this.txtPorcentaje.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.txtPorcentaje_EditValueChanging);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label5.Location = new System.Drawing.Point(44, 346);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "Total:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label6.Location = new System.Drawing.Point(44, 397);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 17);
            this.label6.TabIndex = 9;
            this.label6.Text = "Cambio:";
            // 
            // txtEfectivoSinDescuento
            // 
            this.txtEfectivoSinDescuento.Location = new System.Drawing.Point(182, 93);
            this.txtEfectivoSinDescuento.Name = "txtEfectivoSinDescuento";
            this.txtEfectivoSinDescuento.Properties.Mask.EditMask = "f";
            this.txtEfectivoSinDescuento.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtEfectivoSinDescuento.Size = new System.Drawing.Size(100, 20);
            this.txtEfectivoSinDescuento.TabIndex = 10;
            this.txtEfectivoSinDescuento.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.txtEfectivoSinDescuento_EditValueChanging);
            // 
            // txtCuenta
            // 
            this.txtCuenta.Enabled = false;
            this.txtCuenta.Location = new System.Drawing.Point(182, 144);
            this.txtCuenta.Name = "txtCuenta";
            this.txtCuenta.Size = new System.Drawing.Size(100, 20);
            this.txtCuenta.TabIndex = 11;
            // 
            // txtTotal
            // 
            this.txtTotal.Enabled = false;
            this.txtTotal.Location = new System.Drawing.Point(182, 346);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(100, 20);
            this.txtTotal.TabIndex = 12;
            // 
            // txtCambio
            // 
            this.txtCambio.Enabled = false;
            this.txtCambio.Location = new System.Drawing.Point(182, 397);
            this.txtCambio.Name = "txtCambio";
            this.txtCambio.Size = new System.Drawing.Size(100, 20);
            this.txtCambio.TabIndex = 13;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(146, 452);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(97, 26);
            this.btnAceptar.TabIndex = 14;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // radioBtnPorcentaje
            // 
            this.radioBtnPorcentaje.AutoSize = true;
            this.radioBtnPorcentaje.ForeColor = System.Drawing.SystemColors.ControlText;
            this.radioBtnPorcentaje.Location = new System.Drawing.Point(84, 299);
            this.radioBtnPorcentaje.Name = "radioBtnPorcentaje";
            this.radioBtnPorcentaje.Size = new System.Drawing.Size(36, 17);
            this.radioBtnPorcentaje.TabIndex = 15;
            this.radioBtnPorcentaje.Text = "%";
            this.radioBtnPorcentaje.UseVisualStyleBackColor = true;
            this.radioBtnPorcentaje.Click += new System.EventHandler(this.radioBtnPorcentaje_Click);
            // 
            // radioBtnEfectivo
            // 
            this.radioBtnEfectivo.AutoSize = true;
            this.radioBtnEfectivo.Location = new System.Drawing.Point(84, 263);
            this.radioBtnEfectivo.Name = "radioBtnEfectivo";
            this.radioBtnEfectivo.Size = new System.Drawing.Size(64, 17);
            this.radioBtnEfectivo.TabIndex = 16;
            this.radioBtnEfectivo.Text = "Efectivo";
            this.radioBtnEfectivo.UseVisualStyleBackColor = true;
            this.radioBtnEfectivo.Click += new System.EventHandler(this.radioBtnEfectivo_Click);
            // 
            // imageCollectionCalculadora
            // 
            this.imageCollectionCalculadora.ImageSize = new System.Drawing.Size(32, 32);
            this.imageCollectionCalculadora.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollectionCalculadora.ImageStream")));
            this.imageCollectionCalculadora.Images.SetKeyName(0, "alertIcon.png");
            // 
            // barItem
            // 
            this.barItem.Id = 8;
            this.barItem.Name = "barItem";
            this.barItem.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // ribbonPage
            // 
            this.ribbonPage.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.ribbonPage.Name = "ribbonPage";
            this.ribbonPage.Text = "Opciones";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonAdd);
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonRefrescar);
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonCerrar);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "Principal";
            // 
            // barButtonAdd
            // 
            this.barButtonAdd.Caption = "Guardar";
            this.barButtonAdd.CloseSubMenuOnClick = false;
            this.barButtonAdd.Glyph = global::UIRenderers.Properties.Resources.guardar;
            this.barButtonAdd.Id = 5;
            this.barButtonAdd.Name = "barButtonAdd";
            this.barButtonAdd.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            toolTipTitleItem1.Text = "Nuevo";
            toolTipItem1.LeftIndent = 6;
            toolTipItem1.Text = "Agrega un nuevo producto";
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.barButtonAdd.SuperTip = superToolTip1;
            // 
            // barButtonRefrescar
            // 
            this.barButtonRefrescar.Caption = "Actualizar";
            this.barButtonRefrescar.CloseSubMenuOnClick = false;
            this.barButtonRefrescar.Id = 6;
            this.barButtonRefrescar.LargeGlyph = global::UIRenderers.Properties.Resources.refresh;
            this.barButtonRefrescar.Name = "barButtonRefrescar";
            this.barButtonRefrescar.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            toolTipTitleItem2.Text = "Actualizar";
            toolTipItem2.LeftIndent = 6;
            toolTipItem2.Text = "Refresca la vista de productos";
            superToolTip2.Items.Add(toolTipTitleItem2);
            superToolTip2.Items.Add(toolTipItem2);
            this.barButtonRefrescar.SuperTip = superToolTip2;
            // 
            // barButtonCerrar
            // 
            this.barButtonCerrar.Caption = "Cerrar";
            this.barButtonCerrar.CloseSubMenuOnClick = false;
            this.barButtonCerrar.Id = 7;
            this.barButtonCerrar.LargeGlyph = global::UIRenderers.Properties.Resources.close;
            this.barButtonCerrar.Name = "barButtonCerrar";
            this.barButtonCerrar.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            toolTipTitleItem3.Text = "Cerrar";
            toolTipItem3.LeftIndent = 6;
            toolTipItem3.Text = "Cierra la ventana actual. Los cambios no guardados se perderán";
            superToolTip3.Items.Add(toolTipTitleItem3);
            superToolTip3.Items.Add(toolTipItem3);
            this.barButtonCerrar.SuperTip = superToolTip3;
            // 
            // printDocument
            // 
            this.printDocument.BeginPrint += new System.Drawing.Printing.PrintEventHandler( printDocument_BeginPrint );
            this.printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument_PrintPage);
            this.printDocument.EndPrint += new System.Drawing.Printing.PrintEventHandler( printDocument_EndPrint );
            // 
            // CalculadoraVentas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 517);
            this.Controls.Add(this.radioBtnEfectivo);
            this.Controls.Add(this.radioBtnPorcentaje);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.txtCambio);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.txtCuenta);
            this.Controls.Add(this.txtEfectivoSinDescuento);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtPorcentaje);
            this.Controls.Add(this.txtEfectivo);
            this.Controls.Add(this.radioBtnSinDescuento);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "CalculadoraVentas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CalculadoraVentas_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.txtEfectivo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPorcentaje.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEfectivoSinDescuento.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCuenta.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCambio.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollectionCalculadora)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton radioBtnSinDescuento;
        private DevExpress.XtraEditors.TextEdit txtEfectivo;
        private DevExpress.XtraEditors.TextEdit txtPorcentaje;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.TextEdit txtEfectivoSinDescuento;
        private DevExpress.XtraEditors.TextEdit txtCuenta;
        private DevExpress.XtraEditors.TextEdit txtTotal;
        private DevExpress.XtraEditors.TextEdit txtCambio;
        private DevExpress.XtraEditors.SimpleButton btnAceptar;
        private System.Windows.Forms.RadioButton radioBtnPorcentaje;
        private System.Windows.Forms.RadioButton radioBtnEfectivo;
        private DevExpress.XtraBars.Alerter.AlertControl alertControlCalculadoraVentas;
        private DevExpress.Utils.ImageCollection imageCollectionCalculadora;
        private DevExpress.XtraBars.BarStaticItem barItem;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.BarButtonItem barButtonAdd;
        private DevExpress.XtraBars.BarButtonItem barButtonRefrescar;
        private DevExpress.XtraBars.BarButtonItem barButtonCerrar;
        private System.Drawing.Printing.PrintDocument printDocument;
    }
}
