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
    public partial class DatosEmpresa : XtraForm
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger( "DatosEmpresa" );
        private bool isUpdating = false;
        public int EmpresaID { get; set; }

        public DatosEmpresa( )
        {
            InitializeComponent( );
            log4net.Config.XmlConfigurator.Configure( );
            InitConexionDB( );
        }

        public DatosEmpresa( CommonUtils.Empresa empresa )
        {
            InitializeComponent( );
            log4net.Config.XmlConfigurator.Configure( );
            InitConexionDB( );

            isUpdating = true;
            txtDireccion.Text = empresa.Direccion;
            txtNIT.Text = empresa.NIT;
            txtNomEmpresa.Text = empresa.NombreEmpresa;
            txtRazonSocial.Text = empresa.RazonSocial;
            spinEdit.Text = empresa.SucursalNro.ToString( );
            txtTelefono.Text = empresa.Telefono;
            txtRubro.Text = empresa.Rubro;
        }

        private void InitConexionDB( )
        {
            CommonUtils.ConexionBD.StringDeConexion = CommonUtils.ConexionBD.GetConnectionString( );
        }

        private void btnSave_Click( object sender , EventArgs e )
        {
            if ( txtDireccion.Text == string.Empty && txtNIT.Text == string.Empty && txtNomEmpresa.Text == string.Empty && txtRazonSocial.Text == string.Empty )
            {
                MessageBox.Show( this , "Debe llenar todos los valores del formulario" , "Informacion de la Empresa" , MessageBoxButtons.OK );
                return;
            }
            else
            {
                DialogResult validacion = MessageBox.Show( this , "Esta seguro que los valores son los correctos?" , "Informacion de la Empresa" , MessageBoxButtons.YesNo , MessageBoxIcon.Question );

                if ( validacion == System.Windows.Forms.DialogResult.Yes )
                {
                    SetEmpresaValues( );
                    this.Close( );
                }
                else
                {
                    return;
                }
            }
        }

        private void SetEmpresaValues( )
        {
            string sqlQuery = string.Empty;

            try
            {
                if ( isUpdating )
                {
                    sqlQuery = "UPDATE telecentro set TelecentroId='" + CommonUtils.StringUtils.EscapeSQL( spinEdit.Text ) + "',RazonSocial='" + txtRazonSocial.Text +
                            "',NombreEmpresa='" + CommonUtils.StringUtils.EscapeSQL( txtNomEmpresa.Text ) + "',Direccion='" + txtDireccion.Text +
                             "',NIT='" + CommonUtils.StringUtils.EscapeSQL( txtNIT.Text ) +
                             "',SucursalNro='" + CommonUtils.StringUtils.EscapeSQL( spinEdit.Text ) + "',Rubro='" + CommonUtils.StringUtils.EscapeSQL(txtRubro.Text) + "' where TelecentroId=" + spinEdit.Value;
                }
                else
                {
                    sqlQuery = "INSERT INTO telecentro(TelecentroId,RazonSocial,NombreEmpresa, Direccion,NIT, SucursalNro, Telefono, Rubro) VALUES(" +
                    "" + spinEdit.Value + ", " + "'" + txtRazonSocial.Text + "', " + "'" + txtNomEmpresa.Text + "', " + "'" + txtDireccion.Text + "', " +
                    txtNIT.Text + ", " + spinEdit.Value + ", " + "'" + txtTelefono.Text + "'," + "'" + txtRubro.Text + "')";
                }

                CommonUtils.ConexionBD.Actualizar( sqlQuery );//56,98,600420
            }
            catch ( Exception ex )
            {
                log.Error( ex.Message , ex );
            }
        }

        private void btnCancel_Click( object sender , EventArgs e )
        {
            this.Close( );
        }
    }
}
