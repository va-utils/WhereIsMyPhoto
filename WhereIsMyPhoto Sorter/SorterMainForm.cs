using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhereIsMyPhoto_Sorter
{
    public partial class SorterMainForm : Form
    {
        CancellationTokenSource cancellationTokenSource;//= new CancellationTokenSource();
        CancellationToken token;//= cancellationTokenSource.Token;
        public SorterMainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // panel1.BringToFront();
            fileOperationsComboBox.SelectedIndex = 0;
            destinationComboBox.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                if(dialog.ShowDialog() == DialogResult.OK)
                {
                    sourceDirectoryTextBox.Text = dialog.SelectedPath;
                } 
            }
        }
        
        private void SorterStatus(SortedEventArgs sea)
        {
            if(InvokeRequired)
            {
                Invoke(new Action<SortedEventArgs>(SorterStatus), sea);
            }
            else
            {
                statusLabel.Text = "Обработан: " + sea.CurrectFile + " от " + sea.Date;
                logListBox.Items.Add(sea.CurrectFile + " -> " + sea.DestinationDirectory);
            }
        }

        private void destDirectoryButton_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                     destDirectoryTextBox.Text = dialog.SelectedPath;
                }
            }
        }


        private void Stop(object sender, EventArgs e)
        {
            Trace.WriteLine("Нажата кнопка остановки");
            cancellationTokenSource.Cancel();
        }


        private async void startButton_Click(object sender, EventArgs e)
        {
            Trace.WriteLine("Нажата кнопка запуска");

            if(!Directory.Exists(sourceDirectoryTextBox.Text)) 
            {
                MessageBox.Show("Путь исходной папки указан неверно. Пожалуйста исправьте.", "WhereIsMyPhoto");
                Trace.WriteLine("Путь исходной папки указан неверно");
                return;
            }
            if (!Directory.Exists(destDirectoryTextBox.Text))
            {
                try
                {
                    DialogResult result = MessageBox.Show("Будет создана папка "+ Path.Combine(Environment.CurrentDirectory, destDirectoryTextBox.Text) + "\nПродолжать?","WhereIsMyPhoto",MessageBoxButtons.OKCancel);
                    if(result != DialogResult.OK)
                    {
                        Trace.WriteLine("Путь конечной папки указан неверно.");
                        return;
                    }
                    Directory.CreateDirectory(destDirectoryTextBox.Text);
                    destDirectoryTextBox.Text = Path.Combine(Environment.CurrentDirectory, destDirectoryTextBox.Text);
                }
                catch(IOException ex)
                {
                    MessageBox.Show("Путь конечной папки указан неверно. Пожалуйста исправьте.", "WhereIsMyPhoto");
                    Trace.WriteLine("Путь конечной папки указан неверно. " + ex.Message);
                    return;
                }
                catch(Exception ex)
                {
                    Trace.WriteLine("Ошибка. " + ex.Message);
                    return;
                }
            }



            startButton.Text = "Остановить";
            startButton.Click -= startButton_Click;
            startButton.Click += Stop;
            //--- пуск
            
            cancellationTokenSource = new CancellationTokenSource();
            token = cancellationTokenSource.Token;

            Params prms;
            prms.format = (DestinationDirectoriesFormat)destinationComboBox.SelectedIndex;
            prms.mode = (FileOperations)fileOperationsComboBox.SelectedIndex;
            Sorter sorter = new Sorter(sourceDirectoryTextBox.Text, destDirectoryTextBox.Text, prms);

            sorter.sorterFileChangedEvent += SorterStatus;
            sorter.sorterMetadataErrorEvent += SorterError;
            await sorter.StartTaskSort(token);
            statusLabel.Text = "Завершено!";
            //----
            startButton.Text = "Старт";
            startButton.Click -= Stop;
            startButton.Click += startButton_Click;

        }

        private void SorterError(SorterErrorEventArgs seea)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<SorterErrorEventArgs>(SorterError), seea);
            }
            else
            {
                statusLabel.Text = "Не удалось узнать дату снимка: " + seea.CurrentFile;
                logListBox.Items.Add(seea.CurrentFile + " <?> не удалось выяснить дату съемки или сбой I/O");
            }
        }

        private void about_Click(object sender, EventArgs e)
        {
            string about = @"Программа WhereIsMyPhoto Sorter входит в комплект WhereIsMyPhoto. Настоящая версия ПО находится 
на этапе тестирования, может содержать ошибки и недоработки. Используйте на свой страх и риск. О проблеме можно сообщить по E-mail: viktor70@protonmail.com";
            MessageBox.Show(about, "WhereIsMyPhoto");
        }
    }
}
