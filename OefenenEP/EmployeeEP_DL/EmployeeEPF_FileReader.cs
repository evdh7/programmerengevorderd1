using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeEP_BL.Interfaces;
using EmployeeEP_BL.Model;

namespace EmployeeEP_DL
{
    public class EmployeeEPF_FileReader : IEmployeeEP_FileReader
    {
        public List<Employee> ReadFile(string fileName)
        {
            using StreamReader streamReader = new StreamReader(fileName)
            {

            }
        }

    }
}
