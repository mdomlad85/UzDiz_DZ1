using ToF.FactoryMethod;

namespace ToF.Model
{
    /// <summary>
    /// The 'Adapter' class
    /// </summary>
    public class AkcijskiUredjaj : Uredjaj
    {
        public AkcijskiUredjaj(string[] attrs) : base(attrs) { }

        public override void OdradiPosao()
        {
            /// <summary>
            /// The 'Adaptee' class
            /// Adapter use the factory for creating objects of several classes
            /// </summary>
            TofTvornicaUredjaja
                .Instanca
                .ProizvediDinamikuUredjaja(Vrsta)
                .Izvrsi(this);
            base.OdradiPosao();
        }
    }
}
