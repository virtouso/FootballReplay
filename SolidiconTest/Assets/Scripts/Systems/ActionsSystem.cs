using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ActionsSystem 
{
   public static void Run(IBallObject ball,IPlayerObject[] players,  float currentFloatIndex)
   {
      int currentIndex = (int)currentFloatIndex;
      var currentAction = Data.SequenceData.PlayerActions[currentIndex];
      
   }
}
