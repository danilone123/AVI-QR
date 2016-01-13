using System;
using System.Drawing;
using System.Windows.Forms;
using CommonUtils;
using System.Threading;
using Injector;
using DevExpress.XtraEditors;

namespace AVI
{
    public partial class FormLogin : XtraForm
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger( "FormLogin" );

        public FormLogin()
        {
            InitializeComponent();
            log4net.Config.XmlConfigurator.Configure( );
            InitConexionDB( );
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                Usuarios us = new Usuarios( TxtLogin.Text , TxtPassword.Text );
                if ( us.Existe )
                {
                    new Parser( ).InitValues( us.Privilegios );

                    Hide( );

                    Thread thread = new Thread( new ThreadStart( Splash ) );
                    thread.Start( );
                    Thread.Sleep( 6000 );
                    thread.Abort( );

                    Injector.Link main = new Injector.Link( us );
                    
                    ConexionBD.UsuarioActual = us;
                }
                else
                {
                    MessageBox.Show( "El Login y/o el Password son incorrectos" , "Atención" , MessageBoxButtons.OK , MessageBoxIcon.Information );
                }
            }
            catch ( Exception ex)
            {
                log.Error( ex.Message , ex );
                alertControl.Show( this , "Hubo un error" , ex.Message , Image );
                MessageBox.Show( ex.Message );
            }
        }

        private void Splash( )
        {
            AboutBox splash = new AboutBox( );
            splash.ShowDialog( );
        }

        public Image Image
        {
            get
            {
                return imageCollection1.Images[ 0 ];
            }
        }

        private void InitConexionDB( )
        {
            CommonUtils.ConexionBD.StringDeConexion = CommonUtils.ConexionBD.GetConnectionString( );
        }

        private void enter(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                BtnLogin.Focus();
                BtnLogin_Click(sender, e);
            }
        }


    }
}