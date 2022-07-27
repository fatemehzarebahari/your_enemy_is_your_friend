using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{

    public void OnStart() 
    {
        SceneManager.LoadScene(1);
    }

    public void OnExit() 
    {
        Application.Quit();
    }

    public void LoadMenu() 
    {
        SceneManager.LoadScene(0);
    }

}
