using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Game Settings", menuName = "Configurations/GameSettings")]
public class GameGeneralSettings : ScriptableObject
{

    [SerializeField] public Material FirstTeamMaterial;
    [SerializeField] public Material SecondTeamMaterial;
}
