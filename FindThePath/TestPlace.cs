using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindThePath
{
    public partial class Place
    {
        static public void TestContainer()
        {
            foreach (var elem in Place.Container)
            {
                Console.WriteLine(elem.ToString());
            }
        }
        static public void TestDistances()
        {
            for (int i = 0; i < Place.n; ++i)
            {
                for (int j = 0; j < Place.n; ++j)
                {
                    Console.Write("{0} ", Place.Distances[i, j]);
                }
                Console.WriteLine();
            }
        }
        static public void TestAddresses()
        {
            foreach(var elem in Addresses)
            {
                Console.WriteLine(elem);
            }
        }
    }
}
