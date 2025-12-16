using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorUI_WPF.Model
{
    public class CustomerUI : INotifyPropertyChanged
    {

        public int ID { get; set; }
        private string name;

        public CustomerUI(string name)
        {
            Name = name;
        }

        public string Name
        {
            get => name;
            set { name = value; OnPropertyChanged("Name"); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
