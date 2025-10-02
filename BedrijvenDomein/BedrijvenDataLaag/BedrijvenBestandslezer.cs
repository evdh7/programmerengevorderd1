using BedrijvenDomein;

namespace BedrijvenDataLaag
{
    public class BedrijvenBestandslezer
    {

        public List<Bedrijf> LeesBestand(string pad, string padLog)
        {
            Dictionary<string, Bedrijf> data = new();
            using (StreamWriter sw = new StreamWriter(padLog))
            using (StreamReader reader = new StreamReader(pad))
            {

                string? line;
                int lineNumber = 0;
                string? errorInfo = null;

                static void FoutenVerzamelen(List<string> errors, string contextMessage)
                {
                    if (errors != null && errors.Count > 0)
                    {
                        BedrijfException ex = new BedrijfException(contextMessage);
                        errors.Insert(0, " ");
                        ex.Errors = errors;
                        throw ex;
                    }
                }

                while ((line = reader.ReadLine()) != null)
                {
                    lineNumber++;

                    string[] ss = line.Split('|');
                    string b_naam = ss[0];
                    string b_industrie = ss[1];
                    string b_sector = ss[2];
                    string b_hoofdkwartier = ss[3];
                    //int b_jaaroprichting = int.Parse(ss[4]);
                    bool gelukt = int.TryParse(ss[4], out int b_jaaroprichting);
                    string b_extra = ss[5];
                    int p_id = int.Parse(ss[6]);
                    string p_voornaam = ss[7];
                    string p_achternaam = ss[8];
                    DateTime p_geboortedatum = DateTime.Parse(ss[9]);
                    string a_gemeente = ss[10];
                    int a_postcode = int.Parse(ss[11]);
                    string a_straat = ss[12];
                    string a_huisnr = ss[13];
                    string p_email = ss[14];

                    errorInfo = string.Join('|', ss[0], ss[7], ss[8]); //nodige info voor eventuele errors: bedrijfnaam, naam en voornaam


                    try
                    {

                        List<string> errors = new();

                        Adres adres = new Adres(a_gemeente, a_postcode, a_straat, a_huisnr, errors); //maak adres

                        Personeel personeel = new Personeel(p_id, p_voornaam, p_achternaam, p_geboortedatum, adres, p_email, errors); //maak personeelslid




                        if (data.ContainsKey(b_naam))//check of bedrijf al bestaat
                        {
                            data[b_naam].VoegPersoneelToe(personeel); //voeg personeel toe

                        }
                        else
                        {
                            Bedrijf bedrijf = new Bedrijf(b_naam, b_industrie, b_sector, b_hoofdkwartier, b_jaaroprichting, b_extra, new List<Personeel>() { personeel }, errors);
                            if (errors.Count == 0)
                                data.Add(b_naam, bedrijf); //maak bedrijf
                        }

                        FoutenVerzamelen(errors, $"Fouten bij het inlezen van gegevens");


                    }

                    catch (BedrijfException ex)
                    {
                        sw.WriteLine($"Lijn {lineNumber}: {errorInfo} => {ex.Message}");
                        if (ex.Errors != null && ex.Errors.Count > 0)
                        {
                            foreach (var err in ex.Errors)
                            {
                                sw.WriteLine($"\t {err}");
                            }
                        }
                    }
                    catch (Exception)
                    { Console.WriteLine(line); }


                }
                return data.Values.ToList();

            }
        }
    }
}


