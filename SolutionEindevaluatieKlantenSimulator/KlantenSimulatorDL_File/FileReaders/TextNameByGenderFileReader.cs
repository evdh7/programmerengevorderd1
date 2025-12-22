//using KlantenSimulatorBL.DTOs;
//using KlantenSimulatorBL.Enums;
//using KlantenSimulatorBL.Interfaces;
//using KlantenSimulatorDL_File.Helpers.KlantenSimulatorDL_File.Helpers;

//namespace KlantenSimulatorDL_File.FileReaders
//{
//    public class TextNameByGenderFileReader : INameReader
//    {
//        public Dictionary<NameType, List<NameDTO>> ReadNames(string folder, string fileName, NameType nameType, Gender? gender)
//        {
//            Dictionary<NameType, List<NameDTO>> result = new Dictionary<NameType, List<NameDTO>>();

//            List<NameDTO> names = new List<NameDTO>();

//            using (StreamReader sr = new StreamReader(Path.Combine(folder, fileName)))
//            {
//                string? line;
//                (int skipLines, int freq) = Helper.SkipLines(folder, fileName);

//                for (int i = 0; i < skipLines && !sr.EndOfStream; i++)
//                {
//                    line = sr.ReadLine();
//                }

//                while ((line = sr.ReadLine()) != null)
//                {
//                    string[] ss = line.Split('\t');
//                    int frequency = 0;

//                    string name = ss[0];

//                    if (fileName.ToLower().Contains("last"))
//                    {
//                        frequency = int.Parse(ss[2]);
//                        names.Add(new NameDTO(name, gender, frequency));
//                    }

//                    else if (fileName.ToLower().Contains("first"))
//                    {
//                        if (int.TryParse(ss[1], out int valueF))
//                        {
//                            names.Add(new NameDTO(name, Gender.Female, frequency));
//                        }

//                        if (int.TryParse(ss[2], out int valueM))
//                        {
//                            names.Add(new NameDTO(name, Gender.Male, frequency));
//                        }
//                    }
//                }
//                    result.Add(nameType, names);

//                }
//                return result;
            
//        }

//    }
//}
