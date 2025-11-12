using ProvinciesBL.Model;

namespace ProvinciesBL.Interfaces
{
    public interface IProvincieBestandslezer
    {
        List<Provincie> LeesBestanden(string folder, List<string> bestandsnamen);
        List<string> GeefInhoudZip(string fileName);
        void ClearFolder(string folderName);
        bool IsFolderEmpty(string folderName);

        void Unzip(string zipFile, string outputFolder);
    }
}
