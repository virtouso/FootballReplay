using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsSystem : MonoBehaviour
{
    [SerializeField] private GameGeneralSettings _gameGeneralSettings;
    [SerializeField] private Marker _markerPrefab;
    [SerializeField] private int _numberOfFutureRecordsToCheck;
    [SerializeField] private float _distanceToCheckPrediction;
    private List<IPlayerObject> _team1;
    private List<IPlayerObject> _team2;
    private IPlayerObject[] _allPlayerObjects;
    private IMarker _team1Marker;

    private IMarker _team2Marker;
    //  private IMarker _ownerMarker;

    public void Init(IPlayerObject[] allPlayerObjects)
    {
        _allPlayerObjects = allPlayerObjects;
        _team1 = new List<IPlayerObject>(11);
        _team2 = new List<IPlayerObject>(11);
        for (int i = 0; i < allPlayerObjects.Length; i++)
        {
            if (i < 11)
                _team1.Add(allPlayerObjects[i]);
            else
                _team2.Add(allPlayerObjects[i]);
        }

        _team1Marker = Instantiate(_markerPrefab);
        _team1Marker.Init(_gameGeneralSettings.FirstTeamMaterial);


        _team2Marker = Instantiate(_markerPrefab);
        _team2Marker.Init(_gameGeneralSettings.SecondTeamMaterial);

        // _ownerMarker = Instantiate(_markerPrefab);
        // _ownerMarker.Init(_gameGeneralSettings.OwnerMaterial);

        _currentTeam1ActivePlayer = Data.SequenceData.PlayerActions[0].LeftTeamAction;
        _currentTeam2ActivePlayer = Data.SequenceData.PlayerActions[0].RightTeamAction;
    }


    public void Run(float currentFloatIndex, IBallObject ball)
    {
        int currentIndex = (int)currentFloatIndex;
        var currentAction = Data.SequenceData.PlayerActions[currentIndex];

        SetMarker(_team1Marker, _team1, currentAction.LeftTeamActivePlayerIndex);
        SetMarker(_team2Marker, _team2, currentAction.RightTeamActivePlayerIndex);
        //  SetOwnerMarker(_ownerMarker, currentIndex);
        PredictFuturePositionIfChanged(currentIndex, ball.Position);
    }


    private void SetMarker(IMarker marker, List<IPlayerObject> teamPlayer, int currentIndex)
    {
        marker.SetPosition(teamPlayer[currentIndex].Position + Vector3.up * 5);
    }

    // private void SetOwnerMarker(IMarker marker, int currentIndex)
    // {
    //     Debug.Log(Data.SequenceData.BallOwners.Length);
    //     marker.SetPosition(_allPlayerObjects[Data.SequenceData.BallOwners[currentIndex].BallOwnedPlayer].Position +
    //                        Vector3.up * 5);
    // }


    private int _currentTeam1ActivePlayer;

    private int _currentTeam2ActivePlayer;

// it would be much simple if had ball owner data
    private void PredictFuturePositionIfChanged(int currentRecordIndex, Vector3 ballPosition)
    {
        var currentAction = Data.SequenceData.PlayerActions[currentRecordIndex];
        bool team1MarkerChanged = _currentTeam1ActivePlayer != currentAction.LeftTeamActivePlayerIndex;
        bool team2MarkerChanged = _currentTeam2ActivePlayer != currentAction.RightTeamActivePlayerIndex;

        _currentTeam1ActivePlayer = currentAction.LeftTeamActivePlayerIndex;
        _currentTeam2ActivePlayer = currentAction.RightTeamActivePlayerIndex;


        if (!(team1MarkerChanged || team2MarkerChanged))
            return;

        print("marker changed");
        for (int i = currentRecordIndex; i < currentRecordIndex + _numberOfFutureRecordsToCheck; i++)
        {
            if (i >= Data.SequenceMetaData.TotalSteps - 1)
                break;

            int cachedTeam1ActivePlayer = Data.SequenceData.PlayerActions[i].LeftTeamActivePlayerIndex;
            int cachedTeam2ActivePlayer = Data.SequenceData.PlayerActions[i].RightTeamActivePlayerIndex;

            Vector3 currentBallPosition = Data.SequenceData.BallTransforms[i].Position;
            currentBallPosition = Vector3.Scale(Data.PlayerScale, currentBallPosition);

            Vector3 currentTeam1Position = (Vector3)Data.SequenceData.PlayerTransforms
                .Get(i, cachedTeam1ActivePlayer).Position;
            currentTeam1Position = Vector3.Scale(Data.PlayerScale, YtoZ(currentTeam1Position));

            Vector3 currentTeam2Position = (Vector3)Data.SequenceData.PlayerTransforms
                .Get(i, cachedTeam2ActivePlayer + 11).Position;
            currentTeam2Position = Vector3.Scale(Data.PlayerScale, YtoZ(currentTeam2Position));

            bool player1GotIt =
                Vector3.SqrMagnitude(currentTeam1Position - currentBallPosition) <=
                Mathf.Pow(_distanceToCheckPrediction, 2);


            bool player2GotIt =
                Vector3.SqrMagnitude(currentTeam2Position - currentBallPosition) <=
                Mathf.Pow(_distanceToCheckPrediction, 2);

            if (player1GotIt)
            {
                print("red");
                Debug.DrawLine(ballPosition, currentTeam1Position, Color.red, 2);
                break;
            }

            if (player2GotIt)
            {
                print("blue");
                Debug.DrawLine(ballPosition, currentTeam2Position, Color.blue, 2);
                break;
            }
        }
    }

    private Vector3 YtoZ(Vector3 input)
    {
        return new Vector3(input.x, input.z, input.y);
    }
}