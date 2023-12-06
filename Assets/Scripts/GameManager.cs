using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [System.Serializable]
    struct User
    {
        public string name;
        public int score;
    }

    public static GameManager Instance;
    private User best;
    private User current;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadBestScore();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        SaveBestScore();
    }

    public void GameOver()
    {
        current.score = 0;
    }

    public void AddScore(int score)
    {
        current.score += score;
        if (current.score > best.score)
        {
            best.name = current.name;
            best.score = current.score;
        }
    }
    public string GetScoreText()
    {
        return $"Score: {current.score} - {current.name}";
    }

    public string GetBestScoreText()
    {
        return $"Best Score: {best.score} - {best.name}";
    }

    public void ResetBestScore()
    {
        best.name = "";
        best.score = 0;
    }

    public void StartClicked()
    {
        string name = null;
        GameObject canvas = GameObject.Find("Canvas");
        for (int i = 0; i < canvas.transform.childCount; i++)
        {
            if (canvas.transform.GetChild(i).tag == "NameInput")
            {
                GameObject inputField = canvas.transform.GetChild(i).gameObject;
                name = inputField.GetComponent<TMP_InputField>().text.Trim();
                if (name == "")
                    return;
                break;
            }
        }
        current = new User
        {
            name = name,
            score = 0
        };
        SceneManager.LoadScene(1);
    }

    private const string saveFile = "best.json";
    public void SaveBestScore()
    {
        string json = JsonUtility.ToJson(best);
        File.WriteAllText(saveFile, json);
    }
    private void LoadBestScore()
    {
        if (File.Exists(saveFile))
        {
            string json = File.ReadAllText(saveFile);
            best = JsonUtility.FromJson<User>(json);
        }
        else
        {
            best = new User { name = "", score = 0 };
        }
    }
}
