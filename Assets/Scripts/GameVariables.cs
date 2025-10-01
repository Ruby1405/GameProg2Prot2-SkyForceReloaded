using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GameVariables", menuName = "Scriptable Objects/GameVariables")]
public class GameVariables : ScriptableObject
{
    [SerializeField] private int score = 0;
    public int Score => score;
    [SerializeField] private int lives = 3;
    public int Lives => lives;

    public Action<int> OnScoreChanged;
    public Action<int> OnLivesChanged;

    public void AddScore(int amount)
    {
        score += amount;
        OnScoreChanged?.Invoke(score);
    }
    public void RemoveLife(int amount)
    {
        lives -= amount;
        OnLivesChanged?.Invoke(lives);
    }
}
