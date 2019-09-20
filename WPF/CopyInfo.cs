using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace test1_wpf
{
    class CopyInfo : INotifyPropertyChanged
    {
        private string _srcPath;
        public string srcPath
        {
            get { return _srcPath; }
            set
            {
                _srcPath = value;
                OnPropertyChanged("srcPath");
            }
        }

        private string _desPath;
        public string desPath
        {
            get { return _desPath; }
            set
            {
                _desPath = value;
                OnPropertyChanged("desPath");
            }
        }

        private string _fileName;
        public string fileName
        {
            get { return _fileName; }
            set
            {
                _fileName = value;
                OnPropertyChanged("fileName");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
