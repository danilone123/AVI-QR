using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.LookAndFeel;

namespace AVI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main( )
        {
            Application.EnableVisualStyles( );
            Application.SetCompatibleTextRenderingDefault( false );

            DevExpress.Skins.SkinManager.EnableFormSkins( );
            UserLookAndFeel.Default.SetSkinStyle( "The Asphalt World" );
            CommonUtils.ConexionBD.StringDeConexion = CommonUtils.ConexionBD.GetConnectionString();
            CommonUtils.ConexionBD.AbrirConexion();
            System.Data.DataTable read = CommonUtils.ConexionBD.TraerTabla("Pedido");//("SELECT COUNT(*) FROM  Pedido");

            int numberOfRows = read.Rows.Count;
            CommonUtils.ConexionBD.CerrarConexion();
            if (numberOfRows > 1000)
            {
                string message = "El sistema es una version demo y no podra usar a partir de ahora";
                string caption = "Demo";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;

                // Displays the MessageBox.

                result = MessageBox.Show(message, caption, buttons);

                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    Application.Exit();
                    return;
                }
            }
            Application.Run( new FormLogin( ) );
        }
    }
}
