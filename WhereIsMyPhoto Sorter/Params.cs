using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereIsMyPhoto_Sorter
{
    enum DestinationDirectoriesFormat
    {
        YearMonth,
        YearMonthDay
    }

    enum FileOperations
    {
        Copy,
        Move
    }

    struct Params
    {
        public FileOperations mode;
        public DestinationDirectoriesFormat format;
        public Params(FileOperations fo, DestinationDirectoriesFormat ddf)
        {
            mode = fo;
            format = ddf;
        }
    }
}
