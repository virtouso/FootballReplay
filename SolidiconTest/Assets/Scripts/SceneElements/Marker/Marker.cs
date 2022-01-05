using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : MonoBehaviour,IMarker
{

    [SerializeField] private MeshRenderer _renderer;
    public void Init(Material material)
    {
        _renderer.material = material;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
}
