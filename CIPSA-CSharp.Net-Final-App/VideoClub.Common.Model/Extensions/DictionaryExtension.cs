using System;
using System.Collections.Generic;
using System.Linq;

namespace VideoClub.Common.Model.Extensions
{
    public static class Extensions
    {
        public static void Append<TK, TV>(this IDictionary<TK, TV> source, Dictionary<TK, TV> collection)
        {
            var pairs = collection.ToList();
            pairs.ForEach(pair => source.Add(pair.Key, pair.Value));
        }
    }

}
