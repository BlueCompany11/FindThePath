using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//https://www.nuget.org/packages/GoogleMapsApi/
namespace FindThePath
{
    class Program
    {
        static void Main(string[] args)
        {
            Place.GeneratePlaces();
            Place.TestContainer();
            Place.TestDistances();
            Console.ReadKey();
           
        }
    }
}
