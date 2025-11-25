using JobInterviewBL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobInterviewBL.Interfaces
{
    public interface IJobInterviewRepository
    {
        List <HREmployee> GetHREmployees();
        void AddHREmployee(HREmployee hREmployee);
        void UpdateHREmployee(HREmployee hREmployee);
        void DeleteHREmployee(HREmployee hREmployee);
        List<Expert> GetExperts();

    }
}
