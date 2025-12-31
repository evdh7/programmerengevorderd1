using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.Model
{
    public class Client : INotifyPropertyChanged
    {
        public int? ID { get; set; }

        private string name;
        public Client(string name)
        {
            Name = name;
        }

        public string Name
        {
            get => name;
            set { name = value; OnPropertyChanged(nameof(Name)); }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        public int? Id { get; set; }

        public List<Client> ClientNames { get; set; }
    }
}
