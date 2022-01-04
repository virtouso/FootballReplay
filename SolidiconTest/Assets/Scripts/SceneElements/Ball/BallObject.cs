using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallObject : MonoBehaviour, IBallObject
{
    public void UpdatePosition(Vector3 position)
    {
        transform.position = position;
    }

    public void UpdateRotation(Quaternion rotation)
    {
        transform.rotation = rotation;
    }
}