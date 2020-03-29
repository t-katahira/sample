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
using System.IO;

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ListBox1.Items.Clear();

            string[] drives = Directory.GetLogicalDrives();

            foreach(string str in drives)
            {
                ListBox1.Items.Add(str);
            }

            ListBox1.SelectedIndex = 0;
        }

        private void Control_GotFocus(object sender, RoutedEventArgs e)
        {
            string rootPath = (string)ListBox1.Items[ListBox1.SelectedIndex];

            //TreeViewにルートで表示するフォルダを追加
            var item = new DirectoryTreeItem(new System.IO.DirectoryInfo(rootPath));
            MyRoot.Items.Add(item);
        }


        private void Control_GotFocus1(object sender, RoutedEventArgs e)
        {
            

            //var neko = (DirectoryTreeItem)MyRoot.Items[0];
            //var inu = (TreeViewItem)neko.Items[0];
            //var item = (DirectoryTreeItem)MyRoot.SelectedItem;
            //if (item == null) return;
            //string str = item.ToString();
            //string dir = item.DirectoryInfo.FullName;
        }
    }

    public class DirectoryTreeItem : TreeViewItem
    {
        public readonly System.IO.DirectoryInfo DirectoryInfo;
        private bool IsAdd;//サブフォルダを作成済みかどうか
        private TreeViewItem Dummy;//ダミーアイテム


        public DirectoryTreeItem(System.IO.DirectoryInfo info)
        {
            DirectoryInfo = info;
            Header = info.Name;

            //サブフォルダが1つでもあれば
            if (info.GetDirectories().Length > 0)
            //展開できることを示す▷を表示するためにダミーのTreeViewItemを追加する
            {
                Dummy = new TreeViewItem();
                Items.Add(Dummy);
            }

            //イベント、ツリー展開時
            //サブフォルダを追加
            this.Expanded += (s, e) =>
            {
                if (IsAdd) return;//追加済みなら何もしない
                AddSubDirectory();
            };
        }

        //サブフォルダツリー追加
        public void AddSubDirectory()
        {
            Items.Remove(Dummy);//ダミーのTreeViewItemを削除

            //すべてのサブフォルダを追加
            System.IO.DirectoryInfo[] directories = DirectoryInfo.GetDirectories();
            for (int i = 0; i < directories.Length; i++)
            {
                //隠しフォルダ、システムフォルダは除外する
                var fileAttributes = directories[i].Attributes;
                if ((fileAttributes & System.IO.FileAttributes.Hidden) == System.IO.FileAttributes.Hidden ||
                        (fileAttributes & System.IO.FileAttributes.System) == System.IO.FileAttributes.System)
                {
                    continue;
                }
                //追加
                Items.Add(new DirectoryTreeItem(directories[i]));
            }
            IsAdd = true;//サブフォルダ作成済みフラグ
        }


        public override string ToString()
        {
            return DirectoryInfo.FullName;
        }
    }
}
