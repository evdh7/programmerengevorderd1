using Vakantiepark2DL.TO;

namespace Vakantiepark2DL.DAOs
{
    public class ContactpersoonDAO
    {
        private int contactpersoonId = 1;
        private Dictionary<int, ContactpersoonTO> contactpersonen = new();

        public ContactpersoonDAO()
        {
            contactpersonen.Add(contactpersoonId, new ContactpersoonTO(contactpersoonId, "jos", "jos@gmail.com", "01234566"))
                        ; contactpersoonId++;
            contactpersonen.Add(contactpersoonId, new ContactpersoonTO(contactpersoonId, "julie", "julie@gmail.com", "012345669"))
                ; contactpersoonId++;
        }

    }
}
