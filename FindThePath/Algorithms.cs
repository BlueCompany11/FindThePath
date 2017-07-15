using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindThePath
{
    static public class Algorithms
    {
        /// <summary>
        /// Method returns permutation of nodes of the shortest way.
        /// We made an assumption that we don't have any limitations considering edges.
        /// Algorithm starts with giving all the permutations from 1-number of places.
        /// Then we find the lowest number after adding edges of permutations.
        /// </summary>
        /// <param name="Distnaces"></param>
        /// <param name="Addresses"></param>
        static public int NaiveAlogrithm(double[,] Distnaces, string[] Addresses)
        {
            
            return 0;
            
        }
        /*
         GeneratePermutations(int tab[20])
             */
        /// <summary>
        /// Provides walking through all permutations.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        static private void GeneratePermutations<T>(T t) where T:IEnumerable<T>
        {

        }
        //void perm(char* s, int n, int i)
        //{
        //    if (i >= n - 1) print(s);
        //    else
        //    {
        //        perm(s, n, i + 1);
        //        for (int j = i + 1; j < n; j++)
        //        {
        //            swap(s[i], s[j]);
        //            perm(s, n, i + 1);
        //            swap(s[i], s[j]);
        //        }
        //    }
        //}
    }
}
