using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    //Variables
    [SerializeField]
    private float _speed = 8.0f;

    [SerializeField]
    private float _fireRate = 5f;
    private float _nextFire = 0.0f;
        
    public int _score;

    [SerializeField]
    private int _lives = 3;
    
    //Object Handles
    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _tripleShotPrefab;

    [SerializeField]
    private GameObject _rightEngine;

    [SerializeField]
    private GameObject _leftEngine;

    [SerializeField]
    private GameObject _shieldManager;

    private UIManager _uiManager;

    [SerializeField]
    private SpawnManager _spawnManager;

    [SerializeField]
    private AudioManager _audioManager;

    [SerializeField]
    private GameManager _gameManager;

    [SerializeField]
    private Explosion _explosion;

    //Bool Handlers
    private bool _tripleLaser = false;
    private bool _speedBoost = false;
    private bool _shieldBoost = false;
    public bool _playerOne = false;
    public bool _playerTwo = false;
         
    // Start is called before the first frame update
    void Start()
    {
        //grabs objects and fetches script component from object
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioManager = GameObject.Find("Audio_Manager").GetComponent<AudioManager>();
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if (_gameManager._isCoop == false)
        {
            //take current position = new position (0,-2,0)
            transform.position = new Vector3(0, -2, 0);
        }

        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is NULL");
        }

        if (_uiManager == null)
        {
            Debug.LogError("UI Manager is NULL");
        }

        if (_audioManager == null)
        {
            Debug.LogError("Audio Manager is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerOne == true)
        {
            CalculateMovementPlayerOne();

            //Firing mechanism
            if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFire)
            {
                ShootLaser();
                _audioManager.LaserShot();
            }
        }

        if (_playerTwo == true)
        {
            CalculateMovementPlayerTwo();

            if (Input.GetKeyDown(KeyCode.KeypadEnter) && Time.time > _nextFire)
            {
                ShootLaser();
                _audioManager.LaserShot();
            }
        }
    }

    //Player movement method
    void CalculateMovementPlayerOne()
    {
        //Movement for player
        if (_speedBoost == false)
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector3.up * _speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(Vector3.down * _speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector3.left * _speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.right * _speed * Time.deltaTime);
            }
        }
        else
        {
            _speed = 16.0f;
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector3.up * _speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(Vector3.down * _speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector3.left * _speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.right * _speed * Time.deltaTime);
            }
        }

        //Player restriction
        if (transform.position.y >= 6.2f)
        {
            transform.position = new Vector3(transform.position.x, 6.2f, 0);
        }
        else if (transform.position.y <= -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }

        if (transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.2f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    void CalculateMovementPlayerTwo()
    {
        //Movement for player
        if (_speedBoost == false)
        {
            if (Input.GetKey(KeyCode.Keypad8))
            {
                transform.Translate(Vector3.up * _speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.Keypad5))
            {
                transform.Translate(Vector3.down * _speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.Keypad4))
            {
                transform.Translate(Vector3.left * _speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.Keypad6))
            {
                transform.Translate(Vector3.right * _speed * Time.deltaTime);
            }
        }
        else
        {
            _speed = 16.0f;
            if (Input.GetKey(KeyCode.Keypad8))
            {
                transform.Translate(Vector3.up * _speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.Keypad5))
            {
                transform.Translate(Vector3.down * _speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.Keypad4))
            {
                transform.Translate(Vector3.left * _speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.Keypad6))
            {
                transform.Translate(Vector3.right * _speed * Time.deltaTime);
            }
        }


        //Player restriction
        if (transform.position.y >= 6.2f)
        {
            transform.position = new Vector3(transform.position.x, 6.2f, 0);
        }
        else if (transform.position.y <= -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }

        if (transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.2f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    //Laser Method
    void ShootLaser()
    {
        _nextFire = Time.time + _fireRate;

        if (_tripleLaser == true)   
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }
    }

    //Damage and Lives Methods
    public void Damage()
    {
        //shield boost
        if (_shieldBoost == true)
        {
            _shieldManager.SetActive(false);
            _shieldBoost = false;
            return;
        }
        else
        {
            _lives--;

            if (_lives == 2)
            {
                _rightEngine.SetActive(true);
            }
            else if (_lives == 1)
            {
                _leftEngine.SetActive(true);
            }

            //lives sprite updater
            _uiManager.UpdateLives(_lives);

            //checks if player is dead
            if (_lives < 1)
            {
                Instantiate(_explosion, transform.position, Quaternion.identity);
                _audioManager.ExplosionSound();
                _spawnManager.PlayerDeath();
                _uiManager.CheckForBestScore();
                Destroy(this.gameObject);
            }
        }
    }

    //Powerup Methods
    public void TripleShotActive()
    {
        _audioManager.PowerupSound();
        _tripleLaser = true;
        StartCoroutine(PowerUpCooldown());
    }

    public void SpeedActive()
    {
        _audioManager.PowerupSound();
        _speedBoost = true;
        StartCoroutine(SpeedBoostCooldown());
    }

    public void ShieldActive()
    {
        _audioManager.PowerupSound();
        _shieldBoost = true;
        _shieldManager.SetActive(true);
    }

    IEnumerator PowerUpCooldown()
    {
        yield return new WaitForSeconds(5.0f);
        _tripleLaser = false;
    }

    IEnumerator SpeedBoostCooldown()
    {
        yield return new WaitForSeconds(5.0f);
        _speedBoost = false;
        _speed = 8.0f;
    }

    //UI Methods
    public void ScoreUpdate(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
}
