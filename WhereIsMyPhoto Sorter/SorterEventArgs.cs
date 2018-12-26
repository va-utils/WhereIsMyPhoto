using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereIsMyPhoto_Sorter
{
    public delegate void SortedFileChanged(SortedEventArgs sea);

    public class SortedEventArgs : EventArgs
    {
        public string CurrectFile { get; private set; }
        public string Date { get; private set; }
        public string DestinationDirectory { get; private set; }
        public SortedEventArgs(string currentFile, string date,string destinationDirectory)
        {
            CurrectFile = currentFile;
            Date = date;
            DestinationDirectory = destinationDirectory;
        }
    }
}
