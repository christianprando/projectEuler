using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler.Extensions
{
    public static class ListExtensions
    {
        public static string GetString<T>(this IList<T> list)
        {
            if (list.Count == 0)
            {
                return "[]";
            }

            var str = "[";
            for (var index =0; index < list.Count - 1; index++)
            {
                str += $"{list[index]}, ";
            }

            str += $"{list.Last()}]";

            return str;
        }
    }
}