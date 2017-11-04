using System;
using System.Collections.Generic;
using ToF.Model;
using ToF.Singleton;

/// <summary>
/// A 'ConcreteProduct' class
/// </summary>
namespace ToF.FactoryMethod
{
    class NasumicniTester : ITesterUredjaja
    {
        public void ProvjeriMjesta(List<Mjesto> mjesta)
        {
            var helper = AplikacijskiPomagac.Instanca;
            helper.Logiraj = "Start provjere mjesta nasumičnim testerom";
            var keys = new HashSet<int>();
            int brojMjesta = mjesta.Count;

            do
            {
                var val = 1;
                do
                {
                    val = helper.TofSustav.GeneratorBrojeva.Next(brojMjesta);
                } while (!keys.Add(val));

                mjesta[val].Provjeri();

            } while (keys.Count != brojMjesta);

            helper.Logiraj = "Kraj provjere mjesta nasumičnim testerom";
        }
    }
}

