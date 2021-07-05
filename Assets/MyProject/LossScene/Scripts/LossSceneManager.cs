using UnityEngine;
using UnityEngine.SceneManagement;

public class LossSceneManager : MonoBehaviour
{

    public void Play()
    {
        SceneManager.LoadScene("Levels");
    }

    public void Main_Menu() {
        SceneManager.LoadScene("Enterance");

    }

}

