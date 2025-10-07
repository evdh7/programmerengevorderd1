using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BedrijvenDomein
{
    public static class FoutenLoggen
    {
        public static void Verzamelen(List<string> errors, string contextMessage)
        {
            if (errors != null && errors.Count > 0)
            {
                errors.Insert(0, " ");
                var ex = new BedrijfException(contextMessage)
                {
                    Errors = errors
                };
                throw ex;
            }
        }
    }
}
