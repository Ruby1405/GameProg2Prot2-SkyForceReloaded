using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private GameVariables gameVariables;
    [SerializeField] private TMPro.TMP_Text scoreText;
    [SerializeField] private RectTransform livesContainer;
    [SerializeField] private GameObject gameOverScreen;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        gameVariables.OnScoreChanged += UpdateScoreUI;
        gameVariables.OnLivesChanged += UpdateLivesUI;
    }

    private void OnDestroy()
    {
        if (gameVariables != null)
        {
            gameVariables.OnScoreChanged -= UpdateScoreUI;
            gameVariables.OnLivesChanged -= UpdateLivesUI;
        }
    }

    private void UpdateScoreUI(int newScore)
    {
        // Update the score UI element
        scoreText.text = newScore.ToString();
    }

    private void UpdateLivesUI(int newLives)
    {
        // Update the lives UI element
        if (livesContainer == null)
        {
            Debug.LogWarning("Lives container is not assigned in UIManager.");
            return;
        }
        for (int i = 0; i < livesContainer.childCount; i++)
            {
                livesContainer.GetChild(i).gameObject.SetActive(i < newLives);
            }
        if (newLives <= 0)
        {
            gameOverScreen.SetActive(true);
        }
    }
}
