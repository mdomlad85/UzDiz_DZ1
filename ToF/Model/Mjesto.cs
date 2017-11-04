using System.Collections.Generic;
using ToF.Iznimke;
using ToF.Singleton;

namespace ToF.Model
{
    public class Mjesto
    {
        public string Naziv { get; set; }

        public Tip Tip { get; set; }

        public Uredjaj[] Senzori { get; set; }

        public Uredjaj[] Aktuatori { get; set; }

        public Mjesto() { }

        public Mjesto(string[] attrs)
        {
            try
            {
                Naziv = attrs[0];
                Tip = (Tip)int.Parse(attrs[1]);
                Senzori = new Uredjaj[int.Parse(attrs[2])];
                Aktuatori = new Uredjaj[int.Parse(attrs[3])];
            }
            catch
            {
                throw new LosRedakIzDatoteke();
            }
        }

        internal void Provjeri()
        {
            var helper = AplikacijskiPomagac.Instanca;
            ProvjeriUredjaje(Senzori, helper.TofSustav.Senzori);
            ProvjeriUredjaje(Aktuatori, helper.TofSustav.Aktuatori);
        }

        private void ProvjeriUredjaje(Uredjaj[] koristeniUredjaji, List<Uredjaj> raspoloziviUredjaji)
        {
            var helper = AplikacijskiPomagac.Instanca;
            foreach (var uredjaj in koristeniUredjaji)
            {
                if (uredjaj.JeZdrav() == 1)
                {
                    helper.Logiraj = string.Format("Uređaj {0} je ispravan", uredjaj.Naziv);
                }
                else
                {
                    helper.Logiraj = string.Format("Uređaj {0} je neispravan", uredjaj.Naziv);
                    var zamjenski = koristeniUredjaji.Zamijeni(uredjaj, raspoloziviUredjaji.ToArray());
                    helper.Logiraj = string.Format("Uređaj {0} je zamijenjen sa {1}", uredjaj.Naziv, zamjenski.Naziv);
                }
            }
        }
    }
}
