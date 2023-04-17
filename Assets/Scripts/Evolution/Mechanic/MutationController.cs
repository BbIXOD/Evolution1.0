using System.Collections.Generic;
using UnityEngine;

public class MutationController
{
    private List<IBodyPart> _parts;
    private Transform _parent;

    private int _evoPoints;
    private int _evoPointsMax;
    private const int _lvlIncrement = 2;

    public MutationController(IEnumerable<IBodyPart> parts, Transform parent)
    {
        _parts = (List<IBodyPart>)parts;
        _parent = parent;
    }

    private void AddEvoPoints(int increment)
    {
       _evoPoints += increment;

       if (_evoPoints < _evoPointsMax)
       {
           return;
       }
       
       LvlUp();
       Mutation();
    }

    private void LvlUp()
    {
        _evoPoints -= _evoPointsMax;
        _evoPointsMax *= _lvlIncrement;
    }

    private void Mutation()
    {
        IBodyPart bestPart = null;
        var partValue = -1f;
        foreach (var part in _parts)
        {
            if (partValue > part.NeedValue)
            {
                return;
            }

            partValue = part.NeedValue;
            bestPart = part;
        }

        if (bestPart == null)
        {
            Debug.LogWarning("Can`t find best part");
            return;
        }

        AddParts(bestPart.Add);
        RemoveParts(bestPart.Remove);

        Object.Instantiate(bestPart.Part, _parent);
    }

    private void AddParts(IEnumerable<IBodyPart> parts)
    {
        foreach (var part in parts)
        {
            _parts.Add(part);
        }
    }

    private void RemoveParts(IEnumerable<IBodyPart> parts)
    {
        foreach (var part in parts)
        {
            if (_parts.Contains(part))
            {
                _parts.Remove(part);
            }
        }
    }
}
