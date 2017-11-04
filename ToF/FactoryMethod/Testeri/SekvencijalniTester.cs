using System;
using System.Collections.Generic;
using ToF.Model;
using ToF.Singleton;

/// <summary>
/// A 'ConcreteProduct' class
/// </summary>
namespace ToF.FactoryMethod
{
    class SekvencijalniTester : ITesterUredjaja
    {
        public void ProvjeriMjesta(List<Mjesto> mjesta)
        {
            var helper = AplikacijskiPomagac.Instanca;
            helper.Logiraj = "Start provjere mjesta nasumičnim testerom";
            var keys = new HashSet<int>();

            foreach (var mjesto in mjesta)
            {
                mjesto.Provjeri();
            }

            helper.Logiraj = "Kraj provjere mjesta nasumičnim testerom";
        }
    }
}
