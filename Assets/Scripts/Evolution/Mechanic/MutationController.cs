using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MutationController : MonoBehaviour
{
    private List<IBodyPart> _parts;
    private List<GameObject> _installedParts;
    private Transform _parent;
    private PlayerStateGetter _stateGetter;

    private int _evoPoints;
    private int _evoPointsMax;
    private const int LvlIncrement = 2;

    private void Awake()
    {
        var go = gameObject;
        _parts = new List<IBodyPart>();
        _parent = go.transform;
        _stateGetter = new PlayerStateGetter(go);
        
        MakePartsList();
        ConnectParts(_parts);
    }

    private void FixedUpdate()
    {
        UpdateNeed();
    }

    private void MakePartsList()
    {
        _parts.Add(new FunPropeller());
    }

    private void ConnectParts(IEnumerable<IBodyPart> parts)
    {
        foreach (var part in parts)
        {
            part.Getter = _stateGetter;
        }
    }

    public void AddEvoPoints(int increment)
    {
       _evoPoints += increment;

       if (_evoPoints < _evoPointsMax)
       {
           return;
       }
       
       LvlUp();
       Mutation();
    }

    public void UpdateNeed()
    {
        foreach (var part in _parts)
        {
            part.UpdateValue();
        }
    }

    private void LvlUp()
    {
        _evoPoints -= _evoPointsMax;
        _evoPointsMax *= LvlIncrement;
    }

    private void Mutation()
    {
        FindBestPart(out IBodyPart bestPart);
        
        Instantiate(bestPart.Part, _parent);
        _installedParts.Add(bestPart.Part);

        AddParts(bestPart.Add);
        RemoveParts(bestPart.Remove);
        DestroyParts(bestPart.Destroy);
    }

    private void FindBestPart(out IBodyPart bestPart)
    {
        bestPart = null;
        var partValue = -1f;
        foreach (var part in _parts.Where(part => partValue < part.NeedValue))
        {
            partValue = part.NeedValue;
            bestPart = part;
        }

        if (bestPart == null)
        {
            Debug.LogException(new Exception("Can`t find best part"));
        }
    }

    private void AddParts(IEnumerable<IBodyPart> parts)
    {
        IEnumerable<IBodyPart> bodyParts = parts as IBodyPart[] ?? parts.ToArray();
        
        foreach (var part in bodyParts)
        {
            part.Getter = _stateGetter;
        }
        
        ConnectParts(bodyParts);
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
    
    private void DestroyParts(IEnumerable<IBodyPart> parts)
    {
        foreach (var part in parts.Where(part => _installedParts.Contains(part.Part)))
        {
            Destroy(part.Part);
        }
    }
}
