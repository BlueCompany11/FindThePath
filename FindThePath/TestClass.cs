using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoogleMapsApi;
using GoogleMapsApi.Entities.Common;
using GoogleMapsApi.Entities.Directions.Request;
using GoogleMapsApi.Entities.Directions.Response;
using GoogleMapsApi.Entities.Geocoding.Request;
using GoogleMapsApi.Entities.Geocoding.Response;
using GoogleMapsApi.StaticMaps;
using GoogleMapsApi.StaticMaps.Entities;
using Newtonsoft.Json.Linq;

namespace FindThePath
{
    class TestClass
    {
        static string[] t = new string[3];
        public static void fun()
        {
            for(int i = 0; i < 3; ++i)
            {
                t[i] = i.ToString();
            }
        }
        public static void fun2()
        {
            foreach(var elem in t)
            {
                Console.WriteLine(elem);
            }
        }
        
        
        static public void flist()
        {
            List<List<int>> a = new List<List<int>>();
            a.Add(new List<int> {1,2,3,4 });
            a[0] = new List<int> { 1, 2, 3, 4 };
            int[] t = new int[4];
            t = a[0].ToArray();
        }
            
        
    }
}

