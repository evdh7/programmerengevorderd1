using TestLego_BL.Interfaces;

namespace TestLego_BL.Beheer
{
    public class LegoBeheerder
    {
        private ILegoFileReader fileReader;
        private ILegoRepository repo;

        public LegoBeheerder(ILegoFileReader fileReader, ILegoRepository repo)
        {
            this.fileReader = fileReader;
            this.repo = repo;
            
        }
        public void UploadNaarDatabank(string path)
        {
            //stap 1 lezen bestanden
            var data = fileReader.ReadFile(path);
            //stap 2 schrijven naar databank
            repo.WriteLegoThemes(data);           
        }

    }
}
