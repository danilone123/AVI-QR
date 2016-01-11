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
    public partial class InitCajaForm : XtraForm
    {
        public InitCajaForm()
        {
            InitializeComponent();
            InitConexionDB();
        }

        private void InitConexionDB()
        {
            CommonUtils.ConexionBD.StringDeConexion = CommonUtils.ConexionBD.GetConnectionString();
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            if (txtDinero.Text == string.Empty)
            {
                MessageBox.Show(this, "Debe introducir un valor numerico", "Caja", MessageBoxButtons.OK);
                return;
            }
            string sqlQuery = "INSERT INTO Caja(FechaText,DineroSistema,DineroReal,Diferencia,Descripcion,Fecha,esModificable) VALUES('" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss.fff") + "'," + txtDinero.Text + ",0,0,'Inicio caja','" + DateTime.Now + "','False')";
            CommonUtils.ConexionBD.Actualizar(sqlQuery);
            this.Close();
        }
    }
}
