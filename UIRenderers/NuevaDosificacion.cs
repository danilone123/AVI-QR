using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace UIRenderers
{
    public partial class NuevaDosificacion : XtraForm
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger( "NuevaDosificacion" );
        private bool isUpdating = false;
        public int DosificacionID { get; set; }

        public NuevaDosificacion( )
        {
            InitializeComponent( );
            log4net.Config.XmlConfigurator.Configure( );
            InitConexionDB( );
        }

        public NuevaDosificacion( CommonUtils.Dosificacion dosificacion )
        {
            InitializeComponent( );
            log4net.Config.XmlConfigurator.Configure( );
            InitConexionDB( );

            isUpdating = true;
            DosificacionID = dosificacion.DosificacionID;
            txtLlave.Text = dosificacion.Llave;
            txtAutorizacion.Text = dosificacion.Autorizacion;
            txtFactura.Text = dosificacion.NroFactura;
            dateFechaLimite.Text = Convert.ToDateTime( dosificacion.FechaLimite ).ToShortDateString( );
            checkBoxActiva.Checked = dosificacion.Activa == 1 ? true : false;
        }

        private void InitConexionDB( )
        {
            CommonUtils.ConexionBD.StringDeConexion = CommonUtils.ConexionBD.GetConnectionString( );
        }

        private void btnCancel_Click( object sender , EventArgs e )
        {
            this.Close( );
        }

        private void btnSave_Click( object sender , EventArgs e )
        {
            if ( txtLlave.Text == string.Empty && txtAutorizacion.Text == string.Empty && txtFactura.Text == string.Empty )
            {
                MessageBox.Show( this , "Debe llenar todos los valores del formulario" , "Dosificación" , MessageBoxButtons.OK );
                return;
            }
            else
            {
                DialogResult validacion = MessageBox.Show( this , "Esta seguro que los valores son los correctos?" , "Dosificación" , MessageBoxButtons.YesNo , MessageBoxIcon.Question );

                if ( validacion == System.Windows.Forms.DialogResult.Yes )
                {
                    SetUniqueActiveRow( );
                    SetDosificacionValues( );
                   
                    this.Close( );
                }
                else
                {
                    return;
                }
            }
        }

        private void SetDosificacionValues( )
        {
            int isActive = 0;
            string fechaLimiteEmision = string.Empty;
            string sqlQuery = string.Empty;

            isActive = checkBoxActiva.Checked ? 1 : 0;
            fechaLimiteEmision = dateFechaLimite.DateTime.Year + "-" + dateFechaLimite.DateTime.Month + "-" + dateFechaLimite.DateTime.Day;

            try
            {
                if ( isUpdating )
                {
                    sqlQuery = "UPDATE dosificacion set dosificacion_fechalimite='" + CommonUtils.StringUtils.EscapeSQL( fechaLimiteEmision ) + "',dosificacion_activa='" + isActive +
                            "',dosificacion_llave='" + CommonUtils.StringUtils.EscapeSQL( txtLlave.Text ) + "',dosificacion_nro_autorizacion='" + txtAutorizacion.Text +
                             "',dosificacion_ultimo_valor='" + CommonUtils.StringUtils.EscapeSQL( txtFactura.Text ) + "' where dosificacion_id=" + DosificacionID;
                }
                else
                {
                    sqlQuery = "INSERT INTO dosificacion(dosificacion_fechalimite,dosificacion_activa,dosificacion_llave, dosificacion_nro_autorizacion,dosificacion_ultimo_valor) VALUES(" +
                    "'" + fechaLimiteEmision + "', " + isActive + ", " + "'" + txtLlave.Text + "', " + txtAutorizacion.Text + ", " + txtFactura.Text + ")";
                }

                CommonUtils.ConexionBD.Actualizar( sqlQuery );
                this.Process( );
            }
            catch ( Exception ex )
            {
                log.Error( ex.Message , ex );
            }
            
        }

        private void SetUniqueActiveRow( )
        {
            string sqlQuery = "Select * FROM dosificacion WHERE (dosificacion_id = (SELECT  MAX(dosificacion_id)  FROM dosificacion))";
            DataTable factura = CommonUtils.ConexionBD.EjecutarConsulta( sqlQuery );
            int rows = factura.Rows.Count;

            if ( rows != 0 )
            {
                string sqlUpdate = "UPDATE dosificacion SET dosificacion_activa = 0";
                CommonUtils.ConexionBD.Actualizar( sqlUpdate );
            }
        }

        // Define a delegate named DosificacionHandler, which will encapsulate
        // any method that takes a string as the parameter and returns no value
        public delegate void DosificacionHandler( NuevaDosificacion Dosificacion );

        // Define an Event based on the above Delegate
        public event DosificacionHandler Dosificaciones;

        // Instead of having the Process() function take a delegate
        // as a parameter, we've declared a Log event. Call the Event,
        // using the OnXXXX Method, where XXXX is the name of the Event.
        public void Process( )
        {
            OnChange( this );
        }

        private void OnChange( NuevaDosificacion Dosificacion )
        {
            if ( Dosificaciones != null )
            {
                Dosificaciones( Dosificacion );
            }
        }
    }
}
