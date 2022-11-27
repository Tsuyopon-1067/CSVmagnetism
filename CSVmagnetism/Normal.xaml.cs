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

namespace CSVmagnetism
{
    /// <summary>
    /// Normal.xaml の相互作用ロジック
    /// </summary>
    public partial class Normal : Page
    {
        public Normal()
        {
            InitializeComponent();
        }
        public Normal(MagArray ma)
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

            if (ma.type == 2)
            {
                var page = new Advanced(ma);
                NavigationService.Navigate(page);
            }
            Renew(ma);
        }
        private void Renew(MagArray ma)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    String name = "a";
                    name += i.ToString("00") + j.ToString("00");
                    Arrow ar = this.FindName(name) as Arrow;
                    if (null != ar)
                    {
                        ar.SetRadian(ma.Radians(i, j));
                    }
                }
            }
        }
    }
}
