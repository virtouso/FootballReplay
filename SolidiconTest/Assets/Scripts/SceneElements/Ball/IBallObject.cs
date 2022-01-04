using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBallObject
{
  void UpdatePosition(Vector3 position);
  void UpdateRotation(Quaternion rotation);
}
