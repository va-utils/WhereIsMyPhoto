﻿using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using MetadataExtractor.Formats.FileSystem;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WhereIsMyPhoto
{
    public class Search
    {
        public List<ImageInformation> ImageList { get; private set; }

        private string path;

        bool allDrives;
        bool systemDirectoryScan;

        bool windirSkipFlag;

        public Search(bool sysdirscan = true)
        {
            systemDirectoryScan = sysdirscan;
            allDrives = true;
        }

        public Search(string path, bool sysdirscan = true)
        {
            systemDirectoryScan = sysdirscan;
            this.path = path;
            ImageList = new List<ImageInformation>(50);
        }

        public event SearchChangeFolderEventHandler ChangeFolder;
        public event SearchNewFileEventHandler NewFile;
        public event EventHandler IOFatalError;

        /* главная функция поиска всех изображений по заданному пути */
        public async Task<List<ImageInformation>> GetImagesWithMyParams(ISO iso, Date date, ExposureTime et, ExposureProgram ep, string camName, bool isManualWhiteBalance, bool isFlash, bool isGeo, bool isEdit, CancellationToken token)
        {
            if(!allDrives)
                return await Task.Run(() => ImageList = GetImagesUponMyCriteria(path, iso, date, et, ep, camName, isManualWhiteBalance, isFlash, isGeo, isEdit, token), token);
            else
            {
                DriveInfo[] readyDrives = DriveInfo.GetDrives().Where((d)=>d.IsReady).ToArray();
                ImageList = new List<ImageInformation>(50);
                return await Task.Run(() =>
                {
                    foreach(var d in readyDrives)
                    {
                        if (d.IsReady)
                            ImageList.AddRange(GetImagesUponMyCriteria(d.RootDirectory.FullName, iso, date, et, ep, camName, isManualWhiteBalance, isFlash, isGeo, isEdit, token));
                        else
                            Trace.WriteLine("Диск " + d.Name + " был доступен в начале сканирования, но оказался недоступен в дальнейшем. Не сканировался.");
                    }
                    return ImageList;
                });
            }
        }

        public static string GetInformation(ImageInformation img)
        {
            Debug.Assert(img != null, "Передано пустое значение в GetInformation();");
            Debug.Assert(string.IsNullOrWhiteSpace(img.FileName) == false, "Передано пустое имя файла в структуре CompleteInfoEXIF в GetInformation()");

            string result = string.Empty;
            StringBuilder sb = new StringBuilder();


            //ISO
            var subdir = img.Directories.OfType<ExifSubIfdDirectory>().FirstOrDefault();
            if(subdir!=null)
            {
                //---------------------------------------------------------------------------------------------------
                //Дата и время
                try
                {
                    if(subdir.ContainsTag(ExifDirectoryBase.TagDateTimeOriginal))
                    {
                        DateTime dtdate = subdir.GetDateTime(ExifDirectoryBase.TagDateTimeOriginal);
                        if (dtdate != null)
                            sb.AppendLine("Снимок сделан: " + dtdate.ToString("f"));
                    }
                }
                catch(MetadataException me)
                {
                    Trace.WriteLine(img.FileName + " " + me.Message);
                }

                string iso = subdir.GetDescription(ExifDirectoryBase.TagIsoEquivalent);
                if (iso != null)
                    sb.AppendLine("ISO: " + iso);


                //Вспышка
                try
                {
                    if(subdir.ContainsTag(ExifDirectoryBase.TagFlash))
                    {
                        string sFlash = subdir.GetDescription(ExifDirectoryBase.TagFlash);
                        ushort iFlash = subdir.GetUInt16(ExifDirectoryBase.TagFlash);
                        sb.AppendLine("Вспышка: " + (((iFlash & (0x1)) == 1) ? "Использовалась. " + sFlash : "Не использовалась.")); ;
                    }
                }
                catch(MetadataException me)
                {
                    Trace.WriteLine(img.FileName + " " + me.Message);
                }

                //программа экспозиции
                try
                {
                    if(subdir.ContainsTag(ExifDirectoryBase.TagExposureProgram))
                    {
                        ushort eProgram = subdir.GetUInt16(ExifDirectoryBase.TagExposureProgram);
                        switch (eProgram)
                        {
                            case 1:
                                sb.AppendLine("Программа экспозиции: ручной");
                                break;
                            case 2:
                                sb.AppendLine("Программа экспозиции: программный");
                                break;
                            case 3:
                                sb.AppendLine("Программа экспозиции: приоритет диафрагмы");
                                break;
                            case 4:
                                sb.AppendLine("Программа экспозиции: приоритет выдержки");
                                break;
                            case 5:
                                sb.AppendLine("Программа экспозиции: творческий режим");
                                break;
                            case 6:
                                sb.AppendLine("Программа экспозиции: скорость/спорт");
                                break;
                            case 7:
                                sb.AppendLine("Программа экспозиции: портретный режим");
                                break;
                            case 8:
                                sb.AppendLine("Программа экспозции: пейзажный/ландшафтный режим");
                                break;
                        }
                    }      
                }
                catch(MetadataException me)
                {
                    Trace.WriteLine(img.FileName + " " + me.Message);
                }

                //Баланс белого
                try
                {
                    if(subdir.ContainsTag(ExifDirectoryBase.TagWhiteBalanceMode))
                    {
                        ushort whiteBalance = subdir.GetUInt16(ExifDirectoryBase.TagWhiteBalanceMode);
                        sb.AppendLine("Баланс белого: " + ((whiteBalance == 1) ? "Ручная настройка" : "Автоматически"));
                        if (whiteBalance == 1)
                        {
                            string sLightSource = subdir.GetDescription(ExifDirectoryBase.TagWhiteBalance);
                            if (sLightSource != null)
                                sb.AppendLine("Ручной баланс белого: " + sLightSource);
                        }
                    }
                }
                catch (MetadataException me)
                {
                    
                    Trace.WriteLine(img.FileName + " " + me.Message);
                }

                //выдержка

                if(subdir.ContainsTag(ExifDirectoryBase.TagExposureTime))
                {
                    string sExposureTime = subdir.GetDescription(ExifDirectoryBase.TagExposureTime);
                    if (sExposureTime != null)
                    {
                        sb.AppendLine("Выдержка: " + sExposureTime);
                    }
                }


                //  var sdir = img.dirs.OfType<ExifIfd0Directory>().FirstOrDefault();
                try
                {
                    var subifd0dir = img.Directories.OfType<ExifIfd0Directory>().FirstOrDefault();
                    if(subifd0dir!=null && subifd0dir.ContainsTag(ExifDirectoryBase.TagMake))
                    {
                        string cameraModel = subifd0dir.GetDescription(ExifDirectoryBase.TagMake);
                        if (cameraModel != null)
                        {
                            sb.AppendLine("Камера: " + cameraModel);
                        }
                    }
                }
                catch(MetadataException me)
                {
                    Trace.WriteLine(img.FileName + " " + me.Message);
                }

               // Rational exposureTime = subdir.GetRational(ExifDirectoryBase.TagExposureTime);
               // exposureTime.
                //Геолокация
                var gpsdir = img.Directories.OfType<GpsDirectory>().FirstOrDefault();

                if (gpsdir != null)
                {
                    GeoLocation gl = gpsdir.GetGeoLocation();
                    if(gl!=null)
                        sb.AppendLine("Геолокация (координаты): " + "Долгота: " + gl.Longitude + " Широта: " + gl.Latitude);
                }
                else
                {
                    sb.AppendLine("Геолокация (координаты): Нет информации");
                }

            }

            //был ли редактирован
            try
            {
                var IfdDirectory = img.Directories.OfType<ExifIfd0Directory>().FirstOrDefault();
                if(IfdDirectory!=null && IfdDirectory.ContainsTag(ExifIfd0Directory.TagSoftware))
                {
                    string softWare = IfdDirectory?.GetDescription(ExifDirectoryBase.TagSoftware);
                    sb.AppendLine("Программа: " + softWare);
                }
            }
            catch(Exception ex)
            {
                Trace.WriteLine(img.FileName + " " + ex.Message);
            }


            //Сведения о файле:

            try
            {
                var subfiledir = img.Directories.OfType<FileMetadataDirectory>().FirstOrDefault();
                if(subfiledir!=null && subfiledir.ContainsTag(FileMetadataDirectory.TagFileModifiedDate))
                {
                    sb.AppendLine("Последний раз файл изменялся: " + subfiledir.GetDateTime(FileMetadataDirectory.TagFileModifiedDate).ToString("f"));
                }
            }
            catch(Exception ex)
            {
                Trace.WriteLine(img.FileName + " " + ex.Message);
            }

               
            return sb.ToString();
        }




        //---bool-версия 27.08.18
        public bool isMatch(string fileName, IReadOnlyList<MetadataExtractor.Directory> imgExif, ISO iso, Date date, ExposureTime et, ExposureProgram ep, string camName, bool isManualWhiteBalance, bool isFlash, bool isGeo,bool isEdit/* CancellationToken token*/)
        {

            if (iso == null && date == null && et == null && camName == null && isManualWhiteBalance == false && isFlash == false && isGeo == false && isEdit==false && ep == ExposureProgram.Any)
                return true; // критериев нет, подойдет любое

            var subdir = imgExif.OfType<ExifSubIfdDirectory>().FirstOrDefault();

            if (subdir == null) //
            {
                return false;
            }

            if (iso != null) //ISO 
            {

                Debug.Assert(iso.minISO <= iso.maxISO, "Минимальное ISO больше максимального");

                try
                {
                    if (!subdir.ContainsTag(ExifDirectoryBase.TagIsoEquivalent))
                        return false;

                    ushort iIso = subdir.GetUInt16(ExifDirectoryBase.TagIsoEquivalent);

                    if (!((iIso >= iso.minISO) && (iIso <= iso.maxISO)))
                    {
                            return false;
                    }
                }
                catch (MetadataException me)
                {
                    Trace.WriteLine(fileName + " " + me.Message);
                    return false;
                }
            }

            if (et != null) //время выдержки
            {
                try
                {
                    if (!subdir.ContainsTag(ExifDirectoryBase.TagExposureTime))
                        return false;

                    Rational expTime = subdir.GetRational(ExifDirectoryBase.TagExposureTime);
                    double dExpTime = expTime.ToDouble();
                    double dMinET = et.minET.ToDouble();
                    double dMaxET = et.maxET.ToDouble();
                    Debug.Assert(dMinET <= dMaxET, "Минимальная выдержка больше минимальной");
                    if (!((dExpTime >= dMinET) && (dExpTime <= dMaxET)))
                    {
                        return false;
                    }
                }
                catch (MetadataException me)
                {
                    Trace.WriteLine(fileName + " " + me.Message);
                    return false;
                }
            }

            if(ep!=ExposureProgram.Any)
            {
                try
                {
                    ushort eProgram = subdir.GetUInt16(ExifDirectoryBase.TagExposureProgram);
                    if (eProgram != (ushort)ep)
                        return false;
                }
                catch(MetadataException me)
                {
                    Trace.WriteLine(fileName + " " + me.Message);
                    return false;
                }
            }

            if (camName!=null) //камера
            {
                Debug.Assert(!string.IsNullOrWhiteSpace(camName), "Пришло пустое имя камеры");
                    
                try
                {
                    var subifd0dir = imgExif.OfType<ExifIfd0Directory>().FirstOrDefault();
                    if (subifd0dir == null || !subifd0dir.ContainsTag(ExifIfd0Directory.TagMake))
                        return false;

                    string cam = subifd0dir.GetDescription(ExifIfd0Directory.TagMake).ToLower();
                    if(!(cam.Contains(camName.ToLower())))
                    {
                        return false;
                    }
                }
                catch(Exception e)
                {
                     Trace.WriteLine(fileName + " " + e.Message);
                     return false;
                }
            }

            if (date != null) //дата
            {
                Debug.Assert(date.minDT < date.maxDT, "Конечная дата раньше начальной");

                try
                {
                    if (!subdir.ContainsTag(ExifDirectoryBase.TagDateTimeOriginal))
                        return false;

                    DateTime dDate = subdir.GetDateTime(ExifDirectoryBase.TagDateTimeOriginal);
                    if (!((dDate >= date.minDT) && (dDate <= date.maxDT)))
                    {
                        return false;
                    }
                }
                catch (MetadataException me)
                {
                    Trace.WriteLine(fileName + " " + me.Message);
                    return false;
                }
            }

            if (isManualWhiteBalance) //баланс белого
            {
                try
                {
                    if (!subdir.ContainsTag(ExifDirectoryBase.TagWhiteBalanceMode))
                        return false;

                    ushort whiteBalance = subdir.GetUInt16(ExifDirectoryBase.TagWhiteBalanceMode);
                    if (!(whiteBalance == 1))
                        return false;
                }
                catch (MetadataException me)
                {
                    Trace.WriteLine(fileName + " " + me.Message);
                    return false;
                }
            }

            if (isFlash) //наличие вспышки
            {
                try
                {
                    if (!subdir.ContainsTag(ExifDirectoryBase.TagFlash))
                        return false;

                    ushort iFlash = subdir.GetUInt16(ExifDirectoryBase.TagFlash);
                    if (!((iFlash & 0x1) == 1))
                            return false;
                }
                catch (MetadataException me)
                {
                    Trace.WriteLine(fileName + " " + me.Message);
                    return false;
                }
            }

            if (isGeo) //наличие геометки
            {
                var gpsdir = imgExif.OfType<GpsDirectory>().FirstOrDefault();

                if (gpsdir == null)
                {
                    return false;
                }
                else
                {
                    if (gpsdir.GetGeoLocation() == null)
                        return false;
                }
            }

            if(isEdit) //редактирован 
            {
                try
                {
                    
                    var Ifd0Directory = imgExif.OfType<ExifIfd0Directory>().FirstOrDefault();
                    if (Ifd0Directory == null || !Ifd0Directory.ContainsTag(ExifDirectoryBase.TagSoftware))
                        return false;

                    string softWare = Ifd0Directory?.GetDescription(ExifDirectoryBase.TagSoftware);
                    string[] soft = { "gimp", "photoshop", "paint" };
                    if(softWare==null)
                    {
                        return false;
                    }
                    else
                    {
                        bool edit_flag = false;
                        foreach (var s in soft)
                        {  
                            if(softWare.ToLower().Contains(s) == true)
                            {
                                edit_flag = true;
                                continue;
                            }
                        }
                        if(edit_flag == false)
                        {
                            return false;
                        }
                    }
                }
                catch(MetadataException me)
                {
                    Trace.WriteLine(fileName + " " + me.Message);
                    return false;
                }
            }
            return true;
        }

        private List<ImageInformation> GetImagesUponMyCriteria(string path, ISO iso, Date date, ExposureTime et, ExposureProgram ep, string camName, bool isManualWhiteBalance, bool isFlash, bool isGeo, bool isEdit, CancellationToken token)
        {
            Debug.Assert(String.IsNullOrWhiteSpace(path) == false, "Передана пустая  строка в GetAllImages()");
            List<ImageInformation> result = new List<ImageInformation>();
            string[] dirs = null;
            string[] files = null;
            try
            {
               /* string[]*/ dirs = System.IO.Directory.GetDirectories(path);
               /* string[]*/ files = System.IO.Directory.GetFiles(path, "*.*").Where((s) => s.ToLower().EndsWith(".jpg") || s.ToLower().EndsWith(".jpeg")).ToArray();
            }
            catch(IOException ioe)
            {
                Console.WriteLine(ioe.Message);

                Trace.WriteLine(ioe.Message);
                //выясним, не извлекли ли съемный диск
                DriveInfo drive = new DriveInfo(System.IO.Path.GetPathRoot(path));
                if (!drive.IsReady)
                {
                    Console.WriteLine("Диск " + drive.Name + " был извлечен, или содержит ошибки, мешаюшие поиску");
                    Trace.WriteLine("Диск " + drive.Name + " был извлечен, или содержит ошибки, мешаюшие поиску");
                    return result;
                }
            }
            

            ChangeFolder.Invoke(new SearchChangeFolderEventArgs(path, files.Length));

            foreach (var f in files)
            {
                if (token.IsCancellationRequested)
                {
                    return result;
                }
                try
                {
                    var dirsPhoto = MetadataExtractor.ImageMetadataReader.ReadMetadata(Path.GetFullPath(f));
                    if(isMatch(f,dirsPhoto,iso,date,et,ep, camName, isManualWhiteBalance,isFlash,isGeo,isEdit))
                    {
                        ImageInformation ciexif = new ImageInformation(f, dirsPhoto);
                        result.Add(ciexif);
                        NewFile?.Invoke(new SearchNewFileEventArgs(ciexif));
                    }
                }
                catch (ImageProcessingException ipe)
                {
                    Console.WriteLine(ipe.Message);
                    Trace.WriteLine( ipe.Message);
                }
                catch (IOException ioe)
                {
                    Console.WriteLine(ioe.Message);
                    
                    Trace.WriteLine(ioe.Message);
                    //выясним, не извлекли ли съемный диск
                    DriveInfo drive = new DriveInfo(System.IO.Path.GetPathRoot(f));
                    if(!drive.IsReady)
                    {
                        Console.WriteLine("Диск " + drive.Name + " был извлечен, или содержит ошибки, мешаюшие поиску");
                        Trace.WriteLine("Диск " + drive.Name + " был извлечен, или содержит ошибки, мешаюшие поиску");
                        IOFatalError?.Invoke(this, new EventArgs());
                        return result;
                    }
                
                }
                catch (UnauthorizedAccessException uae)
                {
                    Trace.WriteLine( uae.Message);
                }
            }

            foreach (var d in dirs)
            {
                if ((windirSkipFlag==false) & (systemDirectoryScan==false))
                {
                    if(d.Contains(Environment.GetFolderPath(Environment.SpecialFolder.Windows)))
                    {
                        Trace.WriteLine("Пропуск системного каталога...");
                        windirSkipFlag = true;
                        continue;
                    }
                }

                if (token.IsCancellationRequested)
                {
                    return result;
                }
                try
                {
                    result.AddRange(GetImagesUponMyCriteria(d,iso,date,et,ep, camName, isManualWhiteBalance,isFlash,isGeo,isEdit, token));
                }
                catch (UnauthorizedAccessException uae)
                {
                    Trace.WriteLine(uae.Message);
                }
            }

            return result;
        }
    }
}