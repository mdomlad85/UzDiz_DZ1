using System;
using System.Threading.Tasks;
using ToF.Builder;
using ToF.Builder.Prototype;
using ToF.FactoryMethod;
using ToF.Model;
using ToF.Singleton;
using ToF.Vendor;

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

                dzTofDirektor.TofSustav.Pokreni();

                /*
                var tofSustavClone = dzTofDirektor.TofSustav.Clone();
                tofSustavClone.Postavke.AlgoritamProvjere = TofTvornicaTestera.Instanca.ProizvediTestera(TipTestera.SEKVENCIJALNI);
                tofSustavClone..Pokreni();
                */
                /*
                var tofSustavClone = dzTofDirektor.TofSustav.Clone();
                tofSustavClone1.Postavke.AlgoritamProvjere = TofTvornicaTestera.Instanca.ProizvediTestera(TipTestera.NASUMICNI);
                tofSustavClone..Pokreni();
                 */
                /*
                var tofSustavClone = dzTofDirektor.TofSustav.Clone();
                tofSustavClone.Postavke.AlgoritamProvjere = TofTvornicaTestera.Instanca.ProizvediTestera(TipTestera.HIBRIDNI);
                tofSustavClone..Pokreni();
                */
            }
            catch (Exception ex)
           {
               AplikacijskiPomagac.Instanca.LogirajIznimku = ex;
           } finally
           {
               AplikacijskiPomagac.Instanca.PohraniLogInformacije();
           }
            var table = new ConsoleTable("#Uspješnih ciklusa", "#Neuspješnih ciklusa", "#Prosječno izvršavanje");
            table.AddRow(
                AplikacijskiPomagac.Instanca.Statistika.UspjesnihCiklusa,
                AplikacijskiPomagac.Instanca.Statistika.NeuspjesnihCiklusa,
                AplikacijskiPomagac.Instanca.Statistika.ProsjecnoTrajanjeCiklusa.ToString("N2") + " sec"
                );

            table.Write(Format.Alternative);
            Console.ReadKey();
       }

       private static ITesterUredjaja DohvatiAlgoritamProvjereNasumicno(TofSustavPrototype tofSustavClone)
       {
           var max = Enum.GetNames(typeof(TipTestera)).Length + 1;
           var tip = (TipTestera)(tofSustavClone.GeneratorBrojeva.Next(max));
           return TofTvornicaTestera.Instanca.ProizvediTestera(tip);
       }
   }
}
