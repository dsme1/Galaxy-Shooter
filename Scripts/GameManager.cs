using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _gameOver;

    public bool _isCoop = false;

    public void GameOver()
    {
        _gameOver = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _gameOver == true)
        {
            if (_isCoop == false)
            {
                SceneManager.LoadScene(1); //single player
            }
            else
            {
                SceneManager.LoadScene(2); //coop
            }            
        }

        if (Input.GetKeyDown(KeyCode.Escape) && _gameOver == true)
        {
            SceneManager.LoadScene(0); //main menu
        }


        /*if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }*/
    }
}
