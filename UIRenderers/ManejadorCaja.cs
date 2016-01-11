using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;

namespace UIRenderers
{
    public partial class ManejadorCaja : DevExpress.XtraEditors.XtraUserControl
    {
        string idCaja = string.Empty;
        bool esNuevoRegistroCaja=true;
        private static log4net.ILog log = log4net.LogManager.GetLogger("ManejadorCaja");
        public string MessageBarValue { get; set; }

        public ManejadorCaja()
        {
            InitializeComponent();
            InitConexionDB();
            RefreshArqueoControls();
            RefreshGridArqueo();
            log4net.Config.XmlConfigurator.Configure();
            
        }

        private Image Image
        {
            get
            {
                return imageCollectionManejadorCajas.Images[0];
            }
        }

        private void InitConexionDB()
        {
            CommonUtils.ConexionBD.StringDeConexion = CommonUtils.ConexionBD.GetConnectionString();//GetConnectionString();            
        }
        
        private void RefreshGridArqueo()
        {
            string sqlQuery = "DECLARE @FromDate datetime, @ToDate datetime SET @FromDate = CONVERT(varchar, DATEADD(dd, 1 - DATEPART(dw, GETDATE()), GETDATE()), 7) SET @ToDate = CONVERT(varchar, DATEADD(dd,7 - DATEPART(dw, GETDATE()), GETDATE()), 7) " +
                " Select CajaId,DineroSistema,DineroReal,Diferencia,Descripcion,Fecha from Caja where esModificable='True' and " +
                "( convert(varchar,CONVERT(datetime,FechaText,21),7)  BETWEEN @FromDate AND @ToDate) ORDER BY Caja.Fecha DESC";
 
         //   " " +;
            //" WHERE ( CONVERT(varchar, Compras.FechaCompra , 7)BETWEEN CONVERT(varchar, DATEADD(dd, 1 - DATEPART(dw, GETDATE()), GETDATE()), 7) AND CONVERT(varchar, DATEADD(dd," +
         //   " 7 - DATEPART(dw, GETDATE()), GETDATE()), 7))" +
         //   " ORDER BY Compras.FechaCompra DESC";
            gridControlArqueo.DataSource = null;
            CommonUtils.ConexionBD.AbrirConexion();
            gridControlArqueo.DataSource = CommonUtils.ConexionBD.EjecutarConsulta(sqlQuery);
            gridViewArqueo.Columns[0].FieldName = "CajaId";
            gridViewArqueo.Columns[1].FieldName = "DineroSistema";
            gridViewArqueo.Columns[2].FieldName = "DineroReal";
            gridViewArqueo.Columns[3].FieldName = "Diferencia";
            gridViewArqueo.Columns[4].FieldName = "Descripcion";
            gridViewArqueo.Columns[5].FieldName = "Fecha";
            CommonUtils.ConexionBD.CerrarConexion();
        }

