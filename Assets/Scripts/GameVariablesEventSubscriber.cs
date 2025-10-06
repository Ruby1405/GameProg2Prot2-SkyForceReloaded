using UnityEngine;

public class GameVariablesEventSubscriber : MonoBehaviour
{
    public static GameVariablesEventSubscriber Instance;
    [SerializeField] private GameVariables gameVariables;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if (gameVariables == null)
        {
            Debug.LogError("GameVariables is not assigned in GameVariablesEventSubscriber.");
        }
        else
        {
            ScoreEventManager.OnScoreChanged += gameVariables.ChangeScore;
            HealthEventManager.OnHealthChanged += gameVariables.ChangeLife;
            
            #if UNITY_EDITOR
            gameVariables.Reset();
            #endif
        }
    }
}
