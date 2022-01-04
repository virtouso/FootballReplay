using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerObject 
{
    
    Vector3 Position { get; }
    void UpdatePosition(Vector3 position);
    void UpdateRotation(Quaternion rotation);
}
