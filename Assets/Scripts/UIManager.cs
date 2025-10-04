using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private GameVariables gameVariables;
    [SerializeField] private TMPro.TMP_Text scoreText;
    [SerializeField] private RectTransform livesContainer;

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
        gameVariables.OnScoreChanged += UpdateScoreUI;
        gameVariables.OnLivesChanged += UpdateLivesUI;
    }

    private void UpdateScoreUI(int newScore)
    {
        // Update the score UI element
        scoreText.text = newScore.ToString();
    }

    private void UpdateLivesUI(int newLives)
    {
        // Update the lives UI element
        for (int i = 0; i < livesContainer.childCount; i++)
        {
            livesContainer.GetChild(i).gameObject.SetActive(i < newLives);
        }
    }
}
