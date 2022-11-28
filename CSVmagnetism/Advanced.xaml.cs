using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Application = System.Windows.Forms.Application;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.IO;

namespace CSVmagnetism
{
    /// <summary>
    /// Advanced.xaml の相互作用ロジック
    /// </summary>
    public partial class Advanced : Page
    {
        public Advanced()
        {
            InitializeComponent();
        }
        public Advanced(MagArray ma)
        {
            InitializeComponent();
            Renew(ma);
        }

        private void FileButton_Click(object sender, RoutedEventArgs e)
        {
            MagArray ma;

            var dlg = new CommonOpenFileDialog();
            dlg.IsFolderPicker = false;
            dlg.DefaultDirectory = Application.StartupPath;
            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
            {
                ma = new MagArray(dlg.FileName);
            }
            else
            {
                return;
            }

            if (ma.type == 1)
            {
                var page = new Normal(ma);
                NavigationService.Navigate(page);
                return;
            }

            Renew(ma);
        }
        private void Renew(MagArray ma)
        {
            for (int i = 0; i < 44; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    String name = "a";
                    name += i.ToString("00") + j.ToString("00");
                    Arrow ar = this.FindName(name) as Arrow;
                    if (null != ar)
                    {
                        ar.SetDegree(ma.Radians(i, j));
                    }
                }
            }
        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new CommonSaveFileDialog();
            dlg.DefaultFileName = "result.png";
            dlg.Filters.Add(new CommonFileDialogFilter("png", "*.png, *.PNG"));
            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
            {
                var filename = dlg.FileName;

                this.UpdateLayout();
                var width = MagDisplay.ActualWidth;
                var height = MagDisplay.ActualHeight;
                var dv = new DrawingVisual();
                using (var dc = dv.RenderOpen())
                {
                    dc.DrawRectangle(new BitmapCacheBrush(MagDisplay), null, new Rect(0, 0, width, height));
                }
                var rtb = new RenderTargetBitmap((int)width, (int)height, 96d, 96d, PixelFormats.Pbgra32);
                rtb.Render(dv);

                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(rtb));
                FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite);
                encoder.Save(fs);
                fs.Close();
            }
            dlg.Dispose();
        }
    }
}
