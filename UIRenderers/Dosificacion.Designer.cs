namespace UIRenderers
{
    partial class Dosificacion
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dosificacion));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barButton = new DevExpress.XtraBars.Bar();
            this.barLargeButtonItem = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barItem = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.gridDosificacion = new DevExpress.XtraGrid.GridControl();
            this.gridViewDosificacion = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDosificacion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDosificacion)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barButton,
            this.barItem});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barLargeButtonItem,
            this.barStaticItem1});
            this.barManager1.MaxItemId = 2;
            this.barManager1.StatusBar = this.barItem;
            // 
            // barButton
            // 
            this.barButton.BarName = "Tools";
            this.barButton.DockCol = 0;
            this.barButton.DockRow = 0;
            this.barButton.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.barButton.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barLargeButtonItem)});
            this.barButton.OptionsBar.AllowQuickCustomization = false;
            this.barButton.Text = "Agregar Dosificación";
            // 
            // barLargeButtonItem
            // 
            this.barLargeButtonItem.Caption = "Agregar Dosificación";
            this.barLargeButtonItem.Id = 0;
            this.barLargeButtonItem.Name = "barLargeButtonItem";
            this.barLargeButtonItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barLargeButtonItem1_ItemClick);
            // 
            // barItem
            // 
            this.barItem.BarName = "Custom 3";
            this.barItem.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.barItem.DockCol = 0;
            this.barItem.DockRow = 0;
            this.barItem.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.barItem.OptionsBar.AllowQuickCustomization = false;
            this.barItem.OptionsBar.DrawDragBorder = false;
            this.barItem.OptionsBar.UseWholeRow = true;
            this.barItem.Text = "Custom 3";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(635, 24);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 273);
            this.barDockControlBottom.Size = new System.Drawing.Size(635, 22);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 24);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 249);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(635, 24);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 249);
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Caption = "barStaticItem1";
            this.barStaticItem1.Id = 1;
            this.barStaticItem1.Name = "barStaticItem1";
            this.barStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // gridDosificacion
            // 
            this.gridDosificacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridDosificacion.Location = new System.Drawing.Point(0, 24);
            this.gridDosificacion.MainView = this.gridViewDosificacion;
            this.gridDosificacion.MenuManager = this.barManager1;
            this.gridDosificacion.Name = "gridDosificacion";
            this.gridDosificacion.Size = new System.Drawing.Size(635, 249);
            this.gridDosificacion.TabIndex = 4;
            this.gridDosificacion.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewDosificacion});
            this.gridDosificacion.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.gridDosificacion_MouseDoubleClick);
            // 
            // gridViewDosificacion
            // 
            this.gridViewDosificacion.GridControl = this.gridDosificacion;
            this.gridViewDosificacion.Name = "gridViewDosificacion";
            this.gridViewDosificacion.OptionsBehavior.Editable = false;
            this.gridViewDosificacion.OptionsCustomization.AllowGroup = false;
            // 
            // Dosificacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 295);
            this.Controls.Add(this.gridDosificacion);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Dosificacion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dosificacion";
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDosificacion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDosificacion)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar barButton;
        private DevExpress.XtraBars.BarLargeButtonItem barLargeButtonItem;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraGrid.GridControl gridDosificacion;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewDosificacion;
        private DevExpress.XtraBars.Bar barItem;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;

    }
}