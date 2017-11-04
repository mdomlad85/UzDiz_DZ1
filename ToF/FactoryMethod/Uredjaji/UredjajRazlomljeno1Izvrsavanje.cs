using System;
using ToF.Model;
using ToF.Singleton;

namespace ToF.FactoryMethod
{
    /// <summary>
    /// A 'ConcreteProduct' class
    /// </summary>
    class UredjajRazlomljeno1Izvrsavanje : IUredjajAkcija
    {
        public void Izvrsi(Uredjaj uredjaj)
        {
            if (uredjaj.Obrnuto)
            {               
                uredjaj.TrenutnaVrijednost -= dohvatiVrijednost(uredjaj.TrenutnaVrijednost - uredjaj.Min);
            } else
            {
                uredjaj.TrenutnaVrijednost += dohvatiVrijednost(uredjaj.Max - uredjaj.TrenutnaVrijednost);
            }
        }

        private double dohvatiVrijednost(double max)
        {
            ///<summary>
            /// Minus jedan je iz razloga što se sada uzima u obzir i decimalni 
            /// dio pa njega i izostavljam iz cjelobrojnog dijela
            /// </summary>
            int currMax = (int)max - 1;
            double vrijednost = AplikacijskiPomagac.Instanca.TofSustav.GeneratorBrojeva.Next(currMax);
            vrijednost += Math.Round(AplikacijskiPomagac.Instanca.TofSustav.GeneratorBrojeva.NextDouble(), 1);

            return vrijednost;
        }
    }
}
