using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraNavBar;

namespace UIRenderers
{
    class NodoMenu : NavBarItem
    {
        public string IDMenuItem;
        public string IDPadre;
        public string Texto;
        public string Nivel;
        public string Clase;

        public NodoMenu( DataRow DRow )
        {
            IDMenuItem = DRow[ "IDMenuItem" ].ToString( );
            IDPadre = DRow[ "IDPadre" ].ToString( );
            Texto = DRow[ "Texto" ].ToString( );
            Nivel = DRow[ "Nivel" ].ToString( );
            Clase = DRow[ "Clase" ].ToString( );
            
            Caption = Texto;
        }
    }
}
