using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private GameObject _enemyContainer;

    [SerializeField]
    private GameObject[] _powerUps;

    private bool _stopSpawning = false;

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());

        StartCoroutine(SpawnPowerUpRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(1.0f);
        while (_stopSpawning == false)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, spawnPos, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        //every 3-7 seconds spawn new powerup
        while (_stopSpawning == false)
        {
            //triple shot
            Vector3 spawnPosTriple = new Vector3(Random.Range(-8, 8), 9, 0);
            Instantiate(_powerUps[0], spawnPosTriple, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));

            //speed boost
            Vector3 spawnPosSpeed = new Vector3(Random.Range(-8, 8), 9, 0);
            Instantiate(_powerUps[1], spawnPosSpeed, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));

            //shield boost
            Vector3 spawnPosShield = new Vector3(Random.Range(-8, 8), 9, 0);
            Instantiate(_powerUps[2], spawnPosShield, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }

    public void PlayerDeath()
    {
        _stopSpawning = true;
    }
}
