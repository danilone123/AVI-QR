using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonUtils
{
    public class ReportesUtils
    {
        private ResultDelegate callBack;

        public ReportesUtils(ResultDelegate _callBack)
        {
            callBack = _callBack;
        }

        public void ThreadProc()
        {
            if (callBack != null)
            {
                callBack();
            }
 
        }

        public delegate void ResultDelegate();
    }
}
