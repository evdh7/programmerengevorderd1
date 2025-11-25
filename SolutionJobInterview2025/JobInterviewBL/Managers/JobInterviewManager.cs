using JobInterviewBL.Interfaces;
using JobInterviewBL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobInterviewBL.Managers
{
    public class JobInterviewManager
    {
        private IJobInterviewRepository repo;
        public JobInterviewManager(IJobInterviewRepository repo)
        {
            this.repo = repo;
        }
        public void AddHREmployee(HREmployee hREmployee) 
        {
            repo.AddHREmployee(hREmployee);      
        }

        public List <HREmployee> GetHREmployees()
        {
            return repo.GetHREmployees();
        }
        public void UpdateHREmployee(HREmployee hREmployee)
        {
            repo.UpdateHREmployee(hREmployee);
        }
        public void DeleteHREmployee(HREmployee hREmployee)
        {
            repo.DeleteHREmployee(hREmployee) ;
        }
        public List <Expert> GetExperts()
        {
            return repo.GetExperts();
        }
    }
}
