using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhereIsMyPhoto
{
    public partial class Preview : Form
    {
        FormWindowState StoredWindowState;
        ImageInformation imageInformation;
        ImageWorks imageWorks;

        public Preview(ImageInformation ii)
        {
            imageInformation = ii;
            InitializeComponent();
        }

        

        private const int WM_EXITSIZEMOVE = 0x0232;
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_EXITSIZEMOVE)
                ShowImage();
            base.WndProc(ref m);
        }

        private void Previev_Load(object sender, EventArgs e)
        {
            Text += " - " + System.IO.Path.GetFileName(imageInformation.FileName);
            ShowImage();
        }

        private void RedrawImage()
        {
            pictureBox.Image = imageWorks?.ScaleImage(pictureBox.Width, pictureBox.Height, Color.White);
        }

        private void ShowImage()
        {
            imageWorks = new ImageWorks(imageInformation);
            imageWorks.TryCreateImageWithCorrectOrientation();
            pictureBox.Image = imageWorks?.ScaleImage(pictureBox.Width, pictureBox.Height, Color.White);
        }

        private void Preview_SizeChanged(object sender, EventArgs e)
        {
            if(WindowState != StoredWindowState)
            {
                RedrawImage();
                StoredWindowState = WindowState;
            }
        }

        private void Preview_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(imageWorks!=null)
            {
                imageWorks.img.Dispose();
                imageWorks = null;
            }
        }
    }
}
