using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class WinSceneManager : MonoBehaviour
{
    public TextMeshProUGUI score;
    public void Play()
    {
        SceneManager.LoadScene("Levels");
    }

    public void Main_Menu() {
        SceneManager.LoadScene("Enterance");

    }

    public void Start()
    {
        SetScore();
    }

    public void SetScore() 
    {
        score.text = "Score : " + DataSaver.timeLeft;
    }

}
