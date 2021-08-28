using System;
using System.Threading;
using System.Windows.Forms;

namespace TemporalAssassinUtility
{
    static class Program
    {
        static Mutex mu;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            const string appName = "Temporal Assassin AUtility";
            mu = new Mutex(true, appName, out bool createdNew);

            if (!createdNew)
            {
                MessageBox.Show("You cannot have more than one instance of this program running", $"{appName} is already running", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmTAU());
        }
    }
}
