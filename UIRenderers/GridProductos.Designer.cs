namespace UIRenderers
{
    partial class GridProductos
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
            this.panelControlGrid = new DevExpress.XtraEditors.PanelControl();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.cardView = new DevExpress.XtraGrid.Views.Card.CardView();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlGrid)).BeginInit();
            this.panelControlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControlGrid
            // 
            this.panelControlGrid.Appearance.BackColor = System.Drawing.Color.White;
            this.panelControlGrid.Appearance.Options.UseBackColor = true;
            this.panelControlGrid.Controls.Add(this.gridControl);
            this.panelControlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControlGrid.Location = new System.Drawing.Point(0, 0);
            this.panelControlGrid.Name = "panelControlGrid";
            this.panelControlGrid.Size = new System.Drawing.Size(1026, 490);
            this.panelControlGrid.TabIndex = 0;
            // 
            // gridControl
            // 
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.Location = new System.Drawing.Point(2, 2);
            this.gridControl.MainView = this.cardView;
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(1022, 486);
            this.gridControl.TabIndex = 0;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.cardView,
            this.gridView1});
            // 
            // cardView
            // 
            this.cardView.CardWidth = 275;
            this.cardView.FocusedCardTopFieldIndex = 0;
            this.cardView.GridControl = this.gridControl;
            this.cardView.Name = "cardView";
            this.cardView.OptionsBehavior.Editable = false;
            this.cardView.OptionsBehavior.FieldAutoHeight = true;
            this.cardView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;
            this.cardView.CustomDrawCardCaption += new DevExpress.XtraGrid.Views.Card.CardCaptionCustomDrawEventHandler(this.cardView_CustomDrawCardCaption);
            this.cardView.CustomDrawCardFieldCaption += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.cardView_CustomDrawCardFieldCaption);
            this.cardView.DoubleClick += new System.EventHandler(this.cardView_DoubleClick);
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl;
            this.gridView1.Name = "gridView1";
            this.gridView1.RowHeight = 27;
            // 
            // GridProductos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panelControlGrid);
            this.Name = "GridProductos";
            this.Size = new System.Drawing.Size(1026, 490);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlGrid)).EndInit();
            this.panelControlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControlGrid;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Card.CardView cardView;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}
