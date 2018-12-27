using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MetadataExtractor.Formats.Exif;
using MetadataExtractor;
using Directory = System.IO.Directory;
using System.Threading;
using System.Diagnostics;
namespace WhereIsMyPhoto_Sorter
{
    class Sorter
    {
        public event SorterFileChanged sorterFileChangedEvent;
        public event SorterMetadataError sorterMetadataErrorEvent;
        public string SourceDirectory { get; private set; }
        public string DestinationDirectory { get; private set; }
        public Params Parameters { get; private set; }
        public Sorter(string sourceDirectory, string destinationDirectory, Params parameters)
        {
            SourceDirectory = sourceDirectory;
            DestinationDirectory = destinationDirectory;
            Parameters = parameters;
        }

        public async Task StartTaskSort(CancellationToken token)
        {
            await Task.Run(()=>Sort(SourceDirectory,token), token);
        }

        private static IEnumerable<string> DestinationFileNames(string path)
        {
            yield return path;


            string dir = Path.GetDirectoryName(path);
            string file = Path.GetFileNameWithoutExtension(path);
            string ext = Path.GetExtension(path);

            yield return Path.Combine(dir, file + "_(1)" + ext);
            for (int i = 2; ;i++)
            {
                yield return Path.Combine(dir, file + "_(" + i + ")" + ext);
            }
        }

        private DateTime GetDateTimeForImage(string fileName)
        {
            DateTime dateTime = new DateTime();
            var dirs = MetadataExtractor.ImageMetadataReader.ReadMetadata(Path.GetFullPath(fileName));
            var subdir = dirs.OfType<ExifSubIfdDirectory>().FirstOrDefault();
            if (subdir.ContainsTag(ExifDirectoryBase.TagDateTimeOriginal))
            {
                 dateTime = subdir.GetDateTime(ExifDirectoryBase.TagDateTimeOriginal);
            }
            return dateTime;
        }

        public void Sort(string path, CancellationToken token)
        {
            Trace.WriteLine("Вход в папку " + path);
            int count = 0;
            string[] dirs = null;
            string[] files = null;
            try
            {
                /* string[]*/
                dirs = System.IO.Directory.GetDirectories(path);
                /* string[]*/
                files = System.IO.Directory.GetFiles(path, "*.*").Where((s) => s.ToLower().EndsWith(".jpg") || s.ToLower().EndsWith(".jpeg")).ToArray();
            }
            catch (IOException ioe)
            {
                Trace.WriteLine(ioe.Message);
                //выясним, не извлекли ли съемный диск

                DriveInfo drive = new DriveInfo(System.IO.Path.GetPathRoot(path));
                if (!drive.IsReady)
                {
                    Trace.WriteLine("Диск " + drive.Name + " был извлечен, или содержит ошибки, мешаюшие поиску");
                    return;/*result*/;
                }
                if (dirs == null || files == null)
                {
                    Trace.WriteLine("Массивы со списком файлов или папок - null");
                    return; /*result*/;
                }
            }

            foreach(var f in files)
            {
                if(token.IsCancellationRequested)
                    break;
                try
                {
                    DateTime fileDateTime = GetDateTimeForImage(f);
                    string result_directory = "";
                    if (Parameters.format == DestinationDirectoriesFormat.YearMonth)
                        result_directory = fileDateTime.ToString("yyyy_MM");
                    else
                        result_directory = fileDateTime.ToString("yyyy_MM_dd");

                    if (!Directory.Exists(Path.Combine(DestinationDirectory, result_directory))) 
                        Directory.CreateDirectory(Path.Combine(DestinationDirectory, result_directory));

                    //--------------------------------------------------------
                    string dstname = Path.Combine(DestinationDirectory, result_directory, Path.GetFileName(f));
                    foreach (string dname in DestinationFileNames(dstname))
                    {
                        if(!File.Exists(dname))
                        {
                            if (Parameters.mode == FileOperations.Copy)
                            {
                                File.Copy(f, dname);
                            }
                            else
                            {
                                File.Move(f, dname);
                            }
                            break;
                        }
                    }
                    sorterFileChangedEvent?.Invoke(new SortedEventArgs(f, fileDateTime.Date.ToShortDateString(), Path.Combine(DestinationDirectory, result_directory)));
                    //--------------------------------------------------------
                }
                catch (UnauthorizedAccessException ex)
                {
                    Trace.WriteLine("Windows Access Error : " + ex.Message);
                }
                catch (IOException ex)
                {
                    Trace.WriteLine("I/O Error : " + ex.Message);
                }
                catch(MetadataException ex)
                {
                    Trace.WriteLine("Date Extraction Error: "+ ex.Message);
                    sorterMetadataErrorEvent?.Invoke(new SorterErrorEventArgs(f));
                }
                catch (NullReferenceException ex)
                {
                    Trace.WriteLine("NRE: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Trace.WriteLine("Unknow error: " + ex.Message);
                }

            }

            foreach (var d in dirs)
            {
                if (token.IsCancellationRequested)
                    break;
                try
                {
                    Sort(d, token);
                }
                catch (UnauthorizedAccessException ex)
                {
                    Trace.WriteLine("Windows Access Error : " + ex.Message);
                }
                catch (IOException ex)
                {
                    Trace.WriteLine("I/O Error : " + ex.Message);
                }
            }
            return;
        }
    }
}
