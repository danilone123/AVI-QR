using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using CommonUtils;
using DevExpress.XtraGrid.Views.Base;

namespace UIRenderers
{
    public partial class PnlListaUsuarios : UserControl
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger( "PnlListaUsuarios" );
        
        public string MessageBarValue { get; set; }

        #region Init UI
        public PnlListaUsuarios( )
        {
            InitializeComponent( );
            log4net.Config.XmlConfigurator.Configure( );
            InitConexionDB( );
        }
        #endregion

        #region Methods
        private void InitConexionDB( )
        {
            CommonUtils.ConexionBD.StringDeConexion = CommonUtils.ConexionBD.GetConnectionString();//GetConnectionString( );
        }

        private static string GetConnectionString( )
        {
            return @"Data Source=Marcelo-PC\SQLEXPRESS;Database=Capresso;Initial Catalog=Capresso;Integrated Security=SSPI;";
        }

        private void InitializeGrid( )
        {
            CommonUtils.ConexionBD.AbrirConexion( );

            string sqlUsuarios = "SELECT UsuarioId, Nombre, Apellidos, Login, Password, Telefono, PrivilegiosXML " +
                "From UsuariosSistema ";

            try
            {
                SqlCommand cmdProduct = new SqlCommand( sqlUsuarios , CommonUtils.ConexionBD.Conexion );

                DataSet dataSet = new DataSet( );
                dataSet.Load( cmdProduct.ExecuteReader( ) , LoadOption.OverwriteChanges , "Usuarios" );

                gridControlUsuarios.DataSource = dataSet.Tables[ "Usuarios" ];
               
                MessageBarValue = dataSet.Tables[ "Usuarios" ].Rows.Count.ToString( ) + " Usuarios";
            }
            catch ( Exception ex )
            {
                log.Error( ex.Message , ex );
                MessageBarValue = "No se pudieron listar los productos. Hubo un error: " + ex.Message;
            }
            finally
            {
                barItem.Caption = MessageBarValue;
            }

            CommonUtils.ConexionBD.CerrarConexion( );
        }

        private Image Image
        {
            get
            {
                return imageCollection.Images[ 0 ];
            }
        }
        #endregion

        #region Events & Handlers
        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad( e );
            InitializeGrid( );
        }

        private void barButtonAdd_ItemClick( object sender , DevExpress.XtraBars.ItemClickEventArgs e )
        {
            if ( !( this.ParentForm as mainForm ).ContextControls.ContainsKey( "PnlNuevoPrivilegio" ) )
            {
                PnlNuevoPrivilegio pnlPrivilegios = new PnlNuevoPrivilegio( );
                pnlPrivilegios.Dock = DockStyle.Fill;
                pnlPrivilegios.Privilege += new PnlNuevoPrivilegio.PrivilegesHandler( pnlPrivilegios_Privilege );
                DevExpress.XtraTab.XtraTabPage tabItem = new DevExpress.XtraTab.XtraTabPage( );
                tabItem.Controls.Add( pnlPrivilegios );
                tabItem.Text = "Crear/Editar Usuarios";
                ( this.ParentForm as mainForm ).xtraTabControl.TabPages.Add( tabItem );
                ( this.ParentForm as mainForm ).xtraTabControl.SelectedTabPage = tabItem;

                ( this.ParentForm as mainForm ).ContextControls.Add( "PnlNuevoPrivilegio" , pnlPrivilegios );
            }
        }

        private void barButtonEdit_ItemClick( object sender , DevExpress.XtraBars.ItemClickEventArgs e )
        {
            DataRowView selectedRow;
            selectedRow = ( DataRowView ) gridViewUsuarios.GetFocusedRow( );
            
            if ( !( this.ParentForm as mainForm ).ContextControls.ContainsKey( "PnlNuevoPrivilegio" ) )
            //if (!(this.ParentForm as mainForm).ContextControlsForPrivilegios.ContainsKey(selectedRow.Row.ItemArray[0].ToString()))

            {
                PnlNuevoPrivilegio pnlPrivilegios = new PnlNuevoPrivilegio( selectedRow.Row.ItemArray[0].ToString(),selectedRow.Row.ItemArray[ 1 ].ToString( ) , selectedRow.Row.ItemArray[ 2 ].ToString( ),
                    selectedRow.Row.ItemArray[ 3 ].ToString( ) , selectedRow.Row.ItemArray[ 4 ].ToString( ) , selectedRow.Row.ItemArray[ 5 ].ToString( ) , selectedRow.Row.ItemArray[ 6 ].ToString( ) );
                pnlPrivilegios.Dock = DockStyle.Fill;
                pnlPrivilegios.Privilege += new PnlNuevoPrivilegio.PrivilegesHandler( pnlPrivilegios_Privilege );
                DevExpress.XtraTab.XtraTabPage tabItem = new DevExpress.XtraTab.XtraTabPage( );
                tabItem.Controls.Add( pnlPrivilegios );
                tabItem.Text = "Crear/Editar Usuarios";
                ( this.ParentForm as mainForm ).xtraTabControl.TabPages.Add( tabItem );
                ( this.ParentForm as mainForm ).xtraTabControl.SelectedTabPage = tabItem;
                //(this.ParentForm as mainForm).ContextControlsForPrivilegios.Add(selectedRow.Row.ItemArray[0].ToString(), pnlPrivilegios);
                ( this.ParentForm as mainForm ).ContextControls.Add( "PnlNuevoPrivilegio" , pnlPrivilegios );
            }
        }

        private void pnlPrivilegios_Privilege( PnlNuevoPrivilegio Privilegio )
        {
            InitializeGrid( );
        }

        private void barButtonDelete_ItemClick( object sender , DevExpress.XtraBars.ItemClickEventArgs e )
        {
            DataRowView selectedRow;

            selectedRow = ( DataRowView ) gridViewUsuarios.GetFocusedRow( );

            string sqlQuery;

            if ( selectedRow != null )
            {
                int userID = Convert.ToInt32( selectedRow.Row.ItemArray[ 0 ] );

                if ( MessageBox.Show( this , "Desea eliminar al usuario? ", "Usuarios" , MessageBoxButtons.YesNo ) == DialogResult.No )
                {
                    return;
                }
                else
                {
                    try
                    {
                        sqlQuery = "Delete from UsuariosSistema where UsuarioId=" + userID;
                        CommonUtils.ConexionBD.Actualizar( sqlQuery );

                        alertControl.Show( this.FindForm( ) , "Eliminación satisfactoria." , "El usuario " + selectedRow.Row.ItemArray[ 1 ] + " " +
                           selectedRow.Row.ItemArray[ 2 ] + " se eliminó del sistema." , Image );
                    }
                    catch ( Exception ex )
                    {
                        log.Error( ex.Message , ex );
                        MessageBarValue = " No se pudo completar la eliminación. " + ex.Message;
                    }
                    finally
                    {
                        barItem.Caption = MessageBarValue;
                    }
                }

                InitializeGrid( );
            }
        }

        private void gridControlUsuarios_KeyDown( object sender , KeyEventArgs e )
        {
            if ( e.KeyData == Keys.Delete )
            {
                barButtonDelete_ItemClick( null , null );
            }
            else if ( e.KeyData == Keys.Insert )
            {
                barButtonAdd_ItemClick( null , null );
            }
        }
        #endregion
    }
}
