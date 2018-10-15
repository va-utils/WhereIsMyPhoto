using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhereIsMyPhoto
{
    public partial class MainForm : Form
    {
        [DllImport("user32")]
        private static extern bool HideCaret(IntPtr hWnd);

        [StructLayout(LayoutKind.Sequential)]
        public struct FLASHWINFO
        {
            [MarshalAs(UnmanagedType.U4)]
            public int cbSize;
            public IntPtr hwnd;
            [MarshalAs(UnmanagedType.U4)]
            public int dwFlags;
            [MarshalAs(UnmanagedType.U4)]
            public int uCount;
            [MarshalAs(UnmanagedType.U4)]
            public int dwTimeout;
        }

        [DllImport("user32")]
        public static extern bool FlashWindowEx([MarshalAs(UnmanagedType.Struct)]ref FLASHWINFO pwfi);

        bool isWorking;

        StringBuilder searchSettings = new StringBuilder(200);

        CancellationTokenSource cancelTokenSource;
        CancellationToken token;

        List<ImageInformation> images = new List<ImageInformation>();
        BindingSource imagesBindingSource;
        Search finder;
       // System.IO.FileStream forTraceFileStream;



        private void OpenFileMenu_Click(object sender, EventArgs e)
        {
            OpenImage();
        }

        private void OpenDirectoryMenu_Click(object sender, EventArgs e)
        {
            if (filesListBox.SelectedIndex == -1) return;
            try
            {
                Debug.Assert(!(String.IsNullOrWhiteSpace(images[filesListBox.SelectedIndex].FileName)), "Пустое имя файла для открытия в просмотрщике");
                //  string directoryPath = System.IO.Path.GetDirectoryName(images[filesListBox.SelectedIndex].FileName);
                Process.Start(new ProcessStartInfo("explorer.exe", " /select, " + images[filesListBox.SelectedIndex].FileName));
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }

        private void OpenMapMenu_Click(object sender, EventArgs e)
        {

        }

        public MainForm()
        {
            //--настройка вывода информации для тестирования ---//
            //System.IO.File.Create("whereismyphoto.log.txt");

            //--------------------------------------------------//
            InitializeComponent();
            imagesBindingSource = new BindingSource
            {
                DataSource = images
            };

            
           
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            contextMenuStrip.Items.Insert(0, new ToolStripLabel("..."));
            contextMenuStrip.Items[0].ForeColor = Color.DarkBlue;

            this.Text = Application.ProductName + " " + Application.ProductVersion;
            endDateTimePicker.Value = endDateTimePicker.MaxDate = DateTime.Now.Date;

            filesListBox.DataSource = imagesBindingSource;
            filesListBox.DisplayMember = "FileName";

            if(Properties.Settings.Default.isFirstStart)
            {
                HelpAndInformation.ShowHelpFile();
                Properties.Settings.Default.isFirstStart = false;
                Properties.Settings.Default.Save();
            }

        }

        private void files_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(filesListBox.SelectedIndex!=-1)
            {
                this.imageInformationTextBox.Text = Search.GetInformation(images[filesListBox.SelectedIndex]);
             //   CheckGeoLocationForMenu();
            }
        }

        private void CheckGeoLocationForMenu(int index)
        {
            GeoLocation rgl = Search.GetGPSInformation(images[index]);
            if (rgl != null)
            {
                contextMenuStrip.Items[3].Visible = true;
            }
            else
            {
                contextMenuStrip.Items[3].Visible = false;
            }
        }

        private void ChangeStatus(SearchChangeFolderEventArgs fcfea)
        {
            if (statusStrip.InvokeRequired)
            {
                statusStrip.Invoke(new Action<SearchChangeFolderEventArgs>(ChangeStatus), fcfea);
            }
            else
            {
                status.Text = "В " + fcfea.FolderName + "  " + fcfea.FilesCount + " изобр. для анализа"; 
            }
        }

        private void newFileFinded(SearchNewFileEventArgs fnfea)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<SearchNewFileEventArgs>(newFileFinded), fnfea);
            }
            else
            {
                imagesBindingSource.Add(fnfea.file);
            }
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    pathTextBox.Text = dialog.SelectedPath;
                }
            }
        }

        private bool GetRationalFromString(string expString, out Rational rational)
        {
            string[] elems = expString.Split(new char[] { '/' },2);
            try
            {
                long numerator = long.Parse(elems[0]);
                long denominator;
                if (elems.Length != 1)
                {
                    denominator = long.Parse(elems[1]);
                    if (denominator == 0)
                    {
                        rational = new Rational();
                        return false;
                    }
                }
                   
                else
                    denominator = 1;

                rational = new Rational(numerator, denominator);
                return true;
            }
            catch(FormatException fe)
            {
                Trace.WriteLine(fe.Message);
                rational = new Rational();
                return false;
            }
        }

        private void Stop(object sender, EventArgs e)
        {
            cancelTokenSource.Cancel();
        }

        private async void StartSearch(object sender, EventArgs e)
        {
            if(System.IO.Directory.Exists(pathTextBox.Text) || allDrivesCheckBox.Checked)
            {   
                //формирование критериев поиска
                Date date = null;
                ISO iso = null;
                ExposureProgram ep = ExposureProgram.Any;
                Orientation or = Orientation.Any;
                ExposureTime et = null;
                string camName = null;
                bool isManualWhiteBalance = false;
                bool isFlashOn = false;
                bool isGPS = false;
                bool isEdit = false;
                //---------------------------------


                searchSettings.Clear();

                

                //------ДАТА
                if (datesCheckBox.Checked == true)
                {
                    if(endDateTimePicker.Value.Date < startDateTimePicker.Value.Date)
                    {
                        MessageBox.Show("Неверно выставлены настройки поиска по дате. Пожалуйста исправьте.");
                        return;
                    }
                    searchSettings.AppendLine("Поиск по датам: " + startDateTimePicker.Value.ToShortDateString() + " - " + endDateTimePicker.Value.ToShortDateString());
                    date = new Date(startDateTimePicker.Value.Date, endDateTimePicker.Value.Date);
                }
                //-----ISO
                if(ISOCheckBox.Checked)
                {
                    uint minISO;
                    uint maxISO;
                    bool isMinISOCorrect = uint.TryParse(minISOTextBox.Text, out minISO);
                    bool isMaxISOCorrect = uint.TryParse(maxISOTextBox.Text, out maxISO);
                    if (isMinISOCorrect == true && isMaxISOCorrect == true)
                    {
                        if (maxISO < minISO)
                        {
                            MessageBox.Show("Неверно выставлены настройки поиска по ISO. Пожалуйста исправьте.");
                            return;
                        }
                        if (maxISO > uint.MaxValue)
                        {
                            MessageBox.Show("Неверно выставлены настройки поиска по ISO. Пожалуйста исправьте.");
                            return;
                        }

                        searchSettings.AppendLine("Поиск по ISO: " + minISO + " - " + maxISO);
                        iso = new ISO(minISO, maxISO);
                    }
                    else
                    {
                        MessageBox.Show("Неверно выставлены настройки поиска по ISO. Пожалуйста исправьте.");
                        return;
                    }
                }
                //-----ВЫДЕРЖКА
                if(ExposureTimeCheckBox.Checked)
                {
                    Rational minExposureTime;
                    Rational maxExposureTime;
                    bool isMinValueCorrect = GetRationalFromString(minExposureTimeTextBox.Text, out minExposureTime);
                    bool isMaxValueCorrect = GetRationalFromString(maxExposureTimeTextBox.Text, out maxExposureTime);
                    if (isMinValueCorrect==true && isMaxValueCorrect==true)
                    {
                        //теперь проверим на нижние и верхние границы

                        double min = minExposureTime.ToDouble();
                        double max = maxExposureTime.ToDouble();

                        if( ! (min <= max + double.Epsilon))
                        {
                            MessageBox.Show("Неверно выставлены настройки поиска по выдержке. Пожалуйста исправьте.");
                            return;
                        }
                        searchSettings.AppendLine("Поиск по выдержке: " + minExposureTime.ToSimpleString() + " - " + maxExposureTime.ToSimpleString());
                        et = new ExposureTime(minExposureTime, maxExposureTime);
                    }
                    else
                    {
                        MessageBox.Show("Неверно выставлены настройки поиска по выдержке. Пожалуйста исправьте.");
                        return;
                    }
                }
                //---МОДЕЛЬ КАМЕРЫ
                
                if(cameraCheckBox.Checked)
                {
                    if(!string.IsNullOrWhiteSpace(CameraTextBox.Text))
                    {
                        camName = CameraTextBox.Text;
                        searchSettings.AppendLine("Поиск по модели камеры: " + camName);
                    }
                    else
                    {
                        MessageBox.Show("Неверно выставлены настройки поиска по производителю камеры. Пожалуйста исправьте.");
                        return;
                    }
                }

                if(WhiteBalanceСheckBox.Checked)
                {
                    isManualWhiteBalance = true;
                    searchSettings.AppendLine("Поиск снимков с ручным балансом белого");
                }

                if(flashOnCheckBox.Checked)
                {
                    isFlashOn = true;
                    searchSettings.AppendLine("Поиск снимков со вспышкой");
                }

                if(gpsCheckBox.Checked)
                {
                    isGPS = true;
                    searchSettings.AppendLine("Поиск снимков с геоданными");
                }
                if(EditCheckBox.Checked)
                {
                    isEdit = true;
                    searchSettings.AppendLine("Поиск редактированных снимков");
                }
                if(ExposureProgramCheckBox.Checked)
                {
                    int sel = exposureProgramComboBox.SelectedIndex;
                    if(sel==-1)
                    {
                        MessageBox.Show("Неверно выставлены настройки поиска по программе управления экспозицией. Пожалуйста исправьте.");
                        return;
                    }
                    ep = (ExposureProgram)exposureProgramComboBox.SelectedIndex+1;
                }

                if(orientationСheckBox.Checked)
                {
                    int sel = orientationComboBox.SelectedIndex;
                    if(sel==-1)
                    {
                        MessageBox.Show("Неверно выставлены настройки поиска по ориентации. Пожалуйста исправьте.");
                        return;
                    }
                    switch(sel)
                    {
                        case 0:
                            or = Orientation.Horizonatal;
                            break;
                        case 1:
                            or = Orientation.Vertical;
                            break;
                    }
                }
                
                if(allDrivesCheckBox.Checked)
                {
                    searchSettings.AppendLine("Поиск изображений: весь компьютер");
                    finder = new Search(!skipWinDirectoryCheckBox.Checked);
                }
                else
                {
                    searchSettings.AppendLine("Поиск изображений в: " + pathTextBox.Text);
                    finder = new Search(pathTextBox.Text,!skipWinDirectoryCheckBox.Checked);
                }

                finder.ChangeFolder += ChangeStatus;
                finder.NewFile += newFileFinded;
                status.Text = "Выполняется поиск...";

                //---кнопка меняется на "стоп"
                startButton.Text = "Остановить поиск";
                startButton.Click -= StartSearch;
                startButton.Click += Stop;

                searchToolStripMenuItem.Text = "Остановить поиск";
                searchToolStripMenuItem.Click -= StartSearch;
                searchToolStripMenuItem.Click += Stop;


                imagesBindingSource.Clear();
                imageInformationTextBox.Clear();

                //---пуск задачи---//
                cancelTokenSource = new CancellationTokenSource();
                token = cancelTokenSource.Token;
                

                isWorking = true;
                await finder.GetImagesWithMyParams(iso, date, et, ep, or, camName, isManualWhiteBalance, isFlashOn, isGPS, isEdit, token);
                isWorking = false;

               // MessageBox.Show(searchSettings.ToString() + "\nНайдено изображений: " + imagesBindingSource.Count);
                //------------------//
                status.Text = "Поиск окончен";
               // imagesBindingSource.DataSource = images;

                //---кнопка меняется на старт
                startButton.Text = "Начать поиск";
                startButton.Click += StartSearch;
                startButton.Click -= Stop;

                searchToolStripMenuItem.Text = "Начать поиск";
                searchToolStripMenuItem.Click += StartSearch;
                searchToolStripMenuItem.Click -= Stop;

                if(WindowState == FormWindowState.Minimized)
                {
                    //мигание значка на панели задач, когда поиск завершен (если окно свернуто)
                    FLASHWINFO fwi = new FLASHWINFO();
                    fwi.cbSize = Marshal.SizeOf(fwi);
                    fwi.hwnd = Handle;
                    fwi.dwFlags = 0x00000002;
                    fwi.dwTimeout = 0;
                    fwi.uCount = 5;
                    FlashWindowEx(ref fwi);
                }

#if DEBUG
                #region Tests
                Console.WriteLine("Tests...");
                //---тестируем поиск по датам, удалить в релизе
                if (datesCheckBox.Checked)
                {
                    foreach (var i in images)
                    {
                        var sdir = ((ImageInformation)i).Directories.OfType<ExifSubIfdDirectory>().FirstOrDefault();
                        var dbdate = sdir.GetDateTime(ExifDirectoryBase.TagDateTimeOriginal).Date;
                        Debug.Assert((dbdate >= startDateTimePicker.Value.Date) && (dbdate <= endDateTimePicker.Value.Date), "DateTime Bug: " + ((ImageInformation)i).FileName);
                    }
                }

                //---тестируем баланс белого (ручной)
                if(WhiteBalanceСheckBox.Checked)
                {
                    foreach(var i in images)
                    {
                        var sdir = i.Directories.OfType<ExifSubIfdDirectory>().FirstOrDefault();
                        var wb = sdir.GetUInt16(ExifDirectoryBase.TagWhiteBalanceMode);
                        Debug.Assert(wb == 1, "White Balance Bug: " + i.FileName);
                    }
                }

                //---тестируем модель камеры, удалить в релизе
                if (cameraCheckBox.Checked)
                {
                    foreach(var x in imagesBindingSource)
                    {
                        if(x is ImageInformation)
                        {
                            var sdir = ((ImageInformation)x).Directories.OfType<ExifIfd0Directory>().FirstOrDefault();
                            var cam = sdir.GetDescription(ExifIfd0Directory.TagMake);
                            cam = cam.ToLower();
                            Debug.Assert(cam.Contains(camName.ToLower()), "Неверно отработал поиск по камере" + ((ImageInformation)x).FileName);
                        }
                    }
                }

               
                //---тестируем выдержку, удалить в релизе
                if (ExposureTimeCheckBox.Checked)
                foreach(var i in imagesBindingSource)
                {
                    var sdir = ((ImageInformation)i).Directories.OfType<ExifSubIfdDirectory>().FirstOrDefault();

                    Rational minExposureTime;
                    Rational maxExposureTime;
                    bool isMinValueCorrect = GetRationalFromString(minExposureTimeTextBox.Text, out minExposureTime);
                    bool isMaxValueCorrect = GetRationalFromString(maxExposureTimeTextBox.Text, out maxExposureTime);
                    if(isMinValueCorrect == true && isMaxValueCorrect==true)
                    {
                        double min = minExposureTime.ToDouble();
                        double max = maxExposureTime.ToDouble();
                        double exptime = sdir.GetRational(ExifDirectoryBase.TagExposureTime).ToDouble();
                        Debug.Assert(  (exptime >= min - double.Epsilon) && (exptime <= max + double.Epsilon), "Exposure Bug: " + ((ImageInformation)i).FileName + " " + exptime);
                    }
                }

                //тестируем ISO
                if(ISOCheckBox.Checked)
                {
                    foreach(var i in images)
                    {
                        var sdir = i.Directories.OfType<ExifSubIfdDirectory>().FirstOrDefault();
                        var dbiso = sdir.GetUInt16(ExifDirectoryBase.TagIsoEquivalent);
                        Debug.Assert((dbiso >= ushort.Parse(minISOTextBox.Text)) && (dbiso <= ushort.Parse(maxISOTextBox.Text)), "ISO Bug: " + ((ImageInformation)i).FileName);
                    }
                }

                //тестируем вспышку
                if(flashOnCheckBox.Checked)
                {
                    foreach (var i in images)
                    {
                        var sdir = ((ImageInformation)i).Directories.OfType<ExifSubIfdDirectory>().FirstOrDefault();
                        ushort flash = sdir.GetUInt16(ExifDirectoryBase.TagFlash);
                        Debug.Assert((flash&0x1)!=0, "Flash Bug: " + i.FileName + "Flash value: " + flash);
                    }
                }

                //тестируем программу экспозиции
                if(ExposureProgramCheckBox.Checked)
                {
                    foreach(var i in images)
                    {
                        var sdir = ((ImageInformation)i).Directories.OfType<ExifSubIfdDirectory>().FirstOrDefault();
                        ushort eProgram = sdir.GetUInt16(ExifDirectoryBase.TagExposureProgram);
                        Debug.Assert(eProgram == (int)exposureProgramComboBox.SelectedIndex+1, "Exposure Program Bug: " + i.FileName + " EP Value: " + eProgram);
                    }
                }

                //тестируем ориентацию
                if (ExposureProgramCheckBox.Checked)
                {
                    foreach (var i in images)
                    {
                        var sdir = ((ImageInformation)i).Directories.OfType<ExifIfd0Directory>().FirstOrDefault();
                        ushort orient = sdir.GetUInt16(ExifDirectoryBase.TagOrientation);

                        if (or == Orientation.Horizonatal)
                        {
                            Debug.Assert(orient <= 4, "Orientation bug");
                        }
                        if (or == Orientation.Vertical)
                        {
                            Debug.Assert(orient > 4, "Orientation bug");
                        }
                    }
                }


                #endregion
#endif

            }
            else
            {
                MessageBox.Show("Путь указан неверно!","WhereIsMyPhoto");
            }
        }

        private void files_DoubleClick(object sender, EventArgs e)
        {
            //  Debug.Assert(files.SelectedIndex != -1, "Индекс -1");
            OpenImage();
        }

        private void OpenImage()
        {
            if (filesListBox.SelectedIndex == -1) return;
            try
            {
                Debug.Assert(!(String.IsNullOrWhiteSpace(images[filesListBox.SelectedIndex].FileName)), "Пустое имя файла для открытия в просмотрщике");
                Process.Start(images[filesListBox.SelectedIndex].FileName);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }

        private void ISOVerify(object sender, KeyPressEventArgs e) //проверка для поля ввода ISO
        {
            if (!(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void ExposureTimeVerify(object sender, KeyPressEventArgs e) //проверка для полей ввода выдержки
        {
            if (!(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar) || e.KeyChar == '/'))
            {
                e.Handled = true;
            }
        }

        private void imageInformation_MouseDown(object sender, MouseEventArgs e)
        {
            HideCaret(imageInformationTextBox.Handle);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isWorking == true)
            {
                DialogResult exitDialogResult = MessageBox.Show("Поиск еще не закончен. Прервать?", "WhereIsMyPhoto", MessageBoxButtons.OKCancel);
                if(exitDialogResult == DialogResult.OK)
                {
                    cancelTokenSource.Cancel();
                    DialogResult finallyExitDialogResult = MessageBox.Show("Закрыть программу?", "WhereIsMyPhoto", MessageBoxButtons.OKCancel);

                    if(finallyExitDialogResult!= DialogResult.OK)
                    {
                        e.Cancel = true;
                    }

                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void status_Click(object sender, EventArgs e)
        {

        }

        private void aboutButton_Click(object sender, EventArgs e)
        {
            AboutBox ab = new AboutBox();
            ab.ShowDialog();
        }

        private void allDrivesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            pathTextBox.Enabled = browseButton.Enabled = !allDrivesCheckBox.Checked;
        }

        private void mainMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void saveResultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(imagesBindingSource.Count!=0)
            {
                if(searchSettings.Length != 0)
                {
                    StringBuilder forTextFile = new StringBuilder(1500);
                    forTextFile.Append(searchSettings);
                    forTextFile.AppendLine("-------------");
                    forTextFile.AppendLine("Найдены:");
                    foreach (var f in imagesBindingSource)
                    {
                        forTextFile.AppendLine(((ImageInformation)f).FileName);
                    }
                    forTextFile.Append("-------------");
                    using (SaveFileDialog svd = new SaveFileDialog())
                    {
                        svd.InitialDirectory =Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                        svd.DefaultExt = "txt";
                        svd.Filter = "Текстовые файлы(*.txt)|*.txt";
                        DialogResult dr = svd.ShowDialog();
                        if(dr == DialogResult.OK)
                        {
                            System.IO.File.WriteAllText(svd.FileName, forTextFile.ToString());
                        }
                    }

                }
            }
            else
            {
                MessageBox.Show("Нет результатов поиска для сохранения","WhereIsMyPhoto");
            }
        }

        private void showHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
           HelpAndInformation.ShowHelpFile();
        }

        private void filesListBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int i = filesListBox.IndexFromPoint(e.X, e.Y);
                if (i != -1)
                {
                    filesListBox.SelectedIndex = i;
                    CheckGeoLocationForMenu(i);
                    contextMenuStrip.Items[0].Text = System.IO.Path.GetFileName(images[filesListBox.SelectedIndex].FileName);
                    contextMenuStrip.Show(MousePosition);
                }
            }
            if (e.Button == MouseButtons.Middle)
            {
                int i = filesListBox.IndexFromPoint(e.X, e.Y);
                if(i!=-1)
                {
                    filesListBox.SelectedIndex = i;
                    new Preview(images[filesListBox.SelectedIndex]).Show();
                }

            }
        }

        private void statusStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void openMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filesListBox.SelectedIndex == -1) return;
            try
            {
                Debug.Assert(!(String.IsNullOrWhiteSpace(images[filesListBox.SelectedIndex].FileName)), "Пустое имя файла для открытия в просмотрщике");
                
                GeoLocation gl = Search.GetGPSInformation(images[filesListBox.SelectedIndex]);
                if(gl!=null)
                {
                    string url = string.Format("https://www.openstreetmap.org/?mlat={0}&mlon={1}#map=17/{0}/{1}", gl.Latitude.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US")), gl.Longitude.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US")));
                    Process.Start(url);
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }

        private void filesListBox_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void MainForm_MaximumSizeChanged(object sender, EventArgs e)
        {

        }
    }
}
