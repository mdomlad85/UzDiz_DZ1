using System;
using System.Collections.Generic;
using ToF.Model;
using ToF.Singleton;

/// <summary>
/// A 'ConcreteProduct' class
/// </summary>
namespace ToF.FactoryMethod
{
    class HibridniTester : ITesterUredjaja
    {
        public void ProvjeriMjesta(List<Mjesto> mjesta)
        {
            var helper = AplikacijskiPomagac.Instanca;
            helper.Logiraj = "Start provjere mjesta hibridnim testerom";
            var keys = new HashSet<int>();
            int brojMjesta = mjesta.Count / 2;

            do
            {
                var val = 1;
                do
                {
                    val = helper.TofSustav.GeneratorBrojeva.Next(brojMjesta);
                } while (!keys.Add(val));

                mjesta[val].Provjeri();

            } while (keys.Count != brojMjesta);

            for (int i = 0; i < mjesta.Count; i++)
            {
                if (keys.Add(i))
                {
                    mjesta[i].Provjeri();
                }
            }

            helper.Logiraj = "Kraj provjere mjesta hibridnim testerom";
        }
    }
}
