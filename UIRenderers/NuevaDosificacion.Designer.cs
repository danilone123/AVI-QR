namespace UIRenderers
{
    partial class NuevaDosificacion
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent( )
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NuevaDosificacion));
            this.groupControl = new DevExpress.XtraEditors.GroupControl();
            this.checkBoxActiva = new System.Windows.Forms.CheckBox();
            this.dateFechaLimite = new DevExpress.XtraEditors.DateEdit();
            this.lblFecha = new System.Windows.Forms.Label();
            this.txtFactura = new System.Windows.Forms.TextBox();
            this.lblFactura = new System.Windows.Forms.Label();
            this.txtAutorizacion = new System.Windows.Forms.TextBox();
            this.lblAutorizacion = new System.Windows.Forms.Label();
            this.txtLlave = new System.Windows.Forms.TextBox();
            this.lblLlave = new DevExpress.XtraEditors.LabelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl)).BeginInit();
            this.groupControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateFechaLimite.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateFechaLimite.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl
            // 
            this.groupControl.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl.Appearance.Options.UseFont = true;
            this.groupControl.Controls.Add(this.checkBoxActiva);
            this.groupControl.Controls.Add(this.dateFechaLimite);
            this.groupControl.Controls.Add(this.lblFecha);
            this.groupControl.Controls.Add(this.txtFactura);
            this.groupControl.Controls.Add(this.lblFactura);
            this.groupControl.Controls.Add(this.txtAutorizacion);
            this.groupControl.Controls.Add(this.lblAutorizacion);
            this.groupControl.Controls.Add(this.txtLlave);
            this.groupControl.Controls.Add(this.lblLlave);
            this.groupControl.Location = new System.Drawing.Point(12, 12);
            this.groupControl.Name = "groupControl";
            this.groupControl.Size = new System.Drawing.Size(611, 243);
            this.groupControl.TabIndex = 0;
            this.groupControl.Text = "Datos de la Dosificación";
            // 
            // checkBoxActiva
            // 
            this.checkBoxActiva.AutoSize = true;
            this.checkBoxActiva.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxActiva.Location = new System.Drawing.Point(342, 105);
            this.checkBoxActiva.Name = "checkBoxActiva";
            this.checkBoxActiva.Size = new System.Drawing.Size(60, 17);
            this.checkBoxActiva.TabIndex = 8;
            this.checkBoxActiva.Text = "Activa:";
            this.checkBoxActiva.UseVisualStyleBackColor = true;
            // 
            // dateFechaLimite
            // 
            this.dateFechaLimite.EditValue = null;
            this.dateFechaLimite.Location = new System.Drawing.Point(134, 102);
            this.dateFechaLimite.Name = "dateFechaLimite";
            this.dateFechaLimite.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateFechaLimite.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateFechaLimite.Size = new System.Drawing.Size(199, 20);
            this.dateFechaLimite.TabIndex = 7;
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new System.Drawing.Point(3, 105);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(123, 13);
            this.lblFecha.TabIndex = 6;
            this.lblFecha.Text = "Fecha Limite de Emisión:";
            // 
            // txtFactura
            // 
            this.txtFactura.Location = new System.Drawing.Point(446, 74);
            this.txtFactura.MaxLength = 12;
            this.txtFactura.Name = "txtFactura";
            this.txtFactura.Size = new System.Drawing.Size(158, 21);
            this.txtFactura.TabIndex = 5;
            // 
            // lblFactura
            // 
            this.lblFactura.AutoSize = true;
            this.lblFactura.Location = new System.Drawing.Point(339, 77);
            this.lblFactura.Name = "lblFactura";
            this.lblFactura.Size = new System.Drawing.Size(101, 13);
            this.lblFactura.TabIndex = 4;
            this.lblFactura.Text = "Número de factura:";
            // 
            // txtAutorizacion
            // 
            this.txtAutorizacion.Location = new System.Drawing.Point(134, 74);
            this.txtAutorizacion.MaxLength = 15;
            this.txtAutorizacion.Name = "txtAutorizacion";
            this.txtAutorizacion.Size = new System.Drawing.Size(199, 21);
            this.txtAutorizacion.TabIndex = 3;
            // 
            // lblAutorizacion
            // 
            this.lblAutorizacion.AutoSize = true;
            this.lblAutorizacion.Location = new System.Drawing.Point(3, 77);
            this.lblAutorizacion.Name = "lblAutorizacion";
            this.lblAutorizacion.Size = new System.Drawing.Size(125, 13);
            this.lblAutorizacion.TabIndex = 2;
            this.lblAutorizacion.Text = "Número de Autorización:";
            // 
            // txtLlave
            // 
            this.txtLlave.Location = new System.Drawing.Point(114, 47);
            this.txtLlave.MaxLength = 256;
            this.txtLlave.Name = "txtLlave";
            this.txtLlave.Size = new System.Drawing.Size(490, 21);
            this.txtLlave.TabIndex = 1;
            // 
            // lblLlave
            // 
            this.lblLlave.Location = new System.Drawing.Point(5, 50);
            this.lblLlave.Name = "lblLlave";
            this.lblLlave.Size = new System.Drawing.Size(103, 13);
            this.lblLlave.TabIndex = 0;
            this.lblLlave.Text = "Llave de Dosificación:";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(447, 267);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(95, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "&Guardar y Cerrar";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(548, 267);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "&Cerrar";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // NuevaDosificacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 302);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NuevaDosificacion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nueva Dosificacion";
            ((System.ComponentModel.ISupportInitialize)(this.groupControl)).EndInit();
            this.groupControl.ResumeLayout(false);
            this.groupControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateFechaLimite.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateFechaLimite.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private System.Windows.Forms.TextBox txtLlave;
        private DevExpress.XtraEditors.LabelControl lblLlave;
        private System.Windows.Forms.TextBox txtFactura;
        private System.Windows.Forms.Label lblFactura;
        private System.Windows.Forms.TextBox txtAutorizacion;
        private System.Windows.Forms.Label lblAutorizacion;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.CheckBox checkBoxActiva;
        private DevExpress.XtraEditors.DateEdit dateFechaLimite;

    }
}