using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
public class Targeting : MonoBehaviour
{
    private const int SightIndex = 0;
    private const float MaxDistance = 100f;

    private Image _sightShow;
    private RectTransform _sightTransform;
    
    private Transform _myTransform;
    private Camera _camera;
    

    public void Init(Transform canvas)
    {
        _myTransform = transform;
        _camera = Camera.main;

        var sight = canvas.GetChild(SightIndex).gameObject;
        _sightTransform = sight.GetComponent<RectTransform>();
        _sightShow = sight.GetComponent<Image>();
    }

    private void FixedUpdate()
    {
        const int layerMask = 1;
        var isHit = Physics.Raycast(_myTransform.position, _myTransform.forward, out var hit,
            MaxDistance, layerMask, QueryTriggerInteraction.Ignore);
        
        _sightShow.enabled = isHit;

        _sightTransform.position = _camera.WorldToScreenPoint(hit.point);
    }
}
