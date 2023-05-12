using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MutationController : MonoBehaviour
{
    public readonly IBodyPart[] partsList = new IBodyPart[Enum.GetValues(typeof(PartsEnum)).Length];
    private readonly List<IBodyPart> _partsIterList = new();
    private readonly GameObject[] _installed = new GameObject[Enum.GetValues(typeof(PartsEnum)).Length];
    
    private Transform _parent;
    private PlayerStateGetter _stateGetter;

    private void Awake()
    {
        var go = gameObject;
        _parent = go.transform;
        _stateGetter = new PlayerStateGetter(go);
        
        MakePartsList();
        ConnectParts(partsList);
        
        InstallPart(partsList[(int)PartsEnum.BasicAttack]);
    }

    private void FixedUpdate()
    {
        UpdateNeed();
    }

    private void MakePartsList()
    {
        partsList[(int)PartsEnum.BasicAttack] = new BasicAttack();
        partsList[(int)PartsEnum.FunPropeller] = new FunPropeller();
        partsList[(int)PartsEnum.Horn] = new Horn();
    }

    private void ConnectParts(IEnumerable<IBodyPart> parts)
    {
        foreach (var part in parts)
        {
            part.Getter = _stateGetter;
            
            _partsIterList.Add(part);
        }
    }

    private void UpdateNeed()
    {
        foreach (var part in partsList)
        {
            part.UpdateValue();
        }
    }

    public void Mutation()
    {
        FindBestPart(out IBodyPart bestPart);
        
        InstallPart(bestPart);

        ClearValues();
        
        AddParts(bestPart.Add);
        RemoveParts(bestPart.Remove);
        DestroyParts(bestPart.Destroy);
    }

    private void FindBestPart(out IBodyPart bestPart)
    {
        bestPart = null;
        var partValue = -1f;
        foreach (var part in _partsIterList.Where(part => partValue < part.NeedValue))
        {
            partValue = part.NeedValue;
            bestPart = part;
        }

        if (bestPart == null)
        {
            Debug.LogException(new Exception("Can`t find best part"));
        }
    }

    private void ClearValues()
    {
        foreach (var part in _partsIterList)
        {
            part.ClearValue();
        }
    }

    private void InstallPart(IBodyPart part)
    {
        part.Active = false;
        part.Updating = false;
        _partsIterList.Remove(part);
        
        var instance = (GameObject)Instantiate(Resources.Load(part.Part), _parent, false);

        _installed[(int)part.Index] = instance;
    }

    private void AddParts(IEnumerable<PartsEnum> parts)
    {

        foreach (var part in parts.Where(part => partsList[(int)part].Active))
        {
            var curPart = partsList[(int)part];
            curPart.Updating = true;
            _partsIterList.Add(curPart);
        }
    }

    private void RemoveParts(IEnumerable<PartsEnum> parts)
    {
        foreach (var part in parts)
        {
            var curPart = this.partsList[(int)part]; 
            curPart.Active = false;
            
            if (_partsIterList.Contains(curPart))
            {
                _partsIterList.Remove(curPart);
            }
        }
    }

    private void DestroyParts(IEnumerable<PartsEnum> parts)
    {
        foreach (var part in parts.Where(part => _installed[(int)part] != null))
        {
            Destroy(_installed[(int)part]);
            _installed[(int)part] = null;
            
        }
    }
}
