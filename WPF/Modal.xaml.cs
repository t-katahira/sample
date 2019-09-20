using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Threading;

namespace test1_wpf
{

    /// <summary>
    /// modal.xaml の相互作用ロジック
    /// </summary>
    public partial class modal : Window
    {
        private CancellationTokenSource _tokenSource = null;

        private CopyInfo _viewModel;

        public modal()
        {
            InitializeComponent();

            _viewModel = new CopyInfo { srcPath = "srcPath", desPath = "desPath" };

            this.cancelBtn.IsEnabled = false;

            this.DataContext = _viewModel;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string ldesPath = "C:\\00_testFolder\\C";
            string lsrcpath = "C:\\katahira\\V07R09";
            _viewModel.desPath = ldesPath;
            _viewModel.srcPath = lsrcpath;

            string[] files = Directory.GetFiles(lsrcpath, "*", System.IO.SearchOption.AllDirectories);


            prog.Maximum = files.Count();
            prog.Minimum = 0;
            prog.Value = 0;
            prog.Visibility = Visibility.Visible;


            this.startBtn.IsEnabled = false;
            this.cancelBtn.IsEnabled = true;

            if (_tokenSource == null) _tokenSource = new CancellationTokenSource();
            var token = _tokenSource.Token;

#if true
            Task.Factory.StartNew(() =>
            {
                for(int i = 0; i < files.Count(); i++)
                {
                    string lfileName  = System.IO.Path.GetFileName(files[i]);
                    _viewModel.fileName = lfileName;
                    string str2 = ldesPath + files[i].Remove(0, lsrcpath.Length);
                    SafeCreateDirectory(System.IO.Path.GetDirectoryName(str2));
                    System.IO.File.Copy(files[i], str2, true);

                }
            }, token).ContinueWith(t =>
            {
                _tokenSource.Dispose();
                _tokenSource = null;


            });
#elif false
            for (int i = 0; i < files.Count(); i++)
            {
                Task.Factory.StartNew(() =>
                {
                    string lfileName = System.IO.Path.GetFileName(files[i]);
                    _viewModel.fileName = lfileName;
                    string str2 = ldesPath + files[i].Remove(0, lsrcpath.Length);
                    SafeCreateDirectory(System.IO.Path.GetDirectoryName(str2));
                    System.IO.File.Copy(files[i], str2, true);
                }, token).ContinueWith(t =>
                {
                    _tokenSource.Dispose();
                    _tokenSource = null;


                });
            }

#endif

        }

        private DirectoryInfo SafeCreateDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                return null;
            }
            return Directory.CreateDirectory(path);
        }


            private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_tokenSource != null) _tokenSource.Cancel();
            Close();
        }
    }
}
