using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private Text lastScoreText;

    private void Start()
    {
        UpdateLastScoreText();
    }

    private void UpdateLastScoreText()
    {
        if (MainManager.Instance != null)
        {
            MainManager.Instance.LoadScore();

            lastScoreText.text = "Last Score: " + MainManager.Instance.lastSessionScore;
        }
        else
        {
            lastScoreText.text = "Last Score: " + "test";

        }
    }

    public void StartGame()
    {
        if (MainManager.Instance != null)
        {
            // Save the lastSessionScore before resetting the score
            MainManager.Instance.lastSessionScore = MainManager.Instance.score;

            // Reset the current score to 0 when starting a new game
            MainManager.Instance.score = 0;

            MainManager.Instance.SaveScore(); // Save the updated score values
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}