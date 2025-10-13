using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentBL.Interfaces;
using StudentBL.Model;

namespace StudentBL.Beheerder
{
    public class StudentBeheerder
    {
        private IStudentRepository repo;

        public StudentBeheerder(IStudentRepository repo)
        {
            this.repo = repo;
        }

        public void VoegStudentToe(Student student)
        {
            repo.VoegStudentToe(student);
        }
        public void VoegKlasToe(Klas klas)
        {
            repo.VoegKlasToe(klas);
        }

        public void VoegCursussenToe(List<Cursus> cursussen)
        {
            repo.VoegCursussenToe(cursussen);
        }
        public List<Cursus> GeefCursussen(string voorwaarde)
        {
            return repo.GeefCursussen(voorwaarde);
        }

    }
}
