using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ToF.Model;
using ToF.Singleton;

namespace ToF.Builder.Prototype
{
    /// <summary>
    /// The 'Product' class
    /// </summary>
    public abstract class TofSustavPrototype
    {
        public abstract TofSustavPrototype Clone();

        private object syncLock = new object();

        private List<Mjesto> _mjesta = new List<Mjesto>();

        private Postavke _postavke = new Postavke();

        private List<Uredjaj> _senzori = new List<Uredjaj>();

        private List<Uredjaj> _aktuatori = new List<Uredjaj>();

        private Random _generatorBrojeva;

        public TofSustavPrototype() { }

        public List<Mjesto> Mjesta
        {
            get
            {
                lock (syncLock)
                {
                    return _mjesta;
                }
            }

            internal set
            {
                lock (syncLock)
                {
                    _mjesta = value;
                }
            }
        }

        public Postavke Postavke
        {
            get
            {
                lock (syncLock)
                {
                    return _postavke;
                }
            }

            internal set
            {
                lock (syncLock)
                {
                    _postavke = value;
                }
            }
        }

        public List<Uredjaj> Senzori
        {
            get
            {
                lock (syncLock)
                {
                    return _senzori;
                }
            }

            internal set
            {
                lock (syncLock)
                {
                    _senzori = value;
                }
            }
        }

        public List<Uredjaj> Aktuatori
        {
            get
            {
                lock (syncLock)
                {
                    return _aktuatori;
                }
            }

            internal set
            {
                lock (syncLock)
                {
                    _aktuatori = value;
                }
            }
        }

        public Random GeneratorBrojeva
        {
            get
            {
                lock (syncLock)
                {
                    return _generatorBrojeva;
                }
            }

            internal set
            {
                lock (syncLock)
                {
                    _generatorBrojeva = value;
                }
            }
        }

        public void Pokreni()
        {
            for (int i = 0; i < Postavke.BrojCiklusaDretve; i++)
            {
                if (DoHardWork())
                {
                    AplikacijskiPomagac.Instanca.Statistika.UspjesnihCiklusa++;
                    AplikacijskiPomagac.Instanca.Logiraj = string.Format("Uspješno završen {0}. ciklus ", i + 1);
                } else
                {
                    AplikacijskiPomagac.Instanca.Statistika.NeuspjesnihCiklusa++;
                    AplikacijskiPomagac.Instanca.Logiraj = string.Format("Isteklo je vrijeme u {0}. ciklusu ", i + 1);
                }
            }
            AplikacijskiPomagac.Instanca.Statistika.ProsjecnoTrajanjeCiklusa /= Postavke.BrojCiklusaDretve;
        }

        private bool DoHardWork()
        {
            Thread workerThread = new Thread(new ThreadStart(Run));
            workerThread.Start();
            bool finished = workerThread.Join(new TimeSpan(0, 0, Postavke.TrajanjeDretveSek));

            if (!finished)
            {
                workerThread.Abort();
                AplikacijskiPomagac.Instanca.Statistika.ProsjecnoTrajanjeCiklusa += Postavke.TrajanjeDretveSek;
            }

            return finished;
        }

        public void Run()
        {
            AplikacijskiPomagac.Instanca.Logiraj = "Počinje obrada mjesta...";
            var startTime = DateTime.Now;

            lock (syncLock)
            {
                Postavke.AlgoritamProvjere.ProvjeriMjesta(_mjesta);
            }
            Mjesta.AktivirajUredjaje();

            var totalSec = (DateTime.Now - startTime).TotalSeconds;
            var diff = Postavke.TrajanjeDretveSek - totalSec;
            AplikacijskiPomagac.Instanca.Logiraj = string.Format("...završila obrada mjesta nakon {0} sekundi", totalSec);
            AplikacijskiPomagac.Instanca.Statistika.ProsjecnoTrajanjeCiklusa += diff;
        }
    }
}
