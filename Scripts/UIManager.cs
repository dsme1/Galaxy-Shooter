using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    //Variables
    public int bestScore;

    //Object Handles
    [SerializeField]
    private Text _scoreText, bestText;

    [SerializeField]
    private GameObject _restartText;

    [SerializeField]
    private GameObject _gameOverText;

    [SerializeField]
    private GameObject _pauseMenu;

    [SerializeField]
    private Image _livesImg;

    [SerializeField]
    private Sprite[] _livesIcon;

    [SerializeField]
    private GameManager _gameManager;

    private player _player;

    private Animator _pauseAnimator;

    //Bool Handlers
    private bool _pause = false;

    

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<player>();

        _pauseAnimator = GameObject.Find("Pause_Menu_Panel").GetComponent<Animator>();
        if (_pauseAnimator == null)
        {
            Debug.LogError("Pause Animator not found.");
        }

        // Sets the timescale for the pause menu animation to not use standard timescaling.
        _pauseAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;

        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if (_gameManager == null)
        {
            Debug.LogError("GameManager = null.");
        }

        _gameOverText.SetActive(false);
        _scoreText.text = "Score: " + 0;

        bestScore = PlayerPrefs.GetInt("Best", 0);
        bestText.text = "Best: " + bestScore;
    }

    private void Update()
    {
        Pause();
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }

    //check for best score
    // if current score greater than best
    //bestscore = currentscore

    public void CheckForBestScore()
    {
        if (_player._score > bestScore)
        {
            bestScore = _player._score;
            PlayerPrefs.SetInt("Best", bestScore);
            bestText.text = "Best: " + bestScore;
        }
    }

    public void UpdateLives(int CurrentLives)
    {
        _livesImg.sprite = _livesIcon[CurrentLives];

        if (CurrentLives == 0)
        {
            GameOverSequence();
        }
    }

    private void GameOverSequence()
    {
        _gameManager.GameOver();
        _restartText.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
    }

    private IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            _gameOverText.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _gameOverText.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void Pause()
    {
        //Pause Button
        if (Input.GetKeyDown(KeyCode.P) && _pause != true)
        {      
            _pause = true;
            _pauseMenu.SetActive(true);
            _pauseAnimator.SetBool("isPaused", true);
            Time.timeScale = 0;
        }
    }

    public void Unpause()
    {
        if (_pause == true)
        {
            Time.timeScale = 1;
            _pause = false;
            _pauseMenu.SetActive(false);
        }
    }

    public void ReturnMainMenu()
    {
        if (_pause == true)
        {
            SceneManager.LoadScene(0);
            _pause = false;
            Time.timeScale = 1;
        }
    }
}
