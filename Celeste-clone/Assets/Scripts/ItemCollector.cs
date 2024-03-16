using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] Text applesText;
    [SerializeField] AudioSource collectSoundEffect;

    private void Start()
    {
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (MainManager.Instance != null)
        {
            applesText.text = "Apples: " + MainManager.Instance.score;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Apple"))
        {
            Destroy(collision.gameObject);
            collectSoundEffect.Play();

            if (MainManager.Instance != null)
            {
                MainManager.Instance.score++;
                UpdateScoreText();
            }
        }
    }

    private void OnDisable()
    {
        if (MainManager.Instance != null)
        {
            MainManager.Instance.SaveScore();
        }
    }
}