using System;
using System.Threading.Tasks;
using ToF.Builder;
using ToF.Builder.Prototype;
using ToF.FactoryMethod;
using ToF.Model;
using ToF.Singleton;

namespace ToF
{
    class Program
    {
        static void Main(string[] args)
        {
            try
                {
                var dzTofDirektor = new TofSustavDirector(new Dz1TofSustavBuilder(), args);
                dzTofDirektor.KreirajTofSustav();

                var tofSustavClone1 = dzTofDirektor.TofSustav.Clone();
                tofSustavClone1.Postavke.AlgoritamProvjere = TofTvornicaTestera.Instanca.ProizvediTestera(TipTestera.SEKVENCIJALNI);

                var tofSustavClone2 = dzTofDirektor.TofSustav.Clone();
                tofSustavClone2.Postavke.AlgoritamProvjere = TofTvornicaTestera.Instanca.ProizvediTestera(TipTestera.NASUMICNI);

                Task.WaitAll(
                    dzTofDirektor.TofSustav.Pokreni(),
                    tofSustavClone1.Pokreni(),
                    tofSustavClone2.Pokreni()
                    ); 
            }
            catch (Exception ex)
            {
                AplikacijskiPomagac.Instanca.LogirajIznimku = ex;
            } finally
            {
                AplikacijskiPomagac.Instanca.PohraniLogInformacije();
            }
        }

        private static ITesterUredjaja DohvatiAlgoritamProvjereNasumicno(TofSustavPrototype tofSustavClone)
        {
            var max = Enum.GetNames(typeof(TipTestera)).Length + 1;
            var tip = (TipTestera)(tofSustavClone.GeneratorBrojeva.Next(max));
            return TofTvornicaTestera.Instanca.ProizvediTestera(tip);
        }
    }
}
