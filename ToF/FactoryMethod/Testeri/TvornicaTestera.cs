﻿using ToF.Model;

namespace ToF.FactoryMethod
{
    /// <summary>
    /// The Creator Abstract Class
    /// </summary>
    abstract class TvornicaTestera
    {
        public abstract ITesterUredjaja ProizvediTestera(string nazivtestera);
        public abstract ITesterUredjaja ProizvediTestera(TipTestera tip);
    }
}
