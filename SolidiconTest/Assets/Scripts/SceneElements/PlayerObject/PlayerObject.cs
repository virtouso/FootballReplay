using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour,IPlayerObject
{
    [SerializeField] private MeshRenderer _shirtMaterial;
    [SerializeField] private GameGeneralSettings _gameGeneralSettings;
    public Vector3 Position => transform.position;

    private int _teamIndex;
    public int TeamIndex => _teamIndex;

    private int _totalNumber;
    public int TotalNumber => _totalNumber;
    private int _teamNumber;
    public int TeamNumber => _teamNumber;

    public void UpdatePosition(Vector3 position)
    {
        transform.position = position;
    }

    public void UpdateRotation(Quaternion rotation)
    {
        transform.rotation = rotation;
    }

    public void Init(int totalIndex)
    {
        _totalNumber = totalIndex;

        bool firstTeam = totalIndex < 11;

        if (firstTeam)
        {
            _teamIndex = 0;
            _teamNumber = totalIndex;
            _shirtMaterial.material = _gameGeneralSettings.FirstTeamMaterial;
        }
        else
        {
            _teamIndex = 1;
            _teamNumber = totalIndex - 11;
            _shirtMaterial.material = _gameGeneralSettings.SecondTeamMaterial;

        }

        gameObject.name = $"Player{_teamNumber}Team{_teamIndex}Total{_totalNumber}";
    }
}
