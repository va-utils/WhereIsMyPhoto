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
    class ImageWorks
    {
        public Image img { get; private set; }
        string imagePath;
        ImageInformation imageInformation;



        public ImageWorks(ImageInformation ii)
        {
            imageInformation = ii;
            imagePath = ii.FileName;
        }

        public bool TryCreateImageWithCorrectOrientation()
        {
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
                return true;
            }
            catch (System.IO.FileNotFoundException)
            {
                string errmes = "Не удалось открыть файл для быстрого просмотра: " + imagePath + "\nФайл не найден";
                Trace.WriteLine(errmes);
                MessageBox.Show(errmes, "WhereIsMyPhoto");
                return false;
            }
            catch (Exception ex)
            {
                string errmes = "Не удалось открыть файл для быстрого просмотра: " + imagePath + "\n" + ex.Message;
                Trace.WriteLine(errmes);
                MessageBox.Show(errmes, "WhereIsMyPhoto");
                return false;
            }
        }

        public Image ScaleImage(int width, int height, Color clr) //сохраним пропорции изображения
        {
            Image source = img;
            Image dest = new Bitmap(width, height);
           
            using (Graphics gr = Graphics.FromImage(dest))
            {
                gr.FillRectangle(new SolidBrush(clr), 0, 0, width, height);  // Очищаем экран
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
    }
}
