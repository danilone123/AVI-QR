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
    public partial class DescripcionPedidoForm : XtraForm
    {
        private string _NombreCliente;
        private string _NumeroPedido;
        private string _TipoPedido;
        private string _PedidoId;
        public DescripcionPedidoForm()
        {
            InitializeComponent();
          

        }

        private void InitConexionDB()
        {
            CommonUtils.ConexionBD.StringDeConexion = CommonUtils.ConexionBD.GetConnectionString();
        }
        private void RefreshGridViewDescripcionPedido()
        {
            string sqlQuery = "Select Producto.Nombre as Articulo,Pedido_Producto.Cantidad,Pedido_Producto.PrecioProducto as Precio, Pedido_Producto.Total as Subtotal, Pedido_Producto.Descripcion as Descripcion from Pedido_Producto,Producto where Producto.ProductoId=Pedido_Producto.ProductoId and Pedido_Producto.PedidoId="+_PedidoId;
            gridControlDescripcion.DataSource = CommonUtils.ConexionBD.EjecutarConsulta(sqlQuery);
        }

        public DescripcionPedidoForm(Form parentForm, string nombre, string numeroPedido, string tipoPedido, string pedidoId)
        {
            InitializeComponent();
            _NombreCliente = nombre;
            _NumeroPedido = numeroPedido;
            _TipoPedido = tipoPedido;
            _PedidoId = pedidoId;
            InitializeLabels();
            InitConexionDB();
            RefreshGridViewDescripcionPedido();
            if (parentForm != null)
            {
                Left = parentForm.Left + (parentForm.Width - Width) / 2;
                Top = parentForm.Top + (parentForm.Height - Height) / 2;
            }
        }

        private void InitializeLabels()
        {
            lblValueNumeroPedido.Text = this._NumeroPedido;
            lblValueTipoPedido.Text = this._TipoPedido;
            lblNombreValue.Text = this._NombreCliente;
        }


    }
}
