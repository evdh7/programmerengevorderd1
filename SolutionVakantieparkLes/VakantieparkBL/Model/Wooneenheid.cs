using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace VakantieparkBL.Model
{
    public class Wooneenheid
    {

        public Wooneenheid(int id, string adres, int capaciteit, HuisStatus status)
        {
            Id = id;
            Adres = adres;
            Capaciteit = capaciteit;
            Status = status;
        }

        public int? Id { get; set; }
        public string Adres {  get; set; }
        public int Capaciteit {  get; set; }
        public HuisStatus Status { get; set; }

        public override bool Equals(object? obj)
        {
            //return obj is Wooneenheid wooneenheid &&
            //       Id == wooneenheid.Id &&
            //       Adres == wooneenheid.Adres;
            if (Id == null)
            {
                return obj is Wooneenheid wooneenheid &&
                       Adres == wooneenheid.Adres;
            }
            else
                return obj is Wooneenheid wooneenheid &&
                    Id == wooneenheid.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Adres);
        }
    }
}
