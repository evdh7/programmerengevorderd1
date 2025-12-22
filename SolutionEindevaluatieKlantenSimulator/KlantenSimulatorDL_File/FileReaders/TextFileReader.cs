using KlantenSimulatorBL.DTOs;
using KlantenSimulatorBL.Enums;
using KlantenSimulatorBL.Interfaces;
using KlantenSimulatorDL_File.Helpers.KlantenSimulatorDL_File.Helpers;
using System;
using System.Data.Common;
using System.Globalization;
using System.Text;
using static KlantenSimulatorBL.DTOs.NameDTO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KlantenSimulatorDL_File.FileReaders
{
    public class TextFileReader : INameReader
    {
        public List<NameEntry> ReadNames(string folder, (string, string)[] fileNames, NameType nameType, Gender? gender)
        {
            List<NameEntry> allNames = new();

            foreach (var file in fileNames)

                using (StreamReader sr = new StreamReader(Path.Combine(folder, file.Item2), Encoding.GetEncoding(1252))) //autodetect this
                {
                    (int frequencyColumn, string firstValidLine) = SkipLines(sr);

                    if (firstValidLine!=null) 
                    {
                        ParseLine(firstValidLine, frequencyColumn, gender, allNames, nameType);
                    }

                    string? line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        ParseLine(line, frequencyColumn, gender, allNames, nameType);
                    }
                }

            return allNames;

        }

        private (int frequencyColumn, string? firstValidLine) SkipLines(StreamReader sr)
        {
            string? line;
            int frequencyColumn = 0;

            while ((line = sr.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                string[] ss = line.Split('\t');

                bool hasNumber = ss.Any(cell => IsInteger(cell));

                if (!hasNumber) continue;

                frequencyColumn = FindFrequencyColumn(ss);

                return (frequencyColumn, line);
            }

            return (0,null);    
        }
        private void ParseLine(string line, int fColumn, Gender? gender, List<NameEntry> names, NameType nameType)
        {
            if (string.IsNullOrWhiteSpace(line))
                return;

            string[] ss = line.Split('\t');
            if (ss.Length <= fColumn)
                return;

            string trimmed = ss[fColumn].Trim().Replace(".", "");

            if (!int.TryParse(trimmed, NumberStyles.Integer, CultureInfo.InvariantCulture, out int frequency))
                return;

            string name = ss[fColumn - 1];
            byte[] bytes = Encoding.Default.GetBytes(name);
            name = Encoding.UTF8.GetString(bytes);

            names.Add(new NameEntry(name, nameType, gender, frequency));
        }
        private int FindFrequencyColumn(string[] ss)
        {
            if (IsInteger(ss[0]))
                return 2;

            return 1;
        }
        private bool IsInteger(string input)
        {
            string trimmed = input.Trim().Replace(".", "");

            return int.TryParse(trimmed, CultureInfo.InvariantCulture, out int result);
        }

    }
}