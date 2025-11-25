namespace TestLego_BL.Interfaces
{
    public interface ILegoFileReader
    {
        List<LegoTheme> ReadFile(string path);
    }
}
