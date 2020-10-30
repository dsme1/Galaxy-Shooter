using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 3.0f;

    [SerializeField]
    private GameObject _explosionAnim;

    [SerializeField]
    private SpawnManager _spawnManager;

    [SerializeField]
    private AudioManager _audioManager;

    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is NULL");
        }

        _audioManager = GameObject.Find("Audio_Manager").GetComponent<AudioManager>();
        if (_audioManager == null)
        {
            Debug.LogError("Audio Manager is NULL");
        }
            transform.position = new Vector3(0, 5, 0); 
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward* _rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Laser")
        {
            Instantiate(_explosionAnim, transform.position, Quaternion.identity);
            _spawnManager.StartSpawning();
            Destroy(this.gameObject, 0.25f);
            Destroy(other.gameObject);
            _audioManager.ExplosionSound();
        }
    }
}
