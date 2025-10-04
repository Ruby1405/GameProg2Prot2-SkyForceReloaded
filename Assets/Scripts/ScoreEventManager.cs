using System;
public static class ScoreEventManager
{
    public static event Action<int> OnScoreChanged;

    public static void TriggerScoreChanged(int score)
    {
        OnScoreChanged?.Invoke(score);
    }
}
