using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ToF.Builder.Prototype;
using ToF.FactoryMethod;
using ToF.Iznimke;
using ToF.Model;
using ToF.Singleton;

namespace ToF.Builder
{
    /// <summary>
    /// The 'ConcreteBuilder' class
    /// </summary>
    public class Dz1TofSustavBuilder : ITofSustavBuilder
    {
        TofSustavPrototype _tofSustav = new TofSustav();

        private bool _sustavSpreman = false;
        public void InicijalizirajSustav()
        {
            foreach (var mjesto in _tofSustav.Mjesta)
            {
                InicijalizirajUredjaje(mjesto.Senzori);
                InicijalizirajUredjaje(mjesto.Aktuatori);
            }
            _sustavSpreman = true;
        }

        #region Inicijaliziraj sustav helperi
        private void InicijalizirajUredjaje(Uredjaj[] uredjaji)
        {
            foreach (var uredjaj in uredjaji)
            {
                if (uredjaj.JeZdrav(1) == 1)
                {
                    AplikacijskiPomagac.Instanca
                        .Logiraj = string.Format("Uređaj {0} je ispravan", uredjaj.Naziv);
                }
                else
                {
                    AplikacijskiPomagac.Instanca
                        .Logiraj = string.Format("Uređaj {0} je neispravan", uredjaj.Naziv);
                }
            }
        }
        #endregion

        public void UcitajPodatke()
        {
            UcitajSenzore();
            UcitajAktuatore();
            UcitajMjesta();
        }

        #region Ucitavanje podataka helperi
        private void UcitajSenzore()
        {
            _tofSustav.Senzori = new List<Uredjaj>();
            string[] linije = _tofSustav.Postavke.DatotekaSenzora.ReadAllLinesExceptFirst();
            foreach (var linija in linije)
            {
                try
                {
                    _tofSustav.Senzori.Add(new Uredjaj(linija.Split(';')));
                }
                catch (Exception ex)
                {
                    AplikacijskiPomagac.Instanca.LogirajIznimku = ex;
                }
            }
        }

        private void UcitajAktuatore()
        {
            _tofSustav.Aktuatori = new List<Uredjaj>();
            string[] linije = _tofSustav.Postavke.DatotekaAktuatora.ReadAllLinesExceptFirst();
            foreach (var linija in linije)
            {
                try
                {
                    _tofSustav.Aktuatori.Add(new AkcijskiUredjaj(linija.Split(';')));
                }
                catch (Exception ex)
                {
                    AplikacijskiPomagac.Instanca.LogirajIznimku = ex;
                }
            }
        }

        private void UcitajMjesta()
        {
            _tofSustav.Mjesta = new List<Mjesto>();
            string[] linije = _tofSustav.Postavke.DatotekaMjesta.ReadAllLinesExceptFirst();
            foreach (var linija in linije)
            {
                try
                {
                    var mjesto = new Mjesto(linija.Split(';'));
                    UcitajSenzoreZaMjesto(mjesto);
                    UcitajAktuatoreZaMjesto(mjesto);

                    _tofSustav.Mjesta.Add(mjesto);
                }
                catch (Exception ex)
                {
                    AplikacijskiPomagac.Instanca.LogirajIznimku = ex;
                }
            }
        }

        #region Ucitaj mjesta helperi
        private void UcitajSenzoreZaMjesto(Mjesto mjesto)
        {
            var senzori = _tofSustav.Senzori
                .FindAll(x => x.Tip == mjesto.Tip || x.Tip == Tip.VANJSKI_I_UNUTARNJI)
                .ToArray();

            if (mjesto.Senzori.Count() > senzori.Count())
            {
                throw new NemaDostaUredjaja("Nema dosta Senzora");
            }
            else if (mjesto.Senzori.Count() == senzori.Count())
            {
                Array.Copy(senzori, mjesto.Senzori, senzori.Count());
            }
            else
            {
                var keys = new HashSet<int>();
                var max = senzori.Count();
                for (int i = 0; i < mjesto.Senzori.Length; i++)
                {
                    var haveNewKey = false;
                    var key = -1;
                    while (!haveNewKey)
                    {
                        key = _tofSustav.GeneratorBrojeva.Next(max);
                        haveNewKey = keys.Add(key);
                    }
                    mjesto.Senzori[i] = senzori[key];
                }
            }
        }

        private void UcitajAktuatoreZaMjesto(Mjesto mjesto)
        {
            var aktuatori = _tofSustav.Aktuatori
                .FindAll(x => x.Tip == mjesto.Tip || x.Tip == Tip.VANJSKI_I_UNUTARNJI)
                .ToArray();

            if(mjesto.Aktuatori.Count() > aktuatori.Count())
            {
                throw new NemaDostaUredjaja("Nema dosta Aktuatora");
            } else if (aktuatori.Count() == mjesto.Aktuatori.Count())
            {
                Array.Copy(aktuatori, mjesto.Aktuatori, aktuatori.Count());
            } else
            {
                var keys = new HashSet<int>();
                var max = aktuatori.Count();
                for (int i = 0; i < mjesto.Aktuatori.Length; i++)
                {
                    var haveNewKey = false;
                    var key = -1;
                    while (!haveNewKey)
                    {
                        key = _tofSustav.GeneratorBrojeva.Next(max);
                        haveNewKey = keys.Add(key);
                    }
                    mjesto.Aktuatori[i] = aktuatori[key];
                }
            }            
        }
        #endregion

        #endregion

        public void UcitajPostavke(object ulazniPodaci)
        {
            if (ulazniPodaci is string[] && IsIspravnePostavke((string[])ulazniPodaci))
            {
                var args = (string[])ulazniPodaci;
                _tofSustav.Postavke.Sjeme = int.Parse(args[1]);
                _tofSustav.GeneratorBrojeva = new Random(_tofSustav.Postavke.Sjeme);
                _tofSustav.Postavke.TrajanjeDretveSek = int.Parse(args[6]);
                _tofSustav.Postavke.BrojCiklusaDretve = int.Parse(args[7]);
                _tofSustav.Postavke.DatotekaMjesta = args[2];
                _tofSustav.Postavke.DatotekaSenzora = args[3];
                _tofSustav.Postavke.DatotekaAktuatora = args[4];
                _tofSustav.Postavke.IzlaznaDatoteka = args[8];
                AplikacijskiPomagac.Instanca.TofSustav = _tofSustav;
            }
            else
            {
                throw new NeispravniUlazniArgumenti();
            }
        }

        #region Ucitaj postavke helperi
        private bool IsIspravnePostavke(string[] args)
        {
            int outVal;
            return args.Length == 9
                && args[0].Length >= 3
                && int.TryParse(args[1], out outVal)
                && FilesExists(args[2], args[3], args[4])
                && TesterClassExists(args[5])
                && int.TryParse(args[6], out outVal)
                && int.TryParse(args[7], out outVal);
        }

        private bool TesterClassExists(string className)
        {
            try
            {
                _tofSustav.Postavke.AlgoritamProvjere = TofTvornicaTestera.Instanca.ProizvediTestera(className);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool FilesExists(params string[] vals)
        {
            foreach (var val in vals)
            {
                if (!File.Exists(val))
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        public TofSustavPrototype TofSUstav
        {
            get
            {
                if (_sustavSpreman)
                {
                    return _tofSustav;
                }
                else
                {
                    throw new TofSustavNijeSpreman();
                }
            }
        }
    }
}
