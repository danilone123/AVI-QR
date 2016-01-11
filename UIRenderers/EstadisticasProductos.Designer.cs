namespace UIRenderers
{
    partial class EstadisticasProductos
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
            this.components = new System.ComponentModel.Container();
            DevExpress.Utils.SuperToolTip superToolTip3 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem3 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem3 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip4 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem4 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem4 = new DevExpress.Utils.ToolTipItem();
            DevExpress.XtraCharts.XYDiagram xyDiagram2 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel3 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
            DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel4 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barButtonItem = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonPreview = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonExport = new DevExpress.XtraBars.BarButtonItem();
            this.popupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageChart = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.panelControlEstadistica = new DevExpress.XtraEditors.PanelControl();
            this.chartControl = new DevExpress.XtraCharts.ChartControl();
            this.ribbonPageControl = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.barButtonItemClose = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlEstadistica)).BeginInit();
            this.panelControlEstadistica.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel4)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ApplicationButtonText = null;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItem,
            this.barButtonPreview,
            this.barButtonExport,
            this.barButtonItem1,
            this.barButtonItemClose});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 5;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage});
            this.ribbonControl1.SelectedPage = this.ribbonPage;
            this.ribbonControl1.Size = new System.Drawing.Size(1002, 141);
            // 
            // barButtonItem
            // 
            this.barButtonItem.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
            this.barButtonItem.Caption = "Categorías";
            this.barButtonItem.Glyph = global::UIRenderers.Properties.Resources.chartA;
            this.barButtonItem.Id = 0;
            this.barButtonItem.Name = "barButtonItem";
            this.barButtonItem.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            // 
            // barButtonPreview
            // 
            this.barButtonPreview.Caption = "Vista previa";
            this.barButtonPreview.Glyph = global::UIRenderers.Properties.Resources.printpreview;
            this.barButtonPreview.Id = 1;
            this.barButtonPreview.Name = "barButtonPreview";
            this.barButtonPreview.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            toolTipTitleItem3.Text = "Vista previa";
            toolTipItem3.LeftIndent = 6;
            toolTipItem3.Text = "Genera una vista de impresión previa.";
            superToolTip3.Items.Add(toolTipTitleItem3);
            superToolTip3.Items.Add(toolTipItem3);
            this.barButtonPreview.SuperTip = superToolTip3;
            this.barButtonPreview.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonPreview_ItemClick);
            // 
            // barButtonExport
            // 
            this.barButtonExport.ActAsDropDown = true;
            this.barButtonExport.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.barButtonExport.Caption = "Exportar";
            this.barButtonExport.DropDownControl = this.popupMenu;
            this.barButtonExport.Glyph = global::UIRenderers.Properties.Resources.export;
            this.barButtonExport.Id = 2;
            this.barButtonExport.Name = "barButtonExport";
            this.barButtonExport.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            toolTipTitleItem4.Text = "EXportar e Imprimir";
            toolTipItem4.LeftIndent = 6;
            toolTipItem4.Text = "Exportar/Genera un reporte en diferentes formatos.";
            superToolTip4.Items.Add(toolTipTitleItem4);
            superToolTip4.Items.Add(toolTipItem4);
            this.barButtonExport.SuperTip = superToolTip4;
            // 
            // popupMenu
            // 
            this.popupMenu.ItemLinks.Add(this.barButtonItem1);
            this.popupMenu.MenuDrawMode = DevExpress.XtraBars.MenuDrawMode.SmallImagesText;
            this.popupMenu.Name = "popupMenu";
            this.popupMenu.Ribbon = this.ribbonControl1;
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Exportar a PDF";
            this.barButtonItem1.Glyph = global::UIRenderers.Properties.Resources.pdf;
            this.barButtonItem1.Id = 3;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // ribbonPage
            // 
            this.ribbonPage.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageChart,
            this.ribbonPageGroup,
            this.ribbonPageControl});
            this.ribbonPage.Name = "ribbonPage";
            this.ribbonPage.Text = "Inicio";
            // 
            // ribbonPageChart
            // 
            this.ribbonPageChart.AllowTextClipping = false;
            this.ribbonPageChart.ItemLinks.Add(this.barButtonItem);
            this.ribbonPageChart.Name = "ribbonPageChart";
            this.ribbonPageChart.Text = "Estadísticas";
            // 
            // ribbonPageGroup
            // 
            this.ribbonPageGroup.AllowTextClipping = false;
            this.ribbonPageGroup.ItemLinks.Add(this.barButtonPreview);
            this.ribbonPageGroup.ItemLinks.Add(this.barButtonExport);
            this.ribbonPageGroup.Name = "ribbonPageGroup";
            this.ribbonPageGroup.Text = "Exportar e Imprimir";
            // 
            // panelControlEstadistica
            // 
            this.panelControlEstadistica.Controls.Add(this.chartControl);
            this.panelControlEstadistica.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControlEstadistica.Location = new System.Drawing.Point(0, 141);
            this.panelControlEstadistica.Name = "panelControlEstadistica";
            this.panelControlEstadistica.Size = new System.Drawing.Size(1002, 408);
            this.panelControlEstadistica.TabIndex = 1;
            // 
            // chartControl
            // 
            xyDiagram2.AxisX.Range.ScrollingRange.SideMarginsEnabled = true;
            xyDiagram2.AxisX.Range.SideMarginsEnabled = true;
            xyDiagram2.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram2.AxisY.Range.ScrollingRange.SideMarginsEnabled = true;
            xyDiagram2.AxisY.Range.SideMarginsEnabled = true;
            xyDiagram2.AxisY.VisibleInPanesSerializable = "-1";
            this.chartControl.Diagram = xyDiagram2;
            this.chartControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartControl.Location = new System.Drawing.Point(2, 2);
            this.chartControl.Name = "chartControl";
            sideBySideBarSeriesLabel3.LineVisible = true;
            series2.Label = sideBySideBarSeriesLabel3;
            series2.Name = "Categoria";
            this.chartControl.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series2};
            sideBySideBarSeriesLabel4.LineVisible = true;
            this.chartControl.SeriesTemplate.Label = sideBySideBarSeriesLabel4;
            this.chartControl.Size = new System.Drawing.Size(998, 404);
            this.chartControl.TabIndex = 0;
            // 
            // ribbonPageControl
            // 
            this.ribbonPageControl.ItemLinks.Add(this.barButtonItemClose);
            this.ribbonPageControl.Name = "ribbonPageControl";
            // 
            // barButtonItemClose
            // 
            this.barButtonItemClose.Caption = "Cerrar";
            this.barButtonItemClose.Glyph = global::UIRenderers.Properties.Resources.close;
            this.barButtonItemClose.Id = 4;
            this.barButtonItemClose.Name = "barButtonItemClose";
            this.barButtonItemClose.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.barButtonItemClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemClose_ItemClick);
            // 
            // EstadisticasProductos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControlEstadistica);
            this.Controls.Add(this.ribbonControl1);
            this.Name = "EstadisticasProductos";
            this.Size = new System.Drawing.Size(1002, 549);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlEstadistica)).EndInit();
            this.panelControlEstadistica.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(xyDiagram2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageChart;
        private DevExpress.XtraEditors.PanelControl panelControlEstadistica;
        private DevExpress.XtraCharts.ChartControl chartControl;
        private DevExpress.XtraBars.BarButtonItem barButtonItem;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup;
        private DevExpress.XtraBars.BarButtonItem barButtonPreview;
        private DevExpress.XtraBars.BarButtonItem barButtonExport;
        private DevExpress.XtraBars.PopupMenu popupMenu;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageControl;
        private DevExpress.XtraBars.BarButtonItem barButtonItemClose;
    }
}
