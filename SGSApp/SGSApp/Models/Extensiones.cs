using System;
using System.ComponentModel;

namespace SGSApp.Models
{
    public class Extensiones : INotifyPropertyChanged
    {
        public string nombreExtension { get; set; }
        public string extension { get; set; }
        public string responsable { get; set; }
        public Type TargetType { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged == null)
                return;
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}