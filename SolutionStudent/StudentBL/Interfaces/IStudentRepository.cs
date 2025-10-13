using StudentBL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentBL.Interfaces
{
    public interface IStudentRepository
    {
        List<Cursus> GeefCursussen(string voorwaarde);
        public void VoegCursussenToe(List<Cursus> cursussen);
        public void VoegKlasToe(Klas klas);
        public void VoegStudentToe(Student student);
    }
}
