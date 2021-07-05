using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Manager : MonoBehaviour
{
    public GameObject Buttons,Menu,timer;


    public void Quit()
    {
        Application.Quit();
    }


    public void RestartLevel() 
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Play() {

        SceneManager.LoadScene("Levels");

    }


    public void HideMenu()
    {
        Time.timeScale = 1;
        Menu.SetActive(false);
        timer.SetActive(true);
        Buttons.SetActive(true);
        

    }

    public void ShowMenu()
    {
        Time.timeScale = 0;       
        Buttons.SetActive(false);
        timer.SetActive(false);
        Menu.SetActive(true);
        

    }

    public void Enterence()
    { 
        SceneManager.LoadScene("Enterance");
    }

    public void Easy()
    {
        SceneManager.LoadScene("Easy");
    }

    public void Normal()
    {
        SceneManager.LoadScene("Normal");
    }

    public void Hard()
    {
        SceneManager.LoadScene("Hard");
    }



}
