using System;
using System.Collections.Generic;
using System.Linq;
using ToF.Model;
using ToF.Singleton;

namespace ToF
{
    static class TofIEnumerableExtension
    {
        public static Uredjaj Zamijeni(this Uredjaj[] uredjaji, Uredjaj pokvarenUredjaj, Uredjaj[] zamjenskiUredjaji)
        {
            var pokvareniIndex = uredjaji.ToList().IndexOf(pokvarenUredjaj);
            if (pokvareniIndex != -1)
            {
                var max = zamjenskiUredjaji
                    .Where(x => x.Tip == pokvarenUredjaj.Tip || x.Tip == Tip.VANJSKI_I_UNUTARNJI)
                    .Count() - 1;

                var zamjenskiIndex = AplikacijskiPomagac
                    .Instanca
                    .TofSustav
                    .GeneratorBrojeva
                    .Next(max);

                var zamjenskiUredjaj = zamjenskiUredjaji[zamjenskiIndex];
                uredjaji[pokvareniIndex] = zamjenskiUredjaj;

                return zamjenskiUredjaj;
            }
            return null;
        }
        public static void AktivirajUredjaje(this List<Mjesto> mjesta)
        {
            foreach (var mjesto in mjesta)
            {
                AplikacijskiPomagac.Instanca.Logiraj = string.Format("Počinjem s obradom mjesta {0}", mjesto.Naziv);
                foreach (var senzor in mjesto.Senzori)
                {
                    lock (senzor)
                    {
                        senzor.OdradiPosao();
                    }
                }

                foreach (var aktuator in mjesto.Aktuatori)
                {
                    lock (aktuator)
                    {
                        aktuator.OdradiPosao();
                    }
                }
                AplikacijskiPomagac.Instanca.Logiraj = string.Format("Završavam s obradom mjesta {0}\n", mjesto.Naziv);
            }
        }
    }
}
