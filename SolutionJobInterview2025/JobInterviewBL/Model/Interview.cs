using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobInterviewBL.Model
{
    public class Interview
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public HREmployee HREmployee { get; set; }
        public List<Expert> Experts { get; set; }
        public string Notes { get; set; }
        public string CandidateName {get; set;}
        public string Function {  get; set;}
        public bool IsOnline { get; set; }

    }
}
