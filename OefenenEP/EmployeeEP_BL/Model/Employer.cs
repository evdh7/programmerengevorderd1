using EmployeeEP_BL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeEP_BL.Model
{
    public class Employer
    {
        public Employer(string name, List<Employee> employees, EField field ) 
        {

            
        }

        string Name { get; set; }
        public IReadOnlyList<Employee> Employees { get; set; }
        public EField Field { get; set; } = new EField();
    }
}
