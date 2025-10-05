using System;
using UnityEngine;
public static class HealthEventManager
{
    public static event Action<int> OnHealthChanged;

    public static void TriggerHealthChanged(int health)
    {
        OnHealthChanged?.Invoke(health);
    }
}
