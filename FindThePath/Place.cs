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

namespace FindThePath
{
    public partial class Place
    {
        string _address { get; set; }
        int _position { get; set; }
        //max amount of places to input
        const int n = 15;   
        static int[,] Distances=new int [n,n];  
        //an output where adresses are in the correct order
        static string[] Addresses = new string[n];
        static List<Place> Container = new List<Place>();    

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
            Console.WriteLine("If you would like to finish press n, otherwise any other letter will do.");
            do
            { 
                address = SetAnAddress();
                position = Container.Count;
                Place NewPlace = new Place(address, position);
                Console.WriteLine("Continue?");
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


        override public string ToString()
        {
            return $"Position={_position}\nAddress = {_address}";
        }
    }
}
