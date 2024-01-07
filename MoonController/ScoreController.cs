using CI.QuickSave;
using MXZOO;
using MXZOO.Mineral;
using UnityEngine;

public class ScoreController : SingletonMono<ScoreController>
{
    [SerializeField] private int nowScore;
    [SerializeField] private int maxScore;

    private EventBinding<GameNightEvent> gameNightEvent;


    public int NowScore
    {
        get => nowScore;
        set => nowScore = value;
    }

    public int MaxScore
    {
        get => maxScore;
        set => maxScore = value;
    }

    public void Score(MineralBags bag)
    {
        UpdateMaxScore();
        var score = MineralScore.GetMineralScore(bag);
        NowScore = (int)score;

        if (NowScore > MaxScore)
        {
            MaxScore = NowScore;
            SaveMaxScore();
        }
    }

    private void UpdateMaxScore()
    {
        if (!QuickSaveWriter.Create("Moon").Exists(MaxScore.ToString()))
        {
            MaxScore = 0;
            SaveMaxScore();
        }
        else
            QuickSaveReader.Create("Moon")
                .Read<int>(MaxScore.ToString(), r => { MaxScore = r; });
    }

    private void SaveMaxScore()
    {
        QuickSaveWriter.Create("Moon")
            .Write(MaxScore.ToString(), MaxScore)
            .Commit();
    }
}