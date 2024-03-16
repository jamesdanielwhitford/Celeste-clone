using UnityEngine;
using System.IO;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    public int score; // new variable declared
    public int lastSessionScore; // Last session's score

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        LoadScore();
    }

    [System.Serializable]
    class SaveData
    {
        public int score;
        public int lastSessionScore;
    }

    public void SaveScore()
    {
        SaveData data = new SaveData();
        data.score = score;
        data.lastSessionScore = lastSessionScore;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            score = data.score;
            lastSessionScore = data.lastSessionScore;
        }
    }
}