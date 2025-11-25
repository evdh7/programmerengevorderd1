using BedrijvenTestPrepBL;
using BedrijvenTestPrepBL.Interfaces;
using BedrijvenTestPrepBL.Model;
using BedrijvenTestPrepDL_File.Model;


namespace BedrijvenTestPrepDL_File
{
    public class BedrijfBestandsLezer : IBedrijfBestandslezer
    {
        private List<Bedrijf> Converteer(List<BedrijfDL> data)
        {

            List<Bedrijf> bedrijven = new();

            foreach (BedrijfDL b in data)
            {
                List<Persoon> personeelslijst = new List<Persoon>();
                foreach (PersoonDL persoon in b.Personeelsleden)
                {
                    Adres adres = new Adres(persoon.AdresDL.Gemeente, persoon.AdresDL.Postcode, persoon.AdresDL.Straat, persoon.AdresDL.Huisnummer);
                    personeelslijst.Add(new Persoon(persoon.Id, persoon.FirstName, persoon.LastName, persoon.DateOfBirth, persoon.Email, adres));
                }

                bedrijven.Add(new Bedrijf(b.Name, b.Industrie, b.Sector, b.Location, b.Year, b.Info, personeelslijst));
            }
            return bedrijven;

        }
        public List<Bedrijf> ReadFile(string path, string logPath)
        {
            Dictionary<string, BedrijfDL> bedrijven = new();
            using (StreamWriter sw = new StreamWriter(logPath))
            using (StreamReader sr = new StreamReader(path))
            {
                string? line;
                int lineNumber = 0;


                while ((line = sr.ReadLine()) != null)
                {
                    lineNumber++;
                    string? errorInfo = null;

                    try
                    {
                        string[] s = line.Split('|');
                        if (s.Length < 15)
                        {
                            sw.WriteLine($"Lijn {lineNumber}: onvoldoende velden ({s.Length}/15)");
                            continue;
                        }
                        errorInfo = string.Join('|', s[0], s[7], s[8]);//om fouten bij te houden

                        AdresDL adresDL = new()
                        {
                            Gemeente = s[10],
                            Postcode = int.Parse(s[11]),
                            Straat = s[12],
                            Huisnummer = s[13]
                        };

                        if (!DateTime.TryParse(s[9], out DateTime geboortedatum))
                        {
                            sw.WriteLine($"Lijn {lineNumber}: ongeldige geboortedatum '{s[9]}'");
                            continue;
                        }

                        if (!int.TryParse(s[6], out int id))
                        {
                            sw.WriteLine($"Lijn {lineNumber}: ongeldige id '{s[6]}'");
                            continue;
                        }

                        PersoonDL persoonDL = new()
                        {
                            Id = id,
                            FirstName = s[7],
                            LastName = s[8],
                            DateOfBirth = geboortedatum,
                            Email = s[14],
                            AdresDL = adresDL
                        };

                        bool gelukt = int.TryParse(s[4], out int year);
                        string bedrijfNaam = s[0];

                        if (bedrijven.ContainsKey(bedrijfNaam))
                        {
                            bedrijven[bedrijfNaam].Personeelsleden.Add(persoonDL);
                        }
                        else
                        {
                            BedrijfDL bedrijfDL = new()
                            {
                                Name = bedrijfNaam,
                                Industrie = s[1],
                                Sector = s[2],
                                Location = s[3],
                                Year = year,
                                Info = s[5],
                                Personeelsleden = new List<PersoonDL> { persoonDL }
                            };

                            bedrijven.Add(bedrijfNaam, bedrijfDL);
                        }
                    }
                    catch (BedrijfException ex)
                    {

                        LogException(sw, lineNumber, errorInfo, ex);
                    }

                    catch (Exception ex)
                    { sw.WriteLine($"Lijn {lineNumber}: {errorInfo} => Onverwachte fout: {ex.Message}"); }
                }

                return Converteer(bedrijven.Values.ToList());
            }
        }
        private void LogException(StreamWriter sw, int lineNumber, string errorInfo, BedrijfException ex)
        {
            sw.WriteLine($"Lijn {lineNumber}: {errorInfo} => {ex.Message}");
            if (ex.Errors != null && ex.Errors.Count > 0)
            {
                foreach (var err in ex.Errors)
                {
                    sw.WriteLine($"\t{err}");
                }
            }
        }
        
    }
}

