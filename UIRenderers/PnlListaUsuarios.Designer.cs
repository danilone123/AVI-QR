namespace UIRenderers
{
    partial class PnlListaUsuarios
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
            DevExpress.Utils.SuperToolTip superToolTip7 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem7 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem7 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip8 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem8 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem8 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip9 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem9 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem9 = new DevExpress.Utils.ToolTipItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PnlListaUsuarios));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.gridControlUsuarios = new DevExpress.XtraGrid.GridControl();
            this.gridViewUsuarios = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ribbonControl = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barButtonAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barItemEditar = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barItem = new DevExpress.XtraBars.BarStaticItem();
            this.ribbonPage = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.alertControl = new DevExpress.XtraBars.Alerter.AlertControl(this.components);
            this.imageCollection = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlUsuarios)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewUsuarios)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.gridControlUsuarios);
            this.panelControl1.Controls.Add(this.ribbonStatusBar);
            this.panelControl1.Controls.Add(this.ribbonControl);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1014, 702);
            this.panelControl1.TabIndex = 0;
            // 
            // gridControlUsuarios
            // 
            this.gridControlUsuarios.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlUsuarios.Location = new System.Drawing.Point(2, 143);
            this.gridControlUsuarios.MainView = this.gridViewUsuarios;
            this.gridControlUsuarios.MenuManager = this.ribbonControl;
            this.gridControlUsuarios.Name = "gridControlUsuarios";
            this.gridControlUsuarios.Size = new System.Drawing.Size(1010, 533);
            this.gridControlUsuarios.TabIndex = 2;
            this.gridControlUsuarios.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewUsuarios});
            this.gridControlUsuarios.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridControlUsuarios_KeyDown);
            // 
            // gridViewUsuarios
            // 
            this.gridViewUsuarios.GridControl = this.gridControlUsuarios;
            this.gridViewUsuarios.Name = "gridViewUsuarios";
            this.gridViewUsuarios.OptionsBehavior.Editable = false;
            // 
            // ribbonControl
            // 
            this.ribbonControl.ApplicationButtonText = null;
            this.ribbonControl.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonAdd,
            this.barItemEditar,
            this.barButtonDelete,
            this.barItem});
            this.ribbonControl.Location = new System.Drawing.Point(2, 2);
            this.ribbonControl.MaxItemId = 4;
            this.ribbonControl.Name = "ribbonControl";
            this.ribbonControl.PageCategoryAlignment = DevExpress.XtraBars.Ribbon.RibbonPageCategoryAlignment.Right;
            this.ribbonControl.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage});
            this.ribbonControl.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2010;
            this.ribbonControl.SelectedPage = this.ribbonPage;
            this.ribbonControl.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.True;
            this.ribbonControl.Size = new System.Drawing.Size(1010, 141);
            this.ribbonControl.StatusBar = this.ribbonStatusBar;
            this.ribbonControl.TransparentEditors = true;
            // 
            // barButtonAdd
            // 
            this.barButtonAdd.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
            this.barButtonAdd.Caption = "Nuevo";
            this.barButtonAdd.Glyph = global::UIRenderers.Properties.Resources.add;
            this.barButtonAdd.Id = 0;
            this.barButtonAdd.Name = "barButtonAdd";
            this.barButtonAdd.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            toolTipTitleItem7.Text = "Nuevo Usuario";
            toolTipItem7.LeftIndent = 6;
            toolTipItem7.Text = "Agrega un nuevo usuario del sistema.";
            superToolTip7.Items.Add(toolTipTitleItem7);
            superToolTip7.Items.Add(toolTipItem7);
            this.barButtonAdd.SuperTip = superToolTip7;
            this.barButtonAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonAdd_ItemClick);
            // 
            // barItemEditar
            // 
            this.barItemEditar.Caption = "Editar";
            this.barItemEditar.Glyph = global::UIRenderers.Properties.Resources.edit;
            this.barItemEditar.Id = 1;
            this.barItemEditar.Name = "barItemEditar";
            this.barItemEditar.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            toolTipTitleItem8.Text = "Editar Usuario";
            toolTipItem8.LeftIndent = 6;
            toolTipItem8.Text = "Edita el usuario seleccionado.";
            superToolTip8.Items.Add(toolTipTitleItem8);
            superToolTip8.Items.Add(toolTipItem8);
            this.barItemEditar.SuperTip = superToolTip8;
            this.barItemEditar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonEdit_ItemClick);
            // 
            // barButtonDelete
            // 
            this.barButtonDelete.Caption = "Eliminar";
            this.barButtonDelete.Glyph = global::UIRenderers.Properties.Resources.delete;
            this.barButtonDelete.Id = 2;
            this.barButtonDelete.Name = "barButtonDelete";
            this.barButtonDelete.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            toolTipTitleItem9.Text = "Eliminar usuario";
            toolTipItem9.LeftIndent = 6;
            toolTipItem9.Text = "Elimina del sistema al usuario seleccionado.";
            superToolTip9.Items.Add(toolTipTitleItem9);
            superToolTip9.Items.Add(toolTipItem9);
            this.barButtonDelete.SuperTip = superToolTip9;
            this.barButtonDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonDelete_ItemClick);
            // 
            // barItem
            // 
            this.barItem.Id = 3;
            this.barItem.Name = "barItem";
            this.barItem.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // ribbonPage
            // 
            this.ribbonPage.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup});
            this.ribbonPage.Name = "ribbonPage";
            this.ribbonPage.Text = "Inicio";
            // 
            // ribbonPageGroup
            // 
            this.ribbonPageGroup.AllowTextClipping = false;
            this.ribbonPageGroup.ItemLinks.Add(this.barButtonAdd);
            this.ribbonPageGroup.ItemLinks.Add(this.barItemEditar);
            this.ribbonPageGroup.ItemLinks.Add(this.barButtonDelete);
            this.ribbonPageGroup.Name = "ribbonPageGroup";
            this.ribbonPageGroup.Text = "Administración de registros";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.ItemLinks.Add(this.barItem);
            this.ribbonStatusBar.Location = new System.Drawing.Point(2, 676);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbonControl;
            this.ribbonStatusBar.Size = new System.Drawing.Size(1010, 24);
            // 
            // alertControl
            // 
            this.alertControl.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003;
            this.alertControl.LookAndFeel.UseDefaultLookAndFeel = false;
            // 
            // imageCollection
            // 
            this.imageCollection.ImageSize = new System.Drawing.Size(32, 32);
            this.imageCollection.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection.ImageStream")));
            this.imageCollection.Images.SetKeyName(0, "alertIcon.png");
            // 
            // PnlListaUsuarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Name = "PnlListaUsuarios";
            this.Size = new System.Drawing.Size(1014, 702);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlUsuarios)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewUsuarios)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup;
        private DevExpress.XtraBars.BarButtonItem barButtonAdd;
        private DevExpress.XtraBars.BarButtonItem barItemEditar;
        private DevExpress.XtraBars.BarButtonItem barButtonDelete;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraGrid.GridControl gridControlUsuarios;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewUsuarios;
        private DevExpress.XtraBars.BarStaticItem barItem;
        private DevExpress.XtraBars.Alerter.AlertControl alertControl;
        private DevExpress.Utils.ImageCollection imageCollection;
    }
}
