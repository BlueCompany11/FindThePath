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
        static public void SetAPlace(string address)
        { 
            GeocodingRequest geocodeRequest = new GeocodingRequest()
            {
                Address = address,
            };
            var geocodingEngine = GoogleMaps.Geocode;
            GeocodingResponse geocode = geocodingEngine.Query(geocodeRequest);
            Console.WriteLine(geocode);
        }
        static public IEnumerable<IEnumerable<T>>
GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }
        public static void fun2()
        {
            IEnumerable<IEnumerable<int>> result =
    GetPermutations(Enumerable.Range(1, 3), 3);
            foreach(var elem in result)
            {
                Console.WriteLine(result.ToString());
            }
        }
    }
}
/*
 * TODO:
 * ADD an algorithm add sql
 */