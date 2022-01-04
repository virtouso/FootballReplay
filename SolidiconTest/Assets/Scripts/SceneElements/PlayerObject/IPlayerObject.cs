using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerObject 
{
    
    Vector3 Position { get; }
    int TeamIndex { get; }
    int TotalNumber { get; }
    int TeamNumber { get; }
    
    
    void UpdatePosition(Vector3 position);
    void UpdateRotation(Quaternion rotation);


    void Init(int totalIndex);
}
