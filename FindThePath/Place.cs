using Newtonsoft.Json.Linq;
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
using System.IO;

namespace FindThePath
{
    public partial class Place
    {
        string _address { get; set; }
        int _position { get; set; }
        //if there is no prearranged first and last place the variable is false
        public static bool _sophisticated { get; set; }
        //max amount of places to input
        const int n = 15;
        public static int[,] Distances = new int[n, n];    
        //an output where adresses are in the correct order
        public static string[] Addresses = new string[n];  
        public static List<Place> Container = new List<Place>();

        public static void FindThePath()
        {
            GeneratePlaces();
            PrintAddresses(InOrderAddresses(Algorithms.NaiveAlogrithm(Container, Distances)));
        }

        public Place(string address,int position)
        {
            _address = address;
            //should start with 0 for the first object
            _position = position;
            Container.Add(this);
            UpdateDistances(_position);
        }


        public static void GeneratePlaces()
        {
            char finisher;
            string address;
            int position;
            Console.WriteLine("Type the street, it's number, and the city.");
            Console.WriteLine("When asked to finish press n to stop inserting data" +
                ", otherwise any other letter will do to continue.");
            Console.WriteLine("Do you have in mind the first and the last place to visit?");
            Console.WriteLine("If yes enter the first place as first and the last as the second.");
            Console.Write("Y/N? ");
            char decision=Console.ReadKey().KeyChar;
            Console.WriteLine();
            if (decision.ToString().ToUpper() == "Y")
            {
                _sophisticated = true;
            }
            do
            { 
                address = SetAnAddress();
                position = Container.Count;
                Place NewPlace = new Place(address, position);
                Console.Write("Continue? ");
                finisher = Console.ReadKey().KeyChar;
                Console.WriteLine();
            } while (finisher != 'n' && finisher != 'N');
            Console.WriteLine("Finding the shortest way...");
            //at the end add this to sql
        }


        static public string SetAnAddress()
        {
            Console.WriteLine("Set the destination");
            string address = Console.ReadLine();
            return address;
        }


        /// <summary>
        /// Doing this update during constructor gives us 
        /// a lower triangular matrix filled with distances
        /// </summary>
        /// <param name="position"></param>
        static void UpdateDistances(int position)
        {
            for(int i = 0; i < n; i++)
            {
                if (i<Container.Count)
                {
                    Distances[position, i] = Distance(Container[position], Container[i]);
                }
            }
        }

        public static void PrintAddresses(int distance)
        {
            for (int i = 0; i < Container.Count; ++i)
            {
                Console.WriteLine($"{i}. {Addresses[i]}");
            }
            Console.WriteLine("Total distance is {0} meters", distance);
        }
        /// <summary>
        /// An argument is an array with permutations for the shortest way.
        /// That distance is placed on the last position of the array.
        /// </summary>
        /// <param name="t"></param>
        public static int InOrderAddresses(int[] t)
        {
            int size = t.Length - 1;//last position is the distance 
            for (int i = 0; i < size; ++i)
            {
                Addresses[i] = Container[t[i]]._address;
            }
            return t[size];
        }

        /// <summary>
        /// Reads the distance from google maps in meters
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        static int Distance(Place x, Place y)
        {
            string origin=x._address;
            string destination=y._address;
            System.Threading.Thread.Sleep(1000);
            int distance = 0;
            //string from = origin.Text;
            //string to = destination.Text;
            string url = "http://maps.googleapis.com/maps/api/directions/json?origin=" + origin + "&destination=" + destination + "&sensor=false";
            string requesturl = url;
            //string requesturl = @"http://maps.googleapis.com/maps/api/directions/json?origin=" + from + "&alternatives=false&units=imperial&destination=" + to + "&sensor=false";
            string content = fileGetContents(requesturl);
            JObject o = JObject.Parse(content);
            try
            {
                distance = (int)o.SelectToken("routes[0].legs[0].distance.value");
                return distance;
            }
            catch
            {
                return distance;
            }
        }


        static string fileGetContents(string fileName)
        {
            string sContents = string.Empty;
            string me = string.Empty;
            try
            {
                if (fileName.ToLower().IndexOf("http:") > -1)
                {
                    System.Net.WebClient wc = new System.Net.WebClient();
                    byte[] response = wc.DownloadData(fileName);
                    sContents = System.Text.Encoding.ASCII.GetString(response);

                }
                else
                {
                    System.IO.StreamReader sr = new System.IO.StreamReader(fileName);
                    sContents = sr.ReadToEnd();
                    sr.Close();
                }
            }
            catch { sContents = "unable to connect to server "; }
            return sContents;
        }

        public static void SaveToFile()
        {
            string strPath = Environment.GetFolderPath(
                         System.Environment.SpecialFolder.DesktopDirectory);
           
            using (StreamWriter outputFile = new StreamWriter(strPath + @"\The shortest path.txt"))
            {
                foreach (string line in Addresses)
                    outputFile.WriteLine(line);
            }
        }

        override public string ToString()
        {
            return $"Position={_position}\nAddress = {_address}";
        }
    }
}
