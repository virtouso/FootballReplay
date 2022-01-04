using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
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

        Time.timeScale = 4;
    }

    private void Update()
    {
        Data.HighlightTime += Time.deltaTime * Data.StepsPerSecond;

        var activeGame = Data.SequenceData;
        var playerTransforms = activeGame.PlayerTransforms;

        var time = Data.HighlightTime;


        float progress = time / Data.SequenceMetaData.TotalSteps;
        progress = Math.Min(1, progress);
        int length = Data.SequenceMetaData.TotalSteps - 1;
        float stepIndexFloat = progress * length;


        BallTransformSystem.Run(ball, stepIndexFloat);
        PlayerTransformSystem.Run(players, stepIndexFloat);
        ActionsSystem.Run(ball, players, stepIndexFloat);
    }
}