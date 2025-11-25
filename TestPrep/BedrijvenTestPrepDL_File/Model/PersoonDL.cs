using BedrijvenTestPrepBL.Model;

namespace BedrijvenTestPrepDL_File.Model
{
    internal class PersoonDL
    {

        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; }

        public AdresDL AdresDL { get; set; }

    }

}
