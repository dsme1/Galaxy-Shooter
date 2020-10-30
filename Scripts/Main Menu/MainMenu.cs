using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadSinglePlayer()
    {
        Debug.Log("Single-Player game loading.");
        SceneManager.LoadScene(1);
    }

    public void LoadMultiplayer()
    {
        Debug.Log("Co-op game loading.");
        SceneManager.LoadScene(2);
    }
}
