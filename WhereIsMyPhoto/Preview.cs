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
        string imagePath;
        Image img;

        public Preview(ImageInformation ii)
        {
            imageInformation = ii;
            imagePath = ii.FileName;
            InitializeComponent();
        }

        Image ScaleImage(Image source, int width, int height) //сохраним пропорции изображения
        {
            Image dest = new Bitmap(width, height);

            using (Graphics gr = Graphics.FromImage(dest))
            {

                gr.FillRectangle(new SolidBrush(BackColor), 0, 0, width, height);  // Очищаем экран
                gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;

                float srcwidth = source.Width;
                float srcheight = source.Height;
                float dstwidth = width;
                float dstheight = height;

                if (srcwidth <= dstwidth && srcheight <= dstheight)  // Исходное изображение меньше целевого
                {
                    int left = (width - source.Width) / 2;
                    int top = (height - source.Height) / 2;
                    gr.DrawImage(source, left, top, source.Width, source.Height);
                }
                else if (srcwidth / srcheight > dstwidth / dstheight)  // Пропорции исходного изображения более широкие
                {
                    float cy = srcheight / srcwidth * dstwidth;
                    float top = ((float)dstheight - cy) / 2.0f;
                    if (top < 1.0f) top = 0;
                    gr.DrawImage(source, 0, top, dstwidth, cy);
                }
                else  // Пропорции исходного изображения более узкие
                {
                    float cx = srcwidth / srcheight * dstheight;
                    float left = ((float)dstwidth - cx) / 2.0f;
                    if (left < 1.0f) left = 0;
                    gr.DrawImage(source, left, 0, cx, dstheight);
                }

                return dest;
            }
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
            Text += " - " + System.IO.Path.GetFileName(imagePath);
            try
            {
                img = Image.FromFile(imagePath);

                var subifd0dir = imageInformation.Directories.OfType<ExifIfd0Directory>().FirstOrDefault();
                if (subifd0dir != null && subifd0dir.ContainsTag(ExifDirectoryBase.TagOrientation))
                {
                    uint orient = subifd0dir.GetUInt16(ExifDirectoryBase.TagOrientation);
                    switch (orient)
                    {
                        case 2:
                            img.RotateFlip(RotateFlipType.RotateNoneFlipX);
                            break;
                        case 3:
                            img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                            break;
                        case 4:
                            img.RotateFlip(RotateFlipType.RotateNoneFlipY);
                            break;
                        case 5:
                            img.RotateFlip(RotateFlipType.Rotate270FlipX);
                            break;
                        case 6:
                            img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                            break;
                        case 7:
                            img.RotateFlip(RotateFlipType.Rotate90FlipX);
                            break;
                        case 8:
                            img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                            break;
                    }
                }
                ShowImage();
            }
            catch(System.IO.FileNotFoundException)
            {
                string errmes = "Не удалось открыть файл для быстрого просмотра: " + imagePath + "\nФайл не найден";
                Trace.WriteLine(errmes);
                MessageBox.Show(errmes, "WhereIsMyPhoto");
                Close();
            }
            catch(Exception ex)
            {
                string errmes = "Не удалось открыть файл для быстрого просмотра: " + imagePath + "\n" + ex.Message;
                Trace.WriteLine(errmes);
                MessageBox.Show(errmes, "WhereIsMyPhoto");
                Close();
            }
            
        }

        private void ShowImage()
        {
            pictureBox.Image = ScaleImage(img, pictureBox.Size.Width, pictureBox.Size.Height);
        }

        private void Preview_SizeChanged(object sender, EventArgs e)
        {
            if(WindowState != StoredWindowState)
            {
                ShowImage();
                StoredWindowState = WindowState;
            }
        }
    }
}
