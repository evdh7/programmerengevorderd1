using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobInterviewBL.Model
{
    public class HREmployee
    {
        public HREmployee(int iD, string name, string email, string phone, string expertise)
        {
            ID = iD;
            Name = name;
            Email = email;
            Phone = phone;
            Expertise = expertise;
        }

        public HREmployee(string name, string email, string phone, string expertise)
        {
            Name = name;
            Email = email;
            Phone = phone;
            Expertise = expertise;
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Email {get; set;}
        public string Phone {get; set;}
        public string Expertise {get; set;}
    }
}
