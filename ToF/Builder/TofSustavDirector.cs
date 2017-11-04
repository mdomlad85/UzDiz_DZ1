using ToF.Builder.Prototype;

namespace ToF.Builder
{
    /// <summary>
    /// The 'Director' class
    /// </summary>
    class TofSustavDirector
    {
        private readonly ITofSustavBuilder _tofBuilder;

        private readonly object _ulazniPodaci;

        public TofSustavDirector(ITofSustavBuilder tofBuilder, object ulazniPodaci)
        {
            _tofBuilder = tofBuilder;
            _ulazniPodaci = ulazniPodaci;
        }

        public void KreirajTofSustav()
        {
            _tofBuilder.UcitajPostavke(_ulazniPodaci);
            _tofBuilder.UcitajPodatke();
            _tofBuilder.InicijalizirajSustav();
        }

        public TofSustavPrototype TofSustav => _tofBuilder.TofSUstav;
    }
}
