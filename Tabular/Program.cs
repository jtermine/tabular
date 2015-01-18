using System;
using System.Windows.Forms;
using DevExpress.UserSkins;
using DevExpress.Skins;
using DevExpress.LookAndFeel;

namespace Tabular
{
    static class Program
    {
        public static readonly object Lock = new Object();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            BonusSkins.Register();
            SkinManager.EnableFormSkins();
            UserLookAndFeel.Default.SetSkinStyle("DevExpress Style");
// ReSharper disable once CSharpWarnings::CS0618
            DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
            Application.Run(new TabularMainForm());
        }
    }
}
