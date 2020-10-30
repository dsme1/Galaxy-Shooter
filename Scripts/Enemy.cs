using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;

    private player _player;

    [SerializeField]
    private Animator _onEnemyDeath;

    [SerializeField]
    private AudioManager _audioManager;

    [SerializeField]
    private GameObject _laserPrefab;

    private float _fireRate = 3.0f;
    private float _canFire = -1f;

    //Update is called before the first frame update
    private void Start()
    {
        _onEnemyDeath = GetComponent<Animator>();
        if (_onEnemyDeath == null)
        {
            Debug.LogError("Anim is null");
        }

        _player = GameObject.Find("Player").GetComponent<player>();
        if (_player == null)
        {
            Debug.LogError("Player is null");
        }

        _audioManager = GameObject.Find("Audio_Manager").GetComponent<AudioManager>();
        if (_audioManager == null)
        {
            Debug.LogError("Audio Manager is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Time.time > _canFire)
        {
            if (this.GetComponent<BoxCollider2D>() != null)
            {
                _fireRate = Random.Range(3.0f, 7.0f);
                _canFire = Time.time + _fireRate;
                GameObject enemyLaser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
                LaserBehaviour[] Laser = enemyLaser.GetComponentsInChildren<LaserBehaviour>();
                
                for (int i = 0; i < Laser.Length; i++)
                {
                    Laser[i].AssignEnemyLaser();
                    
                }
            }

        }
    }

    void CalculateMovement()
    {
        //enemy movement
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        //respawn at top with random x position
        if (transform.position.y < -6f)
        {
            transform.position = new Vector3(Random.Range(-10f, 10f), 7, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        void DestroyThisObj()
        {
            _audioManager.ExplosionSound();
            Destroy(this.GetComponent<BoxCollider2D>());
            Destroy(this.gameObject, 2.8f);
        }

        void DestroyOtherObj()
        {
            Destroy(other.gameObject);
        }

        //destroys enemy
        if (other.gameObject.tag=="Player")
        {
            player player = other.transform.GetComponent<player>();
            
            if (player != null)
            {
                player.Damage();
            }

            _speed = 0;
            _onEnemyDeath.SetTrigger("OnDeathEnemy");
            DestroyThisObj();
        }

        //destroys laser and enemy
        if (other.gameObject.tag=="Laser")
        {
            DestroyOtherObj();

            if (_player != null)
            {
                _player.ScoreUpdate(Random.Range(11, 21));
            }

            _speed = 0;
            _onEnemyDeath.SetTrigger("OnDeathEnemy");
            DestroyThisObj();
        }
    }
}
