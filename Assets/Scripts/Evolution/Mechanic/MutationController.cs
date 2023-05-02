using System;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using UnityEngine;

public class MutationController : MonoBehaviour
{
    public readonly IBodyPart[] parts = new IBodyPart[Enum.GetValues(typeof(PartsEnum)).Length];
    private readonly List<IBodyPart> _partsIterList = new List<IBodyPart>();
    private readonly GameObject[] _installed = new GameObject[Enum.GetValues(typeof(PartsEnum)).Length];
    
    private Transform _parent;
    private PlayerStateGetter _stateGetter;

    private void Awake()
    {
        var go = gameObject;
        _parent = go.transform;
        _stateGetter = new PlayerStateGetter(go);
        
        MakePartsList();
        ConnectParts(parts);
        
        InstallPart(parts[(int)PartsEnum.BasicAttack]);
    }

    private void FixedUpdate()
    {
        UpdateNeed();
    }

    private void MakePartsList()
    {
        parts[(int)PartsEnum.BasicAttack] = new BasicAttack();
        parts[(int)PartsEnum.FunPropeller] = new FunPropeller();
        parts[(int)PartsEnum.Horn] = new Horn();
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
        foreach (var part in parts)
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
        
        var instance =
            PhotonNetwork.Instantiate(part.Part, _parent.position, _parent.rotation);
        instance.transform.SetParent(_parent);
        
        _installed[(int)part.Index] = instance;
    }

    private void AddParts(IEnumerable<PartsEnum> parts)
    {

        foreach (var part in parts.Where(part => this.parts[(int)part].Active))
        {
            var curPart = this.parts[(int)part];
            curPart.Updating = true;
            _partsIterList.Add(curPart);
        }
    }

    private void RemoveParts(IEnumerable<PartsEnum> parts)
    {
        foreach (var part in parts)
        {
            var curPart = this.parts[(int)part]; 
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
