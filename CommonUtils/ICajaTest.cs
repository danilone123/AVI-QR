using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace CommonUtils
{
    public interface ICajaTest
    {
        void update(object[] row);
        void insertCaja(object[] row);
        void deleteValueCaja(DataRow row);
    }
}
