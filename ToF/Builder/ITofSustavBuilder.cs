using ToF.Builder.Prototype;

namespace ToF.Builder
{
    /// <summary>
    /// The 'Builder' interface
    /// </summary>
    interface ITofSustavBuilder
    {
        void UcitajPostavke(object ulazniPodaci);
        void UcitajPodatke();
        void InicijalizirajSustav();

        TofSustavPrototype TofSUstav { get; }
    }
}
