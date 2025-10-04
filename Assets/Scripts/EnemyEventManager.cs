using System;
using UnityEngine;
public struct EnemyDestroyedEventArgs
{
    public Vector2 position;
    public int score;
}
public static class EnemyEventManager
{
    public static event Action<EnemyDestroyedEventArgs> OnEnemyDestroyed;

    public static void TriggerEnemyDestroyed(EnemyDestroyedEventArgs args)
    {
        OnEnemyDestroyed?.Invoke(args);
    }
}
