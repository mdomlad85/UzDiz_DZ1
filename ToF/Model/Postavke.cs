using ToF.FactoryMethod;

namespace ToF.Model
{
    public class Postavke
    {
        public int Sjeme { get; set; }

        public string DatotekaMjesta { get; set; }

        public string DatotekaSenzora { get; set; }

        public string DatotekaAktuatora { get; set; }

        public ITesterUredjaja AlgoritamProvjere { get; set; }

        public int TrajanjeDretveSek { get; set; }

        public int BrojCiklusaDretve { get; set; }

        public string IzlaznaDatoteka { get; set; }
    }
}
