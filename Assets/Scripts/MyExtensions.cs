using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

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

    public static int Choose(int[] frequencies)
    {
        var maxVal = frequencies.Sum();

        var choose = Random.Range(0, maxVal) + 1;
        var currentVal = 0;
        var index = 0;

        do
        {
            currentVal += frequencies[index];
            index++;
        } while (currentVal < choose);


        return --index;
    }
}