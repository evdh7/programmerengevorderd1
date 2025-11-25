using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobInterviewBL.Model
{
    public class Expert
    {
        public Expert(string name, string expertise) 
        { 
            this.Name = name;
            this.Expertise = expertise;
        }
        public string Name {get; set;}
        public string Expertise { get; set;}
        public override string ToString()
        {
            return $"{Name} - {Expertise}";
        }
    }
}
