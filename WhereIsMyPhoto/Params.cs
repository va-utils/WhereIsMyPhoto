using System;
using System.Collections.Generic;
using MetadataExtractor;

namespace WhereIsMyPhoto
{
    public enum ExposureProgram
    {
        Any,
        Manual, 
        Program,
        Apperture,
        Shutter,
        Creative,
        Action,
        Portrait,
        Landspace,
    }

    public enum Orientation
    {
        Any,
        Horizonatal,
        Vertical,
    }

    public class SearchChangeFolderEventArgs : EventArgs
    {
        public string FolderName { get; private set; }
        public int FilesCount { get; private set; }
        public SearchChangeFolderEventArgs(string fn, int count)
        {
            FolderName = fn;
            FilesCount = count;
        }
    }

    public delegate void SearchChangeFolderEventHandler(SearchChangeFolderEventArgs f,bool flag = false);

    public class SearchNewFileEventArgs : EventArgs
    {
        public ImageInformation file { get; private set; }
        public SearchNewFileEventArgs(ImageInformation f)
        {
            file = f;
        }
    }

    public delegate void SearchNewFileEventHandler(SearchNewFileEventArgs f, bool flag = false);

    public class ImageInformation
    {
        public string FileName { get; set; }
        public IEnumerable<MetadataExtractor.Directory> Directories { get; set; }
        public ImageInformation(string fi, IEnumerable<MetadataExtractor.Directory> d)
        {
            this.FileName = fi;
            Directories = d;
        }
    }

     public class ISO
     {
        public uint minISO { get; set; }
        public uint maxISO { get; set; }
        public ISO(uint min, uint max)
        {
            minISO = min;
            maxISO = max;
        }   
    }

    public class ExposureTime
    {
        public Rational minET { get; set; }
        public Rational maxET { get; set; }
        public ExposureTime(Rational min, Rational max)
        {
            minET = min;
            maxET = max;
        }
    }


    public class Date
    {
        public DateTime minDT { get; set; }
        public DateTime maxDT { get; set; }
        public Date(DateTime min, DateTime max)
        {
            minDT = min;
            maxDT = max;
        }
    }

}