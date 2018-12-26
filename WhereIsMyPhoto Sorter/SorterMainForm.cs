using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
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

            sorter.sortedFileChangedEvent += SorterStatus;
            await sorter.StartTaskSort(token);
            statusLabel.Text = "Завершено!";
            //----
            startButton.Text = "Старт";
            startButton.Click -= Stop;
            startButton.Click += startButton_Click;

        }
    }
}
