using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BedrijvenTestPrepBL.Model;

namespace BedrijvenTestPrepBL.Interfaces
{
    public interface IBedrijfRepository
    {
        public void UploadToDatabase(List<Bedrijf>data);
    }
}
