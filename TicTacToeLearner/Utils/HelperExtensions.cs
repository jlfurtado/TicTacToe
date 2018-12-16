using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public static class HelperExtensions
    {
        private static readonly Random rand = new Random();

        public static T Random<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.ElementAt(rand.Next(enumerable.Count()));
        }

        public static IEnumerable<T> TakeRandom<T>(this IEnumerable<T> enumerable, out T element)
        {
            int toTake = enumerable.RandomIndex();
            element = enumerable.ElementAt(toTake);
            return enumerable.Take(toTake).Concat(enumerable.Skip(toTake + 1));
        }

        public static int RandomIndex<T>(this IEnumerable<T> enumerable)
        {
            return rand.Next(enumerable.Count());
        }
    }
}
