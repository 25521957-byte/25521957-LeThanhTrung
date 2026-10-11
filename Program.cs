using System;
using System.Windows.Forms;

namespace MyProject
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            Application.Run(new FrmLogin());
        }
    }
}