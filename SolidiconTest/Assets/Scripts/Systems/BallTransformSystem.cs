using System;
using UnityEngine;

public  class BallTransformSystem :MonoBehaviour
{
    public  void Run(IBallObject ball, float floatIndex)
    {
        int step1Index = (int)(floatIndex);
        int step2Index = Math.Min(Data.SequenceMetaData.TotalSteps - 1, step1Index + 1);
        float stepProgress = floatIndex - step1Index;

        var stepTransform = Data.SequenceData.BallTransforms[step1Index];
        var nextStepTransform = Data.SequenceData.BallTransforms[step2Index];

        var pos1 = stepTransform.Position;
        var pos2 = nextStepTransform.Position;
        Vector3 newPos = Vector3.Lerp(pos1, pos2, stepProgress);
        newPos = Vector3.Scale(Data.PlayerScale, newPos);
        ball.UpdatePosition(newPos);


        var rot1 = stepTransform.Rotation; //Direction;
        var rot2 = nextStepTransform.Rotation;
        Vector3 newDir = Vector3.Lerp(rot1, rot2, stepProgress);
        if (newDir != Vector3.zero)
        {
            ball.UpdateRotation(Quaternion.LookRotation(newDir));
        }
    }
}