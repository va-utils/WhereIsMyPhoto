using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhereIsMyPhoto
{
    static class HelpAndInformation
    {
        public static void ShowHelpFile()
        {
            if (System.IO.File.Exists("help.html"))
            {
                string run = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath), "help.html");
                try
                {
                    Process.Start(run);
                }
                catch
                {
                    MessageBox.Show("Не удалось открыть файл со справочной информацией.");
                }
            }
        }

        public static void SendBug()
        {
            try
            {
                Process.Start("http://va-soft.eviko.org/?page_id=15");
            }
            catch
            {
                MessageBox.Show("Не удалось открыть браузер...");
            }
        }
    }
}
