using System;
using UnityEngine;

public static class PlayerTransformSystem
{
    public static void Run(IPlayerObject[] players, float floatIndex)
    {
        int step1Index = (int)(floatIndex);
        int step2Index = Math.Min(Data.SequenceMetaData.TotalSteps - 1, step1Index + 1);
        float stepProgress = floatIndex - step1Index;

        for (int i = 0; i < players.Length; i++)
        {
            var transform = Data.SequenceData.PlayerTransforms.Get(step1Index, i);
            var transform2 = Data.SequenceData.PlayerTransforms.Get(step2Index, i);
            var pos1 = transform.Position;
            var pos2 = transform2.Position;
            var dir1 = transform.Direction;
            var dir2 = transform2.Direction;

            Vector4 temp1 = new(pos1.x, pos1.y, dir1.x, dir1.y);
            Vector4 temp2 = new(pos2.x, pos2.y, dir2.x, dir2.y);

            Vector4 temp = Vector4.Lerp(temp1, temp2, stepProgress);

            Vector3 newPos = new(temp.x, players[i].Position.y, temp.y);
            newPos = Vector3.Scale(Data.PlayerScale, newPos);
            Vector3 newDir = new(temp.z, 0, temp.w);
            players[i].UpdatePosition(newPos);
            players[i].UpdateRotation(Quaternion.LookRotation(newDir));
        }
    }
}