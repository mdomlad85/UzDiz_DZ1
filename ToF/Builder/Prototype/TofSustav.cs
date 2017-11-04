using System;
using System.Collections.Generic;
using ToF.Model;

namespace ToF.Builder.Prototype
{
    public class TofSustav : TofSustavPrototype
    {
        public override TofSustavPrototype Clone()
        {
            // return (TofSustavPrototype)MemberwiseClone();
            // ispod je samo jedna verzija kopiranja, opet ostale koristi kao referencu,
            // no ovo je samo primjer i to je dovoljno
            var tofSustav = new TofSustav();
            tofSustav.Aktuatori = Aktuatori;
            tofSustav.GeneratorBrojeva = new Random(Postavke.Sjeme);
            tofSustav.Mjesta = Mjesta;
            tofSustav.Postavke = new Postavke
            {
                AlgoritamProvjere = Postavke.AlgoritamProvjere,
                BrojCiklusaDretve = Postavke.BrojCiklusaDretve,
                DatotekaAktuatora = Postavke.DatotekaAktuatora,
                DatotekaMjesta = Postavke.DatotekaMjesta,
                DatotekaSenzora = Postavke.DatotekaSenzora,
                IzlaznaDatoteka = Postavke.IzlaznaDatoteka,
                Sjeme = Postavke.Sjeme,
                TrajanjeDretveSek = Postavke.TrajanjeDretveSek
            };
            tofSustav.Senzori = Senzori;

            return tofSustav;
        }
    }
}
