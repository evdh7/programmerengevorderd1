using JobInterviewBL.Interfaces;
using JobInterviewBL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobInterviewDL
{
    public class JobInterviewRepositoryMemory : IJobInterviewRepository
    {
        private Dictionary<int, HREmployee> employees = new();
        private List<Expert> experts = new();
        private int employeeID = 1;

        public JobInterviewRepositoryMemory() 
        {
            employees.Add(employeeID, new HREmployee(employeeID, "Jos", "jos@gmail", "012345", "Finance"));
                employeeID++;
            employees.Add(employeeID, new HREmployee(employeeID, "Julie", "julie@gmail", "678910", "Finance"));
                employeeID++;
            employees.Add(employeeID, new HREmployee(employeeID, "Eddy", "eddy@gmail", "234567", "IT"));
                employeeID++;
            employees.Add(employeeID, new HREmployee(employeeID, "Maria", "marie@gmail", "765432", "HR"));
                employeeID++;
            experts.Add(new Expert("Jan","IT"));
            experts.Add(new Expert("Piet", "Finance"));
            experts.Add(new Expert("Joris", "Sales"));
            experts.Add(new Expert("Corneel", "Marketing"));

        }

        public void AddHREmployee(HREmployee hREmployee)
        {
            hREmployee.ID = employeeID++;
            employees.Add(hREmployee.ID, hREmployee);
        }
        public List<HREmployee> GetHREmployees()
        {
            return employees.Values.ToList();
        }

        public void UpdateHREmployee(HREmployee hREmployee)
        {
            employees[hREmployee.ID]= hREmployee;
        }

        public void DeleteHREmployee(HREmployee hREmployee)
        {
            employees.Remove(hREmployee.ID);
        }
        public List<Expert> GetExperts()
        {
            return experts;
        }
    }
}
