namespace EmployeeEP_BL.Model
{
    public class Employee
    {
        public Employee(string name, string surname, DateTime hireDate, int hoursPerWeek, int salary, Employer employer)
        {
            Name = name;
            Surname = surname;
            HireDate = hireDate;
            HoursPerWeek = hoursPerWeek;
            Employer = employer;
            Salary = salary;
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime HireDate { get; set; }
        public int HoursPerWeek { get; set; }

        public int Salary { get; set; }

        Employer Employer { get; set; }
    }
}
