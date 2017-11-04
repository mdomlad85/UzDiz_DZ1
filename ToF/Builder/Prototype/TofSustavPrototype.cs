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

        public async Task Pokreni()
        {
            var that = this;
            await Task.Run(() =>
            {
                for (int i = 0; i < that.Postavke.BrojCiklusaDretve; i++)
                {
                    AplikacijskiPomagac.Instanca.Logiraj = "Počinje obrada mjesta...";
                    var startTime = DateTime.Now;

                    lock (syncLock)
                    {
                        that.Postavke.AlgoritamProvjere.ProvjeriMjesta(that._mjesta);
                    }
                    that.Mjesta.AktivirajUredjaje();

                    var totalSec = (DateTime.Now - startTime).TotalSeconds;
                    var diff = that.Postavke.TrajanjeDretveSek - totalSec;
                    AplikacijskiPomagac.Instanca.Logiraj = String.Format("...završila obrada mjesta nakon {0} sekundi", totalSec);

                    if (diff > 0)
                    {
                        Thread.Sleep((int)diff * 1000);
                    }
                }
            });
        }
    }
}
