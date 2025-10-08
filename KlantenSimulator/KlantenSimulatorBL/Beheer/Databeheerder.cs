using KlantenSimulatorBL.Interfaces;
using KlantenSimulatorBL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KlantenSimulatorBL.Beheer
{
    public class Databeheerder
    {
        private PersoonSimulator persoonSimulator;
        private AdresSimulator adresSimulator;
        private IBestandsLezer bestandsLezer;
        private IBestandsSchrijver bestandsSchrijver;
        public Databeheerder(IBestandsLezer bestandsLezer, IBestandsSchrijver bestandsSchrijver, int minLeeftijd, int maxLeeftijd, int minId, int maxId, 
            string padVoornamenMannen, string padVoornamenVrouwen, string padFamilienamen, int percentLetter, int maxHuisnummer
            , string padStraten, string padPostcodeGemeente, int aantalAdressen) 
        {
            this.bestandsLezer = bestandsLezer;
            this.bestandsSchrijver = bestandsSchrijver;

            var postcodeGemeente = bestandsLezer.LeesPostcodeGemeente(padPostcodeGemeente);
            List<string> straten = bestandsLezer.LeesStraten(padStraten);
            adresSimulator = new AdresSimulator(straten,postcodeGemeente,maxHuisnummer, percentLetter);
            List<Adres> adressen = adresSimulator.GeefAdressen(aantalAdressen);

            List<string> vn = bestandsLezer.LeesNamen(padVoornamenMannen);
            vn.AddRange(bestandsLezer.LeesNamen(padVoornamenVrouwen));
            List<string> fn = bestandsLezer.LeesNamen(padFamilienamen);

            persoonSimulator = new PersoonSimulator(vn,fn, adressen,minLeeftijd, maxLeeftijd,minId,maxId);
        }
        public void SimuleerPersonen(int aantal, string pad)
        {
            var data = persoonSimulator.MaakPersoon(aantal);
            bestandsSchrijver.SchrijfBestand(data, pad);

        }



    }
}
