using System.Collections.Specialized;
using System.ComponentModel;

namespace JobInterviewUI.Model
{
    public class HREmployeeUI : INotifyPropertyChanged
    {

        public HREmployeeUI(int iD, string name, string email, string phone, string expertise)
        {
            ID = iD;
            Name = name;
            Email = email;
            Phone = phone;
            Expertise = expertise;
        }

        public HREmployeeUI(string name, string email, string phone, string expertise)
        {
            Name = name;
            Email = email;
            Phone = phone;
            Expertise = expertise;
        }

        public int ID { get; set; }
        private string name;
        public string Name { get => name; set { name = value; OnPropertyChanged("Name"); } }
        private string email;
        public string Email { get => email; set { email = value; OnPropertyChanged("Email"); } }
        private string phone;
        public string Phone { get => phone; set { phone = value; OnPropertyChanged("Phone"); } }
        private string expertise;
        public string Expertise { get=> expertise; set { expertise = value; OnPropertyChanged("Expertise"); } }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

