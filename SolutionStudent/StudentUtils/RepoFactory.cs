using StudentBL.Interfaces;
using StudentDL;

namespace StudentUtils
{
    public static class RepoFactory
    {
        public static IStudentRepository GeefRepo(string connectionString)
        {
            return new StudentRepository(connectionString);
        }
    }
}
