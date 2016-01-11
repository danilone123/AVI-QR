namespace UIRenderers
{
    partial class DatosEmpresa
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
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatosEmpresa));
            this.groupControl = new DevExpress.XtraEditors.GroupControl();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNIT = new System.Windows.Forms.TextBox();
            this.spinEdit = new DevExpress.XtraEditors.SpinEdit();
            this.lblId = new DevExpress.XtraEditors.LabelControl();
            this.lblNIT = new System.Windows.Forms.Label();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.lblDireccion = new System.Windows.Forms.Label();
            this.txtNomEmpresa = new System.Windows.Forms.TextBox();
            this.lblNombreEmpresa = new System.Windows.Forms.Label();
            this.txtRazonSocial = new System.Windows.Forms.TextBox();
            this.lblLRazonSocial = new DevExpress.XtraEditors.LabelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.lblRubro = new System.Windows.Forms.Label();
            this.txtRubro = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl)).BeginInit();
            this.groupControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl
            // 
            this.groupControl.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl.Appearance.Options.UseFont = true;
            this.groupControl.Controls.Add(this.txtRubro);
            this.groupControl.Controls.Add(this.lblRubro);
            this.groupControl.Controls.Add(this.txtTelefono);
            this.groupControl.Controls.Add(this.label1);
            this.groupControl.Controls.Add(this.txtNIT);
            this.groupControl.Controls.Add(this.spinEdit);
            this.groupControl.Controls.Add(this.lblId);
            this.groupControl.Controls.Add(this.lblNIT);
            this.groupControl.Controls.Add(this.txtDireccion);
            this.groupControl.Controls.Add(this.lblDireccion);
            this.groupControl.Controls.Add(this.txtNomEmpresa);
            this.groupControl.Controls.Add(this.lblNombreEmpresa);
            this.groupControl.Controls.Add(this.txtRazonSocial);
            this.groupControl.Controls.Add(this.lblLRazonSocial);
            this.groupControl.Location = new System.Drawing.Point(7, 12);
            this.groupControl.Name = "groupControl";
            this.groupControl.Size = new System.Drawing.Size(611, 243);
            this.groupControl.TabIndex = 3;
            this.groupControl.Text = "Datos de la Empresa";
            // 
            // txtTelefono
            // 
            this.txtTelefono.Location = new System.Drawing.Point(114, 150);
            this.txtTelefono.MaxLength = 15;
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(219, 21);
            this.txtTelefono.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 153);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Telefono:";
            // 
            // txtNIT
            // 
            this.txtNIT.Location = new System.Drawing.Point(114, 123);
            this.txtNIT.MaxLength = 15;
            this.txtNIT.Name = "txtNIT";
            this.txtNIT.Size = new System.Drawing.Size(219, 21);
            this.txtNIT.TabIndex = 3;
            // 
            // spinEdit
            // 
            this.spinEdit.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinEdit.Location = new System.Drawing.Point(114, 42);
            this.spinEdit.Name = "spinEdit";
            this.spinEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEdit.Properties.Mask.EditMask = "\\d+";
            this.spinEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.spinEdit.Properties.Mask.ShowPlaceHolders = false;
            this.spinEdit.Size = new System.Drawing.Size(35, 20);
            this.spinEdit.TabIndex = 0;
            this.spinEdit.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            // 
            // lblId
            // 
            this.lblId.Location = new System.Drawing.Point(5, 44);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(55, 13);
            toolTipTitleItem1.Text = "Sucursal";
            toolTipItem1.LeftIndent = 6;
            toolTipItem1.Text = "Valor numerico mayor a 0 que indica el # de sucursal actual.";
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.lblId.SuperTip = superToolTip1;
            this.lblId.TabIndex = 9;
            this.lblId.Text = "Sucursal #:";
            // 
            // lblNIT
            // 
            this.lblNIT.AutoSize = true;
            this.lblNIT.Location = new System.Drawing.Point(3, 126);
            this.lblNIT.Name = "lblNIT";
            this.lblNIT.Size = new System.Drawing.Size(28, 13);
            this.lblNIT.TabIndex = 6;
            this.lblNIT.Text = "NIT:";
            // 
            // txtDireccion
            // 
            this.txtDireccion.Location = new System.Drawing.Point(399, 95);
            this.txtDireccion.MaxLength = 50000;
            this.txtDireccion.Multiline = true;
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.Size = new System.Drawing.Size(205, 48);
            this.txtDireccion.TabIndex = 5;
            // 
            // lblDireccion
            // 
            this.lblDireccion.AutoSize = true;
            this.lblDireccion.Location = new System.Drawing.Point(339, 98);
            this.lblDireccion.Name = "lblDireccion";
            this.lblDireccion.Size = new System.Drawing.Size(54, 13);
            this.lblDireccion.TabIndex = 4;
            this.lblDireccion.Text = "Direccion:";
            // 
            // txtNomEmpresa
            // 
            this.txtNomEmpresa.Location = new System.Drawing.Point(114, 95);
            this.txtNomEmpresa.MaxLength = 15;
            this.txtNomEmpresa.Name = "txtNomEmpresa";
            this.txtNomEmpresa.Size = new System.Drawing.Size(219, 21);
            this.txtNomEmpresa.TabIndex = 2;
            // 
            // lblNombreEmpresa
            // 
            this.lblNombreEmpresa.AutoSize = true;
            this.lblNombreEmpresa.Location = new System.Drawing.Point(3, 98);
            this.lblNombreEmpresa.Name = "lblNombreEmpresa";
            this.lblNombreEmpresa.Size = new System.Drawing.Size(114, 13);
            this.lblNombreEmpresa.TabIndex = 2;
            this.lblNombreEmpresa.Text = "Nombre de la Empresa";
            // 
            // txtRazonSocial
            // 
            this.txtRazonSocial.Location = new System.Drawing.Point(114, 68);
            this.txtRazonSocial.MaxLength = 256;
            this.txtRazonSocial.Name = "txtRazonSocial";
            this.txtRazonSocial.Size = new System.Drawing.Size(490, 21);
            this.txtRazonSocial.TabIndex = 1;
            // 
            // lblLRazonSocial
            // 
            this.lblLRazonSocial.Location = new System.Drawing.Point(5, 71);
            this.lblLRazonSocial.Name = "lblLRazonSocial";
            this.lblLRazonSocial.Size = new System.Drawing.Size(64, 13);
            toolTipTitleItem2.Text = "Razon Social";
            toolTipItem2.LeftIndent = 6;
            toolTipItem2.Text = "Es la denominación por la cual se conoce colectivamente a una empresa.";
            superToolTip2.Items.Add(toolTipTitleItem2);
            superToolTip2.Items.Add(toolTipItem2);
            this.lblLRazonSocial.SuperTip = superToolTip2;
            this.lblLRazonSocial.TabIndex = 0;
            this.lblLRazonSocial.Text = "Razon Social:";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(442, 267);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(95, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "&Guardar y Cerrar";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(543, 267);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "&Cerrar";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblRubro
            // 
            this.lblRubro.AutoSize = true;
            this.lblRubro.Location = new System.Drawing.Point(3, 187);
            this.lblRubro.Name = "lblRubro";
            this.lblRubro.Size = new System.Drawing.Size(108, 13);
            this.lblRubro.TabIndex = 14;
            this.lblRubro.Text = "Actividad Económica:";
            // 
            // txtRubro
            // 
            this.txtRubro.Location = new System.Drawing.Point(114, 182);
            this.txtRubro.MaxLength = 15;
            this.txtRubro.Name = "txtRubro";
            this.txtRubro.Size = new System.Drawing.Size(219, 21);
            this.txtRubro.TabIndex = 15;
            // 
            // DatosEmpresa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 302);
            this.Controls.Add(this.groupControl);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DatosEmpresa";
            this.Text = "Informacion de la Empresa";
            ((System.ComponentModel.ISupportInitialize)(this.groupControl)).EndInit();
            this.groupControl.ResumeLayout(false);
            this.groupControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl;
        private System.Windows.Forms.Label lblNIT;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.Label lblDireccion;
        private System.Windows.Forms.TextBox txtNomEmpresa;
        private System.Windows.Forms.Label lblNombreEmpresa;
        private System.Windows.Forms.TextBox txtRazonSocial;
        private DevExpress.XtraEditors.LabelControl lblLRazonSocial;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SpinEdit spinEdit;
        private DevExpress.XtraEditors.LabelControl lblId;
        private System.Windows.Forms.TextBox txtNIT;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRubro;
        private System.Windows.Forms.Label lblRubro;
    }
}