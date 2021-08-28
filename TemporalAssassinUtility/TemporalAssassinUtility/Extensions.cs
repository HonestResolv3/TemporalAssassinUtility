using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TemporalAssassinUtility
{
    public static class Extensions
    {
        public static void ThreadSafeInvoke(this Control control, MethodInvoker method)
        {
            if (control != null)
            {
                if (control.InvokeRequired)
                    control.Invoke(method);
                else
                    method.Invoke();
            }
        }
    }
}
