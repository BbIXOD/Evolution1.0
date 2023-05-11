using System;
using System.Collections.Generic;

namespace MyExtensions {
    public static class MyExtensions
    {
        public static T FindBest<T>(this IEnumerable<T> collection, Func<T, float> function)
        {
            T best = default;
            var maxValue = float.NegativeInfinity;
            foreach (var item in collection)
            {
                var curValue = function(item);
    
                if (curValue <= maxValue)
                {
                    continue;
                }
    
                best = item;
                maxValue = curValue;
            }
    
            return best;
        }
    }
    }
