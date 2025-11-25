using JobInterviewBL.Model;
using JobInterviewUI.Model;

namespace JobInterviewUI.Mappers
{
    public static class HREmployeeMapper
    {
        public static HREmployeeUI MapFromDomain(HREmployee employee)
        {
            return new HREmployeeUI(employee.ID, employee.Name, employee.Email, employee.Phone, employee.Expertise);
        }
        public static HREmployee MapToDomain(HREmployeeUI employee)
        {
            return new HREmployee(employee.ID, employee.Name, employee.Email, employee.Phone, employee.Expertise);

        }
    }
}

