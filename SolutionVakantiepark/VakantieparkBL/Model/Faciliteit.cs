using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VakantieparkBL.Model
{
    public class Faciliteit
    {
        public Faciliteit(string naam)
        {
            Naam = naam;
        }

        public Faciliteit(int id, string naam)
        {
            Id = id;
            Naam = naam;
        }

        public int Id { get; set; }
        public string Naam { get; set; }
        public override string ToString()
        {
            return $"{Id}-{Naam}";
        }

        public override bool Equals(object? obj)
        {
            return obj is Faciliteit faciliteit &&
                   Id == faciliteit.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
