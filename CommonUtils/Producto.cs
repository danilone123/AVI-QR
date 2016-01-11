using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonUtils
{
    public class Producto
    {
        #region Propiedades
        public int ProductoID { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Tipo { get; set; }
        public int RecetaID { get; set; }
        public string Categoria { get; set; }
        public string Size { get; set; }
        public decimal PrecioVenta { get; set; }
        public byte[] Imagen { get; set; }
        public Boolean Visible { get; set; }
        #endregion

        public Producto( )
        {

        }
    }

    
}
