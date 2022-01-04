using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour,IPlayerObject
{
    public Vector3 Position => transform.position;

    public void UpdatePosition(Vector3 position)
    {
        transform.position = position;
    }

    public void UpdateRotation(Quaternion rotation)
    {
        transform.rotation = rotation;
    }
}
