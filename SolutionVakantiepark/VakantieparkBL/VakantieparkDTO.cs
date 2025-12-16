using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VakantieparkBL.Exceptions;
using VakantieparkBL.Model;

namespace VakantieparkBL
{
    public class VakantieparkDTO : INotifyPropertyChanged
    {
        public VakantieparkDTO(int id, string naam, string locatie, int capaciteit, int maxCapaciteit, int aantalWooneenheden, int aantalFaciliteiten, string contact)
        {
            Id = id;
            Naam = naam;
            Locatie = locatie;
            Capaciteit = capaciteit;
            MaxCapaciteit = maxCapaciteit;
            AantalWooneenheden = aantalWooneenheden;
            AantalFaciliteiten = aantalFaciliteiten;
            Contact = contact;
        }

        public int Id { get; set; }
        private string naam;
        public string Naam { get => naam; set { naam = value; OnPropertyChanged("Naam"); } }
        private string locatie;
        public string Locatie { get => locatie; set { locatie = value; OnPropertyChanged("Locatie"); } }
        private int capaciteit;
        public int Capaciteit { get => capaciteit; set { capaciteit = value; OnPropertyChanged("Capaciteit"); } }
        public int MaxCapaciteit{ get; set; }
        public int AantalWooneenheden { get; set; }
        public int AantalFaciliteiten { get; set; }
        public string Contact {  get; set; }//email

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
