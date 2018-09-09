using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhereIsMyPhoto
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            System.IO.FileStream forTraceFileStream = null;
            
            if (args.Contains("trace"))
            {
                string forTracePath =System.IO.Path.Combine( Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "whereismyphoto.log.txt");
                forTraceFileStream = new System.IO.FileStream(forTracePath, System.IO.FileMode.Create);
                Trace.Listeners.Clear();
                Trace.Listeners.Add(new DefaultTraceListener());
                Trace.Listeners.Add(new TextWriterTraceListener(forTraceFileStream));
                Trace.AutoFlush = true;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
            forTraceFileStream?.Close();
        }
    }
}