        //method used to show data about caja
        private void RefreshArqueoControls()
        {
            string sqlQuery = "Select CajaId,DineroSistema,DineroReal,Diferencia,Descripcion,Fecha,esModificable from Caja WHERE   (CajaId = (SELECT MAX(CajaId) FROM Caja))";
            CommonUtils.ConexionBD.AbrirConexion();
            SqlDataReader reader= CommonUtils.ConexionBD.EjecutarConsultaReader(sqlQuery);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    //(int)reader.GetDecimal(0);
                    idCaja=((int)reader.GetDecimal(0)).ToString();                    
                    txtCantidadActualSistema.Text=((float)reader.GetDouble(2)).ToString();
                    textEditDineroSistema.Text = ((float)reader.GetDouble(1)).ToString();             
                    memoEditDescripcion.Text = reader.GetString(4);
                    textEditDiff.Text = ((float)reader.GetDouble(3)).ToString();
                    if (reader.GetBoolean(6))//
                    {
                        esNuevoRegistroCaja = false;
                        labelControlTitleUpdate.Visible = true;
                        labelTitleNew.Visible = false;
                    }
                    else 
                    {
                        esNuevoRegistroCaja = true;
                        labelControlTitleUpdate.Visible = false;
                        labelTitleNew.Visible = true;
                    }

                    textEditDineroReal.Text = ((float)reader.GetDouble(2)).ToString();
                    DateTime time=reader.GetDateTime(5);                    
                    txtDate.Text = time.ToString();                   
                }
            }
            CommonUtils.ConexionBD.CerrarConexion();
        }

        //method used in order to avoid a modification in compras, ingresos, ventas after an arqueo was completed
        // it is not completed
        private void updateAllCajaUses()
        {
            string sqlQuery = string.Empty;
            //for compras
            sqlQuery = "update Compras set EsModificable='False' where EsModificable='True'";
            CommonUtils.ConexionBD.Actualizar(sqlQuery);
            //for compras especiales
            sqlQuery = "update ComprasEspeciales set EsModificable='False' where EsModificable='True'";
            CommonUtils.ConexionBD.Actualizar(sqlQuery);
            //for ingresos egresos
            sqlQuery = "update IngresosEgresos set EsModificable='False' where EsModificable='True'";
            CommonUtils.ConexionBD.Actualizar(sqlQuery);
        }
      

        private void textEditDineroReal_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (e.NewValue.ToString()!= string.Empty && textEditDineroSistema.Text != string.Empty)
            {
                textEditDiff.Text = (float.Parse(e.NewValue.ToString()) - float.Parse(textEditDineroSistema.Text)).ToString();
                
                txtCantidadActualSistema.Text = e.NewValue.ToString();
            }
        }

        private void btnArqueo_Click(object sender, EventArgs e)
        {
            string sql = string.Empty;
            sql = "Select CajaId from Caja WHERE (CajaId = (SELECT MAX(CajaId) FROM Caja))";
            string cajaId = CommonUtils.ConexionBD.EjecutarConsultaEspecial(sql);
            sql = "Select DineroSistema from Caja WHERE (CajaId = (SELECT MAX(CajaId) FROM Caja))";
            //string dineroSistema = CommonUtils.ConexionBD.EjecutarConsultaEspecial(sql);
            string dineroSistema = ( ( float ) ( Double.Parse( CommonUtils.ConexionBD.EjecutarConsultaEspecial( sql ) ) ) ).ToString( );

            if (cajaId == idCaja && dineroSistema == textEditDineroSistema.Text)    //hasnt been a change in caja table        
            {
                //  string dineroReal = textEditDineroReal.Text.Replace(".", "");
                float dineroReal = float.Parse(textEditDineroReal.Text);//checkValidValue();
                // string dineroReal = textEditDineroReal.Text;
                try
                {
                    if (esNuevoRegistroCaja)
                    {
                        sql = "insert into Caja (FechaText,DineroSistema,DineroReal,Diferencia,Descripcion,Fecha,esModificable) values('" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss.fff") + "','" + textEditDineroSistema.Text + "','" + dineroReal + "'," + textEditDiff.Text + ",'" + CommonUtils.StringUtils.EscapeSQL(memoEditDescripcion.Text) + "','" + DateTime.Now + "','True')";
                        idCaja = CommonUtils.ConexionBD.ActualizarRetornandoId(sql).ToString();
                        txtDate.Text = DateTime.Now.ToString();
                        esNuevoRegistroCaja = false;
                        labelTitleNew.Visible = false;
                        labelControlTitleUpdate.Visible = true;
                        updateAllCajaUses();
                        RefreshGridArqueo();
                    }
                    else
                    {
                        sql = "update Caja set DineroReal='" + dineroReal + "', Diferencia='" + textEditDiff.Text + "', Descripcion='" + CommonUtils.StringUtils.EscapeSQL(memoEditDescripcion.Text) + "' where CajaId=" + idCaja;
                        CommonUtils.ConexionBD.Actualizar(sql);
                        labelControlTitleUpdate.Visible = true;
                        labelTitleNew.Visible = false;
                        RefreshGridArqueo();
                    }
                   
                    MessageBarValue="Caja fue modificada satisfactoriamente.";
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message, ex);
                    MessageBarValue = "No se pudo modificar caja. Hubo un error: " + ex.Message; 
                }
                finally
                {
                    alertControlManejadorCaja.Show(this.FindForm(),
                                          "Modificacion caja.", MessageBarValue
                                          , Image);
                }
            }
            else
            {
                //FIXME show a message indicating that caja was changed
                //MessageBox.Show();
                try
                {
                    MessageBox.Show(this, "Se realizaron cambios en el sistema que afectaron Caja", "Caja", MessageBoxButtons.OK);
                    RefreshArqueoControls();
                    RefreshGridArqueo();
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message, ex);
                    MessageBarValue = "No se pudo refrescar caja. Hubo un error: " + ex.Message;
                    alertControlManejadorCaja.Show(this.FindForm(),
                                         "Modificacion caja.", MessageBarValue
                                         , Image);
                }
               
            }
        }

        private void barButtonRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                RefreshArqueoControls();
                RefreshGridArqueo();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                MessageBarValue = "No se pudo refrescar caja. Hubo un error: " + ex.Message;
                alertControlManejadorCaja.Show(this.FindForm(),
                                         "Refrescar.", MessageBarValue
                                         , Image);
            }           
        }
    }
}
    