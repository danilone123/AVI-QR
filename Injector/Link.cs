using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIRenderers;
using CommonUtils;

namespace Injector
{
    public class Link
    {
        public Link( Usuarios us )
        {
            mainForm main = new mainForm(  );
            main.UsActual = us;
            main.Show( );
        }
    }
}
