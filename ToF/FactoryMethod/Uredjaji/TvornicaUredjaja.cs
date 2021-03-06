﻿using ToF.Model;

namespace ToF.FactoryMethod
{
    /// <summary>
    /// The Creator Abstract Class
    /// </summary>
    abstract class TvornicaUredjaja
    {
        public abstract IUredjajAkcija ProizvediDinamikuUredjaja(Vrsta vrsta);
    }
}
