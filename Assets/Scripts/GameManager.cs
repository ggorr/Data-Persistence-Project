using System.Collections;
using System.Collections.Generic;
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
            best = new User { name = "", score = 0 };
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetName(string name)
    {
        current = new User { name = name };
    }

    public string GetName()
    {
        return current.name;
    }

    public string GetBestName()
    {
        return best.name;
    }

    public void SetScore(int score)
    {
        current.score = score;
        if (score > best.score)
        {
            best.name = current.name;
            best.score = score;
        }
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

    public int GetScore()
    {
        return current.score;
    }

    public int GetBestScore()
    {
        return best.score;
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
                name = inputField.GetComponent<TMP_InputField>().text;
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
}
