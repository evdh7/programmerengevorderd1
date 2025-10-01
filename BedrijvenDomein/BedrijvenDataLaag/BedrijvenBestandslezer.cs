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

                string line;
                int lineNumber = 0;
                string errorInfo = null;

                static void ThrowIfErrors(List<string> errors,string contextMessage)
                {
                    if (errors != null && errors.Count > 0)
                    {
                        BedrijfException ex = new BedrijfException(contextMessage);
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
                    int b_jaaroprichting = int.Parse(ss[4]);
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
                        List<string> errors2 = new();

                        Adres adres = new Adres(a_gemeente, a_postcode, a_straat, a_huisnr, errors); //maak adres

                        Personeel personeel = new Personeel(p_id, p_voornaam, p_achternaam, p_geboortedatum, adres, p_email, errors); //maak personeelslid


                        

                            if (data.ContainsKey(b_naam))//check of bedrijf al bestaat
                            {
                                bool exists = false;

                                foreach (var p in data[b_naam].Personeel) //check of personeel al bestaat
                                {
                                    if (p.ID == p_id || (p.Voornaam == p_voornaam && p.Achternaam == p_achternaam && p.DateOfBirth == p_geboortedatum))
                                    {
                                        exists = true;
                                        break;
                                    }
                                }

                                if (!exists && errors.Count == 0) //als personeel nog niet bestaat en er zijn geen fouten
                                {
                                    data[b_naam].Personeel.Add(personeel); //voeg personeel toe
                                }
                                else
                                {
                                    sw.WriteLine($"Lijn {lineNumber}: dubbele waarden voor {p_id}{p_voornaam}{p_achternaam} bij {b_naam}");
                                }
                            }
                            else
                            {
                                Bedrijf bedrijf = new Bedrijf(b_naam, b_industrie, b_sector, b_hoofdkwartier, b_jaaroprichting, b_extra, new List<Personeel>() { personeel }, errors);
                                if (errors.Count == 0)         
                                    data.Add(b_naam, bedrijf); //maak bedrijf
                            }

                            ThrowIfErrors(errors, $"Fouten bij het inlezen van gegevens");

                            //if (errors.Count > 0)
                            //{
                            //   BedrijfException ex = new BedrijfException("Fouten bij het inlezen van gegevens");
                            // ex.Errors = errors;
                            //  throw ex;
                            // }
                        
                    }

                    catch (BedrijfException ex)
                    {
                        sw.WriteLine($"Lijn {lineNumber}: {errorInfo} => {ex.Message}");
                        if (ex.Errors != null && ex.Errors.Count > 0)
                        {
                            foreach (var err in ex.Errors)
                            {
                                sw.WriteLine($"\t- {err}");
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


