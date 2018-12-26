using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace WhereIsMyPhoto_Sorter
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        /// 
        
        [STAThread]
        static void Main()
        {
            Stream forTraceFileStream = null;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string forTracePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "whereismyphoto-sorter.log.txt");
            forTraceFileStream = new System.IO.FileStream(forTracePath, System.IO.FileMode.Create);
            Trace.Listeners.Clear();
            Trace.Listeners.Add(new DefaultTraceListener());
            Trace.Listeners.Add(new TextWriterTraceListener(forTraceFileStream));
            Trace.AutoFlush = true;
            Application.Run(new SorterMainForm());
        }
    }
}
