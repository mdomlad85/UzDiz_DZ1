using System;
using System.IO;
using System.Text;
using ToF.Builder.Prototype;

namespace ToF.Singleton
{
    /// <summary>
    /// The 'Singleton' class
    /// </summary>
    public class AplikacijskiPomagac
    {
        private static AplikacijskiPomagac _instance;

        private StringBuilder _log;

        // Lock synchronization object
        private static object staticSyncLock = new object();

        private object syncLock = new object();

        public static AplikacijskiPomagac Instanca
        {
            get
            {
                // Support multithreaded applications through
                // 'Double checked locking' pattern which (once
                // the instance exists) avoids locking each
                // time the method is invoked
                if (_instance == null)
                {
                    lock (staticSyncLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new AplikacijskiPomagac();
                        }
                    }
                }

                return _instance;
            }

        }

        private AplikacijskiPomagac()
        {
            _log = new StringBuilder();
        }      

        public string Logiraj
        {
            set
            {
                lock (syncLock)
                {
                    var msg = string.Format("{0}\t{1}", DateTime.Now, value);
                    Console.WriteLine(msg);
                    _log.AppendLine(msg);
                }
            }
        }

        public Exception LogirajIznimku
        {
            set
            {
                lock (syncLock)
                {
                    var msg = string.Format("{0}\t{1}", DateTime.Now, value.Message);
                    Console.WriteLine(msg);
                    _log.AppendLine(msg);
                }
            }
        }

        public void PohraniLogInformacije()
        {
            File.WriteAllText(TofSustav.Postavke.IzlaznaDatoteka, _log.ToString());
        }

        public TofSustavPrototype TofSustav { get; set; }
    }
}
