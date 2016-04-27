using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ADSI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Forms.Usuario.frmUsuarios());
            //pruebaAltamira.lol();
            //Application.Run(new Controles.frmWaitScreen());
            Application.Run(new Forms.Usuario.frmUsuarios());

        }
    }
}
