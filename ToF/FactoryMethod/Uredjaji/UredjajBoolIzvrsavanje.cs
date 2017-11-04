using ToF.Model;

namespace ToF.FactoryMethod
{
    /// <summary>
    /// A 'ConcreteProduct' class
    /// </summary>
    class UredjajBoolIzvrsavanje : IUredjajAkcija
    {
        public void Izvrsi(Uredjaj uredjaj)
        {
            uredjaj.TrenutnaVrijednost = uredjaj.TrenutnaVrijednost == 0 ? 1 : 0;
        }
    }
}
