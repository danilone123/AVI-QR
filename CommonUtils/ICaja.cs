using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace CommonUtils
{
    public interface ICaja
    {
         void update(object[] row);
         void insertCaja(DataRow row);
         void deleteValueCaja(DataRow row);
    }
}
