using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereIsMyPhoto_Sorter
{
    public delegate void SorterMetadataError(SorterErrorEventArgs seea);
    public class SorterErrorEventArgs : EventArgs
    {
        public string CurrentFile { get; private set; }
        public SorterErrorEventArgs(string fname)
        {
            CurrentFile = fname;
        }
    }
}
