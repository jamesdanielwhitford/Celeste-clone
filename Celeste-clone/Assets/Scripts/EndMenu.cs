using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class EndMenu : MonoBehaviour
{
    public void Quit()
    {
        if (MainManager.Instance != null)
        {
            // Update the lastSessionScore with the current score
            MainManager.Instance.lastSessionScore = MainManager.Instance.score;

            // Save the scores
            MainManager.Instance.SaveScore();
        }

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}