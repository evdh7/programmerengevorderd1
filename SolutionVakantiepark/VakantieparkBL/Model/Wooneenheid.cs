using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VakantieparkBL.Model
{
    public class Wooneenheid
    {
        public Wooneenheid(int capaciteit, string adres, HuisStatus status)
        {
            Capaciteit = capaciteit;
            Adres = adres;
            Status = status;
        }
        public Wooneenheid(int id, int capaciteit, string adres, HuisStatus status)
        {
            Id = id;
            Capaciteit = capaciteit;
            Adres = adres;
            Status = status;
        }

        public int Id { get; set; }
        public int Capaciteit { get; set; }
        public string Adres { get; set; }
        public HuisStatus Status { get; set; }
        public override string ToString()
        {
            return $"{Adres} - {Status} - {Capaciteit}";
        }
    }
}
