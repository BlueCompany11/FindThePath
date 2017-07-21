using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindThePath
{
    static public class Algorithms
    {
        static List<List<int>> comb;
        static bool[] used;

        /// <summary>
        /// Method returns permutation of nodes(ints) of the shortest way.
        /// I made an assumption that we don't have any limitations considering edges.
        /// </summary>
        /// <param name="Distnaces"></param>
        /// <param name="Addresses"></param>
        static public int[] NaiveAlogrithm(List<Place> cont, int[,] Distances)
        {
            GetPermutations(cont.Count);
            int shortest = Int32.MaxValue;
            int temp = 0;
            int[] t = new int[cont.Count];
            int[] treturn = new int[cont.Count];
            for (int i = 0; i < comb.Count; ++i)
            {
                temp = 0;
                t = comb[i].ToArray();
                for (int j = 0, len = cont.Count - 1; j < len; ++j)
                {
                    if (t[j] > t[j + 1])
                    {
                        temp += Distances[t[j], t[j + 1]];
                    }
                    else
                    {
                        temp += Distances[t[j + 1], t[j]];
                    }
                }
                if (temp < shortest)
                {
                    treturn = t;
                    shortest = temp;
                }
            }
            //last variable is the distance
            int[] treturn2 = new int[cont.Count + 1];
            for(int i = 0; i < cont.Count; ++i)
            {
                treturn2[i] = treturn[i];
            }
            treturn2[cont.Count] = shortest;
            //foreach(var elem in treturn2)
            //{
            //    Console.Write($"{elem} , ");
            //}
            //Console.WriteLine();
            return treturn2;

        }

        public static void GetPermutations(int size)
        {
            //int size = cont.Count;
            int[] arr = new int[size];
            for (int i = 0; i < size; ++i)
            {
                arr[i] = i;
            }
            used = new bool[arr.Length];
            comb = new List<List<int>>();
            List<int> c = new List<int>();
            GetComb(arr, 0, c);
            //uncomment to test generation of permutations
            //foreach (var item in comb)
            //{
            //    foreach (var x in item)
            //    {
            //        Console.Write(x + ",");
            //    }
            //    Console.WriteLine("");
            //}
        }
        static void GetComb(int[] arr, int colindex, List<int> c)
        {

            if (colindex >= arr.Length)
            {
                comb.Add(new List<int>(c));
                return;
            }
            for (int i = 0; i < arr.Length; i++)
            {
                if (!used[i])
                {
                    used[i] = true;
                    c.Add(arr[i]);
                    GetComb(arr, colindex + 1, c);
                    c.RemoveAt(c.Count - 1);
                    used[i] = false;
                }
            }
        }
        //static public void PrintPerm()
        //{
        //    NaiveAlogrithm(Place.Container,Place.Distances);
        //    Console.WriteLine("Wypisuje adresy");
        //    foreach(var elem in Place.Addresses)
        //    {
        //        Console.WriteLine(elem);
        //    }
        //}
    }
}