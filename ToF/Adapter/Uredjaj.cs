using ToF.Iznimke;
using ToF.Singleton;

namespace ToF.Model
{
    /// <summary>
    /// The 'Target' class
    /// </summary>
    public class Uredjaj
    {
        public string Naziv { get; set; }

        public Tip Tip { get; set; }

        public Vrsta Vrsta { get; set; }

        public string Komentar { get; set; }

        public Uredjaj(string[] attrs)
        {
            try
            {
                Naziv = attrs[0];
                Tip = (Tip)int.Parse(attrs[1]);
                Vrsta = (Vrsta)int.Parse(attrs[2]);
                Min = double.Parse(attrs[3].Replace('.', ','));
                Max = double.Parse(attrs[4].Replace('.', ','));
                Komentar = attrs[5];
                TrenutnaVrijednost = Min;
            }
            catch
            {
                throw new LosRedakIzDatoteke();
            }
        }

        public void Inicijaliziraj(string poruka)
        {
            System.Console.WriteLine(poruka);
            JeZdrav(1);
        }

        /// <summary>
        /// Ovisno o vrsti polje će zaprimiti takvu vrijednost (cjelobrojno, boolean, ...) 
        /// </summary>
        public double Min { get; set; }

        /// <summary>
        /// Ovisno o vrsti polje će zaprimiti takvu vrijednost (cjelobrojno, boolean, ...) 
        /// </summary>
        public double Max { get; set; }

        /// <summary>
        /// Ovisno o izvršavanju akcije aktuator će poprimiti vrijednost prema 
        /// pravilima izvršavanja dok će se senzoru automatski pridjeliti
        /// </summary>
        public double TrenutnaVrijednost { get; set; }

        /// <summary>
        /// Ako je vrijednost istinita onda ide od max prema min, vrijedi i obrat tvrdnje
        /// </summary>
        public bool Obrnuto => TrenutnaVrijednost == Max;

        private int _neispravanZaRedom;

        private bool _jeIspravan = true;

        public bool JeIspravan => _jeIspravan;

        public int JeZdrav(int maxNezdrav = 3)
        {
            if (_jeIspravan)
            {
                if ((AplikacijskiPomagac.Instanca.TofSustav.GeneratorBrojeva.NextDouble() * 100) <= 90)
                {
                    _neispravanZaRedom = 0;
                    return 1;
                }
                else
                {
                    _neispravanZaRedom++;
                    if (_neispravanZaRedom == maxNezdrav)
                    {
                        _jeIspravan = false;
                    }
                    return 0;
                }
            }
            return 0;
        }

        public virtual void OdradiPosao()
        {
            AplikacijskiPomagac.Instanca.Logiraj = string.Format("Vrijednost {0} je {1} {2}", Naziv, TrenutnaVrijednost, Komentar);
        }
    }
}
