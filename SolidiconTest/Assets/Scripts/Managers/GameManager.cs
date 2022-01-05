using UnityEngine;
using System;
using Zenject;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float _simulationSpeed;
    [SerializeField] private ActionsSystem _actionSystem;
    [SerializeField] private BallTransformSystem _ballTransformSystem;
    [SerializeField] private PlayerTransformSystem _playerTransformSystem;
    [SerializeField] private PlayerObject playerPrefab;
    [SerializeField] private BallObject ballPrefab;

    private IBallObject ball;
    private IPlayerObject[] players;

    void Start()
    {
        Data.Init();
        var bytesMetaData = Resources.Load<TextAsset>("4239236_highlight_CornerTopRight_4_meta").bytes;
        var metaData = ByteConverter.GetStruct<ReplaySequenceMetaData>(bytesMetaData);
        var bytes = Resources.Load<TextAsset>("4239236_highlight_CornerTopRight_4").bytes;
        Data.SequenceData = ByteConverter.GetSequenceData(bytes, metaData.TotalSteps);
        Data.SequenceMetaData = metaData;
        ball = Instantiate(ballPrefab);
        players = new IPlayerObject[Data.TotalPlayers];
        for (int i = 0; i < players.Length; i++)
        {
            players[i] = Instantiate(playerPrefab);
            players[i].Init(i);
        }

        Time.timeScale = _simulationSpeed;
        _actionSystem.Init(players);

       //because some data is not recorded properly
        Data.SequenceData.BallOwners[0].BallOwnedPlayer = 10;
        Data.SequenceData.BallOwners[0].BallOwnedTeam = 0;
    }

    private void Update()
    {
        //did some refactor to avoid repetitive codes is systems
        Data.HighlightTime += Time.deltaTime * Data.StepsPerSecond;
        var activeGame = Data.SequenceData;
        var time = Data.HighlightTime;
        float progress = time / Data.SequenceMetaData.TotalSteps;
        progress = Math.Min(1, progress);
        int length = Data.SequenceMetaData.TotalSteps - 1;
        float stepIndexFloat = progress * length;

        //systems function
        _ballTransformSystem.Run(ball, stepIndexFloat);
        _playerTransformSystem.Run(players, stepIndexFloat);
        _actionSystem.Run(stepIndexFloat, ball);
    }
}